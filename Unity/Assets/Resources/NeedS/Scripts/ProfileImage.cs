using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileImage : MonoBehaviour
{

    public Image image;

    //이미지 파일의 경로
    public string imagePath = "Picture1";


    // Start is called before the first frame update
    void Start()
    {
        LoadImage();
    }

    // Update is called once per frame
    void LoadImage()
    {
        if (System.IO.File.Exists(imagePath))
        {
            // 텍스처를 생성하고 읽기
            Texture2D texture = new Texture2D(2, 2);
            byte[] fileData = System.IO.File.ReadAllBytes(imagePath);
            texture.LoadImage(fileData);

            // 텍스처를 스프라이트로 변환하여 UI 이미지에 표시
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            image.sprite = sprite;
        }
        else
        {
            Debug.LogError("경로에 이미지가 없습니다. " + imagePath);
        }
    }
}
