using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the kind of action to take if a match is found when combining menu items on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000255 RID: 597
	public enum MergeAction
	{
		/// <summary>Appends the item to the end of the collection, ignoring match results.</summary>
		// Token: 0x04001389 RID: 5001
		Append,
		/// <summary>Inserts the item to the target's collection immediately preceding the matched item. A match of the end of the list results in the item being appended to the list. If there is no match or the match is at the beginning of the list, the item is inserted at the beginning of the collection.</summary>
		// Token: 0x0400138A RID: 5002
		Insert,
		/// <summary>Replaces the matched item with the source item. The original item's drop-down items do not become children of the incoming item.</summary>
		// Token: 0x0400138B RID: 5003
		Replace,
		/// <summary>Removes the matched item.</summary>
		// Token: 0x0400138C RID: 5004
		Remove,
		/// <summary>A match is required, but no action is taken. Use this for tree creation and successful access to nested layouts.</summary>
		// Token: 0x0400138D RID: 5005
		MatchOnly
	}
}
