using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the HTML scope attribute for classes that represent header cells in a table. </summary>
	// Token: 0x02000319 RID: 793
	public enum TableHeaderScope
	{
		/// <summary>The scope attribute is not rendered for the header cell.</summary>
		// Token: 0x04001778 RID: 6008
		NotSet,
		/// <summary>The object that represents a header cell of a table is rendered with the scope attribute set to "Row".</summary>
		// Token: 0x04001779 RID: 6009
		Row,
		/// <summary>The object that represents a header cell of a table is rendered with the scope attribute set to "Column".</summary>
		// Token: 0x0400177A RID: 6010
		Column
	}
}
