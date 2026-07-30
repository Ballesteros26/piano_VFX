using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides a property that is used by the <see cref="T:System.Web.DynamicData.DynamicDataManager" /> control to enable selecting data in a data-bound control through the query string.</summary>
	// Token: 0x020002D6 RID: 726
	public interface IPersistedSelector
	{
		/// <summary>Gets or sets the data-key value for the selected record in a data-bound control.</summary>
		/// <returns>The data-key value for the selected record in a data-bound control.</returns>
		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001B77 RID: 7031
		// (set) Token: 0x06001B78 RID: 7032
		DataKey DataKey { get; set; }
	}
}
