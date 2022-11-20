using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttackerDisplayer : MonoBehaviour
{
    [SerializeField]
    private Transform pivotTransform;

    [SerializeField]
    private Moveable contentMoveable;

    [SerializeField]
    private GameObject notBoughtObject;

    public Attacker Attacker { get; private set; }

    private float maxDistanceFromEnemy;

    private const float ATTACK_DURATION_S = .1f;
    private const float RETREAT_DURATION_S = .3f;
    private const float MIN_DISTANCE_FROM_ENEMY = .7f;

    public void Initialize(float angleRelToEnemy, float distanceFromEnemy)
    {
        this.maxDistanceFromEnemy = distanceFromEnemy;

        pivotTransform.localEulerAngles
            = new Vector3(
                pivotTransform.eulerAngles.x,
                pivotTransform.eulerAngles.y,
                angleRelToEnemy
            );

        contentMoveable.MoveY(
            distanceFromEnemy,
            0,
            TransformScope.LOCAL
        );
    }

    public void Bind(Attacker attacker)
    {
        Attacker = attacker;
        OnUpgradeLevelChanged(attacker.UpgradeLevel);
        attacker.onUpgradeLevelChanged += OnUpgradeLevelChanged;
    }

    private void OnUpgradeLevelChanged(int level)
    {
        var isBought = Attacker.UpgradeLevel > 0;
        notBoughtObject.SetActive(!isBought);
    }

    public void Attack()
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
                contentMoveable.MoveY(
                    maxDistanceFromEnemy,
                    RETREAT_DURATION_S,
                    TransformScope.LOCAL
                );
            }
        );
    }

    private void OnDestroy()
    {
        Attacker.onUpgradeLevelChanged -= OnUpgradeLevelChanged;
    }
}
