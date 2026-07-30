using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies the visual state of a text box that is drawn with visual styles.</summary>
	// Token: 0x0200052B RID: 1323
	public enum TextBoxState
	{
		/// <summary>The text box appears normal.</summary>
		// Token: 0x04002BDC RID: 11228
		Normal = 1,
		/// <summary>The text box appears hot.</summary>
		// Token: 0x04002BDD RID: 11229
		Hot,
		/// <summary>The text box appears selected.</summary>
		// Token: 0x04002BDE RID: 11230
		Selected,
		/// <summary>The text box appears disabled.</summary>
		// Token: 0x04002BDF RID: 11231
		Disabled,
		/// <summary>The text box appears read-only.</summary>
		// Token: 0x04002BE0 RID: 11232
		Readonly = 6,
		/// <summary>The text box appears in assist mode.</summary>
		// Token: 0x04002BE1 RID: 11233
		Assist
	}
}
