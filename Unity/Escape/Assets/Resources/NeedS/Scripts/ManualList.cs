using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ManualList;

public class ManualList : MonoBehaviour
{
    public enum ItemType
    {
        ������,
        ����,
        �볪��,
        ��ũ��,
        ö����,
        ��ũ��,
        ��Ʈ,
        ö��,
        ����,
        ��������,
        ��������,
        ��,
        ����,
        ���,
        ��ġ,
        ȸ��,
        ���͸�
    }

    public enum InterfaceType
    {
        ��������,
        ���ļ���,
        �κ��丮,
        ����������,
        �����۽���,
        ����â,
    }


    public static Dictionary<ItemType, string> item = new Dictionary<ItemType, string>()
        {
            { ItemType.������, "�⺻���� \n: ����⸦ ����ϴµ� �ʿ��� ���� \nȹ�� ��� \n: �ʵ忡�� ��� \n��ġ \n: ���� �ʵ忡�� Ȯ�ΰ���" },
            { ItemType.����, "�⺻���� \n: ������ ����� ���� ��Ḧ ����µ� �ʿ��� ��� \nȹ�� ��� \n: ���۴뿡�� ���� ���� \n ���չ� \n: �볪�� x 2" },
            { ItemType.�볪��, "�⺻���� \n: ���縦 ����µ� �ʿ��� ��� \nȹ�� ��� \n: �ʵ忡�� ������ ä���Ͽ� ���� ���� \n" },
            { ItemType.��ũ��, "�⺻���� \n: ������ ����� ���� ��Ḧ ����µ� �ʿ��� ��� \nȹ�� ��� \n: ���۴뿡�� ���� ���� \n���չ� : ö���� x 2"},
            { ItemType.ö����, "�⺻���� \n: ��ũ���� ��ũ���� ����µ� �ʿ��� ��� \nȹ�� ��� \n: ���� ä���Ͽ� ȹ�� ����" },
            { ItemType.��ũ��, "�⺻���� \n: �پ��� �����۵��� ����ִ� �� \n��ġ \n: ���� �ʵ忡�� Ȯ�ΰ���" },
            { ItemType.��Ʈ, "�⺻���� \n: ����⸦ �����ϴµ� �ʿ��� ��� \nȹ�� ��� \n: ���۴뿡�� ���� ���� \n���չ� \n: ��ũ�� x 1"},
            { ItemType.ö��, "�⺻���� \n: ������ ���� ���� �� �ִ�. \nȹ�� ��� \n: ���۴뿡�� ���� ���� \n���չ� \n: ��ũ�� x 1 ��Ʈ x 2"},
            { ItemType.����, "�⺻���� \n: ������ ����µ� �ʿ������ \nȹ�� ��� \n: ���۴뿡�� ���� ���� \n���չ� \n: ���� ���� x 3"},
            { ItemType.��������, "�⺻���� \n: ������ ����µ� �ʿ��� ��� \nȹ�� ��� \n: �ʵ忡�� ���������� ä���Ͽ� ȹ�� ����" },
            { ItemType.��������, "�⺻���� \n: ����⸦ �����ϴµ� �ʿ��� ��� \nȹ�� ��� \n: ���۴뿡�� ���� ���� \n���չ� \n: �� x 5"},
            { ItemType.��, "�⺻���� \n: ö������ ����ϴ� �ʵ� ������Ʈ \n��ġ \n: �ʵ忡�� Ȯ�ΰ���" },
            { ItemType.����, "�⺻���� \n: ������ ä���ϴµ� ������ ���� \nȹ�� ��� \n: ���۴뿡�� ���� ���� \n���չ� \n: ��ũ�� x 2 ���� x 4"},
            { ItemType.���, "�⺻���� \n: ������ ä���ϴµ� ������ ���� \nȹ�� ��� \n: ���۴뿡�� ���� ���� \n���չ� \n: ��ũ�� x 3 ���� x 4"},
            { ItemType.��ġ, "�⺻���� \n: ����⸦ �����ϴµ� �ʿ��� ��� \nȹ�� ��� \n: ���۴뿡�� ���� ���� \n���չ� \n: ��ũ�� x 3 ���� x 4 ��Ʈ x 2"},
            { ItemType.ȸ��, "�⺻���� \n: ����⸦ �����ϴµ� �ʿ��� ��� \nȹ�� ��� \n: ���۴뿡�� ���� ���� \n���չ� \n: ��ũ�� x 2 ���� x 3 �������� x 2"},
            { ItemType.���͸�, "�⺻���� \n: ����⸦ �����ϴµ� �ʿ��� ��� \nȹ�� ��� \n: ���۴뿡�� ���� ���� \n���չ� \n: ��ũ�� x 3 ���� x 2 �������� x 2"}
    };
    public static Dictionary<InterfaceType, string> interfaceDescription = new Dictionary<InterfaceType, string>()
    {
        { InterfaceType.��������, "wasd, �� �� �� �� �� �̿��Ͽ� �����¿� �̵�����" },
        { InterfaceType.���ļ���, "���콺 ������ ��ư Ŭ��" },
        { InterfaceType.�κ��丮, "I(��) ��ư�� ���� �κ��丮â Ȱ��ȭ" },
        { InterfaceType.����������, "Tab Ű�� ���� ������ ���۴� Ȱ��ȭ" },
        { InterfaceType.�����۽���, "�����ۿ� ������ �ٰ��� E(��) ��ư�� ���� ������ ���� ����" },
        { InterfaceType.����â, "����� ��ó�� ������ �ٰ��� ����⸦ �ٶ󺸰� F(��)Ű�� ���� ����� ����" },
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
