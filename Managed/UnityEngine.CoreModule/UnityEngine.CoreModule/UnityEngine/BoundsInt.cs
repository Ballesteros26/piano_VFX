using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000C4 RID: 196
	[UsedByNativeCode]
	public struct BoundsInt : IEquatable<BoundsInt>, IFormattable
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00007310 File Offset: 0x00005510
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0000732D File Offset: 0x0000552D
		public int x
		{
			get
			{
				return this.m_Position.x;
			}
			set
			{
				this.m_Position.x = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00007340 File Offset: 0x00005540
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000735D File Offset: 0x0000555D
		public int y
		{
			get
			{
				return this.m_Position.y;
			}
			set
			{
				this.m_Position.y = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00007370 File Offset: 0x00005570
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0000738D File Offset: 0x0000558D
		public int z
		{
			get
			{
				return this.m_Position.z;
			}
			set
			{
				this.m_Position.z = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000073A0 File Offset: 0x000055A0
		public Vector3 center
		{
			get
			{
				return new Vector3((float)this.x + (float)this.m_Size.x / 2f, (float)this.y + (float)this.m_Size.y / 2f, (float)this.z + (float)this.m_Size.z / 2f);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00007408 File Offset: 0x00005608
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00007431 File Offset: 0x00005631
		public Vector3Int min
		{
			get
			{
				return new Vector3Int(this.xMin, this.yMin, this.zMin);
			}
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
				this.zMin = value.z;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00007460 File Offset: 0x00005660
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00007489 File Offset: 0x00005689
		public Vector3Int max
		{
			get
			{
				return new Vector3Int(this.xMax, this.yMax, this.zMax);
			}
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
				this.zMax = value.z;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000074B8 File Offset: 0x000056B8
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x000074F4 File Offset: 0x000056F4
		public int xMin
		{
			get
			{
				return Math.Min(this.m_Position.x, this.m_Position.x + this.m_Size.x);
			}
			set
			{
				int xMax = this.xMax;
				this.m_Position.x = value;
				this.m_Size.x = xMax - this.m_Position.x;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00007530 File Offset: 0x00005730
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x0000756C File Offset: 0x0000576C
		public int yMin
		{
			get
			{
				return Math.Min(this.m_Position.y, this.m_Position.y + this.m_Size.y);
			}
			set
			{
				int yMax = this.yMax;
				this.m_Position.y = value;
				this.m_Size.y = yMax - this.m_Position.y;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x000075A8 File Offset: 0x000057A8
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x000075E4 File Offset: 0x000057E4
		public int zMin
		{
			get
			{
				return Math.Min(this.m_Position.z, this.m_Position.z + this.m_Size.z);
			}
			set
			{
				int zMax = this.zMax;
				this.m_Position.z = value;
				this.m_Size.z = zMax - this.m_Position.z;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00007620 File Offset: 0x00005820
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00007659 File Offset: 0x00005859
		public int xMax
		{
			get
			{
				return Math.Max(this.m_Position.x, this.m_Position.x + this.m_Size.x);
			}
			set
			{
				this.m_Size.x = value - this.m_Position.x;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00007678 File Offset: 0x00005878
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x000076B1 File Offset: 0x000058B1
		public int yMax
		{
			get
			{
				return Math.Max(this.m_Position.y, this.m_Position.y + this.m_Size.y);
			}
			set
			{
				this.m_Size.y = value - this.m_Position.y;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000076D0 File Offset: 0x000058D0
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x00007709 File Offset: 0x00005909
		public int zMax
		{
			get
			{
				return Math.Max(this.m_Position.z, this.m_Position.z + this.m_Size.z);
			}
			set
			{
				this.m_Size.z = value - this.m_Position.z;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00007728 File Offset: 0x00005928
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00007740 File Offset: 0x00005940
		public Vector3Int position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000774C File Offset: 0x0000594C
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00007764 File Offset: 0x00005964
		public Vector3Int size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000776E File Offset: 0x0000596E
		public BoundsInt(int xMin, int yMin, int zMin, int sizeX, int sizeY, int sizeZ)
		{
			this.m_Position = new Vector3Int(xMin, yMin, zMin);
			this.m_Size = new Vector3Int(sizeX, sizeY, sizeZ);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00007790 File Offset: 0x00005990
		public BoundsInt(Vector3Int position, Vector3Int size)
		{
			this.m_Position = position;
			this.m_Size = size;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000077A1 File Offset: 0x000059A1
		public void SetMinMax(Vector3Int minPosition, Vector3Int maxPosition)
		{
			this.min = minPosition;
			this.max = maxPosition;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000077B4 File Offset: 0x000059B4
		public void ClampToBounds(BoundsInt bounds)
		{
			this.position = new Vector3Int(Math.Max(Math.Min(bounds.xMax, this.position.x), bounds.xMin), Math.Max(Math.Min(bounds.yMax, this.position.y), bounds.yMin), Math.Max(Math.Min(bounds.zMax, this.position.z), bounds.zMin));
			this.size = new Vector3Int(Math.Min(bounds.xMax - this.position.x, this.size.x), Math.Min(bounds.yMax - this.position.y, this.size.y), Math.Min(bounds.zMax - this.position.z, this.size.z));
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000078C8 File Offset: 0x00005AC8
		public bool Contains(Vector3Int position)
		{
			return position.x >= this.xMin && position.y >= this.yMin && position.z >= this.zMin && position.x < this.xMax && position.y < this.yMax && position.z < this.zMax;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00007938 File Offset: 0x00005B38
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000795C File Offset: 0x00005B5C
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00007980 File Offset: 0x00005B80
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return UnityString.Format("Position: {0}, Size: {1}", new object[]
			{
				this.m_Position.ToString(format, formatProvider),
				this.m_Size.ToString(format, formatProvider)
			});
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000079C4 File Offset: 0x00005BC4
		public static bool operator ==(BoundsInt lhs, BoundsInt rhs)
		{
			return lhs.m_Position == rhs.m_Position && lhs.m_Size == rhs.m_Size;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00007A00 File Offset: 0x00005C00
		public static bool operator !=(BoundsInt lhs, BoundsInt rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00007A1C File Offset: 0x00005C1C
		public override bool Equals(object other)
		{
			bool flag = !(other is BoundsInt);
			return !flag && this.Equals((BoundsInt)other);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00007A50 File Offset: 0x00005C50
		public bool Equals(BoundsInt other)
		{
			return this.m_Position.Equals(other.m_Position) && this.m_Size.Equals(other.m_Size);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00007A8C File Offset: 0x00005C8C
		public override int GetHashCode()
		{
			return this.m_Position.GetHashCode() ^ (this.m_Size.GetHashCode() << 2);
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00007AC4 File Offset: 0x00005CC4
		public BoundsInt.PositionEnumerator allPositionsWithin
		{
			get
			{
				return new BoundsInt.PositionEnumerator(this.min, this.max);
			}
		}

		// Token: 0x04000239 RID: 569
		private Vector3Int m_Position;

		// Token: 0x0400023A RID: 570
		private Vector3Int m_Size;

		// Token: 0x020000C5 RID: 197
		public struct PositionEnumerator : IEnumerator<Vector3Int>, IEnumerator, IDisposable
		{
			// Token: 0x060004F6 RID: 1270 RVA: 0x00007AE8 File Offset: 0x00005CE8
			public PositionEnumerator(Vector3Int min, Vector3Int max)
			{
				this._current = min;
				this._min = min;
				this._max = max;
				this.Reset();
			}

			// Token: 0x060004F7 RID: 1271 RVA: 0x00007B14 File Offset: 0x00005D14
			public BoundsInt.PositionEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x060004F8 RID: 1272 RVA: 0x00007B2C File Offset: 0x00005D2C
			public bool MoveNext()
			{
				bool flag = this._current.z >= this._max.z;
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
							this._current.y = this._min.y;
							num = this._current.z;
							this._current.z = num + 1;
							bool flag5 = this._current.z >= this._max.z;
							if (flag5)
							{
								return false;
							}
						}
					}
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x060004F9 RID: 1273 RVA: 0x00007C58 File Offset: 0x00005E58
			public void Reset()
			{
				this._current = this._min;
				int x = this._current.x;
				this._current.x = x - 1;
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x060004FA RID: 1274 RVA: 0x00007C88 File Offset: 0x00005E88
			public Vector3Int Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x060004FB RID: 1275 RVA: 0x00007CA0 File Offset: 0x00005EA0
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060004FC RID: 1276 RVA: 0x00002EC3 File Offset: 0x000010C3
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0400023B RID: 571
			private readonly Vector3Int _min;

			// Token: 0x0400023C RID: 572
			private readonly Vector3Int _max;

			// Token: 0x0400023D RID: 573
			private Vector3Int _current;
		}
	}
}
