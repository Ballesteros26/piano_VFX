using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Ookii.Dialogs
{
	// Token: 0x0200000F RID: 15
	internal partial class InputDialogForm : ExtendedForm
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000084 RID: 132 RVA: 0x0000447C File Offset: 0x0000267C
		// (remove) Token: 0x06000085 RID: 133 RVA: 0x000044B4 File Offset: 0x000026B4
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OkButtonClickedEventArgs> OkButtonClicked;

		// Token: 0x06000086 RID: 134 RVA: 0x000044E9 File Offset: 0x000026E9
		public InputDialogForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004518 File Offset: 0x00002718
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00004530 File Offset: 0x00002730
		public string MainInstruction
		{
			get
			{
				return this._mainInstruction;
			}
			set
			{
				this._mainInstruction = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000453C File Offset: 0x0000273C
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00004554 File Offset: 0x00002754
		public string Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004560 File Offset: 0x00002760
		// (set) Token: 0x0600008C RID: 140 RVA: 0x0000457D File Offset: 0x0000277D
		public string Input
		{
			get
			{
				return this._inputTextBox.Text;
			}
			set
			{
				this._inputTextBox.Text = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004590 File Offset: 0x00002790
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000045AD File Offset: 0x000027AD
		public int MaxLength
		{
			get
			{
				return this._inputTextBox.MaxLength;
			}
			set
			{
				this._inputTextBox.MaxLength = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000045C0 File Offset: 0x000027C0
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000045DD File Offset: 0x000027DD
		public bool UsePasswordMasking
		{
			get
			{
				return this._inputTextBox.UseSystemPasswordChar;
			}
			set
			{
				this._inputTextBox.UseSystemPasswordChar = value;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000045F0 File Offset: 0x000027F0
		protected virtual void OnOkButtonClicked(OkButtonClickedEventArgs e)
		{
			bool flag = this.OkButtonClicked != null;
			if (flag)
			{
				this.OkButtonClicked.Invoke(this, e);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004619 File Offset: 0x00002819
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			this._textMargin = new SizeF(this._textMargin.Width * factor.Width, this._textMargin.Height * factor.Height);
			base.ScaleControl(factor, specified);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004658 File Offset: 0x00002858
		private void SizeDialog()
		{
			int num = (int)this._textMargin.Width * 2;
			int num2 = base.ClientSize.Height - this._inputTextBox.Top + (int)this._textMargin.Height * 3;
			using (Graphics graphics = this._primaryPanel.CreateGraphics())
			{
				base.ClientSize = DialogHelper.SizeDialog(graphics, this.MainInstruction, this.Content, Screen.FromControl(this), new Font(this.Font, 1), this.Font, num, num2, base.ClientSize.Width, 0);
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000470C File Offset: 0x0000290C
		private static void DrawThemeBackground(IDeviceContext dc, VisualStyleElement element, Rectangle bounds, Rectangle clipRectangle)
		{
			bool isTaskDialogThemeSupported = DialogHelper.IsTaskDialogThemeSupported;
			if (isTaskDialogThemeSupported)
			{
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(element);
				visualStyleRenderer.DrawBackground(dc, bounds, clipRectangle);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004736 File Offset: 0x00002936
		private void DrawText(IDeviceContext dc, ref Point location, bool measureOnly, int width)
		{
			DialogHelper.DrawText(dc, this.MainInstruction, this.Content, ref location, new Font(this.Font, 1), this.Font, measureOnly, width);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004764 File Offset: 0x00002964
		private void _primaryPanel_Paint(object sender, PaintEventArgs e)
		{
			InputDialogForm.DrawThemeBackground(e.Graphics, AdditionalVisualStyleElements.TaskDialog.PrimaryPanel, this._primaryPanel.ClientRectangle, e.ClipRectangle);
			Point point;
			point..ctor((int)this._textMargin.Width, (int)this._textMargin.Height);
			this.DrawText(e.Graphics, ref point, false, base.ClientSize.Width - (int)this._textMargin.Width * 2);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000047E0 File Offset: 0x000029E0
		private void _secondaryPanel_Paint(object sender, PaintEventArgs e)
		{
			InputDialogForm.DrawThemeBackground(e.Graphics, AdditionalVisualStyleElements.TaskDialog.SecondaryPanel, this._secondaryPanel.ClientRectangle, e.ClipRectangle);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004805 File Offset: 0x00002A05
		private void NewInputBoxForm_Load(object sender, EventArgs e)
		{
			this.SizeDialog();
			base.CenterToScreen();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004818 File Offset: 0x00002A18
		private void _okButton_Click(object sender, EventArgs e)
		{
			OkButtonClickedEventArgs okButtonClickedEventArgs = new OkButtonClickedEventArgs(this._inputTextBox.Text, this);
			this.OnOkButtonClicked(okButtonClickedEventArgs);
			bool flag = !okButtonClickedEventArgs.Cancel;
			if (flag)
			{
				base.DialogResult = DialogResult.OK;
			}
		}

		// Token: 0x04000035 RID: 53
		private SizeF _textMargin = new SizeF(12f, 9f);

		// Token: 0x04000036 RID: 54
		private string _mainInstruction;

		// Token: 0x04000037 RID: 55
		private string _content;
	}
}
