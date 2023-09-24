using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
public class CodingTest : MonoBehaviour
{
    public InputField inputField;
    public Text outputText;

    private Dictionary<string, object> variables = new Dictionary<string, object>();
    void Start()
    {
        //Interpret();
    }
    public void Interpret() {
        string code = inputField.text;
        string[] lines = code.Split('\n');
        foreach (string line in lines) {
            InterpretLine(line);
        }
    }

    private void InterpretLine(string line) {
        if (line.StartsWith("print(\"")) {
            int startIndex = line.IndexOf("(\"") + 2;
            int endIndex = line.IndexOf("\")");
            string message = line.Substring(startIndex, endIndex - startIndex);
            Debug.Log(message);
            outputText.text += message + "\n";
        }
        else if (line.StartsWith("warn(\"")) {
            int startIndex = line.IndexOf("(\"") + 2;
            int endIndex = line.IndexOf("\")");
            string message = line.Substring(startIndex, endIndex - startIndex);
            Debug.Log("<color=yellow>" + message + "</color>");
            outputText.text += message + "\n";
        }
        // Implement other language constructs as needed
    }
}
