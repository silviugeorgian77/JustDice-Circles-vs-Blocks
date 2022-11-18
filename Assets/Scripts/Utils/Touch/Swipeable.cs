using System;
using System.Collections.Generic;
using UnityEngine;
using static Swipeable;

public class Swipeable : MonoBehaviour
{
    public Touchable touchable;

    /// <summary>
    /// The click must be dragged by at least this value, in order to
    /// be considered a swipe.
    /// </summary>
    public float startThreshold = .05f;

    /// <summary>
    /// After this time has passed, the swipe becomes invalid.
    /// </summary>
    public float maxDurationS = 2f;

#if UNITY_EDITOR
    public bool showDebugPoints;
#endif

    public Action<Swipe> OnSwipeStarted;
    public Action<Swipe> OnSwiping;
    public Action<Swipe> OnSwipeFinished;

    /// <summary>
    /// Called after the swipe began, but <see cref="startThreshold"/> has
    /// passed and the swipe was not finished.
    /// </summary>
    public Action<Swipe> OnSwipeTimeout;

    private Vector3 startPoint;
    private Swipe swipe;
    private Swipe lastSwipe;
    private const float DIRECTION_ANGLE_THRESHOLD = 10f;

    private void Awake()
    {
        touchable.OnClickStartedInsideCallBack = touchable =>
        {
            startPoint = touchable.MouseWorldPosition;
        };

        touchable.OnClickHoldingInsideCallBack = touchable =>
        {
            if (swipe == null
                &&
                Vector3.Distance(
                    touchable.MouseWorldPosition,
                    startPoint
                )
                >= startThreshold)
            {
                swipe = new Swipe();
                swipe.points.Add(startPoint);
                OnSwipeStarted?.Invoke(swipe);
            }
            if (swipe != null)
            {
                swipe.durationS += Time.deltaTime;
                if (swipe.durationS > maxDurationS)
                {
                    OnSwipeTimeout?.Invoke(swipe);
                    swipe = null;
                    return;
                }

                swipe.points.Add(touchable.MouseWorldPosition);
                OnSwiping?.Invoke(swipe);
            }
        };

        touchable.OnClickEndedCallBack = touchable =>
        {
            if (swipe != null)
            {
                swipe.Compute();
                swipe.Complete();
                OnSwipeFinished?.Invoke(swipe);
                lastSwipe = swipe;
                swipe = null;
            }
        };
    }

    public class Swipe
    {
        public float durationS;
        public List<Vector3> points = new List<Vector3>();
        public bool IsCompleted;

        private List<DirectionHolder> directionHolders
            = new List<DirectionHolder>();


        public void Compute()
        {
            if (points.Count < 2)
            {
                return;
            }

            ComputeDirectionHolders();
        }

        private void ComputeDirectionHolders()
        {
            directionHolders.Clear();
            Direction currentDirection = Direction.N;
            Direction lastDirection = currentDirection;
            Vector3 lastPoint;
            Vector3 currentPoint;
            Vector3 firstPointInCurrentDirection = points[0];
            for (var i = 1; i < points.Count; i++)
            {
                lastPoint = points[i - 1];
                currentPoint = points[i];
                if (lastPoint.EqualsApproximately(currentPoint, .01f)
                    && i != points.Count - 1)
                {
                    continue;
                }

                var angle = MathUtils.GetLookAtAngle(lastPoint, currentPoint);
                if (angle >= 90 - DIRECTION_ANGLE_THRESHOLD
                    && angle <= 90 + DIRECTION_ANGLE_THRESHOLD)
                {
                    currentDirection = Direction.N;
                }
                else if (angle >= 270 - DIRECTION_ANGLE_THRESHOLD
                    && angle <= 270 + DIRECTION_ANGLE_THRESHOLD)
                {
                    currentDirection = Direction.S;
                }
                else if (angle >= -DIRECTION_ANGLE_THRESHOLD
                    && angle <= DIRECTION_ANGLE_THRESHOLD)
                {
                    currentDirection = Direction.E;
                }
                else if (angle >= 180 - DIRECTION_ANGLE_THRESHOLD
                    && angle <= 180 + DIRECTION_ANGLE_THRESHOLD)
                {
                    currentDirection = Direction.W;
                }
                else if (angle >= 0 && angle <= 90)
                {
                    currentDirection = Direction.NE;
                }
                else if (angle >= 90 && angle <= 180)
                {
                    currentDirection = Direction.NW;
                }
                else if (angle >= 180 && angle <= 270)
                {
                    currentDirection = Direction.SW;
                }
                else if (angle >= 270 && angle <= 360)
                {
                    currentDirection = Direction.SE;
                }

                if (i == 1)
                {
                    lastDirection = currentDirection;
                }

                if ((directionHolders.Count == 0 && i == points.Count - 1)
                    || currentDirection != lastDirection)
                {
                    // If the direction has changed, or if we reached the
                    // end of the point list and there was no direction change,
                    // then we save the current direction info.
                    directionHolders.Add(
                        new DirectionHolder()
                        {
                            direction = currentDirection,
                            distance = Vector3.Distance(
                                lastPoint,
                                firstPointInCurrentDirection
                            )
                        }
                    );
                    firstPointInCurrentDirection = currentPoint;
                    lastDirection = currentDirection;
                }
            }
        }

        public void Complete()
        {
            IsCompleted = true;
        }

        public List<DirectionHolder> GetDirectionHolders()
        {
            return directionHolders;
        }
    }

    public enum Direction
    {
        N,
        S,
        E,
        W,
        NE,
        NW,
        SE,
        SW
    }

    public class DirectionHolder
    {
        public Direction direction;
        public float distance;
    }

    private class DirectionHolderComparer : IComparer<DirectionHolder>
    {
        public int Compare(DirectionHolder x, DirectionHolder y)
        {
            return x.distance.CompareTo(y.distance);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!showDebugPoints)
        {
            return;
        }

        Swipe referenceSwipe;
        if (swipe != null)
        {
            referenceSwipe = swipe;
        }
        else
        {
            referenceSwipe = lastSwipe;
        }
        if (referenceSwipe == null)
        {
            return;
        }

        foreach (var point in referenceSwipe.points)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(point, .01f);
        }
    }
#endif
}
