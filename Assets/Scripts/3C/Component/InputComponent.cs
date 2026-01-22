using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    IAA_Player controls = null;
    InputAction moveAction = null;
    InputAction rotateAction = null;
    InputAction interact = null;
    InputAction leftHandPos = null;
    InputAction rightHandPos = null;
    public InputAction MoveAction => moveAction;
    public InputAction RotateAction => rotateAction;
    public InputAction Interact => interact;
    public InputAction LeftHandPos => leftHandPos;
    public InputAction RightHandPos => rightHandPos;



    private void Awake()
    {
        controls = new IAA_Player();
    }

    private void OnEnable()
    {
        moveAction = controls.Player.Move;
        rotateAction = controls.Player.Rotate;
        interact = controls.Player.Interact;
        leftHandPos = controls.LeftController.Position;
        rightHandPos = controls.RightController.Position;

        moveAction.Enable();
        rotateAction.Enable();
        interact.Enable();
        leftHandPos.Enable();
        rightHandPos.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        rotateAction.Disable();
        interact.Disable();
        leftHandPos.Disable();
        rightHandPos.Disable();
    }
}