using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the visibility a property has to the design-time serializer.</summary>
	// Token: 0x02000262 RID: 610
	[ComVisible(true)]
	public enum DesignerSerializationVisibility
	{
		/// <summary>The code generator does not produce code for the object.</summary>
		// Token: 0x040012C2 RID: 4802
		Hidden,
		/// <summary>The code generator produces code for the object.</summary>
		// Token: 0x040012C3 RID: 4803
		Visible,
		/// <summary>The code generator produces code for the contents of the object, rather than for the object itself.</summary>
		// Token: 0x040012C4 RID: 4804
		Content
	}
}
