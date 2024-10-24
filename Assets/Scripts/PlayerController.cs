using System.Collections;
using TMPro;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.1f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject enemyObject;

    private Rigidbody rb;
    private int count;
    //private float movementX;
    //private float movementY;
    //public XRController rightHand;
    public XRBaseController rightHandController; // Use Right Hand controller
    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        enemyObject.SetActive(false);
    }

    IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(5);
        // Debug.Log("Hello!");  
    }

    //void OnMove(InputValue movementValue)
    //{
    //    Vector2 movementVector = movementValue.Get<Vector2>();

    //    movementX = movementVector.x;
    //    movementY = movementVector.y;
    //}
    void Update()
    {
        // Get the controller's rotation
        //if (rightHandController != null && rightHandController.inputDevice.isValid)
        //{
        //    Quaternion controllerRotation = rightHandController.inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out var rotation) ? rotation : Quaternion.identity;
        //    movementDirection = controllerRotation * Vector3.forward;
        //}
        //if (rightHandController != null)
        //{
        //    // Get the rotation of the controller
        //    Quaternion controllerRotation = rightHandController.transform.rotation;

        //    Vector3 forwardDirection = controllerRotation * Vector3.forward;

        //    // Get the tilt angle (pitch) of the controller
        //    float pitchAngle = Vector3.SignedAngle(Vector3.up, forwardDirection, rightHandController.transform.right);

        //    // Adjust the movement direction based on pitch angle
        //    if (pitchAngle > 5f)  // Tilt up threshold
        //    {
        //        // If tilted up beyond 5 degrees, move backward
        //        movementDirection = -forwardDirection;
        //    }
        //    else if (pitchAngle < -5f)  // Tilt down threshold
        //    {
        //        // If tilted down beyond -5 degrees, move forward
        //        movementDirection = forwardDirection;
        //    }
        //    else
        //    {
        //        // If within the small angle range, don't move
        //        movementDirection = Vector3.zero;
        //    }
        //}
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 13)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
    void FixedUpdate()
    {   
        Vector3 movement = new Vector3(movementDirection.x, 0.0f, movementDirection.z);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            // Debug.Log("You Lose!");
            Destroy(gameObject);
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }
}
