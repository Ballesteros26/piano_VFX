using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.QueryContinueDrag" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002AE RID: 686
	[ComVisible(true)]
	public class QueryContinueDragEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QueryContinueDragEventArgs" /> class.</summary>
		/// <param name="keyState">The current state of the SHIFT, CTRL, and ALT keys. </param>
		/// <param name="escapePressed">true if the ESC key was pressed; otherwise, false. </param>
		/// <param name="action">A <see cref="T:System.Windows.Forms.DragAction" /> value. </param>
		// Token: 0x06002DD4 RID: 11732 RVA: 0x000B11A8 File Offset: 0x000AF3A8
		public QueryContinueDragEventArgs(int keyState, bool escapePressed, DragAction action)
		{
			this.key_state = keyState;
			this.escape_pressed = escapePressed;
			this.drag_action = action;
		}

		/// <summary>Gets or sets the status of a drag-and-drop operation.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DragAction" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06002DD5 RID: 11733 RVA: 0x000B11C8 File Offset: 0x000AF3C8
		// (set) Token: 0x06002DD6 RID: 11734 RVA: 0x000B11D0 File Offset: 0x000AF3D0
		public DragAction Action
		{
			get
			{
				return this.drag_action;
			}
			set
			{
				this.drag_action = value;
			}
		}

		/// <summary>Gets whether the user pressed the ESC key.</summary>
		/// <returns>true if the ESC key was pressed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06002DD7 RID: 11735 RVA: 0x000B11DC File Offset: 0x000AF3DC
		public bool EscapePressed
		{
			get
			{
				return this.escape_pressed;
			}
		}

		/// <summary>Gets the current state of the SHIFT, CTRL, and ALT keys.</summary>
		/// <returns>The current state of the SHIFT, CTRL, and ALT keys.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x000B11E4 File Offset: 0x000AF3E4
		public int KeyState
		{
			get
			{
				return this.key_state;
			}
		}

		// Token: 0x04001611 RID: 5649
		internal int key_state;

		// Token: 0x04001612 RID: 5650
		internal bool escape_pressed;

		// Token: 0x04001613 RID: 5651
		internal DragAction drag_action;
	}
}
