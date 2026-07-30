using System;

namespace System.Windows.Forms
{
	/// <summary>Allows a custom control to prevent the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event from being sent to its parent container.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001AE RID: 430
	public class HandledMouseEventArgs : MouseEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HandledMouseEventArgs" /> class with the specified mouse button, number of mouse button clicks, horizontal and vertical screen coordinates, and the change of mouse pointer position.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values indicating which mouse button was pressed. </param>
		/// <param name="clicks">The number of times a mouse button was pressed. </param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels. </param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
		/// <param name="delta">A signed count of the number of detents the wheel has rotated. </param>
		// Token: 0x06001C03 RID: 7171 RVA: 0x0006C160 File Offset: 0x0006A360
		public HandledMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
			: base(button, clicks, x, y, delta)
		{
			this.handled = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HandledMouseEventArgs" /> class with the specified mouse button, number of mouse button clicks, horizontal and vertical screen coordinates, the change of mouse pointer position, and the value indicating whether the event is handled.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values indicating which mouse button was pressed. </param>
		/// <param name="clicks">The number of times a mouse button was pressed. </param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels. </param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
		/// <param name="delta">A signed count of the number of detents the wheel has rotated. </param>
		/// <param name="defaultHandledValue">true if the event is handled; otherwise, false. </param>
		// Token: 0x06001C04 RID: 7172 RVA: 0x0006C178 File Offset: 0x0006A378
		public HandledMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta, bool defaultHandledValue)
			: base(button, clicks, x, y, delta)
		{
			this.handled = defaultHandledValue;
		}

		/// <summary>Gets or sets whether this event should be forwarded to the control's parent container.</summary>
		/// <returns>true if the mouse event should go to the parent control; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0006C190 File Offset: 0x0006A390
		// (set) Token: 0x06001C06 RID: 7174 RVA: 0x0006C198 File Offset: 0x0006A398
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		// Token: 0x04000F25 RID: 3877
		private bool handled;
	}
}
