using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the selection behavior of a list box.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002D7 RID: 727
	[ComVisible(true)]
	public enum SelectionMode
	{
		/// <summary>No items can be selected.</summary>
		// Token: 0x040016FE RID: 5886
		None,
		/// <summary>Only one item can be selected.</summary>
		// Token: 0x040016FF RID: 5887
		One,
		/// <summary>Multiple items can be selected.</summary>
		// Token: 0x04001700 RID: 5888
		MultiSimple,
		/// <summary>Multiple items can be selected, and the user can use the SHIFT, CTRL, and arrow keys to make selections </summary>
		// Token: 0x04001701 RID: 5889
		MultiExtended
	}
}
