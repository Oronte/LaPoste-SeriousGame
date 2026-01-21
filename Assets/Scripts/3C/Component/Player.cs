using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] InputComponent inputs = null;
    [SerializeField] MovementComponent movement = null;

    [SerializeField] Rigidbody rb = null;

    public InputComponent Inputs => inputs;
    public MovementComponent Movement => movement;
    public Rigidbody Rigidbody => rb;

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Init()
    {
        inputs = GetComponent<InputComponent>();
        movement = GetComponent<MovementComponent>();
        rb = GetComponent<Rigidbody>();

        if (!inputs || !movement) return;
        movement.Init(inputs.MoveAction, inputs.RotateAction);

        //Cursor.lockState = CursorLockMode.Locked;
    }
}