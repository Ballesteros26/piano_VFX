using System;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x0200002A RID: 42
	internal static class ComDlgResources
	{
		// Token: 0x0600023C RID: 572 RVA: 0x00009F80 File Offset: 0x00008180
		public static string LoadString(ComDlgResources.ComDlgResourceId id)
		{
			return ComDlgResources._resources.LoadString((uint)id);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009FA0 File Offset: 0x000081A0
		public static string FormatString(ComDlgResources.ComDlgResourceId id, params string[] args)
		{
			return ComDlgResources._resources.FormatString((uint)id, args);
		}

		// Token: 0x040000C5 RID: 197
		private static Win32Resources _resources = new Win32Resources("comdlg32.dll");

		// Token: 0x02000070 RID: 112
		public enum ComDlgResourceId
		{
			// Token: 0x0400023F RID: 575
			OpenButton = 370,
			// Token: 0x04000240 RID: 576
			Open = 384,
			// Token: 0x04000241 RID: 577
			FileNotFound = 391,
			// Token: 0x04000242 RID: 578
			CreatePrompt = 402,
			// Token: 0x04000243 RID: 579
			ReadOnly = 427,
			// Token: 0x04000244 RID: 580
			ConfirmSaveAs = 435
		}
	}
}
