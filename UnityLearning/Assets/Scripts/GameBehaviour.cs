using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText; 
        Time.timeScale = 0f;
    }
    public bool showWinScreen = false;
    public string labelText  = "Collect all 4 items and win your freedom!";
    public int maxItems =4;
    public Button LossButton;

    public Text HealthText; 
    public Text ProgressText;
    public Text ItemText;
    public Button WinButton; 


    private int _itemsCollected =0;
    public int Items
    {
        get { return _itemsCollected; }
        set 
        {
                _itemsCollected = value;
                ItemText.text = "Items Collected: " +Items;
                if (_itemsCollected>= maxItems)
                {
                  WinButton.gameObject.SetActive(true);
                  UpdateScene("You've found all the items!");
                }
            else
            {
                ProgressText.text = "Item Found, only " + (maxItems - _itemsCollected) +" more to go!";
            }
        }
    }
    private int _playerHP = 10;
    public int HP
    {
        get{ return _playerHP; }
        set{
            _playerHP = value;
                HealthText.text = "Player Health:" +HP ;

            if(_playerHP <=0)
            {
                LossButton.gameObject.SetActive(true);
                UpdateScene("You want another life with that?");
            }
            else
            {
                ProgressText.text = "Ouch... that;s got to hurt. "; 
            }

            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect (20,20,150,25), "Player Health:" + _playerHP);
        GUI.Box(new Rect(20,50,150,25), "Items Collected: "+ _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 -100, Screen.height - 50,300,50), labelText);

        if(showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width/2 - 100,Screen.height/2 -50 ,200,100), "YOU WON!"))
            {
                SceneManager.LoadScene(0);
                Time.timeScale =1.0f;
            }
        }
    }
}
