using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Tilemap Configuration", menuName = "Tilemap Configuration")]
public class TilesScriptableObject : ScriptableObject
{
    public RuleTile RuleTiles;
    public ArrayLayout Tiles;
}

[Serializable]
public class ArrayLayout
{
    [Serializable]
    public struct ArrayData
    {
        public bool[] ColumnData;
    }
    public int Rows;
    public int Columns;
    public ArrayData[] Data;
}

[CustomPropertyDrawer(typeof(ArrayLayout))]
public class ArrayLayoutPropertyDrawer : PropertyDrawer
{
    float propertyDrawerHeight;

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var minValue = 0;
        var rowMaxValue = 10;
        var columnMaxValue = 8;
        propertyDrawerHeight = 0f;

        EditorGUI.PrefixLabel(position, label);

        propertyDrawerHeight += 20f;

        var positionSize = position;
        positionSize.y += 18f;
        positionSize.x += 10f;
        positionSize.height = 20f;
        EditorGUI.LabelField(positionSize, "Size: ");

        positionSize.x += position.width - 120f;
        positionSize.width = 50f;
        var rowsValue = property.FindPropertyRelative("Rows");
        EditorGUI.PropertyField(positionSize, rowsValue, GUIContent.none);
        if (rowsValue.intValue > rowMaxValue)
            rowsValue.intValue = rowMaxValue;
        if (rowsValue.intValue < minValue)
            rowsValue.intValue = minValue;

        positionSize.x += 55f;
        var columnsValue = property.FindPropertyRelative("Columns");
        EditorGUI.PropertyField(positionSize, columnsValue, GUIContent.none);
        if (columnsValue.intValue > columnMaxValue)
            columnsValue.intValue = columnMaxValue;
        if (columnsValue.intValue < minValue)
            columnsValue.intValue = minValue;

        propertyDrawerHeight += positionSize.height;

        var positionArrayData = position;
        positionArrayData.y += positionSize.height + 5;
        positionArrayData.x += 10f;
        positionArrayData.width = position.width / columnsValue.intValue;
        positionArrayData.height = 20f;

        var rows = property.FindPropertyRelative("Data");
        if (rows.arraySize != rowsValue.intValue)
            rows.arraySize = rowsValue.intValue;

        for (int i = 0; i < rowsValue.intValue; i++)
        {
            positionArrayData.y += 20f;
            positionArrayData.x = position.x;
            propertyDrawerHeight += positionArrayData.height;

            var columns = rows.GetArrayElementAtIndex(i).FindPropertyRelative("ColumnData");
            if (columns.arraySize != columnsValue.intValue)
                columns.arraySize = columnsValue.intValue;

            for (int j = 0; j < columnsValue.intValue; j++)
            {
                EditorGUI.PropertyField(positionArrayData, columns.GetArrayElementAtIndex(j), GUIContent.none);
                positionArrayData.x += positionArrayData.width;
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return propertyDrawerHeight;
    }
}