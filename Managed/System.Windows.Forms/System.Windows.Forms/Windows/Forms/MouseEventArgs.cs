using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.MouseUp" />, <see cref="E:System.Windows.Forms.Control.MouseDown" />, and <see cref="E:System.Windows.Forms.Control.MouseMove" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000270 RID: 624
	[ComVisible(true)]
	public class MouseEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MouseEventArgs" /> class.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values indicating which mouse button was pressed. </param>
		/// <param name="clicks">The number of times a mouse button was pressed. </param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels. </param>
		/// <param name="y"></param>
		/// <param name="delta">A signed count of the number of detents the wheel has rotated. </param>
		// Token: 0x060028A8 RID: 10408 RVA: 0x0009DA4C File Offset: 0x0009BC4C
		public MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
		{
			this.buttons = button;
			this.clicks = clicks;
			this.delta = delta;
			this.x = x;
			this.y = y;
		}

		/// <summary>Gets which mouse button was pressed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x0009DA7C File Offset: 0x0009BC7C
		public MouseButtons Button
		{
			get
			{
				return this.buttons;
			}
		}

		/// <summary>Gets the number of times the mouse button was pressed and released.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of times the mouse button was pressed and released.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x0009DA84 File Offset: 0x0009BC84
		public int Clicks
		{
			get
			{
				return this.clicks;
			}
		}

		/// <summary>Gets a signed count of the number of detents the mouse wheel has rotated. A detent is one notch of the mouse wheel.</summary>
		/// <returns>A signed count of the number of detents the mouse wheel has rotated.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x0009DA8C File Offset: 0x0009BC8C
		public int Delta
		{
			get
			{
				return this.delta;
			}
		}

		/// <summary>Gets the x-coordinate of the mouse during the generating mouse event.</summary>
		/// <returns>The x-coordinate of the mouse, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x0009DA94 File Offset: 0x0009BC94
		public int X
		{
			get
			{
				return this.x;
			}
		}

		/// <summary>Gets the y-coordinate of the mouse during the generating mouse event.</summary>
		/// <returns>The y-coordinate of the mouse, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x0009DA9C File Offset: 0x0009BC9C
		public int Y
		{
			get
			{
				return this.y;
			}
		}

		/// <summary>Gets the location of the mouse during the generating mouse event.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> containing the x- and y- coordinate of the mouse, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x0009DAA4 File Offset: 0x0009BCA4
		public Point Location
		{
			get
			{
				return new Point(this.x, this.y);
			}
		}

		// Token: 0x0400145D RID: 5213
		private MouseButtons buttons;

		// Token: 0x0400145E RID: 5214
		private int clicks;

		// Token: 0x0400145F RID: 5215
		private int delta;

		// Token: 0x04001460 RID: 5216
		private int x;

		// Token: 0x04001461 RID: 5217
		private int y;
	}
}
