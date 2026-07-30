using System;

namespace Ookii.Dialogs
{
	// Token: 0x0200000A RID: 10
	public class ExpandButtonClickedEventArgs : EventArgs
	{
		// Token: 0x0600004B RID: 75 RVA: 0x0000343C File Offset: 0x0000163C
		public ExpandButtonClickedEventArgs(bool expanded)
		{
			this._expanded = expanded;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003450 File Offset: 0x00001650
		public bool Expanded
		{
			get
			{
				return this._expanded;
			}
		}

		// Token: 0x04000024 RID: 36
		private bool _expanded;
	}
}
