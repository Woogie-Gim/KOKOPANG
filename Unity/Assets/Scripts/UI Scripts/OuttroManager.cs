using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OuttroManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;

        TCPConnectManager.Instance.uploadScore();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        #region ¾À ³Ñ±â±â
        SceneManager.LoadScene("Result");
        ResultManager.isResultManager = true;
        #endregion
    }
}
