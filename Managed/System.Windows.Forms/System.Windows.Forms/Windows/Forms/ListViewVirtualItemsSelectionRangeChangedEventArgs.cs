using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.VirtualItemsSelectionRangeChanged" /> event. </summary>
	// Token: 0x02000239 RID: 569
	public class ListViewVirtualItemsSelectionRangeChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventArgs" /> class. </summary>
		/// <param name="startIndex">The index of the first item in the range that has changed.</param>
		/// <param name="endIndex">The index of the last item in the range that has changed.</param>
		/// <param name="isSelected">true to indicate the items are selected; false to indicate the items are deselected.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> is larger than <paramref name="endIndex." /></exception>
		// Token: 0x06002532 RID: 9522 RVA: 0x0008CAE4 File Offset: 0x0008ACE4
		public ListViewVirtualItemsSelectionRangeChangedEventArgs(int startIndex, int endIndex, bool isSelected)
		{
			this.start_index = startIndex;
			this.end_index = endIndex;
			this.is_selected = isSelected;
		}

		/// <summary>Gets the index for the first item in the range of items whose selection state has changed.</summary>
		/// <returns>The index of the first item in the range of items whose selection state has changed.</returns>
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x0008CB04 File Offset: 0x0008AD04
		public int StartIndex
		{
			get
			{
				return this.start_index;
			}
		}

		/// <summary>Gets a value indicating whether the range of items is selected. </summary>
		/// <returns>true if the range of items is selected; false if the range of items is deselected.</returns>
		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x0008CB0C File Offset: 0x0008AD0C
		public bool IsSelected
		{
			get
			{
				return this.is_selected;
			}
		}

		/// <summary>Gets the index for the last item in the range of items whose selection state has changed</summary>
		/// <returns>The index of the last item in the range of items whose selection state has changed.</returns>
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x0008CB14 File Offset: 0x0008AD14
		public int EndIndex
		{
			get
			{
				return this.end_index;
			}
		}

		// Token: 0x040012E2 RID: 4834
		private bool is_selected;

		// Token: 0x040012E3 RID: 4835
		private int end_index;

		// Token: 0x040012E4 RID: 4836
		private int start_index;
	}
}
