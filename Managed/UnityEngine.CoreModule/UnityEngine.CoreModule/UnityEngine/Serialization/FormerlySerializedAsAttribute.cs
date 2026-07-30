using System;
using UnityEngine.Scripting;

namespace UnityEngine.Serialization
{
	// Token: 0x02000265 RID: 613
	[AttributeUsage(256, AllowMultiple = true, Inherited = false)]
	[RequiredByNativeCode]
	public class FormerlySerializedAsAttribute : Attribute
	{
		// Token: 0x060019D4 RID: 6612 RVA: 0x0002A320 File Offset: 0x00028520
		public FormerlySerializedAsAttribute(string oldName)
		{
			this.m_oldName = oldName;
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x0002A334 File Offset: 0x00028534
		public string oldName
		{
			get
			{
				return this.m_oldName;
			}
		}

		// Token: 0x040007EE RID: 2030
		private string m_oldName;
	}
}
