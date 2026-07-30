using System;

namespace System.Windows.Forms
{
	/// <summary>Defines mouse events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CD RID: 461
	public interface IDropTarget
	{
		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragDrop" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DF8 RID: 7672
		void OnDragDrop(DragEventArgs e);

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragEnter" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DF9 RID: 7673
		void OnDragEnter(DragEventArgs e);

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DFA RID: 7674
		void OnDragLeave(EventArgs e);

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DFB RID: 7675
		void OnDragOver(DragEventArgs e);
	}
}
