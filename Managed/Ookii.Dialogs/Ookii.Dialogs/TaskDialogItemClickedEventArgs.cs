using System;
using System.ComponentModel;

namespace Ookii.Dialogs
{
	// Token: 0x02000020 RID: 32
	public class TaskDialogItemClickedEventArgs : CancelEventArgs
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x00007EEE File Offset: 0x000060EE
		public TaskDialogItemClickedEventArgs(TaskDialogItem item)
		{
			this._item = item;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007F00 File Offset: 0x00006100
		public TaskDialogItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x040000A1 RID: 161
		private readonly TaskDialogItem _item;
	}
}
