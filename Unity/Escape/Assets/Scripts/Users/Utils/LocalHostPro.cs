using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LocalHostPro : MonoBehaviour
{
    public Image image;

    public RawImage img;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTexture(image)); // image 변수를 전달합니다.
    }

    IEnumerator GetTexture(Image image) // 매개변수를 Image로 변경합니다.
    {
        var url = $"https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjA0MDhfNTQg%2FMDAxNjQ5NDIxNDAwMjg5.Q59-vSP8gl_NqnU8cg4n2mivrCXlCE0tkGfJyCFlGYkg.uUTeg0247Y4FWj32ejDgYj4PoDAgPOxWEb8j8B6pEOIg.JPEG.pinkgirl1639%2Fcu1649336552218.JPEG&type=sc960_832";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            image.sprite = Sprite.Create(((DownloadHandlerTexture)www.downloadHandler).texture, new Rect(0, 0, ((DownloadHandlerTexture)www.downloadHandler).texture.width, ((DownloadHandlerTexture)www.downloadHandler).texture.height), Vector2.zero);
        }
    }
}
