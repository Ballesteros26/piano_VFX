using System;

namespace UnityEngine
{
	// Token: 0x02000195 RID: 405
	public sealed class AddComponentMenu : Attribute
	{
		// Token: 0x060012F4 RID: 4852 RVA: 0x0001F2FE File Offset: 0x0001D4FE
		public AddComponentMenu(string menuName)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = 0;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0001F316 File Offset: 0x0001D516
		public AddComponentMenu(string menuName, int order)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = order;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0001F330 File Offset: 0x0001D530
		public string componentMenu
		{
			get
			{
				return this.m_AddComponentMenu;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x0001F348 File Offset: 0x0001D548
		public int componentOrder
		{
			get
			{
				return this.m_Ordering;
			}
		}

		// Token: 0x0400063B RID: 1595
		private string m_AddComponentMenu;

		// Token: 0x0400063C RID: 1596
		private int m_Ordering;
	}
}
