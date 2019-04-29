using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stringer : MonoBehaviour
{
    public int brushType;
    public string getString() {
        return this.brushType + ";" + this.transform.position.x + ";" + this.transform.position.y;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
