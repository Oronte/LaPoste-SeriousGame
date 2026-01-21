using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class MovementComponent : MonoBehaviour
{
    [SerializeField] Player owner = null;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] bool canMove = true;
    [SerializeField] bool canRotate = true;

    InputAction moveAction = null;
    InputAction rotateAction = null;

    public void SetCanMove(bool _canMove)
    {
        if (canMove == _canMove) return;
        canMove = _canMove;
    }

    public void Init(InputAction _moveAction, InputAction _rotateAction)
    {
        owner = GetComponent<Player>();
        moveAction = _moveAction;
        rotateAction = _rotateAction;
    }

    void FixedUpdate()
    {
        MoveManual();
        RotateManual();
    }

    void MoveManual()
    {
        if (!canMove) return;

        Vector2 _dir = moveAction.ReadValue<Vector2>();
        transform.position += transform.right * _dir.x * moveSpeed * Time.deltaTime;
        transform.position += transform.forward * _dir.y * moveSpeed * Time.deltaTime;
    }

    void RotateManual()
    {
        Vector2 _axis = rotateAction.ReadValue<Vector2>();
        transform.eulerAngles += transform.up * _axis.x * rotationSpeed * Time.deltaTime;
    }
}