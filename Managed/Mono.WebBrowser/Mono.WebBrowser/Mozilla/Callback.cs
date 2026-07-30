using System;
using System.Runtime.InteropServices;
using Mono.Mozilla.DOM;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla
{
	// Token: 0x02000039 RID: 57
	internal class Callback
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x00002A9C File Offset: 0x00000C9C
		public Callback(WebBrowser owner)
		{
			this.owner = owner;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00002AAB File Offset: 0x00000CAB
		public void OnWidgetLoaded()
		{
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public void OnStateChange(nsIWebProgress progress, nsIRequest request, int status, uint state)
		{
			if (!this.owner.created)
			{
				this.owner.created = true;
			}
			bool flag = (state & 1U) > 0U;
			bool flag2 = (state & 4U) > 0U;
			bool flag3 = (state & 2U) > 0U;
			bool flag4 = (state & 16U) > 0U;
			bool flag5 = (state & 65536U) > 0U;
			bool flag6 = (state & 131072U) > 0U;
			bool flag7 = (state & 262144U) > 0U;
			bool flag8 = (state & 524288U) > 0U;
			if (flag && flag5 && flag6 && !this.calledLoadStarted)
			{
				nsIDOMWindow nsIDOMWindow;
				progress.getDOMWindow(out nsIDOMWindow);
				nsIURI nsIURI;
				((nsIChannel)request).getURI(out nsIURI);
				if (nsIURI == null)
				{
					this.currentUri = "about:blank";
				}
				else
				{
					AsciiString asciiString = new AsciiString(string.Empty);
					nsIURI.getSpec(asciiString.Handle);
					this.currentUri = asciiString.ToString();
				}
				this.calledLoadStarted = true;
				LoadStartedEventHandler loadStartedEventHandler = (LoadStartedEventHandler)this.owner.Events[WebBrowser.LoadStartedEvent];
				if (loadStartedEventHandler != null)
				{
					AsciiString asciiString2 = new AsciiString(string.Empty);
					nsIDOMWindow.getName(asciiString2.Handle);
					LoadStartedEventArgs loadStartedEventArgs = new LoadStartedEventArgs(this.currentUri, asciiString2.ToString());
					loadStartedEventHandler(this, loadStartedEventArgs);
					if (loadStartedEventArgs.Cancel)
					{
						request.cancel(2152398850U);
					}
				}
				return;
			}
			if (flag6 && flag5 && flag2)
			{
				nsIDOMWindow nsIDOMWindow2;
				progress.getDOMWindow(out nsIDOMWindow2);
				nsIURI nsIURI2;
				((nsIChannel)request).getURI(out nsIURI2);
				if (nsIURI2 == null)
				{
					this.currentUri = "about:blank";
				}
				else
				{
					AsciiString asciiString3 = new AsciiString(string.Empty);
					nsIURI2.getSpec(asciiString3.Handle);
					this.currentUri = asciiString3.ToString();
				}
				nsIDOMWindow nsIDOMWindow3;
				nsIDOMWindow2.getTop(out nsIDOMWindow3);
				if (nsIDOMWindow3 == null || nsIDOMWindow3.GetHashCode() == nsIDOMWindow2.GetHashCode())
				{
					this.owner.Reset();
					nsIDOMDocument nsIDOMDocument;
					nsIDOMWindow2.getDocument(out nsIDOMDocument);
					if (nsIDOMDocument != null)
					{
						this.owner.document = new Document(this.owner, nsIDOMDocument);
					}
				}
				LoadCommitedEventHandler loadCommitedEventHandler = (LoadCommitedEventHandler)this.owner.Events[WebBrowser.LoadCommitedEvent];
				if (loadCommitedEventHandler != null)
				{
					LoadCommitedEventArgs loadCommitedEventArgs = new LoadCommitedEventArgs(this.currentUri);
					loadCommitedEventHandler(this, loadCommitedEventArgs);
				}
				return;
			}
			if (flag6 && flag5 && flag3)
			{
				nsIDOMWindow nsIDOMWindow4;
				progress.getDOMWindow(out nsIDOMWindow4);
				nsIURI nsIURI3;
				((nsIChannel)request).getURI(out nsIURI3);
				if (nsIURI3 == null)
				{
					this.currentUri = "about:blank";
					return;
				}
				AsciiString asciiString4 = new AsciiString(string.Empty);
				nsIURI3.getSpec(asciiString4.Handle);
				this.currentUri = asciiString4.ToString();
				return;
			}
			else
			{
				if (flag4 && !flag5 && !flag6 && flag7 && flag8)
				{
					this.calledLoadStarted = false;
					LoadFinishedEventHandler loadFinishedEventHandler = (LoadFinishedEventHandler)this.owner.Events[WebBrowser.LoadFinishedEvent];
					if (loadFinishedEventHandler != null)
					{
						nsIDOMWindow nsIDOMWindow5;
						progress.getDOMWindow(out nsIDOMWindow5);
						LoadFinishedEventArgs loadFinishedEventArgs = new LoadFinishedEventArgs(this.currentUri);
						loadFinishedEventHandler(this, loadFinishedEventArgs);
					}
					return;
				}
				if (flag4 && !flag5 && flag6 && !flag7 && !flag8)
				{
					nsIDOMWindow nsIDOMWindow6;
					progress.getDOMWindow(out nsIDOMWindow6);
					nsIDOMDocument nsIDOMDocument2;
					nsIDOMWindow6.getDocument(out nsIDOMDocument2);
					if (nsIDOMDocument2 != null)
					{
						int hashCode = nsIDOMDocument2.GetHashCode();
						if (this.owner.documents.ContainsKey(hashCode))
						{
							EventHandler eventHandler = (EventHandler)(this.owner.documents[hashCode] as Document).Events[Document.LoadStoppedEvent];
							if (eventHandler != null)
							{
								eventHandler(this, null);
							}
						}
					}
					this.calledLoadStarted = false;
					return;
				}
				return;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00002E3C File Offset: 0x0000103C
		public void OnProgress(nsIWebProgress progress, nsIRequest request, int currentTotalProgress, int maxTotalProgress)
		{
			ProgressChangedEventHandler progressChangedEventHandler = (ProgressChangedEventHandler)this.owner.Events[WebBrowser.ProgressChangedEvent];
			if (progressChangedEventHandler != null)
			{
				ProgressChangedEventArgs progressChangedEventArgs = new ProgressChangedEventArgs(currentTotalProgress, maxTotalProgress);
				progressChangedEventHandler(this, progressChangedEventArgs);
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00002E78 File Offset: 0x00001078
		public void OnLocationChanged(nsIWebProgress progress, nsIRequest request, nsIURI uri)
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00002E7C File Offset: 0x0000107C
		public void OnStatusChange(nsIWebProgress progress, nsIRequest request, string message, int status)
		{
			StatusChangedEventHandler statusChangedEventHandler = (StatusChangedEventHandler)this.owner.Events[WebBrowser.StatusChangedEvent];
			if (statusChangedEventHandler != null)
			{
				StatusChangedEventArgs statusChangedEventArgs = new StatusChangedEventArgs(message, status);
				statusChangedEventHandler(this, statusChangedEventArgs);
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00002EB8 File Offset: 0x000010B8
		public void OnSecurityChange(nsIWebProgress progress, nsIRequest request, uint status)
		{
			SecurityChangedEventHandler securityChangedEventHandler = (SecurityChangedEventHandler)this.owner.Events[WebBrowser.SecurityChangedEvent];
			if (securityChangedEventHandler != null)
			{
				SecurityLevel securityLevel = SecurityLevel.Insecure;
				switch (status)
				{
				case 1U:
					securityLevel = SecurityLevel.Mixed;
					break;
				case 2U:
					securityLevel = SecurityLevel.Secure;
					break;
				case 4U:
					securityLevel = SecurityLevel.Insecure;
					break;
				}
				SecurityChangedEventArgs securityChangedEventArgs = new SecurityChangedEventArgs(securityLevel);
				securityChangedEventHandler(this, securityChangedEventArgs);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00002F18 File Offset: 0x00001118
		public bool OnClientDomKeyDown(KeyInfo keyInfo, ModifierKeys modifiers, nsIDOMNode target)
		{
			INode node = new Node(this.owner, target);
			string text = string.Intern(node.GetHashCode() + ":keydown");
			EventHandler eventHandler = (EventHandler)this.owner.DomEvents[text];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(node, eventArgs);
			}
			NodeEventHandler nodeEventHandler = (NodeEventHandler)this.owner.Events[WebBrowser.KeyDownEvent];
			if (nodeEventHandler != null)
			{
				NodeEventArgs nodeEventArgs = new NodeEventArgs(node);
				nodeEventHandler(this, nodeEventArgs);
				return true;
			}
			return false;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00002FAC File Offset: 0x000011AC
		public bool OnClientDomKeyUp(KeyInfo keyInfo, ModifierKeys modifiers, nsIDOMNode target)
		{
			INode node = new Node(this.owner, target);
			string text = string.Intern(node.GetHashCode() + ":keyup");
			EventHandler eventHandler = (EventHandler)this.owner.DomEvents[text];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(node, eventArgs);
			}
			NodeEventHandler nodeEventHandler = (NodeEventHandler)this.owner.Events[WebBrowser.KeyUpEvent];
			if (nodeEventHandler != null)
			{
				NodeEventArgs nodeEventArgs = new NodeEventArgs(node);
				nodeEventHandler(this, nodeEventArgs);
				return true;
			}
			return false;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00003040 File Offset: 0x00001240
		public bool OnClientDomKeyPress(KeyInfo keyInfo, ModifierKeys modifiers, nsIDOMNode target)
		{
			INode node = new Node(this.owner, target);
			string text = string.Intern(node.GetHashCode() + ":keypress");
			EventHandler eventHandler = (EventHandler)this.owner.DomEvents[text];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(node, eventArgs);
			}
			NodeEventHandler nodeEventHandler = (NodeEventHandler)this.owner.Events[WebBrowser.KeyPressEvent];
			if (nodeEventHandler != null)
			{
				NodeEventArgs nodeEventArgs = new NodeEventArgs(node);
				nodeEventHandler(this, nodeEventArgs);
				return true;
			}
			return false;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000030D4 File Offset: 0x000012D4
		public bool OnClientMouseDown(MouseInfo mouseInfo, ModifierKeys modifiers, nsIDOMNode target)
		{
			INode node = new Node(this.owner, target);
			string text = string.Intern(node.GetHashCode() + ":mousedown");
			EventHandler eventHandler = (EventHandler)this.owner.DomEvents[text];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(node, eventArgs);
			}
			NodeEventHandler nodeEventHandler = (NodeEventHandler)this.owner.Events[WebBrowser.MouseDownEvent];
			if (nodeEventHandler != null)
			{
				NodeEventArgs nodeEventArgs = new NodeEventArgs(node);
				nodeEventHandler(this, nodeEventArgs);
				return true;
			}
			return false;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00003168 File Offset: 0x00001368
		public bool OnClientMouseUp(MouseInfo mouseInfo, ModifierKeys modifiers, nsIDOMNode target)
		{
			INode node = new Node(this.owner, target);
			string text = string.Intern(node.GetHashCode() + ":mouseup");
			EventHandler eventHandler = (EventHandler)this.owner.DomEvents[text];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(node, eventArgs);
			}
			NodeEventHandler nodeEventHandler = (NodeEventHandler)this.owner.Events[WebBrowser.MouseUpEvent];
			if (nodeEventHandler != null)
			{
				NodeEventArgs nodeEventArgs = new NodeEventArgs(node);
				nodeEventHandler(this, nodeEventArgs);
				return true;
			}
			return false;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000031FC File Offset: 0x000013FC
		public bool OnClientMouseClick(MouseInfo mouseInfo, ModifierKeys modifiers, nsIDOMNode target)
		{
			INode node = new Node(this.owner, target);
			string text = string.Intern(node.GetHashCode() + ":click");
			EventHandler eventHandler = (EventHandler)this.owner.DomEvents[text];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(node, eventArgs);
			}
			NodeEventHandler nodeEventHandler = (NodeEventHandler)this.owner.Events[WebBrowser.MouseClickEvent];
			if (nodeEventHandler != null)
			{
				NodeEventArgs nodeEventArgs = new NodeEventArgs(node);
				nodeEventHandler(this, nodeEventArgs);
				return true;
			}
			return false;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00003290 File Offset: 0x00001490
		public bool OnClientMouseDoubleClick(MouseInfo mouseInfo, ModifierKeys modifiers, nsIDOMNode target)
		{
			INode node = new Node(this.owner, target);
			string text = string.Intern(node.GetHashCode() + ":dblclick");
			EventHandler eventHandler = (EventHandler)this.owner.DomEvents[text];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(node, eventArgs);
			}
			NodeEventHandler nodeEventHandler = (NodeEventHandler)this.owner.Events[WebBrowser.MouseDoubleClickEvent];
			if (nodeEventHandler != null)
			{
				NodeEventArgs nodeEventArgs = new NodeEventArgs(node);
				nodeEventHandler(this, nodeEventArgs);
				return true;
			}
			return false;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00003324 File Offset: 0x00001524
		public bool OnClientMouseOver(MouseInfo mouseInfo, ModifierKeys modifiers, nsIDOMNode target)
		{
			INode typedNode = new DOMObject(this.owner).GetTypedNode(target);
			string text = string.Intern(typedNode.GetHashCode() + ":mouseover");
			EventHandler eventHandler = (EventHandler)this.owner.DomEvents[text];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(typedNode, eventArgs);
			}
			NodeEventHandler nodeEventHandler = (NodeEventHandler)this.owner.Events[WebBrowser.MouseEnterEvent];
			if (nodeEventHandler != null)
			{
				NodeEventArgs nodeEventArgs = new NodeEventArgs(typedNode);
				nodeEventHandler(typedNode, nodeEventArgs);
				return true;
			}
			return false;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000033BC File Offset: 0x000015BC
		public bool OnClientMouseOut(MouseInfo mouseInfo, ModifierKeys modifiers, nsIDOMNode target)
		{
			INode node = new Node(this.owner, target);
			string text = string.Intern(node.GetHashCode() + ":mouseout");
			EventHandler eventHandler = (EventHandler)this.owner.DomEvents[text];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(node, eventArgs);
			}
			NodeEventHandler nodeEventHandler = (NodeEventHandler)this.owner.Events[WebBrowser.MouseLeaveEvent];
			if (nodeEventHandler != null)
			{
				NodeEventArgs nodeEventArgs = new NodeEventArgs(node);
				nodeEventHandler(this, nodeEventArgs);
				return true;
			}
			return false;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000344D File Offset: 0x0000164D
		public bool OnClientActivate()
		{
			return false;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00003450 File Offset: 0x00001650
		public bool OnClientFocus()
		{
			EventHandler eventHandler = (EventHandler)this.owner.Events[WebBrowser.FocusEvent];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(this, eventArgs);
			}
			return false;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000348C File Offset: 0x0000168C
		public bool OnClientBlur()
		{
			EventHandler eventHandler = (EventHandler)this.owner.Events[WebBrowser.BlurEvent];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(this, eventArgs);
			}
			return false;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000034C8 File Offset: 0x000016C8
		public bool OnCreateNewWindow()
		{
			bool flag = false;
			CreateNewWindowEventHandler createNewWindowEventHandler = (CreateNewWindowEventHandler)this.owner.Events[WebBrowser.CreateNewWindowEvent];
			if (createNewWindowEventHandler != null)
			{
				CreateNewWindowEventArgs createNewWindowEventArgs = new CreateNewWindowEventArgs(false);
				flag = createNewWindowEventHandler(this, createNewWindowEventArgs);
			}
			return flag;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00003508 File Offset: 0x00001708
		public void OnAlert(IntPtr title, IntPtr text)
		{
			AlertEventHandler alertEventHandler = (AlertEventHandler)this.owner.Events[WebBrowser.AlertEvent];
			if (alertEventHandler != null)
			{
				AlertEventArgs alertEventArgs = new AlertEventArgs();
				alertEventArgs.Type = DialogType.Alert;
				if (title != IntPtr.Zero)
				{
					alertEventArgs.Title = Marshal.PtrToStringUni(title);
				}
				if (text != IntPtr.Zero)
				{
					alertEventArgs.Text = Marshal.PtrToStringUni(text);
				}
				alertEventHandler(this, alertEventArgs);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000357C File Offset: 0x0000177C
		public bool OnAlertCheck(IntPtr title, IntPtr text, IntPtr chkMsg, ref bool chkState)
		{
			AlertEventHandler alertEventHandler = (AlertEventHandler)this.owner.Events[WebBrowser.AlertEvent];
			if (alertEventHandler != null)
			{
				AlertEventArgs alertEventArgs = new AlertEventArgs();
				alertEventArgs.Type = DialogType.AlertCheck;
				if (title != IntPtr.Zero)
				{
					alertEventArgs.Title = Marshal.PtrToStringUni(title);
				}
				if (text != IntPtr.Zero)
				{
					alertEventArgs.Text = Marshal.PtrToStringUni(text);
				}
				if (chkMsg != IntPtr.Zero)
				{
					alertEventArgs.CheckMessage = Marshal.PtrToStringUni(chkMsg);
				}
				alertEventArgs.CheckState = chkState;
				alertEventHandler(this, alertEventArgs);
				return alertEventArgs.BoolReturn;
			}
			return false;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00003618 File Offset: 0x00001818
		public bool OnConfirm(IntPtr title, IntPtr text)
		{
			AlertEventHandler alertEventHandler = (AlertEventHandler)this.owner.Events[WebBrowser.AlertEvent];
			if (alertEventHandler != null)
			{
				AlertEventArgs alertEventArgs = new AlertEventArgs();
				alertEventArgs.Type = DialogType.Confirm;
				if (title != IntPtr.Zero)
				{
					alertEventArgs.Title = Marshal.PtrToStringUni(title);
				}
				if (text != IntPtr.Zero)
				{
					alertEventArgs.Text = Marshal.PtrToStringUni(text);
				}
				alertEventHandler(this, alertEventArgs);
				return alertEventArgs.BoolReturn;
			}
			return false;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00003694 File Offset: 0x00001894
		public bool OnConfirmCheck(IntPtr title, IntPtr text, IntPtr chkMsg, ref bool chkState)
		{
			AlertEventHandler alertEventHandler = (AlertEventHandler)this.owner.Events[WebBrowser.AlertEvent];
			if (alertEventHandler != null)
			{
				AlertEventArgs alertEventArgs = new AlertEventArgs();
				alertEventArgs.Type = DialogType.ConfirmCheck;
				if (title != IntPtr.Zero)
				{
					alertEventArgs.Title = Marshal.PtrToStringUni(title);
				}
				if (text != IntPtr.Zero)
				{
					alertEventArgs.Text = Marshal.PtrToStringUni(text);
				}
				if (chkMsg != IntPtr.Zero)
				{
					alertEventArgs.CheckMessage = Marshal.PtrToStringUni(chkMsg);
				}
				alertEventArgs.CheckState = chkState;
				alertEventHandler(this, alertEventArgs);
				chkState = alertEventArgs.CheckState;
				return alertEventArgs.BoolReturn;
			}
			return false;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000373C File Offset: 0x0000193C
		public bool OnConfirmEx(IntPtr title, IntPtr text, DialogButtonFlags flags, IntPtr title0, IntPtr title1, IntPtr title2, IntPtr chkMsg, ref bool chkState, out int retVal)
		{
			retVal = -1;
			AlertEventHandler alertEventHandler = (AlertEventHandler)this.owner.Events[WebBrowser.AlertEvent];
			if (alertEventHandler != null)
			{
				AlertEventArgs alertEventArgs = new AlertEventArgs();
				alertEventArgs.Type = DialogType.ConfirmEx;
				if (title != IntPtr.Zero)
				{
					alertEventArgs.Title = Marshal.PtrToStringUni(title);
				}
				if (text != IntPtr.Zero)
				{
					alertEventArgs.Text = Marshal.PtrToStringUni(text);
				}
				if (chkMsg != IntPtr.Zero)
				{
					alertEventArgs.CheckMessage = Marshal.PtrToStringUni(chkMsg);
				}
				alertEventArgs.CheckState = chkState;
				alertEventHandler(this, alertEventArgs);
				chkState = alertEventArgs.CheckState;
				return alertEventArgs.BoolReturn;
			}
			return false;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000037E8 File Offset: 0x000019E8
		public bool OnPrompt(IntPtr title, IntPtr text, ref IntPtr retVal)
		{
			AlertEventHandler alertEventHandler = (AlertEventHandler)this.owner.Events[WebBrowser.AlertEvent];
			if (alertEventHandler != null)
			{
				AlertEventArgs alertEventArgs = new AlertEventArgs();
				alertEventArgs.Type = DialogType.Prompt;
				if (title != IntPtr.Zero)
				{
					alertEventArgs.Title = Marshal.PtrToStringUni(title);
				}
				if (text != IntPtr.Zero)
				{
					alertEventArgs.Text = Marshal.PtrToStringUni(text);
				}
				if (retVal != IntPtr.Zero)
				{
					alertEventArgs.Text2 = Marshal.PtrToStringUni(retVal);
				}
				alertEventHandler(this, alertEventArgs);
				retVal = Marshal.StringToHGlobalUni(alertEventArgs.StringReturn);
				return alertEventArgs.BoolReturn;
			}
			return false;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000388C File Offset: 0x00001A8C
		public bool OnPromptUsernameAndPassword(IntPtr title, IntPtr text, IntPtr chkMsg, ref bool chkState, out IntPtr username, out IntPtr password)
		{
			username = IntPtr.Zero;
			password = IntPtr.Zero;
			AlertEventHandler alertEventHandler = (AlertEventHandler)this.owner.Events[WebBrowser.AlertEvent];
			if (alertEventHandler != null)
			{
				AlertEventArgs alertEventArgs = new AlertEventArgs();
				alertEventArgs.Type = DialogType.PromptUsernamePassword;
				if (title != IntPtr.Zero)
				{
					alertEventArgs.Title = Marshal.PtrToStringUni(title);
				}
				if (text != IntPtr.Zero)
				{
					alertEventArgs.Text = Marshal.PtrToStringUni(text);
				}
				if (chkMsg != IntPtr.Zero)
				{
					alertEventArgs.CheckMessage = Marshal.PtrToStringUni(chkMsg);
				}
				alertEventArgs.CheckState = chkState;
				alertEventHandler(this, alertEventArgs);
				return alertEventArgs.BoolReturn;
			}
			return false;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00003938 File Offset: 0x00001B38
		public bool OnPromptPassword(IntPtr title, IntPtr text, IntPtr chkMsg, ref bool chkState, out IntPtr password)
		{
			password = IntPtr.Zero;
			AlertEventHandler alertEventHandler = (AlertEventHandler)this.owner.Events[WebBrowser.AlertEvent];
			if (alertEventHandler != null)
			{
				AlertEventArgs alertEventArgs = new AlertEventArgs();
				alertEventArgs.Type = DialogType.PromptPassword;
				if (title != IntPtr.Zero)
				{
					alertEventArgs.Title = Marshal.PtrToStringUni(title);
				}
				if (text != IntPtr.Zero)
				{
					alertEventArgs.Text = Marshal.PtrToStringUni(text);
				}
				if (chkMsg != IntPtr.Zero)
				{
					alertEventArgs.CheckMessage = Marshal.PtrToStringUni(chkMsg);
				}
				alertEventArgs.CheckState = chkState;
				alertEventHandler(this, alertEventArgs);
				return alertEventArgs.BoolReturn;
			}
			return false;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000039DC File Offset: 0x00001BDC
		public bool OnSelect(IntPtr title, IntPtr text, uint count, IntPtr list, out int retVal)
		{
			retVal = 0;
			AlertEventHandler alertEventHandler = (AlertEventHandler)this.owner.Events[WebBrowser.AlertEvent];
			if (alertEventHandler != null)
			{
				AlertEventArgs alertEventArgs = new AlertEventArgs();
				alertEventArgs.Type = DialogType.Select;
				if (title != IntPtr.Zero)
				{
					alertEventArgs.Title = Marshal.PtrToStringUni(title);
				}
				if (text != IntPtr.Zero)
				{
					alertEventArgs.Text = Marshal.PtrToStringUni(text);
				}
				alertEventHandler(this, alertEventArgs);
				return alertEventArgs.BoolReturn;
			}
			return false;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00003A5B File Offset: 0x00001C5B
		public void OnLoad()
		{
			((Window)this.owner.Window).OnLoad();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00003A72 File Offset: 0x00001C72
		public void OnUnload()
		{
			((Window)this.owner.Window).OnUnload();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00003A8C File Offset: 0x00001C8C
		public void OnShowContextMenu(uint contextFlags, [MarshalAs(UnmanagedType.Interface)] nsIDOMEvent eve, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode node)
		{
			ContextMenuEventHandler contextMenuEventHandler = (ContextMenuEventHandler)this.owner.Events[WebBrowser.ContextMenuEvent];
			if (contextMenuEventHandler != null)
			{
				nsIDOMMouseEvent nsIDOMMouseEvent = (nsIDOMMouseEvent)eve;
				int num;
				nsIDOMMouseEvent.getClientX(out num);
				int num2;
				nsIDOMMouseEvent.getClientY(out num2);
				ContextMenuEventArgs contextMenuEventArgs = new ContextMenuEventArgs(num, num2);
				contextMenuEventHandler(this.owner, contextMenuEventArgs);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public void OnGeneric(string type)
		{
			EventHandler eventHandler = (EventHandler)this.owner.Events[WebBrowser.GenericEvent];
			if (eventHandler != null)
			{
				EventArgs eventArgs = new EventArgs();
				eventHandler(type, eventArgs);
				return;
			}
		}

		// Token: 0x04000091 RID: 145
		private WebBrowser owner;

		// Token: 0x04000092 RID: 146
		private string currentUri;

		// Token: 0x04000093 RID: 147
		private bool calledLoadStarted;
	}
}
