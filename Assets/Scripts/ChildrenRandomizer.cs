using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int childToUse = Random.Range(0, this.transform.childCount);
        for(int i=0; i<this.transform.childCount; i++) {
            this.transform.GetChild(i).gameObject.SetActive(i==childToUse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
