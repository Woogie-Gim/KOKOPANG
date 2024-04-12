using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTutorial : MonoBehaviour
{
    public GameObject canvasToPopup; // �˾��� ĵ����
    public Button Openbutton;
    public Button Closebutton;

    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ ����
        Debug.Log("�̰� ���Ծ��Ӹ�");
        Openbutton.onClick.AddListener(PopupCanvas);
        Closebutton.onClick.AddListener(CloseCanvas);

    }

    // �˾��� ĵ������ Ȱ��ȭ
    public void PopupCanvas()
    {
        if (canvasToPopup != null)
        {
            Debug.Log("�Ӹ�");
            canvasToPopup.SetActive(true);
        }
    }
    public void CloseCanvas()
    {
        if (canvasToPopup != null)
        {
            Debug.Log("�ȵ��� �Ӹ�");
            canvasToPopup.SetActive(false);
        }
    }

}
