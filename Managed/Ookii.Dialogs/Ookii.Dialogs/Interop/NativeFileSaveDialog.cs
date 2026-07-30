using System;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000041 RID: 65
	[Guid("84bccd23-5fde-4cdb-aea4-af64b83d78ab")]
	[CoClass(typeof(FileSaveDialogRCW))]
	[ComImport]
	internal interface NativeFileSaveDialog : IFileSaveDialog, IFileDialog, IModalWindow
	{
	}
}
