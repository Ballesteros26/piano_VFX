using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x020003CB RID: 971
	internal class X11Dnd
	{
		// Token: 0x06004594 RID: 17812 RVA: 0x0010F5A0 File Offset: 0x0010D7A0
		public X11Dnd(IntPtr display, X11Keyboard keyboard)
		{
			this.display = display;
			this.Init();
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x0010F678 File Offset: 0x0010D878
		public bool InDrag()
		{
			return this.drag_data != null && this.drag_data.State != X11Dnd.DragState.None;
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x0010F698 File Offset: 0x0010D898
		public void SetAllowDrop(Hwnd hwnd, bool allow)
		{
			if (hwnd.allow_drop == allow)
			{
				return;
			}
			int[] array = new int[X11Dnd.XdndVersion.Length];
			for (int i = 0; i < X11Dnd.XdndVersion.Length; i++)
			{
				array[i] = X11Dnd.XdndVersion[i].ToInt32();
			}
			XplatUIX11.XChangeProperty(this.display, hwnd.whole_window, this.XdndAware, (IntPtr)4, 32, PropertyMode.Replace, array, (!allow) ? 0 : 1);
			hwnd.allow_drop = allow;
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x0010F720 File Offset: 0x0010D920
		public DragDropEffects StartDrag(IntPtr handle, object data, DragDropEffects allowed_effects)
		{
			this.drag_data = new X11Dnd.DragData();
			this.drag_data.Window = handle;
			this.drag_data.State = X11Dnd.DragState.Beginning;
			this.drag_data.MouseState = XplatUIX11.MouseState;
			this.drag_data.Data = data;
			this.drag_data.SupportedTypes = this.DetermineSupportedTypes(data);
			this.drag_data.AllowedEffects = allowed_effects;
			this.drag_data.Action = this.ActionFromEffect(allowed_effects);
			if (this.CursorNo == null)
			{
				this.CursorNo = new Cursor(typeof(X11Dnd), "DnDNo.cur");
				this.CursorCopy = new Cursor(typeof(X11Dnd), "DnDCopy.cur");
				this.CursorMove = new Cursor(typeof(X11Dnd), "DnDMove.cur");
				this.CursorLink = new Cursor(typeof(X11Dnd), "DnDLink.cur");
			}
			this.drag_data.LastTopLevel = IntPtr.Zero;
			this.control = null;
			MSG msg = default(MSG);
			object obj = XplatUI.StartLoop(Thread.CurrentThread);
			Timer timer = new Timer();
			timer.Tick += new EventHandler(this.DndTickHandler);
			timer.Interval = 100;
			this.drag_data.State = X11Dnd.DragState.Dragging;
			if (XplatUIX11.XSetSelectionOwner(this.display, this.XdndSelection, this.drag_data.Window, IntPtr.Zero) == 0)
			{
				Console.Error.WriteLine("Could not take ownership of XdndSelection aborting drag.");
				this.drag_data.Reset();
				return DragDropEffects.None;
			}
			this.drag_data.State = X11Dnd.DragState.Dragging;
			this.drag_data.CurMousePos = default(Point);
			this.source = (this.toplevel = (this.target = IntPtr.Zero));
			this.dropped = false;
			this.tracking = true;
			this.motion_poll = -1;
			timer.Start();
			this.SendEnter(this.drag_data.Window, this.drag_data.Window, this.drag_data.SupportedTypes);
			this.drag_data.LastTopLevel = this.toplevel;
			while (this.tracking && XplatUI.GetMessage(obj, ref msg, IntPtr.Zero, 0, 0))
			{
				if (msg.message >= Msg.WM_KEYDOWN && msg.message <= Msg.WM_KEYLAST)
				{
					this.HandleKeyMessage(msg);
				}
				else
				{
					Msg message = msg.message;
					switch (message)
					{
					case Msg.WM_MOUSEMOVE:
						this.motion_poll = 0;
						this.drag_data.CurMousePos.X = Control.LowOrder(msg.lParam.ToInt32());
						this.drag_data.CurMousePos.Y = Control.HighOrder((long)msg.lParam.ToInt32());
						this.HandleMouseOver();
						continue;
					default:
						if (message != Msg.WM_MBUTTONUP)
						{
							goto IL_0378;
						}
						break;
					case Msg.WM_LBUTTONUP:
					case Msg.WM_RBUTTONUP:
						break;
					}
					if (msg.message != Msg.WM_LBUTTONDOWN || this.drag_data.MouseState == MouseButtons.Left)
					{
						if (msg.message != Msg.WM_RBUTTONDOWN || this.drag_data.MouseState == MouseButtons.Right)
						{
							if (msg.message != Msg.WM_MBUTTONDOWN || this.drag_data.MouseState == MouseButtons.Middle)
							{
								this.HandleButtonUpMsg();
								this.RemoveCapture(msg.hwnd);
								continue;
							}
						}
					}
					IL_0378:
					XplatUI.DispatchMessage(ref msg);
				}
			}
			timer.Stop();
			if (this.control != null)
			{
				Application.DoEvents();
			}
			if (!this.dropped)
			{
				return DragDropEffects.None;
			}
			if (this.drag_event != null)
			{
				return this.drag_event.Effect;
			}
			return DragDropEffects.None;
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x0010FB08 File Offset: 0x0010DD08
		private void DndTickHandler(object sender, EventArgs e)
		{
			if (this.dropped)
			{
				Timer timer = (Timer)sender;
				if (timer.Interval == 500)
				{
					this.tracking = false;
				}
				else
				{
					timer.Interval = 500;
				}
			}
			if (this.motion_poll > 1)
			{
				this.HandleMouseOver();
			}
			else if (this.motion_poll > -1)
			{
				this.motion_poll++;
			}
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x0010FB80 File Offset: 0x0010DD80
		private void DefaultEnterLeave(object user_data)
		{
			IntPtr intPtr;
			IntPtr intPtr2;
			int num;
			int num2;
			this.GetWindowsUnderPointer(out intPtr, out intPtr2, out num, out num2);
			Control control = Control.FromHandle(intPtr);
			if (control == null || !control.AllowDrop)
			{
				return;
			}
			Point mousePosition = Control.MousePosition;
			DragEventArgs dragEventArgs = new DragEventArgs(this.data, 0, mousePosition.X, mousePosition.Y, this.drag_data.AllowedEffects, DragDropEffects.None);
			control.DndEnter(dragEventArgs);
			if ((dragEventArgs.Effect & this.drag_data.AllowedEffects) != DragDropEffects.None)
			{
				control.DndDrop(dragEventArgs);
			}
			else
			{
				control.DndLeave(EventArgs.Empty);
			}
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x0010FC24 File Offset: 0x0010DE24
		public void HandleButtonUpMsg()
		{
			if (this.drag_data.State != X11Dnd.DragState.Beginning)
			{
				if (this.drag_data.State != X11Dnd.DragState.None)
				{
					if (this.drag_data.WillAccept)
					{
						if (this.QueryContinue(false, DragAction.Drop))
						{
							return;
						}
					}
					else
					{
						if (this.QueryContinue(false, DragAction.Cancel))
						{
							return;
						}
						if (this.motion_poll == -1)
						{
							this.DefaultEnterLeave(this.drag_data.Data);
						}
					}
					this.drag_data.State = X11Dnd.DragState.None;
				}
			}
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x0010FCB4 File Offset: 0x0010DEB4
		private void RemoveCapture(IntPtr handle)
		{
			Control control = this.MwfWindow(handle);
			if (control.InternalCapture)
			{
				control.InternalCapture = false;
			}
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x0010FCDC File Offset: 0x0010DEDC
		public bool HandleMouseOver()
		{
			IntPtr intPtr;
			IntPtr intPtr2;
			int num;
			int num2;
			this.GetWindowsUnderPointer(out intPtr, out intPtr2, out num, out num2);
			if (intPtr != this.drag_data.LastWindow && this.drag_data.State == X11Dnd.DragState.Entered)
			{
				this.drag_data.State = X11Dnd.DragState.Dragging;
				if (intPtr2 != this.drag_data.LastTopLevel)
				{
					this.SendLeave(this.drag_data.LastTopLevel, intPtr2);
				}
			}
			this.drag_data.State = X11Dnd.DragState.Entered;
			if (intPtr2 != this.drag_data.LastTopLevel)
			{
				this.SendEnter(intPtr2, this.drag_data.Window, this.drag_data.SupportedTypes);
			}
			else
			{
				this.SendPosition(intPtr2, this.drag_data.Window, this.drag_data.Action, num, num2, IntPtr.Zero);
			}
			this.drag_data.LastTopLevel = intPtr2;
			this.drag_data.LastWindow = intPtr;
			return true;
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x0010FDD4 File Offset: 0x0010DFD4
		private void GetWindowsUnderPointer(out IntPtr window, out IntPtr toplevel, out int x_root, out int y_root)
		{
			toplevel = IntPtr.Zero;
			window = XplatUIX11.RootWindowHandle;
			bool flag = false;
			int num = (x_root = this.drag_data.CurMousePos.X);
			int num2 = (y_root = this.drag_data.CurMousePos.Y);
			IntPtr intPtr;
			IntPtr intPtr2;
			int num3;
			int num4;
			int num5;
			while (XplatUIX11.XQueryPointer(this.display, window, out intPtr, out intPtr2, out num3, out num4, out num, out num2, out num5))
			{
				if (!flag)
				{
					flag = this.IsWindowDndAware(window);
					if (flag)
					{
						toplevel = window;
						x_root = num3;
						y_root = num4;
					}
				}
				if (intPtr2 == IntPtr.Zero)
				{
					break;
				}
				window = intPtr2;
			}
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x0010FE84 File Offset: 0x0010E084
		public void HandleKeyMessage(MSG msg)
		{
			if (msg.wParam.ToInt32() == 27)
			{
				this.QueryContinue(true, DragAction.Cancel);
			}
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x0010FEA4 File Offset: 0x0010E0A4
		public bool HandleClientMessage(ref XEvent xevent)
		{
			if (xevent.ClientMessageEvent.message_type == this.XdndPosition)
			{
				return this.Accepting_HandlePositionEvent(ref xevent);
			}
			if (xevent.ClientMessageEvent.message_type == this.XdndEnter)
			{
				return this.Accepting_HandleEnterEvent(ref xevent);
			}
			if (xevent.ClientMessageEvent.message_type == this.XdndDrop)
			{
				return this.Accepting_HandleDropEvent(ref xevent);
			}
			if (xevent.ClientMessageEvent.message_type == this.XdndLeave)
			{
				return this.Accepting_HandleLeaveEvent(ref xevent);
			}
			if (xevent.ClientMessageEvent.message_type == this.XdndStatus)
			{
				return this.HandleStatusEvent(ref xevent);
			}
			return xevent.ClientMessageEvent.message_type == this.XdndFinished && this.HandleFinishedEvent(ref xevent);
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x0010FF84 File Offset: 0x0010E184
		public bool HandleSelectionNotifyEvent(ref XEvent xevent)
		{
			X11Dnd.MimeHandler mimeHandler = this.FindHandler(xevent.SelectionEvent.target);
			if (mimeHandler == null)
			{
				return false;
			}
			if (this.data == null)
			{
				this.data = new DataObject();
			}
			mimeHandler.Converter.GetData(this, this.data, ref xevent);
			this.converts_pending--;
			if (this.converts_pending <= 0 && this.position_recieved)
			{
				this.drag_event = new DragEventArgs(this.data, 0, this.pos_x, this.pos_y, this.allowed, DragDropEffects.None);
				this.control.DndEnter(this.drag_event);
				this.SendStatus(this.source, this.drag_event.Effect);
				this.status_sent = true;
			}
			return true;
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x00110050 File Offset: 0x0010E250
		public bool HandleSelectionRequestEvent(ref XEvent xevent)
		{
			if (xevent.SelectionRequestEvent.selection != this.XdndSelection)
			{
				return false;
			}
			X11Dnd.MimeHandler mimeHandler = this.FindHandler(xevent.SelectionRequestEvent.target);
			if (mimeHandler == null)
			{
				return false;
			}
			mimeHandler.Converter.SetData(this, this.drag_data.Data, ref xevent);
			return true;
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x001100B0 File Offset: 0x0010E2B0
		private bool QueryContinue(bool escape, DragAction action)
		{
			QueryContinueDragEventArgs queryContinueDragEventArgs = new QueryContinueDragEventArgs((int)XplatUI.State.ModifierKeys, escape, action);
			Control control = this.MwfWindow(this.source);
			if (control == null)
			{
				this.tracking = false;
				return false;
			}
			control.DndContinueDrag(queryContinueDragEventArgs);
			switch (queryContinueDragEventArgs.Action)
			{
			case DragAction.Continue:
				return true;
			case DragAction.Drop:
				this.SendDrop(this.drag_data.LastTopLevel, this.source, IntPtr.Zero);
				this.tracking = false;
				return true;
			case DragAction.Cancel:
				this.drag_data.Reset();
				control.InternalCapture = false;
				break;
			}
			this.SendLeave(this.drag_data.LastTopLevel, this.toplevel);
			this.RestoreDefaultCursor();
			this.tracking = false;
			return false;
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x00110170 File Offset: 0x0010E370
		private void RestoreDefaultCursor()
		{
			XplatUIX11.XChangeActivePointerGrab(this.display, EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.PointerMotionMask | EventMask.ButtonMotionMask, Cursors.Default.Handle, IntPtr.Zero);
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x001101A0 File Offset: 0x0010E3A0
		private void GiveFeedback(IntPtr action)
		{
			GiveFeedbackEventArgs giveFeedbackEventArgs = new GiveFeedbackEventArgs(this.EffectFromAction(this.drag_data.Action), true);
			Control control = this.MwfWindow(this.source);
			control.DndFeedback(giveFeedbackEventArgs);
			if (giveFeedbackEventArgs.UseDefaultCursors)
			{
				Cursor cursor = this.CursorNo;
				if (this.drag_data.WillAccept)
				{
					if (action == this.XdndActionCopy)
					{
						cursor = this.CursorCopy;
					}
					else if (action == this.XdndActionLink)
					{
						cursor = this.CursorLink;
					}
					else if (action == this.XdndActionMove)
					{
						cursor = this.CursorMove;
					}
				}
				XplatUIX11.XChangeActivePointerGrab(this.display, EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.PointerMotionMask | EventMask.ButtonMotionMask, cursor.Handle, IntPtr.Zero);
			}
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x0011026C File Offset: 0x0010E46C
		private void SetProperty(ref XEvent xevent, IntPtr data, int length)
		{
			XEvent xevent2 = default(XEvent);
			xevent2.SelectionEvent.type = XEventName.SelectionNotify;
			xevent2.SelectionEvent.send_event = true;
			xevent2.SelectionEvent.display = this.display;
			xevent2.SelectionEvent.selection = xevent.SelectionRequestEvent.selection;
			xevent2.SelectionEvent.target = xevent.SelectionRequestEvent.target;
			xevent2.SelectionEvent.requestor = xevent.SelectionRequestEvent.requestor;
			xevent2.SelectionEvent.time = xevent.SelectionRequestEvent.time;
			xevent2.SelectionEvent.property = IntPtr.Zero;
			XplatUIX11.XChangeProperty(this.display, xevent.SelectionRequestEvent.requestor, xevent.SelectionRequestEvent.property, xevent.SelectionRequestEvent.target, 8, PropertyMode.Replace, data, length);
			xevent2.SelectionEvent.property = xevent.SelectionRequestEvent.property;
			XplatUIX11.XSendEvent(this.display, xevent.SelectionRequestEvent.requestor, false, (IntPtr)0, ref xevent2);
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x00110384 File Offset: 0x0010E584
		private void Reset()
		{
			this.ResetSourceData();
			this.ResetTargetData();
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x00110394 File Offset: 0x0010E594
		private void ResetSourceData()
		{
			this.converts_pending = 0;
			this.data = null;
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x001103A4 File Offset: 0x0010E5A4
		private void ResetTargetData()
		{
			this.position_recieved = false;
			this.status_sent = false;
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x001103B4 File Offset: 0x0010E5B4
		private bool Accepting_HandleEnterEvent(ref XEvent xevent)
		{
			this.Reset();
			this.source = xevent.ClientMessageEvent.ptr1;
			this.toplevel = xevent.AnyEvent.window;
			this.target = IntPtr.Zero;
			this.ConvertData(ref xevent);
			return true;
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x00110400 File Offset: 0x0010E600
		private bool Accepting_HandlePositionEvent(ref XEvent xevent)
		{
			this.pos_x = (int)xevent.ClientMessageEvent.ptr3 >> 16;
			this.pos_y = (int)xevent.ClientMessageEvent.ptr3 & 65535;
			if (this.MwfWindow(this.source) == null)
			{
				this.allowed = this.EffectsFromX11Source(this.source, xevent.ClientMessageEvent.ptr5) | DragDropEffects.Copy;
			}
			else
			{
				this.allowed = this.drag_data.AllowedEffects;
			}
			IntPtr intPtr = XplatUIX11.XRootWindow(this.display, 0);
			IntPtr intPtr2 = this.toplevel;
			IntPtr intPtr3 = IntPtr.Zero;
			for (;;)
			{
				IntPtr zero = IntPtr.Zero;
				int num;
				int num2;
				if (!XplatUIX11.XTranslateCoordinates(this.display, intPtr, intPtr2, this.pos_x, this.pos_y, out num, out num2, out zero))
				{
					break;
				}
				if (zero == IntPtr.Zero)
				{
					break;
				}
				intPtr2 = zero;
				Hwnd hwnd = Hwnd.ObjectFromHandle(intPtr2);
				Control control = Control.FromHandle(hwnd.client_window);
				if (control != null && control.allow_drop)
				{
					intPtr3 = intPtr2;
				}
			}
			if (intPtr3 != IntPtr.Zero)
			{
				intPtr2 = intPtr3;
			}
			if (this.target != intPtr2)
			{
				this.Finish();
			}
			this.target = intPtr2;
			Hwnd hwnd2 = Hwnd.ObjectFromHandle(this.target);
			Control control2 = Control.FromHandle(hwnd2.client_window);
			if (control2 == null)
			{
				return true;
			}
			if (!control2.allow_drop)
			{
				this.SendStatus(this.source, DragDropEffects.None);
				this.Finish();
				return true;
			}
			this.control = control2;
			this.position_recieved = true;
			if (this.converts_pending > 0)
			{
				return true;
			}
			if (!this.status_sent)
			{
				this.drag_event = new DragEventArgs(this.data, 0, this.pos_x, this.pos_y, this.allowed, DragDropEffects.None);
				this.control.DndEnter(this.drag_event);
				this.SendStatus(this.source, this.drag_event.Effect);
				this.status_sent = true;
			}
			else
			{
				this.drag_event.x = this.pos_x;
				this.drag_event.y = this.pos_y;
				this.control.DndOver(this.drag_event);
				this.SendStatus(this.source, this.drag_event.Effect);
			}
			return true;
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x00110660 File Offset: 0x0010E860
		private void Finish()
		{
			if (this.control != null)
			{
				if (this.drag_event == null)
				{
					if (this.data == null)
					{
						this.data = new DataObject();
					}
					this.drag_event = new DragEventArgs(this.data, 0, this.pos_x, this.pos_y, this.allowed, DragDropEffects.None);
				}
				this.control.DndLeave(this.drag_event);
				this.control = null;
			}
			this.ResetTargetData();
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x001106DC File Offset: 0x0010E8DC
		private bool Accepting_HandleDropEvent(ref XEvent xevent)
		{
			if (this.control != null && this.drag_event != null)
			{
				this.drag_event = new DragEventArgs(this.data, 0, this.pos_x, this.pos_y, this.allowed, this.drag_event.Effect);
				this.control.DndDrop(this.drag_event);
			}
			this.SendFinished();
			return true;
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x00110748 File Offset: 0x0010E948
		private bool Accepting_HandleLeaveEvent(ref XEvent xevent)
		{
			if (this.control != null && this.drag_event != null)
			{
				this.control.DndLeave(this.drag_event);
			}
			return true;
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x00110780 File Offset: 0x0010E980
		private bool HandleStatusEvent(ref XEvent xevent)
		{
			if (this.drag_data != null && this.drag_data.State == X11Dnd.DragState.Entered)
			{
				if (!this.QueryContinue(false, DragAction.Continue))
				{
					return true;
				}
				this.drag_data.WillAccept = ((int)xevent.ClientMessageEvent.ptr2 & 1) != 0;
				this.GiveFeedback(xevent.ClientMessageEvent.ptr5);
			}
			return true;
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x001107F0 File Offset: 0x0010E9F0
		private bool HandleFinishedEvent(ref XEvent xevent)
		{
			return true;
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x001107F4 File Offset: 0x0010E9F4
		private DragDropEffects EffectsFromX11Source(IntPtr source, IntPtr action_atom)
		{
			DragDropEffects dragDropEffects = DragDropEffects.None;
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr;
			int num;
			IntPtr intPtr2;
			IntPtr intPtr3;
			XplatUIX11.XGetWindowProperty(this.display, source, this.XdndActionList, IntPtr.Zero, new IntPtr(32), false, (IntPtr)0, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
			int num2 = Marshal.SizeOf(typeof(IntPtr));
			for (int i = 0; i < intPtr2.ToInt32(); i++)
			{
				IntPtr intPtr4 = Marshal.ReadIntPtr(zero, i * num2);
				dragDropEffects |= this.EffectFromAction(intPtr4);
			}
			if (dragDropEffects == DragDropEffects.None)
			{
				dragDropEffects = this.EffectFromAction(action_atom);
			}
			return dragDropEffects;
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x00110890 File Offset: 0x0010EA90
		private DragDropEffects EffectFromAction(IntPtr action)
		{
			if (action == this.XdndActionCopy)
			{
				return DragDropEffects.Copy;
			}
			if (action == this.XdndActionMove)
			{
				return DragDropEffects.Move;
			}
			if (action == this.XdndActionLink)
			{
				return DragDropEffects.Link;
			}
			return DragDropEffects.None;
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x001108D8 File Offset: 0x0010EAD8
		private IntPtr ActionFromEffect(DragDropEffects effect)
		{
			IntPtr intPtr = IntPtr.Zero;
			if ((effect & DragDropEffects.Copy) != DragDropEffects.None)
			{
				intPtr = this.XdndActionCopy;
			}
			else if ((effect & DragDropEffects.Move) != DragDropEffects.None)
			{
				intPtr = this.XdndActionMove;
			}
			else if ((effect & DragDropEffects.Link) != DragDropEffects.None)
			{
				intPtr = this.XdndActionLink;
			}
			return intPtr;
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x00110924 File Offset: 0x0010EB24
		private bool ConvertData(ref XEvent xevent)
		{
			bool flag = false;
			Control control = this.MwfWindow(this.source);
			if (control == null || this.drag_data == null)
			{
				foreach (IntPtr intPtr in this.SourceSupportedList(ref xevent))
				{
					X11Dnd.MimeHandler mimeHandler = this.FindHandler(intPtr);
					if (mimeHandler != null)
					{
						XplatUIX11.XConvertSelection(this.display, this.XdndSelection, mimeHandler.Type, mimeHandler.NonProtocol, this.toplevel, IntPtr.Zero);
						this.converts_pending++;
						flag = true;
					}
				}
				return flag;
			}
			if (!this.tracking)
			{
				return false;
			}
			IDataObject dataObject = this.drag_data.Data as IDataObject;
			if (dataObject != null)
			{
				this.data = dataObject;
			}
			else
			{
				if (this.data == null)
				{
					this.data = new DataObject();
				}
				this.SetDataWithFormats(this.drag_data.Data);
			}
			return true;
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x00110A24 File Offset: 0x0010EC24
		private void SetDataWithFormats(object value)
		{
			if (value is string)
			{
				this.data.SetData(DataFormats.Text, value);
				this.data.SetData(DataFormats.UnicodeText, value);
			}
			this.data.SetData(value);
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x00110A60 File Offset: 0x0010EC60
		private X11Dnd.MimeHandler FindHandler(IntPtr atom)
		{
			if (atom == IntPtr.Zero)
			{
				return null;
			}
			foreach (X11Dnd.MimeHandler mimeHandler in this.MimeHandlers)
			{
				if (mimeHandler.Type == atom)
				{
					return mimeHandler;
				}
			}
			return null;
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x00110AB4 File Offset: 0x0010ECB4
		private X11Dnd.MimeHandler FindHandler(string name)
		{
			foreach (X11Dnd.MimeHandler mimeHandler in this.MimeHandlers)
			{
				foreach (string text in mimeHandler.Aliases)
				{
					if (text == name)
					{
						return mimeHandler;
					}
				}
			}
			return null;
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x00110B18 File Offset: 0x0010ED18
		private void SendStatus(IntPtr source, DragDropEffects effect)
		{
			XEvent xevent = default(XEvent);
			xevent.AnyEvent.type = XEventName.ClientMessage;
			xevent.AnyEvent.display = this.display;
			xevent.ClientMessageEvent.window = source;
			xevent.ClientMessageEvent.message_type = this.XdndStatus;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = this.toplevel;
			if (effect != DragDropEffects.None && (effect & this.allowed) != DragDropEffects.None)
			{
				xevent.ClientMessageEvent.ptr2 = (IntPtr)1;
			}
			xevent.ClientMessageEvent.ptr5 = this.ActionFromEffect(effect);
			XplatUIX11.XSendEvent(this.display, source, false, IntPtr.Zero, ref xevent);
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x00110BDC File Offset: 0x0010EDDC
		private void SendEnter(IntPtr handle, IntPtr from, IntPtr[] supported)
		{
			XEvent xevent = default(XEvent);
			xevent.AnyEvent.type = XEventName.ClientMessage;
			xevent.AnyEvent.display = this.display;
			xevent.ClientMessageEvent.window = handle;
			xevent.ClientMessageEvent.message_type = this.XdndEnter;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = from;
			xevent.ClientMessageEvent.ptr2 = (IntPtr)((long)X11Dnd.XdndVersion[0] << 24);
			if (supported.Length > 0)
			{
				xevent.ClientMessageEvent.ptr3 = supported[0];
			}
			if (supported.Length > 1)
			{
				xevent.ClientMessageEvent.ptr4 = supported[1];
			}
			if (supported.Length > 2)
			{
				xevent.ClientMessageEvent.ptr5 = supported[2];
			}
			XplatUIX11.XSendEvent(this.display, handle, false, IntPtr.Zero, ref xevent);
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x00110CC8 File Offset: 0x0010EEC8
		private void SendDrop(IntPtr handle, IntPtr from, IntPtr time)
		{
			XEvent xevent = default(XEvent);
			xevent.AnyEvent.type = XEventName.ClientMessage;
			xevent.AnyEvent.display = this.display;
			xevent.ClientMessageEvent.window = handle;
			xevent.ClientMessageEvent.message_type = this.XdndDrop;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = from;
			xevent.ClientMessageEvent.ptr3 = time;
			XplatUIX11.XSendEvent(this.display, handle, false, IntPtr.Zero, ref xevent);
			this.dropped = true;
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x00110D60 File Offset: 0x0010EF60
		private void SendPosition(IntPtr handle, IntPtr from, IntPtr action, int x, int y, IntPtr time)
		{
			XEvent xevent = default(XEvent);
			xevent.AnyEvent.type = XEventName.ClientMessage;
			xevent.AnyEvent.display = this.display;
			xevent.ClientMessageEvent.window = handle;
			xevent.ClientMessageEvent.message_type = this.XdndPosition;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = from;
			xevent.ClientMessageEvent.ptr3 = (IntPtr)((x << 16) | (y & 65535));
			xevent.ClientMessageEvent.ptr4 = time;
			xevent.ClientMessageEvent.ptr5 = action;
			XplatUIX11.XSendEvent(this.display, handle, false, IntPtr.Zero, ref xevent);
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x00110E20 File Offset: 0x0010F020
		private void SendLeave(IntPtr handle, IntPtr from)
		{
			XEvent xevent = default(XEvent);
			xevent.AnyEvent.type = XEventName.ClientMessage;
			xevent.AnyEvent.display = this.display;
			xevent.ClientMessageEvent.window = handle;
			xevent.ClientMessageEvent.message_type = this.XdndLeave;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = from;
			XplatUIX11.XSendEvent(this.display, handle, false, IntPtr.Zero, ref xevent);
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x00110EA4 File Offset: 0x0010F0A4
		private void SendFinished()
		{
			XEvent xevent = default(XEvent);
			xevent.AnyEvent.type = XEventName.ClientMessage;
			xevent.AnyEvent.display = this.display;
			xevent.ClientMessageEvent.window = this.source;
			xevent.ClientMessageEvent.message_type = this.XdndFinished;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = this.toplevel;
			XplatUIX11.XSendEvent(this.display, this.source, false, IntPtr.Zero, ref xevent);
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x00110F38 File Offset: 0x0010F138
		private void Init()
		{
			this.XdndAware = XplatUIX11.XInternAtom(this.display, "XdndAware", false);
			this.XdndEnter = XplatUIX11.XInternAtom(this.display, "XdndEnter", false);
			this.XdndLeave = XplatUIX11.XInternAtom(this.display, "XdndLeave", false);
			this.XdndPosition = XplatUIX11.XInternAtom(this.display, "XdndPosition", false);
			this.XdndStatus = XplatUIX11.XInternAtom(this.display, "XdndStatus", false);
			this.XdndDrop = XplatUIX11.XInternAtom(this.display, "XdndDrop", false);
			this.XdndSelection = XplatUIX11.XInternAtom(this.display, "XdndSelection", false);
			this.XdndFinished = XplatUIX11.XInternAtom(this.display, "XdndFinished", false);
			this.XdndTypeList = XplatUIX11.XInternAtom(this.display, "XdndTypeList", false);
			this.XdndActionCopy = XplatUIX11.XInternAtom(this.display, "XdndActionCopy", false);
			this.XdndActionMove = XplatUIX11.XInternAtom(this.display, "XdndActionMove", false);
			this.XdndActionLink = XplatUIX11.XInternAtom(this.display, "XdndActionLink", false);
			this.XdndActionList = XplatUIX11.XInternAtom(this.display, "XdndActionList", false);
			foreach (X11Dnd.MimeHandler mimeHandler in this.MimeHandlers)
			{
				mimeHandler.Type = XplatUIX11.XInternAtom(this.display, mimeHandler.Name, false);
				mimeHandler.NonProtocol = XplatUIX11.XInternAtom(this.display, "MWFNonP+" + mimeHandler.Name, false);
			}
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x001110CC File Offset: 0x0010F2CC
		private IntPtr[] SourceSupportedList(ref XEvent xevent)
		{
			IntPtr[] array;
			if (((int)xevent.ClientMessageEvent.ptr2 & 1) == 0)
			{
				array = new IntPtr[]
				{
					xevent.ClientMessageEvent.ptr3,
					xevent.ClientMessageEvent.ptr4,
					xevent.ClientMessageEvent.ptr5
				};
			}
			else
			{
				IntPtr zero = IntPtr.Zero;
				IntPtr intPtr;
				int num;
				IntPtr intPtr2;
				IntPtr intPtr3;
				XplatUIX11.XGetWindowProperty(this.display, this.source, this.XdndTypeList, IntPtr.Zero, new IntPtr(32), false, (IntPtr)4, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
				array = new IntPtr[intPtr2.ToInt32()];
				for (int i = 0; i < intPtr2.ToInt32(); i++)
				{
					array[i] = (IntPtr)Marshal.ReadInt32(zero, i * Marshal.SizeOf(typeof(int)));
				}
				XplatUIX11.XFree(zero);
			}
			return array;
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x001111DC File Offset: 0x0010F3DC
		private string GetText(ref XEvent xevent, bool unicode)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			IntPtr zero;
			for (;;)
			{
				zero = IntPtr.Zero;
				IntPtr intPtr;
				int num2;
				IntPtr intPtr2;
				IntPtr intPtr3;
				if (XplatUIX11.XGetWindowProperty(this.display, xevent.AnyEvent.window, xevent.SelectionEvent.property, IntPtr.Zero, new IntPtr(16777215), false, (IntPtr)0, out intPtr, out num2, out intPtr2, out intPtr3, ref zero) != 0)
				{
					break;
				}
				if (unicode)
				{
					stringBuilder.Append(Marshal.PtrToStringUni(zero));
				}
				else
				{
					stringBuilder.Append(Marshal.PtrToStringAnsi(zero));
				}
				num += intPtr2.ToInt32();
				XplatUIX11.XFree(zero);
				if (intPtr3.ToInt32() <= 0)
				{
					goto IL_00A8;
				}
			}
			XplatUIX11.XFree(zero);
			IL_00A8:
			if (num == 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x001112A0 File Offset: 0x0010F4A0
		private MemoryStream GetData(ref XEvent xevent)
		{
			int num = 0;
			MemoryStream memoryStream = new MemoryStream();
			IntPtr zero;
			for (;;)
			{
				zero = IntPtr.Zero;
				IntPtr intPtr;
				int num2;
				IntPtr intPtr2;
				IntPtr intPtr3;
				if (XplatUIX11.XGetWindowProperty(this.display, xevent.AnyEvent.window, xevent.SelectionEvent.property, IntPtr.Zero, new IntPtr(16777215), false, (IntPtr)0, out intPtr, out num2, out intPtr2, out intPtr3, ref zero) != 0)
				{
					break;
				}
				for (int i = 0; i < intPtr2.ToInt32(); i++)
				{
					memoryStream.WriteByte(Marshal.ReadByte(zero, i));
				}
				num += intPtr2.ToInt32();
				XplatUIX11.XFree(zero);
				if (intPtr3.ToInt32() <= 0)
				{
					return memoryStream;
				}
			}
			XplatUIX11.XFree(zero);
			return memoryStream;
		}

		// Token: 0x060045C2 RID: 17858 RVA: 0x0011135C File Offset: 0x0010F55C
		private Control MwfWindow(IntPtr window)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(window);
			if (hwnd == null)
			{
				return null;
			}
			Control control = Control.FromHandle(hwnd.client_window);
			if (control == null)
			{
				control = Control.FromHandle(window);
			}
			return control;
		}

		// Token: 0x060045C3 RID: 17859 RVA: 0x00111394 File Offset: 0x0010F594
		private bool IsWindowDndAware(IntPtr handle)
		{
			bool flag = true;
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr;
			int num;
			IntPtr intPtr2;
			IntPtr intPtr3;
			XplatUIX11.XGetWindowProperty(this.display, handle, this.XdndAware, IntPtr.Zero, new IntPtr(134217728), false, (IntPtr)4, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
			if (intPtr != (IntPtr)4 || num != 32 || intPtr2.ToInt32() == 0 || zero == IntPtr.Zero)
			{
				if (zero != IntPtr.Zero)
				{
					XplatUIX11.XFree(zero);
				}
				return false;
			}
			int num2 = Marshal.ReadInt32(zero, 0);
			if (num2 < 3)
			{
				Console.Error.WriteLine("XDND Version too old (" + num2 + ").");
				XplatUIX11.XFree(zero);
				return false;
			}
			if (intPtr2.ToInt32() > 1)
			{
				flag = false;
				for (int i = 1; i < intPtr2.ToInt32(); i++)
				{
					IntPtr intPtr4 = (IntPtr)Marshal.ReadInt32(zero, i * Marshal.SizeOf(typeof(int)));
					for (int j = 0; j < this.drag_data.SupportedTypes.Length; j++)
					{
						if (this.drag_data.SupportedTypes[j] == intPtr4)
						{
							flag = true;
							break;
						}
					}
				}
			}
			XplatUIX11.XFree(zero);
			return flag;
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x00111504 File Offset: 0x0010F704
		private IntPtr[] DetermineSupportedTypes(object data)
		{
			ArrayList arrayList = new ArrayList();
			if (data is string)
			{
				X11Dnd.MimeHandler mimeHandler = this.FindHandler("text/plain");
				if (mimeHandler != null)
				{
					arrayList.Add(mimeHandler.Type);
				}
			}
			IDataObject dataObject = data as IDataObject;
			if (dataObject != null)
			{
				foreach (string text in dataObject.GetFormats(true))
				{
					X11Dnd.MimeHandler mimeHandler2 = this.FindHandler(text);
					if (mimeHandler2 != null && !arrayList.Contains(mimeHandler2.Type))
					{
						arrayList.Add(mimeHandler2.Type);
					}
				}
			}
			if (data is ISerializable)
			{
				X11Dnd.MimeHandler mimeHandler3 = this.FindHandler("application/x-mono-serialized-object");
				if (mimeHandler3 != null)
				{
					arrayList.Add(mimeHandler3.Type);
				}
			}
			return (IntPtr[])arrayList.ToArray(typeof(IntPtr));
		}

		// Token: 0x04001D6E RID: 7534
		private X11Dnd.MimeHandler[] MimeHandlers = new X11Dnd.MimeHandler[]
		{
			new X11Dnd.MimeHandler("text/plain", new X11Dnd.TextConverter()),
			new X11Dnd.MimeHandler("text/plain", new X11Dnd.TextConverter(), new string[]
			{
				"System.String",
				DataFormats.Text
			}),
			new X11Dnd.MimeHandler("text/html", new X11Dnd.HtmlConverter(), new string[] { DataFormats.Html }),
			new X11Dnd.MimeHandler("text/uri-list", new X11Dnd.UriListConverter(), new string[] { DataFormats.FileDrop }),
			new X11Dnd.MimeHandler("application/x-mono-serialized-object", new X11Dnd.SerializedObjectConverter())
		};

		// Token: 0x04001D6F RID: 7535
		private static readonly IntPtr[] XdndVersion = new IntPtr[]
		{
			new IntPtr(4)
		};

		// Token: 0x04001D70 RID: 7536
		private IntPtr display;

		// Token: 0x04001D71 RID: 7537
		private X11Dnd.DragData drag_data;

		// Token: 0x04001D72 RID: 7538
		private IntPtr XdndAware;

		// Token: 0x04001D73 RID: 7539
		private IntPtr XdndSelection;

		// Token: 0x04001D74 RID: 7540
		private IntPtr XdndEnter;

		// Token: 0x04001D75 RID: 7541
		private IntPtr XdndLeave;

		// Token: 0x04001D76 RID: 7542
		private IntPtr XdndPosition;

		// Token: 0x04001D77 RID: 7543
		private IntPtr XdndDrop;

		// Token: 0x04001D78 RID: 7544
		private IntPtr XdndFinished;

		// Token: 0x04001D79 RID: 7545
		private IntPtr XdndStatus;

		// Token: 0x04001D7A RID: 7546
		private IntPtr XdndTypeList;

		// Token: 0x04001D7B RID: 7547
		private IntPtr XdndActionCopy;

		// Token: 0x04001D7C RID: 7548
		private IntPtr XdndActionMove;

		// Token: 0x04001D7D RID: 7549
		private IntPtr XdndActionLink;

		// Token: 0x04001D7E RID: 7550
		private IntPtr XdndActionList;

		// Token: 0x04001D7F RID: 7551
		private int converts_pending;

		// Token: 0x04001D80 RID: 7552
		private bool position_recieved;

		// Token: 0x04001D81 RID: 7553
		private bool status_sent;

		// Token: 0x04001D82 RID: 7554
		private IntPtr target;

		// Token: 0x04001D83 RID: 7555
		private IntPtr source;

		// Token: 0x04001D84 RID: 7556
		private IntPtr toplevel;

		// Token: 0x04001D85 RID: 7557
		private IDataObject data;

		// Token: 0x04001D86 RID: 7558
		private Control control;

		// Token: 0x04001D87 RID: 7559
		private int pos_x;

		// Token: 0x04001D88 RID: 7560
		private int pos_y;

		// Token: 0x04001D89 RID: 7561
		private DragDropEffects allowed;

		// Token: 0x04001D8A RID: 7562
		private DragEventArgs drag_event;

		// Token: 0x04001D8B RID: 7563
		private Cursor CursorNo;

		// Token: 0x04001D8C RID: 7564
		private Cursor CursorCopy;

		// Token: 0x04001D8D RID: 7565
		private Cursor CursorMove;

		// Token: 0x04001D8E RID: 7566
		private Cursor CursorLink;

		// Token: 0x04001D8F RID: 7567
		private bool tracking;

		// Token: 0x04001D90 RID: 7568
		private bool dropped;

		// Token: 0x04001D91 RID: 7569
		private int motion_poll;

		// Token: 0x020003CC RID: 972
		private enum State
		{
			// Token: 0x04001D93 RID: 7571
			Accepting,
			// Token: 0x04001D94 RID: 7572
			Dragging
		}

		// Token: 0x020003CD RID: 973
		private enum DragState
		{
			// Token: 0x04001D96 RID: 7574
			None,
			// Token: 0x04001D97 RID: 7575
			Beginning,
			// Token: 0x04001D98 RID: 7576
			Dragging,
			// Token: 0x04001D99 RID: 7577
			Entered
		}

		// Token: 0x020003CE RID: 974
		private interface IDataConverter
		{
			// Token: 0x060045C5 RID: 17861
			void GetData(X11Dnd dnd, IDataObject data, ref XEvent xevent);

			// Token: 0x060045C6 RID: 17862
			void SetData(X11Dnd dnd, object data, ref XEvent xevent);
		}

		// Token: 0x020003CF RID: 975
		private class MimeHandler
		{
			// Token: 0x060045C7 RID: 17863 RVA: 0x001115FC File Offset: 0x0010F7FC
			public MimeHandler(string name, X11Dnd.IDataConverter converter)
				: this(name, converter, new string[] { name })
			{
			}

			// Token: 0x060045C8 RID: 17864 RVA: 0x00111610 File Offset: 0x0010F810
			public MimeHandler(string name, X11Dnd.IDataConverter converter, params string[] aliases)
			{
				this.Name = name;
				this.Converter = converter;
				this.Aliases = aliases;
			}

			// Token: 0x060045C9 RID: 17865 RVA: 0x00111630 File Offset: 0x0010F830
			public override string ToString()
			{
				return "MimeHandler {" + this.Name + "}";
			}

			// Token: 0x04001D9A RID: 7578
			public string Name;

			// Token: 0x04001D9B RID: 7579
			public string[] Aliases;

			// Token: 0x04001D9C RID: 7580
			public IntPtr Type;

			// Token: 0x04001D9D RID: 7581
			public IntPtr NonProtocol;

			// Token: 0x04001D9E RID: 7582
			public X11Dnd.IDataConverter Converter;
		}

		// Token: 0x020003D0 RID: 976
		private class SerializedObjectConverter : X11Dnd.IDataConverter
		{
			// Token: 0x060045CB RID: 17867 RVA: 0x00111650 File Offset: 0x0010F850
			public void GetData(X11Dnd dnd, IDataObject data, ref XEvent xevent)
			{
				MemoryStream data2 = dnd.GetData(ref xevent);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				if (data2.Length == 0L)
				{
					return;
				}
				data2.Seek(0L, 0);
				object obj = binaryFormatter.Deserialize(data2);
				data.SetData(obj);
			}

			// Token: 0x060045CC RID: 17868 RVA: 0x00111690 File Offset: 0x0010F890
			public void SetData(X11Dnd dnd, object data, ref XEvent xevent)
			{
				if (data == null)
				{
					return;
				}
				MemoryStream memoryStream = new MemoryStream();
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, data);
				IntPtr intPtr = Marshal.AllocHGlobal((int)memoryStream.Length);
				memoryStream.Seek(0L, 0);
				int num = 0;
				while ((long)num < memoryStream.Length)
				{
					Marshal.WriteByte(intPtr, num, (byte)memoryStream.ReadByte());
					num++;
				}
				dnd.SetProperty(ref xevent, intPtr, (int)memoryStream.Length);
			}
		}

		// Token: 0x020003D1 RID: 977
		private class HtmlConverter : X11Dnd.IDataConverter
		{
			// Token: 0x060045CE RID: 17870 RVA: 0x0011170C File Offset: 0x0010F90C
			public void GetData(X11Dnd dnd, IDataObject data, ref XEvent xevent)
			{
				string text = dnd.GetText(ref xevent, false);
				if (text == null)
				{
					return;
				}
				data.SetData(DataFormats.Text, text);
				data.SetData(DataFormats.UnicodeText, text);
			}

			// Token: 0x060045CF RID: 17871 RVA: 0x00111744 File Offset: 0x0010F944
			public void SetData(X11Dnd dnd, object data, ref XEvent xevent)
			{
				string text = data as string;
				if (text == null)
				{
					return;
				}
				IntPtr intPtr;
				int num;
				if (xevent.SelectionRequestEvent.target == (IntPtr)31)
				{
					byte[] bytes = Encoding.ASCII.GetBytes(text);
					intPtr = Marshal.AllocHGlobal(bytes.Length);
					num = bytes.Length;
					for (int i = 0; i < num; i++)
					{
						Marshal.WriteByte(intPtr, i, bytes[i]);
					}
				}
				else
				{
					intPtr = Marshal.StringToHGlobalAnsi(text);
					num = 0;
					while (Marshal.ReadByte(intPtr, num) != 0)
					{
						num++;
					}
				}
				dnd.SetProperty(ref xevent, intPtr, num);
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x020003D2 RID: 978
		private class TextConverter : X11Dnd.IDataConverter
		{
			// Token: 0x060045D1 RID: 17873 RVA: 0x001117F0 File Offset: 0x0010F9F0
			public void GetData(X11Dnd dnd, IDataObject data, ref XEvent xevent)
			{
				string text = dnd.GetText(ref xevent, true);
				if (text == null)
				{
					return;
				}
				data.SetData(DataFormats.Text, text);
				data.SetData(DataFormats.UnicodeText, text);
			}

			// Token: 0x060045D2 RID: 17874 RVA: 0x00111828 File Offset: 0x0010FA28
			public void SetData(X11Dnd dnd, object data, ref XEvent xevent)
			{
				string text = data as string;
				if (text == null)
				{
					IDataObject dataObject = data as IDataObject;
					if (dataObject == null)
					{
						return;
					}
					text = (string)dataObject.GetData("System.String", true);
				}
				IntPtr intPtr;
				int num;
				if (xevent.SelectionRequestEvent.target == (IntPtr)31)
				{
					byte[] bytes = Encoding.ASCII.GetBytes(text);
					intPtr = Marshal.AllocHGlobal(bytes.Length);
					num = bytes.Length;
					for (int i = 0; i < num; i++)
					{
						Marshal.WriteByte(intPtr, i, bytes[i]);
					}
				}
				else
				{
					intPtr = Marshal.StringToHGlobalAnsi(text);
					num = 0;
					while (Marshal.ReadByte(intPtr, num) != 0)
					{
						num++;
					}
				}
				dnd.SetProperty(ref xevent, intPtr, num);
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x020003D3 RID: 979
		private class UriListConverter : X11Dnd.IDataConverter
		{
			// Token: 0x060045D4 RID: 17876 RVA: 0x001118F8 File Offset: 0x0010FAF8
			public void GetData(X11Dnd dnd, IDataObject data, ref XEvent xevent)
			{
				string text = dnd.GetText(ref xevent, false);
				if (text == null)
				{
					return;
				}
				ArrayList arrayList = new ArrayList();
				string[] array = text.Split(new char[] { '\r', '\n' });
				foreach (string text2 in array)
				{
					if (!text2.StartsWith("#"))
					{
						try
						{
							Uri uri = new Uri(text2);
							arrayList.Add(uri.LocalPath);
						}
						catch
						{
						}
					}
				}
				string[] array3 = (string[])arrayList.ToArray(typeof(string));
				if (array3.Length < 1)
				{
					return;
				}
				data.SetData(DataFormats.FileDrop, array3);
				data.SetData("FileName", array3[0]);
				data.SetData("FileNameW", array3[0]);
			}

			// Token: 0x060045D5 RID: 17877 RVA: 0x001119F4 File Offset: 0x0010FBF4
			public void SetData(X11Dnd dnd, object data, ref XEvent xevent)
			{
				string[] array = data as string[];
				if (array == null)
				{
					IDataObject dataObject = data as IDataObject;
					if (dataObject == null)
					{
						return;
					}
					array = dataObject.GetData(DataFormats.FileDrop, true) as string[];
				}
				if (array == null)
				{
					return;
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in array)
				{
					Uri uri = new Uri(text);
					stringBuilder.Append(uri.ToString());
					stringBuilder.Append("\r\n");
				}
				IntPtr intPtr = Marshal.StringToHGlobalAnsi(stringBuilder.ToString());
				int num = 0;
				while (Marshal.ReadByte(intPtr, num) != 0)
				{
					num++;
				}
				dnd.SetProperty(ref xevent, intPtr, num);
			}
		}

		// Token: 0x020003D4 RID: 980
		private class DragData
		{
			// Token: 0x060045D7 RID: 17879 RVA: 0x00111ABC File Offset: 0x0010FCBC
			public void Reset()
			{
				this.State = X11Dnd.DragState.None;
				this.Data = null;
				this.SupportedTypes = null;
				this.WillAccept = false;
			}

			// Token: 0x04001D9F RID: 7583
			public IntPtr Window;

			// Token: 0x04001DA0 RID: 7584
			public X11Dnd.DragState State;

			// Token: 0x04001DA1 RID: 7585
			public object Data;

			// Token: 0x04001DA2 RID: 7586
			public IntPtr Action;

			// Token: 0x04001DA3 RID: 7587
			public IntPtr[] SupportedTypes;

			// Token: 0x04001DA4 RID: 7588
			public MouseButtons MouseState;

			// Token: 0x04001DA5 RID: 7589
			public DragDropEffects AllowedEffects;

			// Token: 0x04001DA6 RID: 7590
			public Point CurMousePos;

			// Token: 0x04001DA7 RID: 7591
			public IntPtr LastWindow;

			// Token: 0x04001DA8 RID: 7592
			public IntPtr LastTopLevel;

			// Token: 0x04001DA9 RID: 7593
			public bool WillAccept;
		}

		// Token: 0x0200064B RID: 1611
		// (Invoke) Token: 0x060050DE RID: 20702
		private delegate void MimeConverter(IntPtr dsp, IDataObject data, ref XEvent xevent);
	}
}
