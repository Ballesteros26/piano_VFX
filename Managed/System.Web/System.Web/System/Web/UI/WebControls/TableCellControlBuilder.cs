using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.TableCell" /> control.</summary>
	// Token: 0x0200041C RID: 1052
	public class TableCellControlBuilder : ControlBuilder
	{
		/// <summary>Specifies whether white space literals are allowed.</summary>
		/// <returns>false.</returns>
		// Token: 0x06002F8F RID: 12175 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
