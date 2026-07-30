using System;
using System.Collections;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Design.Behavior.BehaviorService.BeginDrag" /> and <see cref="E:System.Windows.Forms.Design.Behavior.BehaviorService.EndDrag" /> events.</summary>
	// Token: 0x02000043 RID: 67
	public class BehaviorDragDropEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorDragDropEventArgs" /> class.</summary>
		/// <param name="dragComponents">The <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.ComponentModel.IComponent" /> objects currently being dragged.</param>
		// Token: 0x0600023E RID: 574 RVA: 0x000088C5 File Offset: 0x00006AC5
		public BehaviorDragDropEventArgs(ICollection dragComponents)
		{
			this.components = dragComponents;
		}

		/// <summary>Gets the list of <see cref="T:System.ComponentModel.IComponent" /> objects currently being dragged.</summary>
		/// <returns>The <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.ComponentModel.IComponent" /> objects currently being dragged.</returns>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000088D4 File Offset: 0x00006AD4
		public ICollection DragComponents
		{
			get
			{
				return this.components;
			}
		}

		// Token: 0x040000F6 RID: 246
		private ICollection components;
	}
}
