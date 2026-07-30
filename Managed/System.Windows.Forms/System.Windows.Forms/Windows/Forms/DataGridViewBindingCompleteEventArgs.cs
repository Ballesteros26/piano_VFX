using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.DataBindingComplete" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000DF RID: 223
	public class DataGridViewBindingCompleteEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs" /> class.</summary>
		/// <param name="listChangedType">One of the <see cref="T:System.ComponentModel.ListChangedType" /> values.</param>
		// Token: 0x0600114F RID: 4431 RVA: 0x00045220 File Offset: 0x00043420
		public DataGridViewBindingCompleteEventArgs(ListChangedType listChangedType)
		{
			this.listChangedType = listChangedType;
		}

		/// <summary>Gets a value specifying how the list changed.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListChangedType" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x00045230 File Offset: 0x00043430
		public ListChangedType ListChangedType
		{
			get
			{
				return this.listChangedType;
			}
		}

		// Token: 0x04000AD3 RID: 2771
		private ListChangedType listChangedType;
	}
}
