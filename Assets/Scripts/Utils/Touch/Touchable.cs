using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using System.Linq;

public class Touchable : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public TMP_Text tmpText;
    public Collider objCollider;
    public Collider2D objCollider2D;
    public Camera referenceCamera;
    public bool autoAssignReferences = false;
    public bool inputEnabled = true;
    public bool autoComputeMouseWorldPosition = false;
    public bool requireClickInside = true;
    public bool block3dRaycasts = true;
    public bool ignoreOtherBlocking3dRaycasts = false;

    public bool Clicked { get; private set; } = false;
    public bool Hovering { get; private set; } = false;
    public Vector3 MouseWorldPosition { get; private set; }
    public delegate void OnClickStartedCallBackDelegate(Touchable touchable);
    public OnClickStartedCallBackDelegate OnClickStartedCallBack;
    public delegate void OnClickStartedInsideCallBackDelegate(Touchable touchable);
    public OnClickStartedInsideCallBackDelegate OnClickStartedInsideCallBack;
    public delegate void OnClickStartedOutsideCallBackDelegate(Touchable touchable);
    public OnClickStartedOutsideCallBackDelegate OnClickStartedOutsideCallBack;
    public delegate void OnClickHoldingCallBackDelegate(Touchable touchable);
    public OnClickHoldingCallBackDelegate OnClickHoldingCallBack;
    public delegate void OnClickHoldingInsideCallBackDelegate(Touchable touchable);
    public OnClickHoldingInsideCallBackDelegate OnClickHoldingInsideCallBack;
    public delegate void OnClickHoldingOutsideCallBackDelegate(Touchable touchable);
    public OnClickHoldingOutsideCallBackDelegate OnClickHoldingOutsideCallBack;
    public delegate void OnClickEndedCallBackDelgate(Touchable touchable);
    public OnClickEndedCallBackDelgate OnClickEndedCallBack;
    public delegate void OnClickEndedInsideCallBackDelgate(Touchable touchable);
    public OnClickEndedInsideCallBackDelgate OnClickEndedInsideCallBack;
    public delegate void OnClickEndedOutsideCallBackDelegate(Touchable touchable);
    public OnClickEndedOutsideCallBackDelegate OnClickEndedOutsideCallBack;
    public delegate void OnHoverStartedCallBackDelegate(Touchable touchable);
    public OnHoverStartedCallBackDelegate OnHoverStartedCallBack;
    public delegate void OnHoverCallBackDelegate(Touchable touchable);
    public OnHoverCallBackDelegate OnHoverCallBack;
    public delegate void OnHoverEndedCallBackDelegate(Touchable touchable);
    public OnHoverEndedCallBackDelegate OnHoverEndedCallBack;

    public List<SpriteRenderer> OtherIntersectRenderers { get; set; }
       = new List<SpriteRenderer>();

    protected virtual void OnClickStarted() { }
    protected virtual void OnClickStartedInside() { }
    protected virtual void OnClickStartedOutside() { }
    protected virtual void OnClickHolding() { }
    protected virtual void OnClickHoldingInside() { }
    protected virtual void OnClickHoldingOutside() { }
    protected virtual void OnClickEnded() { }
    protected virtual void OnClickEndedInside() { }
    protected virtual void OnClickEndedOutside() { }
    protected virtual void OnHoverStarted() { }
    protected virtual void OnHover() { }
    protected virtual void OnHoverEnded() { }
    private Vector3 deltaMouse;
    public bool followMouse = false;
    public bool followMouseRestriced = false;
    public float minXFollow, minYFollow, maxXFollow, maxYFollow;

    public void RestrictFollowMouse(
        float minX,
        float maxX,
        float minY,
        float maxY)
    {
        followMouseRestriced = true;
        minXFollow = minX;
        maxXFollow = maxX;
        minYFollow = minY;
        maxYFollow = maxY;
    }
    public void UnrestrictFollowMouse()
    {
        followMouseRestriced = false;
    }
    public void EnableFollowMouse()
    {
        followMouse = true;
    }
    public void DisableFollowMouse()
    {
        followMouse = false;
    }

    protected virtual void Awake()
    {
        if (autoAssignReferences)
        {
            if (objCollider == null)
            {
                objCollider = GetComponent<Collider>();
            }
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            if (tmpText == null)
            {
                tmpText = GetComponent<TMP_Text>();
            }
            if (objCollider2D == null)
            {
                objCollider2D = GetComponent<Collider2D>();
            }
        }
        TouchUtils.AllTouchables.Add(this);
    }

    protected virtual void OnDestroy()
    {
        TouchUtils.AllTouchables.Remove(this);
    }

    protected virtual void Update()
    {
        if (referenceCamera == null)
        {
            referenceCamera = Camera.main;
        }
        if (autoComputeMouseWorldPosition)
        {
            MouseWorldPosition = GetMouseWorldPosition();
        }
        if (inputEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseWorldPosition
                    = GetMouseWorldPosition();
                OnClickStarted();
                OnClickStartedCallBack?.Invoke(this);
                if (CheckPointInsideTouchable(MouseWorldPosition)
                    && CheckPointInsideOtherSpriteRenderers(
                        MouseWorldPosition
                    )
                )
                {
                    OnClickStartedInside();
                    OnClickStartedInsideCallBack?.Invoke(this);
                    if (followMouse)
                    {
                        Vector3 mouseWorldPos
                            = referenceCamera.ScreenToWorldPoint(
                                Input.mousePosition
                            );
                        deltaMouse = new Vector3(
                            transform.position.x - mouseWorldPos.x,
                            transform.position.y - mouseWorldPos.y,
                            0);
                    }
                    Clicked = true;
                }
                else
                {
                    OnClickStartedOutside();
                    OnClickStartedOutsideCallBack?.Invoke(this);
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (Clicked || !requireClickInside)
                {
                    MouseWorldPosition
                        = GetMouseWorldPosition();
                    OnClickHolding();
                    OnClickHoldingCallBack?.Invoke(this);
                    if (CheckPointInsideTouchable(MouseWorldPosition)
                        && CheckPointInsideOtherSpriteRenderers(
                            MouseWorldPosition
                        )
                    )
                    {
                        OnClickHoldingInside();
                        OnClickHoldingInsideCallBack?.Invoke(this);
                    }
                    else
                    {
                        OnClickHoldingOutside();
                        OnClickHoldingOutsideCallBack?.Invoke(this);
                    }
                    if (followMouse)
                    {
                        MouseWorldPosition = GetMouseWorldPosition();
                        float newPosX = MouseWorldPosition.x + deltaMouse.x;
                        float newPosY = MouseWorldPosition.y + deltaMouse.y;
                        if (followMouseRestriced)
                        {
                            if (newPosX < minXFollow)
                            {
                                newPosX = minXFollow;
                            }
                            if (newPosX > maxXFollow)
                            {
                                newPosX = maxXFollow;
                            }
                            if (newPosY < minYFollow)
                            {
                                newPosY = minYFollow;
                            }
                            if (newPosY > maxYFollow)
                            {
                                newPosY = maxYFollow;
                            }
                        }
                        transform.position = new Vector3(
                            newPosX,
                            newPosY,
                            transform.position.z
                        );
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (Clicked || !requireClickInside)
                {
                    MouseWorldPosition
                        = GetMouseWorldPosition();
                    OnClickEnded();
                    OnClickEndedCallBack?.Invoke(this);
                    if (CheckPointInsideTouchable(MouseWorldPosition)
                        && CheckPointInsideOtherSpriteRenderers(
                                MouseWorldPosition
                        )
                    )
                    {
                        OnClickEndedInside();
                        OnClickEndedInsideCallBack?.Invoke(this);
                    }
                    else
                    {
                        OnClickEndedOutside();
                        OnClickEndedOutsideCallBack?.Invoke(this);
                    }
                    Clicked = false;
                }
            }

            MouseWorldPosition
                = GetMouseWorldPosition();
            if (CheckPointInsideTouchable(MouseWorldPosition)
                && CheckPointInsideOtherSpriteRenderers(MouseWorldPosition))
            {
                if (!Hovering)
                {
                    OnHoverStarted();
                    OnHoverStartedCallBack?.Invoke(this);
                    Hovering = true;
                }
                else
                {
                    OnHover();
                    OnHoverCallBack?.Invoke(this);
                }
            }
            else if (Hovering)
            {
                OnHoverEnded();
                OnHoverEndedCallBack?.Invoke(this);
                Hovering = false;
            }
        }
        else
        {
            if (Clicked)
            {
                OnClickEnded();
                OnClickEndedCallBack?.Invoke(this);
                Clicked = false;
            }
        }
    }

    private bool CheckPointInsideOtherSpriteRenderers(Vector3 point)
    {
        if (OtherIntersectRenderers != null)
        {
            foreach (SpriteRenderer intersectRenderer
                in OtherIntersectRenderers)
            {
                if (!CheckPointInsideTouchable(
                    point, intersectRenderer, null, null))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool CheckPointInsideTouchable(Vector3 point)
    {
        return CheckPointInsideTouchable(
            point, spriteRenderer, tmpText, objCollider2D
        );
    }

    private bool CheckPointInsideTouchable(
        Vector3 point,
        SpriteRenderer spriteRenderer,
        TMP_Text tmpText,
        Collider2D objCollider2D)
    {
        if (objCollider != null)
        {
            return float.IsFinite(point.x)
                && float.IsFinite(point.y)
                && float.IsFinite(point.z);
        }
        else if (objCollider2D != null)
        {
            return objCollider2D.OverlapPoint(MouseWorldPosition);
        }
        else if (spriteRenderer != null)
        {
            var position = spriteRenderer.transform.position;
            var size = spriteRenderer.bounds.size;

            if (point.x < position.x - size.x / 2
                || point.x > position.x + size.x / 2)
            {
                return false;
            }
            if (point.y < position.y - size.y / 2
                || point.y > position.y + size.y / 2)
            {
                return false;
            }
            return true;
        }
        else if (tmpText != null)
        {
            var position = tmpText.transform.position;
            var size = tmpText.bounds.size;

            if (point.x < position.x - size.x / 2
                || point.x > position.x + size.x / 2)
            {
                return false;
            }
            if (point.y < position.y - size.y / 2
                || point.y > position.y + size.y / 2)
            {
                return false;
            }
            return true;
        }
        return false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        if (referenceCamera.orthographic)
        {
            return GetMouseWorldPosition2D();
        }
        return GetMouseWorldPosition3D();
    }

    private Vector3 GetMouseWorldPosition2D()
    {
        var mouseScreenPosirtion = Input.mousePosition;
        mouseScreenPosirtion.z = -referenceCamera.transform.position.z;
        var mouseWorldPosition
            = referenceCamera
                .ScreenToWorldPoint(mouseScreenPosirtion);
        return mouseWorldPosition;
    }

    private Vector3 GetMouseWorldPosition3D()
    {
        Ray mouseRay = referenceCamera.ScreenPointToRay(Input.mousePosition);

        var mouseHits
               = Physics.RaycastAll(mouseRay, referenceCamera.farClipPlane);

        var sortedMouseHits = mouseHits.ToList();
        sortedMouseHits.Sort(
            new RaycastHitDistanceFromCameraComparer(referenceCamera)
        );

        foreach (var mouseHit in sortedMouseHits)
        {
            if (!ignoreOtherBlocking3dRaycasts)
            {
                foreach (var touchable in TouchUtils.AllTouchables)
                {
                    if (mouseHit.collider == touchable.objCollider
                        && touchable.objCollider != objCollider)
                    {
                        if (touchable.block3dRaycasts)
                        {
                            return Vector3.positiveInfinity;
                        }
                    }
                }
            }
            if (mouseHit.collider == objCollider)
            {
                return RaycastHitToWorldPoint(mouseHit);
            }
        }

        return Vector3.positiveInfinity;
    }

    private Vector3 RaycastHitToWorldPoint(RaycastHit raycastHit)
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z
            = referenceCamera
                .transform
                .InverseTransformPoint(raycastHit.point)
                .z;

        return referenceCamera.ScreenToWorldPoint(mousePoint);
    }

    private class RaycastHitDistanceFromCameraComparer : IComparer<RaycastHit>
    {
        private Camera referenceCamera;

        public RaycastHitDistanceFromCameraComparer(Camera referenceCamera)
        {
            this.referenceCamera = referenceCamera;
        }

        public int Compare(RaycastHit x, RaycastHit y)
        {
            var distanceFromCamX = Vector3.Distance(
                x.point,
                referenceCamera.transform.position
            );
            var distanceFromCamY = Vector3.Distance(
                y.point,
                referenceCamera.transform.position
            );
            return distanceFromCamX.CompareTo(distanceFromCamY);
        }
    }
}