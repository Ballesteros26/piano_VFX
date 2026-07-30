using System;
using System.ComponentModel;

namespace System.Windows.Forms.PropertyGridInternal
{
	/// <summary>Defines methods and a property that allow filtering on specific attributes.</summary>
	// Token: 0x020001D2 RID: 466
	public interface IRootGridEntry
	{
		/// <summary>Gets or sets the attributes on which the property browser filters.</summary>
		/// <returns>The attributes on which the property browser filters.</returns>
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001E02 RID: 7682
		// (set) Token: 0x06001E03 RID: 7683
		AttributeCollection BrowsableAttributes { get; set; }

		/// <summary>Sorts the properties in the property browser.</summary>
		/// <param name="showCategories">true to group the properties by category; otherwise, false.</param>
		// Token: 0x06001E04 RID: 7684
		void ShowCategories(bool showCategories);

		/// <summary>Resets the <see cref="P:System.Windows.Forms.PropertyGridInternal.IRootGridEntry.BrowsableAttributes" /> property to the default value.</summary>
		// Token: 0x06001E05 RID: 7685
		void ResetBrowsableAttributes();
	}
}
