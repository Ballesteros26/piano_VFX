using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.TextBox" /> control.</summary>
	// Token: 0x0200042B RID: 1067
	public class TextBoxControlBuilder : ControlBuilder
	{
		/// <summary>Specifies whether white-space literals are allowed.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x0600303E RID: 12350 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		/// <summary>Determines whether the literal string of the <see cref="T:System.Web.UI.WebControls.TextBox" /> control must be HTML decoded.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x0600303F RID: 12351 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool HtmlDecodeLiterals()
		{
			return true;
		}
	}
}
