using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttackerDisplayer : MonoBehaviour
{
    [SerializeField]
    private Transform pivotTransform;

    [SerializeField]
    private Transform contentTransform;

    [SerializeField]
    private GameObject notBoughtObject;

    private Attacker attacker;

    public void Initialize(float angleRelToEnemy, float distanceFromEnemy)
    {
        pivotTransform.localEulerAngles
            = new Vector3(
                pivotTransform.eulerAngles.x,
                pivotTransform.eulerAngles.y,
                angleRelToEnemy
            );

        contentTransform.localPosition
            = new Vector3(
                pivotTransform.localPosition.x,
                distanceFromEnemy,
                pivotTransform.localPosition.z
            );
    }

    public void Bind(Attacker attacker)
    {
        this.attacker = attacker;
        OnUpgradeLevelChanged(attacker.UpgradeLevel);
        attacker.onUpgradeLevelChanged += OnUpgradeLevelChanged;
    }

    private void OnUpgradeLevelChanged(int level)
    {
        var isBought = attacker.UpgradeLevel > 0;
        notBoughtObject.SetActive(!isBought);
    }

    private void OnDestroy()
    {
        attacker.onUpgradeLevelChanged -= OnUpgradeLevelChanged;
    }
}
