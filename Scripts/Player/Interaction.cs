using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObj;
    private IInteractable curInteractable;
    public TextMeshProUGUI promptText;
    private Collider playerCollider;

    private bool isTutorialActive = false;
    private Transform video; // 튜토리얼 영상

    void Start()
    {
        playerCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            ShootRay();
        }

        // E키 입력 감지
        if (isTutorialActive && Input.GetKeyDown(KeyCode.E))
        {
            ToggleVideo(); // 영상 활성화/비활성화 토글
        }

    }

    private void SetPromptText()
    {
        if (promptText != null && curInteractable != null)
        {
            promptText.gameObject.SetActive(true);
            promptText.text = curInteractable.GetInteractPrompt();
        }
    }

    private void SetTutorialText()
    {
        // E 키를 누르면 튜토리얼 영상 보기와 같은 상호작용 
        promptText.gameObject.SetActive(true);
        promptText.text = "[E]키를 눌러 튜토리얼 영상 \n확인하기!";
    }

    void ShootRay()
    {
        RaycastHit hit;

        // 플레이어 콜라이더의 중심에서 레이 발사
        Vector3 playerCenter = playerCollider.bounds.center;
        Ray ray = new Ray(playerCenter, transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * maxCheckDistance, Color.red, checkRate); // 레이 시각화

        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
        {
            if (hit.collider.gameObject != curInteractGameObj)
            {
                curInteractGameObj = hit.collider.gameObject;
                curInteractable = hit.collider.gameObject.GetComponent<IInteractable>();

                // layer 10 -> Interactable
                if (curInteractGameObj.layer == 10)
                {
                    if (curInteractable != null)
                    {
                        SetPromptText();
                    }
                    else
                    {
                        curInteractGameObj = null;
                        promptText.gameObject.SetActive(false);
                    }
                }
                // layer 15 -> Tutorial
                else if (curInteractGameObj.layer == 15)
                {
                    if (curInteractable != null)
                    {
                        isTutorialActive = true;
                        SetTutorialText();
                        video = curInteractGameObj.transform.Find("Video"); // 영상 Transform 찾기
                    }
                }
            }
        }
        else
        {
            isTutorialActive = false;
            curInteractGameObj = null;
            curInteractable = null;
            if (promptText != null)
            {
                promptText.gameObject.SetActive(false);
            }
        }
    }

    void ToggleVideo()
    {
        if (video != null)
        {
            if (video.gameObject.activeSelf) // 초기는 false인 상태
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f; 
            }
            video.gameObject.SetActive(!video.gameObject.activeSelf); // 영상 활성화/비활성화 토글
        }
    }
}
