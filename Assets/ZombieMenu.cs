using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieMenu : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2,
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -20f;
    public float maximumVert = 20f;
    public float minimumHori = -30f;
    public float maximumHori = 30f;

    private float verticalRot = 0;
    private float horizontalRot = 0;

    private Button lastButtonClicked;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
        lastButtonClicked = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (axes == RotationAxes.MouseXAndY)
        {
            verticalRot -= mouseY * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            horizontalRot += mouseX * sensitivityHor;
            horizontalRot = Mathf.Clamp(horizontalRot, minimumHori, maximumHori);

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            horizontalRot += mouseX * sensitivityHor;
            horizontalRot = Mathf.Clamp(horizontalRot, minimumHori, maximumHori);

            transform.localEulerAngles = new Vector3(0, horizontalRot, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            verticalRot -= mouseY * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            transform.localEulerAngles = new Vector3(verticalRot, 0, 0);
        }

        // Check for mouse click
        if (Input.GetMouseButtonDown(0)) // Change the button number if needed
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has a ButtonScript attached
                Button buttonScript = hit.collider.GetComponent<Button>();

                if (buttonScript != null)
                {
                    // If a button was clicked, call its respective method
                    if (hit.collider.CompareTag("PlayButton"))
                    {
                        buttonScript.OnPlayClick();
                    }
                    else if (hit.collider.CompareTag("QuitButton"))
                    {
                        buttonScript.OnQuitClick();
                    }
                }
            }
        }
    }
}
