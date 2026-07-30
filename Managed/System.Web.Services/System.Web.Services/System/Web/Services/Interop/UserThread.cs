using System;
using System.Runtime.InteropServices;

namespace System.Web.Services.Interop
{
	// Token: 0x0200009A RID: 154
	[StructLayout(LayoutKind.Sequential)]
	internal class UserThread
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x0001235C File Offset: 0x0001055C
		internal UserThread()
		{
			this.pSidBuffer = 0;
			this.dwSidLen = 0;
			this.dwTid = 0;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001237C File Offset: 0x0001057C
		public override bool Equals(object obj)
		{
			if (!(obj is UserThread))
			{
				return false;
			}
			UserThread userThread = (UserThread)obj;
			return userThread.dwTid == this.dwTid && userThread.pSidBuffer == this.pSidBuffer && userThread.dwSidLen == this.dwSidLen;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000123C7 File Offset: 0x000105C7
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000320 RID: 800
		internal int pSidBuffer;

		// Token: 0x04000321 RID: 801
		internal int dwSidLen;

		// Token: 0x04000322 RID: 802
		internal int dwTid;
	}
}
