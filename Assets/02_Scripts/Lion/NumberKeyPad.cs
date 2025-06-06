using TMPro;
using UnityEngine;

public class NumberKeyPad : MonoBehaviour
{
    public GameObject doorLockUI;
    public string password;
    public string KeyPadNumber;
    public TMP_Text NumberTMPText;
    public Animator doorAnim;


    public void ResetDoorLock()
    {
        KeyPadNumber = "";
        NumberTMPText.text = KeyPadNumber;
    }
    public void OnInputNumber(string input)
    {
        KeyPadNumber += input;
        NumberTMPText.text = KeyPadNumber;
    }

    public void BackSpace()
    {
        if (KeyPadNumber.Length < 1) return;
        KeyPadNumber = KeyPadNumber.Remove(KeyPadNumber.Length - 1);
        
        NumberTMPText.text = KeyPadNumber;
    }

    public void OnCheckNumber()
    {
        if (KeyPadNumber == password)
        {
            Debug.Log("SUCCESS");
            doorLockUI.SetActive(false);
            ResetDoorLock();
            doorAnim.SetTrigger("Open");
        }
        else
        {
            Debug.Log("Wrong Password");
        }
    }
}
