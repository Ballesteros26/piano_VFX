using System;

namespace System.Web
{
	// Token: 0x02000072 RID: 114
	internal sealed class ExceptionPageTemplateFragmentValue
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x000092F8 File Offset: 0x000074F8
		public string Value
		{
			get
			{
				if (this.valueProvider != null)
				{
					return this.valueProvider(this.name);
				}
				return this.value;
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000931A File Offset: 0x0000751A
		public ExceptionPageTemplateFragmentValue(string name, Func<string, string> valueProvider)
		{
			this.valueProvider = valueProvider;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00009329 File Offset: 0x00007529
		public ExceptionPageTemplateFragmentValue(string name, string value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x04000E85 RID: 3717
		private Func<string, string> valueProvider;

		// Token: 0x04000E86 RID: 3718
		private string value;

		// Token: 0x04000E87 RID: 3719
		private string name;
	}
}
