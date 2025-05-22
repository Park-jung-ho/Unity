using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewWallKickData", menuName = "Tetris/Wall Kick Data")]
public class WallKickDataSO : ScriptableObject
{
    // J, L, S, T, Z 블록의 Wall Kick 데이터 (SRS 표준)
    public List<RotationKickData> JLSZT_WallKicks = new List<RotationKickData>();

    // I 블록의 Wall Kick 데이터 (SRS 표준)
    public List<RotationKickData> I_WallKicks = new List<RotationKickData>();

    // 새로 추가된 WallKickKeys 필드
    // 이 리스트는 각 회전 전환 (0->R, R->0 등)의 "기본" 또는 "기준" kick 값을 저장할 수 있습니다.
    // 표의 Test 1 열에 해당하는 값들을 여기 넣을 수 있습니다.
    public List<Vector2Int> WallKicks_Keys = new List<Vector2Int>();

    void OnEnable()
    {
        // ScriptableObject가 처음 생성될 때만 초기화
        if (JLSZT_WallKicks.Count == 0 || JLSZT_WallKicks[0].kicks.Count != 5)
        {
            InitializeJLSZTData();
        }
        if (I_WallKicks.Count == 0 || I_WallKicks.Count != 5) // Note: Fixed a typo from I_WallKicks[0].kicks.Count to I_Kicks.Count assuming you meant the property, if not then change to I_WallKicks[0].kicks.Count again
        {
            InitializeIData();
        }

        // WallKicks_Keys 초기화 (필요하다면)
        // 8개의 회전 전환에 대한 키 값이 필요하므로 8개로 초기화합니다.
        if (WallKicks_Keys.Count == 0 || WallKicks_Keys.Count != 8) // 8개 전환 (0->R, R->0, R->2, 2->R, 2->L, L->2, L->0, 0->L)
        {
            InitializeWallKickKeys();
        }
    }

    private void InitializeJLSZTData()
    {
        JLSZT_WallKicks.Clear();
        // 0 -> R (from 0 to Right)
        JLSZT_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, +1), new Vector2Int(0, -2), new Vector2Int(-1, -2)
        ));
        // R -> 0 (from Right to 0)
        JLSZT_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(+1, 0), new Vector2Int(+1, +1), new Vector2Int(0, -2), new Vector2Int(+1, -2)
        ));
        // R -> 2 (from Right to 180)
        JLSZT_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(+1, 0), new Vector2Int(+1, -1), new Vector2Int(0, +2), new Vector2Int(+1, +2)
        ));
        // 2 -> R (from 180 to Right)
        JLSZT_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, +2), new Vector2Int(-1, +2)
        ));
        // 2 -> L (from 180 to Left)
        JLSZT_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(+1, 0), new Vector2Int(+1, +1), new Vector2Int(0, -2), new Vector2Int(+1, -2)
        ));
        // L -> 2 (from Left to 180)
        JLSZT_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, +2), new Vector2Int(-1, +2)
        ));
        // L -> 0 (from Left to 0)
        JLSZT_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, +1), new Vector2Int(0, -2), new Vector2Int(-1, -2)
        ));
        // 0 -> L (from 0 to Left)
        JLSZT_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(+1, 0), new Vector2Int(+1, -1), new Vector2Int(0, +2), new Vector2Int(+1, +2)
        ));
    }

    private void InitializeIData()
    {
        I_WallKicks.Clear();
        // 0 -> R
        I_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int(+1, 0), new Vector2Int(-2, -1), new Vector2Int(+1, +2)
        ));
        // R -> 0
        I_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(+2, 0), new Vector2Int(-1, 0), new Vector2Int(+2, +1), new Vector2Int(-1, -2)
        ));
        // R -> 2
        I_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(+2, 0), new Vector2Int(-1, +2), new Vector2Int(+2, -1)
        ));
        // 2 -> R
        I_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(+1, 0), new Vector2Int(-2, 0), new Vector2Int(+1, -2), new Vector2Int(-2, +1)
        ));
        // 2 -> L
        I_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(+2, 0), new Vector2Int(-1, 0), new Vector2Int(+2, +1), new Vector2Int(-1, -2)
        ));
        // L -> 2
        I_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int(+1, 0), new Vector2Int(-2, -1), new Vector2Int(+1, +2)
        ));
        // L -> 0
        I_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(+1, 0), new Vector2Int(-2, 0), new Vector2Int(+1, -2), new Vector2Int(-2, +1)
        ));
        // 0 -> L
        I_WallKicks.Add(new RotationKickData(
            new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(+2, 0), new Vector2Int(-1, +2), new Vector2Int(+2, -1)
        ));
    }

    // WallKicks_Keys를 초기화하는 새로운 메소드
    private void InitializeWallKickKeys()
    {
        WallKicks_Keys.Clear();
        // 8개의 모든 전환에 대해 Test 1 값 (0,0)을 키로 가정
        for (int i = 0; i < 8; i++)
        {
            WallKicks_Keys.Add(new Vector2Int(0, 0));
        }
        // JLSZT_WallKicks[i].kicks[0] 또는 I_WallKicks[i].kicks[0]에서 가져올 수도 있습니다.
        // 예를 들어 JLSZT_WallKicks의 Test1 값으로 초기화하려면:
        // for (int i = 0; i < JLSZT_WallKicks.Count; i++)
        // {
        //     WallKicks_Keys.Add(JLSZT_WallKicks[i].kicks[0]);
        // }
    }
}

// 각 회전 전환 (e.g., 0->R)에 대한 Wall Kick 데이터를 담는 클래스
[System.Serializable]
public class RotationKickData
{
    public List<Vector2Int> kicks;

    public RotationKickData()
    {
        kicks = new List<Vector2Int>(5); // 5개 요소를 가질 리스트 초기화
        for (int i = 0; i < 5; i++)
        {
            kicks.Add(Vector2Int.zero); // 기본값으로 (0,0) 초기화
        }
    }

    public RotationKickData(Vector2Int t1, Vector2Int t2, Vector2Int t3, Vector2Int t4, Vector2Int t5)
    {
        kicks = new List<Vector2Int>(5) { t1, t2, t3, t4, t5 };
    }
}