using UnityEngine;

public static class Extensions
{
	public static Vector3 To2D(this Vector2 vector)
	{
		return new Vector3(vector.x, vector.y, 0);
	}

	public static Vector3 WithZ(this Vector3 vector, float z)
	{
		return new Vector3(vector.x, vector.y, z);
	}
}