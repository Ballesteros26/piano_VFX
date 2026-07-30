using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.PlaceHolder" /> control.</summary>
	// Token: 0x020003F4 RID: 1012
	public class PlaceHolderControlBuilder : ControlBuilder
	{
		/// <summary>Specifies whether white-space literals are allowed.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x06002CB8 RID: 11448 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
