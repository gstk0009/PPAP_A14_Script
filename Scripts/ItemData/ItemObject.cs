using System.Collections;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData itemData;

    private Player playerObject;

    private AudioSource itemSound;
    private void Start()
    {
        itemSound = GetComponent<AudioSource>();
        playerObject = CharacterManager.Instance.Player;

        if (CharacterManager.Instance.Player.sprintParticle != null)
            CharacterManager.Instance.Player.sprintParticle.SetActive(false);

        if (CharacterManager.Instance.Player.shieldParticle != null)
            CharacterManager.Instance.Player.shieldParticle.SetActive(false);
    }
    public string GetInteractPrompt() // UI에 아이템 설명 출력
    {
        return $"{itemData.displayName}\n{itemData.description}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!playerObject.controller.isDead && other.gameObject.layer == 6 && gameObject.layer == 10)
        {
            itemSound.Play();
            StartCoroutine(ItemTriggerInteraction());
        }
    }

    private IEnumerator ItemTriggerInteraction()
    {
        yield return new WaitForSeconds(.3f);
        GameManager.Instance.curScore += itemData.itemScore;

        gameObject.SetActive(false);
        UIManager.Instance.SetItemGauge(itemData);

        GameManager.Instance.itemSpawner.spawnPositions.Add(gameObject.transform.position);

        for (int i = 0; i < itemData.passiveTypes.Length; i++)
        {
            if (itemData.passiveTypes[i].type == PassiveType.Sprint)
            {
                if (CharacterManager.Instance.Player.sprintParticle != null)
                    CharacterManager.Instance.Player.sprintParticle.SetActive(true);

                //최대로 증가할 수 있는 스피드는 15로 제한
                if (playerObject.controller.moveSpeed <= 15)
                {
                    UIManager.Instance.OnItemGaugeFinish += SetPlayerMoveSpeedInit;
                    playerObject.controller.moveSpeed += 5f;
                }
            }

            if (itemData.passiveTypes[i].type == PassiveType.Shield)
            {
                if (CharacterManager.Instance.Player.shieldParticle != null)
                {
                    CharacterManager.Instance.Player.shieldParticle.SetActive(true);
                    playerObject.controller.isInvincible = true;
                }

                UIManager.Instance.OnItemGaugeFinish += SetPlayerInvincibilityInit;
            }

            //Star를 제외한 아이템들은 충돌 후 일정한 간격(15f)을 두고 아이템을 다시 활성화 함
            if(itemData.passiveTypes[i].type != PassiveType.None)
            {
                GameManager.Instance.itemSpawner.RespawnItem(gameObject);

            }
        }
    }

    private void SetPlayerMoveSpeedInit(int idx)
    {
        if (idx == (int)PassiveType.Sprint)
        {
            playerObject.controller.moveSpeed -= 5f;

            if (CharacterManager.Instance.Player.sprintParticle != null)
                CharacterManager.Instance.Player.sprintParticle.SetActive(false);

            UIManager.Instance.OnItemGaugeFinish -= SetPlayerMoveSpeedInit;
        }
    }

    private void SetPlayerInvincibilityInit(int idx)
    {
        if (idx == (int)PassiveType.Shield)
        {
            playerObject.controller.isInvincible = false;

            if (CharacterManager.Instance.Player.shieldParticle != null)
                CharacterManager.Instance.Player.shieldParticle.SetActive(false);

            UIManager.Instance.OnItemGaugeFinish -= SetPlayerInvincibilityInit;
        }
    }
}
