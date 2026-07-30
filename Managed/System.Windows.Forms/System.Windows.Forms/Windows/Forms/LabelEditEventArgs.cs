using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.BeforeLabelEdit" /> and <see cref="E:System.Windows.Forms.ListView.AfterLabelEdit" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FD RID: 509
	public class LabelEditEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LabelEditEventArgs" /> class with the specified index to the <see cref="T:System.Windows.Forms.ListViewItem" /> to edit.</summary>
		/// <param name="item">The zero-based index of the <see cref="T:System.Windows.Forms.ListViewItem" />, containing the label to edit. </param>
		// Token: 0x06001F7C RID: 8060 RVA: 0x00076170 File Offset: 0x00074370
		public LabelEditEventArgs(int item)
		{
			this.item = item;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LabelEditEventArgs" /> class with the specified index to the <see cref="T:System.Windows.Forms.ListViewItem" /> being edited and the new text for the label of the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <param name="item">The zero-based index of the <see cref="T:System.Windows.Forms.ListViewItem" />, containing the label to edit. </param>
		/// <param name="label">The new text assigned to the label of the <see cref="T:System.Windows.Forms.ListViewItem" />. </param>
		// Token: 0x06001F7D RID: 8061 RVA: 0x00076180 File Offset: 0x00074380
		public LabelEditEventArgs(int item, string label)
		{
			this.item = item;
			this.label = label;
		}

		/// <summary>Gets or sets a value indicating whether changes made to the label of the <see cref="T:System.Windows.Forms.ListViewItem" /> should be canceled.</summary>
		/// <returns>true if the edit operation of the label for the <see cref="T:System.Windows.Forms.ListViewItem" /> should be canceled; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x00076198 File Offset: 0x00074398
		// (set) Token: 0x06001F7F RID: 8063 RVA: 0x000761A0 File Offset: 0x000743A0
		public bool CancelEdit
		{
			get
			{
				return this.cancelEdit;
			}
			set
			{
				this.cancelEdit = value;
			}
		}

		/// <summary>Gets the zero-based index of the <see cref="T:System.Windows.Forms.ListViewItem" /> containing the label to edit.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x000761AC File Offset: 0x000743AC
		public int Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the new text assigned to the label of the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>The new text to associate with the <see cref="T:System.Windows.Forms.ListViewItem" /> or null if the text is unchanged. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x000761B4 File Offset: 0x000743B4
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x000761BC File Offset: 0x000743BC
		internal void SetLabel(string label)
		{
			this.label = label;
		}

		// Token: 0x0400113B RID: 4411
		private int item;

		// Token: 0x0400113C RID: 4412
		private string label;

		// Token: 0x0400113D RID: 4413
		private bool cancelEdit;
	}
}
