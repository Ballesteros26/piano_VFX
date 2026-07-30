using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies how the content in a <see cref="T:System.Web.UI.WebControls.Literal" /> control is rendered.</summary>
	// Token: 0x020002DF RID: 735
	public enum LiteralMode
	{
		/// <summary>The literal control's unsupported markup-language elements are removed. If the literal control is rendered on a browser that supports HTML or XHTML, the control's contents are not modified.</summary>
		// Token: 0x04001710 RID: 5904
		Transform,
		/// <summary>The literal control's contents are not modified.</summary>
		// Token: 0x04001711 RID: 5905
		PassThrough,
		/// <summary>The literal control's contents are HTML-encoded.</summary>
		// Token: 0x04001712 RID: 5906
		Encode
	}
}
