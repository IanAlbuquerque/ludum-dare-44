using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rewired.Player player;

    [Header("Sensitivity")]
    public float deltaDeadzone = 0.2f;

    [Header("Collision")]

    public LayerMask collisionLayer;

    public int boulderLayer = 11;

    public AudioSource walkAudio;
    public AudioSource boulderAudio;
    
    void Awake() {
        this.player = Rewired.ReInput.players.GetPlayer("Player0");
    }

    // Start is called before the first frame update
    void Start()
    {
    }
    
    private void move(Vector2 movement) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, 1.0f, this.collisionLayer);
        if(hit.collider != null) {
            // must be boulder
            Vector3 posBoulder = hit.collider.gameObject.transform.position;
            posBoulder.x += movement.x;
            posBoulder.y += movement.y;
            hit.collider.gameObject.transform.position = posBoulder;
            this.boulderAudio.Play();    
        } else {
            Vector3 pos = this.transform.position;
            pos.x += movement.x;
            pos.y += movement.y;
            this.transform.position = pos;
            this.walkAudio.Play();    
        }

    }
    
    private void attemptMove(Vector2 movement) {
        if(this.canMove(movement)) {
            this.move(movement);
        }
    }

    public bool canMove(Vector2 movement) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, 1.0f, this.collisionLayer);
        if(hit.collider == null) {
            return true;
        } else {
            if(hit.collider.gameObject.layer == this.boulderLayer) {
                RaycastHit2D hitNext = Physics2D.Raycast(transform.position + 2 * new Vector3(movement.x, movement.y, 0.0f), -movement, 1.0f, this.collisionLayer);
                return hitNext.collider.gameObject == hit.collider.gameObject;
            } else {
                return false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.Instance.levelIsRunning) {
            if(this.player.GetButtonDown("MoveXAxis"))                  this.attemptMove(new Vector2(1.0f, 0.0f));
            else if(this.player.GetNegativeButtonDown("MoveXAxis"))     this.attemptMove(new Vector2(-1.0f, 0.0f));
            else if(this.player.GetButtonDown("MoveYAxis"))             this.attemptMove(new Vector2(0.0f, 1.0f));
            else if(this.player.GetNegativeButtonDown("MoveYAxis"))     this.attemptMove(new Vector2(0.0f, -1.0f));
        }
    }
}
