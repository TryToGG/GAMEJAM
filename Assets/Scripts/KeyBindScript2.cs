using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindScript2 : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>(); //saving keybinds and controls ex)move up, W
    public TextMeshProUGUI up, left, down, right, select, cancel;
    private GameObject currentkey;

    void Start()
    {
        keys.Add("Up", KeyCode.UpArrow);
        keys.Add("Left", KeyCode.LeftArrow);
        keys.Add("Down", KeyCode.DownArrow);
        keys.Add("Right", KeyCode.RightArrow);
        keys.Add("Select", KeyCode.LeftBracket);
        keys.Add("Cancel", KeyCode.RightBracket);

        up.text = keys["Up"].ToString();
        left.text = keys["Left"].ToString();
        down.text = keys["Down"].ToString();
        right.text = keys["Right"].ToString();
        select.text = keys["Select"].ToString();
        cancel.text = keys["Cancel"].ToString();
    }

    private void OnGUI() // sensitively catches key input
    {
        if (currentkey != null)
        {
            Event e = Event.current;
            if (e.isKey) //if the event is key press
            {
                keys[currentkey.name] = e.keyCode; //go to the dictionary and find the original key
                currentkey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString(); //change the text to a new key
                currentkey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked) //clicked == the button that is clicked
    {
        currentkey = clicked; //currentkey is something like "Up" (Button's name)

    }

    //public void SaveKeys()
    //{
    //    foreach(var key in keys) //referring dictionary keys
    //    {
    //        PlayerPrefs.SetString(key.Key, key.Value.ToString()); //<string, Keycode>
    //    }

    //    PlayerPrefs.Save();
    //}
}