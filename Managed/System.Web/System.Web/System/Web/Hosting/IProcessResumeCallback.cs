using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x02000541 RID: 1345
	[Guid("BB1AEEC0-E4EC-47BA-8724-D26AC4F16604")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IProcessResumeCallback
	{
		// Token: 0x06003A80 RID: 14976
		void Resume();
	}
}
