using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.HelpRequested" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B0 RID: 432
	[ComVisible(true)]
	public class HelpEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HelpEventArgs" /> class.</summary>
		/// <param name="mousePos">The coordinates of the mouse pointer. </param>
		// Token: 0x06001C0E RID: 7182 RVA: 0x0006C22C File Offset: 0x0006A42C
		public HelpEventArgs(Point mousePos)
		{
			this.mouse_position = mousePos;
			this.event_handled = false;
		}

		/// <summary>Gets or sets a value indicating whether the help event was handled.</summary>
		/// <returns>true if the event is handled; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x0006C244 File Offset: 0x0006A444
		// (set) Token: 0x06001C10 RID: 7184 RVA: 0x0006C24C File Offset: 0x0006A44C
		public bool Handled
		{
			get
			{
				return this.event_handled;
			}
			set
			{
				this.event_handled = value;
			}
		}

		/// <summary>Gets the screen coordinates of the mouse pointer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> representing the screen coordinates of the mouse pointer.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x0006C258 File Offset: 0x0006A458
		public Point MousePos
		{
			get
			{
				return this.mouse_position;
			}
		}

		// Token: 0x04000F26 RID: 3878
		private Point mouse_position;

		// Token: 0x04000F27 RID: 3879
		private bool event_handled;
	}
}
