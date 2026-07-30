using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemDrag" /> event of the <see cref="T:System.Windows.Forms.ListView" /> and <see cref="T:System.Windows.Forms.TreeView" /> controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F2 RID: 498
	[ComVisible(true)]
	public class ItemDragEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ItemDragEventArgs" /> class with a specified mouse button.</summary>
		/// <param name="button">A bitwise combination of <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicates which mouse buttons were pressed. </param>
		// Token: 0x06001EFF RID: 7935 RVA: 0x00074ED8 File Offset: 0x000730D8
		public ItemDragEventArgs(MouseButtons button)
		{
			this.button = button;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ItemDragEventArgs" /> class with a specified mouse button and the item that is being dragged.</summary>
		/// <param name="button">A bitwise combination of <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicates which mouse buttons were pressed. </param>
		/// <param name="item">The item being dragged. </param>
		// Token: 0x06001F00 RID: 7936 RVA: 0x00074EE8 File Offset: 0x000730E8
		public ItemDragEventArgs(MouseButtons button, object item)
		{
			this.button = button;
			this.item = item;
		}

		/// <summary>Gets a value that indicates which mouse buttons were pressed during the drag operation.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.MouseButtons" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x00074F00 File Offset: 0x00073100
		public MouseButtons Button
		{
			get
			{
				return this.button;
			}
		}

		/// <summary>Gets the item that is being dragged.</summary>
		/// <returns>An object that represents the item being dragged.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x00074F08 File Offset: 0x00073108
		public object Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x0400104A RID: 4170
		private MouseButtons button;

		// Token: 0x0400104B RID: 4171
		private object item;
	}
}
