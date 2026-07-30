using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ookii.Dialogs
{
	// Token: 0x02000011 RID: 17
	public class OkButtonClickedEventArgs : CancelEventArgs
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00004C6A File Offset: 0x00002E6A
		public OkButtonClickedEventArgs(string input, IWin32Window inputBoxWindow)
		{
			this._input = input;
			this._inputBoxWindow = inputBoxWindow;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004C84 File Offset: 0x00002E84
		public string Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004C9C File Offset: 0x00002E9C
		public IWin32Window InputBoxWindow
		{
			get
			{
				return this._inputBoxWindow;
			}
		}

		// Token: 0x04000049 RID: 73
		private string _input;

		// Token: 0x0400004A RID: 74
		private IWin32Window _inputBoxWindow;
	}
}
