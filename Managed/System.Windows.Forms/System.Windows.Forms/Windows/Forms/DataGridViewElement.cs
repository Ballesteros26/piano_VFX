using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides the base class for elements of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000112 RID: 274
	public class DataGridViewElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewElement" /> class.</summary>
		// Token: 0x06001411 RID: 5137 RVA: 0x0004C3AC File Offset: 0x0004A5AC
		public DataGridViewElement()
		{
			this.dataGridView = null;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridView" /> control associated with this element.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridView" /> control that contains this element. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x0004C3BC File Offset: 0x0004A5BC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public DataGridView DataGridView
		{
			get
			{
				return this.dataGridView;
			}
		}

		/// <summary>Gets the user interface (UI) state of the element.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values representing the state.</returns>
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x0004C3C4 File Offset: 0x0004A5C4
		[Browsable(false)]
		[EditorBrowsable(2)]
		public virtual DataGridViewElementStates State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Called when the element is associated with a different <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x06001414 RID: 5140 RVA: 0x0004C3CC File Offset: 0x0004A5CC
		protected virtual void OnDataGridViewChanged()
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellClick" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		// Token: 0x06001415 RID: 5141 RVA: 0x0004C3D0 File Offset: 0x0004A5D0
		protected void RaiseCellClick(DataGridViewCellEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.InternalOnCellClick(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContentClick" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		// Token: 0x06001416 RID: 5142 RVA: 0x0004C3EC File Offset: 0x0004A5EC
		protected void RaiseCellContentClick(DataGridViewCellEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.InternalOnCellContentClick(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContentDoubleClick" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		// Token: 0x06001417 RID: 5143 RVA: 0x0004C408 File Offset: 0x0004A608
		protected void RaiseCellContentDoubleClick(DataGridViewCellEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.InternalOnCellContentDoubleClick(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValueChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		// Token: 0x06001418 RID: 5144 RVA: 0x0004C424 File Offset: 0x0004A624
		protected void RaiseCellValueChanged(DataGridViewCellEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.InternalOnCellValueChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs" /> that contains the event data. </param>
		// Token: 0x06001419 RID: 5145 RVA: 0x0004C440 File Offset: 0x0004A640
		protected void RaiseDataError(DataGridViewDataErrorEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.InternalOnDataError(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x0600141A RID: 5146 RVA: 0x0004C45C File Offset: 0x0004A65C
		protected void RaiseMouseWheel(MouseEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.InternalOnMouseWheel(e);
			}
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0004C478 File Offset: 0x0004A678
		internal virtual void SetDataGridView(DataGridView dataGridView)
		{
			if (dataGridView != this.DataGridView)
			{
				this.dataGridView = dataGridView;
				this.OnDataGridViewChanged();
			}
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0004C494 File Offset: 0x0004A694
		internal virtual void SetState(DataGridViewElementStates state)
		{
			this.state = state;
		}

		// Token: 0x04000BAC RID: 2988
		private DataGridView dataGridView;

		// Token: 0x04000BAD RID: 2989
		private DataGridViewElementStates state;
	}
}
