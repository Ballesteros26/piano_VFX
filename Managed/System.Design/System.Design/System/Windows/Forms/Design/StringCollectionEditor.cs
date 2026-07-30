using System;
using System.ComponentModel.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200003B RID: 59
	internal class StringCollectionEditor : CollectionEditor
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00005128 File Offset: 0x00003328
		public StringCollectionEditor(Type type)
			: base(type)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000075D2 File Offset: 0x000057D2
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			return new StringCollectionEditor.StringCollectionEditForm(this);
		}

		// Token: 0x0200003C RID: 60
		private class StringCollectionEditForm : CollectionEditor.CollectionForm
		{
			// Token: 0x060001F5 RID: 501 RVA: 0x000075DA File Offset: 0x000057DA
			public StringCollectionEditForm(CollectionEditor editor)
				: base(editor)
			{
				this.InitializeComponent();
			}

			// Token: 0x060001F6 RID: 502 RVA: 0x000075EC File Offset: 0x000057EC
			private void InitializeComponent()
			{
				this.txtItems = new TextBox();
				this.label1 = new Label();
				this.butOk = new Button();
				this.butCancel = new Button();
				base.SuspendLayout();
				this.txtItems.Anchor = 15;
				this.txtItems.Location = new Point(12, 25);
				this.txtItems.Multiline = true;
				this.txtItems.AcceptsTab = true;
				this.txtItems.Name = "txtItems";
				this.txtItems.ScrollBars = 3;
				this.txtItems.Size = new Size(378, 168);
				this.txtItems.TabIndex = 1;
				this.label1.AutoSize = true;
				this.label1.Location = new Point(9, 9);
				this.label1.Name = "label1";
				this.label1.Size = new Size(227, 13);
				this.label1.TabIndex = 0;
				this.label1.Text = "&Enter the strings in the collection (one per line):";
				this.butOk.Anchor = 10;
				this.butOk.DialogResult = 1;
				this.butOk.Location = new Point(234, 199);
				this.butOk.Name = "butOk";
				this.butOk.Size = new Size(75, 23);
				this.butOk.TabIndex = 3;
				this.butOk.Text = "OK";
				this.butOk.Click += this.butOk_Click;
				this.butCancel.Anchor = 10;
				this.butCancel.DialogResult = 2;
				this.butCancel.Location = new Point(315, 199);
				this.butCancel.Name = "butCancel";
				this.butCancel.Size = new Size(75, 23);
				this.butCancel.TabIndex = 4;
				this.butCancel.Text = "Cancel";
				this.butCancel.Click += this.butCancel_Click;
				base.ClientSize = new Size(402, 228);
				base.Controls.Add(this.butCancel);
				base.Controls.Add(this.butOk);
				base.Controls.Add(this.label1);
				base.Controls.Add(this.txtItems);
				base.CancelButton = this.butCancel;
				base.MaximizeBox = false;
				base.MinimizeBox = false;
				base.Name = "StringEditorForm";
				this.Text = "String Collection Editor";
				base.ResumeLayout(false);
				base.PerformLayout();
			}

			// Token: 0x060001F7 RID: 503 RVA: 0x000078B4 File Offset: 0x00005AB4
			protected override void OnEditValueChanged()
			{
				object[] items = base.Items;
				string text = string.Empty;
				for (int i = 0; i < items.Length; i++)
				{
					if (items[i] is string)
					{
						text += (string)items[i];
						if (i != items.Length - 1)
						{
							text += Environment.NewLine;
						}
					}
				}
				this.txtItems.Text = text;
			}

			// Token: 0x060001F8 RID: 504 RVA: 0x00007918 File Offset: 0x00005B18
			private void butOk_Click(object sender, EventArgs e)
			{
				if (this.txtItems.Text == string.Empty)
				{
					base.Items = new string[0];
					return;
				}
				string[] lines = this.txtItems.Lines;
				object[] array = new object[(lines[lines.Length - 1].Trim().Length == 0) ? (lines.Length - 1) : lines.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = lines[i];
				}
				base.Items = array;
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x00007996 File Offset: 0x00005B96
			private void butCancel_Click(object sender, EventArgs e)
			{
				base.Close();
			}

			// Token: 0x040000E2 RID: 226
			private TextBox txtItems;

			// Token: 0x040000E3 RID: 227
			private Label label1;

			// Token: 0x040000E4 RID: 228
			private Button butOk;

			// Token: 0x040000E5 RID: 229
			private Button butCancel;
		}
	}
}
