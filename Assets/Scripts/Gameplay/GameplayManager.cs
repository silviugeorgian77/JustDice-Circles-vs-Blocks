using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject attackerDisplayerPrefab;

    private GameConfig gameConfig;
    private UserData userData;

    private const float DISTANCE_FROM_ENEMY = 2f;

    public void Init(GameConfig gameConfig, UserData userData)
    {
        this.gameConfig = gameConfig;
        this.userData = userData;
        SpawnAttackers();
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
}
