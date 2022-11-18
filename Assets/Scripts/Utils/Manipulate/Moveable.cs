using UnityEngine;

public class Moveable : MonoBehaviour
{
    private enum MoveMode
    {
        DIRECT,
        BEZIER_CURVE
    }

    protected bool executeMove = false;
    private float moveX;
    private float moveY;
    private float moveZ;
    private TransformScope transformScope;
    private EaseEquations.EaseFunctionDelegate easeFunctionMove;
    private float startX, startY, startZ;
    public float durationMove;
    private float elapsedTimeMove;
    private float changeInValueMoveX;
    private float changeInValueMoveY;
    private float changeInValueMoveZ;
    public delegate void MoveEndedCallBackFunction();
    private MoveEndedCallBackFunction MoveEndedCallBack;
    private float posX, posY, posZ;
    private Vector3 position = new Vector3();
    private MoveMode moveMode;

    private BezierCurve bezierCurve;

    public void Move(
        float newX,
        float newY,
        float newZ,
        float duration,
        TransformScope transformScope,
        EaseEquations.EaseFunctionDelegate easeFunction = null,
        MoveEndedCallBackFunction EndCallBack = null)
    {
        MoveStop();
        MoveEndedCallBack = EndCallBack;
        if (duration == 0)
        {
            position.x = newX;
            position.y = newY;
            position.z = newZ;
            if (transformScope == TransformScope.GLOBAL)
            {
                transform.position = position;
            }
            else
            {
                transform.localPosition = position;
            }
            OnFinished();
            return;
        }
        if (easeFunction == null)
        {
            easeFunction = EaseEquations.noEaseFunction;
        }
        moveX = newX;
        moveY = newY;
        moveZ = newZ;
        executeMove = true;
        durationMove = duration;
        this.transformScope = transformScope;
        if (transformScope == TransformScope.GLOBAL)
        {
            startX = transform.position.x;
            startY = transform.position.y;
            startZ = transform.position.z;
        }
        else
        {
            startX = transform.localPosition.x;
            startY = transform.localPosition.y;
            startZ = transform.localPosition.z;
        }
        elapsedTimeMove = 0;
        changeInValueMoveX = newX - startX;
        changeInValueMoveY = newY - startY;
        changeInValueMoveZ = newZ - startZ;
        easeFunctionMove = easeFunction;
        moveMode = MoveMode.DIRECT;
    }

    public void MoveX(
        float newX,
        float duration,
        TransformScope transformScope,
        EaseEquations.EaseFunctionDelegate easeFunction = null,
        MoveEndedCallBackFunction EndCallBack = null)
    {
        MoveStop();
        MoveEndedCallBack = EndCallBack;
        if (duration == 0)
        {
            position.x = newX;
            if (transformScope == TransformScope.GLOBAL)
            {
                position.y = transform.position.y;
                position.z = transform.position.z;
                transform.position = position;
            }
            else
            {
                position.y = transform.localPosition.y;
                position.z = transform.localPosition.z;
                transform.localPosition = position;
            }
            OnFinished();
        }
        if (easeFunction == null)
        {
            easeFunction = EaseEquations.noEaseFunction;
        }
        moveX = newX;
        executeMove = true;
        durationMove = duration;
        this.transformScope = transformScope;
        if (transformScope == TransformScope.GLOBAL)
        {
            startX = transform.position.x;
            startY = transform.position.y;
            startZ = transform.position.z;
            moveY = transform.position.y;
            moveZ = transform.position.z;
        }
        else
        {
            startX = transform.localPosition.x;
            startY = transform.localPosition.y;
            startZ = transform.localPosition.z;
            moveY = transform.localPosition.y;
            moveZ = transform.localPosition.z;
        }
        elapsedTimeMove = 0;
        changeInValueMoveX = newX - startX;
        changeInValueMoveY = 0;
        changeInValueMoveY = 0;
        easeFunctionMove = easeFunction;
        moveMode = MoveMode.DIRECT;
    }

    public void MoveY(
        float newY,
        float duration,
        TransformScope transformScope,
        EaseEquations.EaseFunctionDelegate easeFunction = null,
        MoveEndedCallBackFunction EndCallBack = null)
    {
        MoveStop();
        MoveEndedCallBack = EndCallBack;
        if (duration == 0)
        {
            position.y = newY;
            if (transformScope == TransformScope.GLOBAL)
            {
                position.x = transform.position.x;
                position.z = transform.position.z;
                transform.position = position;
            }
            else
            {
                position.x = transform.localPosition.x;
                position.z = transform.localPosition.z;
                transform.localPosition = position;
            }
            OnFinished();
        }
        if (easeFunction == null)
        {
            easeFunction = EaseEquations.noEaseFunction;
        }
        moveY = newY;
        executeMove = true;
        durationMove = duration;
        this.transformScope = transformScope;
        if (transformScope == TransformScope.GLOBAL)
        {
            startX = transform.position.x;
            startY = transform.position.y;
            startZ = transform.position.z;
            moveX = transform.position.x;
            moveZ = transform.position.z;
        }
        else
        {
            startX = transform.localPosition.x;
            startY = transform.localPosition.y;
            startZ = transform.localPosition.z;
            moveX = transform.localPosition.x;
            moveZ = transform.localPosition.z;
        }
        elapsedTimeMove = 0;
        changeInValueMoveX = 0;
        changeInValueMoveY = newY - startY;
        changeInValueMoveZ = 0;
        easeFunctionMove = easeFunction;
        moveMode = MoveMode.DIRECT;
    }

    public void MoveZ(
        float newZ,
        float duration,
        TransformScope transformScope,
        EaseEquations.EaseFunctionDelegate easeFunction = null,
        MoveEndedCallBackFunction EndCallBack = null)
    {
        MoveStop();
        MoveEndedCallBack = EndCallBack;
        if (duration == 0)
        {
            position.z = newZ;
            if (transformScope == TransformScope.GLOBAL)
            {
                position.x = transform.position.x;
                position.y = transform.position.y;
                transform.position = position;
            }
            else
            {
                position.x = transform.localPosition.x;
                position.y = transform.localPosition.y;
                transform.localPosition = position;
            }
            OnFinished();
        }
        if (easeFunction == null)
        {
            easeFunction = EaseEquations.noEaseFunction;
        }
        moveZ = newZ;
        executeMove = true;
        durationMove = duration;
        this.transformScope = transformScope;
        if (transformScope == TransformScope.GLOBAL)
        {
            startX = transform.position.x;
            startY = transform.position.y;
            startZ = transform.position.z;
            moveX = transform.position.x;
            moveY = transform.position.y;
        }
        else
        {
            startX = transform.localPosition.x;
            startY = transform.localPosition.y;
            startZ = transform.localPosition.z;
            moveX = transform.localPosition.x;
            moveY = transform.localPosition.y;
        }
        elapsedTimeMove = 0;
        changeInValueMoveX = 0;
        changeInValueMoveY = 0;
        changeInValueMoveZ = newZ - startZ;
        easeFunctionMove = easeFunction;
        moveMode = MoveMode.DIRECT;
    }

    public void MoveOnBezierCurve(
        BezierCurve bezierCurve,
        float duration,
        TransformScope transformScope,
        EaseEquations.EaseFunctionDelegate easeFunction = null,
        MoveEndedCallBackFunction EndCallBack = null)
    {
        executeMove = true;
        moveX = bezierCurve.ControlPoints[3].x;
        moveY = bezierCurve.ControlPoints[3].y;
        moveZ = bezierCurve.ControlPoints[3].z;
        durationMove = duration;
        this.bezierCurve = bezierCurve;
        this.transformScope = transformScope;
        elapsedTimeMove = 0;
        moveMode = MoveMode.BEZIER_CURVE;
    }

    public void MoveStop()
    {
        executeMove = false;
    }

    protected virtual void Update()
    {
        if (executeMove)
        {
            elapsedTimeMove += Time.deltaTime;
            if (moveMode == MoveMode.DIRECT)
            {
                position.x = easeFunctionMove(
                    changeInValueMoveX,
                    elapsedTimeMove,
                    durationMove,
                    startX);
                position.y = easeFunctionMove(
                    changeInValueMoveY,
                    elapsedTimeMove,
                    durationMove, startY);
                position.z = easeFunctionMove(
                    changeInValueMoveZ,
                    elapsedTimeMove,
                    durationMove, startZ);
            }
            else if (moveMode == MoveMode.BEZIER_CURVE
                && bezierCurve != null)
            {
                position = bezierCurve.CalculateBezierPoint(
                    elapsedTimeMove / durationMove
                );
            }

            if (float.IsNaN(position.x)
                    || float.IsInfinity(position.x))
            {
                position.x = 0;
            }
            if (float.IsNaN(position.y)
                || float.IsInfinity(position.y))
            {
                position.y = 0;
            }
            if (float.IsNaN(position.z)
                || float.IsInfinity(position.z))
            {
                position.z = 0;
            }

            if (transformScope == TransformScope.GLOBAL)
            {
                transform.position = position;
            }
            else
            {
                transform.localPosition = position;
            }

            if (elapsedTimeMove >= durationMove)
            {
                executeMove = false;
                position.x = moveX;
                position.y = moveY;
                position.z = moveZ;
                if (transformScope == TransformScope.GLOBAL)
                {
                    transform.position = position;
                }
                else
                {
                    transform.localPosition = position;
                }
                OnFinished();
            }
        }
    }

    private void OnFinished()
    {
        MoveEndedCallBack?.Invoke();
    }
}
