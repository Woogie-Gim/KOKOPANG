using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileImage : MonoBehaviour
{

    public Image image;

    //�̹��� ������ ���
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
            // �ؽ�ó�� �����ϰ� �б�
            Texture2D texture = new Texture2D(2, 2);
            byte[] fileData = System.IO.File.ReadAllBytes(imagePath);
            texture.LoadImage(fileData);

            // �ؽ�ó�� ��������Ʈ�� ��ȯ�Ͽ� UI �̹����� ǥ��
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            image.sprite = sprite;
        }
        else
        {
            Debug.LogError("��ο� �̹����� �����ϴ�. " + imagePath);
        }
    }
}
