using TMPro;
using UnityEngine;

public class AttackerDisplayer : MonoBehaviour
{
    [SerializeField]
    private Rotatable pivotRotatable;

    [SerializeField]
    private Moveable contentMoveable;

    [SerializeField]
    private GameObject notBoughtObject;

    [SerializeField]
    private TMP_Text levelText;

    [SerializeField]
    private GameObject damageDisplayerPrefab;

    public Attacker Attacker { get; private set; }

    private AttackIncomeFormula attackIncomeFormula;

    private float angleRelativeToEnemy;
    private float maxDistanceFromEnemy;

    private const float ATTACK_DURATION_S = .1f;
    private const float RETREAT_DURATION_S = .3f;
    private const float MIN_DISTANCE_FROM_ENEMY = .7f;

    private const float MIN_IDLE_DELTA_ANGLE = 5f;
    private const float MAX_IDLE_DELTA_ANGLE = 10f;
    private const float MIN_IDLE_ROTATION_DURATION = .2f;
    private const float MAX_IDLE_ROTATION_DURATION = .4f;

    public void Initialize(
        float angleRelativeToEnemy,
        float distanceFromEnemy)
    {
        this.angleRelativeToEnemy = angleRelativeToEnemy;
        this.maxDistanceFromEnemy = distanceFromEnemy;

        ResetRotation();
        ResetPosition();
    }

    private void AnimateIdle()
    {
        if (pivotRotatable == null)
        {
            return;
        }

        var deltaAngle = Random.Range(
            MIN_IDLE_DELTA_ANGLE,
            MAX_IDLE_DELTA_ANGLE
        );
        var sign = Random.Range(0, 2) == 0 ? -1 : 1;
        var angle = angleRelativeToEnemy + sign * deltaAngle;
        var duration = Random.Range(
            MIN_IDLE_ROTATION_DURATION,
            MAX_IDLE_ROTATION_DURATION
        );
        pivotRotatable.RotateToZ(
            angle,
            duration,
            Rotatable.RotateMode.SHORT,
            EndCallBack: () =>
            {
                AnimateIdle();
            }
        );
    }

    private void ResetRotation()
    {
        pivotRotatable.RotateToZ(angleRelativeToEnemy, 0);
    }

    private void ResetPosition()
    {
        contentMoveable.MoveY(
            maxDistanceFromEnemy,
            0,
            TransformScope.LOCAL
        );
    }

    public void Bind(
        Attacker attacker,
        AttackIncomeFormula attackIncomeFormula)
    {
        Attacker = attacker;
        this.attackIncomeFormula = attackIncomeFormula;
        OnUpgradeLevelChanged(attacker.UpgradeLevel);
        attacker.onUpgradeLevelChanged += OnUpgradeLevelChanged;
    }

    private void OnUpgradeLevelChanged(int level)
    {
        var isBought = Attacker.UpgradeLevel > 0;
        notBoughtObject.SetActive(!isBought);

        levelText.gameObject.SetActive(isBought);

        pivotRotatable.RotateStop();
        if (isBought)
        {
            levelText.text = level.ToString();
            AnimateIdle();
        }
        else
        {
            ResetRotation();
            ResetPosition();
        }
    }

    public void AnimateAttack()
    {
        if (contentMoveable == null)
        {
            return;
        }
        contentMoveable.MoveY(
            MIN_DISTANCE_FROM_ENEMY,
            ATTACK_DURATION_S,
            TransformScope.LOCAL,
            EndCallBack: () =>
            {
                var damageDisplayerObject = Instantiate(damageDisplayerPrefab);
                damageDisplayerObject.transform.position
                    = contentMoveable.transform.position;
                var damageDisplayer
                    = damageDisplayerObject.GetComponent<DamageDisplayer>();
                damageDisplayer.Bind(
                    attackIncomeFormula.GetValue(Attacker.UpgradeLevel)
                );
                damageDisplayer.AnimateAndDestroy();

                contentMoveable.MoveY(
                    maxDistanceFromEnemy,
                    RETREAT_DURATION_S,
                    TransformScope.LOCAL
                );
            }
        );
    }

    private void Update()
    {
        levelText.transform.rotation = Quaternion.identity;
    }

    private void OnDestroy()
    {
        Attacker.onUpgradeLevelChanged -= OnUpgradeLevelChanged;
    }
}
