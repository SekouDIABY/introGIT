using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;                                      //Le joueur peut marcher, courir avec la touche shift gauce et sauter avec la touche espace

    public float jumpForce = 7f;


    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    private float gravity = 10f;                                    //La vitesse de son regard
    private float upperLookLimit = 80f;
    private float lowerLookLimit = 80f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        float moveDirectionY = moveDirection.y;                                             // Récupère les axes de mouvement
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionZ = Input.GetAxis("Vertical");


        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;                   // Détecte le sprint


        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        moveDirection = (forward * moveDirectionZ + right * moveDirectionX) * speed;            // Calcul des mouvements en X et Z

        // Gestion du saut
        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            moveDirection.y = jumpForce;
        }
        else
        {
            moveDirection.y = moveDirectionY;
        }


        if (!characterController.isGrounded)                                                 // Applique la gravité si le personnage ne touche pas le sol
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }


        characterController.Move(moveDirection * Time.deltaTime);                            // Applique le mouvement au CharacterController


        if (playerCamera)                                                                           // Gère la rotation de la caméra 
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
            rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);


            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);     // Gère la rotation du joueur (gauche-droite)
        }
    }
}
