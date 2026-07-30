using System;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x020001C0 RID: 448
	internal sealed class DataBindingBuilder : CodeBuilder
	{
		// Token: 0x0600122F RID: 4655 RVA: 0x0003261F File Offset: 0x0003081F
		public DataBindingBuilder(string code, ILocation location)
			: base(code, false, location)
		{
			base.SetControlType(typeof(DataBoundLiteralControl));
		}
	}
}
