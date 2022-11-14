using UnityEngine;

public class Player : MonoBehaviour
{
    private bool thrusting;
    public float thrustSpeed = 1.0f;
    private float turnDirection;

    public float turnSpeed = 0.1f;

    public Bullet bulletPrefab;
    private Rigidbody2D rb;

    private void Awake(){

        rb = GetComponent<Rigidbody2D>();
    }


    private void Update(){

        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1.0f;

        }else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){

            turnDirection =-1.0f;

        }else{
            
            turnDirection = 0.0f;

        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Shoot();
        }
        
   }

    private void FixedUpdate(){
        
        if (thrusting)
        {
            rb.AddForce(this.transform.up * this.thrustSpeed);
        }

        if (turnDirection != 0.0f)
        {
            rb.AddTorque(turnDirection * this.turnSpeed);
        }

        
    }

    private void Shoot(){

        Bullet bullet = Instantiate(this.bulletPrefab,this.transform.position,this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Asteroid")
        {
            rb.velocity=Vector3.zero;
            rb.angularVelocity=0.0f;
            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied(); 
        }
    }


}
