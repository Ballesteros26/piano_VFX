using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.DragDrop" />, <see cref="E:System.Windows.Forms.Control.DragEnter" />, or <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000154 RID: 340
	[ComVisible(true)]
	public class DragEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DragEventArgs" /> class.</summary>
		/// <param name="data">The data associated with this event. </param>
		/// <param name="keyState">The current state of the SHIFT, CTRL, and ALT keys. </param>
		/// <param name="x">The x-coordinate of the mouse cursor in pixels. </param>
		/// <param name="y">The y-coordinate of the mouse cursor in pixels. </param>
		/// <param name="allowedEffect">One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values. </param>
		/// <param name="effect">One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values. </param>
		// Token: 0x0600173B RID: 5947 RVA: 0x00055ED0 File Offset: 0x000540D0
		public DragEventArgs(IDataObject data, int keyState, int x, int y, DragDropEffects allowedEffect, DragDropEffects effect)
		{
			this.x = x;
			this.y = y;
			this.keystate = keyState;
			this.allowed_effect = allowedEffect;
			this.current_effect = effect;
			this.data_object = data;
		}

		/// <summary>Gets which drag-and-drop operations are allowed by the originator (or source) of the drag event.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x00055F08 File Offset: 0x00054108
		public DragDropEffects AllowedEffect
		{
			get
			{
				return this.allowed_effect;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.IDataObject" /> that contains the data associated with this event.</summary>
		/// <returns>The data associated with this event.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00055F10 File Offset: 0x00054110
		public IDataObject Data
		{
			get
			{
				return this.data_object;
			}
		}

		/// <summary>Gets or sets the target drop effect in a drag-and-drop operation.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x00055F18 File Offset: 0x00054118
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x00055F20 File Offset: 0x00054120
		public DragDropEffects Effect
		{
			get
			{
				return this.current_effect;
			}
			set
			{
				this.current_effect = value;
			}
		}

		/// <summary>Gets the current state of the SHIFT, CTRL, and ALT keys, as well as the state of the mouse buttons.</summary>
		/// <returns>The current state of the SHIFT, CTRL, and ALT keys and of the mouse buttons.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x00055F2C File Offset: 0x0005412C
		public int KeyState
		{
			get
			{
				return this.keystate;
			}
		}

		/// <summary>Gets the x-coordinate of the mouse pointer, in screen coordinates.</summary>
		/// <returns>The x-coordinate of the mouse pointer in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x00055F34 File Offset: 0x00054134
		public int X
		{
			get
			{
				return this.x;
			}
		}

		/// <summary>Gets the y-coordinate of the mouse pointer, in screen coordinates.</summary>
		/// <returns>The y-coordinate of the mouse pointer in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00055F3C File Offset: 0x0005413C
		public int Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x04000CC1 RID: 3265
		internal int x;

		// Token: 0x04000CC2 RID: 3266
		internal int y;

		// Token: 0x04000CC3 RID: 3267
		internal int keystate;

		// Token: 0x04000CC4 RID: 3268
		internal DragDropEffects allowed_effect;

		// Token: 0x04000CC5 RID: 3269
		internal DragDropEffects current_effect;

		// Token: 0x04000CC6 RID: 3270
		internal IDataObject data_object;
	}
}
