using System;

namespace System.Web.Hosting
{
	// Token: 0x0200054B RID: 1355
	internal class RegisteredItem
	{
		// Token: 0x06003AB1 RID: 15025 RVA: 0x0009E406 File Offset: 0x0009C606
		public RegisteredItem(IRegisteredObject item, bool autoclean)
		{
			this.Item = item;
			this.AutoClean = autoclean;
		}

		// Token: 0x04001FDB RID: 8155
		public IRegisteredObject Item;

		// Token: 0x04001FDC RID: 8156
		public bool AutoClean;
	}
}
