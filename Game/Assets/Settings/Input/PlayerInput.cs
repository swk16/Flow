using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "PlayerInput", order = 0)]
public class PlayerInput : ScriptableObject, InputActions.IGameActions
{

    InputActions inputActions;

    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction onStopMove = delegate { };

    private void OnEnable() {
        inputActions = new InputActions();
        inputActions.Game.SetCallbacks(this);
        //inputActions.Menu.SetCallbacks(this);
    }

    private void OnDisable() {
        DisableAllInput();
    }

    public void DisableAllInput() {
        inputActions.Game.Disable();
        //inputActions.Menu.Disbale();
    }

    public void EnableGameInput() {
        inputActions.Game.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DisableGameInput(){
        inputActions.Game.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed){
            onMove.Invoke(context.ReadValue<Vector2>());
        }else if(context.phase == InputActionPhase.Canceled){
            onStopMove.Invoke();
        }
    }
}
