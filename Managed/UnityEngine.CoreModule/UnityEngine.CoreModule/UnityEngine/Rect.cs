using System;
using System.Globalization;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000CA RID: 202
	[NativeClass("Rectf", "template<typename T> class RectT; typedef RectT<float> Rectf;")]
	[NativeHeader("Runtime/Math/Rect.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Rect : IEquatable<Rect>, IFormattable
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0000853E File Offset: 0x0000673E
		public Rect(float x, float y, float width, float height)
		{
			this.m_XMin = x;
			this.m_YMin = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000855E File Offset: 0x0000675E
		public Rect(Vector2 position, Vector2 size)
		{
			this.m_XMin = position.x;
			this.m_YMin = position.y;
			this.m_Width = size.x;
			this.m_Height = size.y;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00008591 File Offset: 0x00006791
		public Rect(Rect source)
		{
			this.m_XMin = source.m_XMin;
			this.m_YMin = source.m_YMin;
			this.m_Width = source.m_Width;
			this.m_Height = source.m_Height;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x000085C4 File Offset: 0x000067C4
		public static Rect zero
		{
			get
			{
				return new Rect(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000085E0 File Offset: 0x000067E0
		public static Rect MinMaxRect(float xmin, float ymin, float xmax, float ymax)
		{
			return new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000853E File Offset: 0x0000673E
		public void Set(float x, float y, float width, float height)
		{
			this.m_XMin = x;
			this.m_YMin = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00008600 File Offset: 0x00006800
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00008618 File Offset: 0x00006818
		public float x
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				this.m_XMin = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00008624 File Offset: 0x00006824
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0000863C File Offset: 0x0000683C
		public float y
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				this.m_YMin = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00008648 File Offset: 0x00006848
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0000866B File Offset: 0x0000686B
		public Vector2 position
		{
			get
			{
				return new Vector2(this.m_XMin, this.m_YMin);
			}
			set
			{
				this.m_XMin = value.x;
				this.m_YMin = value.y;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00008688 File Offset: 0x00006888
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x000086C5 File Offset: 0x000068C5
		public Vector2 center
		{
			get
			{
				return new Vector2(this.x + this.m_Width / 2f, this.y + this.m_Height / 2f);
			}
			set
			{
				this.m_XMin = value.x - this.m_Width / 2f;
				this.m_YMin = value.y - this.m_Height / 2f;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000086FC File Offset: 0x000068FC
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0000871F File Offset: 0x0000691F
		public Vector2 min
		{
			get
			{
				return new Vector2(this.xMin, this.yMin);
			}
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000873C File Offset: 0x0000693C
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0000875F File Offset: 0x0000695F
		public Vector2 max
		{
			get
			{
				return new Vector2(this.xMax, this.yMax);
			}
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000877C File Offset: 0x0000697C
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x00008794 File Offset: 0x00006994
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x000087A0 File Offset: 0x000069A0
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x000087B8 File Offset: 0x000069B8
		public float height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x000087C4 File Offset: 0x000069C4
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x000087E7 File Offset: 0x000069E7
		public Vector2 size
		{
			get
			{
				return new Vector2(this.m_Width, this.m_Height);
			}
			set
			{
				this.m_Width = value.x;
				this.m_Height = value.y;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00008804 File Offset: 0x00006A04
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0000881C File Offset: 0x00006A1C
		public float xMin
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				float xMax = this.xMax;
				this.m_XMin = value;
				this.m_Width = xMax - this.m_XMin;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00008848 File Offset: 0x00006A48
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x00008860 File Offset: 0x00006A60
		public float yMin
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				float yMax = this.yMax;
				this.m_YMin = value;
				this.m_Height = yMax - this.m_YMin;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0000888C File Offset: 0x00006A8C
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x000088AB File Offset: 0x00006AAB
		public float xMax
		{
			get
			{
				return this.m_Width + this.m_XMin;
			}
			set
			{
				this.m_Width = value - this.m_XMin;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000088BC File Offset: 0x00006ABC
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x000088DB File Offset: 0x00006ADB
		public float yMax
		{
			get
			{
				return this.m_Height + this.m_YMin;
			}
			set
			{
				this.m_Height = value - this.m_YMin;
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000088EC File Offset: 0x00006AEC
		public bool Contains(Vector2 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000893C File Offset: 0x00006B3C
		public bool Contains(Vector3 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000898C File Offset: 0x00006B8C
		public bool Contains(Vector3 point, bool allowInverse)
		{
			bool flag = !allowInverse;
			bool flag2;
			if (flag)
			{
				flag2 = this.Contains(point);
			}
			else
			{
				bool flag3 = (this.width < 0f && point.x <= this.xMin && point.x > this.xMax) || (this.width >= 0f && point.x >= this.xMin && point.x < this.xMax);
				bool flag4 = (this.height < 0f && point.y <= this.yMin && point.y > this.yMax) || (this.height >= 0f && point.y >= this.yMin && point.y < this.yMax);
				flag2 = flag3 && flag4;
			}
			return flag2;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00008A6C File Offset: 0x00006C6C
		private static Rect OrderMinMax(Rect rect)
		{
			bool flag = rect.xMin > rect.xMax;
			if (flag)
			{
				float xMin = rect.xMin;
				rect.xMin = rect.xMax;
				rect.xMax = xMin;
			}
			bool flag2 = rect.yMin > rect.yMax;
			if (flag2)
			{
				float yMin = rect.yMin;
				rect.yMin = rect.yMax;
				rect.yMax = yMin;
			}
			return rect;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00008AF0 File Offset: 0x00006CF0
		public bool Overlaps(Rect other)
		{
			return other.xMax > this.xMin && other.xMin < this.xMax && other.yMax > this.yMin && other.yMin < this.yMax;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00008B44 File Offset: 0x00006D44
		public bool Overlaps(Rect other, bool allowInverse)
		{
			Rect rect = this;
			if (allowInverse)
			{
				rect = Rect.OrderMinMax(rect);
				other = Rect.OrderMinMax(other);
			}
			return rect.Overlaps(other);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00008B7C File Offset: 0x00006D7C
		public static Vector2 NormalizedToPoint(Rect rectangle, Vector2 normalizedRectCoordinates)
		{
			return new Vector2(Mathf.Lerp(rectangle.x, rectangle.xMax, normalizedRectCoordinates.x), Mathf.Lerp(rectangle.y, rectangle.yMax, normalizedRectCoordinates.y));
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00008BC8 File Offset: 0x00006DC8
		public static Vector2 PointToNormalized(Rect rectangle, Vector2 point)
		{
			return new Vector2(Mathf.InverseLerp(rectangle.x, rectangle.xMax, point.x), Mathf.InverseLerp(rectangle.y, rectangle.yMax, point.y));
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00008C14 File Offset: 0x00006E14
		public static bool operator !=(Rect lhs, Rect rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00008C30 File Offset: 0x00006E30
		public static bool operator ==(Rect lhs, Rect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00008C88 File Offset: 0x00006E88
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.width.GetHashCode() << 2) ^ (this.y.GetHashCode() >> 2) ^ (this.height.GetHashCode() >> 1);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00008CDC File Offset: 0x00006EDC
		public override bool Equals(object other)
		{
			bool flag = !(other is Rect);
			return !flag && this.Equals((Rect)other);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00008D10 File Offset: 0x00006F10
		public bool Equals(Rect other)
		{
			return this.x.Equals(other.x) && this.y.Equals(other.y) && this.width.Equals(other.width) && this.height.Equals(other.height);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00008D80 File Offset: 0x00006F80
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00008DA4 File Offset: 0x00006FA4
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00008DC8 File Offset: 0x00006FC8
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F2";
			}
			return UnityString.Format("(x:{0}, y:{1}, width:{2}, height:{3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.width.ToString(format, formatProvider),
				this.height.ToString(format, formatProvider)
			});
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00008E48 File Offset: 0x00007048
		[Obsolete("use xMin")]
		public float left
		{
			get
			{
				return this.m_XMin;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00008E60 File Offset: 0x00007060
		[Obsolete("use xMax")]
		public float right
		{
			get
			{
				return this.m_XMin + this.m_Width;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00008E80 File Offset: 0x00007080
		[Obsolete("use yMin")]
		public float top
		{
			get
			{
				return this.m_YMin;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00008E98 File Offset: 0x00007098
		[Obsolete("use yMax")]
		public float bottom
		{
			get
			{
				return this.m_YMin + this.m_Height;
			}
		}

		// Token: 0x04000245 RID: 581
		[NativeName("x")]
		private float m_XMin;

		// Token: 0x04000246 RID: 582
		[NativeName("y")]
		private float m_YMin;

		// Token: 0x04000247 RID: 583
		[NativeName("width")]
		private float m_Width;

		// Token: 0x04000248 RID: 584
		[NativeName("height")]
		private float m_Height;
	}
}
