using System;
using System.ComponentModel;

namespace System
{
	// Token: 0x0200011F RID: 287
	[AttributeUsage(AttributeTargets.All)]
	internal class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x000269B3 File Offset: 0x00024BB3
		public SRDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000269BC File Offset: 0x00024BBC
		public override string Description
		{
			get
			{
				if (!this.isReplaced)
				{
					this.isReplaced = true;
					base.DescriptionValue = global::Locale.GetText(base.DescriptionValue);
				}
				return base.DescriptionValue;
			}
		}

		// Token: 0x04000D6C RID: 3436
		private bool isReplaced;
	}
}
