using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001CA RID: 458
	[RequiredByNativeCode]
	[StructLayout(0)]
	public sealed class WaitForSeconds : YieldInstruction
	{
		// Token: 0x06001465 RID: 5221 RVA: 0x00021894 File Offset: 0x0001FA94
		public WaitForSeconds(float seconds)
		{
			this.m_Seconds = seconds;
		}

		// Token: 0x04000682 RID: 1666
		internal float m_Seconds;
	}
}
