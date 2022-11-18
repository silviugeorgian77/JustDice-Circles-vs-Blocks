using UnityEngine;

public class Rotatable : MonoBehaviour
{
    protected bool executeRotate = false;
    private float finalRotationX;
    private float finalRotationY;
    private float finalRotationZ;
    public float durationRotation;
    private float startRotationX;
    private float startRotationY;
    private float startRotationZ;
    private float elapsedTimeRotation;
    private float changeInValueRotationX;
    private float changeInValueRotationY;
    private float changeInValueRotationZ;
    private EaseEquations.EaseFunctionDelegate easeFunctionRotate;
    public delegate void RotateEndedCallBackFunction();
    private RotateEndedCallBackFunction RotateEndedCallBack;
    private float rotationX;
    private float rotationY;
    private float rotationZ;

    public void RotateTo(
        float newRotationX,
        float newRotationY,
        float newRotationZ,
        float duration,
        EaseEquations.EaseFunctionDelegate easeFunction = null,
        RotateEndedCallBackFunction EndCallBack = null)
    {
        RotateStop();
        RotateEndedCallBack = EndCallBack;
        if (duration == 0)
        {
            transform.rotation = Quaternion.Euler(
                newRotationX,
                newRotationY,
                newRotationZ
            );
            OnFinished();
            return;
        }
        if (easeFunction == null)
        {
            easeFunction = EaseEquations.noEaseFunction;
        }
        finalRotationX = newRotationX;
        finalRotationY = newRotationY;
        finalRotationZ = newRotationZ;
        executeRotate = true;
        durationRotation = duration;
        startRotationX = transform.rotation.eulerAngles.x;
        startRotationY = transform.rotation.eulerAngles.y;
        startRotationZ = transform.rotation.eulerAngles.z;
        elapsedTimeRotation = 0;
        changeInValueRotationX = finalRotationX - startRotationX;
        changeInValueRotationY = finalRotationY - startRotationY;
        changeInValueRotationZ = finalRotationZ - startRotationZ;
        easeFunctionRotate = easeFunction;
    }

    public void RotateToX(
        float newRotationX,
        float duration,
        EaseEquations.EaseFunctionDelegate easeFunction = null,
        RotateEndedCallBackFunction EndCallBack = null)
    {
        RotateTo(
            newRotationX,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z,
            duration,
            easeFunction,
            EndCallBack
        );
    }

    public void RotateToY(
        float newRotationY,
        float duration,
        EaseEquations.EaseFunctionDelegate easeFunction = null,
        RotateEndedCallBackFunction EndCallBack = null)
    {
        RotateTo(
            transform.rotation.eulerAngles.x,
            newRotationY,
            transform.rotation.eulerAngles.z,
            duration,
            easeFunction,
            EndCallBack
        );
    }

    public void RotateToZ(
       float newRotationZ,
       float duration,
       EaseEquations.EaseFunctionDelegate easeFunction = null,
       RotateEndedCallBackFunction EndCallBack = null)
    {
        RotateTo(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            newRotationZ,
            duration,
            easeFunction,
            EndCallBack
        );
    }

    public void RotateBy(
        float amountX,
        float amountY,
        float amountZ,
        float duration,
        EaseEquations.EaseFunctionDelegate easeFunction = null,
        RotateEndedCallBackFunction EndCallBack = null)
    {
        RotateStop();
        if (duration == 0)
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x + amountX,
                transform.rotation.eulerAngles.y + amountY,
                transform.rotation.eulerAngles.z + amountZ
            );
            OnFinished();
        }
        if (easeFunction == null)
        {
            easeFunction = EaseEquations.noEaseFunction;
        }
        finalRotationX = transform.rotation.eulerAngles.x + amountX;
        finalRotationY = transform.rotation.eulerAngles.y + amountY;
        finalRotationZ = transform.rotation.eulerAngles.z + amountZ;
        executeRotate = true;
        durationRotation = duration;
        startRotationX = transform.rotation.eulerAngles.x;
        startRotationY = transform.rotation.eulerAngles.y;
        startRotationZ = transform.rotation.eulerAngles.z;
        elapsedTimeRotation = 0;
        changeInValueRotationX = finalRotationX - startRotationX;
        changeInValueRotationY = finalRotationY - startRotationY;
        changeInValueRotationZ = finalRotationZ - startRotationZ;
        easeFunctionRotate = easeFunction;
        RotateEndedCallBack = EndCallBack;
    }

    public void RotateByX(
        float amountX,
        float duration,
        EaseEquations.EaseFunctionDelegate easeFunction = null,
        RotateEndedCallBackFunction EndCallBack = null)
    {
        RotateBy(amountX, 0, 0, duration, easeFunction, EndCallBack);
    }

    public void RotateByY(
        float amountY,
        float duration,
        EaseEquations.EaseFunctionDelegate easeFunction = null,
        RotateEndedCallBackFunction EndCallBack = null)
    {
        RotateBy(0, amountY, 0, duration, easeFunction, EndCallBack);
    }

    public void RotateByZ(
        float amountZ,
        float duration,
        EaseEquations.EaseFunctionDelegate easeFunction = null,
        RotateEndedCallBackFunction EndCallBack = null)
    {
        RotateBy(0, 0, amountZ, duration, easeFunction, EndCallBack);
    }

    public void RotateStop()
    {
        executeRotate = false;
    }

    private void Update()
    {
        if (executeRotate)
        {
            elapsedTimeRotation += Time.deltaTime;
            rotationX = easeFunctionRotate(
                changeInValueRotationX,
                elapsedTimeRotation,
                durationRotation,
                startRotationX
            );
            rotationY = easeFunctionRotate(
                changeInValueRotationY,
                elapsedTimeRotation,
                durationRotation,
                startRotationY
            );
            rotationZ = easeFunctionRotate(
                changeInValueRotationZ,
                elapsedTimeRotation,
                durationRotation,
                startRotationZ
            );
            transform.rotation = Quaternion.Euler(
                rotationX,
                rotationY,
                rotationZ
            );
            if (elapsedTimeRotation >= durationRotation)
            {
                executeRotate = false;
                transform.rotation = Quaternion.Euler(
                    finalRotationX,
                    finalRotationY,
                    finalRotationZ
                );
                OnFinished();
            }
        }
    }

    private void OnFinished()
    {
        RotateEndedCallBack?.Invoke();
    }
}
