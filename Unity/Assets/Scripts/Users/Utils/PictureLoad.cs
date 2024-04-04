using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureLoad : MonoBehaviour
{
    public Image image;

    // 이미지 파일의 이름 (확장자 없이)
    public string imageName = "Picture1";

    void Start()
    {
        LoadImage();
    }

    void LoadImage() 
    {
        // Resources 폴더에서 이미지를 로드하여 Image 컴포넌트에 할당
        Sprite sprite = Resources.Load<Sprite>("Images/" + imageName);

        if (sprite != null)
        {
            // 이미지가 로드되었다면 Image 컴포넌트에 할당
            image.sprite = sprite;
        }
        else
        {
            // 이미지를 찾을 수 없는 경우 경고 출력
            Debug.LogWarning("이미지를 찾을 수 없습니다: " + imageName);
        }
    }
}
