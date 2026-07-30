using System;
using System.ComponentModel;

namespace System.Drawing
{
	// Token: 0x02000083 RID: 131
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x00013BC3 File Offset: 0x00011DC3
		public SRDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00013BCC File Offset: 0x00011DCC
		public override string Description
		{
			get
			{
				if (!this.isReplaced)
				{
					this.isReplaced = true;
					base.DescriptionValue = Locale.GetText(base.DescriptionValue);
				}
				return base.DescriptionValue;
			}
		}

		// Token: 0x0400054D RID: 1357
		private bool isReplaced;
	}
}
