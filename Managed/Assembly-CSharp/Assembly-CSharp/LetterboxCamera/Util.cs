using System;
using System.Globalization;
using System.Linq.Expressions;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x02000064 RID: 100
	public static class Util
	{
		// Token: 0x060002FF RID: 767 RVA: 0x000166DA File Offset: 0x000148DA
		public static string GrootWhatAreYouDoing()
		{
			return "I am Groot.";
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000166E1 File Offset: 0x000148E1
		public static float AsPositive(float value)
		{
			return Mathf.Abs(value);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000166E9 File Offset: 0x000148E9
		public static float AsNegative(float value)
		{
			return -Mathf.Abs(value);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000166F4 File Offset: 0x000148F4
		public static float BezierCurve(float[] p, float t)
		{
			if (p.Length > 2)
			{
				float[] array = new float[p.Length - 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Util.Lerp(p[i], p[i + 1], t);
				}
				return Util.BezierCurve(array, t);
			}
			if (p.Length == 2)
			{
				return Util.Lerp(p[0], p[1], t);
			}
			Debug.Log("WARNING: A class attempted to get a Bezier Curve with less than two points!");
			return 0f;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001675C File Offset: 0x0001495C
		public static Vector3 BezierCurve(Vector3[] p, float t)
		{
			if (p.Length > 2)
			{
				Vector3[] array = new Vector3[p.Length - 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Util.Lerp(p[i], p[i + 1], t);
				}
				return Util.BezierCurve(array, t);
			}
			if (p.Length == 2)
			{
				return Util.Lerp(p[0], p[1], t);
			}
			return Vector3.zero;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000167D0 File Offset: 0x000149D0
		public static Vector3 CalculateReflectedVelocity(Vector3 originalVelocity, Vector3 normalOfCollision)
		{
			Vector3 vector = -normalOfCollision;
			Vector3 vector2 = originalVelocity.normalized;
			vector2 = new Vector3(vector2.x - vector.x, vector2.y - vector.y, 0f);
			if (vector2.sqrMagnitude == 0f)
			{
				vector2 = -originalVelocity.normalized;
			}
			return vector2;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001682E File Offset: 0x00014A2E
		public static float Clamp(float min, float max, float value)
		{
			value = ((value < min) ? min : value);
			value = ((value > max) ? max : value);
			return value;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001682E File Offset: 0x00014A2E
		public static int Clamp(int min, int max, int value)
		{
			value = ((value < min) ? min : value);
			value = ((value > max) ? max : value);
			return value;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00016845 File Offset: 0x00014A45
		public static string ColorToHex(Color32 color)
		{
			return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001687F File Offset: 0x00014A7F
		public static Vector2 DegreesToVector(float _angle)
		{
			return new Vector2((float)Math.Cos((double)(_angle * 0.017453292f)), (float)Math.Sin((double)(_angle * 0.017453292f)));
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000168A2 File Offset: 0x00014AA2
		public static float Difference(float a, float b)
		{
			if (a <= b)
			{
				return b - a;
			}
			return a - b;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000168A2 File Offset: 0x00014AA2
		public static int Difference(int a, int b)
		{
			if (a <= b)
			{
				return b - a;
			}
			return a - b;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000168B0 File Offset: 0x00014AB0
		public static Vector3 DirectionVector(Vector3 origin, Vector3 target)
		{
			return (target - origin).normalized;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000168CC File Offset: 0x00014ACC
		public static void Error(object sender, string error)
		{
			if (sender != null)
			{
				Debug.Log(sender.GetType().ToString() + ": " + error);
				return;
			}
			Debug.Log("NULL SENDER: " + error);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000168FD File Offset: 0x00014AFD
		public static string GetMemberName<T>(Expression<Func<T>> expression)
		{
			return ((MemberExpression)expression.Body).Member.Name;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00016914 File Offset: 0x00014B14
		public static Color HexToColor(string hex)
		{
			byte b = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
			byte b2 = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
			byte b3 = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
			return new Color32(b, b2, b3, byte.MaxValue);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001696C File Offset: 0x00014B6C
		public static int IndexToFlags(int _index)
		{
			switch (_index)
			{
			case 0:
				return 1;
			case 1:
				return 2;
			case 2:
				return 4;
			case 3:
				return 8;
			case 4:
				return 16;
			case 5:
				return 32;
			case 6:
				return 64;
			case 7:
				return 128;
			case 8:
				return 256;
			case 9:
				return 512;
			default:
				return -1;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000169CD File Offset: 0x00014BCD
		public static bool IsOdd(int value)
		{
			return value % 2 != 0;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000169D5 File Offset: 0x00014BD5
		public static Vector3 Lerp(Vector3 p1, Vector3 p2, float t)
		{
			return p1 + (p2 - p1) * t;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000169EA File Offset: 0x00014BEA
		public static float Lerp(float p1, float p2, float t)
		{
			return p1 + (p2 - p1) * t;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000169F4 File Offset: 0x00014BF4
		public static void NullError(object sender, string variableName, string extraNotes = "")
		{
			if (sender == null)
			{
				Debug.Log("NULL SENDER: " + variableName + " is null! " + extraNotes);
				return;
			}
			Debug.Log(string.Concat(new string[]
			{
				sender.GetType().ToString(),
				": ",
				variableName,
				" is null! ",
				extraNotes
			}));
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00016A51 File Offset: 0x00014C51
		public static float Percent(float min, float max, float value)
		{
			if (max - min == 0f)
			{
				return 0f;
			}
			return Util.Clamp(0f, 1f, (value - min) / (max - min));
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00016A79 File Offset: 0x00014C79
		public static float PercentUnclampled(float min, float max, float value)
		{
			if (max - min == 0f)
			{
				Debug.Log("WARNING: A class attempted to find an unclamped percentage of 0!");
				return 0f;
			}
			return (value - min) / (max - min);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00016A9C File Offset: 0x00014C9C
		public static bool RectContainsRect(Vector2 extremeMinA, Vector2 extremeMaxA, Vector2 extremeMinB, Vector2 extremeMaxB)
		{
			return extremeMinA.y <= extremeMaxB.y && extremeMaxA.y >= extremeMinB.y && extremeMinA.x <= extremeMaxB.x && extremeMaxA.x >= extremeMinB.x;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00016AD9 File Offset: 0x00014CD9
		public static Vector3 ReflectOnXAxis(Vector3 _vector)
		{
			_vector.x = -_vector.x;
			return _vector;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00016AEA File Offset: 0x00014CEA
		public static Vector3 ReflectOnXandYAxis(Vector3 _vector)
		{
			_vector.x = -_vector.x;
			_vector.y = -_vector.y;
			return _vector;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00016B09 File Offset: 0x00014D09
		public static Vector3 ReflectOnYAxis(Vector3 _vector)
		{
			_vector.y = -_vector.y;
			return _vector;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00016B1C File Offset: 0x00014D1C
		public static float VectorToDegrees(Vector2 _vector)
		{
			float num = Mathf.Atan2(_vector.y, _vector.x) * 57.29578f;
			if (num <= 0f)
			{
				return num + 360f;
			}
			return num;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00016B53 File Offset: 0x00014D53
		public static Vector3 XYplaneUpDirection()
		{
			return new Vector3(0f, 0f, -1f);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00016B6C File Offset: 0x00014D6C
		public static bool IsPointOnMainCamera(Vector3 _point)
		{
			if (Camera.main == null)
			{
				return false;
			}
			Vector3 vector = Camera.main.WorldToScreenPoint(_point);
			return vector.x >= 0f && vector.y <= (float)Screen.width && vector.y >= 0f && vector.y <= (float)Screen.height;
		}
	}
}
