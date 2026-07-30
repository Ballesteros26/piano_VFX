namespace Ookii.Dialogs
{
	// Token: 0x0200000F RID: 15
	internal partial class InputDialogForm : global::Ookii.Dialogs.ExtendedForm
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00004858 File Offset: 0x00002A58
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004890 File Offset: 0x00002A90
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Ookii.Dialogs.InputDialogForm));
			this._primaryPanel = new global::System.Windows.Forms.Panel();
			this._inputTextBox = new global::System.Windows.Forms.TextBox();
			this._secondaryPanel = new global::System.Windows.Forms.Panel();
			this._cancelButton = new global::System.Windows.Forms.Button();
			this._okButton = new global::System.Windows.Forms.Button();
			this._primaryPanel.SuspendLayout();
			this._secondaryPanel.SuspendLayout();
			base.SuspendLayout();
			this._primaryPanel.Controls.Add(this._inputTextBox);
			componentResourceManager.ApplyResources(this._primaryPanel, "_primaryPanel");
			this._primaryPanel.Name = "_primaryPanel";
			this._primaryPanel.Paint += new global::System.Windows.Forms.PaintEventHandler(this._primaryPanel_Paint);
			componentResourceManager.ApplyResources(this._inputTextBox, "_inputTextBox");
			this._inputTextBox.Name = "_inputTextBox";
			this._secondaryPanel.Controls.Add(this._cancelButton);
			this._secondaryPanel.Controls.Add(this._okButton);
			componentResourceManager.ApplyResources(this._secondaryPanel, "_secondaryPanel");
			this._secondaryPanel.Name = "_secondaryPanel";
			this._secondaryPanel.Paint += new global::System.Windows.Forms.PaintEventHandler(this._secondaryPanel_Paint);
			componentResourceManager.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this._okButton, "_okButton");
			this._okButton.Name = "_okButton";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new global::System.EventHandler(this._okButton_Click);
			base.AcceptButton = this._okButton;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this._cancelButton;
			base.Controls.Add(this._primaryPanel);
			base.Controls.Add(this._secondaryPanel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "InputDialogForm";
			base.ShowInTaskbar = false;
			base.UseSystemFont = true;
			base.Load += new global::System.EventHandler(this.NewInputBoxForm_Load);
			this._primaryPanel.ResumeLayout(false);
			this._primaryPanel.PerformLayout();
			this._secondaryPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000039 RID: 57
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400003A RID: 58
		private global::System.Windows.Forms.Panel _primaryPanel;

		// Token: 0x0400003B RID: 59
		private global::System.Windows.Forms.Panel _secondaryPanel;

		// Token: 0x0400003C RID: 60
		private global::System.Windows.Forms.Button _cancelButton;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.Button _okButton;

		// Token: 0x0400003E RID: 62
		private global::System.Windows.Forms.TextBox _inputTextBox;
	}
}
