using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000042 RID: 66
	internal static class TextUtilities
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x0001A210 File Offset: 0x00018410
		public static bool IsIntersectingRectTransform(RectTransform rectTransform, Vector3 position, Camera camera)
		{
			TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			rectTransform.GetWorldCorners(TextUtilities.s_RectWorldCorners);
			return TextUtilities.PointIntersectRectangle(position, TextUtilities.s_RectWorldCorners[0], TextUtilities.s_RectWorldCorners[1], TextUtilities.s_RectWorldCorners[2], TextUtilities.s_RectWorldCorners[3]);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0001A270 File Offset: 0x00018470
		private static bool PointIntersectRectangle(Vector3 m, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = m - a;
			Vector3 vector3 = c - b;
			Vector3 vector4 = m - b;
			float num = Vector3.Dot(vector, vector2);
			float num2 = Vector3.Dot(vector3, vector4);
			return 0f <= num && num <= Vector3.Dot(vector, vector) && 0f <= num2 && num2 <= Vector3.Dot(vector3, vector3);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0001A2E4 File Offset: 0x000184E4
		public static bool ScreenPointToWorldPointInRectangle(Transform transform, Vector2 screenPoint, Camera cam, out Vector3 worldPoint)
		{
			worldPoint = Vector3.zero;
			Ray ray = cam.ScreenPointToRay(screenPoint);
			float num;
			bool flag = !new Plane(transform.rotation * Vector3.back, transform.position).Raycast(ray, out num);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				worldPoint = ray.GetPoint(num);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0001A354 File Offset: 0x00018554
		private static bool IntersectLinePlane(TextUtilities.LineSegment line, Vector3 point, Vector3 normal, out Vector3 intersectingPoint)
		{
			intersectingPoint = Vector3.zero;
			Vector3 vector = line.Point2 - line.Point1;
			Vector3 vector2 = line.Point1 - point;
			float num = Vector3.Dot(normal, vector);
			float num2 = -Vector3.Dot(normal, vector2);
			bool flag = Mathf.Abs(num) < Mathf.Epsilon;
			bool flag2;
			if (flag)
			{
				flag2 = num2 == 0f;
			}
			else
			{
				float num3 = num2 / num;
				bool flag3 = num3 < 0f || num3 > 1f;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					intersectingPoint = line.Point1 + num3 * vector;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0001A404 File Offset: 0x00018604
		public static float DistanceToLine(Vector3 a, Vector3 b, Vector3 point)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = a - point;
			float num = Vector3.Dot(vector, vector2);
			bool flag = num > 0f;
			float num2;
			if (flag)
			{
				num2 = Vector3.Dot(vector2, vector2);
			}
			else
			{
				Vector3 vector3 = point - b;
				bool flag2 = Vector3.Dot(vector, vector3) > 0f;
				if (flag2)
				{
					num2 = Vector3.Dot(vector3, vector3);
				}
				else
				{
					Vector3 vector4 = vector2 - vector * (num / Vector3.Dot(vector, vector));
					num2 = Vector3.Dot(vector4, vector4);
				}
			}
			return num2;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0001A490 File Offset: 0x00018690
		public static char ToLowerFast(char c)
		{
			bool flag = (int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1;
			char c2;
			if (flag)
			{
				c2 = c;
			}
			else
			{
				c2 = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".get_Chars((int)c);
			}
			return c2;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001A4C4 File Offset: 0x000186C4
		public static char ToUpperFast(char c)
		{
			bool flag = (int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1;
			char c2;
			if (flag)
			{
				c2 = c;
			}
			else
			{
				c2 = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".get_Chars((int)c);
			}
			return c2;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0001A4F8 File Offset: 0x000186F8
		public static uint ToUpperASCIIFast(uint c)
		{
			bool flag = (ulong)c > (ulong)((long)("-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1));
			uint num;
			if (flag)
			{
				num = c;
			}
			else
			{
				num = (uint)"-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".get_Chars((int)c);
			}
			return num;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0001A530 File Offset: 0x00018730
		public static uint ToLowerASCIIFast(uint c)
		{
			bool flag = (ulong)c > (ulong)((long)("-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1));
			uint num;
			if (flag)
			{
				num = c;
			}
			else
			{
				num = (uint)"-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".get_Chars((int)c);
			}
			return num;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0001A568 File Offset: 0x00018768
		public static int GetHashCodeCaseSensitive(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num) ^ (int)s.get_Chars(i);
			}
			return num;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0001A5A0 File Offset: 0x000187A0
		public static int GetHashCodeCaseInSensitive(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num) ^ (int)TextUtilities.ToUpperASCIIFast((uint)s.get_Chars(i));
			}
			return num;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0001A5E0 File Offset: 0x000187E0
		public static uint GetSimpleHashCodeLowercase(string s)
		{
			uint num = 5381U;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num) ^ (uint)TextUtilities.ToLowerFast(s.get_Chars(i));
			}
			return num;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0001A624 File Offset: 0x00018824
		public static int HexToInt(char hex)
		{
			switch (hex)
			{
			case '0':
				return 0;
			case '1':
				return 1;
			case '2':
				return 2;
			case '3':
				return 3;
			case '4':
				return 4;
			case '5':
				return 5;
			case '6':
				return 6;
			case '7':
				return 7;
			case '8':
				return 8;
			case '9':
				return 9;
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				break;
			case 'A':
				return 10;
			case 'B':
				return 11;
			case 'C':
				return 12;
			case 'D':
				return 13;
			case 'E':
				return 14;
			case 'F':
				return 15;
			default:
				switch (hex)
				{
				case 'a':
					return 10;
				case 'b':
					return 11;
				case 'c':
					return 12;
				case 'd':
					return 13;
				case 'e':
					return 14;
				case 'f':
					return 15;
				}
				break;
			}
			return 15;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0001A72C File Offset: 0x0001892C
		public static int StringHexToInt(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num += TextUtilities.HexToInt(s.get_Chars(i)) * (int)Mathf.Pow(16f, (float)(s.Length - 1 - i));
			}
			return num;
		}

		// Token: 0x04000363 RID: 867
		private static Vector3[] s_RectWorldCorners = new Vector3[4];

		// Token: 0x04000364 RID: 868
		private const string k_LookupStringL = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-";

		// Token: 0x04000365 RID: 869
		private const string k_LookupStringU = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-";

		// Token: 0x02000043 RID: 67
		private struct LineSegment
		{
			// Token: 0x060001C3 RID: 451 RVA: 0x0001A78B File Offset: 0x0001898B
			public LineSegment(Vector3 p1, Vector3 p2)
			{
				this.Point1 = p1;
				this.Point2 = p2;
			}

			// Token: 0x04000366 RID: 870
			public Vector3 Point1;

			// Token: 0x04000367 RID: 871
			public Vector3 Point2;
		}
	}
}
