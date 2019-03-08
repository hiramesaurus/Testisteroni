using UnityEngine;

namespace Hiramesaurus.Testisteroni
{
    public class FreeCamera : MonoBehaviour
    {
        public float FlightSpeed = 3;
        public float Acceleration = 8;

        [Header ("Look")]
        public float HorizontalLook = 300;
        public float VerticalLook = 8;

        public bool InvertY = true;      
        public Vector2 VerticalLimits = new Vector2 (-60, 60);

        public bool RequireInput;
        public KeyCode LookKey = KeyCode.Mouse1;
        
        [Header ("Movement")]
        public string Horizontal = "Horizontal";
        public string Forward = "Vertical";
        public string Vertical = "UpDown";
     
        private float currentSpeed;
        private float currentVelocity;

        private Vector2 currentRotation;
        
        private Transform attachedTransform;

        private void Awake ()
        {
            attachedTransform = GetComponent<Transform> ();
        }

        private void Update ()
        {
            var deltaTime = Time.deltaTime;
            
            var movementInput = new Vector3 (
                Input.GetAxis (Horizontal),
                Input.GetAxis (Vertical),
                Input.GetAxis (Forward));

            if (!RequireInput || Input.GetKey (LookKey))
            {
                var lookInput = new Vector2(
                    Input.GetAxis ("Mouse Y") * VerticalLook * deltaTime,
                    Input.GetAxis ("Mouse X") * HorizontalLook * deltaTime);

                if (InvertY)
                    lookInput.x = -lookInput.x;
            
            
                currentRotation += lookInput;
                currentRotation.x = Mathf.Clamp (currentRotation.x, VerticalLimits.x, VerticalLimits.y);
                currentRotation.y %= 360 * Mathf.Sign (currentRotation.y);
            }
            
            movementInput = attachedTransform.TransformDirection (movementInput);
            currentSpeed = Mathf.SmoothDamp (currentSpeed, FlightSpeed, ref currentVelocity, deltaTime * Acceleration);

            var newPosition = attachedTransform.position + movementInput * currentSpeed * deltaTime;
            attachedTransform.SetPositionAndRotation (newPosition, Quaternion.Euler (currentRotation));
        }
    }

}