using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200019B RID: 411
	[UsedByNativeCode]
	[AttributeUsage(4, AllowMultiple = false)]
	public sealed class HelpURLAttribute : Attribute
	{
		// Token: 0x06001305 RID: 4869 RVA: 0x0001F3CF File Offset: 0x0001D5CF
		public HelpURLAttribute(string url)
		{
			this.m_Url = url;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x0001F3E0 File Offset: 0x0001D5E0
		public string URL
		{
			get
			{
				return this.m_Url;
			}
		}

		// Token: 0x04000643 RID: 1603
		internal readonly string m_Url;
	}
}
