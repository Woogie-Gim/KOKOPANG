using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static ManualList;

public class TutorialControl : MonoBehaviour
{
    public GameObject itemImageUp;
    public GameObject itemImageDown;
    public GameObject interImageUp;
    public GameObject interImageDown;

    public GameObject itemCanvas;
    public GameObject interfaceCanvas;
    public GameObject purposeCanvas;
    public Button ItemButton;
    public Button interfaceButton;
    public Button purposeButton;
    public Button ItemBeforeButton;
    public Button ItemNextButton;
    public Button interfaceBeforeButton;
    public Button interfaceNextButton;
    public TextMeshProUGUI itemNameTextUp;
    public TextMeshProUGUI itemNameTextDown;
    public TextMeshProUGUI itemInfoTextUp;
    public TextMeshProUGUI itemInfoTextDown;
    public TextMeshProUGUI interfaceNameTextUp;
    public TextMeshProUGUI interfaceNameTextDown;
    public TextMeshProUGUI interfaceInfoTextUp;
    public TextMeshProUGUI interfaceInfoTextDown;

    public TextMeshProUGUI itemPage;

    public TextMeshProUGUI interfacePage;

    private int currentItemIndex = 0;
    private int currentInterfaceIndex = 0;
    private List<ItemType> itemList;
    private List<InterfaceType> interfaceList;
    public List<Sprite> itemImages; // ������ �̹����� ������ ����Ʈ
    private List<Sprite> interfaceImages; // �������̽� �̹����� ������ ����Ʈ

    void Start()
    {
        itemList = new List<ItemType>((ItemType[])Enum.GetValues(typeof(ItemType)));
        interfaceList = new List<InterfaceType>((InterfaceType[])Enum.GetValues(typeof(InterfaceType)));

        // �� ��ư�� Ŭ�� �̺�Ʈ �߰�
        ItemButton.onClick.AddListener(OnItemButtonClick);
        interfaceButton.onClick.AddListener(OnInterfaceButtonClick);
        purposeButton.onClick.AddListener(OnPurposeButtonClick);

        // �ʱ� ���¿����� ù ��° ĵ������ Ȱ��ȭ
        itemCanvas.SetActive(true);
        interfaceCanvas.SetActive(false);
        purposeCanvas.SetActive(false);

        // ������ �� �������̽� �̹��� ����Ʈ �ʱ�ȭ
        InitializeItemImages();
        InitializeInterfaceImages();

        // ������ �� �������̽� ������ ������Ʈ
        UpdateItemInfo();
        UpdateInterfaceInfo();

        // ����/���� ��ư �̺�Ʈ ������ �߰�
        ItemBeforeButton.onClick.AddListener(OnItemBeforeButtonClick);
        ItemNextButton.onClick.AddListener(OnItemNextButtonClick);
        interfaceBeforeButton.onClick.AddListener(OnInterfaceBeforeButtonClick);
        interfaceNextButton.onClick.AddListener(OnInterfaceNextButtonClick);
    }

    // ������ ��ư Ŭ�� �� ����Ǵ� �޼���
    private void OnItemButtonClick()
    {
        SwitchCanvas(itemCanvas);
    }

    // �������̽� ��ư Ŭ�� �� ����Ǵ� �޼���
    private void OnInterfaceButtonClick()
    {
        SwitchCanvas(interfaceCanvas);
    }

    // ���� ��ư Ŭ�� �� ����Ǵ� �޼���
    private void OnPurposeButtonClick()
    {
        SwitchCanvas(purposeCanvas);
    }

    // ĵ������ ��ü�ϴ� �޼���
    private void SwitchCanvas(GameObject newCanvas)
    {
        itemCanvas.SetActive(false);
        interfaceCanvas.SetActive(false);
        purposeCanvas.SetActive(false);

        newCanvas.SetActive(true); // ���ο� ĵ������ Ȱ��ȭ

        // ĵ������ �ٲ� �� �ε��� �ʱ�ȭ
        currentItemIndex = 0;
        currentInterfaceIndex = 0;
        
        UpdateItemInfo();
        UpdateInterfaceInfo();
    }

    public void OnItemNextButtonClick()
    {
        currentItemIndex = (currentItemIndex + 1) % ((itemList.Count + 1) / 2);
        UpdateItemInfo();
    }

    public void OnItemBeforeButtonClick()
    {
        currentItemIndex = (currentItemIndex - 1 + ((itemList.Count + 1) / 2)) % ((itemList.Count + 1) / 2);
        UpdateItemInfo();
    }

    public void OnInterfaceNextButtonClick()
    {
        currentInterfaceIndex = (currentInterfaceIndex + 1) % ((interfaceList.Count + 1) / 2);
        UpdateInterfaceInfo();
    }

    public void OnInterfaceBeforeButtonClick()
    {
        currentInterfaceIndex = (currentInterfaceIndex - 1 + ((interfaceList.Count + 1) / 2)) % ((interfaceList.Count + 1) / 2);
        UpdateInterfaceInfo();
    }

    // ������ �̹��� �ʱ�ȭ
    private void InitializeItemImages()
    {
        itemImages = new List<Sprite>();
        foreach (ItemImageType itemType in Enum.GetValues(typeof(ItemImageType)))
        {
            if(itemType.ToString() == "Empty")
            {
                continue;
            }
            string imagePath = "images/ItemImages/" + itemType.ToString();
            Sprite sprite = Resources.Load<Sprite>(imagePath);
            if (sprite != null)
            {
                itemImages.Add(sprite);
            }
            else
            {
                Debug.LogError("Failed to load sprite at path: " + imagePath);
            }
        }
    }

    // �������̽� �̹��� �ʱ�ȭ
    private void InitializeInterfaceImages()
    {
        interfaceImages = new List<Sprite>();
  
        foreach (InterfaceImageType interfaceType in Enum.GetValues(typeof(InterfaceImageType)))
        {
            string imagePath = "images/InterfaceImages/" + interfaceType.ToString();
            Sprite sprite = Resources.Load<Sprite>(imagePath);
            if (sprite != null)
            {
                interfaceImages.Add(sprite);
            }
            else
            {
                Debug.LogError("Failed to load sprite at path: " + imagePath);
            }
        }
    }

    // ������ ������ ������Ʈ�ϴ� �޼���
    private void UpdateItemInfo()
    {
        itemPage.text = (currentItemIndex + 1).ToString();
        if (itemList.Count > currentItemIndex * 2 + 1)
        {
            itemImageDown.SetActive(true);
            ItemType currentTypeUp = itemList[currentItemIndex * 2];
            ItemType currentTypeDown = itemList[currentItemIndex * 2 + 1];
            itemNameTextUp.text = currentTypeUp.ToString();
            itemNameTextDown.text = currentTypeDown.ToString();
            itemInfoTextUp.text = ManualList.item[currentTypeUp];
            itemInfoTextDown.text = ManualList.item[currentTypeDown];

            // ������ �̹��� ����
            itemImageUp.GetComponent<Image>().sprite = itemImages[currentItemIndex * 2];
            itemImageDown.GetComponent<Image>().sprite = itemImages[currentItemIndex * 2 + 1];
        }
        else if (itemList.Count > currentItemIndex * 2)
        {
            ItemType currentTypeUp = itemList[currentItemIndex * 2];
            itemNameTextUp.text = currentTypeUp.ToString();
            itemInfoTextUp.text = ManualList.item[currentTypeUp];

            itemNameTextDown.text = "";
            itemInfoTextDown.text = "";

            // ������ �̹��� ����
            itemImageUp.GetComponent<Image>().sprite = itemImages[currentItemIndex * 2];
            itemImageDown.GetComponent<Image>().sprite = null;
            itemImageDown.SetActive(false);
        }
    }

    // �������̽� ������ ������Ʈ�ϴ� �޼���
    private void UpdateInterfaceInfo()
    {
        interfacePage.text = (currentInterfaceIndex + 1).ToString();
        if (interfaceList.Count > currentInterfaceIndex * 2 + 1)
        {
            InterfaceType currentTypeUp = interfaceList[currentInterfaceIndex * 2];
            InterfaceType currentTypeDown = interfaceList[currentInterfaceIndex * 2 + 1];
            interfaceNameTextUp.text = currentTypeUp.ToString();
            interfaceNameTextDown.text = currentTypeDown.ToString();
            interfaceInfoTextUp.text = ManualList.interfaceDescription[currentTypeUp];
            interfaceInfoTextDown.text = ManualList.interfaceDescription[currentTypeDown];

            // �������̽� �̹��� ����
            interImageUp.GetComponent<Image>().sprite = interfaceImages[currentInterfaceIndex * 2];
            interImageDown.GetComponent<Image>().sprite = interfaceImages[currentInterfaceIndex * 2 + 1];
        }
        else if (interfaceList.Count > currentInterfaceIndex * 2)
        {
            InterfaceType currentTypeUp = interfaceList[currentInterfaceIndex * 2];
            interfaceNameTextUp.text = currentTypeUp.ToString();
            interfaceInfoTextUp.text = ManualList.interfaceDescription[currentTypeUp];

            interfaceNameTextDown.text = "";
            interfaceInfoTextDown.text = "";

            // �������̽� �̹��� ����
            interImageUp.GetComponent<Image>().sprite = interfaceImages[currentInterfaceIndex * 2];
            interImageDown.GetComponent<Image>().sprite = interfaceImages[currentInterfaceIndex * 2+1];
        }
    }
}
