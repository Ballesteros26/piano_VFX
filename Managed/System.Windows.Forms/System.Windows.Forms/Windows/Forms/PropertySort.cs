using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies how properties are sorted in the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002AA RID: 682
	[ComVisible(true)]
	public enum PropertySort
	{
		/// <summary>Properties are displayed in the order in which they are retrieved from the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		// Token: 0x04001606 RID: 5638
		NoSort,
		/// <summary>Properties are sorted in an alphabetical list.</summary>
		// Token: 0x04001607 RID: 5639
		Alphabetical,
		/// <summary>Properties are displayed according to their category in a group. The categories are defined by the properties themselves.</summary>
		// Token: 0x04001608 RID: 5640
		Categorized,
		/// <summary>Properties are displayed according to their category in a group. The properties are further sorted alphabetically within the group. The categories are defined by the properties themselves.</summary>
		// Token: 0x04001609 RID: 5641
		CategorizedAlphabetical
	}
}
