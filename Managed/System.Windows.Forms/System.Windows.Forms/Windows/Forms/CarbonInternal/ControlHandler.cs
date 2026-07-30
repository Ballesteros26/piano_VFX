using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004A2 RID: 1186
	internal class ControlHandler : EventHandlerBase, IEventHandler
	{
		// Token: 0x06004B99 RID: 19353 RVA: 0x0012C990 File Offset: 0x0012AB90
		internal ControlHandler(XplatUICarbon driver)
			: base(driver)
		{
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x0012C99C File Offset: 0x0012AB9C
		public bool ProcessEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg)
		{
			ControlHandler.GetEventParameter(eventref, 757935405U, 1668575852U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(IntPtr)), IntPtr.Zero, ref handle);
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return false;
			}
			msg.hwnd = hwnd.Handle;
			bool flag = hwnd.ClientWindow == handle;
			switch (kind)
			{
			case 18U:
			case 19U:
			case 20U:
			case 21U:
				return Dnd.HandleEvent(callref, eventref, handle, kind, ref msg);
			default:
				switch (kind)
				{
				case 154U:
				{
					HIRect hirect = default(HIRect);
					ControlHandler.HIViewGetFrame(handle, ref hirect);
					if (!flag)
					{
						hwnd.X = (int)hirect.origin.x;
						hwnd.Y = (int)hirect.origin.y;
						hwnd.Width = (int)hirect.size.width;
						hwnd.Height = (int)hirect.size.height;
						this.Driver.PerformNCCalc(hwnd);
					}
					msg.message = Msg.WM_WINDOWPOSCHANGED;
					msg.hwnd = hwnd.Handle;
					return true;
				}
				default:
					if (kind != 4U)
					{
						if (kind != 8U)
						{
							return false;
						}
						short num = 0;
						ControlHandler.SetEventParameter(eventref, 1668313716U, 1668313716U, (uint)Marshal.SizeOf(typeof(short)), ref num);
						return false;
					}
					else
					{
						IntPtr zero = IntPtr.Zero;
						HIRect hirect2 = default(HIRect);
						ControlHandler.GetEventParameter(eventref, 1919381096U, 1919381096U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(IntPtr)), IntPtr.Zero, ref zero);
						if (zero != IntPtr.Zero)
						{
							Rect rect = default(Rect);
							ControlHandler.GetRegionBounds(zero, ref rect);
							hirect2.origin.x = (float)rect.left;
							hirect2.origin.y = (float)rect.top;
							hirect2.size.width = (float)(rect.right - rect.left);
							hirect2.size.height = (float)(rect.bottom - rect.top);
						}
						else
						{
							ControlHandler.HIViewGetBounds(handle, ref hirect2);
						}
						if (!hwnd.visible)
						{
							if (flag)
							{
								hwnd.expose_pending = false;
							}
							else
							{
								hwnd.nc_expose_pending = false;
							}
							return false;
						}
						if (!flag)
						{
							this.DrawBorders(hwnd);
						}
						this.Driver.AddExpose(hwnd, flag, hirect2);
						return true;
					}
					break;
				case 157U:
					if (flag)
					{
						msg.message = Msg.WM_SHOWWINDOW;
						msg.lParam = (IntPtr)0;
						msg.wParam = ((!ControlHandler.HIViewIsVisible(handle)) ? ((IntPtr)0) : ((IntPtr)1));
						return true;
					}
					return false;
				}
				break;
			}
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x0012CC68 File Offset: 0x0012AE68
		private void DrawBorders(Hwnd hwnd)
		{
			FormBorderStyle border_style = hwnd.border_style;
			if (border_style != FormBorderStyle.FixedSingle)
			{
				if (border_style == FormBorderStyle.Fixed3D)
				{
					Graphics graphics = Graphics.FromHwnd(hwnd.whole_window);
					if (hwnd.border_static)
					{
						ControlPaint.DrawBorder3D(graphics, new Rectangle(0, 0, hwnd.Width, hwnd.Height), Border3DStyle.SunkenOuter);
					}
					else
					{
						ControlPaint.DrawBorder3D(graphics, new Rectangle(0, 0, hwnd.Width, hwnd.Height), Border3DStyle.Sunken);
					}
					graphics.Dispose();
				}
			}
			else
			{
				Graphics graphics2 = Graphics.FromHwnd(hwnd.whole_window);
				ControlPaint.DrawBorder(graphics2, new Rectangle(0, 0, hwnd.Width, hwnd.Height), Color.Black, ButtonBorderStyle.Solid);
				graphics2.Dispose();
			}
		}

		// Token: 0x06004B9C RID: 19356
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetRegionBounds(IntPtr rgnhandle, ref Rect region);

		// Token: 0x06004B9D RID: 19357
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref IntPtr data);

		// Token: 0x06004B9E RID: 19358
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetEventParameter(IntPtr eventref, uint name, uint type, uint size, ref short data);

		// Token: 0x06004B9F RID: 19359
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewGetBounds(IntPtr handle, ref HIRect rect);

		// Token: 0x06004BA0 RID: 19360
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewGetFrame(IntPtr handle, ref HIRect rect);

		// Token: 0x06004BA1 RID: 19361
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern bool HIViewIsVisible(IntPtr vHnd);

		// Token: 0x0400286F RID: 10351
		internal const uint kEventControlInitialize = 1000U;

		// Token: 0x04002870 RID: 10352
		internal const uint kEventControlDispose = 1001U;

		// Token: 0x04002871 RID: 10353
		internal const uint kEventControlGetOptimalBounds = 1003U;

		// Token: 0x04002872 RID: 10354
		internal const uint kEventControlDefInitialize = 1000U;

		// Token: 0x04002873 RID: 10355
		internal const uint kEventControlDefDispose = 1001U;

		// Token: 0x04002874 RID: 10356
		internal const uint kEventControlHit = 1U;

		// Token: 0x04002875 RID: 10357
		internal const uint kEventControlSimulateHit = 2U;

		// Token: 0x04002876 RID: 10358
		internal const uint kEventControlHitTest = 3U;

		// Token: 0x04002877 RID: 10359
		internal const uint kEventControlDraw = 4U;

		// Token: 0x04002878 RID: 10360
		internal const uint kEventControlApplyBackground = 5U;

		// Token: 0x04002879 RID: 10361
		internal const uint kEventControlApplyTextColor = 6U;

		// Token: 0x0400287A RID: 10362
		internal const uint kEventControlSetFocusPart = 7U;

		// Token: 0x0400287B RID: 10363
		internal const uint kEventControlGetFocusPart = 8U;

		// Token: 0x0400287C RID: 10364
		internal const uint kEventControlActivate = 9U;

		// Token: 0x0400287D RID: 10365
		internal const uint kEventControlDeactivate = 10U;

		// Token: 0x0400287E RID: 10366
		internal const uint kEventControlSetCursor = 11U;

		// Token: 0x0400287F RID: 10367
		internal const uint kEventControlContextualMenuClick = 12U;

		// Token: 0x04002880 RID: 10368
		internal const uint kEventControlClick = 13U;

		// Token: 0x04002881 RID: 10369
		internal const uint kEventControlGetNextFocusCandidate = 14U;

		// Token: 0x04002882 RID: 10370
		internal const uint kEventControlGetAutoToggleValue = 15U;

		// Token: 0x04002883 RID: 10371
		internal const uint kEventControlInterceptSubviewClick = 16U;

		// Token: 0x04002884 RID: 10372
		internal const uint kEventControlGetClickActivation = 17U;

		// Token: 0x04002885 RID: 10373
		internal const uint kEventControlDragEnter = 18U;

		// Token: 0x04002886 RID: 10374
		internal const uint kEventControlDragWithin = 19U;

		// Token: 0x04002887 RID: 10375
		internal const uint kEventControlDragLeave = 20U;

		// Token: 0x04002888 RID: 10376
		internal const uint kEventControlDragReceive = 21U;

		// Token: 0x04002889 RID: 10377
		internal const uint kEventControlInvalidateForSizeChange = 22U;

		// Token: 0x0400288A RID: 10378
		internal const uint kEventControlTrackingAreaEntered = 23U;

		// Token: 0x0400288B RID: 10379
		internal const uint kEventControlTrackingAreaExited = 24U;

		// Token: 0x0400288C RID: 10380
		internal const uint kEventControlTrack = 51U;

		// Token: 0x0400288D RID: 10381
		internal const uint kEventControlGetScrollToHereStartPoint = 52U;

		// Token: 0x0400288E RID: 10382
		internal const uint kEventControlGetIndicatorDragConstraint = 53U;

		// Token: 0x0400288F RID: 10383
		internal const uint kEventControlIndicatorMoved = 54U;

		// Token: 0x04002890 RID: 10384
		internal const uint kEventControlGhostingFinished = 55U;

		// Token: 0x04002891 RID: 10385
		internal const uint kEventControlGetActionProcPart = 56U;

		// Token: 0x04002892 RID: 10386
		internal const uint kEventControlGetPartRegion = 101U;

		// Token: 0x04002893 RID: 10387
		internal const uint kEventControlGetPartBounds = 102U;

		// Token: 0x04002894 RID: 10388
		internal const uint kEventControlSetData = 103U;

		// Token: 0x04002895 RID: 10389
		internal const uint kEventControlGetData = 104U;

		// Token: 0x04002896 RID: 10390
		internal const uint kEventControlGetSizeConstraints = 105U;

		// Token: 0x04002897 RID: 10391
		internal const uint kEventControlGetFrameMetrics = 106U;

		// Token: 0x04002898 RID: 10392
		internal const uint kEventControlValueFieldChanged = 151U;

		// Token: 0x04002899 RID: 10393
		internal const uint kEventControlAddedSubControl = 152U;

		// Token: 0x0400289A RID: 10394
		internal const uint kEventControlRemovingSubControl = 153U;

		// Token: 0x0400289B RID: 10395
		internal const uint kEventControlBoundsChanged = 154U;

		// Token: 0x0400289C RID: 10396
		internal const uint kEventControlVisibilityChanged = 157U;

		// Token: 0x0400289D RID: 10397
		internal const uint kEventControlTitleChanged = 158U;

		// Token: 0x0400289E RID: 10398
		internal const uint kEventControlOwningWindowChanged = 159U;

		// Token: 0x0400289F RID: 10399
		internal const uint kEventControlHiliteChanged = 160U;

		// Token: 0x040028A0 RID: 10400
		internal const uint kEventControlEnabledStateChanged = 161U;

		// Token: 0x040028A1 RID: 10401
		internal const uint kEventControlLayoutInfoChanged = 162U;

		// Token: 0x040028A2 RID: 10402
		internal const uint kEventControlArbitraryMessage = 201U;

		// Token: 0x040028A3 RID: 10403
		internal const uint kEventParamCGContextRef = 1668183160U;

		// Token: 0x040028A4 RID: 10404
		internal const uint kEventParamDirectObject = 757935405U;

		// Token: 0x040028A5 RID: 10405
		internal const uint kEventParamControlPart = 1668313716U;

		// Token: 0x040028A6 RID: 10406
		internal const uint kEventParamControlLikesDrag = 1668047975U;

		// Token: 0x040028A7 RID: 10407
		internal const uint kEventParamRgnHandle = 1919381096U;

		// Token: 0x040028A8 RID: 10408
		internal const uint typeControlRef = 1668575852U;

		// Token: 0x040028A9 RID: 10409
		internal const uint typeCGContextRef = 1668183160U;

		// Token: 0x040028AA RID: 10410
		internal const uint typeQDPoint = 1363439732U;

		// Token: 0x040028AB RID: 10411
		internal const uint typeQDRgnHandle = 1919381096U;

		// Token: 0x040028AC RID: 10412
		internal const uint typeControlPartCode = 1668313716U;

		// Token: 0x040028AD RID: 10413
		internal const uint typeBoolean = 1651470188U;
	}
}
