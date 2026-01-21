using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    IAA_Player controls = null;
    InputAction moveAction = null;
    InputAction rotateAction = null;
    InputAction interact = null;
    public InputAction MoveAction => moveAction;
    public InputAction RotateAction => rotateAction;
    public InputAction Interact => interact;



    private void Awake()
    {
        controls = new IAA_Player();
    }

    private void OnEnable()
    {
        moveAction = controls.Player.Move;
        rotateAction = controls.Player.Rotate;
        interact = controls.Player.Interact;

        moveAction.Enable();
        rotateAction.Enable();
        interact.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        rotateAction.Disable();
        interact.Disable();
    }
}