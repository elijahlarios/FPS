using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{

    private Camera cam;
    private float timeSinceLastShot = 0;
    public float cooldown = 0.1f;

    // private Coroutine currentCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 point = new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0);

            Ray ray = cam.ScreenPointToRay(point);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {

                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
            
                if (target != null) {
                    Vector3 hitDirection = transform.position - hitObject.transform.position;
                    ReactiveTarget.HitDirection direction = Vector3.Dot(hitDirection, transform.forward) > 0 ? ReactiveTarget.HitDirection.Forward : ReactiveTarget.HitDirection.Backward;
                    target.ReactToHit(direction);

                } else {
                    if (timeSinceLastShot >= cooldown) {
                       // StartCoroutine(SphereIndicator(hit.point));
                        timeSinceLastShot = 0;

                    }
                }
            }            
        }               
        timeSinceLastShot += Time.deltaTime;
    }

    private IEnumerator SphereIndicator(Vector3 pos) {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.localScale = Vector3.one * 0.1f;

        sphere.transform.position = pos;

        yield return new WaitForSeconds(10);

        Destroy(sphere);
    }

    private void OnGUI() {
        int size = 12;
        float posX = cam.pixelWidth/2 - size/4;
        float posY = cam.pixelHeight/2 - size/2;

        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
