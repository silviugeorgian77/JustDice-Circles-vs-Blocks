using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject attackerDisplayerPrefab;

    [SerializeField]
    private EnemyDisplayer enemyDisplayer;

    [SerializeField]
    private Touchable userAttackTouchable;

    [SerializeField]
    private Delayer delayer;

    private GameConfig gameConfig;
    private UserData userData;

    private List<AttackerDisplayer> attackerDisplayers
        = new List<AttackerDisplayer>();

    private const float DISTANCE_FROM_ENEMY = 2f;

    private const int DELAY_BETWEEN_ATTACKERS_MS = 500;

    public void Init(GameConfig gameConfig, UserData userData)
    {
        this.gameConfig = gameConfig;
        this.userData = userData;
        SpawnAttackers();
        InitAttackersAttack();
        InitUserAttack();
    }

    private void SpawnAttackers()
    {
        attackerDisplayers.Clear();
        var deltaAngleBetweenAttackers
            = 360 / gameConfig.maxAttackersCount;
        var currentAngle = 0;
        GameObject attackerDisplayerObject;
        AttackerDisplayer attackerDisplayer;
        foreach (var attacker in userData.Attackers)
        {
            attackerDisplayerObject = Instantiate(
                attackerDisplayerPrefab,
                transform
            );
            attackerDisplayer
                = attackerDisplayerObject.GetComponent<AttackerDisplayer>();
            attackerDisplayers.Add(attackerDisplayer);
            attackerDisplayer.Initialize(currentAngle, DISTANCE_FROM_ENEMY);
            attackerDisplayer.Bind(attacker);
            currentAngle += deltaAngleBetweenAttackers;
        }
    }

    private async void InitAttackersAttack()
    {
        AttackerDisplayer attackerDisplayer;
        bool needsDelay = false;
        bool atLeastOneAttacker = false;
        while (true)
        {
            atLeastOneAttacker = false;
            for (var i = 0; i < attackerDisplayers.Count; i++)
            {
                attackerDisplayer = attackerDisplayers[i];
                if (attackerDisplayer.Attacker.UpgradeLevel > 0)
                {
                    if (needsDelay)
                    {
                        needsDelay = false;
                        await Task.Delay(DELAY_BETWEEN_ATTACKERS_MS);
                    }
                    atLeastOneAttacker = true;
                    attackerDisplayer.AnimateAttack();
                    delayer.AddDelay(gameConfig.attackTimeS, delay =>
                    {
                        var income
                            = gameConfig
                                .attackerIncomeFormula
                                .GetValue(
                                    attackerDisplayer.Attacker.UpgradeLevel
                                );
                        userData.CurrencyCount += income;
                    });
                    needsDelay = true;
                }
            }
            if (!atLeastOneAttacker)
            {
                await Task.Delay(DELAY_BETWEEN_ATTACKERS_MS);
            }
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
