using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCUIManager : MonoBehaviour
{
    public static NPCUIManager Instance;

    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private TextMeshProUGUI npcText;
    [SerializeField] private Image npcImage;

    [SerializeField] private string[] npcNames;
    [SerializeField] private Sprite[] npcFace;
    private string[] npcTalk= { "최악 중의 최악이네요.\n (피자를 던져 버리며) 쓰레기를 왜 주시죠? 줘도 안 먹습니다. \n 따뜻한 과일은 죄악입니다." ,
        "절대 돈 주고는 안 사먹을 피자네요.\n 공짜로 주신다면 먹기야 하겠습니다. 개꿀~" ,
        "(피자를 던져 버리며) 오 파인애플 피자라니..! 싸우자는 건가요?"};
    [SerializeField] private Sprite WSImage;
    public GameObject Panel;
    private int idx = 0;

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
        Panel.SetActive(false);
    }

    public void MeetFakeWS()
    {
        npcImage.sprite = npcFace[idx];
        npcName.text = npcNames[idx];
        npcText.text = npcTalk[idx++];
        idx %= 3;
        GameManager.Instance.havePizza = false;
        Panel.SetActive(true);
        StartCoroutine("ReleaseObject",2f);
    }

    public void MeetWS()
    {
        npcImage.sprite = WSImage;
        npcName.text = "신우석 매니저님";
        npcText.text = "오 파인애플 피자라니..! 배우신 분이군요?\n(피자를 허겁지겁 먹으며) 자고로 파인애플은 구워서 먹는 과일입니다~ \n 여러분들도 다들 파인애플 피자 드세요~~";
        GameManager.Instance.havePizza = false;
        Panel.SetActive(true);
        GameManager.Instance.curScore += 100; //클리어시 100점 추가
        StartCoroutine("ReleaseObject", 2f);
    }

    IEnumerator ReleaseObject(float time)
    {
        yield return new WaitForSeconds(time);
        Panel.SetActive(false);

        if (npcName.text == "신우석 매니저님")
            SceneManager.LoadScene("EndingScene");
    }
}
