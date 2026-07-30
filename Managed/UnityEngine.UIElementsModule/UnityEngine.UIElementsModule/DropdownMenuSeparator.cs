using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000012 RID: 18
	public class DropdownMenuSeparator : DropdownMenuItem
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000031DB File Offset: 0x000013DB
		public string subMenuPath { get; }

		// Token: 0x0600005B RID: 91 RVA: 0x000031E3 File Offset: 0x000013E3
		public DropdownMenuSeparator(string subMenuPath)
		{
			this.subMenuPath = subMenuPath;
		}
	}
}
