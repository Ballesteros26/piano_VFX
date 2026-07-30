using System;

namespace System.Web.UI
{
	/// <summary>Represents a control that renders text that can be changed by the user.</summary>
	// Token: 0x0200019D RID: 413
	public interface IEditableTextControl : ITextControl
	{
		/// <summary>Occurs when the content of the text changes between posts to the server. </summary>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000FD5 RID: 4053
		// (remove) Token: 0x06000FD6 RID: 4054
		event EventHandler TextChanged;
	}
}
