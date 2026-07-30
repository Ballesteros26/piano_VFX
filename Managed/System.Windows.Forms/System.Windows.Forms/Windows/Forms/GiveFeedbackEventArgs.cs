using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.GiveFeedback" /> event, which occurs during a drag operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A1 RID: 417
	[ComVisible(true)]
	public class GiveFeedbackEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> class.</summary>
		/// <param name="effect">The type of drag-and-drop operation. Possible values are obtained by applying the bitwise OR (|) operation to the constants defined in the <see cref="T:System.Windows.Forms.DragDropEffects" />. </param>
		/// <param name="useDefaultCursors">true if default pointers are used; otherwise, false. </param>
		// Token: 0x06001B0D RID: 6925 RVA: 0x00069774 File Offset: 0x00067974
		public GiveFeedbackEventArgs(DragDropEffects effect, bool useDefaultCursors)
		{
			this.effect = effect;
			this.use_default_cursors = useDefaultCursors;
		}

		/// <summary>Gets the drag-and-drop operation feedback that is displayed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x0006978C File Offset: 0x0006798C
		public DragDropEffects Effect
		{
			get
			{
				return this.effect;
			}
		}

		/// <summary>Gets or sets whether drag operation should use the default cursors that are associated with drag-drop effects.</summary>
		/// <returns>true if the default pointers are used; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x00069794 File Offset: 0x00067994
		// (set) Token: 0x06001B10 RID: 6928 RVA: 0x0006979C File Offset: 0x0006799C
		public bool UseDefaultCursors
		{
			get
			{
				return this.use_default_cursors;
			}
			set
			{
				this.use_default_cursors = value;
			}
		}

		// Token: 0x04000F06 RID: 3846
		internal DragDropEffects effect;

		// Token: 0x04000F07 RID: 3847
		internal bool use_default_cursors;
	}
}
