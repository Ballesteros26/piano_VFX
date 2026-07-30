using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Extends the <see cref="T:System.ComponentModel.IBindingList" /> interface by providing advanced sorting and filtering capabilities.</summary>
	// Token: 0x02000277 RID: 631
	public interface IBindingListView : IBindingList, IList, ICollection, IEnumerable
	{
		/// <summary>Sorts the data source based on the given <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />.</summary>
		/// <param name="sorts">The <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> containing the sorts to apply to the data source.</param>
		// Token: 0x06001437 RID: 5175
		void ApplySort(ListSortDescriptionCollection sorts);

		/// <summary>Gets or sets the filter to be used to exclude items from the collection of items returned by the data source</summary>
		/// <returns>The string used to filter items out in the item collection returned by the data source. </returns>
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001438 RID: 5176
		// (set) Token: 0x06001439 RID: 5177
		string Filter { get; set; }

		/// <summary>Gets the collection of sort descriptions currently applied to the data source.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> currently applied to the data source.</returns>
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600143A RID: 5178
		ListSortDescriptionCollection SortDescriptions { get; }

		/// <summary>Removes the current filter applied to the data source.</summary>
		// Token: 0x0600143B RID: 5179
		void RemoveFilter();

		/// <summary>Gets a value indicating whether the data source supports advanced sorting. </summary>
		/// <returns>true if the data source supports advanced sorting; otherwise, false. </returns>
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600143C RID: 5180
		bool SupportsAdvancedSorting { get; }

		/// <summary>Gets a value indicating whether the data source supports filtering. </summary>
		/// <returns>true if the data source supports filtering; otherwise, false. </returns>
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600143D RID: 5181
		bool SupportsFiltering { get; }
	}
}
