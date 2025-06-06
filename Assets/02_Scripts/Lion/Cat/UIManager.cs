using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CatGame
{
    public class UIManager : MonoBehaviour
    {
        public TMP_InputField inputField;
        public TextMeshProUGUI nameText;

        public void OnStartButton()
        {
            nameText.text = inputField.text;
        }
    }
}