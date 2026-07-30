using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.CacheVirtualItems" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006F RID: 111
	public class CacheVirtualItemsEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CacheVirtualItemsEventArgs" /> class with the specified starting and ending indices.</summary>
		/// <param name="startIndex">The starting index of a range of items needed by the <see cref="T:System.Windows.Forms.ListView" /> for the next <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event that occurs.</param>
		/// <param name="endIndex">The ending index of a range of items needed by the <see cref="T:System.Windows.Forms.ListView" /> for the next <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event that occurs.</param>
		// Token: 0x060004F8 RID: 1272 RVA: 0x000169F4 File Offset: 0x00014BF4
		public CacheVirtualItemsEventArgs(int startIndex, int endIndex)
		{
			this.start_index = startIndex;
			this.end_index = endIndex;
		}

		/// <summary>Gets the starting index for a range of values needed by a <see cref="T:System.Windows.Forms.ListView" /> control in virtual mode.</summary>
		/// <returns>The index at the start of the range of values needed by the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00016A0C File Offset: 0x00014C0C
		public int StartIndex
		{
			get
			{
				return this.start_index;
			}
		}

		/// <summary>Gets the ending index for the range of values needed by a <see cref="T:System.Windows.Forms.ListView" /> control in virtual mode.</summary>
		/// <returns>The index at the end of the range of values needed by the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00016A14 File Offset: 0x00014C14
		public int EndIndex
		{
			get
			{
				return this.end_index;
			}
		}

		// Token: 0x040006AB RID: 1707
		private int start_index;

		// Token: 0x040006AC RID: 1708
		private int end_index;
	}
}
