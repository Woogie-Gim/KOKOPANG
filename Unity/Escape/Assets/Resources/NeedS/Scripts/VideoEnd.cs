using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEnd : MonoBehaviour
{
    public VideoPlayer videoPlayer; // ���� �÷��̾� ������Ʈ ����
    public GameObject Loading;
    public string nextSceneName = "Loading"; // ���� ���� �̸�

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>(); // ���� �÷��̾� ������Ʈ�� �ڵ����� ã�ƿɴϴ�.
        }

        videoPlayer.loopPointReached += EndReached; // ���� ����� ������ �� ȣ��� �ݹ� �Լ��� ����մϴ�.
    }

    // ���� ����� ������ �� ȣ��� �ݹ� �Լ�
    void EndReached(VideoPlayer vp)
    {
        //SceneManager.LoadScene(nextSceneName); // ������ ������ �̵��մϴ�.
        Loading.SetActive(true);
        gameObject.SetActive(false);
    }
}
