using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// This script is used to create a console for the game.
// Dieses Skript wird verwendet um eine Konsole für das Spiel zu erstellen.

// The console can be opened by pressing the backquote key.
// Die Konsole kann geöffnet werden indem die Backquote Taste gedrückt wird.

// The console can be closed by pressing the backquote key again.
// Die Konsole kann geschlossen werden indem die Backquote Taste erneut gedrückt wird.

// Requirements:
    // Drag both prefects into the canvas
    // Attach the console script to anywhere in the each scene used
    // Enter your own PASSWORD into the console script in the inspector



// Anforderungen:
    // Füge beide prefabs in das canvas rein.
    // Füge das “Console” Skript irgendwo in allen Szenen, die benutzt werden.
    // Füge dein eigenes PASSWORT in unity inspector auf der “Console” Skript ein.

namespace Tobilinski_Code_Tools
{
    public class Console : MonoBehaviour
{
    private InputField _inputField;
    private Text _consoleText;
    private GameObject _ConsoleParent;
    
    private InputField _inputFieldPassword;
    private Text _consoleTextPassword;
    private GameObject _PasswordParent;
    
    [SerializeField]
    private string _password;
    static List<string> typedCommands = new List<string>();
    private void Start()
    {
        // Finding and assigning references to UI elements.
        // Finden von Gameobject fuer die Konsole der Referenze.
        _consoleText = GameObject.Find("Output").GetComponent<Text>();
        _inputField = GameObject.Find("Console").GetComponent<InputField>();
        _ConsoleParent = GameObject.Find("ConsoleParent");
        
        _PasswordParent = GameObject.Find("PasswordParent");
        _inputFieldPassword = GameObject.Find("PasswordInput").GetComponent<InputField>();
        _consoleTextPassword = GameObject.Find("OutputPass").GetComponent<Text>();
        
        // Setting the console UI to be inactive by default.
        // Setzen der Konsole auf inaktiv als Standard.
        _PasswordParent.SetActive(false);
        _ConsoleParent.SetActive(false);
        
        // Displaying previously typed commands in the console.
        // Anzeigen von zuvor eingegebenen Befehlen in der Konsole.
        for (int i = 0; i < typedCommands.Count; i++)
        {
            _consoleText.text += "\n" + typedCommands[i];
        }
        for (int i = 0; i < typedCommands.Count; i++)
        {
            _consoleTextPassword.text += "\n" + typedCommands[i];
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        //Input to open console.
        //Eingabe um Konsole zu öffnen.
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
           if(_ConsoleParent.activeSelf)
            {
                toggleConsole();
            }
            else
            {
                togglePassword();
            }
        }

        if (!_ConsoleParent.activeSelf  && !_PasswordParent.activeSelf)
        {
            Cursor.visible = false;
        }
       
        
        //Input to send command.
        //Eingabe um Befehl zu senden.
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string command = _inputField.text;
            //Adding the command to the list of typed commands.
            //Hinzufügen des Befehls zur Liste der eingegebenen Befehle.
            typedCommands.Add(command);
            //Only adding the command to the consoleText (Display) if it is not empty.
            //Nur hinzufügen des Befehls zum consoleText (Anzeige) wenn er nicht leer ist.
            if (!string.IsNullOrEmpty(command))
            {
                _consoleText.text += "\n" + command;
                _inputField.text = "";
            }

            
            //Example use case: Loading a level via Command.
            //Beischpiel für die Verwendung von laden eines Levels über Befehl.
            switch (command)
            {
                //Command Name: LoadLevel_1
                case "LL_1":
                    SceneManager.LoadScene("Level1");
                    break;
                //Command Name: LoadLevel_2
                case "LL_PG":
                    SceneManager.LoadScene("Playground");
                    break;
            }
        }
        //Input to send password to access console.
        //Eingabe um Passwort zu senden um Konsole zu öffnen.
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string command = _inputFieldPassword.text;

            //Adding the command to the list of typed commands.
            //Hinzufügen des Befehls zur Liste der eingegebenen Befehle.
            typedCommands.Add(command);
            //Only adding the command to the consoleText (Display) if it is not empty.
            //Nur hinzufügen des Befehls zum consoleText (Anzeige) wenn er nicht leer ist.
            if (!string.IsNullOrEmpty(command))
            {
                _consoleTextPassword.text += "\n" + command;
                _inputFieldPassword.text = "";
            }
            
            //Checking if the passwas is correct and opening the console if it is.
            //Überprüfen ob das Passwort korrekt ist und öffnen der Konsole wenn es korrekt ist.
            if (command == _password)
            {
                togglePassword();
                toggleConsole();
            }
            else
            {
                _consoleTextPassword.text += "\n" + "Wrong Password";
            }
        }
        void toggleConsole()
        {
            bool currentstateConsole = _ConsoleParent.activeSelf;
            _ConsoleParent.SetActive(!currentstateConsole);
        }
        void togglePassword()
        {
            bool currentstatePassword = _PasswordParent.activeSelf;
            _PasswordParent.SetActive(!currentstatePassword);
        }
    }
}
}

