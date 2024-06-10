using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI curScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    private void Start()
    {
        curScoreText.text = "Current Score\n" + Mathf.Round(GameManager.Instance.curScore);
        bestScoreText.text = "Best Score\n" + Mathf.Round(PlayerPrefs.GetFloat(GameManager.Instance.bestScoreKey));
        StartCoroutine("SceneChange", 6f);
    }

    IEnumerator SceneChange(float time)
    {
       
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("StartScene");
    }
}
