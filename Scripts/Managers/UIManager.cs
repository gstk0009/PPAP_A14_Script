using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI curTimeText;
    [SerializeField] private TextMeshProUGUI curScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private Image[] itemGauges;
    public GameObject EndPanel;

    public event Action<int> OnItemGaugeFinish;

    //키 값을 통해 동일한 코루틴이 실행중인지를 판단하기 위한 Dictionary
    private Dictionary<int, Coroutine> activeCoroutines = new Dictionary<int, Coroutine>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        EndPanel.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            curTimeText.text = "Score : " + Mathf.Round(GameManager.Instance.curScore);
        }
    }

    public void SetEndPanel()
    {
        curScoreText.text = "Current Score\n" + Mathf.Round(GameManager.Instance.curScore);
        bestScoreText.text = "Best Score\n" + Mathf.Round(PlayerPrefs.GetFloat(GameManager.Instance.bestScoreKey));
        EndPanel.SetActive(true);
    }

    public void SetItemGauge(ItemData itemData)
    {
        for (int i = 0; i < itemData.passiveTypes.Length; i++)
        {
            int idx = (int)itemData.passiveTypes[i].type;

            if (itemData.passiveTypes[i].type != PassiveType.None)
            {
                //현재 passiveTypes[i].type(Sprint 혹은 Shield)과 동일한 활성화된 코루틴이 있다면, 
                if (activeCoroutines.ContainsKey(idx) && activeCoroutines[idx] != null)
                {
                    //해당 코루틴을 중단
                    StopCoroutine(activeCoroutines[idx]);
                }

                itemGauges[idx].enabled = true;
                activeCoroutines[idx] = StartCoroutine(ItemGaugeCoroutine(itemData.passiveTypes[i].value, idx));
            }
        }
    }

    //각 아이템의 지속 시간에 따라 게이지가 줄어들도록
    public IEnumerator ItemGaugeCoroutine(float value, int idx)
    {
        
        if (itemGauges[idx] != null)
        {
            itemGauges[idx].fillAmount = 1f;
            float timePassed = 0f;

            while (timePassed < value)
            {
                if (CharacterManager.Instance.Player.controller.isDead)
                {
                    itemGauges[idx].fillAmount = 0f;
                    itemGauges[idx].enabled = false;

                    OnItemGaugeFinish?.Invoke(idx);
                }

                timePassed += Time.deltaTime;
                itemGauges[idx].fillAmount = Mathf.Clamp(1f - (timePassed / value), 0f, 1f);

                yield return null;
            }
        }
        itemGauges[idx].fillAmount = 0f;
        itemGauges[idx].enabled = false;

        OnItemGaugeFinish?.Invoke(idx);
    }
}
