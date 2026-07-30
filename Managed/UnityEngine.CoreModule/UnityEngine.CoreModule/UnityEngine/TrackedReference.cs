using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C0 RID: 448
	[UsedByNativeCode]
	[StructLayout(0)]
	public class TrackedReference
	{
		// Token: 0x06001408 RID: 5128 RVA: 0x000166AA File Offset: 0x000148AA
		protected TrackedReference()
		{
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00020D60 File Offset: 0x0001EF60
		public static bool operator ==(TrackedReference x, TrackedReference y)
		{
			bool flag = y == null && x == null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = y == null;
				if (flag3)
				{
					flag2 = x.m_Ptr == IntPtr.Zero;
				}
				else
				{
					bool flag4 = x == null;
					if (flag4)
					{
						flag2 = y.m_Ptr == IntPtr.Zero;
					}
					else
					{
						flag2 = x.m_Ptr == y.m_Ptr;
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00020DD4 File Offset: 0x0001EFD4
		public static bool operator !=(TrackedReference x, TrackedReference y)
		{
			return !(x == y);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00020DF0 File Offset: 0x0001EFF0
		public override bool Equals(object o)
		{
			return o as TrackedReference == this;
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00020E10 File Offset: 0x0001F010
		public override int GetHashCode()
		{
			return (int)this.m_Ptr;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00020E30 File Offset: 0x0001F030
		public static implicit operator bool(TrackedReference exists)
		{
			return exists != null;
		}

		// Token: 0x04000667 RID: 1639
		internal IntPtr m_Ptr;
	}
}
