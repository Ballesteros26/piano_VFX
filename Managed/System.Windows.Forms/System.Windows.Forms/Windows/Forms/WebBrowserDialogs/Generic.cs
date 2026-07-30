using System;
using System.Drawing;

namespace System.Windows.Forms.WebBrowserDialogs
{
	// Token: 0x02000631 RID: 1585
	internal partial class Generic : Form
	{
		// Token: 0x0600506F RID: 20591 RVA: 0x0013A1A8 File Offset: 0x001383A8
		public Generic(string title)
		{
			base.SuspendLayout();
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoSize = true;
			base.ControlBox = true;
			base.MinimizeBox = false;
			base.MaximizeBox = false;
			base.ShowInTaskbar = base.Owner == null;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.table = new TableLayoutPanel();
			this.table.SuspendLayout();
			this.table.AutoSize = true;
			base.Controls.Add(this.table);
			this.Text = title;
		}

		// Token: 0x06005070 RID: 20592 RVA: 0x0013A234 File Offset: 0x00138434
		public new DialogResult Show()
		{
			return this.RunDialog();
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x0013A23C File Offset: 0x0013843C
		private void InitSize()
		{
		}

		// Token: 0x06005072 RID: 20594 RVA: 0x0013A240 File Offset: 0x00138440
		protected void InitTable(int rows, int cols)
		{
			this.table.ColumnCount = cols;
			for (int i = 0; i < cols; i++)
			{
				this.table.ColumnStyles.Add(new ColumnStyle());
			}
			this.table.RowCount = rows;
			for (int j = 0; j < rows; j++)
			{
				this.table.RowStyles.Add(new RowStyle());
			}
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x0013A2B8 File Offset: 0x001384B8
		protected void AddLabel(int row, int col, int colspan, string text, int width, int height)
		{
			Label label = new Label();
			label.Text = text;
			if (width == -1 && height == -1)
			{
				label.AutoSize = true;
			}
			else
			{
				label.Width = width;
				label.Height = height;
			}
			this.table.Controls.Add(label, col, row);
			if (colspan > 1)
			{
				this.table.SetColumnSpan(label, colspan);
			}
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x0013A328 File Offset: 0x00138528
		protected void AddButton(int row, int col, int colspan, string text, int width, int height, bool isAccept, bool isCancel, EventHandler onClick)
		{
			Button button = new Button();
			button.Text = text;
			if (width != -1 || height != -1)
			{
				button.Width = width;
				button.Height = height;
			}
			if (onClick != null)
			{
				button.Click += onClick;
			}
			if (isAccept)
			{
				base.AcceptButton = button;
			}
			if (isCancel)
			{
				base.CancelButton = button;
			}
			this.table.Controls.Add(button, col, row);
			if (colspan > 1)
			{
				this.table.SetColumnSpan(button, colspan);
			}
		}

		// Token: 0x06005075 RID: 20597 RVA: 0x0013A3BC File Offset: 0x001385BC
		protected void AddCheck(int row, int col, int colspan, string text, bool check, int width, int height, EventHandler onCheck)
		{
			CheckBox checkBox = new CheckBox();
			checkBox.Text = text;
			checkBox.Checked = check;
			if (width == -1 && height == -1)
			{
				SizeF sizeF = TextRenderer.MeasureString(text, checkBox.Font);
				checkBox.Width += (int)(sizeF.Width / 62f);
				if (sizeF.Height > (float)checkBox.Height)
				{
					checkBox.Height = (int)sizeF.Height;
				}
			}
			else
			{
				checkBox.Width = width;
				checkBox.Height = height;
			}
			if (onCheck != null)
			{
				checkBox.CheckedChanged += onCheck;
			}
			this.table.Controls.Add(checkBox, col, row);
			if (colspan > 1)
			{
				this.table.SetColumnSpan(checkBox, colspan);
			}
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x0013A488 File Offset: 0x00138688
		protected void AddText(int row, int col, int colspan, string text, int width, int height, EventHandler onText)
		{
			TextBox textBox = new TextBox();
			textBox.Text = text;
			if (width > -1)
			{
				textBox.Width = width;
			}
			if (height > -1)
			{
				textBox.Height = height;
			}
			if (onText != null)
			{
				textBox.TextChanged += onText;
			}
			this.table.Controls.Add(textBox, col, row);
			if (colspan > 1)
			{
				this.table.SetColumnSpan(textBox, colspan);
			}
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x0013A4FC File Offset: 0x001386FC
		protected void AddPassword(int row, int col, int colspan, string text, int width, int height, EventHandler onText)
		{
			TextBox textBox = new TextBox();
			textBox.PasswordChar = '*';
			textBox.Text = text;
			if (width > -1)
			{
				textBox.Width = width;
			}
			if (height > -1)
			{
				textBox.Height = height;
			}
			if (onText != null)
			{
				textBox.TextChanged += onText;
			}
			this.table.Controls.Add(textBox, col, row);
			if (colspan > 1)
			{
				this.table.SetColumnSpan(textBox, colspan);
			}
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x0013A578 File Offset: 0x00138778
		protected DialogResult RunDialog()
		{
			base.StartPosition = FormStartPosition.CenterScreen;
			this.InitSize();
			this.table.ResumeLayout(false);
			this.table.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
			base.ShowDialog();
			return base.DialogResult;
		}

		// Token: 0x04002D60 RID: 11616
		private TableLayoutPanel table;
	}
}
