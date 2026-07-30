using System;

namespace System.Web.UI
{
	// Token: 0x020001D6 RID: 470
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class HtmlControlPersistableAttribute : Attribute
	{
		// Token: 0x06001310 RID: 4880 RVA: 0x00033743 File Offset: 0x00031943
		public HtmlControlPersistableAttribute(bool persist)
		{
			this.persist = persist;
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x00033752 File Offset: 0x00031952
		public bool Persist
		{
			get
			{
				return this.persist;
			}
		}

		// Token: 0x04001441 RID: 5185
		private bool persist;
	}
}
