using System;

namespace System.Windows.Forms.WebBrowserDialogs
{
	// Token: 0x0200062F RID: 1583
	internal partial class AlertCheck : Generic
	{
		// Token: 0x06005066 RID: 20582 RVA: 0x0013A040 File Offset: 0x00138240
		public AlertCheck(string title, string text, string checkMessage, bool checkState)
			: base(title)
		{
			base.InitTable(3, 1);
			base.AddLabel(0, 0, 0, text, -1, -1);
			base.AddCheck(1, 0, 0, checkMessage, checkState, -1, -1, new EventHandler(this.CheckedChanged));
			base.AddButton(2, 0, 0, "OK", -1, -1, true, false, new EventHandler(this.OkClick));
		}

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06005067 RID: 20583 RVA: 0x0013A0A0 File Offset: 0x001382A0
		public bool Checked
		{
			get
			{
				return this.check;
			}
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x0013A0A8 File Offset: 0x001382A8
		private void OkClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x0013A0B8 File Offset: 0x001382B8
		private void CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			this.check = checkBox.Checked;
		}

		// Token: 0x04002D5E RID: 11614
		private bool check;
	}
}
