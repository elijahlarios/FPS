using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class weapons : MonoBehaviour
{
    //Gun Information
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic, bloodHit;
    public AudioClip shootSound, reloadSound; // Audio clips for shooting and reloading
    // Volume setting for each gun (range from 0 to 1 internally)
    [Range(0, 100)]
    public float volumeLevel = 100.0f;
    private AudioSource audioSource;

    // public CamShake camShake;
    // public float camShakeMagnitude, cameShakeDuration;
    public TextMeshProUGUI text;

    private void Awake(){
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update(){
        MyInput();

        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void MyInput(){
        if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0){
            bulletsShot = bulletsPerTap;
            PlaySound(shootSound);
            Shoot();
        } 
        else if (Input.GetKeyUp(KeyCode.Mouse0) && timeBetweenShooting < 0.4) // AK specific - stop sound after keyup
        {
            StopSound();
        }

       


    }
    private void Shoot()
    {
        readyToShoot = false;
        bool isZombie = false;
        bool isBoss = false;
        //spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        //Raycast
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range))
        {
            ReactiveTarget target = rayHit.transform.gameObject.GetComponent<ReactiveTarget>();
            bossTarget boss = rayHit.transform.gameObject.GetComponent<bossTarget>();
            if (boss)
            {
                isBoss = true;
                boss.ReactToHit(bossTarget.HitDirection.Backward, damage);
                Debug.Log("Boss Hit");
            }
            else if (target)
            {
                isZombie = true;
                target.ReactToHit(ReactiveTarget.HitDirection.Backward, damage);
                Debug.Log("Z Hit");
            }
        }

        //ShakeCamera
        // camShake.Shake(camShakeDuration,camShakeMagnitude);

        //Graphics
        if (isZombie)
        {
            // Blood effect 
            Instantiate(bloodHit, rayHit.point, rayHit.rigidbody.rotation);
        }
        else if (isBoss)
        {
            // Blood effect 
            Instantiate(bloodHit, rayHit.point, rayHit.rigidbody.rotation);
        }
        // Bullethole effect
        else { Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal, Vector3.up)); }
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);


        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot(){
        readyToShoot = true;
    }

    private void Reload(){
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
        PlaySound(reloadSound);

    }
    private void ReloadFinished(){
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null )
        {
            // Convert volume level from 0-100 to 0-1
            float volume = volumeLevel / 100f;
            audioSource.volume = volume; // Set volume based on volume level
            if (timeBetweenShooting > 0.4) {
                audioSource.PlayOneShot(clip);
            } else if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(clip); // AK specific - avoid overlapping audio
            }
        }
    }

    private void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void IncreaseAmmo(int amount)
    {
        magazineSize += amount;
        text.SetText(bulletsLeft + " / " + magazineSize); 
        // Debug.Log("Ammo increased. New mag size: " + magazineSize);

    }

}
