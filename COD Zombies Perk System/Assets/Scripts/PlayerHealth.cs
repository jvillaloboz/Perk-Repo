using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float Health;

    public GameObject armPerk;
    public GameObject weapon;

    public GameObject zombie;
    public GameObject hitScreen;


    public Animator animator;
    public Animator armanimation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       if(hitScreen != null)
        {
            if(hitScreen.GetComponent<Image>().color.a > 0)
            {
                var color = hitScreen.GetComponent<Image>().color;
                color.a -= 0.01f;

                hitScreen.GetComponent<Image>().color = color;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey("e") && other.gameObject.tag == "Juggernog")
        {
            armPerk.SetActive(true);
            Health = 5f;
            animator.SetBool("ArmMove", true);
            armanimation.SetBool("ArmAnim", true);
        }
        if (!Input.GetKey("e"))
        {
            
            Invoke("hideObject", 1.6f);
            animator.SetBool("ArmMove", false);
        }
    }
    void hideObject()
    {
        armPerk.SetActive(false);

    }

    public void takeDamage(float amount)
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        var Speed = GetComponent<PlayerMovement>();
        var Jump = GetComponent<PlayerMovement>();
        var zSpeed = GetComponent<Zombie>();

        if (collision.gameObject.tag == "Zombie")
        {
            Health--;
            HPScreen();
        }
       
        if (Health <= 0)
        {
            Destroy(zombie.gameObject);
            weapon.SetActive(false);
            Speed.speed = 0f;
            Jump.jump = 0f;
           
        }
    }

    private void HPScreen()
    {
        var color = hitScreen.GetComponent<Image>().color;
        color.a = 0.8f;

        hitScreen.GetComponent<Image>().color = color;


    }

}

