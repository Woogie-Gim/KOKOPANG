using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureLoad : MonoBehaviour
{
    public Image image;

    // �̹��� ������ �̸� (Ȯ���� ����)
    public string imageName = "Picture1";

    void Start()
    {
        LoadImage();
    }

    void LoadImage() 
    {
        // Resources �������� �̹����� �ε��Ͽ� Image ������Ʈ�� �Ҵ�
        Sprite sprite = Resources.Load<Sprite>("Images/" + imageName);

        if (sprite != null)
        {
            // �̹����� �ε�Ǿ��ٸ� Image ������Ʈ�� �Ҵ�
            image.sprite = sprite;
        }
        else
        {
            // �̹����� ã�� �� ���� ��� ��� ���
            Debug.LogWarning("�̹����� ã�� �� �����ϴ�: " + imageName);
        }
    }
}
