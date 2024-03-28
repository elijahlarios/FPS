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
   // public CamShake camShake;
   // public float camShakeMagnitude, cameShakeDuration;
    public TextMeshProUGUI text;

    private void Awake(){
        bulletsLeft = magazineSize;
        readyToShoot = true;
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
            Shoot();
        }
        // just added this here bc its an input function, we can refactor later
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }

    }
    private void Shoot()
    {
        readyToShoot = false;
        bool isZombie = false;
        //spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        //Raycast
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range))
        {
            ReactiveTarget target = rayHit.transform.gameObject.GetComponent<ReactiveTarget>();
            if (target)
            {
                isZombie = true;
                target.ReactToHit(ReactiveTarget.HitDirection.Backward, damage);
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

    }
    private void ReloadFinished(){
        bulletsLeft = magazineSize;
        reloading = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

   
   
}
