using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.WebBrowserDialogs;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace System.Windows.Forms
{
	/// <summary>Provides a wrapper for a generic ActiveX control for use as a base class by the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	// Token: 0x020003AB RID: 939
	[DefaultEvent("Enter")]
	[Designer("System.Windows.Forms.Design.AxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(1)]
	[ComVisible(true)]
	[DefaultProperty("Name")]
	public class WebBrowserBase : Control
	{
		// Token: 0x06004490 RID: 17552 RVA: 0x0010CFC0 File Offset: 0x0010B1C0
		internal WebBrowserBase()
		{
			this.webHost = Manager.GetNewInstance();
			if (!this.webHost.Load(this.Handle, base.Width, base.Height))
			{
				return;
			}
			this.state = WebBrowserBase.State.Loaded;
			this.webHost.MouseClick += new NodeEventHandler(this.OnWebHostMouseClick);
			this.webHost.Focus += new EventHandler(this.OnWebHostFocus);
			this.webHost.CreateNewWindow += new CreateNewWindowEventHandler(this.OnWebHostCreateNewWindow);
			this.webHost.LoadStarted += new LoadStartedEventHandler(this.OnWebHostLoadStarted);
			this.webHost.LoadCommited += new LoadCommitedEventHandler(this.OnWebHostLoadCommited);
			this.webHost.ProgressChanged += new ProgressChangedEventHandler(this.OnWebHostProgressChanged);
			this.webHost.LoadFinished += new LoadFinishedEventHandler(this.OnWebHostLoadFinished);
			if (!this.suppressDialogs)
			{
				this.webHost.Alert += new AlertEventHandler(this.OnWebHostAlert);
			}
			this.webHost.StatusChanged += new StatusChangedEventHandler(this.OnWebHostStatusChanged);
			this.webHost.SecurityChanged += new SecurityChangedEventHandler(this.OnWebHostSecurityChanged);
			this.webHost.ContextMenuShown += new ContextMenuEventHandler(this.OnWebHostContextMenuShown);
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000446 RID: 1094
		// (add) Token: 0x06004491 RID: 17553 RVA: 0x0010D11C File Offset: 0x0010B31C
		// (remove) Token: 0x06004492 RID: 17554 RVA: 0x0010D128 File Offset: 0x0010B328
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for BackColorChanged");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000447 RID: 1095
		// (add) Token: 0x06004493 RID: 17555 RVA: 0x0010D12C File Offset: 0x0010B32C
		// (remove) Token: 0x06004494 RID: 17556 RVA: 0x0010D138 File Offset: 0x0010B338
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for BackgroundImageChanged");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000448 RID: 1096
		// (add) Token: 0x06004495 RID: 17557 RVA: 0x0010D13C File Offset: 0x0010B33C
		// (remove) Token: 0x06004496 RID: 17558 RVA: 0x0010D148 File Offset: 0x0010B348
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for BackgroundImageLayoutChanged");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000449 RID: 1097
		// (add) Token: 0x06004497 RID: 17559 RVA: 0x0010D14C File Offset: 0x0010B34C
		// (remove) Token: 0x06004498 RID: 17560 RVA: 0x0010D158 File Offset: 0x0010B358
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler BindingContextChanged
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for BindingContextChanged");
			}
			remove
			{
			}
		}

		/// <summary>Occurs when the focus or keyboard user interface (UI) cues change.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400044A RID: 1098
		// (add) Token: 0x06004499 RID: 17561 RVA: 0x0010D15C File Offset: 0x0010B35C
		// (remove) Token: 0x0600449A RID: 17562 RVA: 0x0010D168 File Offset: 0x0010B368
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event UICuesEventHandler ChangeUICues
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for ChangeUICues");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400044B RID: 1099
		// (add) Token: 0x0600449B RID: 17563 RVA: 0x0010D16C File Offset: 0x0010B36C
		// (remove) Token: 0x0600449C RID: 17564 RVA: 0x0010D178 File Offset: 0x0010B378
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler Click
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for Click");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400044C RID: 1100
		// (add) Token: 0x0600449D RID: 17565 RVA: 0x0010D17C File Offset: 0x0010B37C
		// (remove) Token: 0x0600449E RID: 17566 RVA: 0x0010D188 File Offset: 0x0010B388
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler CursorChanged
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for CursorChanged");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400044D RID: 1101
		// (add) Token: 0x0600449F RID: 17567 RVA: 0x0010D18C File Offset: 0x0010B38C
		// (remove) Token: 0x060044A0 RID: 17568 RVA: 0x0010D198 File Offset: 0x0010B398
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler DoubleClick
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for DoubleClick");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400044E RID: 1102
		// (add) Token: 0x060044A1 RID: 17569 RVA: 0x0010D19C File Offset: 0x0010B39C
		// (remove) Token: 0x060044A2 RID: 17570 RVA: 0x0010D1A8 File Offset: 0x0010B3A8
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event DragEventHandler DragDrop
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for DragDrop");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400044F RID: 1103
		// (add) Token: 0x060044A3 RID: 17571 RVA: 0x0010D1AC File Offset: 0x0010B3AC
		// (remove) Token: 0x060044A4 RID: 17572 RVA: 0x0010D1B8 File Offset: 0x0010B3B8
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event DragEventHandler DragEnter
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for DragEnter");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000450 RID: 1104
		// (add) Token: 0x060044A5 RID: 17573 RVA: 0x0010D1BC File Offset: 0x0010B3BC
		// (remove) Token: 0x060044A6 RID: 17574 RVA: 0x0010D1C8 File Offset: 0x0010B3C8
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler DragLeave
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for DragLeave");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000451 RID: 1105
		// (add) Token: 0x060044A7 RID: 17575 RVA: 0x0010D1CC File Offset: 0x0010B3CC
		// (remove) Token: 0x060044A8 RID: 17576 RVA: 0x0010D1D8 File Offset: 0x0010B3D8
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event DragEventHandler DragOver
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for DragOver");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000452 RID: 1106
		// (add) Token: 0x060044A9 RID: 17577 RVA: 0x0010D1DC File Offset: 0x0010B3DC
		// (remove) Token: 0x060044AA RID: 17578 RVA: 0x0010D1E8 File Offset: 0x0010B3E8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for EnabledChanged");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000453 RID: 1107
		// (add) Token: 0x060044AB RID: 17579 RVA: 0x0010D1EC File Offset: 0x0010B3EC
		// (remove) Token: 0x060044AC RID: 17580 RVA: 0x0010D1F8 File Offset: 0x0010B3F8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler Enter
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for Enter");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000454 RID: 1108
		// (add) Token: 0x060044AD RID: 17581 RVA: 0x0010D1FC File Offset: 0x0010B3FC
		// (remove) Token: 0x060044AE RID: 17582 RVA: 0x0010D208 File Offset: 0x0010B408
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler FontChanged
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for FontChanged");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000455 RID: 1109
		// (add) Token: 0x060044AF RID: 17583 RVA: 0x0010D20C File Offset: 0x0010B40C
		// (remove) Token: 0x060044B0 RID: 17584 RVA: 0x0010D218 File Offset: 0x0010B418
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for ForeColorChanged");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000456 RID: 1110
		// (add) Token: 0x060044B1 RID: 17585 RVA: 0x0010D21C File Offset: 0x0010B41C
		// (remove) Token: 0x060044B2 RID: 17586 RVA: 0x0010D228 File Offset: 0x0010B428
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for GiveFeedback");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000457 RID: 1111
		// (add) Token: 0x060044B3 RID: 17587 RVA: 0x0010D22C File Offset: 0x0010B42C
		// (remove) Token: 0x060044B4 RID: 17588 RVA: 0x0010D238 File Offset: 0x0010B438
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event HelpEventHandler HelpRequested
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for HelpRequested");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000458 RID: 1112
		// (add) Token: 0x060044B5 RID: 17589 RVA: 0x0010D23C File Offset: 0x0010B43C
		// (remove) Token: 0x060044B6 RID: 17590 RVA: 0x0010D248 File Offset: 0x0010B448
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for ImeModeChanged");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000459 RID: 1113
		// (add) Token: 0x060044B7 RID: 17591 RVA: 0x0010D24C File Offset: 0x0010B44C
		// (remove) Token: 0x060044B8 RID: 17592 RVA: 0x0010D258 File Offset: 0x0010B458
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for KeyDown");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400045A RID: 1114
		// (add) Token: 0x060044B9 RID: 17593 RVA: 0x0010D25C File Offset: 0x0010B45C
		// (remove) Token: 0x060044BA RID: 17594 RVA: 0x0010D268 File Offset: 0x0010B468
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for KeyPress");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400045B RID: 1115
		// (add) Token: 0x060044BB RID: 17595 RVA: 0x0010D26C File Offset: 0x0010B46C
		// (remove) Token: 0x060044BC RID: 17596 RVA: 0x0010D278 File Offset: 0x0010B478
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for KeyUp");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400045C RID: 1116
		// (add) Token: 0x060044BD RID: 17597 RVA: 0x0010D27C File Offset: 0x0010B47C
		// (remove) Token: 0x060044BE RID: 17598 RVA: 0x0010D288 File Offset: 0x0010B488
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event LayoutEventHandler Layout
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for Layout");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400045D RID: 1117
		// (add) Token: 0x060044BF RID: 17599 RVA: 0x0010D28C File Offset: 0x0010B48C
		// (remove) Token: 0x060044C0 RID: 17600 RVA: 0x0010D298 File Offset: 0x0010B498
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler Leave
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for Leave");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400045E RID: 1118
		// (add) Token: 0x060044C1 RID: 17601 RVA: 0x0010D29C File Offset: 0x0010B49C
		// (remove) Token: 0x060044C2 RID: 17602 RVA: 0x0010D2A8 File Offset: 0x0010B4A8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler MouseCaptureChanged
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for MouseCaptureChanged");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400045F RID: 1119
		// (add) Token: 0x060044C3 RID: 17603 RVA: 0x0010D2AC File Offset: 0x0010B4AC
		// (remove) Token: 0x060044C4 RID: 17604 RVA: 0x0010D2B8 File Offset: 0x0010B4B8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event MouseEventHandler MouseClick
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for MouseClick");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000460 RID: 1120
		// (add) Token: 0x060044C5 RID: 17605 RVA: 0x0010D2BC File Offset: 0x0010B4BC
		// (remove) Token: 0x060044C6 RID: 17606 RVA: 0x0010D2C8 File Offset: 0x0010B4C8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event MouseEventHandler MouseDoubleClick
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for MouseDoubleClick");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000461 RID: 1121
		// (add) Token: 0x060044C7 RID: 17607 RVA: 0x0010D2CC File Offset: 0x0010B4CC
		// (remove) Token: 0x060044C8 RID: 17608 RVA: 0x0010D2D8 File Offset: 0x0010B4D8
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event MouseEventHandler MouseDown
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for MouseDown");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000462 RID: 1122
		// (add) Token: 0x060044C9 RID: 17609 RVA: 0x0010D2DC File Offset: 0x0010B4DC
		// (remove) Token: 0x060044CA RID: 17610 RVA: 0x0010D2E8 File Offset: 0x0010B4E8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler MouseEnter
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for MouseEnter");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000463 RID: 1123
		// (add) Token: 0x060044CB RID: 17611 RVA: 0x0010D2EC File Offset: 0x0010B4EC
		// (remove) Token: 0x060044CC RID: 17612 RVA: 0x0010D2F8 File Offset: 0x0010B4F8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler MouseHover
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for MouseHover");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000464 RID: 1124
		// (add) Token: 0x060044CD RID: 17613 RVA: 0x0010D2FC File Offset: 0x0010B4FC
		// (remove) Token: 0x060044CE RID: 17614 RVA: 0x0010D308 File Offset: 0x0010B508
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler MouseLeave
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for MouseLeave");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000465 RID: 1125
		// (add) Token: 0x060044CF RID: 17615 RVA: 0x0010D30C File Offset: 0x0010B50C
		// (remove) Token: 0x060044D0 RID: 17616 RVA: 0x0010D318 File Offset: 0x0010B518
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for MouseMove");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000466 RID: 1126
		// (add) Token: 0x060044D1 RID: 17617 RVA: 0x0010D31C File Offset: 0x0010B51C
		// (remove) Token: 0x060044D2 RID: 17618 RVA: 0x0010D328 File Offset: 0x0010B528
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event MouseEventHandler MouseUp
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for MouseUp");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000467 RID: 1127
		// (add) Token: 0x060044D3 RID: 17619 RVA: 0x0010D32C File Offset: 0x0010B52C
		// (remove) Token: 0x060044D4 RID: 17620 RVA: 0x0010D338 File Offset: 0x0010B538
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event MouseEventHandler MouseWheel
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for MouseWheel");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000468 RID: 1128
		// (add) Token: 0x060044D5 RID: 17621 RVA: 0x0010D33C File Offset: 0x0010B53C
		// (remove) Token: 0x060044D6 RID: 17622 RVA: 0x0010D348 File Offset: 0x0010B548
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event PaintEventHandler Paint
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for Paint");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x14000469 RID: 1129
		// (add) Token: 0x060044D7 RID: 17623 RVA: 0x0010D34C File Offset: 0x0010B54C
		// (remove) Token: 0x060044D8 RID: 17624 RVA: 0x0010D358 File Offset: 0x0010B558
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for QueryAccessibilityHelp");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400046A RID: 1130
		// (add) Token: 0x060044D9 RID: 17625 RVA: 0x0010D35C File Offset: 0x0010B55C
		// (remove) Token: 0x060044DA RID: 17626 RVA: 0x0010D368 File Offset: 0x0010B568
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for QueryContinueDrag");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400046B RID: 1131
		// (add) Token: 0x060044DB RID: 17627 RVA: 0x0010D36C File Offset: 0x0010B56C
		// (remove) Token: 0x060044DC RID: 17628 RVA: 0x0010D378 File Offset: 0x0010B578
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for RightToLeftChanged");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400046C RID: 1132
		// (add) Token: 0x060044DD RID: 17629 RVA: 0x0010D37C File Offset: 0x0010B57C
		// (remove) Token: 0x060044DE RID: 17630 RVA: 0x0010D388 File Offset: 0x0010B588
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler StyleChanged
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for StyleChanged");
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x1400046D RID: 1133
		// (add) Token: 0x060044DF RID: 17631 RVA: 0x0010D38C File Offset: 0x0010B58C
		// (remove) Token: 0x060044E0 RID: 17632 RVA: 0x0010D398 File Offset: 0x0010B598
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler TextChanged
		{
			add
			{
				throw new NotSupportedException("Invalid event handler for TextChanged");
			}
			remove
			{
			}
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x0010D39C File Offset: 0x0010B59C
		// (set) Token: 0x060044E2 RID: 17634 RVA: 0x0010D3A4 File Offset: 0x0010B5A4
		internal bool SuppressDialogs
		{
			get
			{
				return this.suppressDialogs;
			}
			set
			{
				this.suppressDialogs = value;
				this.webHost.Alert -= new AlertEventHandler(this.OnWebHostAlert);
				if (!this.suppressDialogs)
				{
					this.webHost.Alert += new AlertEventHandler(this.OnWebHostAlert);
				}
			}
		}

		/// <summary>Gets the underlying ActiveX WebBrowser control.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the underlying ActiveX WebBrowser control.</returns>
		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x060044E3 RID: 17635 RVA: 0x0010D3F4 File Offset: 0x0010B5F4
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public object ActiveXInstance
		{
			get
			{
				throw new NotSupportedException("Retrieving a reference to an activex interface is not supported. Sorry.");
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>false in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x060044E4 RID: 17636 RVA: 0x0010D400 File Offset: 0x0010B600
		// (set) Token: 0x060044E5 RID: 17637 RVA: 0x0010D408 File Offset: 0x0010B608
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x060044E6 RID: 17638 RVA: 0x0010D414 File Offset: 0x0010B614
		// (set) Token: 0x060044E7 RID: 17639 RVA: 0x0010D41C File Offset: 0x0010B61C
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>null.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x060044E8 RID: 17640 RVA: 0x0010D428 File Offset: 0x0010B628
		// (set) Token: 0x060044E9 RID: 17641 RVA: 0x0010D430 File Offset: 0x0010B630
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x060044EA RID: 17642 RVA: 0x0010D43C File Offset: 0x0010B63C
		// (set) Token: 0x060044EB RID: 17643 RVA: 0x0010D444 File Offset: 0x0010B644
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>The value of this property is not meaningful for this control.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x060044EC RID: 17644 RVA: 0x0010D450 File Offset: 0x0010B650
		// (set) Token: 0x060044ED RID: 17645 RVA: 0x0010D458 File Offset: 0x0010B658
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>true in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x060044EE RID: 17646 RVA: 0x0010D460 File Offset: 0x0010B660
		// (set) Token: 0x060044EF RID: 17647 RVA: 0x0010D468 File Offset: 0x0010B668
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The value of this property is not meaningful for this control.</returns>
		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x060044F0 RID: 17648 RVA: 0x0010D470 File Offset: 0x0010B670
		// (set) Token: 0x060044F1 RID: 17649 RVA: 0x0010D478 File Offset: 0x0010B678
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The value of this property is not meaningful for this control.</returns>
		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x060044F2 RID: 17650 RVA: 0x0010D484 File Offset: 0x0010B684
		// (set) Token: 0x060044F3 RID: 17651 RVA: 0x0010D48C File Offset: 0x0010B68C
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The value of this property is not meaningful for this control.</returns>
		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x060044F4 RID: 17652 RVA: 0x0010D498 File Offset: 0x0010B698
		// (set) Token: 0x060044F5 RID: 17653 RVA: 0x0010D4A0 File Offset: 0x0010B6A0
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>The value of this property is not meaningful for this control.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x060044F6 RID: 17654 RVA: 0x0010D4AC File Offset: 0x0010B6AC
		// (set) Token: 0x060044F7 RID: 17655 RVA: 0x0010D4B4 File Offset: 0x0010B6B4
		[Browsable(false)]
		[EditorBrowsable(1)]
		[Localizable(false)]
		public new virtual RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.Windows.Forms.Control" />, if any.</returns>
		// Token: 0x170011D1 RID: 4561
		// (set) Token: 0x060044F8 RID: 17656 RVA: 0x0010D4C0 File Offset: 0x0010B6C0
		public override ISite Site
		{
			set
			{
				base.Site = value;
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>
		///   <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x060044F9 RID: 17657 RVA: 0x0010D4CC File Offset: 0x0010B6CC
		// (set) Token: 0x060044FA RID: 17658 RVA: 0x0010D4D4 File Offset: 0x0010B6D4
		[Bindable(false)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public override string Text
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>false in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x060044FB RID: 17659 RVA: 0x0010D4DC File Offset: 0x0010B6DC
		// (set) Token: 0x060044FC RID: 17660 RVA: 0x0010D4E4 File Offset: 0x0010B6E4
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new bool UseWaitCursor
		{
			get
			{
				return base.UseWaitCursor;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x060044FD RID: 17661 RVA: 0x0010D4EC File Offset: 0x0010B6EC
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 100);
			}
		}

		/// <summary>This method is not supported by this control.</summary>
		/// <param name="bitmap">A <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="targetBounds">A <see cref="T:System.Drawing.Rectangle" />. </param>
		// Token: 0x060044FE RID: 17662 RVA: 0x0010D4F8 File Offset: 0x0010B6F8
		[EditorBrowsable(1)]
		public new void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
		{
			base.DrawToBitmap(bitmap, targetBounds);
		}

		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the message to process. The possible values are WM_KEYDOWN, WM_SYSKEYDOWN, WM_CHAR, and WM_SYSCHAR. </param>
		// Token: 0x060044FF RID: 17663 RVA: 0x0010D504 File Offset: 0x0010B704
		public override bool PreProcessMessage(ref Message msg)
		{
			return base.PreProcessMessage(ref msg);
		}

		/// <summary>Called by the control when the underlying ActiveX control is created.</summary>
		/// <param name="nativeActiveXObject">An object that represents the underlying ActiveX control.</param>
		// Token: 0x06004500 RID: 17664 RVA: 0x0010D510 File Offset: 0x0010B710
		protected virtual void AttachInterfaces(object nativeActiveXObject)
		{
			throw new NotSupportedException("Retrieving a reference to an activex interface is not supported. Sorry.");
		}

		/// <summary>Called by the control to prepare it for listening to events. </summary>
		// Token: 0x06004501 RID: 17665 RVA: 0x0010D51C File Offset: 0x0010B71C
		protected virtual void CreateSink()
		{
			throw new NotSupportedException("Retrieving a reference to an activex interface is not supported. Sorry.");
		}

		/// <summary>Returns a reference to the unmanaged ActiveX control site.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.WebBrowserSiteBase" /> that represents the site of the underlying ActiveX control.</returns>
		// Token: 0x06004502 RID: 17666 RVA: 0x0010D528 File Offset: 0x0010B728
		protected virtual WebBrowserSiteBase CreateWebBrowserSiteBase()
		{
			throw new NotSupportedException("Retrieving a reference to an activex interface is not supported. Sorry.");
		}

		/// <summary>Called by the control when the underlying ActiveX control is discarded.</summary>
		// Token: 0x06004503 RID: 17667 RVA: 0x0010D534 File Offset: 0x0010B734
		protected virtual void DetachInterfaces()
		{
			throw new NotSupportedException("Retrieving a reference to an activex interface is not supported. Sorry.");
		}

		/// <summary>Called by the control when it stops listening to events.</summary>
		// Token: 0x06004504 RID: 17668 RVA: 0x0010D540 File Offset: 0x0010B740
		protected virtual void DetachSink()
		{
			throw new NotSupportedException("Retrieving a reference to an activex interface is not supported. Sorry.");
		}

		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06004505 RID: 17669 RVA: 0x0010D54C File Offset: 0x0010B74C
		protected override void Dispose(bool disposing)
		{
			this.WebHost.Shutdown();
			base.Dispose(disposing);
		}

		/// <returns>true if the character should be sent directly to the control and not preprocessed; otherwise, false.</returns>
		/// <param name="charCode">The character to test. </param>
		// Token: 0x06004506 RID: 17670 RVA: 0x0010D560 File Offset: 0x0010B760
		protected override bool IsInputChar(char charCode)
		{
			return base.IsInputChar(charCode);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06004507 RID: 17671 RVA: 0x0010D56C File Offset: 0x0010B76C
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06004508 RID: 17672 RVA: 0x0010D578 File Offset: 0x0010B778
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06004509 RID: 17673 RVA: 0x0010D584 File Offset: 0x0010B784
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600450A RID: 17674 RVA: 0x0010D590 File Offset: 0x0010B790
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Threading.ThreadStateException">The <see cref="P:System.Threading.Thread.ApartmentState" /> property of the application is not set to <see cref="F:System.Threading.ApartmentState.STA" />. </exception>
		// Token: 0x0600450B RID: 17675 RVA: 0x0010D59C File Offset: 0x0010B79C
		[EditorBrowsable(2)]
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600450C RID: 17676 RVA: 0x0010D5A8 File Offset: 0x0010B7A8
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.WebHost.FocusOut();
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.OnParentChanged(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.Reflection.TargetInvocationException">Unable to get the window handle for the ActiveX control. Windowless ActiveX controls are not supported.</exception>
		// Token: 0x0600450D RID: 17677 RVA: 0x0010D5BC File Offset: 0x0010B7BC
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}

		/// <summary>This method is not meaningful for this control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object.</param>
		// Token: 0x0600450E RID: 17678 RVA: 0x0010D5C8 File Offset: 0x0010B7C8
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.OnVisibleChanged(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.Reflection.TargetInvocationException">Unable to get the window handle for the ActiveX control. Windowless ActiveX controls are not supported.</exception>
		// Token: 0x0600450F RID: 17679 RVA: 0x0010D5D4 File Offset: 0x0010B7D4
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible && !base.Disposing && !base.IsDisposed && this.state == WebBrowserBase.State.Loaded)
			{
				this.state = WebBrowserBase.State.Active;
				this.webHost.Activate();
			}
			else if (!base.Visible && this.state == WebBrowserBase.State.Active)
			{
				this.state = WebBrowserBase.State.Loaded;
				this.webHost.Deactivate();
			}
		}

		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x06004510 RID: 17680 RVA: 0x0010D658 File Offset: 0x0010B858
		protected override bool ProcessMnemonic(char charCode)
		{
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.</summary>
		/// <param name="m">The windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06004511 RID: 17681 RVA: 0x0010D664 File Offset: 0x0010B864
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x0010D670 File Offset: 0x0010B870
		internal IWebBrowser WebHost
		{
			get
			{
				return this.webHost;
			}
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x0010D678 File Offset: 0x0010B878
		internal override void SetBoundsCoreInternal(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCoreInternal(x, y, width, height, specified);
			this.webHost.Resize(width, height);
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x0010D698 File Offset: 0x0010B898
		private void OnWebHostAlert(object sender, AlertEventArgs e)
		{
			switch (e.Type)
			{
			case 1:
				MessageBox.Show(e.Text, e.Title);
				break;
			case 2:
			{
				AlertCheck alertCheck = new AlertCheck(e.Title, e.Text, e.CheckMessage, e.CheckState);
				alertCheck.Show();
				e.CheckState = alertCheck.Checked;
				e.BoolReturn = true;
				break;
			}
			case 3:
			{
				DialogResult dialogResult = MessageBox.Show(e.Text, e.Title, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
				e.BoolReturn = dialogResult == DialogResult.OK;
				break;
			}
			case 4:
				MessageBox.Show(e.Text, e.Title);
				break;
			case 5:
			{
				ConfirmCheck confirmCheck = new ConfirmCheck(e.Title, e.Text, e.CheckMessage, e.CheckState);
				DialogResult dialogResult2 = confirmCheck.Show();
				e.CheckState = confirmCheck.Checked;
				e.BoolReturn = dialogResult2 == DialogResult.OK;
				break;
			}
			case 6:
			{
				Prompt prompt = new Prompt(e.Title, e.Text, e.Text2);
				DialogResult dialogResult3 = prompt.Show();
				e.StringReturn = prompt.Text;
				e.BoolReturn = dialogResult3 == DialogResult.OK;
				break;
			}
			case 7:
				MessageBox.Show(e.Text, e.Title);
				break;
			case 8:
				MessageBox.Show(e.Text, e.Title);
				break;
			case 9:
				MessageBox.Show(e.Text, e.Title);
				break;
			}
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x0010D834 File Offset: 0x0010BA34
		private bool OnWebHostCreateNewWindow(object sender, CreateNewWindowEventArgs e)
		{
			return this.OnNewWindowInternal();
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x0010D83C File Offset: 0x0010BA3C
		internal override void OnResizeInternal(EventArgs e)
		{
			base.OnResizeInternal(e);
			if (this.state == WebBrowserBase.State.Active)
			{
				this.webHost.Resize(base.Width, base.Height);
			}
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x0010D874 File Offset: 0x0010BA74
		private void OnWebHostMouseClick(object sender, EventArgs e)
		{
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x0010D878 File Offset: 0x0010BA78
		private void OnWebHostFocus(object sender, EventArgs e)
		{
			base.Focus();
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x0010D884 File Offset: 0x0010BA84
		internal virtual bool OnNewWindowInternal()
		{
			return false;
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x0010D888 File Offset: 0x0010BA88
		internal virtual void OnWebHostLoadStarted(object sender, LoadStartedEventArgs e)
		{
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x0010D88C File Offset: 0x0010BA8C
		internal virtual void OnWebHostLoadCommited(object sender, LoadCommitedEventArgs e)
		{
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x0010D890 File Offset: 0x0010BA90
		internal virtual void OnWebHostProgressChanged(object sender, ProgressChangedEventArgs e)
		{
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x0010D894 File Offset: 0x0010BA94
		internal virtual void OnWebHostLoadFinished(object sender, LoadFinishedEventArgs e)
		{
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x0010D898 File Offset: 0x0010BA98
		internal virtual void OnWebHostSecurityChanged(object sender, SecurityChangedEventArgs e)
		{
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x0010D89C File Offset: 0x0010BA9C
		internal virtual void OnWebHostContextMenuShown(object sender, ContextMenuEventArgs e)
		{
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x0010D8A0 File Offset: 0x0010BAA0
		internal virtual void OnWebHostStatusChanged(object sender, StatusChangedEventArgs e)
		{
		}

		// Token: 0x04001CAD RID: 7341
		internal bool documentReady;

		// Token: 0x04001CAE RID: 7342
		private bool suppressDialogs;

		// Token: 0x04001CAF RID: 7343
		protected string status;

		// Token: 0x04001CB0 RID: 7344
		private WebBrowserBase.State state;

		// Token: 0x04001CB1 RID: 7345
		private IWebBrowser webHost;

		// Token: 0x020003AC RID: 940
		private enum State
		{
			// Token: 0x04001CB3 RID: 7347
			Unloaded,
			// Token: 0x04001CB4 RID: 7348
			Loaded,
			// Token: 0x04001CB5 RID: 7349
			Active
		}
	}
}
