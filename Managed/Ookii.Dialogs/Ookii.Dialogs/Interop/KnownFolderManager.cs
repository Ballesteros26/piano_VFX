using System;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000042 RID: 66
	[Guid("44BEAAEC-24F4-4E90-B3F0-23D258FBB146")]
	[CoClass(typeof(KnownFolderManagerRCW))]
	[ComImport]
	internal interface KnownFolderManager : IKnownFolderManager
	{
	}
}
