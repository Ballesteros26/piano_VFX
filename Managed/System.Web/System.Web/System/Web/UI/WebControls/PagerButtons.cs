using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the types of buttons to display for navigating between pages of content in a paginated control.</summary>
	// Token: 0x020002FA RID: 762
	public enum PagerButtons
	{
		/// <summary>A set of pagination controls consisting of Previous and Next buttons.</summary>
		// Token: 0x0400173A RID: 5946
		NextPrevious,
		/// <summary>A set of pagination controls consisting of numbered link buttons to access pages directly.</summary>
		// Token: 0x0400173B RID: 5947
		Numeric,
		/// <summary>A set of pagination controls consisting of Previous, Next, First, and Last buttons.</summary>
		// Token: 0x0400173C RID: 5948
		NextPreviousFirstLast,
		/// <summary>A set of pagination controls consisting of numbered and First and Last link buttons.</summary>
		// Token: 0x0400173D RID: 5949
		NumericFirstLast
	}
}
