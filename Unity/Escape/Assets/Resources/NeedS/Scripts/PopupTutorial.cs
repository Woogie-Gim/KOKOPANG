using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTutorial : MonoBehaviour
{
    public GameObject canvasToPopup; // 팝업할 캔버스
    public Button Openbutton;
    public Button Closebutton;

    void Start()
    {
        // 버튼 클릭 이벤트 연결
        Debug.Log("이건 들어왔어임마");
        Openbutton.onClick.AddListener(PopupCanvas);
        Closebutton.onClick.AddListener(CloseCanvas);

    }

    // 팝업할 캔버스를 활성화
    public void PopupCanvas()
    {
        if (canvasToPopup != null)
        {
            Debug.Log("임마");
            canvasToPopup.SetActive(true);
        }
    }
    public void CloseCanvas()
    {
        if (canvasToPopup != null)
        {
            Debug.Log("안들어와 임마");
            canvasToPopup.SetActive(false);
        }
    }

}
