using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Image progressBar;

    private void Start()
    {
        if (!string.IsNullOrEmpty(nextScene))
        {
            StartCoroutine(LoadScene());
        }
    }

    public static void LoadScene(string name)
    {
        nextScene = name;
        SceneManager.LoadScene("LoadScene");
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.1f);

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        float minLoadTime = 1.0f;  // 최소 로딩 시간

        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            // 최소 로딩 시간 동안 진행 바를 일정하게 증가
            if (timer < minLoadTime)
            {
                progressBar.fillAmount = timer / minLoadTime;
            }
            else if (op.progress >= 0.9f)
            {
                progressBar.fillAmount = 1f;
                op.allowSceneActivation = true;
            }
        }
    }
}

