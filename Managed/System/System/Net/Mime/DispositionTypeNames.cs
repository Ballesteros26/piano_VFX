using System;

namespace System.Net.Mime
{
	/// <summary>Supplies the strings used to specify the disposition type for an e-mail attachment.</summary>
	// Token: 0x02000599 RID: 1433
	public static class DispositionTypeNames
	{
		/// <summary>Specifies that the attachment is to be displayed as part of the e-mail message body.</summary>
		// Token: 0x04002500 RID: 9472
		public const string Inline = "inline";

		/// <summary>Specifies that the attachment is to be displayed as a file attached to the e-mail message.</summary>
		// Token: 0x04002501 RID: 9473
		public const string Attachment = "attachment";
	}
}
