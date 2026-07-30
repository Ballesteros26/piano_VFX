using System;

namespace UnityEngine.Experimental.AI
{
	// Token: 0x0200001C RID: 28
	public struct PolygonId : IEquatable<PolygonId>
	{
		// Token: 0x06000167 RID: 359 RVA: 0x000030C8 File Offset: 0x000012C8
		public bool IsNull()
		{
			return this.polyRef == 0UL;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000030E4 File Offset: 0x000012E4
		public static bool operator ==(PolygonId x, PolygonId y)
		{
			return x.polyRef == y.polyRef;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00003104 File Offset: 0x00001304
		public static bool operator !=(PolygonId x, PolygonId y)
		{
			return x.polyRef != y.polyRef;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00003128 File Offset: 0x00001328
		public override int GetHashCode()
		{
			return this.polyRef.GetHashCode();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00003148 File Offset: 0x00001348
		public bool Equals(PolygonId rhs)
		{
			return rhs == this;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00003168 File Offset: 0x00001368
		public override bool Equals(object obj)
		{
			bool flag = obj == null || !(obj is PolygonId);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				PolygonId polygonId = (PolygonId)obj;
				flag2 = polygonId == this;
			}
			return flag2;
		}

		// Token: 0x04000060 RID: 96
		internal ulong polyRef;
	}
}
