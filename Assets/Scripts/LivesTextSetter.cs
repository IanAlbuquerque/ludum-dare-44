using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesTextSetter : MonoBehaviour
{
    public Text textUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.textUI.text = "Lives: " + GameController.Instance.lives.ToString();
    }
}
