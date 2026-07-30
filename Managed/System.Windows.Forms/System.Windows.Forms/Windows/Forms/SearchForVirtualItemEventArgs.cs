using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.SearchForVirtualItem" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002D4 RID: 724
	public class SearchForVirtualItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SearchForVirtualItemEventArgs" /> class. </summary>
		/// <param name="isTextSearch">A value indicating whether the search is a text search.</param>
		/// <param name="isPrefixSearch">A value indicating whether the search is a prefix search.</param>
		/// <param name="includeSubItemsInSearch">A value indicating whether to include subitems of list items in the search.</param>
		/// <param name="text">The text of the item to search for.</param>
		/// <param name="startingPoint">The <see cref="T:System.Drawing.Point" /> at which to start the search.</param>
		/// <param name="direction">One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</param>
		/// <param name="startIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> at which to start the search.</param>
		// Token: 0x06002FED RID: 12269 RVA: 0x000B9888 File Offset: 0x000B7A88
		public SearchForVirtualItemEventArgs(bool isTextSearch, bool isPrefixSearch, bool includeSubItemsInSearch, string text, Point startingPoint, SearchDirectionHint direction, int startIndex)
		{
			this.is_text_search = isTextSearch;
			this.is_prefix_search = isPrefixSearch;
			this.include_sub_items_in_search = includeSubItemsInSearch;
			this.text = text;
			this.starting_point = startingPoint;
			this.direction = direction;
			this.start_index = startIndex;
			this.index = -1;
		}

		/// <summary>Gets the direction from the current item that the search should take place.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x000B98D8 File Offset: 0x000B7AD8
		public SearchDirectionHint Direction
		{
			get
			{
				return this.direction;
			}
		}

		/// <summary>Gets a value indicating whether the search should include subitems of list items.</summary>
		/// <returns>true if subitems should be included in the search; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06002FEF RID: 12271 RVA: 0x000B98E0 File Offset: 0x000B7AE0
		public bool IncludeSubItemsInSearch
		{
			get
			{
				return this.include_sub_items_in_search;
			}
		}

		/// <summary>Gets or sets the index of the <see cref="T:System.Windows.Forms.ListViewItem" /> found in the <see cref="T:System.Windows.Forms.ListView" /> .</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> found in the <see cref="T:System.Windows.Forms.ListView" />.</returns>
		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06002FF0 RID: 12272 RVA: 0x000B98E8 File Offset: 0x000B7AE8
		// (set) Token: 0x06002FF1 RID: 12273 RVA: 0x000B98F0 File Offset: 0x000B7AF0
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		/// <summary>Gets a value indicating whether the search should return an item if its text starts with the search text.</summary>
		/// <returns>true if the search should match item text that starts with the search text; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06002FF2 RID: 12274 RVA: 0x000B98FC File Offset: 0x000B7AFC
		public bool IsPrefixSearch
		{
			get
			{
				return this.is_prefix_search;
			}
		}

		/// <summary>Gets a value indicating whether the search is a text search.</summary>
		/// <returns>true if the search is a text search; false if the search is a location search.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06002FF3 RID: 12275 RVA: 0x000B9904 File Offset: 0x000B7B04
		public bool IsTextSearch
		{
			get
			{
				return this.is_text_search;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Windows.Forms.ListViewItem" /> where the search starts.</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> indicating where the search starts</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06002FF4 RID: 12276 RVA: 0x000B990C File Offset: 0x000B7B0C
		public int StartIndex
		{
			get
			{
				return this.start_index;
			}
		}

		/// <summary>Gets the starting location of the search.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that indicates the starting location of the search.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06002FF5 RID: 12277 RVA: 0x000B9914 File Offset: 0x000B7B14
		public Point StartingPoint
		{
			get
			{
				return this.starting_point;
			}
		}

		/// <summary>Gets the text used to find an item in the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>The text used to find an item in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06002FF6 RID: 12278 RVA: 0x000B991C File Offset: 0x000B7B1C
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x040016E9 RID: 5865
		private SearchDirectionHint direction;

		// Token: 0x040016EA RID: 5866
		private bool include_sub_items_in_search;

		// Token: 0x040016EB RID: 5867
		private int index;

		// Token: 0x040016EC RID: 5868
		private bool is_prefix_search;

		// Token: 0x040016ED RID: 5869
		private bool is_text_search;

		// Token: 0x040016EE RID: 5870
		private int start_index;

		// Token: 0x040016EF RID: 5871
		private Point starting_point;

		// Token: 0x040016F0 RID: 5872
		private string text;
	}
}
