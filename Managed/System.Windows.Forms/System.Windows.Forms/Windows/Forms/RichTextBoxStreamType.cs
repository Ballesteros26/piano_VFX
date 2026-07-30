using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the types of input and output streams used to load and save data in the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002BE RID: 702
	public enum RichTextBoxStreamType
	{
		/// <summary>A Rich Text Format (RTF) stream.</summary>
		// Token: 0x04001666 RID: 5734
		RichText,
		/// <summary>A plain text stream that includes spaces in places of Object Linking and Embedding (OLE) objects.</summary>
		// Token: 0x04001667 RID: 5735
		PlainText,
		/// <summary>A Rich Text Format (RTF) stream with spaces in place of OLE objects. This value is only valid for use with the <see cref="M:System.Windows.Forms.RichTextBox.SaveFile(System.String)" /> method of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		// Token: 0x04001668 RID: 5736
		RichNoOleObjs,
		/// <summary>A plain text stream with a textual representation of OLE objects. This value is only valid for use with the <see cref="M:System.Windows.Forms.RichTextBox.SaveFile(System.String)" /> method of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		// Token: 0x04001669 RID: 5737
		TextTextOleObjs,
		/// <summary>A text stream that contains spaces in place of Object Linking and Embedding (OLE) objects. The text is encoded in Unicode.</summary>
		// Token: 0x0400166A RID: 5738
		UnicodePlainText
	}
}
