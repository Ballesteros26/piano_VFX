using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000CB RID: 203
	[UsedByNativeCode]
	public struct RectInt : IEquatable<RectInt>, IFormattable
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00008EB8 File Offset: 0x000070B8
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00008ED0 File Offset: 0x000070D0
		public int x
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

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00008EDC File Offset: 0x000070DC
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00008EF4 File Offset: 0x000070F4
		public int y
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

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00008F00 File Offset: 0x00007100
		public Vector2 center
		{
			get
			{
				return new Vector2((float)this.x + (float)this.m_Width / 2f, (float)this.y + (float)this.m_Height / 2f);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00008F44 File Offset: 0x00007144
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00008F67 File Offset: 0x00007167
		public Vector2Int min
		{
			get
			{
				return new Vector2Int(this.xMin, this.yMin);
			}
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00008F88 File Offset: 0x00007188
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x00008FAB File Offset: 0x000071AB
		public Vector2Int max
		{
			get
			{
				return new Vector2Int(this.xMax, this.yMax);
			}
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00008FCC File Offset: 0x000071CC
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00008FE4 File Offset: 0x000071E4
		public int width
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

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00008FF0 File Offset: 0x000071F0
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x00009008 File Offset: 0x00007208
		public int height
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

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00009014 File Offset: 0x00007214
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00009040 File Offset: 0x00007240
		public int xMin
		{
			get
			{
				return Math.Min(this.m_XMin, this.m_XMin + this.m_Width);
			}
			set
			{
				int xMax = this.xMax;
				this.m_XMin = value;
				this.m_Width = xMax - this.m_XMin;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000906C File Offset: 0x0000726C
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x00009098 File Offset: 0x00007298
		public int yMin
		{
			get
			{
				return Math.Min(this.m_YMin, this.m_YMin + this.m_Height);
			}
			set
			{
				int yMax = this.yMax;
				this.m_YMin = value;
				this.m_Height = yMax - this.m_YMin;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000090C4 File Offset: 0x000072C4
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x000090EE File Offset: 0x000072EE
		public int xMax
		{
			get
			{
				return Math.Max(this.m_XMin, this.m_XMin + this.m_Width);
			}
			set
			{
				this.m_Width = value - this.m_XMin;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00009100 File Offset: 0x00007300
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x0000912A File Offset: 0x0000732A
		public int yMax
		{
			get
			{
				return Math.Max(this.m_YMin, this.m_YMin + this.m_Height);
			}
			set
			{
				this.m_Height = value - this.m_YMin;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000913C File Offset: 0x0000733C
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x0000915F File Offset: 0x0000735F
		public Vector2Int position
		{
			get
			{
				return new Vector2Int(this.m_XMin, this.m_YMin);
			}
			set
			{
				this.m_XMin = value.x;
				this.m_YMin = value.y;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000917C File Offset: 0x0000737C
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0000919F File Offset: 0x0000739F
		public Vector2Int size
		{
			get
			{
				return new Vector2Int(this.m_Width, this.m_Height);
			}
			set
			{
				this.m_Width = value.x;
				this.m_Height = value.y;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000091BC File Offset: 0x000073BC
		public void SetMinMax(Vector2Int minPosition, Vector2Int maxPosition)
		{
			this.min = minPosition;
			this.max = maxPosition;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000091CF File Offset: 0x000073CF
		public RectInt(int xMin, int yMin, int width, int height)
		{
			this.m_XMin = xMin;
			this.m_YMin = yMin;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000091EF File Offset: 0x000073EF
		public RectInt(Vector2Int position, Vector2Int size)
		{
			this.m_XMin = position.x;
			this.m_YMin = position.y;
			this.m_Width = size.x;
			this.m_Height = size.y;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00009228 File Offset: 0x00007428
		public void ClampToBounds(RectInt bounds)
		{
			this.position = new Vector2Int(Math.Max(Math.Min(bounds.xMax, this.position.x), bounds.xMin), Math.Max(Math.Min(bounds.yMax, this.position.y), bounds.yMin));
			this.size = new Vector2Int(Math.Min(bounds.xMax - this.position.x, this.size.x), Math.Min(bounds.yMax - this.position.y, this.size.y));
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000092EC File Offset: 0x000074EC
		public bool Contains(Vector2Int position)
		{
			return position.x >= this.xMin && position.y >= this.yMin && position.x < this.xMax && position.y < this.yMax;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00009340 File Offset: 0x00007540
		public bool Overlaps(RectInt other)
		{
			return other.xMin < this.xMax && other.xMax > this.xMin && other.yMin < this.yMax && other.yMax > this.yMin;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00009394 File Offset: 0x00007594
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000093B8 File Offset: 0x000075B8
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000093DC File Offset: 0x000075DC
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return UnityString.Format("(x:{0}, y:{1}, width:{2}, height:{3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.width.ToString(format, formatProvider),
				this.height.ToString(format, formatProvider)
			});
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000944C File Offset: 0x0000764C
		public bool Equals(RectInt other)
		{
			return this.m_XMin == other.m_XMin && this.m_YMin == other.m_YMin && this.m_Width == other.m_Width && this.m_Height == other.m_Height;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000949C File Offset: 0x0000769C
		public RectInt.PositionEnumerator allPositionsWithin
		{
			get
			{
				return new RectInt.PositionEnumerator(this.min, this.max);
			}
		}

		// Token: 0x04000249 RID: 585
		private int m_XMin;

		// Token: 0x0400024A RID: 586
		private int m_YMin;

		// Token: 0x0400024B RID: 587
		private int m_Width;

		// Token: 0x0400024C RID: 588
		private int m_Height;

		// Token: 0x020000CC RID: 204
		public struct PositionEnumerator : IEnumerator<Vector2Int>, IEnumerator, IDisposable
		{
			// Token: 0x06000589 RID: 1417 RVA: 0x000094C0 File Offset: 0x000076C0
			public PositionEnumerator(Vector2Int min, Vector2Int max)
			{
				this._current = min;
				this._min = min;
				this._max = max;
				this.Reset();
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x000094EC File Offset: 0x000076EC
			public RectInt.PositionEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x00009504 File Offset: 0x00007704
			public bool MoveNext()
			{
				bool flag = this._current.y >= this._max.y;
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					int num = this._current.x;
					this._current.x = num + 1;
					bool flag3 = this._current.x >= this._max.x;
					if (flag3)
					{
						this._current.x = this._min.x;
						num = this._current.y;
						this._current.y = num + 1;
						bool flag4 = this._current.y >= this._max.y;
						if (flag4)
						{
							return false;
						}
					}
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x0600058C RID: 1420 RVA: 0x000095D8 File Offset: 0x000077D8
			public void Reset()
			{
				this._current = this._min;
				int x = this._current.x;
				this._current.x = x - 1;
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x0600058D RID: 1421 RVA: 0x00009608 File Offset: 0x00007808
			public Vector2Int Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x0600058E RID: 1422 RVA: 0x00009620 File Offset: 0x00007820
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x00002EC3 File Offset: 0x000010C3
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0400024D RID: 589
			private readonly Vector2Int _min;

			// Token: 0x0400024E RID: 590
			private readonly Vector2Int _max;

			// Token: 0x0400024F RID: 591
			private Vector2Int _current;
		}
	}
}
