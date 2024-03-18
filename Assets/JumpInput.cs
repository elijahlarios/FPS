using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpInput : MonoBehaviour
{

        private float default_y = 0.8299999f;
        public float jumpForce = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Space) )
        {
            // Jump
            this.transform.position = new Vector3(this.transform.position.x, default_y + jumpForce, this.transform.position.z);
            
        } else if( Input.GetKeyUp(KeyCode.Space) )
        {
            // Land
            this.transform.position = new Vector3(this.transform.position.x, default_y, this.transform.position.z);
        }
        
    }
}
