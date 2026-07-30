using System;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	// Token: 0x02000025 RID: 37
	internal class VistaFileDialogEvents : IFileDialogEvents, IFileDialogControlEvents
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00009024 File Offset: 0x00007224
		public VistaFileDialogEvents(VistaFileDialog dialog)
		{
			bool flag = dialog == null;
			if (flag)
			{
				throw new ArgumentNullException("dialog");
			}
			this._dialog = dialog;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00009054 File Offset: 0x00007254
		public HRESULT OnFileOk(IFileDialog pfd)
		{
			bool flag = this._dialog.DoFileOk(pfd);
			HRESULT hresult;
			if (flag)
			{
				hresult = HRESULT.S_OK;
			}
			else
			{
				hresult = HRESULT.S_FALSE;
			}
			return hresult;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009080 File Offset: 0x00007280
		public HRESULT OnFolderChanging(IFileDialog pfd, IShellItem psiFolder)
		{
			GC.SuppressFinalize(psiFolder);
			return HRESULT.S_OK;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000909B File Offset: 0x0000729B
		public void OnFolderChange(IFileDialog pfd)
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000909B File Offset: 0x0000729B
		public void OnSelectionChange(IFileDialog pfd)
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000909B File Offset: 0x0000729B
		public void OnShareViolation(IFileDialog pfd, IShellItem psi)
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000909B File Offset: 0x0000729B
		public void OnTypeChange(IFileDialog pfd)
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000909B File Offset: 0x0000729B
		public void OnOverwrite(IFileDialog pfd, IShellItem psi)
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000909B File Offset: 0x0000729B
		public void OnItemSelected(IFileDialogCustomize pfdc, int dwIDCtl, int dwIDItem)
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000090A0 File Offset: 0x000072A0
		public void OnButtonClicked(IFileDialogCustomize pfdc, int dwIDCtl)
		{
			bool flag = dwIDCtl == 16385;
			if (flag)
			{
				this._dialog.DoHelpRequest();
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000909B File Offset: 0x0000729B
		public void OnCheckButtonToggled(IFileDialogCustomize pfdc, int dwIDCtl, bool bChecked)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000909B File Offset: 0x0000729B
		public void OnControlActivating(IFileDialogCustomize pfdc, int dwIDCtl)
		{
		}

		// Token: 0x040000B4 RID: 180
		private const uint S_OK = 0U;

		// Token: 0x040000B5 RID: 181
		private const uint S_FALSE = 1U;

		// Token: 0x040000B6 RID: 182
		private const uint E_NOTIMPL = 2147500033U;

		// Token: 0x040000B7 RID: 183
		private VistaFileDialog _dialog;
	}
}
