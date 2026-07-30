using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Specifies a component that creates an icon in the notification area. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000274 RID: 628
	[ToolboxItemFilter("System.Windows.Forms", 0)]
	[DefaultEvent("MouseDoubleClick")]
	[Designer("System.Windows.Forms.Design.NotifyIconDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Text")]
	public sealed class NotifyIcon : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NotifyIcon" /> class.</summary>
		// Token: 0x060028CB RID: 10443 RVA: 0x0009E104 File Offset: 0x0009C304
		public NotifyIcon()
		{
			this.window = new NotifyIcon.NotifyIconWindow(this);
			this.systray_active = false;
			this.balloon_title = string.Empty;
			this.balloon_text = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NotifyIcon" /> class with the specified container.</summary>
		/// <param name="container">An <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the <see cref="T:System.Windows.Forms.NotifyIcon" /> control. </param>
		// Token: 0x060028CC RID: 10444 RVA: 0x0009E138 File Offset: 0x0009C338
		public NotifyIcon(IContainer container)
			: this()
		{
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x0009E140 File Offset: 0x0009C340
		// Note: this type is marked as 'beforefieldinit'.
		static NotifyIcon()
		{
			NotifyIcon.ClickEvent = new object();
			NotifyIcon.DoubleClickEvent = new object();
			NotifyIcon.MouseDownEvent = new object();
			NotifyIcon.MouseMoveEvent = new object();
			NotifyIcon.MouseUpEvent = new object();
			NotifyIcon.BalloonTipClickedEvent = new object();
			NotifyIcon.BalloonTipClosedEvent = new object();
			NotifyIcon.BalloonTipShownEvent = new object();
			NotifyIcon.MouseClickEvent = new object();
			NotifyIcon.MouseDoubleClickEvent = new object();
		}

		/// <summary>Occurs when the balloon tip is clicked.</summary>
		// Token: 0x1400025A RID: 602
		// (add) Token: 0x060028CE RID: 10446 RVA: 0x0009E1B4 File Offset: 0x0009C3B4
		// (remove) Token: 0x060028CF RID: 10447 RVA: 0x0009E1C8 File Offset: 0x0009C3C8
		[MWFCategory("Action")]
		public event EventHandler BalloonTipClicked
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.BalloonTipClickedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.BalloonTipClickedEvent, value);
			}
		}

		/// <summary>Occurs when the balloon tip is closed by the user.</summary>
		// Token: 0x1400025B RID: 603
		// (add) Token: 0x060028D0 RID: 10448 RVA: 0x0009E1DC File Offset: 0x0009C3DC
		// (remove) Token: 0x060028D1 RID: 10449 RVA: 0x0009E1F0 File Offset: 0x0009C3F0
		[MWFCategory("Action")]
		public event EventHandler BalloonTipClosed
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.BalloonTipClosedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.BalloonTipClosedEvent, value);
			}
		}

		/// <summary>Occurs when the balloon tip is displayed on the screen.</summary>
		// Token: 0x1400025C RID: 604
		// (add) Token: 0x060028D2 RID: 10450 RVA: 0x0009E204 File Offset: 0x0009C404
		// (remove) Token: 0x060028D3 RID: 10451 RVA: 0x0009E218 File Offset: 0x0009C418
		[MWFCategory("Action")]
		public event EventHandler BalloonTipShown
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.BalloonTipShownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.BalloonTipShownEvent, value);
			}
		}

		/// <summary>Occurs when the user clicks a <see cref="T:System.Windows.Forms.NotifyIcon" /> with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400025D RID: 605
		// (add) Token: 0x060028D4 RID: 10452 RVA: 0x0009E22C File Offset: 0x0009C42C
		// (remove) Token: 0x060028D5 RID: 10453 RVA: 0x0009E240 File Offset: 0x0009C440
		[MWFCategory("Action")]
		public event MouseEventHandler MouseClick
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.MouseClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.MouseClickEvent, value);
			}
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.NotifyIcon" /> with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400025E RID: 606
		// (add) Token: 0x060028D6 RID: 10454 RVA: 0x0009E254 File Offset: 0x0009C454
		// (remove) Token: 0x060028D7 RID: 10455 RVA: 0x0009E268 File Offset: 0x0009C468
		[MWFCategory("Action")]
		public event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.MouseDoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.MouseDoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when the user clicks the icon in the notification area.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400025F RID: 607
		// (add) Token: 0x060028D8 RID: 10456 RVA: 0x0009E27C File Offset: 0x0009C47C
		// (remove) Token: 0x060028D9 RID: 10457 RVA: 0x0009E290 File Offset: 0x0009C490
		[MWFCategory("Action")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.ClickEvent, value);
			}
		}

		/// <summary>Occurs when the user double-clicks the icon in the notification area of the taskbar.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000260 RID: 608
		// (add) Token: 0x060028DA RID: 10458 RVA: 0x0009E2A4 File Offset: 0x0009C4A4
		// (remove) Token: 0x060028DB RID: 10459 RVA: 0x0009E2B8 File Offset: 0x0009C4B8
		[MWFCategory("Action")]
		public event EventHandler DoubleClick
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.DoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.DoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when the user presses the mouse button while the pointer is over the icon in the notification area of the taskbar.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000261 RID: 609
		// (add) Token: 0x060028DC RID: 10460 RVA: 0x0009E2CC File Offset: 0x0009C4CC
		// (remove) Token: 0x060028DD RID: 10461 RVA: 0x0009E2E0 File Offset: 0x0009C4E0
		public event MouseEventHandler MouseDown
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.MouseDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.MouseDownEvent, value);
			}
		}

		/// <summary>Occurs when the user moves the mouse while the pointer is over the icon in the notification area of the taskbar.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000262 RID: 610
		// (add) Token: 0x060028DE RID: 10462 RVA: 0x0009E2F4 File Offset: 0x0009C4F4
		// (remove) Token: 0x060028DF RID: 10463 RVA: 0x0009E308 File Offset: 0x0009C508
		public event MouseEventHandler MouseMove
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.MouseMoveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.MouseMoveEvent, value);
			}
		}

		/// <summary>Occurs when the user releases the mouse button while the pointer is over the icon in the notification area of the taskbar.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000263 RID: 611
		// (add) Token: 0x060028E0 RID: 10464 RVA: 0x0009E31C File Offset: 0x0009C51C
		// (remove) Token: 0x060028E1 RID: 10465 RVA: 0x0009E330 File Offset: 0x0009C530
		public event MouseEventHandler MouseUp
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.MouseUpEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.MouseUpEvent, value);
			}
		}

		/// <summary>Displays a balloon tip in the taskbar for the specified time period.</summary>
		/// <param name="timeout">The time period, in milliseconds, the balloon tip should display.</param>
		// Token: 0x060028E2 RID: 10466 RVA: 0x0009E344 File Offset: 0x0009C544
		public void ShowBalloonTip(int timeout)
		{
			this.ShowBalloonTip(timeout, this.balloon_title, this.balloon_text, this.balloon_icon);
		}

		/// <summary>Displays a balloon tip with the specified title, text, and icon in the taskbar for the specified time period.</summary>
		/// <param name="timeout">The time period, in milliseconds, the balloon tip should display.</param>
		/// <param name="tipTitle">The title to display on the balloon tip.</param>
		/// <param name="tipText">The text to display on the balloon tip.</param>
		/// <param name="tipIcon">One of the <see cref="T:System.Windows.Forms.ToolTipIcon" /> values.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="tipText" /> is null or an empty string.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="tipIcon" /> is not a member of <see cref="T:System.Windows.Forms.ToolTipIcon" />.</exception>
		// Token: 0x060028E3 RID: 10467 RVA: 0x0009E360 File Offset: 0x0009C560
		public void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
		{
			XplatUI.SystrayBalloon(this.window.Handle, timeout, tipTitle, tipText, tipIcon);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x0009E378 File Offset: 0x0009C578
		private void OnBalloonTipClicked(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.BalloonTipClickedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x0009E3AC File Offset: 0x0009C5AC
		private void OnBalloonTipClosed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.BalloonTipClosedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x0009E3E0 File Offset: 0x0009C5E0
		private void OnBalloonTipShown(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.BalloonTipShownEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0009E414 File Offset: 0x0009C614
		private void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.ClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0009E448 File Offset: 0x0009C648
		private void OnDoubleClick(EventArgs e)
		{
			this.double_click = true;
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.DoubleClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x0009E480 File Offset: 0x0009C680
		private void OnMouseClick(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.MouseClickEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0009E4B4 File Offset: 0x0009C6B4
		private void OnMouseDoubleClick(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.MouseDoubleClickEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x0009E4E8 File Offset: 0x0009C6E8
		private void OnMouseDown(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.MouseDownEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x0009E51C File Offset: 0x0009C71C
		private void OnMouseUp(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
			{
				if (this.context_menu != null)
				{
					XplatUI.SetForegroundWindow(this.window.Handle);
					this.context_menu.Show(this.window, new Point(e.X, e.Y));
				}
				else if (this.context_menu_strip != null)
				{
					XplatUI.SetForegroundWindow(this.window.Handle);
					this.context_menu_strip.Show(this.window, new Point(e.X, e.Y), ToolStripDropDownDirection.AboveLeft);
				}
			}
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.MouseUpEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
			if (!this.double_click)
			{
				this.OnClick(EventArgs.Empty);
				this.OnMouseClick(e);
				this.double_click = false;
			}
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x0009E608 File Offset: 0x0009C808
		private void OnMouseMove(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.MouseMoveEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x0009E63C File Offset: 0x0009C83C
		private void Recalculate()
		{
			this.window.CalculateIconRect();
			if (!this.Visible || (this.text == string.Empty && this.icon == null))
			{
				this.HideSystray();
			}
			else if (this.systray_active)
			{
				this.UpdateSystray();
			}
			else
			{
				this.ShowSystray();
			}
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x0009E6A8 File Offset: 0x0009C8A8
		private void ShowSystray()
		{
			if (this.icon == null)
			{
				return;
			}
			this.icon_bitmap = this.icon.ToBitmap();
			this.systray_active = true;
			XplatUI.SystrayAdd(this.window.Handle, this.text, this.icon, out this.tooltip);
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x0009E6FC File Offset: 0x0009C8FC
		private void HideSystray()
		{
			if (!this.systray_active)
			{
				return;
			}
			this.systray_active = false;
			XplatUI.SystrayRemove(this.window.Handle, ref this.tooltip);
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x0009E728 File Offset: 0x0009C928
		private void UpdateSystray()
		{
			if (this.icon_bitmap != null)
			{
				this.icon_bitmap.Dispose();
			}
			if (this.icon != null)
			{
				this.icon_bitmap = this.icon.ToBitmap();
			}
			this.window.Invalidate();
			XplatUI.SystrayChange(this.window.Handle, this.text, this.icon, ref this.tooltip);
		}

		/// <summary>Gets or sets the icon to display on the balloon tip associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolTipIcon" /> to display on the balloon tip associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a <see cref="T:System.Windows.Forms.ToolTipIcon" />.</exception>
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x0009E794 File Offset: 0x0009C994
		// (set) Token: 0x060028F3 RID: 10483 RVA: 0x0009E79C File Offset: 0x0009C99C
		[DefaultValue("None")]
		public ToolTipIcon BalloonTipIcon
		{
			get
			{
				return this.balloon_icon;
			}
			set
			{
				if (value == this.balloon_icon)
				{
					return;
				}
				this.balloon_icon = value;
			}
		}

		/// <summary>Gets or sets the text to display on the balloon tip associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
		/// <returns>The text to display on the balloon tip associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</returns>
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x0009E7B4 File Offset: 0x0009C9B4
		// (set) Token: 0x060028F5 RID: 10485 RVA: 0x0009E7BC File Offset: 0x0009C9BC
		[DefaultValue("")]
		[Localizable(true)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string BalloonTipText
		{
			get
			{
				return this.balloon_text;
			}
			set
			{
				if (value == this.balloon_text)
				{
					return;
				}
				this.balloon_text = value;
			}
		}

		/// <summary>Gets or sets the title of the balloon tip displayed on the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
		/// <returns>The text to display as the title of the balloon tip.</returns>
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x0009E7D8 File Offset: 0x0009C9D8
		// (set) Token: 0x060028F7 RID: 10487 RVA: 0x0009E7E0 File Offset: 0x0009C9E0
		[DefaultValue("")]
		[Localizable(true)]
		public string BalloonTipTitle
		{
			get
			{
				return this.balloon_title;
			}
			set
			{
				if (value == this.balloon_title)
				{
					return;
				}
				this.balloon_title = value;
			}
		}

		/// <summary>Gets or sets the shortcut menu for the icon.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> for the icon. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x0009E7FC File Offset: 0x0009C9FC
		// (set) Token: 0x060028F9 RID: 10489 RVA: 0x0009E804 File Offset: 0x0009CA04
		[DefaultValue(null)]
		[Browsable(false)]
		public ContextMenu ContextMenu
		{
			get
			{
				return this.context_menu;
			}
			set
			{
				if (this.context_menu != value)
				{
					this.context_menu = value;
					this.window.ContextMenu = value;
				}
			}
		}

		/// <summary>Gets or sets the shortcut menu associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the <see cref="T:System.Windows.Forms.NotifyIcon" /></returns>
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060028FA RID: 10490 RVA: 0x0009E828 File Offset: 0x0009CA28
		// (set) Token: 0x060028FB RID: 10491 RVA: 0x0009E830 File Offset: 0x0009CA30
		[DefaultValue(null)]
		public ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.context_menu_strip;
			}
			set
			{
				if (this.context_menu_strip != value)
				{
					this.context_menu_strip = value;
					this.window.ContextMenuStrip = value;
				}
			}
		}

		/// <summary>Gets or sets the current icon.</summary>
		/// <returns>The <see cref="T:System.Drawing.Icon" /> displayed by the <see cref="T:System.Windows.Forms.NotifyIcon" /> component. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x0009E854 File Offset: 0x0009CA54
		// (set) Token: 0x060028FD RID: 10493 RVA: 0x0009E85C File Offset: 0x0009CA5C
		[Localizable(true)]
		[DefaultValue(null)]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (this.icon != value)
				{
					this.icon = value;
					this.Recalculate();
				}
			}
		}

		/// <summary>Gets or sets an object that contains data about the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.NotifyIcon" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x0009E878 File Offset: 0x0009CA78
		// (set) Token: 0x060028FF RID: 10495 RVA: 0x0009E880 File Offset: 0x0009CA80
		[Bindable(true)]
		[DefaultValue(null)]
		[Localizable(false)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text displayed when the mouse pointer rests on a notification area icon.</summary>
		/// <returns>The ToolTip text displayed when the mouse pointer rests on a notification area icon.</returns>
		/// <exception cref="T:System.ArgumentException">ToolTip text is more than 63 characters long.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x0009E88C File Offset: 0x0009CA8C
		// (set) Token: 0x06002901 RID: 10497 RVA: 0x0009E894 File Offset: 0x0009CA94
		[Localizable(true)]
		[DefaultValue("")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (this.text != value)
				{
					if (value.Length >= 64)
					{
						throw new ArgumentException("ToolTip length must be less than 64 characters long", "Text");
					}
					this.text = value;
					this.Recalculate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the icon is visible in the notification area of the taskbar.</summary>
		/// <returns>true if the icon is visible in the notification area; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x0009E8D4 File Offset: 0x0009CAD4
		// (set) Token: 0x06002903 RID: 10499 RVA: 0x0009E8DC File Offset: 0x0009CADC
		[Localizable(true)]
		[DefaultValue(false)]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (this.visible != value)
				{
					this.visible = value;
					this.window.is_visible = value;
					if (this.visible)
					{
						this.ShowSystray();
					}
					else
					{
						this.HideSystray();
					}
				}
			}
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x0009E91C File Offset: 0x0009CB1C
		protected override void Dispose(bool disposing)
		{
			if (this.visible)
			{
				this.HideSystray();
			}
			if (this.icon_bitmap != null)
			{
				this.icon_bitmap.Dispose();
			}
			if (disposing)
			{
				this.icon = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04001469 RID: 5225
		private ContextMenu context_menu;

		// Token: 0x0400146A RID: 5226
		private Icon icon;

		// Token: 0x0400146B RID: 5227
		private Bitmap icon_bitmap;

		// Token: 0x0400146C RID: 5228
		private string text;

		// Token: 0x0400146D RID: 5229
		private bool visible;

		// Token: 0x0400146E RID: 5230
		private NotifyIcon.NotifyIconWindow window;

		// Token: 0x0400146F RID: 5231
		private bool systray_active;

		// Token: 0x04001470 RID: 5232
		private ToolTip tooltip;

		// Token: 0x04001471 RID: 5233
		private bool double_click;

		// Token: 0x04001472 RID: 5234
		private string balloon_text;

		// Token: 0x04001473 RID: 5235
		private string balloon_title;

		// Token: 0x04001474 RID: 5236
		private ToolTipIcon balloon_icon;

		// Token: 0x04001475 RID: 5237
		private ContextMenuStrip context_menu_strip;

		// Token: 0x04001476 RID: 5238
		private object tag;

		// Token: 0x02000275 RID: 629
		internal class NotifyIconWindow : Form
		{
			// Token: 0x06002905 RID: 10501 RVA: 0x0009E95C File Offset: 0x0009CB5C
			public NotifyIconWindow(NotifyIcon owner)
			{
				this.owner = owner;
				this.is_visible = false;
				this.rect = new Rectangle(0, 0, 1, 1);
				base.FormBorderStyle = FormBorderStyle.None;
				base.SizeChanged += new EventHandler(this.HandleSizeChanged);
				base.DoubleClick += new EventHandler(this.HandleDoubleClick);
				base.MouseDown += this.HandleMouseDown;
				base.MouseUp += this.HandleMouseUp;
				base.MouseMove += this.HandleMouseMove;
				this.ContextMenu = owner.context_menu;
				this.ContextMenuStrip = owner.context_menu_strip;
			}

			// Token: 0x17000A09 RID: 2569
			// (get) Token: 0x06002906 RID: 10502 RVA: 0x0009EA08 File Offset: 0x0009CC08
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Parent = IntPtr.Zero;
					createParams.Style = int.MinValue;
					createParams.Style |= 67108864;
					createParams.ExStyle = 128;
					return createParams;
				}
			}

			// Token: 0x06002907 RID: 10503 RVA: 0x0009EA50 File Offset: 0x0009CC50
			protected override void WndProc(ref Message m)
			{
				Msg msg = (Msg)m.Msg;
				if (msg == Msg.WM_CONTEXTMENU)
				{
					return;
				}
				if (msg != Msg.WM_USER)
				{
					base.WndProc(ref m);
					return;
				}
				Msg msg2 = (Msg)m.LParam.ToInt32();
				switch (msg2)
				{
				case Msg.WM_MOUSEMOVE:
					this.owner.OnMouseMove(new MouseEventArgs(MouseButtons.None, 1, Control.MousePosition.X, Control.MousePosition.Y, 0));
					return;
				case Msg.WM_LBUTTONDOWN:
					this.owner.OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, Control.MousePosition.X, Control.MousePosition.Y, 0));
					return;
				case Msg.WM_LBUTTONUP:
					this.owner.OnMouseUp(new MouseEventArgs(MouseButtons.Left, 1, Control.MousePosition.X, Control.MousePosition.Y, 0));
					return;
				case Msg.WM_LBUTTONDBLCLK:
					this.owner.OnDoubleClick(EventArgs.Empty);
					this.owner.OnMouseDoubleClick(new MouseEventArgs(MouseButtons.Left, 2, Control.MousePosition.X, Control.MousePosition.Y, 0));
					return;
				case Msg.WM_RBUTTONDOWN:
					this.owner.OnMouseDown(new MouseEventArgs(MouseButtons.Right, 1, Control.MousePosition.X, Control.MousePosition.Y, 0));
					return;
				case Msg.WM_RBUTTONUP:
					this.owner.OnMouseUp(new MouseEventArgs(MouseButtons.Right, 1, Control.MousePosition.X, Control.MousePosition.Y, 0));
					return;
				case Msg.WM_RBUTTONDBLCLK:
					this.owner.OnDoubleClick(EventArgs.Empty);
					this.owner.OnMouseDoubleClick(new MouseEventArgs(MouseButtons.Left, 2, Control.MousePosition.X, Control.MousePosition.Y, 0));
					return;
				default:
					switch (msg2)
					{
					case Msg.NIN_BALLOONSHOW:
						this.owner.OnBalloonTipShown(EventArgs.Empty);
						return;
					case Msg.WM_ASYNC_MESSAGE:
					case Msg.NIN_BALLOONTIMEOUT:
						this.owner.OnBalloonTipClosed(EventArgs.Empty);
						return;
					case Msg.NIN_BALLOONUSERCLICK:
						this.owner.OnBalloonTipClicked(EventArgs.Empty);
						return;
					default:
						return;
					}
					break;
				}
			}

			// Token: 0x06002908 RID: 10504 RVA: 0x0009EC98 File Offset: 0x0009CE98
			internal void CalculateIconRect()
			{
				int num;
				if (base.ClientRectangle.Width < base.ClientRectangle.Height)
				{
					num = base.ClientRectangle.Width;
				}
				else
				{
					num = base.ClientRectangle.Height;
				}
				int num2 = base.ClientRectangle.Width / 2 - num / 2;
				int num3 = base.ClientRectangle.Height / 2 - num / 2;
				this.rect = new Rectangle(num2, num3, num, num);
				base.Bounds = new Rectangle(0, 0, num, num);
			}

			// Token: 0x06002909 RID: 10505 RVA: 0x0009ED38 File Offset: 0x0009CF38
			internal override void OnPaintInternal(PaintEventArgs e)
			{
				if (this.owner.icon != null)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(SystemColors.Window), this.rect);
					e.Graphics.DrawImage(this.owner.icon_bitmap, this.rect, new Rectangle(0, 0, this.owner.icon_bitmap.Width, this.owner.icon_bitmap.Height), 2);
				}
			}

			// Token: 0x0600290A RID: 10506 RVA: 0x0009EDC0 File Offset: 0x0009CFC0
			internal void InternalRecreateHandle()
			{
				base.RecreateHandle();
			}

			// Token: 0x0600290B RID: 10507 RVA: 0x0009EDC8 File Offset: 0x0009CFC8
			private void HandleSizeChanged(object sender, EventArgs e)
			{
				this.owner.Recalculate();
			}

			// Token: 0x0600290C RID: 10508 RVA: 0x0009EDD8 File Offset: 0x0009CFD8
			private void HandleDoubleClick(object sender, EventArgs e)
			{
				this.owner.OnDoubleClick(e);
				this.owner.OnMouseDoubleClick(new MouseEventArgs(MouseButtons.Left, 2, Control.MousePosition.X, Control.MousePosition.Y, 0));
			}

			// Token: 0x0600290D RID: 10509 RVA: 0x0009EE24 File Offset: 0x0009D024
			private void HandleMouseDown(object sender, MouseEventArgs e)
			{
				this.owner.OnMouseDown(e);
			}

			// Token: 0x0600290E RID: 10510 RVA: 0x0009EE34 File Offset: 0x0009D034
			private void HandleMouseUp(object sender, MouseEventArgs e)
			{
				this.owner.OnMouseUp(e);
			}

			// Token: 0x0600290F RID: 10511 RVA: 0x0009EE44 File Offset: 0x0009D044
			private void HandleMouseMove(object sender, MouseEventArgs e)
			{
				this.owner.OnMouseMove(e);
			}

			// Token: 0x04001481 RID: 5249
			private NotifyIcon owner;

			// Token: 0x04001482 RID: 5250
			private Rectangle rect;
		}

		// Token: 0x02000276 RID: 630
		internal class BalloonWindow : Form
		{
			// Token: 0x06002910 RID: 10512 RVA: 0x0009EE54 File Offset: 0x0009D054
			public BalloonWindow(IntPtr owner)
			{
				this.owner = owner;
				base.StartPosition = FormStartPosition.Manual;
				base.FormBorderStyle = FormBorderStyle.None;
				base.MouseDown += this.HandleMouseDown;
				this.timer = new Timer();
				this.timer.Enabled = false;
				this.timer.Tick += new EventHandler(this.HandleTimer);
			}

			// Token: 0x06002911 RID: 10513 RVA: 0x0009EEBC File Offset: 0x0009D0BC
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.timer.Stop();
					this.timer.Dispose();
				}
				base.Dispose(disposing);
			}

			// Token: 0x17000A0A RID: 2570
			// (get) Token: 0x06002912 RID: 10514 RVA: 0x0009EEE4 File Offset: 0x0009D0E4
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style = int.MinValue;
					createParams.Style |= 67108864;
					createParams.ExStyle = 136;
					return createParams;
				}
			}

			// Token: 0x06002913 RID: 10515 RVA: 0x0009EF24 File Offset: 0x0009D124
			public new void Close()
			{
				base.Close();
				XplatUI.SendMessage(this.owner, Msg.WM_USER, IntPtr.Zero, (IntPtr)1027);
			}

			// Token: 0x06002914 RID: 10516 RVA: 0x0009EF58 File Offset: 0x0009D158
			protected override void OnShown(EventArgs e)
			{
				base.OnShown(e);
				this.timer.Start();
			}

			// Token: 0x06002915 RID: 10517 RVA: 0x0009EF6C File Offset: 0x0009D16C
			protected override void OnPaint(PaintEventArgs e)
			{
				ThemeEngine.Current.DrawBalloonWindow(e.Graphics, base.ClientRectangle, this);
				base.OnPaint(e);
			}

			// Token: 0x06002916 RID: 10518 RVA: 0x0009EF98 File Offset: 0x0009D198
			private void Recalculate()
			{
				Rectangle rectangle = ThemeEngine.Current.BalloonWindowRect(this);
				base.Left = rectangle.Left;
				base.Top = rectangle.Top;
				base.Width = rectangle.Width;
				base.Height = rectangle.Height;
			}

			// Token: 0x06002917 RID: 10519 RVA: 0x0009EFE8 File Offset: 0x0009D1E8
			private void HandleMouseDown(object sender, MouseEventArgs e)
			{
				XplatUI.SendMessage(this.owner, Msg.WM_USER, IntPtr.Zero, (IntPtr)1029);
				base.Close();
			}

			// Token: 0x06002918 RID: 10520 RVA: 0x0009F01C File Offset: 0x0009D21C
			private void HandleTimer(object sender, EventArgs e)
			{
				this.timer.Stop();
				XplatUI.SendMessage(this.owner, Msg.WM_USER, IntPtr.Zero, (IntPtr)1028);
				base.Close();
			}

			// Token: 0x17000A0B RID: 2571
			// (get) Token: 0x06002919 RID: 10521 RVA: 0x0009F050 File Offset: 0x0009D250
			internal StringFormat Format
			{
				get
				{
					return new StringFormat
					{
						Alignment = 0,
						HotkeyPrefix = 2
					};
				}
			}

			// Token: 0x17000A0C RID: 2572
			// (get) Token: 0x0600291A RID: 10522 RVA: 0x0009F074 File Offset: 0x0009D274
			// (set) Token: 0x0600291B RID: 10523 RVA: 0x0009F07C File Offset: 0x0009D27C
			public new ToolTipIcon Icon
			{
				get
				{
					return this.icon;
				}
				set
				{
					if (value == this.icon)
					{
						return;
					}
					this.icon = value;
					this.Recalculate();
				}
			}

			// Token: 0x17000A0D RID: 2573
			// (get) Token: 0x0600291C RID: 10524 RVA: 0x0009F098 File Offset: 0x0009D298
			// (set) Token: 0x0600291D RID: 10525 RVA: 0x0009F0A0 File Offset: 0x0009D2A0
			public string Title
			{
				get
				{
					return this.title;
				}
				set
				{
					if (value == this.title)
					{
						return;
					}
					this.title = value;
					this.Recalculate();
				}
			}

			// Token: 0x17000A0E RID: 2574
			// (get) Token: 0x0600291E RID: 10526 RVA: 0x0009F0C4 File Offset: 0x0009D2C4
			// (set) Token: 0x0600291F RID: 10527 RVA: 0x0009F0CC File Offset: 0x0009D2CC
			public override string Text
			{
				get
				{
					return this.text;
				}
				set
				{
					if (value == this.text)
					{
						return;
					}
					this.text = value;
					this.Recalculate();
				}
			}

			// Token: 0x17000A0F RID: 2575
			// (get) Token: 0x06002920 RID: 10528 RVA: 0x0009F0F0 File Offset: 0x0009D2F0
			// (set) Token: 0x06002921 RID: 10529 RVA: 0x0009F100 File Offset: 0x0009D300
			public int Timeout
			{
				get
				{
					return this.timer.Interval;
				}
				set
				{
					if (value < 10000)
					{
						this.timer.Interval = 10000;
					}
					else if (value > 30000)
					{
						this.timer.Interval = 30000;
					}
					else
					{
						this.timer.Interval = value;
					}
				}
			}

			// Token: 0x04001483 RID: 5251
			private IntPtr owner;

			// Token: 0x04001484 RID: 5252
			private Timer timer;

			// Token: 0x04001485 RID: 5253
			private string title;

			// Token: 0x04001486 RID: 5254
			private string text;

			// Token: 0x04001487 RID: 5255
			private ToolTipIcon icon;
		}
	}
}
