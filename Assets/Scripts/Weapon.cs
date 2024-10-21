using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{

    public GunSO weapon;
    private float nextShotTime = 0f;
    private int currentBullets;
    private int totalBullets;
    private bool isReloading = false;

    [SerializeField] private ParticleSystem muzzle;

    private Animator animator;
    public TMP_Text ammoText;

    private Transform cam;

    private void OnEnable()
    {
        UpdateUI();
    }

    private void Start()
    {
        cam = Camera.main.transform;
        animator = GetComponent<Animator>();
        currentBullets = weapon.maxBullets;
        totalBullets = weapon.totalAmmo;
        UpdateUI();
    }

    void FixedUpdate()
    {
        if (isReloading) return;

        if(currentBullets <= 0 && totalBullets >= 1)
        {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButton("Fire1") && Time.time >= nextShotTime && currentBullets >= 1)
        {
            animator.SetBool("shoot", true);
            nextShotTime = Time.time + 1f / weapon.fireInterval;
            Shoot();
        }
        else
        {
            animator.SetBool("shoot", false);
        }
    }

    private void Shoot()
    {
        muzzle.Play();
        currentBullets--;
        UpdateUI();

        RaycastHit hitInfo;
        if(Physics.Raycast(cam.position, cam.transform.forward, out hitInfo, weapon.range))
        {
            Debug.Log(hitInfo.transform);
            EnemyHealth enemy = hitInfo.transform.GetComponent<EnemyHealth>();
            if(enemy != null)
            {
                enemy.TakeDamage(weapon.damage);
            }
            Instantiate(weapon.hitParticle, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("shoot", false);
        animator.SetBool("reload", true);
        yield return new WaitForSeconds(weapon.reloadTime);
        isReloading = false;
        animator.SetBool("reload", false);

        if (totalBullets <= weapon.maxBullets)
        {
            currentBullets = totalBullets;
            totalBullets = 0;
        }
        else
        {
            totalBullets -= weapon.maxBullets - currentBullets;
            currentBullets = weapon.maxBullets;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        ammoText.text = currentBullets.ToString() + " / " + totalBullets.ToString();
    }
}
