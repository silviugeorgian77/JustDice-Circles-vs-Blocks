using UnityEngine;

public class AttackerDisplayer : MonoBehaviour
{
    [SerializeField]
    private Transform pivotTransform;

    [SerializeField]
    private Transform contentTransform;

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
}
