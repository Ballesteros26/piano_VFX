using System;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000030 RID: 48
	[Guid("EBBC7C04-315E-11d2-B62F-006097DF5BD4")]
	[CoClass(typeof(ProgressDialogRCW))]
	[ComImport]
	internal interface ProgressDialog : IProgressDialog
	{
	}
}
