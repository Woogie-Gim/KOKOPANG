using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ManualList;

public class ManualList : MonoBehaviour
{
    public enum ItemType
    {
        연료통,
        목재,
        통나무,
        스크랩,
        철광석,
        오크통,
        볼트,
        철판,
        구리,
        구리원석,
        유리섬유,
        돌,
        도끼,
        곡괭이,
        망치,
        회로,
        배터리
    }

    public enum InterfaceType
    {
        방향조작,
        음식섭취,
        인벤토리,
        아이템제작,
        아이템습득,
        수리창,
    }


    public static Dictionary<ItemType, string> item = new Dictionary<ItemType, string>()
        {
            { ItemType.연료통, "기본설명 \n: 비행기를 출발하는데 필요한 연료 \n획득 방법 \n: 필드에서 드랍 \n위치 \n: 메인 필드에서 확인가능" },
            { ItemType.목재, "기본설명 \n: 도구와 비행기 수리 재료를 만드는데 필요한 재료 \n획득 방법 \n: 제작대에서 제작 가능 \n 조합법 \n: 통나무 x 2" },
            { ItemType.통나무, "기본설명 \n: 목재를 만드는데 필요한 재료 \n획득 방법 \n: 필드에서 나무를 채집하여 수집 가능 \n" },
            { ItemType.스크랩, "기본설명 \n: 도구와 비행기 수리 재료를 만드는데 필요한 재료 \n획득 방법 \n: 제작대에서 제작 가능 \n조합법 : 철광석 x 2"},
            { ItemType.철광석, "기본설명 \n: 스크랩과 스크류를 만드는데 필요한 재료 \n획득 방법 \n: 돌을 채광하여 획득 가능" },
            { ItemType.오크통, "기본설명 \n: 다양한 아이템들이 들어있는 통 \n위치 \n: 메인 필드에서 확인가능" },
            { ItemType.볼트, "기본설명 \n: 비행기를 수리하는데 필요한 재료 \n획득 방법 \n: 제작대에서 제작 가능 \n조합법 \n: 스크랩 x 1"},
            { ItemType.철판, "기본설명 \n: 제작을 통해 만들 수 있다. \n획득 방법 \n: 제작대에서 제작 가능 \n조합법 \n: 스크랩 x 1 볼트 x 2"},
            { ItemType.구리, "기본설명 \n: 도구를 만드는데 필요한재료 \n획득 방법 \n: 제작대에서 제작 가능 \n조합법 \n: 구리 원석 x 3"},
            { ItemType.구리원석, "기본설명 \n: 구리를 만드는데 필요한 재료 \n획득 방법 \n: 필드에서 구리원석을 채광하여 획득 가능" },
            { ItemType.유리섬유, "기본설명 \n: 비행기를 수리하는데 필요한 재료 \n획득 방법 \n: 제작대에서 제작 가능 \n조합법 \n: 돌 x 5"},
            { ItemType.돌, "기본설명 \n: 철광석을 드랍하는 필드 오브젝트 \n위치 \n: 필드에서 확인가능" },
            { ItemType.도끼, "기본설명 \n: 나무를 채집하는데 유용한 도구 \n획득 방법 \n: 제작대에서 제작 가능 \n조합법 \n: 스크랩 x 2 목재 x 4"},
            { ItemType.곡괭이, "기본설명 \n: 광물을 채광하는데 유용한 도구 \n획득 방법 \n: 제작대에서 제작 가능 \n조합법 \n: 스크랩 x 3 목재 x 4"},
            { ItemType.망치, "기본설명 \n: 비행기를 수리하는데 필요한 재료 \n획득 방법 \n: 제작대에서 제작 가능 \n조합법 \n: 스크랩 x 3 목재 x 4 볼트 x 2"},
            { ItemType.회로, "기본설명 \n: 비행기를 수리하는데 필요한 재료 \n획득 방법 \n: 제작대에서 제작 가능 \n조합법 \n: 스크랩 x 2 구리 x 3 유리섬유 x 2"},
            { ItemType.배터리, "기본설명 \n: 비행기를 수리하는데 필요한 재료 \n획득 방법 \n: 제작대에서 제작 가능 \n조합법 \n: 스크랩 x 3 구리 x 2 유리섬유 x 2"}
    };
    public static Dictionary<InterfaceType, string> interfaceDescription = new Dictionary<InterfaceType, string>()
    {
        { InterfaceType.방향조작, "wasd, ← ↑ ↓ → 를 이용하여 상하좌우 이동가능" },
        { InterfaceType.음식섭취, "마우스 오른쪽 버튼 클릭" },
        { InterfaceType.인벤토리, "I(ㅑ) 버튼을 눌러 인벤토리창 활성화" },
        { InterfaceType.아이템제작, "Tab 키를 눌러 아이템 제작대 활성화" },
        { InterfaceType.아이템습득, "아이템에 가까이 다가가 E(ㄷ) 버튼을 눌러 아이템 습득 가능" },
        { InterfaceType.수리창, "비행기 근처로 가까이 다가가 비행기를 바라보고 F(ㄹ)키를 눌러 비행기 수리" },
    };


    public enum ItemImageType
    {
        Item_OilBarrel,
        Item_Wooden_Board,
        Log,
        Item_Scrap,
        Item_IronOre,
        Orc,
        Item_Screw,
        Item_IronPlate,
        Item_Copper,
        Item_CopperOre,
        Item_Fiberglass,
        Stone,
        Axe,
        PickAxe,
        Item_Hammer,
        Item_Circuit,
        Item_Battery,
        Empty
    }
    public static Dictionary<ItemImageType, string> itemImageDescription = new Dictionary<ItemImageType, string>()
    {
        { ItemImageType.Item_OilBarrel, "Item_OilBarrel" },
        { ItemImageType.Item_Wooden_Board, "Item_Wooden_Board" },
        { ItemImageType.Log, "Log" },
        { ItemImageType.Item_Scrap, "Item_Scrap" },
        { ItemImageType.Item_IronOre, "Item_IronOre" },
        { ItemImageType.Orc, "Orc" },
        { ItemImageType.Item_Screw, "Item_Screw" },
        { ItemImageType.Item_IronPlate, "Item_IronPlate" },
        { ItemImageType.Item_Copper, "Item_Copper" },
        { ItemImageType.Item_CopperOre, "Item_CopperOre" },
        { ItemImageType.Item_Fiberglass, "Item_Fiberglass" },
        { ItemImageType.Stone, "Stone" },
        { ItemImageType.Axe, "Axe" },
        { ItemImageType.PickAxe, "PickAxe" },
        { ItemImageType.Item_Hammer, "Item_Hammer" },
        { ItemImageType.Item_Circuit, "Item_Circuit" },
        { ItemImageType.Item_Battery, "Item_Battery" },
        //{ ItemImageType.Empty, "Empty" },
    };

    public enum InterfaceImageType
    {
        Direction,
        Eat,
        Inventory,
        Craft,
        Get,
        Fix
    }


    public static Dictionary<InterfaceImageType, string> interfaceImageDescription = new Dictionary<InterfaceImageType, string>()
    {
        { InterfaceImageType.Direction,"Direction"},
        { InterfaceImageType.Eat, "Eat"},
        { InterfaceImageType.Inventory, "Inventory"},
        { InterfaceImageType.Craft,"Craft"},
        { InterfaceImageType.Get, "Get"},
        { InterfaceImageType.Fix,"Fix"},
    };


}
