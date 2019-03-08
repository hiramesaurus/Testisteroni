using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace Hiramesaurus.Testisteroni
{    
    public class HandOfGOD : MonoBehaviour
    {
        private Rigidbody rb;
        private bool notThrow = true;

        private Camera camera;
        private float distance;

        [SerializeField]
        private float scrollScale = 1;
        
        [SerializeField]
        private float forcePower = 10;
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

                if (Physics.Raycast(ray, out hit, 100))
                {
                    rb = hit.rigidbody;
                    rb.velocity = Vector3.zero;
                    distance = Vector3.Distance(transform.position, hit.point);
                }
                else
                {
                    rb = null;
                }
                
            }

            if (Input.GetMouseButton(0) && notThrow)
            {
                distance += Input.mouseScrollDelta.y * scrollScale;
                Vector3 pos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, distance));
                
                
                rb?.MovePosition(pos);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    
                    Vector3 force = (rb.position - transform.position) * 50;
                    
                    rb.AddForce(force * forcePower);
                }

                
            }
        }
    }
}
