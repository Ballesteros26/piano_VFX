using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the different data-entry modes of a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
	// Token: 0x020002A5 RID: 677
	public enum DetailsViewMode
	{
		/// <summary>A display mode that prevents the user from modifying the values of a record.</summary>
		// Token: 0x040016BB RID: 5819
		ReadOnly,
		/// <summary>An editing mode that allows the user to update the values of an existing record.</summary>
		// Token: 0x040016BC RID: 5820
		Edit,
		/// <summary>An inserting mode that allows the user to enter the values for a new record.</summary>
		// Token: 0x040016BD RID: 5821
		Insert
	}
}
