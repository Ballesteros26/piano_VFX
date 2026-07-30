using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the MeasureItem event of the <see cref="T:System.Windows.Forms.ListBox" />, <see cref="T:System.Windows.Forms.ComboBox" />, <see cref="T:System.Windows.Forms.CheckedListBox" />, and <see cref="T:System.Windows.Forms.MenuItem" /> controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000248 RID: 584
	public class MeasureItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object being written to. </param>
		/// <param name="index">The index of the item for which you need the height or width. </param>
		// Token: 0x0600263D RID: 9789 RVA: 0x00091310 File Offset: 0x0008F510
		public MeasureItemEventArgs(Graphics graphics, int index)
		{
			this.graphics = graphics;
			this.index = index;
			this.itemHeight = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> class providing a parameter for the item height.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object being written to. </param>
		/// <param name="index">The index of the item for which you need the height or width. </param>
		/// <param name="itemHeight">The height of the item to measure relative to the <paramref name="graphics" /> object. </param>
		// Token: 0x0600263E RID: 9790 RVA: 0x00091330 File Offset: 0x0008F530
		public MeasureItemEventArgs(Graphics graphics, int index, int itemHeight)
		{
			this.graphics = graphics;
			this.index = index;
			this.itemHeight = itemHeight;
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> object to measure against.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> object to use to determine the scale of the item you are drawing.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x00091350 File Offset: 0x0008F550
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the index of the item for which the height and width is needed.</summary>
		/// <returns>The index of the item to be measured.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x00091358 File Offset: 0x0008F558
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets or sets the height of the item specified by the <see cref="P:System.Windows.Forms.MeasureItemEventArgs.Index" />.</summary>
		/// <returns>The height of the item measured.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x00091360 File Offset: 0x0008F560
		// (set) Token: 0x06002642 RID: 9794 RVA: 0x00091368 File Offset: 0x0008F568
		public int ItemHeight
		{
			get
			{
				return this.itemHeight;
			}
			set
			{
				this.itemHeight = value;
			}
		}

		/// <summary>Gets or sets the width of the item specified by the <see cref="P:System.Windows.Forms.MeasureItemEventArgs.Index" />.</summary>
		/// <returns>The width of the item measured.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x00091374 File Offset: 0x0008F574
		// (set) Token: 0x06002644 RID: 9796 RVA: 0x0009137C File Offset: 0x0008F57C
		public int ItemWidth
		{
			get
			{
				return this.itemWidth;
			}
			set
			{
				this.itemWidth = value;
			}
		}

		// Token: 0x0400132E RID: 4910
		private Graphics graphics;

		// Token: 0x0400132F RID: 4911
		private int index;

		// Token: 0x04001330 RID: 4912
		private int itemHeight;

		// Token: 0x04001331 RID: 4913
		private int itemWidth;
	}
}
