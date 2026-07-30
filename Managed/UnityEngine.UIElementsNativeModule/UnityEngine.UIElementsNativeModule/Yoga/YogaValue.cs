using System;

namespace UnityEngine.Yoga
{
	// Token: 0x0200001E RID: 30
	internal struct YogaValue
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00003960 File Offset: 0x00001B60
		public YogaUnit Unit
		{
			get
			{
				return this.unit;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00003978 File Offset: 0x00001B78
		public float Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00003990 File Offset: 0x00001B90
		public static YogaValue Point(float value)
		{
			return new YogaValue
			{
				value = value,
				unit = (YogaConstants.IsUndefined(value) ? YogaUnit.Undefined : YogaUnit.Point)
			};
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000039C8 File Offset: 0x00001BC8
		public bool Equals(YogaValue other)
		{
			return this.Unit == other.Unit && (this.Value.Equals(other.Value) || this.Unit == YogaUnit.Undefined);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00003A10 File Offset: 0x00001C10
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is YogaValue && this.Equals((YogaValue)obj);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00003A48 File Offset: 0x00001C48
		public override int GetHashCode()
		{
			return (this.Value.GetHashCode() * 397) ^ (int)this.Unit;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00003A78 File Offset: 0x00001C78
		public static YogaValue Undefined()
		{
			return new YogaValue
			{
				value = float.NaN,
				unit = YogaUnit.Undefined
			};
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00003AA8 File Offset: 0x00001CA8
		public static YogaValue Auto()
		{
			return new YogaValue
			{
				value = float.NaN,
				unit = YogaUnit.Auto
			};
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public static YogaValue Percent(float value)
		{
			return new YogaValue
			{
				value = value,
				unit = (YogaConstants.IsUndefined(value) ? YogaUnit.Undefined : YogaUnit.Percent)
			};
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00003B10 File Offset: 0x00001D10
		public static implicit operator YogaValue(float pointValue)
		{
			return YogaValue.Point(pointValue);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00003B28 File Offset: 0x00001D28
		internal static YogaValue MarshalValue(YogaValue value)
		{
			return value;
		}

		// Token: 0x04000057 RID: 87
		private float value;

		// Token: 0x04000058 RID: 88
		private YogaUnit unit;
	}
}
