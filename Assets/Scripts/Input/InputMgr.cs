using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
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

    private Vector2 GetMousePos()
    {
        return GameMgr.Instance.mapCamera.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
    }

    //Start Drag
    private void Touch_performed(InputAction.CallbackContext obj)
    {
        if (!isInitInput)
        {
            return;
        }

        switch (GameMgr.Instance.interactType)
        {
            case InteractType.Move:
            case InteractType.Shop:
                StartDrag();
                break;
            case InteractType.Action:
                ClickAction();
                break;
        }
    }

    //End Drag
    private void Touch_canceled(InputAction.CallbackContext obj)
    {
        if (!isInitInput)
        {
            return;
        }

        switch (GameMgr.Instance.interactType)
        {
            case InteractType.Move:
            case InteractType.Shop:
                EndDrag();
                break;
        }
    }

    private void EndDragFurni()
    {
        isDragging = false;
        draggingFurni = null;
    }
    #endregion

    #region Dragging
    private void StartDrag()
    {
        RaycastHit2D hit = Physics2D.Raycast(GetMousePos(), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Furniture"));

        if (hit.transform == null)
        {
            Debug.Log("NoHitFurni");
            return;
        }

        if (hit.transform.parent.GetComponent<RoomFurniItem>() != null)
        {
            isDragging = true;
            Debug.Log("StartDragFurni");
            draggingFurni = hit.transform.parent.GetComponent<RoomFurniItem>();
        }
    }

    private void EndDrag()
    {
        if (isDragging && draggingFurni != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(GetMousePos(), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Floor"));

            //Release Outside
            if (hit.transform == null)
            {
                Debug.Log("ReleaseOutside");
                draggingFurni.SetPosID(new Vector2Int(-1, -1),false);
                PublicTool.RefreshOccupy();
                EndDragFurni();
                return;
            }

            //Release At Floor
            if (hit.transform.GetComponent<RoomFloorItem>() != null)
            {
                Debug.Log("ReleaseAtFloor");
                RoomFloorItem floor = hit.transform.GetComponent<RoomFloorItem>();

                Vector2Int tarPos = floor.GetPosID();
                Vector2Int size = draggingFurni.GetSize();

                if (!PublicTool.CheckRoomOccupy(draggingFurni.GetKeyID(), tarPos, size))
                {
                    draggingFurni.SetPosID(floor.GetPosID(),false);
                    PublicTool.RefreshOccupy();
                }
                else
                {
                    //Back
                    draggingFurni.BackPos();
                }

                //Set Position
            }
        }

        EndDragFurni();
    }


    private void CheckRayDrag()
    {
        if (isDragging && draggingFurni != null)
        {
            draggingFurni.transform.position = GetMousePos();
        }
    }



    #endregion

    #region Havor

    private void CheckHavor()
    {
        if (!isDragging)
        {

            RaycastHit2D hit = Physics2D.Raycast(GetMousePos(), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Furniture"));

            if (hit.transform == null)
            {
                EventCenter.Instance.EventTrigger("HideTip", null);
                return;
            }

            if (hit.transform.parent.GetComponent<RoomFurniItem>() != null)
            {
                RoomFurniItem havorFurni = hit.transform.parent.GetComponent<RoomFurniItem>();
                FurnitureExcelItem furniExcel = havorFurni.GetFurniData();

                ShowTipStruct showTipInfo = new ShowTipStruct(furniExcel.name, havorFurni.Level, furniExcel.desc, furniExcel.furnitureType.ToString(),
                    havorFurni.CoinChange, havorFurni.EnergyChange, havorFurni.TaskChange, furniExcel.GetSupportEffectDesc, GetMousePos());

                EventCenter.Instance.EventTrigger("ShowTip", showTipInfo);
            }
        }
        else
        {
            EventCenter.Instance.EventTrigger("HideTip", null);

        }

    }

    #endregion



    #region Click

    private void ClickAction()
    {
        RaycastHit2D hit = Physics2D.Raycast(GetMousePos(), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Furniture"));

        if (hit.transform == null)
        {
            Debug.Log("NoHitFurni");
            return;
        }

        if (hit.transform.parent.GetComponent<RoomFurniItem>() != null)
        {
            RoomFurniItem clickFurni = hit.transform.parent.GetComponent<RoomFurniItem>();

            clickFurni.ClickDeal();
        }
    }


    #endregion



    private void FixedUpdate()
    {
        if (isInitInput)
        {
            CheckRayDrag();
            CheckHavor();
        }
    }
}
