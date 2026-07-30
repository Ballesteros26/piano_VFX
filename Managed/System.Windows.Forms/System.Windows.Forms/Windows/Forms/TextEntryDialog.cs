using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000172 RID: 370
	internal partial class TextEntryDialog : Form
	{
		// Token: 0x060018AD RID: 6317 RVA: 0x0005CDAC File Offset: 0x0005AFAC
		public TextEntryDialog()
		{
			this.groupBox1 = new GroupBox();
			this.cancelButton = new Button();
			this.iconPictureBox = new PictureBox();
			this.newNameTextBox = new TextBox();
			this.okButton = new Button();
			this.label1 = new Label();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.newNameTextBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.iconPictureBox);
			this.groupBox1.Location = new Point(8, 8);
			this.groupBox1.Size = new Size(232, 160);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "New Name";
			this.cancelButton.DialogResult = DialogResult.Cancel;
			this.cancelButton.Location = new Point(168, 176);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.iconPictureBox.BorderStyle = BorderStyle.Fixed3D;
			this.iconPictureBox.Location = new Point(86, 24);
			this.iconPictureBox.Size = new Size(60, 60);
			this.iconPictureBox.TabIndex = 3;
			this.iconPictureBox.TabStop = false;
			this.iconPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
			this.newNameTextBox.Location = new Point(16, 128);
			this.newNameTextBox.Size = new Size(200, 20);
			this.newNameTextBox.TabIndex = 5;
			this.newNameTextBox.Text = string.Empty;
			this.okButton.DialogResult = DialogResult.OK;
			this.okButton.Location = new Point(80, 176);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "OK";
			this.label1.Location = new Point(16, 96);
			this.label1.Size = new Size(200, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Enter Name:";
			this.label1.TextAlign = 32;
			base.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new Size(5, 13);
			base.CancelButton = this.cancelButton;
			base.ClientSize = new Size(248, 205);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.okButton);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.Text = "New Folder or File";
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
			this.newNameTextBox.Select();
		}

		// Token: 0x170005EC RID: 1516
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x0005D0C0 File Offset: 0x0005B2C0
		public Image IconPictureBoxImage
		{
			set
			{
				this.iconPictureBox.Image = value;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0005D0D0 File Offset: 0x0005B2D0
		// (set) Token: 0x060018B0 RID: 6320 RVA: 0x0005D0E0 File Offset: 0x0005B2E0
		public string FileName
		{
			get
			{
				return this.newNameTextBox.Text;
			}
			set
			{
				this.newNameTextBox.Text = value;
			}
		}

		// Token: 0x04000DC4 RID: 3524
		private Label label1;

		// Token: 0x04000DC5 RID: 3525
		private Button okButton;

		// Token: 0x04000DC6 RID: 3526
		private TextBox newNameTextBox;

		// Token: 0x04000DC7 RID: 3527
		private PictureBox iconPictureBox;

		// Token: 0x04000DC8 RID: 3528
		private Button cancelButton;

		// Token: 0x04000DC9 RID: 3529
		private GroupBox groupBox1;
	}
}
