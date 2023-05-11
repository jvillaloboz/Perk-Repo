using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    // Start is called before the first frame update

    public float health = 50f;
    public GameObject Target;
    public float speed;
    void Update()
    {
        transform.LookAt(Target.gameObject.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "FPS")
        {
            var Health = collision.gameObject.GetComponent<PlayerHealth>();
            if (Health != null)
            {
                Health.takeDamage(1);
            }
        }
        Debug.Log("hit");
    }
}

