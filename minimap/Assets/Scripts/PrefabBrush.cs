//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEditor.Tilemaps;
//using UnityEngine;

//[CreateAssetMenu(fileName = "Prefab bush", menuName = "Brushes/Prefab brush")]
//[CustomGridBrush(hideAssetInstances: false, hideDefaultInstance: true, defaultBrush: false, defaultName: "Prefab Brush")]

//public class PrefabBrush : GameObjectBrush
//{
//    public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
//    {
//        if (brushTarget.layer == 31)
//        {
//            return;
//        }
//        Transform erased = GetObjectIncell(gridLayout, brushTarget.transform,
//            position: new Vector3Int(position.x, position.y,z:0);
//        if (erased != null)
//        {
//            Undo.DestroyObjectImmediate(erased.gameObject);
//        }
//    }

//    private static Transform GetObjectIncell(GridLayout gird, Transform parent, Vector3Int position)
//    {
//        int childCount = parent.childCount;
//        Vector3 min = gird.LocalToWorld(grid.CellTOLocalInterpolated((Vector3)position));
//        Vector3 max = gird.LocalToWorld(grid.CellTOLocalInterpolated((Vector3)position));
//        Bounds bounds = new Bounds((max + min * 0.5f, size: max - min);

//        for(int i = 0; i < childCount; i++)
//        {
//            Transform child = parent.GetChild(i);
//            if (bounds.Contains(child.position))
//            {
//                return child;

//            }
//        }

//    }


//}
