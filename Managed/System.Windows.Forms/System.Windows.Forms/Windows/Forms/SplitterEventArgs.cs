using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for <see cref="E:System.Windows.Forms.Splitter.SplitterMoving" /> and the <see cref="E:System.Windows.Forms.Splitter.SplitterMoved" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002E5 RID: 741
	[ComVisible(true)]
	public class SplitterEventArgs : EventArgs
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.SplitterEventArgs" /> class with the specified coordinates of the mouse pointer and the coordinates of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> control.</summary>
		/// <param name="x">The x-coordinate of the mouse pointer (in client coordinates). </param>
		/// <param name="y">The y-coordinate of the mouse pointer (in client coordinates). </param>
		/// <param name="splitX">The x-coordinate of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> (in client coordinates). </param>
		/// <param name="splitY">The y-coordinate of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> (in client coordinates). </param>
		// Token: 0x060030ED RID: 12525 RVA: 0x000BD22C File Offset: 0x000BB42C
		public SplitterEventArgs(int x, int y, int splitX, int splitY)
		{
			this.x = x;
			this.y = y;
			this.SplitX = splitX;
			this.SplitY = splitY;
		}

		/// <summary>Gets or sets the x-coordinate of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> (in client coordinates).</summary>
		/// <returns>The x-coordinate of the upper-left corner of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x060030EE RID: 12526 RVA: 0x000BD254 File Offset: 0x000BB454
		// (set) Token: 0x060030EF RID: 12527 RVA: 0x000BD25C File Offset: 0x000BB45C
		public int SplitX
		{
			get
			{
				return this.split_x;
			}
			set
			{
				this.split_x = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of the upper-left corner of the <see cref="T:System.Windows.Forms.Splitter" /> (in client coordinates).</summary>
		/// <returns>The y-coordinate of the upper-left corner of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x000BD268 File Offset: 0x000BB468
		// (set) Token: 0x060030F1 RID: 12529 RVA: 0x000BD270 File Offset: 0x000BB470
		public int SplitY
		{
			get
			{
				return this.split_y;
			}
			set
			{
				this.split_y = value;
			}
		}

		/// <summary>Gets the x-coordinate of the mouse pointer (in client coordinates).</summary>
		/// <returns>The x-coordinate of the mouse pointer.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x000BD27C File Offset: 0x000BB47C
		public int X
		{
			get
			{
				return this.x;
			}
		}

		/// <summary>Gets the y-coordinate of the mouse pointer (in client coordinates).</summary>
		/// <returns>The y-coordinate of the mouse pointer.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x060030F3 RID: 12531 RVA: 0x000BD284 File Offset: 0x000BB484
		public int Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x040017E1 RID: 6113
		internal int split_x;

		// Token: 0x040017E2 RID: 6114
		internal int split_y;

		// Token: 0x040017E3 RID: 6115
		internal int x;

		// Token: 0x040017E4 RID: 6116
		internal int y;
	}
}
