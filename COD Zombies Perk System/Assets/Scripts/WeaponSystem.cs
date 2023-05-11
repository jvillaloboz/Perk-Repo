using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponSystem : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpscamera;
    public ParticleSystem muzzleflash;
    public ParticleSystem perkflash;
    public GameObject impactEffect;

    public GameObject armPerk;
    public GameObject rifle;

    public Animator animator;
    public Animator armanimation;

    private float nextTimeToFire = 0f;

    public bool doubleTapOn = false;


    private void Awake()
    {
        armPerk.SetActive(false);
    }
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        
        isReloading = false;
        animator.SetBool("Reloading", false);
        armanimation.SetBool("ArmAnim", false);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (isReloading)
        {
            return;
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        
    }

    IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(1f);


        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        if (doubleTapOn == false)
        {
            muzzleflash.Play();
        }
        else
        {
            perkflash.Play();
            
        }


        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, range))
        {
            

             Zombie target = hit.transform.GetComponent<Zombie>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey("e") && other.gameObject.tag == "DoubleTap")
        {
            fireRate = 10f;
            doubleTapOn = true;
            armPerk.SetActive(true);
            animator.SetBool("ArmMove", true);
            armanimation.SetBool("ArmAnim", true);
            

        }
        
        
        if (Input.GetKey("e") && other.gameObject.tag == "SpeedCola")
        {
            reloadTime = 1f;
            armPerk.SetActive(true);
            animator.SetBool("ArmMove", true);
            armanimation.SetBool("ArmAnim", true);
            
        }

       if (!Input.GetKey("e"))
        {
            animator.SetBool("ArmMove", false);
            Invoke("hideObject", 1.6f);
        }


    }
    void hideObject()
    {
        armPerk.SetActive(false);
       
    }
}
