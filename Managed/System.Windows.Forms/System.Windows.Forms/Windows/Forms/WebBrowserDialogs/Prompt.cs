using System;

namespace System.Windows.Forms.WebBrowserDialogs
{
	// Token: 0x02000632 RID: 1586
	internal partial class Prompt : Generic
	{
		// Token: 0x06005079 RID: 20601 RVA: 0x0013A5C4 File Offset: 0x001387C4
		public Prompt(string title, string message, string text)
			: base(title)
		{
			base.InitTable(3, 1);
			base.AddLabel(0, 0, 0, message, -1, -1);
			base.AddText(1, 0, 0, text, -1, -1, new EventHandler(this.onText));
			base.AddButton(2, 0, 0, Locale.GetText("OK"), -1, -1, true, false, new EventHandler(this.OkClick));
		}

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x0600507A RID: 20602 RVA: 0x0013A628 File Offset: 0x00138828
		public new string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x0013A630 File Offset: 0x00138830
		private void OkClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600507C RID: 20604 RVA: 0x0013A640 File Offset: 0x00138840
		private void onText(object sender, EventArgs e)
		{
			TextBox textBox = sender as TextBox;
			this.text = textBox.Text;
		}

		// Token: 0x04002D61 RID: 11617
		private string text;
	}
}
