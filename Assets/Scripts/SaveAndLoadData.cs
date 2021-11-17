using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveAndLoadData : MonoBehaviour
{
    public Text gem;
    string test;
    
    public void saveData()
    {
       PlayerPrefs.SetString("gem",gem.text);
       GameObject.Find("DontDestroyOnLoad").GetComponent<DontDestroyOnLoad>().gem = gem;
    }

    public void loadData()
    {
      test = PlayerPrefs.GetString("gem",gem.text);
    }
}
