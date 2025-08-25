using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }

    private State currentState = State.Ready;

    public State CurrentState
    {         
        get { return currentState; }
        private set { currentState = value; }
    }   

    public GunData gunData;

    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletImpactEffect;

    private LineRenderer lineRenderer;
    private AudioSource audioSource;

    public Transform bulletSpawnPoint;

    public int currentAmmo;
    public int currentMagazine;

    private float lastFireTime;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        currentAmmo = gunData.startingAmmo;
        currentMagazine = gunData.magazineSize;
        currentState = State.Ready;
        lastFireTime = 0f;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(ShotEffect());
        //}

        switch(currentState)
        {
            case State.Ready:
                UpdateReadyState();
                break;
            case State.Empty:
                UpdateEmptyState();
                break;
            case State.Reloading:
                UpdateReloadingState();
                break;
        }
    }

    public void Fire()
    {
       if (currentState == State.Ready && Time.time >= lastFireTime + gunData.fireRate)
       {
           lastFireTime = Time.time;
           Shoot();
        }
    }
    
    private void Shoot()
    {
        Vector3 hitPos = Vector3.zero;

        RaycastHit hit;
        if (Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.forward, out hit, gunData.range))
        {
            hitPos = hit.point;
            var target = hit.collider.GetComponent<IDamagable>();
            if (target != null)
            {
                target.TakeDamage(gunData.damage, hit.point, hit.normal);
            }
            //hit.collider.SendMessage("TakeDamage", gunData.damage, SendMessageOptions.DontRequireReceiver);
            //Instantiate(bulletImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
        else
        {
            hitPos = bulletSpawnPoint.position + bulletSpawnPoint.forward * gunData.range;
        }


        StartCoroutine(ShotEffect(hitPos));

        currentAmmo--;
        Debug.Log("Ammo: " + currentAmmo + " / " + currentMagazine);
        if (currentAmmo <= 0)
        {
            currentState = State.Empty;
        }
    }

    public bool Reload()
    {
        bool isReload = currentState == State.Empty && currentMagazine > 0 || currentState == State.Ready && currentMagazine > 0 && currentAmmo < gunData.magazineSize;
        if (isReload)
        {
            currentState = State.Reloading;
            StartCoroutine(ReloadCoroutine());
        }
        return isReload;
    }

    private IEnumerator ReloadCoroutine()
    {
        audioSource.PlayOneShot(gunData.reloadSound);

        yield return new WaitForSeconds(gunData.reloadTime);
        int ammoNeeded = gunData.magazineSize - currentAmmo;
        if (currentMagazine >= ammoNeeded)
        {
            currentAmmo += ammoNeeded;
            currentMagazine -= ammoNeeded;
        }
        else
        {
            currentAmmo += currentMagazine;
            currentMagazine = 0;
        }
        Debug.Log("Ammo: " + currentAmmo + " / " + currentMagazine);
        currentState = State.Ready;
    }

    private void UpdateEmptyState()
    {
        //if (Input.GetKeyDown(KeyCode.R) && currentMagazine > 0)
        //{
        //    currentState = State.Reloading;
        //    StartCoroutine(Reload());
        //}
    }

    private void UpdateReadyState()
    {
        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    Fire();
        //}

        //if (Input.GetKeyDown(KeyCode.R) && currentMagazine > 0 && currentAmmo < gunData.magazineSize)
        //{
        //    currentState = State.Reloading;
        //    StartCoroutine(Reload());
        //}
    }


    private void UpdateReloadingState()
    {
       
    }

    private IEnumerator ShotEffect(Vector3 hitPos)
    {
        audioSource.PlayOneShot(gunData.shootSound);

        muzzleFlash.Play();
        audioSource.Play();
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, bulletSpawnPoint.position);

        //Vector3 endPos = bulletSpawnPoint.position + bulletSpawnPoint.forward * 10f;
        lineRenderer.SetPosition(1, hitPos);

        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
    }

}
