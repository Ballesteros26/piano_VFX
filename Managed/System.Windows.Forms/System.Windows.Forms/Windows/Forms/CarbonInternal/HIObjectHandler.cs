using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004AE RID: 1198
	internal class HIObjectHandler : EventHandlerBase, IEventHandler
	{
		// Token: 0x06004BE9 RID: 19433 RVA: 0x0012DEB0 File Offset: 0x0012C0B0
		internal HIObjectHandler(XplatUICarbon driver)
			: base(driver)
		{
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x0012DEBC File Offset: 0x0012C0BC
		public bool ProcessEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg)
		{
			switch (kind)
			{
			case 1U:
			{
				IntPtr zero = IntPtr.Zero;
				HIObjectHandler.GetEventParameter(eventref, 1751740265U, 1751740258U, IntPtr.Zero, 4U, IntPtr.Zero, ref zero);
				return false;
			}
			case 2U:
				HIObjectHandler.CallNextEventHandler(callref, eventref);
				return false;
			case 3U:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x06004BEB RID: 19435
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int CallNextEventHandler(IntPtr callref, IntPtr eventref);

		// Token: 0x06004BEC RID: 19436
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref IntPtr data);

		// Token: 0x0400292D RID: 10541
		internal const uint kEventHIObjectConstruct = 1U;

		// Token: 0x0400292E RID: 10542
		internal const uint kEventHIObjectInitialize = 2U;

		// Token: 0x0400292F RID: 10543
		internal const uint kEventHIObjectDestruct = 3U;
	}
}
