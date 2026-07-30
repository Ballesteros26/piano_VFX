using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the horizontal alignment of items within a container.</summary>
	// Token: 0x020002CD RID: 717
	[TypeConverter(typeof(HorizontalAlignConverter))]
	public enum HorizontalAlign
	{
		/// <summary>The horizontal alignment is not set.</summary>
		// Token: 0x040016ED RID: 5869
		NotSet,
		/// <summary>The contents of a container are left justified.</summary>
		// Token: 0x040016EE RID: 5870
		Left,
		/// <summary>The contents of a container are centered.</summary>
		// Token: 0x040016EF RID: 5871
		Center,
		/// <summary>The contents of a container are right justified.</summary>
		// Token: 0x040016F0 RID: 5872
		Right,
		/// <summary>The contents of a container are uniformly spread out and aligned with both the left and right margins.</summary>
		// Token: 0x040016F1 RID: 5873
		Justify
	}
}
