using TMPro;
using UnityEngine;

public class DamageDisplayer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text damageText;

    [SerializeField]
    private Moveable moveable;

    [SerializeField]
    private AlphaModifier alphaModifier;

    private const float DELTA_DISTANCE = 1f;
    private const float ANIMATION_DURATION_S = .6f;

    public void Bind(int damage)
    {
        damageText.text = "+" + damage;
    }

    public void AnimateAndDestroy()
    {
        moveable.MoveY(
            DELTA_DISTANCE,
            ANIMATION_DURATION_S,
            TransformScope.LOCAL
        );

        alphaModifier.AlphaTo(
            0,
            ANIMATION_DURATION_S,
            easeFunction: EaseEquations.easeOut,
            EndCallBack: () =>
            {
                Destroy(gameObject);
            }
        );
    }
}
