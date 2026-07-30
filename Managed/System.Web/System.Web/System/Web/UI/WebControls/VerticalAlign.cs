using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the vertical alignment of an object or text in a control.</summary>
	// Token: 0x02000328 RID: 808
	[TypeConverter(typeof(VerticalAlignConverter))]
	public enum VerticalAlign
	{
		/// <summary>Vertical alignment is not set.</summary>
		// Token: 0x040017D4 RID: 6100
		NotSet,
		/// <summary>Text or object is aligned with the top of the enclosing control.</summary>
		// Token: 0x040017D5 RID: 6101
		Top,
		/// <summary>Text or object is aligned with the center of the enclosing control.</summary>
		// Token: 0x040017D6 RID: 6102
		Middle,
		/// <summary>Text or object is aligned with the bottom of the enclosing control.</summary>
		// Token: 0x040017D7 RID: 6103
		Bottom
	}
}
