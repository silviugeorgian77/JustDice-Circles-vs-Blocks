using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject attackerDisplayerPrefab;

    [SerializeField]
    private EnemyDisplayer enemyDisplayer;

    [SerializeField]
    private Touchable userAttackTouchable;

    private GameConfig gameConfig;
    private UserData userData;

    private const float DISTANCE_FROM_ENEMY = 2f;

    public void Init(GameConfig gameConfig, UserData userData)
    {
        this.gameConfig = gameConfig;
        this.userData = userData;
        SpawnAttackers();
        InitUserAttack();
    }

    private void SpawnAttackers()
    {
        var deltaAngleBetweenAttackers
            = 360 / gameConfig.maxAttackersCount;
        var currentAngle = 0;
        GameObject attackerDisplayerObject;
        AttackerDisplayer attackerDisplayer;
        for (var i = 0; i < gameConfig.maxAttackersCount; i++)
        {
            attackerDisplayerObject = Instantiate(attackerDisplayerPrefab);
            attackerDisplayer
                = attackerDisplayerObject.GetComponent<AttackerDisplayer>();
            attackerDisplayer.Initialize(currentAngle, DISTANCE_FROM_ENEMY);
            currentAngle += deltaAngleBetweenAttackers;
        }
    }

    private void InitUserAttack()
    {
        userAttackTouchable.OnClickEndedInsideCallBack = (touchable) =>
        {
            enemyDisplayer.TakeUserDamage();
            var income
                = gameConfig
                    .tapAttackIncomeFormula
                    .GetValue(userData.UserAttacker.UpgradeLevel);
            userData.CurrencyCount += income;
        };
    }

    public void EnableInput()
    {
        userAttackTouchable.inputEnabled = true;
    }

    public void DisableInput()
    {
        userAttackTouchable.inputEnabled = false;
    }
}
