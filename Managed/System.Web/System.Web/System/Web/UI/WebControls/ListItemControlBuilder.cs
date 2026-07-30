using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.ListItem" /> control.</summary>
	// Token: 0x020003C3 RID: 963
	public class ListItemControlBuilder : ControlBuilder
	{
		/// <summary>Determines whether white spaces in the text associated with the <see cref="T:System.Web.UI.WebControls.ListItem" /> are represented by <see cref="T:System.Web.UI.LiteralControl" /> objects.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x06002817 RID: 10263 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		/// <summary>Determines whether HTML entities in the text associated with the <see cref="T:System.Web.UI.WebControls.ListItem" /> are converted to their equivalent characters when the text is parsed.</summary>
		/// <returns>true for all cases.</returns>
		// Token: 0x06002818 RID: 10264 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool HtmlDecodeLiterals()
		{
			return true;
		}
	}
}
