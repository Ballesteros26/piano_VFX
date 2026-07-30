using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200018F RID: 399
	[RequiredByNativeCode]
	[StructLayout(0)]
	public class ResourceRequest : AsyncOperation
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0001ED5C File Offset: 0x0001CF5C
		public Object asset
		{
			get
			{
				return Resources.Load(this.m_Path, this.m_Type);
			}
		}

		// Token: 0x04000634 RID: 1588
		internal string m_Path;

		// Token: 0x04000635 RID: 1589
		internal Type m_Type;
	}
}
