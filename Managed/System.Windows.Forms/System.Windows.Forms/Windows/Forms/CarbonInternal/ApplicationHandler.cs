using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004A1 RID: 1185
	internal class ApplicationHandler : EventHandlerBase, IEventHandler
	{
		// Token: 0x06004B97 RID: 19351 RVA: 0x0012C7D4 File Offset: 0x0012A9D4
		internal ApplicationHandler(XplatUICarbon driver)
			: base(driver)
		{
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x0012C7E0 File Offset: 0x0012A9E0
		public bool ProcessEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg)
		{
			if (kind != 1U)
			{
				if (kind == 2U)
				{
					if (XplatUICarbon.FocusWindow != IntPtr.Zero)
					{
						this.Driver.SendMessage(XplatUICarbon.FocusWindow, Msg.WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
					}
					if (XplatUICarbon.Grab.Hwnd != IntPtr.Zero)
					{
						this.Driver.SendMessage(Hwnd.ObjectFromHandle(XplatUICarbon.Grab.Hwnd).Handle, Msg.WM_LBUTTONDOWN, (IntPtr)1, (IntPtr)((this.Driver.MousePosition.X << 16) | this.Driver.MousePosition.Y));
					}
					foreach (object obj in XplatUICarbon.UtilityWindows)
					{
						IntPtr intPtr = (IntPtr)obj;
						if (XplatUICarbon.IsWindowVisible(intPtr))
						{
							XplatUICarbon.HideWindow(intPtr);
						}
					}
				}
			}
			else
			{
				foreach (object obj2 in XplatUICarbon.UtilityWindows)
				{
					IntPtr intPtr2 = (IntPtr)obj2;
					if (!XplatUICarbon.IsWindowVisible(intPtr2))
					{
						XplatUICarbon.ShowWindow(intPtr2);
					}
				}
			}
			return true;
		}

		// Token: 0x0400285C RID: 10332
		internal const uint kEventAppActivated = 1U;

		// Token: 0x0400285D RID: 10333
		internal const uint kEventAppDeactivated = 2U;

		// Token: 0x0400285E RID: 10334
		internal const uint kEventAppQuit = 3U;

		// Token: 0x0400285F RID: 10335
		internal const uint kEventAppLaunchNotification = 4U;

		// Token: 0x04002860 RID: 10336
		internal const uint kEventAppLaunched = 5U;

		// Token: 0x04002861 RID: 10337
		internal const uint kEventAppTerminated = 6U;

		// Token: 0x04002862 RID: 10338
		internal const uint kEventAppFrontSwitched = 7U;

		// Token: 0x04002863 RID: 10339
		internal const uint kEventAppFocusMenuBar = 8U;

		// Token: 0x04002864 RID: 10340
		internal const uint kEventAppFocusNextDocumentWindow = 9U;

		// Token: 0x04002865 RID: 10341
		internal const uint kEventAppFocusNextFloatingWindow = 10U;

		// Token: 0x04002866 RID: 10342
		internal const uint kEventAppFocusToolbar = 11U;

		// Token: 0x04002867 RID: 10343
		internal const uint kEventAppFocusDrawer = 12U;

		// Token: 0x04002868 RID: 10344
		internal const uint kEventAppGetDockTileMenu = 20U;

		// Token: 0x04002869 RID: 10345
		internal const uint kEventAppIsEventInInstantMouser = 104U;

		// Token: 0x0400286A RID: 10346
		internal const uint kEventAppHidden = 107U;

		// Token: 0x0400286B RID: 10347
		internal const uint kEventAppShown = 108U;

		// Token: 0x0400286C RID: 10348
		internal const uint kEventAppSystemUIModeChanged = 109U;

		// Token: 0x0400286D RID: 10349
		internal const uint kEventAppAvailableWindowBoundsChanged = 110U;

		// Token: 0x0400286E RID: 10350
		internal const uint kEventAppActiveWindowChanged = 111U;
	}
}
