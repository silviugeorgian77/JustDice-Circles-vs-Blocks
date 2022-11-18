using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class Follower : MonoBehaviour
{
    /// <summary>
    /// The position that that object will be following.
    /// </summary>
    [SerializeField]
    private Transform target;
    public Transform Target
    {
        get { return target; }
        set
        {
            target = value;
            ComputeOffset();
        }
    }

    /// <summary>
    /// The speed with which the object will be following.
    /// </summary>
    public float smoothing = 5f;

    public bool xAxisEnabled = true;
    public bool yAxisEnabled = true;

    [SerializeField]
    private Transform referenceTransform;
    public Transform ReferenceTransform
    {
        get { return referenceTransform; }
        set
        {
            referenceTransform = value;
            ComputeLimits();
        }
    }

    private float minX;
    private float minY;
    private float maxX;
    private float maxY;

    public bool supportInitalOffset = false;

    /// <summary>
    /// The initial offset from the target.
    /// </summary>
    private Vector3 offset = Vector3.zero;

    private Vector2 referenceSize;
    private Vector2 transformSize;

    private void OnEnable()
    {
        ComputeOffset();
    }

#if UNITY_EDITOR
    private void Update()
    {
        ManageFollow();
    }
#else
    private void FixedUpdate()
    {
        ManageFollow();
    }
#endif

    /// <summary>
    /// Interpolate between the object position
    /// and it's target position.
    /// </summary>
    private void ManageFollow()
    {
        if (target != null)
        {
            ComputeLimits();

            Vector3 targetPos = target.position + offset;
            targetPos.z = transform.position.z;

            if (!xAxisEnabled)
            {
                targetPos.x = transform.position.x;
            }
            if (!yAxisEnabled)
            {
                targetPos.y = transform.position.y;
            }

            if (smoothing != 0)
            {
                targetPos = Vector3.Lerp(
                    transform.position,
                    targetPos,
                    smoothing * Time.deltaTime
                );
            }

            if (referenceTransform != null)
            {
                targetPos.x = MathUtils.ClampValue(
                    targetPos.x,
                    minX,
                    maxX
                );
                targetPos.y = MathUtils.ClampValue(
                    targetPos.y,
                    minY,
                    maxY
                );
            }

            transform.position = targetPos;
        }
    }

    private void ComputeLimits()
    {
        if (referenceTransform != null)
        {
            transformSize = MathUtils.GetSizeOfTransform(transform);

            referenceSize = MathUtils.GetSizeOfTransform(referenceTransform);
            minX = referenceTransform.position.x
                - referenceSize.x / 2
                + transformSize.x / 2;
            maxX = referenceTransform.position.x
                + referenceSize.x / 2
                - transformSize.x / 2;
            minY = referenceTransform.position.y
                - referenceSize.y / 2
                + transformSize.y / 2;
            maxY = referenceTransform.position.y
                + referenceSize.y / 2
                - transformSize.y / 2;
        }
    }

    /// <summary>
    /// Calculate the initial offset.
    /// </summary>
    private void ComputeOffset()
    {
        if (supportInitalOffset && target != null)
        {
            offset = transform.position - target.position;
        }
        else
        {
            offset = Vector3.zero;
        }
    }

}
