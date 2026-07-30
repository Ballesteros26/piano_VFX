using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.Label" /> control.</summary>
	// Token: 0x020003BC RID: 956
	public class LabelControlBuilder : ControlBuilder
	{
		/// <summary>Specifies whether white space literals are allowed.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x0600276E RID: 10094 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
