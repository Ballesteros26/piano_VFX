using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for splitter events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002E4 RID: 740
	public class SplitterCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SplitterCancelEventArgs" /> class with the specified coordinates of the mouse pointer and the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
		/// <param name="mouseCursorX">The X coordinate of the mouse pointer in client coordinates. </param>
		/// <param name="mouseCursorY">The Y coordinate of the mouse pointer in client coordinates. </param>
		/// <param name="splitX">The X coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" /> in client coordinates. </param>
		/// <param name="splitY">The Y coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" /> in client coordinates. </param>
		// Token: 0x060030E6 RID: 12518 RVA: 0x000BD1CC File Offset: 0x000BB3CC
		public SplitterCancelEventArgs(int mouseCursorX, int mouseCursorY, int splitX, int splitY)
		{
			this.mouse_cursor_x = mouseCursorX;
			this.mouse_cursor_y = mouseCursorY;
			this.split_x = splitX;
			this.split_y = splitY;
		}

		/// <summary>Gets the X coordinate of the mouse pointer in client coordinates.</summary>
		/// <returns>An integer representing the X coordinate of the mouse pointer in client coordinates.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x060030E7 RID: 12519 RVA: 0x000BD1F4 File Offset: 0x000BB3F4
		public int MouseCursorX
		{
			get
			{
				return this.mouse_cursor_x;
			}
		}

		/// <summary>Gets the Y coordinate of the mouse pointer in client coordinates.</summary>
		/// <returns>An integer representing the Y coordinate of the mouse pointer in client coordinates.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x000BD1FC File Offset: 0x000BB3FC
		public int MouseCursorY
		{
			get
			{
				return this.mouse_cursor_y;
			}
		}

		/// <summary>Gets or sets the X coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" /> in client coordinates.</summary>
		/// <returns>An integer representing the X coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x060030E9 RID: 12521 RVA: 0x000BD204 File Offset: 0x000BB404
		// (set) Token: 0x060030EA RID: 12522 RVA: 0x000BD20C File Offset: 0x000BB40C
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

		/// <summary>Gets or sets the Y coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" /> in client coordinates.</summary>
		/// <returns>An integer representing the Y coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x060030EB RID: 12523 RVA: 0x000BD218 File Offset: 0x000BB418
		// (set) Token: 0x060030EC RID: 12524 RVA: 0x000BD220 File Offset: 0x000BB420
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

		// Token: 0x040017DD RID: 6109
		private int mouse_cursor_x;

		// Token: 0x040017DE RID: 6110
		private int mouse_cursor_y;

		// Token: 0x040017DF RID: 6111
		private int split_x;

		// Token: 0x040017E0 RID: 6112
		private int split_y;
	}
}
