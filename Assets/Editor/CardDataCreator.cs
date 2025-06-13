using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class CardDataCreator // MonoBehaviour를 상속받지 않아도 됩니다!
{
    private static string folderPath = "Assets/06_ScriptableObjects/Poker/Cards"; 

    // 스프라이트 시트의 경로 (예: "Assets/Sprites/CardAtlas.png")
    // 이 경로는 단일 이미지 파일이어야 합니다.
    private static string spriteSheetPath = "Assets/Resources/Poker/Tilesheet/cards.png"; 

    [MenuItem("Tools/Create All CardData Assets")]
    public static void CreateAllCardDataAssets()
    {
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            Directory.CreateDirectory(Application.dataPath + "/ScriptableObjects/Cards");
            AssetDatabase.Refresh(); 
        }

        // 모든 하위 스프라이트들을 미리 로드합니다.
        // Resources.LoadAll<Sprite>("폴더이름") 과는 다르게 이 경로는 직접적인 에셋 경로입니다.
        Sprite[] allSpritesInSheet = AssetDatabase.LoadAllAssetsAtPath(spriteSheetPath)
                                     .OfType<Sprite>() // Sprite 타입만 필터링
                                     .ToArray();

        if (allSpritesInSheet.Length == 0)
        {
            Debug.LogError($"No sprites found at {spriteSheetPath}. Please ensure the sprite sheet is correctly imported and cut into multiple sprites.");
            return;
        }

        foreach (CardSuit suit in System.Enum.GetValues(typeof(CardSuit)))
        {
            foreach (CardRank rank in System.Enum.GetValues(typeof(CardRank)))
            {
                if ((int)rank < 2 || (int)rank > 14) continue;

                pokerCardData newCardData = ScriptableObject.CreateInstance<pokerCardData>();
                newCardData.suit = suit;
                newCardData.rank = rank;

                // 스프라이트 이름 규칙에 따라 특정 스프라이트 찾기
                // 스프라이트 에디터에서 각 카드 스프라이트의 이름을 규칙적으로 지정해야 합니다.
                // 예: "Clubs_2", "Diamonds_Ace"
                string spriteName = $"{suit}_{(int)rank}";
                Sprite foundSprite = System.Array.Find(allSpritesInSheet, s => s.name == spriteName);

                if (foundSprite != null)
                {
                    newCardData.frontSprite = foundSprite;
                }
                else
                {
                    Debug.LogWarning($"Sprite not found for {suit} {rank} with name '{spriteName}' in {spriteSheetPath}.");
                }

                string assetPath = $"{folderPath}/CardData_{suit}_{rank}.asset";
                AssetDatabase.CreateAsset(newCardData, assetPath);
            }
        }

        AssetDatabase.SaveAssets(); 
        AssetDatabase.Refresh();    
        Debug.Log("Successfully created all 52 CardData ScriptableObjects!");
    }
}