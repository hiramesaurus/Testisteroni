using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hiramesaurus.Testisteroni
{    
    public class HandOfGOD : MonoBehaviour
    {
        private Rigidbody rb;

        private Camera camera;
        private float distance;
        
        private void Start()
        {
            camera = GetComponent<Camera>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    rb = hit.rigidbody;
                    distance = Vector3.Distance(transform.position, hit.point);
                }
                else
                {
                    rb = null;
                }
                
            }

            if (Input.GetMouseButton(0))
            {
                
                Vector3 pos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, distance));
                
                
                rb.MovePosition(pos);
            }
        }
    }
}
