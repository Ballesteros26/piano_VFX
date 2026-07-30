using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the layout of items in a list control.</summary>
	// Token: 0x020002FF RID: 767
	public enum RepeatLayout
	{
		/// <summary>Items are displayed in a table.</summary>
		// Token: 0x0400174C RID: 5964
		Table,
		/// <summary>Items are displayed without a table structure. Rendered markup consists of a span element and items are separated by br elements.</summary>
		// Token: 0x0400174D RID: 5965
		Flow,
		/// <summary>Items are displayed without a table structure. Rendered markup consists of a ul element that contains li elements.</summary>
		// Token: 0x0400174E RID: 5966
		UnorderedList,
		/// <summary>Items are displayed without a table structure. Rendered markup consists of an ol element that contains li elements.</summary>
		// Token: 0x0400174F RID: 5967
		OrderedList
	}
}
