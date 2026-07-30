using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the different data-entry modes for a data-bound control or a particular field in ASP.NET Dynamic Data.</summary>
	// Token: 0x0200028D RID: 653
	public enum DataBoundControlMode
	{
		/// <summary>Represents the display mode, which prevents the user from modifying the values of a record or a data field.</summary>
		// Token: 0x04001694 RID: 5780
		ReadOnly,
		/// <summary>Represents the edit mode, which enables users to update the values of an existing record or data field. </summary>
		// Token: 0x04001695 RID: 5781
		Edit,
		/// <summary>Represents the insert mode, which enables users to enter values for a new record or data field.</summary>
		// Token: 0x04001696 RID: 5782
		Insert
	}
}
