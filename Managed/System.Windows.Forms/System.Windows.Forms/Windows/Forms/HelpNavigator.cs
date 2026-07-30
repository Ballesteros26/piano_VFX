using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants indicating which elements of the Help file to display.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B1 RID: 433
	public enum HelpNavigator
	{
		/// <summary>The Help file opens to a specified topic, if the topic exists.</summary>
		// Token: 0x04000F29 RID: 3881
		Topic = -2147483647,
		/// <summary>The Help file opens to the table of contents.</summary>
		// Token: 0x04000F2A RID: 3882
		TableOfContents,
		/// <summary>The Help file opens to the index.</summary>
		// Token: 0x04000F2B RID: 3883
		Index,
		/// <summary>The Help file opens to the search page.</summary>
		// Token: 0x04000F2C RID: 3884
		Find,
		/// <summary>The Help file opens to the index entry for the first letter of a specified topic.</summary>
		// Token: 0x04000F2D RID: 3885
		AssociateIndex,
		/// <summary>The Help file opens to the topic with the specified index entry, if one exists; otherwise, the index entry closest to the specified keyword is displayed.</summary>
		// Token: 0x04000F2E RID: 3886
		KeywordIndex,
		/// <summary>The Help file opens to a topic indicated by a numeric topic identifier.</summary>
		// Token: 0x04000F2F RID: 3887
		TopicId
	}
}
