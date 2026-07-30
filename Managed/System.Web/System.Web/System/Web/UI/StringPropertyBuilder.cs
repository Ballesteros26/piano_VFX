using System;

namespace System.Web.UI
{
	// Token: 0x0200022C RID: 556
	internal class StringPropertyBuilder : ControlBuilder
	{
		// Token: 0x060016E9 RID: 5865 RVA: 0x0003D8C2 File Offset: 0x0003BAC2
		public StringPropertyBuilder(string prop_name)
		{
			this.prop_name = prop_name;
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0003D8D1 File Offset: 0x0003BAD1
		public string PropertyName
		{
			get
			{
				return this.prop_name;
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0003D8D9 File Offset: 0x0003BAD9
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
			throw new HttpException("StringPropertyBuilder should never be called");
		}

		// Token: 0x04001585 RID: 5509
		private string prop_name;
	}
}
