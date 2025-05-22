using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(WallKickDataSO))]
public class WallKickDataSOEditor : Editor
{
    private SerializedProperty JLSZT_WallKicksProp;
    private SerializedProperty I_WallKicksProp;
    private SerializedProperty WallKicks_KeysProp; // 새로 추가된 필드의 SerializedProperty

    private string[] rotationLabels = {
        "0 -> R", "R -> 0", "R -> 2", "2 -> R", "2 -> L", "L -> 2", "L -> 0", "0 -> L"
    };

    private void OnEnable()
    {
        JLSZT_WallKicksProp = serializedObject.FindProperty("JLSZT_WallKicks");
        I_WallKicksProp = serializedObject.FindProperty("I_WallKicks");
        WallKicks_KeysProp = serializedObject.FindProperty("WallKicks_Keys"); // WallKicks_Keys 필드 찾기
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // 시리얼라이즈된 오브젝트 업데이트

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("J, L, S, T, Z Tetromino Wall Kick Data", EditorStyles.boldLabel);
        DrawWallKickTable(JLSZT_WallKicksProp);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("I Tetromino Wall Kick Data", EditorStyles.boldLabel);
        DrawWallKickTable(I_WallKicksProp);

        // --- 여기에 WallKicks_Keys 필드를 그리는 코드 추가 ---
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Wall Kicks Keys (Rotation Base Offsets)", EditorStyles.boldLabel);
        // 리스트를 기본 UI로 그립니다. true는 리스트를 펼쳐서 각 요소를 보여줍니다.
        EditorGUILayout.PropertyField(WallKicks_KeysProp, true);

        // 만약 WallKicks_Keys를 표처럼 직접 그리고 싶다면, 아래와 같이 DrawListAsTable 등을 사용할 수 있습니다.
        // DrawListAsTable(WallKicks_KeysProp, "Keys", rotationLabels);
        // --- 여기까지 추가 ---


        serializedObject.ApplyModifiedProperties(); // 변경사항 적용
    }

    private void DrawWallKickTable(SerializedProperty kickDataListProp)
    {
        // 테이블 헤더
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("", GUILayout.Width(80)); // 빈 공간
        EditorGUILayout.LabelField("Test 1", EditorStyles.boldLabel, GUILayout.Width(60));
        EditorGUILayout.LabelField("Test 2", EditorStyles.boldLabel, GUILayout.Width(60));
        EditorGUILayout.LabelField("Test 3", EditorStyles.boldLabel, GUILayout.Width(60));
        EditorGUILayout.LabelField("Test 4", EditorStyles.boldLabel, GUILayout.Width(60));
        EditorGUILayout.LabelField("Test 5", EditorStyles.boldLabel, GUILayout.Width(60));
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < kickDataListProp.arraySize; i++)
        {
            SerializedProperty rotationKickDataProp = kickDataListProp.GetArrayElementAtIndex(i);
            SerializedProperty kicksListProp = rotationKickDataProp.FindPropertyRelative("kicks");

            string label = (i < rotationLabels.Length) ? rotationLabels[i] : $"Rotation {i}";
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(80));

            if (kicksListProp != null && kicksListProp.isArray)
            {
                // 리스트의 크기가 5가 아닐 경우 자동으로 5로 맞춰줌 (옵션)
                if (kicksListProp.arraySize != 5)
                {
                    kicksListProp.arraySize = 5;
                    for (int j = 0; j < kicksListProp.arraySize; j++)
                    {
                        // 새로 추가된 요소는 Vector2Int.zero로 초기화
                        if (kicksListProp.GetArrayElementAtIndex(j).propertyType == SerializedPropertyType.Vector2Int)
                        {
                            kicksListProp.GetArrayElementAtIndex(j).vector2IntValue = Vector2Int.zero;
                        }
                    }
                }

                for (int j = 0; j < kicksListProp.arraySize; j++)
                {
                    DrawVector2IntField(kicksListProp.GetArrayElementAtIndex(j));
                }
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    private void DrawVector2IntField(SerializedProperty prop)
    {
        SerializedProperty xProp = prop.FindPropertyRelative("x");
        SerializedProperty yProp = prop.FindPropertyRelative("y");

        EditorGUILayout.BeginHorizontal(GUILayout.Width(60));
        EditorGUILayout.PropertyField(xProp, GUIContent.none, GUILayout.Width(25));
        EditorGUILayout.PropertyField(yProp, GUIContent.none, GUILayout.Width(25));
        EditorGUILayout.EndHorizontal();
    }

    // 추가: WallKicks_Keys를 테이블 형태로 그리는 헬퍼 함수 (선택 사항)
    // 이 함수를 사용하려면 DrawWallKickTable 위에 선언된 SerializedProperty WallKicks_KeysProp을
    // OnInspectorGUI에서 호출해야 합니다.
    private void DrawListAsTable(SerializedProperty listProp, string columnHeader, string[] rowLabels)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("", GUILayout.Width(80)); // Row label column
        EditorGUILayout.LabelField(columnHeader, EditorStyles.boldLabel, GUILayout.Width(60));
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < listProp.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            string label = (i < rowLabels.Length) ? rowLabels[i] : $"Item {i}";
            EditorGUILayout.LabelField(label, GUILayout.Width(80));
            DrawVector2IntField(listProp.GetArrayElementAtIndex(i));
            EditorGUILayout.EndHorizontal();
        }
    }
}