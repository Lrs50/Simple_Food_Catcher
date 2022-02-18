using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    public GameManager gameManager;
    public AudioSource scoreSound;
    public ParticleSystem explosion;


    private Vector2 _direction = Vector2.zero;
    private Vector2 _foodPos = Vector2.zero;
    private float speed = 10f;
    private float rotationSpeed = 5f;
    private Rigidbody2D _rigidbody;



    private void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        explosion.Stop();
    }


    private void FixedUpdate()
    {
        Move();
        if(_direction!=Vector2.zero){
            _rigidbody.AddForce(_direction*speed);
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward,_direction)*Quaternion.Euler(0,0,45);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,toRotation,rotationSpeed);
        }
    }


    private void Move()
    {

        _direction = new Vector2(_foodPos.x-this.transform.position.x,_foodPos.y-this.transform.position.y);
        _direction.Normalize();

    }

    public void GetFoodPos(Vector2 pos){
        this._foodPos = pos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Food"){

            explosion.transform.position = other.GetContact(0).point;

            explosion.Play();
            scoreSound.Play();
            

            Destroy(other.gameObject);
            ResetPhysics();
            gameManager.SpawnFood();
        }
    }


    private void ResetPhysics()
    {
        this._rigidbody.velocity = Vector2.zero;
        this._rigidbody.angularVelocity = 0f;
        this._direction = Vector2.zero;
    }

    


}
