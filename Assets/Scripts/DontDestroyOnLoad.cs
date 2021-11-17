using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroyOnLoad : MonoBehaviour
{
    public Text gem;
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}
