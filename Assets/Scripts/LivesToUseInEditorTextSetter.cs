using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesToUseInEditorTextSetter : MonoBehaviour
{
    public Text livesUI;
    public Text crditsUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.livesUI.text = "Lives: " + GameController.Instance.livesToUse.ToString();
        this.crditsUI.text = "Credits: " + GameController.Instance.currentCredits.ToString() + "/" + GameController.Instance.creditsAvailable.ToString();
    }
}
