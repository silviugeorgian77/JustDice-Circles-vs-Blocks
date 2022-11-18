using UnityEngine;

public class EnemyDisplayer : MonoBehaviour
{
    [SerializeField]
    private Rotatable rotatable;

    [SerializeField]
    private Scalable scalable;

    private Vector3 initScale;
    private Quaternion initRotation;

    private const float MIN_ROTATE_DURATION = 5f;
    private const float MAX_ROTATE_DURATION = 15f;

    private const float SCALE_USER_DAMAGE = 1.2f;
    private const float DURATION_USER_DAMAGE = .2f;

    private void Awake()
    {
        initScale = transform.localScale;
        initRotation = transform.rotation;
        Rotate();
    }

    private void Rotate()
    {
        var sign = Random.Range(0, 2) == 0 ? 1 : -1;
        var duration = Random.Range(MIN_ROTATE_DURATION, MAX_ROTATE_DURATION);

        rotatable.RotateByZ(
            sign * 180,
            duration,
            easeFunction: EaseEquations.easeIn,
            EndCallBack: () =>
            {
                rotatable.RotateByZ(
                    sign * 180,
                    duration,
                    easeFunction: EaseEquations.easeOut,
                    EndCallBack: () =>
                    {
                        // The following line is in order to avoid huge rotation values
                        rotatable.transform.rotation = initRotation;

                        Rotate();
                    }
                );
            }
        );
    }

    public void TakeUserDamage()
    {
        var scale = initScale * SCALE_USER_DAMAGE;
        scalable.ScaleTo(
            scale.x,
            scale.y,
            scale.z,
            DURATION_USER_DAMAGE,
            EndCallBack: () =>
            {
                scalable.ScaleTo(
                    initScale.x,
                    initScale.y,
                    initScale.z,
                    DURATION_USER_DAMAGE
                );
            }
        );
    }

    public void TakeAttackerDamage()
    {

    }
}
