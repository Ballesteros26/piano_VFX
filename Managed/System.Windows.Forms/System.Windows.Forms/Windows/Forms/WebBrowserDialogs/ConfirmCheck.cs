using System;

namespace System.Windows.Forms.WebBrowserDialogs
{
	// Token: 0x02000630 RID: 1584
	internal partial class ConfirmCheck : Generic
	{
		// Token: 0x0600506A RID: 20586 RVA: 0x0013A0D8 File Offset: 0x001382D8
		public ConfirmCheck(string title, string text, string checkMessage, bool checkState)
			: base(title)
		{
			base.InitTable(3, 2);
			base.AddLabel(0, 0, 2, text, -1, -1);
			base.AddCheck(1, 0, 2, checkMessage, checkState, -1, -1, new EventHandler(this.CheckedChanged));
			base.AddButton(2, 0, 0, Locale.GetText("OK"), -1, -1, true, false, new EventHandler(this.OkClick));
			base.AddButton(2, 1, 0, Locale.GetText("Cancel"), -1, -1, false, true, new EventHandler(this.CancelClick));
		}

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x0600506B RID: 20587 RVA: 0x0013A160 File Offset: 0x00138360
		public bool Checked
		{
			get
			{
				return this.check;
			}
		}

		// Token: 0x0600506C RID: 20588 RVA: 0x0013A168 File Offset: 0x00138368
		private void OkClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600506D RID: 20589 RVA: 0x0013A178 File Offset: 0x00138378
		private void CancelClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x0013A188 File Offset: 0x00138388
		private void CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			this.check = checkBox.Checked;
		}

		// Token: 0x04002D5F RID: 11615
		private bool check;
	}
}
