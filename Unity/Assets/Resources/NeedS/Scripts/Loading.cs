using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Image progressBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        //SceneManager.LoadScene("Loading");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    private void OnEnable()
    {
        LoadScene("MainScene");
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                if (timer > 1f) // 로딩 완료 후 1초 동안 대기
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }


    //IEnumerator LoadSceneProcess()
    //{
    //    AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
    //    op.allowSceneActivation = false;

    //    float timer = 0f;
    //    while (!op.isDone)
    //    {
    //        yield return null;
    //        if(op.progress<0.9f)
    //        {
    //            progressBar.fillAmount = op.progress;
    //        }
    //        else
    //        {
    //            timer += Time.unscaledDeltaTime;
    //            progressBar.fillAmount = Mathf.Lerp(0.9f,1f,timer);
    //            if(progressBar.fillAmount >= 1f )
    //            {
    //                op.allowSceneActivation = true;
    //                yield break;

    //            }
    //        }
    //    }
    //}

}
