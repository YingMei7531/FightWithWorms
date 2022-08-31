using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    public Text helpText;
    public TextAsset helpFile;
    List<string> helpNotes = new List<string>();
    int helpNum;
    int helpIndex;

    void Awake()
    {
        var lineData = helpFile.text.Split('\n');
        foreach (var line in lineData) {
            helpNotes.Add(line);
        }
        helpNum = helpNotes.Count;
        helpIndex = 0;

    }

    void OnEnable()
    {
        helpIndex = 0;
        helpText.text = helpNotes[helpIndex];
    }

    public void NextPage() {
        helpIndex = (helpIndex + 1) % helpNum;
        helpText.text = helpNotes[helpIndex];
    }
    public void LastPage() {
        helpIndex = helpIndex - 1;
        if (helpIndex < 0) helpIndex = helpNum - 1;
        helpText.text = helpNotes[helpIndex];
    }
    public void Back() {
        this.gameObject.SetActive(false);
    }
}
