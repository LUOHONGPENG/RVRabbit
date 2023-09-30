using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class InputMgr : MonoSingleton<InputMgr>
{
    private PlayerInput playerInput;

    private InputAction touchAction;
    private InputAction touchPositionAction;

    private bool isInitInput = false;

    public IEnumerator IE_Init()
    {
        InitInput();
        yield break;
    }

    private void InitInput()
    {
        if (!isInitInput)
        {
            playerInput = new PlayerInput();

            touchAction = playerInput.Gameplay.Touch;
            touchPositionAction = playerInput.Gameplay.TouchPosition;
            isInitInput = true;
        }
    }

    private void OnEnable()
    {
        EnableInput();
    }

    private void OnDisable()
    {
        DisableInput();
    }

    private void EnableInput()
    {
        if (playerInput == null)
        {
            InitInput();
        }

        playerInput.Enable();
        touchAction.performed += Touch_performed;
        touchAction.canceled += Touch_canceled;
    }

    private void DisableInput()
    {
        touchAction.performed -= Touch_performed;
        touchAction.canceled -= Touch_canceled;
        playerInput.Disable();
    }


    #region Drop

    private bool isDragging = false;
    private RoomFurniItem draggingFurni = null;

    private Ray GetMouseRay()
    {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();
        Ray ray = GameMgr.Instance.mapCamera.ScreenPointToRay(screenPosition);
        return ray;
    }

    private Vector2 GetMousePos()
    {
        return GameMgr.Instance.mapCamera.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
    }

    //Start Drag
    private void Touch_performed(InputAction.CallbackContext obj)
    {
        Debug.Log(GetMousePos());
        RaycastHit2D hit = Physics2D.Raycast(GetMousePos(),Vector2.zero,Mathf.Infinity, LayerMask.GetMask("Furniture"));

        if (hit.transform == null)
        {
            Debug.Log("NoHit");
            return;
        }

        if (hit.transform.parent.GetComponent<RoomFurniItem>() != null)
        {
            isDragging = true;
            Debug.Log("StartDragFurni");
            draggingFurni = hit.transform.parent.GetComponent<RoomFurniItem>();
        }

        //Debug.Log("Click "+ screenPosition);
    }

    //End Drag
    private void Touch_canceled(InputAction.CallbackContext obj)
    {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();
        Debug.Log("Canceled");

        if(isDragging && draggingFurni != null)
        {
            Ray ray = GetMouseRay();

            //Release At Floor

            //Release Outside
        }

        EndDragFurni();
    }

    private void EndDragFurni()
    {
        isDragging = false;
        draggingFurni = null;
    }
    #endregion

    #region Dragging

    private void CheckRayDrag()
    {
        if (isDragging && draggingFurni != null)
        {
            Ray ray = GetMouseRay();
            draggingFurni.transform.position = GetMousePos();
        }
    }



    #endregion

    private void FixedUpdate()
    {
        if (isInitInput)
        {
            CheckRayDrag();
        }
    }
}
