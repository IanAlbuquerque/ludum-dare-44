using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSource : MonoBehaviour
{
    public GameObject laserBeamPrefab;

    private GameObject nextObj;
    public LayerMask collisionLayer;

    public Vector2 propagateDirection;
    
    // Start is called before the first frame update
    void Start()
    {
       this.handlePropagation(); 
    }

    // Update is called once per frame
    void Update()
    {
        this.handlePropagation();
    }

    public void depropagate() {
        if(this.nextObj == null) {
            Destroy(this.gameObject);
        } else {
            this.nextObj.GetComponent<LaserSource>().depropagate();
            this.nextObj = null;
            Destroy(this.gameObject);
        }
    }

    public void handlePropagation() {
        if(GameController.Instance.levelIsRunning) {
            if(this.nextObj == null) {
                if(this.canPropagate(propagateDirection)) {
                    this.nextObj = Instantiate(this.laserBeamPrefab, this.transform.position + new Vector3(this.propagateDirection.x, this.propagateDirection.y, 0.0f), Quaternion.identity, GameController.Instance.tempGrid.transform);
                }
            } else {
                if(!this.canPropagate(propagateDirection)) {
                    this.nextObj.GetComponent<LaserSource>().depropagate();
                    this.nextObj = null;
                }
            }
        }
    }

    public bool canPropagate(Vector2 movement) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(movement.x, movement.y, 0.0f), -movement, 1.0f, this.collisionLayer);
        return hit.collider == null || (hit.collider != null && hit.collider.gameObject == this.gameObject);
    }
}
