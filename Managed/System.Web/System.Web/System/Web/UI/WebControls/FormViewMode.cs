using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the different data-entry modes of a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	// Token: 0x020002B3 RID: 691
	public enum FormViewMode
	{
		/// <summary>A display mode that prevents the user from modifying the values of a record.</summary>
		// Token: 0x040016D7 RID: 5847
		ReadOnly,
		/// <summary>An editing mode that allows the user to update the values of an existing record.</summary>
		// Token: 0x040016D8 RID: 5848
		Edit,
		/// <summary>An inserting mode that allows the user to enter the values for a new record.</summary>
		// Token: 0x040016D9 RID: 5849
		Insert
	}
}
