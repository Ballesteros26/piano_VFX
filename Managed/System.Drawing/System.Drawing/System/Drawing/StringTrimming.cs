using System;

namespace System.Drawing
{
	/// <summary>Specifies how to trim characters from a string that does not completely fit into a layout shape.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000036 RID: 54
	public enum StringTrimming
	{
		/// <summary>Specifies no trimming.</summary>
		// Token: 0x040002AA RID: 682
		None,
		/// <summary>Specifies that the text is trimmed to the nearest character.</summary>
		// Token: 0x040002AB RID: 683
		Character,
		/// <summary>Specifies that text is trimmed to the nearest word.</summary>
		// Token: 0x040002AC RID: 684
		Word,
		/// <summary>Specifies that the text is trimmed to the nearest character, and an ellipsis is inserted at the end of a trimmed line.</summary>
		// Token: 0x040002AD RID: 685
		EllipsisCharacter,
		/// <summary>Specifies that text is trimmed to the nearest word, and an ellipsis is inserted at the end of a trimmed line.</summary>
		// Token: 0x040002AE RID: 686
		EllipsisWord,
		/// <summary>The center is removed from trimmed lines and replaced by an ellipsis. The algorithm keeps as much of the last slash-delimited segment of the line as possible.</summary>
		// Token: 0x040002AF RID: 687
		EllipsisPath
	}
}
