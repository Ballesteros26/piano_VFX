using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004C2 RID: 1218
	internal class WindowHandler : EventHandlerBase, IEventHandler
	{
		// Token: 0x06004C1F RID: 19487 RVA: 0x0012EF18 File Offset: 0x0012D118
		internal WindowHandler(XplatUICarbon driver)
			: base(driver)
		{
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x0012EF24 File Offset: 0x0012D124
		public bool ProcessEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg)
		{
			IntPtr intPtr = this.Driver.HandleToWindow(handle);
			Hwnd hwnd = Hwnd.ObjectFromHandle(intPtr);
			if (intPtr != IntPtr.Zero)
			{
				switch (kind)
				{
				case 24U:
					msg.message = Msg.WM_SHOWWINDOW;
					msg.lParam = (IntPtr)1;
					msg.wParam = (IntPtr)0;
					msg.hwnd = hwnd.Handle;
					return true;
				default:
					switch (kind)
					{
					case 67U:
						NativeWindow.WndProc(hwnd.Handle, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
						msg.hwnd = hwnd.Handle;
						msg.message = Msg.WM_EXITSIZEMOVE;
						return true;
					default:
						if (kind != 5U)
						{
							if (kind != 6U)
							{
								if (kind == 86U)
								{
									foreach (object obj in XplatUICarbon.UtilityWindows)
									{
										IntPtr intPtr2 = (IntPtr)obj;
										if (intPtr2 != handle && XplatUICarbon.IsWindowVisible(intPtr2))
										{
											XplatUICarbon.HideWindow(intPtr2);
										}
									}
									msg.hwnd = hwnd.Handle;
									msg.message = Msg.WM_ENTERSIZEMOVE;
									return true;
								}
								if (kind == 87U)
								{
									foreach (object obj2 in XplatUICarbon.UtilityWindows)
									{
										IntPtr intPtr3 = (IntPtr)obj2;
										if (intPtr3 != handle && !XplatUICarbon.IsWindowVisible(intPtr3))
										{
											XplatUICarbon.ShowWindow(intPtr3);
										}
									}
									msg.hwnd = hwnd.Handle;
									msg.message = Msg.WM_ENTERSIZEMOVE;
									return true;
								}
							}
							else
							{
								Control control = Control.FromHandle(hwnd.client_window);
								if (control != null)
								{
									Form form = control.FindForm();
									if (form != null)
									{
										this.Driver.SendMessage(form.Handle, Msg.WM_ACTIVATE, (IntPtr)0, IntPtr.Zero);
										XplatUICarbon.ActiveWindow = IntPtr.Zero;
									}
								}
								foreach (object obj3 in XplatUICarbon.UtilityWindows)
								{
									IntPtr intPtr4 = (IntPtr)obj3;
									if (intPtr4 != handle && XplatUICarbon.IsWindowVisible(intPtr4))
									{
										XplatUICarbon.HideWindow(intPtr4);
									}
								}
							}
						}
						else
						{
							Control control2 = Control.FromHandle(hwnd.client_window);
							if (control2 != null)
							{
								Form form2 = control2.FindForm();
								if (form2 != null && !form2.IsDisposed)
								{
									this.Driver.SendMessage(form2.Handle, Msg.WM_ACTIVATE, (IntPtr)1, IntPtr.Zero);
									XplatUICarbon.ActiveWindow = hwnd.client_window;
								}
							}
							foreach (object obj4 in XplatUICarbon.UtilityWindows)
							{
								IntPtr intPtr5 = (IntPtr)obj4;
								if (intPtr5 != handle && !XplatUICarbon.IsWindowVisible(intPtr5))
								{
									XplatUICarbon.ShowWindow(intPtr5);
								}
							}
						}
						break;
					case 70U:
						NativeWindow.WndProc(hwnd.Handle, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
						msg.hwnd = hwnd.Handle;
						msg.message = Msg.WM_EXITSIZEMOVE;
						return true;
					case 72U:
						NativeWindow.WndProc(hwnd.Handle, Msg.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
						return false;
					}
					break;
				case 27U:
				{
					Rect rect = default(Rect);
					HIRect hirect = default(HIRect);
					WindowHandler.GetWindowBounds(handle, 33U, ref rect);
					hirect.size.width = (float)(rect.right - rect.left);
					hirect.size.height = (float)(rect.bottom - rect.top);
					WindowHandler.HIViewSetFrame(hwnd.WholeWindow, ref hirect);
					Size size = XplatUICarbon.TranslateQuartzWindowSizeToWindowSize(Control.FromHandle(hwnd.Handle).GetCreateParams(), (int)hirect.size.width, (int)hirect.size.height);
					hwnd.X = (int)rect.left;
					hwnd.Y = (int)rect.top;
					hwnd.Width = size.Width;
					hwnd.Height = size.Height;
					this.Driver.PerformNCCalc(hwnd);
					msg.hwnd = hwnd.Handle;
					msg.message = Msg.WM_WINDOWPOSCHANGED;
					this.Driver.SetCaretPos(XplatUICarbon.Caret.Hwnd, XplatUICarbon.Caret.X, XplatUICarbon.Caret.Y);
					return true;
				}
				case 28U:
					msg.message = Msg.WM_ENTERSIZEMOVE;
					msg.hwnd = hwnd.Handle;
					return true;
				case 29U:
					msg.message = Msg.WM_EXITSIZEMOVE;
					msg.hwnd = hwnd.Handle;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004C21 RID: 19489
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetWindowBounds(IntPtr handle, uint region, ref Rect bounds);

		// Token: 0x06004C22 RID: 19490
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewSetFrame(IntPtr handle, ref HIRect bounds);

		// Token: 0x04002993 RID: 10643
		internal const uint kEventWindowUpdate = 1U;

		// Token: 0x04002994 RID: 10644
		internal const uint kEventWindowDrawContent = 2U;

		// Token: 0x04002995 RID: 10645
		internal const uint kEventWindowActivated = 5U;

		// Token: 0x04002996 RID: 10646
		internal const uint kEventWindowDeactivated = 6U;

		// Token: 0x04002997 RID: 10647
		internal const uint kEventWindowGetClickActivation = 7U;

		// Token: 0x04002998 RID: 10648
		internal const uint kEventWindowShowing = 22U;

		// Token: 0x04002999 RID: 10649
		internal const uint kEventWindowHiding = 23U;

		// Token: 0x0400299A RID: 10650
		internal const uint kEventWindowShown = 24U;

		// Token: 0x0400299B RID: 10651
		internal const uint kEventWindowHidden = 25U;

		// Token: 0x0400299C RID: 10652
		internal const uint kEventWindowCollapsing = 86U;

		// Token: 0x0400299D RID: 10653
		internal const uint kEventWindowExpanding = 87U;

		// Token: 0x0400299E RID: 10654
		internal const uint kEventWindowZoomed = 76U;

		// Token: 0x0400299F RID: 10655
		internal const uint kEventWindowBoundsChanging = 26U;

		// Token: 0x040029A0 RID: 10656
		internal const uint kEventWindowBoundsChanged = 27U;

		// Token: 0x040029A1 RID: 10657
		internal const uint kEventWindowResizeStarted = 28U;

		// Token: 0x040029A2 RID: 10658
		internal const uint kEventWindowResizeCompleted = 29U;

		// Token: 0x040029A3 RID: 10659
		internal const uint kEventWindowDragStarted = 30U;

		// Token: 0x040029A4 RID: 10660
		internal const uint kEventWindowDragCompleted = 31U;

		// Token: 0x040029A5 RID: 10661
		internal const uint kEventWindowTransitionStarted = 88U;

		// Token: 0x040029A6 RID: 10662
		internal const uint kEventWindowTransitionCompleted = 89U;

		// Token: 0x040029A7 RID: 10663
		internal const uint kEventWindowClickDragRgn = 32U;

		// Token: 0x040029A8 RID: 10664
		internal const uint kEventWindowClickResizeRgn = 33U;

		// Token: 0x040029A9 RID: 10665
		internal const uint kEventWindowClickCollapseRgn = 34U;

		// Token: 0x040029AA RID: 10666
		internal const uint kEventWindowClickCloseRgn = 35U;

		// Token: 0x040029AB RID: 10667
		internal const uint kEventWindowClickZoomRgn = 36U;

		// Token: 0x040029AC RID: 10668
		internal const uint kEventWindowClickContentRgn = 37U;

		// Token: 0x040029AD RID: 10669
		internal const uint kEventWindowClickProxyIconRgn = 38U;

		// Token: 0x040029AE RID: 10670
		internal const uint kEventWindowClickToolbarButtonRgn = 41U;

		// Token: 0x040029AF RID: 10671
		internal const uint kEventWindowClickStructureRgn = 42U;

		// Token: 0x040029B0 RID: 10672
		internal const uint kEventWindowCursorChange = 40U;

		// Token: 0x040029B1 RID: 10673
		internal const uint kEventWindowCollapse = 66U;

		// Token: 0x040029B2 RID: 10674
		internal const uint kEventWindowCollapsed = 67U;

		// Token: 0x040029B3 RID: 10675
		internal const uint kEventWindowCollapseAll = 68U;

		// Token: 0x040029B4 RID: 10676
		internal const uint kEventWindowExpand = 69U;

		// Token: 0x040029B5 RID: 10677
		internal const uint kEventWindowExpanded = 70U;

		// Token: 0x040029B6 RID: 10678
		internal const uint kEventWindowExpandAll = 71U;

		// Token: 0x040029B7 RID: 10679
		internal const uint kEventWindowClose = 72U;

		// Token: 0x040029B8 RID: 10680
		internal const uint kEventWindowClosed = 73U;

		// Token: 0x040029B9 RID: 10681
		internal const uint kEventWindowCloseAll = 74U;

		// Token: 0x040029BA RID: 10682
		internal const uint kEventWindowZoom = 75U;

		// Token: 0x040029BB RID: 10683
		internal const uint kEventWindowZoomAll = 77U;

		// Token: 0x040029BC RID: 10684
		internal const uint kEventWindowContextualMenuSelect = 78U;

		// Token: 0x040029BD RID: 10685
		internal const uint kEventWindowPathSelect = 79U;

		// Token: 0x040029BE RID: 10686
		internal const uint kEventWindowGetIdealSize = 80U;

		// Token: 0x040029BF RID: 10687
		internal const uint kEventWindowGetMinimumSize = 81U;

		// Token: 0x040029C0 RID: 10688
		internal const uint kEventWindowGetMaximumSize = 82U;

		// Token: 0x040029C1 RID: 10689
		internal const uint kEventWindowConstrain = 83U;

		// Token: 0x040029C2 RID: 10690
		internal const uint kEventWindowHandleContentClick = 85U;

		// Token: 0x040029C3 RID: 10691
		internal const uint kEventWindowGetDockTileMenu = 90U;

		// Token: 0x040029C4 RID: 10692
		internal const uint kEventWindowHandleActivate = 91U;

		// Token: 0x040029C5 RID: 10693
		internal const uint kEventWindowHandleDeactivate = 92U;

		// Token: 0x040029C6 RID: 10694
		internal const uint kEventWindowProxyBeginDrag = 128U;

		// Token: 0x040029C7 RID: 10695
		internal const uint kEventWindowProxyEndDrag = 129U;

		// Token: 0x040029C8 RID: 10696
		internal const uint kEventWindowToolbarSwitchMode = 150U;

		// Token: 0x040029C9 RID: 10697
		internal const uint kEventWindowFocusAcquired = 200U;

		// Token: 0x040029CA RID: 10698
		internal const uint kEventWindowFocusRelinquish = 201U;

		// Token: 0x040029CB RID: 10699
		internal const uint kEventWindowFocusContent = 202U;

		// Token: 0x040029CC RID: 10700
		internal const uint kEventWindowFocusToolbar = 203U;

		// Token: 0x040029CD RID: 10701
		internal const uint kEventWindowDrawerOpening = 220U;

		// Token: 0x040029CE RID: 10702
		internal const uint kEventWindowDrawerOpened = 221U;

		// Token: 0x040029CF RID: 10703
		internal const uint kEventWindowDrawerClosing = 222U;

		// Token: 0x040029D0 RID: 10704
		internal const uint kEventWindowDrawerClosed = 223U;

		// Token: 0x040029D1 RID: 10705
		internal const uint kEventWindowDrawFrame = 1000U;

		// Token: 0x040029D2 RID: 10706
		internal const uint kEventWindowDrawPart = 1001U;

		// Token: 0x040029D3 RID: 10707
		internal const uint kEventWindowGetRegion = 1002U;

		// Token: 0x040029D4 RID: 10708
		internal const uint kEventWindowHitTest = 1003U;

		// Token: 0x040029D5 RID: 10709
		internal const uint kEventWindowInit = 1004U;

		// Token: 0x040029D6 RID: 10710
		internal const uint kEventWindowDispose = 1005U;

		// Token: 0x040029D7 RID: 10711
		internal const uint kEventWindowDragHilite = 1006U;

		// Token: 0x040029D8 RID: 10712
		internal const uint kEventWindowModified = 1007U;

		// Token: 0x040029D9 RID: 10713
		internal const uint kEventWindowSetupProxyDragImage = 1008U;

		// Token: 0x040029DA RID: 10714
		internal const uint kEventWindowStateChanged = 1009U;

		// Token: 0x040029DB RID: 10715
		internal const uint kEventWindowMeasureTitle = 1010U;

		// Token: 0x040029DC RID: 10716
		internal const uint kEventWindowDrawGrowBox = 1011U;

		// Token: 0x040029DD RID: 10717
		internal const uint kEventWindowGetGrowImageRegion = 1012U;

		// Token: 0x040029DE RID: 10718
		internal const uint kEventWindowPaint = 1013U;
	}
}
