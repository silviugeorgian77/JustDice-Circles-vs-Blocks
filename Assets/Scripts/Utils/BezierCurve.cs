using UnityEngine;

public class BezierCurve
{
	#region Variables Declaration
	private Vector3[] controlPoints = new Vector3[4];
	private int segmentCount = 0;
	private Vector3[] segmentsPoints;
	private float[] distancesFromStartToPoint;
	private float length;
	#endregion

	#region Constructors
	public BezierCurve(Vector3[] points, int segments)
	{
		Initialize(points[0], points[1], points[2], points[3], segments);
	}

	public BezierCurve(
		Vector3 p0,
		Vector3 p1, Vector3 p2,
		Vector3 p3,
		int segmentCount)
	{
		controlPoints = new Vector3[4];
		Initialize(p0, p1, p2, p3, segmentCount);
	}

	public void Initialize(
		Vector3 p0,
		Vector3 p1,
		Vector3 p2,
		Vector3 p3,
		int segmentCount)
	{
		this.segmentCount = segmentCount;
		controlPoints[0] = p0;
		controlPoints[1] = p1;
		controlPoints[2] = p2;
		controlPoints[3] = p3;
		CalculateParameters();
	}
	#endregion

	#region Getters and Setters
	public Vector3[] GetControlPoints()
	{
		return controlPoints;
	}

	public Vector3 GetCotrolPoint(int index)
	{
		return controlPoints[index];
	}

	public Vector3[] ControlPoints
	{
		get
        {
			return controlPoints;
        }
		set
		{
			controlPoints = value;
			CalculateParameters();
		}
	}

	public int SegmentCount
	{
		get
		{
			return segmentCount;
		}
		set
		{
			segmentCount = value;
			CalculateParameters();
		}
	}

	public Vector3[] GetSegmentsPoints()
	{
		return segmentsPoints;
	}

	public Vector3 GetSegmentsPoint(int index)
	{
		return segmentsPoints[index];
	}

	public int SegmentsPointsCount
	{
		get { return segmentsPoints.Length; }
	}

	public float[] GetDistancesFromStartToPoint()
	{
		return distancesFromStartToPoint;
	}

	public float GetDistanceFromStartToPoint(int index)
	{
		return distancesFromStartToPoint[index];
	}

	public float Length
	{
		get { return length; }
	}
	#endregion

	#region Functionality
	private void CalculateParameters()
	{
		CalculateBezierSegmentsPoints();
		CalculateBezierSegmentsLength();
	}

	private void CalculateBezierSegmentsPoints()
	{
		segmentsPoints = new Vector3[segmentCount];
		float t;
		for (int i = 0; i < segmentCount; i++)
		{
			t = i / (float)segmentCount;
			segmentsPoints[i] = CalculateBezierPoint(t);
		}
	}

	public Vector3 CalculateBezierPoint(float t)
	{
		return (1 - t) * (1 - t) * (1 - t) * controlPoints[0]
			+ 3 * (1 - t) * (1 - t) * t * controlPoints[1]
			+ 3 * (1 - t) * t * t * controlPoints[2]
			+ t * t * t * controlPoints[3];
	}

	public void DrawBezierSegments(LineRenderer lineRenderer)
	{
		lineRenderer.positionCount = segmentsPoints.Length;
		for (int i = 0; i < segmentsPoints.Length; i++)
		{
			lineRenderer.SetPosition(i, segmentsPoints[i]);
		}
	}

	public void CalculateBezierSegmentsLength()
	{
		length = 0;
		distancesFromStartToPoint = new float[segmentsPoints.Length];
		for (int i = 0; i < segmentsPoints.Length - 1; i++)
		{
			distancesFromStartToPoint[i] = length;
			length += MathUtils.GetDistance(
				segmentsPoints[i],
				segmentsPoints[i + 1]
			);
		}
		distancesFromStartToPoint[segmentsPoints.Length - 1] = length;
	}
	#endregion
}
