using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEnd : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 비디오 플레이어 컴포넌트 참조
    public GameObject Loading;
    public string nextSceneName = "Loading"; // 다음 씬의 이름

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>(); // 비디오 플레이어 컴포넌트를 자동으로 찾아옵니다.
        }

        videoPlayer.loopPointReached += EndReached; // 비디오 재생이 끝났을 때 호출될 콜백 함수를 등록합니다.
    }

    // 비디오 재생이 끝났을 때 호출될 콜백 함수
    void EndReached(VideoPlayer vp)
    {
        //SceneManager.LoadScene(nextSceneName); // 지정된 씬으로 이동합니다.
        Loading.SetActive(true);
        gameObject.SetActive(false);
    }
}
