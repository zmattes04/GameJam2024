using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditableTextBox : MonoBehaviour
{
    public TMP_InputField inputField;

    void Start()
    {
        inputField.onEndEdit.AddListener(SaveText);
    }

    private void SaveText(string text)
    {
        GameManager.username = text;
        PlayerPrefs.SetString("Username", text);
        Debug.Log("Saved Text: " + GameManager.username);
        PlayerPrefs.Save();
    }

    void OnDestroy()
    {
        inputField.onEndEdit.RemoveListener(SaveText);
    }
}

