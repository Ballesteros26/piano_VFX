using System;

namespace System.Web.UI
{
	/// <summary>Defines the interface a control implements to get or set its text content.</summary>
	// Token: 0x0200019F RID: 415
	public interface ITextControl
	{
		/// <summary>Gets or sets the text content of a control.</summary>
		/// <returns>The text content of a control.</returns>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000FDA RID: 4058
		// (set) Token: 0x06000FDB RID: 4059
		string Text { get; set; }
	}
}
