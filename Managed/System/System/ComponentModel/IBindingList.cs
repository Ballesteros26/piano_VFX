using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides the features required to support both complex and simple scenarios when binding to a data source.</summary>
	// Token: 0x02000276 RID: 630
	public interface IBindingList : IList, ICollection, IEnumerable
	{
		/// <summary>Gets whether you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>true if you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />; otherwise, false.</returns>
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001426 RID: 5158
		bool AllowNew { get; }

		/// <summary>Adds a new item to the list.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.AllowNew" /> is false. </exception>
		// Token: 0x06001427 RID: 5159
		object AddNew();

		/// <summary>Gets whether you can update items in the list.</summary>
		/// <returns>true if you can update the items in the list; otherwise, false.</returns>
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001428 RID: 5160
		bool AllowEdit { get; }

		/// <summary>Gets whether you can remove items from the list, using <see cref="M:System.Collections.IList.Remove(System.Object)" /> or <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <returns>true if you can remove items from the list; otherwise, false.</returns>
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001429 RID: 5161
		bool AllowRemove { get; }

		/// <summary>Gets whether a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or an item in the list changes.</summary>
		/// <returns>true if a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or when an item changes; otherwise, false.</returns>
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600142A RID: 5162
		bool SupportsChangeNotification { get; }

		/// <summary>Gets whether the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method.</summary>
		/// <returns>true if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method; otherwise, false.</returns>
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600142B RID: 5163
		bool SupportsSearching { get; }

		/// <summary>Gets whether the list supports sorting.</summary>
		/// <returns>true if the list supports sorting; otherwise, false.</returns>
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600142C RID: 5164
		bool SupportsSorting { get; }

		/// <summary>Gets whether the items in the list are sorted.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" /> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /> has not been called; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600142D RID: 5165
		bool IsSorted { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600142E RID: 5166
		PropertyDescriptor SortProperty { get; }

		/// <summary>Gets the direction of the sort.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600142F RID: 5167
		ListSortDirection SortDirection { get; }

		/// <summary>Occurs when the list changes or an item in the list changes.</summary>
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001430 RID: 5168
		// (remove) Token: 0x06001431 RID: 5169
		event ListChangedEventHandler ListChanged;

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the indexes used for searching. </param>
		// Token: 0x06001432 RID: 5170
		void AddIndex(PropertyDescriptor property);

		/// <summary>Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by. </param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x06001433 RID: 5171
		void ApplySort(PropertyDescriptor property, ListSortDirection direction);

		/// <summary>Returns the index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on. </param>
		/// <param name="key">The value of the <paramref name="property" /> parameter to search for. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" /> is false. </exception>
		// Token: 0x06001434 RID: 5172
		int Find(PropertyDescriptor property, object key);

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching. </param>
		// Token: 0x06001435 RID: 5173
		void RemoveIndex(PropertyDescriptor property);

		/// <summary>Removes any sort applied using <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x06001436 RID: 5174
		void RemoveSort();
	}
}
