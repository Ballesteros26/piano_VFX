using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a window or dialog box that makes up an application's user interface.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000196 RID: 406
	[DefaultEvent("Load")]
	[InitializationEvent("Load")]
	[ComVisible(true)]
	[ToolboxItemFilter("System.Windows.Forms.Control.TopLevel")]
	[ToolboxItem(false)]
	[DesignerCategory("Form")]
	[DesignTimeVisible(false)]
	[Designer("System.Windows.Forms.Design.FormDocumentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[ClassInterface(1)]
	public class Form : ContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Form" /> class.</summary>
		// Token: 0x060019E2 RID: 6626 RVA: 0x00064BF0 File Offset: 0x00062DF0
		public Form()
		{
			SizeF autoScaleSize = Form.GetAutoScaleSize(this.Font);
			this.autoscale = true;
			this.autoscale_base_size = new Size((int)Math.Round((double)autoScaleSize.Width), (int)Math.Round((double)autoScaleSize.Height));
			this.allow_transparency = false;
			this.closing = false;
			this.is_modal = false;
			this.dialog_result = DialogResult.None;
			this.start_position = FormStartPosition.WindowsDefaultLocation;
			this.form_border_style = FormBorderStyle.Sizable;
			this.window_state = FormWindowState.Normal;
			this.key_preview = false;
			this.opacity = 1.0;
			this.menu = null;
			this.icon = Form.default_icon;
			this.minimum_size = Size.Empty;
			this.maximum_size = Size.Empty;
			this.clientsize_set = Size.Empty;
			this.control_box = true;
			this.minimize_box = true;
			this.maximize_box = true;
			this.help_button = false;
			this.show_in_taskbar = true;
			this.is_visible = false;
			this.is_toplevel = true;
			this.size_grip_style = SizeGripStyle.Auto;
			this.maximized_bounds = Rectangle.Empty;
			this.default_maximized_bounds = Rectangle.Empty;
			this.owned_forms = new Form.ControlCollection(this);
			this.transparency_key = Color.Empty;
			base.InternalClientSize = new Size(base.Width - SystemInformation.FrameBorderSize.Width * 2, base.Height - SystemInformation.FrameBorderSize.Height * 2 - SystemInformation.CaptionHeight);
			this.restore_bounds = this.Bounds;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00064D6C File Offset: 0x00062F6C
		static Form()
		{
			Form.ActivatedEvent = new object();
			Form.ClosedEvent = new object();
			Form.ClosingEvent = new object();
			Form.DeactivateEvent = new object();
			Form.InputLanguageChangedEvent = new object();
			Form.InputLanguageChangingEvent = new object();
			Form.LoadEvent = new object();
			Form.MaximizedBoundsChangedEvent = new object();
			Form.MaximumSizeChangedEvent = new object();
			Form.MdiChildActivateEvent = new object();
			Form.MenuCompleteEvent = new object();
			Form.MenuStartEvent = new object();
			Form.MinimumSizeChangedEvent = new object();
			Form.FormClosingEvent = new object();
			Form.FormClosedEvent = new object();
			Form.HelpButtonClickedEvent = new object();
			Form.ResizeEndEvent = new object();
			Form.ResizeBeginEvent = new object();
			Form.RightToLeftLayoutChangedEvent = new object();
			Form.ShownEvent = new object();
			Form.UIAMenuChangedEvent = new object();
			Form.UIATopMostChangedEvent = new object();
			Form.UIAWindowStateChangedEvent = new object();
			Form.default_icon = ResourceImageLoader.GetIcon("mono.ico");
		}

		/// <summary>Occurs when the form is activated in code or by the user.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000198 RID: 408
		// (add) Token: 0x060019E4 RID: 6628 RVA: 0x00064E70 File Offset: 0x00063070
		// (remove) Token: 0x060019E5 RID: 6629 RVA: 0x00064E84 File Offset: 0x00063084
		public event EventHandler Activated
		{
			add
			{
				base.Events.AddHandler(Form.ActivatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.ActivatedEvent, value);
			}
		}

		/// <summary>Occurs when the form is closed. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000199 RID: 409
		// (add) Token: 0x060019E6 RID: 6630 RVA: 0x00064E98 File Offset: 0x00063098
		// (remove) Token: 0x060019E7 RID: 6631 RVA: 0x00064EAC File Offset: 0x000630AC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public event EventHandler Closed
		{
			add
			{
				base.Events.AddHandler(Form.ClosedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.ClosedEvent, value);
			}
		}

		/// <summary>Occurs when the form is closing.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400019A RID: 410
		// (add) Token: 0x060019E8 RID: 6632 RVA: 0x00064EC0 File Offset: 0x000630C0
		// (remove) Token: 0x060019E9 RID: 6633 RVA: 0x00064ED4 File Offset: 0x000630D4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public event CancelEventHandler Closing
		{
			add
			{
				base.Events.AddHandler(Form.ClosingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.ClosingEvent, value);
			}
		}

		/// <summary>Occurs when the form loses focus and is no longer the active form.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400019B RID: 411
		// (add) Token: 0x060019EA RID: 6634 RVA: 0x00064EE8 File Offset: 0x000630E8
		// (remove) Token: 0x060019EB RID: 6635 RVA: 0x00064EFC File Offset: 0x000630FC
		public event EventHandler Deactivate
		{
			add
			{
				base.Events.AddHandler(Form.DeactivateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.DeactivateEvent, value);
			}
		}

		/// <summary>Occurs after the input language of the form has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400019C RID: 412
		// (add) Token: 0x060019EC RID: 6636 RVA: 0x00064F10 File Offset: 0x00063110
		// (remove) Token: 0x060019ED RID: 6637 RVA: 0x00064F24 File Offset: 0x00063124
		public event InputLanguageChangedEventHandler InputLanguageChanged
		{
			add
			{
				base.Events.AddHandler(Form.InputLanguageChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.InputLanguageChangedEvent, value);
			}
		}

		/// <summary>Occurs when the user attempts to change the input language for the form.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400019D RID: 413
		// (add) Token: 0x060019EE RID: 6638 RVA: 0x00064F38 File Offset: 0x00063138
		// (remove) Token: 0x060019EF RID: 6639 RVA: 0x00064F4C File Offset: 0x0006314C
		public event InputLanguageChangingEventHandler InputLanguageChanging
		{
			add
			{
				base.Events.AddHandler(Form.InputLanguageChangingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.InputLanguageChangingEvent, value);
			}
		}

		/// <summary>Occurs before a form is displayed for the first time.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400019E RID: 414
		// (add) Token: 0x060019F0 RID: 6640 RVA: 0x00064F60 File Offset: 0x00063160
		// (remove) Token: 0x060019F1 RID: 6641 RVA: 0x00064F74 File Offset: 0x00063174
		public event EventHandler Load
		{
			add
			{
				base.Events.AddHandler(Form.LoadEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.LoadEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Form.MaximizedBounds" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400019F RID: 415
		// (add) Token: 0x060019F2 RID: 6642 RVA: 0x00064F88 File Offset: 0x00063188
		// (remove) Token: 0x060019F3 RID: 6643 RVA: 0x00064F9C File Offset: 0x0006319C
		public event EventHandler MaximizedBoundsChanged
		{
			add
			{
				base.Events.AddHandler(Form.MaximizedBoundsChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.MaximizedBoundsChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Form.MaximumSize" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001A0 RID: 416
		// (add) Token: 0x060019F4 RID: 6644 RVA: 0x00064FB0 File Offset: 0x000631B0
		// (remove) Token: 0x060019F5 RID: 6645 RVA: 0x00064FC4 File Offset: 0x000631C4
		public event EventHandler MaximumSizeChanged
		{
			add
			{
				base.Events.AddHandler(Form.MaximumSizeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.MaximumSizeChangedEvent, value);
			}
		}

		/// <summary>Occurs when a multiple-document interface (MDI) child form is activated or closed within an MDI application.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001A1 RID: 417
		// (add) Token: 0x060019F6 RID: 6646 RVA: 0x00064FD8 File Offset: 0x000631D8
		// (remove) Token: 0x060019F7 RID: 6647 RVA: 0x00064FEC File Offset: 0x000631EC
		public event EventHandler MdiChildActivate
		{
			add
			{
				base.Events.AddHandler(Form.MdiChildActivateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.MdiChildActivateEvent, value);
			}
		}

		/// <summary>Occurs when the menu of a form loses focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001A2 RID: 418
		// (add) Token: 0x060019F8 RID: 6648 RVA: 0x00065000 File Offset: 0x00063200
		// (remove) Token: 0x060019F9 RID: 6649 RVA: 0x00065014 File Offset: 0x00063214
		[Browsable(false)]
		public event EventHandler MenuComplete
		{
			add
			{
				base.Events.AddHandler(Form.MenuCompleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.MenuCompleteEvent, value);
			}
		}

		/// <summary>Occurs when the menu of a form receives focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001A3 RID: 419
		// (add) Token: 0x060019FA RID: 6650 RVA: 0x00065028 File Offset: 0x00063228
		// (remove) Token: 0x060019FB RID: 6651 RVA: 0x0006503C File Offset: 0x0006323C
		[Browsable(false)]
		public event EventHandler MenuStart
		{
			add
			{
				base.Events.AddHandler(Form.MenuStartEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.MenuStartEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Form.MinimumSize" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001A4 RID: 420
		// (add) Token: 0x060019FC RID: 6652 RVA: 0x00065050 File Offset: 0x00063250
		// (remove) Token: 0x060019FD RID: 6653 RVA: 0x00065064 File Offset: 0x00063264
		public event EventHandler MinimumSizeChanged
		{
			add
			{
				base.Events.AddHandler(Form.MinimumSizeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.MinimumSizeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Form.TabIndex" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001A5 RID: 421
		// (add) Token: 0x060019FE RID: 6654 RVA: 0x00065078 File Offset: 0x00063278
		// (remove) Token: 0x060019FF RID: 6655 RVA: 0x00065084 File Offset: 0x00063284
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler TabIndexChanged
		{
			add
			{
				base.TabIndexChanged += value;
			}
			remove
			{
				base.TabIndexChanged -= value;
			}
		}

		// Token: 0x140001A6 RID: 422
		// (add) Token: 0x06001A00 RID: 6656 RVA: 0x00065090 File Offset: 0x00063290
		// (remove) Token: 0x06001A01 RID: 6657 RVA: 0x0006509C File Offset: 0x0006329C
		[Browsable(true)]
		[EditorBrowsable(0)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		// Token: 0x140001A7 RID: 423
		// (add) Token: 0x06001A02 RID: 6658 RVA: 0x000650A8 File Offset: 0x000632A8
		// (remove) Token: 0x06001A03 RID: 6659 RVA: 0x000650B4 File Offset: 0x000632B4
		[Browsable(true)]
		[EditorBrowsable(0)]
		public new event EventHandler AutoValidateChanged
		{
			add
			{
				base.AutoValidateChanged += value;
			}
			remove
			{
				base.AutoValidateChanged -= value;
			}
		}

		/// <summary>Occurs before the form is closed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001A8 RID: 424
		// (add) Token: 0x06001A04 RID: 6660 RVA: 0x000650C0 File Offset: 0x000632C0
		// (remove) Token: 0x06001A05 RID: 6661 RVA: 0x000650D4 File Offset: 0x000632D4
		public event FormClosingEventHandler FormClosing
		{
			add
			{
				base.Events.AddHandler(Form.FormClosingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.FormClosingEvent, value);
			}
		}

		/// <summary>Occurs after the form is closed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001A9 RID: 425
		// (add) Token: 0x06001A06 RID: 6662 RVA: 0x000650E8 File Offset: 0x000632E8
		// (remove) Token: 0x06001A07 RID: 6663 RVA: 0x000650FC File Offset: 0x000632FC
		public event FormClosedEventHandler FormClosed
		{
			add
			{
				base.Events.AddHandler(Form.FormClosedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.FormClosedEvent, value);
			}
		}

		/// <summary>Occurs when the Help button is clicked.</summary>
		// Token: 0x140001AA RID: 426
		// (add) Token: 0x06001A08 RID: 6664 RVA: 0x00065110 File Offset: 0x00063310
		// (remove) Token: 0x06001A09 RID: 6665 RVA: 0x00065124 File Offset: 0x00063324
		[Browsable(true)]
		[EditorBrowsable(0)]
		public event CancelEventHandler HelpButtonClicked
		{
			add
			{
				base.Events.AddHandler(Form.HelpButtonClickedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.HelpButtonClickedEvent, value);
			}
		}

		// Token: 0x140001AB RID: 427
		// (add) Token: 0x06001A0A RID: 6666 RVA: 0x00065138 File Offset: 0x00063338
		// (remove) Token: 0x06001A0B RID: 6667 RVA: 0x00065144 File Offset: 0x00063344
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler MarginChanged
		{
			add
			{
				base.MarginChanged += value;
			}
			remove
			{
				base.MarginChanged -= value;
			}
		}

		/// <summary>Occurs after the value of the <see cref="P:System.Windows.Forms.Form.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x140001AC RID: 428
		// (add) Token: 0x06001A0C RID: 6668 RVA: 0x00065150 File Offset: 0x00063350
		// (remove) Token: 0x06001A0D RID: 6669 RVA: 0x00065164 File Offset: 0x00063364
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(Form.RightToLeftLayoutChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.RightToLeftLayoutChangedEvent, value);
			}
		}

		/// <summary>Occurs when a form enters resizing mode.</summary>
		// Token: 0x140001AD RID: 429
		// (add) Token: 0x06001A0E RID: 6670 RVA: 0x00065178 File Offset: 0x00063378
		// (remove) Token: 0x06001A0F RID: 6671 RVA: 0x0006518C File Offset: 0x0006338C
		public event EventHandler ResizeBegin
		{
			add
			{
				base.Events.AddHandler(Form.ResizeBeginEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.ResizeBeginEvent, value);
			}
		}

		/// <summary>Occurs when a form exits resizing mode.</summary>
		// Token: 0x140001AE RID: 430
		// (add) Token: 0x06001A10 RID: 6672 RVA: 0x000651A0 File Offset: 0x000633A0
		// (remove) Token: 0x06001A11 RID: 6673 RVA: 0x000651B4 File Offset: 0x000633B4
		public event EventHandler ResizeEnd
		{
			add
			{
				base.Events.AddHandler(Form.ResizeEndEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.ResizeEndEvent, value);
			}
		}

		/// <summary>Occurs whenever the form is first displayed.</summary>
		// Token: 0x140001AF RID: 431
		// (add) Token: 0x06001A12 RID: 6674 RVA: 0x000651C8 File Offset: 0x000633C8
		// (remove) Token: 0x06001A13 RID: 6675 RVA: 0x000651DC File Offset: 0x000633DC
		public event EventHandler Shown
		{
			add
			{
				base.Events.AddHandler(Form.ShownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.ShownEvent, value);
			}
		}

		// Token: 0x140001B0 RID: 432
		// (add) Token: 0x06001A14 RID: 6676 RVA: 0x000651F0 File Offset: 0x000633F0
		// (remove) Token: 0x06001A15 RID: 6677 RVA: 0x000651FC File Offset: 0x000633FC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		// Token: 0x140001B1 RID: 433
		// (add) Token: 0x06001A16 RID: 6678 RVA: 0x00065208 File Offset: 0x00063408
		// (remove) Token: 0x06001A17 RID: 6679 RVA: 0x0006521C File Offset: 0x0006341C
		internal event EventHandler UIAMenuChanged
		{
			add
			{
				base.Events.AddHandler(Form.UIAMenuChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.UIAMenuChangedEvent, value);
			}
		}

		// Token: 0x140001B2 RID: 434
		// (add) Token: 0x06001A18 RID: 6680 RVA: 0x00065230 File Offset: 0x00063430
		// (remove) Token: 0x06001A19 RID: 6681 RVA: 0x00065244 File Offset: 0x00063444
		internal event EventHandler UIATopMostChanged
		{
			add
			{
				base.Events.AddHandler(Form.UIATopMostChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.UIATopMostChangedEvent, value);
			}
		}

		// Token: 0x140001B3 RID: 435
		// (add) Token: 0x06001A1A RID: 6682 RVA: 0x00065258 File Offset: 0x00063458
		// (remove) Token: 0x06001A1B RID: 6683 RVA: 0x0006526C File Offset: 0x0006346C
		internal event EventHandler UIAWindowStateChanged
		{
			add
			{
				base.Events.AddHandler(Form.UIAWindowStateChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.UIAWindowStateChangedEvent, value);
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00065280 File Offset: 0x00063480
		internal bool IsLoaded
		{
			get
			{
				return this.is_loaded;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x00065288 File Offset: 0x00063488
		// (set) Token: 0x06001A1E RID: 6686 RVA: 0x00065290 File Offset: 0x00063490
		internal bool IsActive
		{
			get
			{
				return this.is_active;
			}
			set
			{
				if (this.is_active == value || base.IsRecreating)
				{
					return;
				}
				this.is_active = value;
				if (this.is_active)
				{
					Application.AddForm(this);
					this.OnActivated(EventArgs.Empty);
				}
				else
				{
					this.OnDeactivate(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x000652E8 File Offset: 0x000634E8
		private void ControlAddedHandler(object sender, ControlEventArgs e)
		{
			if (this.mdi_container != null)
			{
				this.mdi_container.SendToBack();
			}
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x00065300 File Offset: 0x00063500
		internal bool FireClosingEvents(CloseReason reason, bool cancel)
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs(cancel);
			this.OnClosing(cancelEventArgs);
			FormClosingEventArgs formClosingEventArgs = new FormClosingEventArgs(reason, cancelEventArgs.Cancel);
			this.OnFormClosing(formClosingEventArgs);
			return formClosingEventArgs.Cancel;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00065338 File Offset: 0x00063538
		private void FireClosedEvents(CloseReason reason)
		{
			this.OnClosed(EventArgs.Empty);
			this.OnFormClosed(new FormClosedEventArgs(reason));
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00065354 File Offset: 0x00063554
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			Size empty = Size.Empty;
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				Size size;
				if (control.AutoSize)
				{
					size = control.PreferredSize;
				}
				else
				{
					size = control.ExplicitBounds.Size;
				}
				int num = control.Bounds.X + size.Width;
				int num2 = control.Bounds.Y + size.Height;
				if (control.Dock == DockStyle.Fill)
				{
					if (num > empty.Width)
					{
						empty.Width = num;
					}
				}
				else if (control.Dock != DockStyle.Top && control.Dock != DockStyle.Bottom && num > empty.Width)
				{
					empty.Width = num + control.Margin.Right;
				}
				if (control.Dock == DockStyle.Fill)
				{
					if (num2 > empty.Height)
					{
						empty.Height = num2;
					}
				}
				else if (control.Dock != DockStyle.Left && control.Dock != DockStyle.Right && num2 > empty.Height)
				{
					empty.Height = num2 + control.Margin.Bottom;
				}
			}
			if (empty == Size.Empty)
			{
				empty.Height += base.Padding.Top;
				empty.Width += base.Padding.Left;
			}
			empty.Height += base.Padding.Bottom;
			empty.Width += base.Padding.Right;
			return this.SizeFromClientSize(empty);
		}

		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the bounds within which the control is scaled.</returns>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display bounds.</param>
		/// <param name="factor">The height and width of the control's bounds.</param>
		/// <param name="specified">One of the values of <see cref="T:System.Windows.Forms.BoundsSpecified" /> that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x06001A23 RID: 6691 RVA: 0x00065578 File Offset: 0x00063778
		[EditorBrowsable(2)]
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width)
			{
				int num = this.Size.Width - this.ClientSize.Width;
				bounds.Width = (int)Math.Round((double)((float)(bounds.Width - num) * factor.Width)) + num;
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
			{
				int num2 = this.Size.Height - this.ClientSize.Height;
				bounds.Height = (int)Math.Round((double)((float)(bounds.Height - num2) * factor.Height)) + num2;
			}
			return bounds;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0006561C File Offset: 0x0006381C
		protected override bool ProcessMnemonic(char charCode)
		{
			return base.ProcessMnemonic(charCode);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00065628 File Offset: 0x00063828
		[EditorBrowsable(2)]
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00065634 File Offset: 0x00063834
		internal void OnActivatedInternal()
		{
			this.OnActivated(EventArgs.Empty);
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00065644 File Offset: 0x00063844
		internal void OnDeactivateInternal()
		{
			this.OnDeactivate(EventArgs.Empty);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00065654 File Offset: 0x00063854
		internal override void UpdateWindowText()
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			if (this.shown_raised)
			{
				XplatUI.SetWindowStyle(this.window.Handle, this.CreateParams);
			}
			XplatUI.Text(this.Handle, this.Text.Replace(Environment.NewLine, string.Empty));
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000656B0 File Offset: 0x000638B0
		internal void SelectActiveControl()
		{
			if (this.IsMdiContainer)
			{
				this.mdi_container.SendFocusToActiveChild();
				return;
			}
			if (this.ActiveControl == null)
			{
				bool is_visible = this.is_visible;
				this.is_visible = true;
				if (!base.SelectNextControl(this, true, true, true, true))
				{
					base.Select(this);
				}
				this.is_visible = is_visible;
			}
			else
			{
				base.Select(this.ActiveControl);
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00065720 File Offset: 0x00063920
		private new void UpdateSizeGripVisible()
		{
			bool flag = false;
			switch (this.size_grip_style)
			{
			case SizeGripStyle.Auto:
				flag = this.is_modal && (this.form_border_style == FormBorderStyle.Sizable || this.form_border_style == FormBorderStyle.SizableToolWindow);
				break;
			case SizeGripStyle.Show:
				flag = this.form_border_style == FormBorderStyle.Sizable || this.form_border_style == FormBorderStyle.SizableToolWindow;
				break;
			case SizeGripStyle.Hide:
				flag = false;
				break;
			}
			if (!flag)
			{
				if (this.size_grip != null && this.size_grip.Visible)
				{
					this.size_grip.Visible = false;
				}
			}
			else
			{
				if (this.size_grip == null)
				{
					this.size_grip = new SizeGrip(this);
					this.size_grip.Virtual = true;
					this.size_grip.FillBackground = false;
				}
				this.size_grip.Visible = true;
			}
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0006580C File Offset: 0x00063A0C
		internal void ChangingParent(Control new_parent)
		{
			if (this.IsMdiChild)
			{
				return;
			}
			bool flag = false;
			if (new_parent == null)
			{
				this.window_manager = null;
			}
			else if (new_parent is MdiClient)
			{
				this.window_manager = new MdiWindowManager(this, (MdiClient)new_parent);
			}
			else
			{
				this.window_manager = new FormWindowManager(this);
				flag = true;
			}
			if (flag)
			{
				if (base.IsHandleCreated)
				{
					if (new_parent != null && new_parent.IsHandleCreated)
					{
						base.RecreateHandle();
					}
					else
					{
						this.DestroyHandle();
					}
				}
			}
			else if (base.IsHandleCreated)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (new_parent != null && new_parent.IsHandleCreated)
				{
					intPtr = new_parent.Handle;
				}
				XplatUI.SetParent(this.Handle, intPtr);
			}
			if (this.window_manager != null)
			{
				this.window_manager.UpdateWindowState(this.window_state, this.window_state, true);
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000658FC File Offset: 0x00063AFC
		internal override bool FocusInternal(bool skip_check)
		{
			if (this.IsMdiChild && !base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			return base.FocusInternal(skip_check);
		}

		/// <summary>Gets the currently active form for this application.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> that represents the currently active form, or null if there is no active form.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x0006592C File Offset: 0x00063B2C
		public static Form ActiveForm
		{
			get
			{
				Control control = Control.FromHandle(XplatUI.GetActive());
				if (control != null)
				{
					if (control is Form)
					{
						return (Form)control;
					}
					for (Control control2 = control.Parent; control2 != null; control2 = control2.Parent)
					{
						if (control2 is Form)
						{
							return (Form)control2;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Gets or sets the button on the form that is clicked when the user presses the ENTER key.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IButtonControl" /> that represents the button to use as the accept button for the form.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x00065990 File Offset: 0x00063B90
		// (set) Token: 0x06001A2F RID: 6703 RVA: 0x00065998 File Offset: 0x00063B98
		[DefaultValue(null)]
		public IButtonControl AcceptButton
		{
			get
			{
				return this.accept_button;
			}
			set
			{
				if (this.accept_button != null)
				{
					this.accept_button.NotifyDefault(false);
				}
				this.accept_button = value;
				if (this.accept_button != null)
				{
					this.accept_button.NotifyDefault(true);
				}
				this.CheckAcceptButton();
			}
		}

		/// <summary>Gets or sets a value indicating whether the opacity of the form can be adjusted.</summary>
		/// <returns>true if the opacity of the form can be changed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x000659D8 File Offset: 0x00063BD8
		// (set) Token: 0x06001A31 RID: 6705 RVA: 0x000659E0 File Offset: 0x00063BE0
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool AllowTransparency
		{
			get
			{
				return this.allow_transparency;
			}
			set
			{
				if (value == this.allow_transparency)
				{
					return;
				}
				this.allow_transparency = value;
				if (value)
				{
					if (base.IsHandleCreated)
					{
						if ((XplatUI.SupportsTransparency() & TransparencySupport.Set) != TransparencySupport.None)
						{
							XplatUI.SetWindowTransparency(this.Handle, this.Opacity, this.TransparencyKey);
						}
					}
					else
					{
						base.UpdateStyles();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the form adjusts its size to fit the height of the font used on the form and scales its controls.</summary>
		/// <returns>true if the form will automatically scale itself and its controls based on the current font assigned to the form; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x00065A40 File Offset: 0x00063C40
		// (set) Token: 0x06001A33 RID: 6707 RVA: 0x00065A48 File Offset: 0x00063C48
		[Browsable(false)]
		[EditorBrowsable(1)]
		[Obsolete("This property has been deprecated in favor of AutoScaleMode.")]
		[DesignerSerializationVisibility(0)]
		[MWFCategory("Layout")]
		public bool AutoScale
		{
			get
			{
				return this.autoscale;
			}
			set
			{
				if (value)
				{
					base.AutoScaleMode = AutoScaleMode.None;
				}
				this.autoscale = value;
			}
		}

		/// <summary>Gets or sets the base size used for autoscaling of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the base size that this form uses for autoscaling.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x00065A60 File Offset: 0x00063C60
		// (set) Token: 0x06001A35 RID: 6709 RVA: 0x00065A68 File Offset: 0x00063C68
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Localizable(true)]
		[Browsable(false)]
		public virtual Size AutoScaleBaseSize
		{
			get
			{
				return this.autoscale_base_size;
			}
			[MonoTODO("Setting this is probably unintentional and can cause Forms to be improperly sized.  See http://www.mono-project.com/FAQ:_Winforms#My_forms_are_sized_improperly for details.")]
			set
			{
				this.autoscale_base_size = value;
				this.autoscale_base_size_set = true;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form enables autoscrolling.</summary>
		/// <returns>true to enable autoscrolling on the form; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x00065A78 File Offset: 0x00063C78
		// (set) Token: 0x06001A37 RID: 6711 RVA: 0x00065A80 File Offset: 0x00063C80
		[Localizable(true)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00065A8C File Offset: 0x00063C8C
		internal bool ShouldSerializeAutoScroll()
		{
			return this.AutoScroll;
		}

		/// <summary>Resize the form according to the setting of <see cref="P:System.Windows.Forms.Form.AutoSizeMode" />.</summary>
		/// <returns>true if the form will automatically resize; false if it must be manually resized.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x00065A9C File Offset: 0x00063C9C
		// (set) Token: 0x06001A3A RID: 6714 RVA: 0x00065AA4 File Offset: 0x00063CA4
		[EditorBrowsable(0)]
		[Browsable(true)]
		[DesignerSerializationVisibility(1)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				if (base.AutoSize != value)
				{
					base.AutoSize = value;
					base.PerformLayout(this, "AutoSize");
				}
			}
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x00065AC8 File Offset: 0x00063CC8
		internal bool ShouldSerializeAutoSize()
		{
			return this.AutoSize;
		}

		/// <summary>Gets or sets the mode by which the form automatically resizes itself.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoSizeMode" /> enumerated value. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />. </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not a valid <see cref="T:System.Windows.Forms.AutoSizeMode" /> value.</exception>
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x00065AD8 File Offset: 0x00063CD8
		// (set) Token: 0x06001A3D RID: 6717 RVA: 0x00065AE0 File Offset: 0x00063CE0
		[Localizable(true)]
		[Browsable(true)]
		[DefaultValue(AutoSizeMode.GrowOnly)]
		public AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.GetAutoSizeMode();
			}
			set
			{
				if (base.GetAutoSizeMode() != value)
				{
					if (!Enum.IsDefined(typeof(AutoSizeMode), value))
					{
						throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for AutoSizeMode", value));
					}
					base.SetAutoSizeMode(value);
					base.PerformLayout(this, "AutoSizeMode");
				}
			}
		}

		/// <returns>An <see cref="T:System.Windows.Forms.AutoValidate" /> enumerated value that indicates whether contained controls are implicitly validated on focus change. The default is <see cref="F:System.Windows.Forms.AutoValidate.Inherit" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00065B3C File Offset: 0x00063D3C
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x00065B44 File Offset: 0x00063D44
		[EditorBrowsable(0)]
		[Browsable(true)]
		public override AutoValidate AutoValidate
		{
			get
			{
				return base.AutoValidate;
			}
			set
			{
				base.AutoValidate = value;
			}
		}

		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x00065B50 File Offset: 0x00063D50
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x00065B70 File Offset: 0x00063D70
		public override Color BackColor
		{
			get
			{
				if (this.background_color.IsEmpty)
				{
					return Control.DefaultBackColor;
				}
				return this.background_color;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>Gets or sets the button control that is clicked when the user presses the ESC key.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IButtonControl" /> that represents the cancel button for the form.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x00065B7C File Offset: 0x00063D7C
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x00065B84 File Offset: 0x00063D84
		[DefaultValue(null)]
		public IButtonControl CancelButton
		{
			get
			{
				return this.cancel_button;
			}
			set
			{
				this.cancel_button = value;
				if (this.cancel_button != null && this.cancel_button.DialogResult == DialogResult.None)
				{
					this.cancel_button.DialogResult = DialogResult.Cancel;
				}
			}
		}

		/// <summary>Gets or sets the size of the client area of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the form's client area.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x00065BC0 File Offset: 0x00063DC0
		// (set) Token: 0x06001A45 RID: 6725 RVA: 0x00065BC8 File Offset: 0x00063DC8
		[DesignerSerializationVisibility(1)]
		[Localizable(true)]
		public new Size ClientSize
		{
			get
			{
				return base.ClientSize;
			}
			set
			{
				this.is_clientsize_set = true;
				base.ClientSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a control box is displayed in the caption bar of the form.</summary>
		/// <returns>true if the form displays a control box in the upper left corner of the form; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x00065BD8 File Offset: 0x00063DD8
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x00065BE0 File Offset: 0x00063DE0
		[DefaultValue(true)]
		[MWFCategory("Window Style")]
		public bool ControlBox
		{
			get
			{
				return this.control_box;
			}
			set
			{
				if (this.control_box != value)
				{
					this.control_box = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets the size and location of the form on the Windows desktop.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the form on the Windows desktop using desktop coordinates.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x00065BFC File Offset: 0x00063DFC
		// (set) Token: 0x06001A49 RID: 6729 RVA: 0x00065C10 File Offset: 0x00063E10
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Rectangle DesktopBounds
		{
			get
			{
				return new Rectangle(this.Location, this.Size);
			}
			set
			{
				base.Bounds = value;
			}
		}

		/// <summary>Gets or sets the location of the form on the Windows desktop.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the location of the form on the desktop.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x00065C1C File Offset: 0x00063E1C
		// (set) Token: 0x06001A4B RID: 6731 RVA: 0x00065C24 File Offset: 0x00063E24
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public Point DesktopLocation
		{
			get
			{
				return this.Location;
			}
			set
			{
				this.Location = value;
			}
		}

		/// <summary>Gets or sets the dialog result for the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DialogResult" /> that represents the result of the form when used as a dialog box.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00065C30 File Offset: 0x00063E30
		// (set) Token: 0x06001A4D RID: 6733 RVA: 0x00065C38 File Offset: 0x00063E38
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public DialogResult DialogResult
		{
			get
			{
				return this.dialog_result;
			}
			set
			{
				if (value < DialogResult.None || value > DialogResult.No)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DialogResult));
				}
				this.dialog_result = value;
				this.closing = this.dialog_result != DialogResult.None && this.is_modal;
			}
		}

		/// <summary>Gets or sets the border style of the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormBorderStyle" /> that represents the style of border to display for the form. The default is FormBorderStyle.Sizable.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x00065C8C File Offset: 0x00063E8C
		// (set) Token: 0x06001A4F RID: 6735 RVA: 0x00065C94 File Offset: 0x00063E94
		[DispId(-504)]
		[MWFCategory("Appearance")]
		[DefaultValue(FormBorderStyle.Sizable)]
		public FormBorderStyle FormBorderStyle
		{
			get
			{
				return this.form_border_style;
			}
			set
			{
				this.form_border_style = value;
				if (this.window_manager == null)
				{
					if (base.IsHandleCreated)
					{
						XplatUI.SetBorderStyle(this.window.Handle, this.form_border_style);
					}
				}
				else
				{
					this.window_manager.UpdateBorderStyle(value);
				}
				Size clientSize = this.ClientSize;
				base.UpdateStyles();
				if (base.IsHandleCreated)
				{
					this.Size = base.InternalSizeFromClientSize(clientSize);
					XplatUI.InvalidateNC(this.Handle);
				}
				else if (this.is_clientsize_set)
				{
					this.Size = base.InternalSizeFromClientSize(clientSize);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a Help button should be displayed in the caption box of the form.</summary>
		/// <returns>true to display a Help button in the form's caption bar; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x00065D34 File Offset: 0x00063F34
		// (set) Token: 0x06001A51 RID: 6737 RVA: 0x00065D3C File Offset: 0x00063F3C
		[MWFCategory("Window Style")]
		[DefaultValue(false)]
		public bool HelpButton
		{
			get
			{
				return this.help_button;
			}
			set
			{
				if (this.help_button != value)
				{
					this.help_button = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets the icon for the form.</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> that represents the icon for the form.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x00065D58 File Offset: 0x00063F58
		// (set) Token: 0x06001A53 RID: 6739 RVA: 0x00065D60 File Offset: 0x00063F60
		[Localizable(true)]
		[MWFCategory("Window Style")]
		[AmbientValue(null)]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (value == null)
				{
					value = Form.default_icon;
				}
				if (this.icon == value)
				{
					return;
				}
				this.icon = value;
				if (base.IsHandleCreated)
				{
					XplatUI.SetIcon(this.Handle, this.icon);
				}
			}
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00065DA0 File Offset: 0x00063FA0
		internal bool ShouldSerializeIcon()
		{
			return this.Icon != Form.default_icon;
		}

		/// <summary>Gets a value indicating whether the form is a multiple-document interface (MDI) child form.</summary>
		/// <returns>true if the form is an MDI child form; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x00065DB4 File Offset: 0x00063FB4
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool IsMdiChild
		{
			get
			{
				return this.mdi_parent != null;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form is a container for multiple-document interface (MDI) child forms.</summary>
		/// <returns>true if the form is a container for MDI child forms; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x00065DC4 File Offset: 0x00063FC4
		// (set) Token: 0x06001A57 RID: 6743 RVA: 0x00065DD4 File Offset: 0x00063FD4
		[DefaultValue(false)]
		[MWFCategory("Window Style")]
		public bool IsMdiContainer
		{
			get
			{
				return this.mdi_container != null;
			}
			set
			{
				if (value && this.mdi_container == null)
				{
					this.mdi_container = new MdiClient();
					base.Controls.Add(this.mdi_container);
					base.ControlAdded += this.ControlAddedHandler;
					this.mdi_container.SendToBack();
					this.mdi_container.SetParentText(true);
				}
				else if (!value && this.mdi_container != null)
				{
					base.Controls.Remove(this.mdi_container);
					this.mdi_container = null;
				}
			}
		}

		/// <summary>Gets the currently active multiple-document interface (MDI) child window.</summary>
		/// <returns>Returns a <see cref="T:System.Windows.Forms.Form" /> that represents the currently active MDI child window, or null if there are currently no child windows present.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x00065E68 File Offset: 0x00064068
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Form ActiveMdiChild
		{
			get
			{
				if (!this.IsMdiContainer)
				{
					return null;
				}
				return this.mdi_container.ActiveMdiChild;
			}
		}

		/// <summary>Gets a value indicating whether the form can use all windows and user input events without restriction.</summary>
		/// <returns>true if the form has restrictions; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x00065E84 File Offset: 0x00064084
		[EditorBrowsable(2)]
		[Browsable(false)]
		public bool IsRestrictedWindow
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form will receive key events before the event is passed to the control that has focus.</summary>
		/// <returns>true if the form will receive all key events; false if the currently selected control on the form receives key events. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x00065E88 File Offset: 0x00064088
		// (set) Token: 0x06001A5B RID: 6747 RVA: 0x00065E90 File Offset: 0x00064090
		[DefaultValue(false)]
		public bool KeyPreview
		{
			get
			{
				return this.key_preview;
			}
			set
			{
				this.key_preview = value;
			}
		}

		/// <summary>Gets or sets the primary menu container for the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuStrip" /> that represents the container for the menu structure of the form. The default is null.</returns>
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x00065E9C File Offset: 0x0006409C
		// (set) Token: 0x06001A5D RID: 6749 RVA: 0x00065EA4 File Offset: 0x000640A4
		[TypeConverter(typeof(ReferenceConverter))]
		[DefaultValue(null)]
		public MenuStrip MainMenuStrip
		{
			get
			{
				return this.main_menu_strip;
			}
			set
			{
				if (this.main_menu_strip != value)
				{
					this.main_menu_strip = value;
					this.main_menu_strip.RefreshMdiItems();
				}
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x00065EC4 File Offset: 0x000640C4
		// (set) Token: 0x06001A5F RID: 6751 RVA: 0x00065ECC File Offset: 0x000640CC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new Padding Margin
		{
			get
			{
				return base.Margin;
			}
			set
			{
				base.Margin = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Maximize button is displayed in the caption bar of the form.</summary>
		/// <returns>true to display a Maximize button for the form; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x00065ED8 File Offset: 0x000640D8
		// (set) Token: 0x06001A61 RID: 6753 RVA: 0x00065EE0 File Offset: 0x000640E0
		[MWFCategory("Window Style")]
		[DefaultValue(true)]
		public bool MaximizeBox
		{
			get
			{
				return this.maximize_box;
			}
			set
			{
				if (this.maximize_box != value)
				{
					this.maximize_box = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets the maximum size the form can be resized to.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the maximum size for the form.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size" /> object are less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x00065EFC File Offset: 0x000640FC
		// (set) Token: 0x06001A63 RID: 6755 RVA: 0x00065F04 File Offset: 0x00064104
		[DefaultValue(typeof(Size), "0, 0")]
		[RefreshProperties(2)]
		[Localizable(true)]
		[MWFCategory("Layout")]
		public override Size MaximumSize
		{
			get
			{
				return this.maximum_size;
			}
			set
			{
				if (this.maximum_size != value)
				{
					this.maximum_size = value;
					if (!this.minimum_size.IsEmpty)
					{
						if (this.maximum_size.Width <= this.minimum_size.Width)
						{
							this.minimum_size.Width = this.maximum_size.Width;
						}
						if (this.maximum_size.Height <= this.minimum_size.Height)
						{
							this.minimum_size.Height = this.maximum_size.Height;
						}
					}
					this.OnMaximumSizeChanged(EventArgs.Empty);
					if (base.IsHandleCreated)
					{
						XplatUI.SetWindowMinMax(this.Handle, this.maximized_bounds, this.minimum_size, this.maximum_size);
					}
				}
			}
		}

		/// <summary>Gets an array of forms that represent the multiple-document interface (MDI) child forms that are parented to this form.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.Form" /> objects, each of which identifies one of this form's MDI child forms.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x00065FD0 File Offset: 0x000641D0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Form[] MdiChildren
		{
			get
			{
				if (this.mdi_container != null)
				{
					return this.mdi_container.MdiChildren;
				}
				return new Form[0];
			}
		}

		/// <summary>Gets or sets the current multiple-document interface (MDI) parent form of this form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> that represents the MDI parent form.</returns>
		/// <exception cref="T:System.Exception">The <see cref="T:System.Windows.Forms.Form" /> assigned to this property is not marked as an MDI container.-or- The <see cref="T:System.Windows.Forms.Form" /> assigned to this property is both a child and an MDI container form.-or- The <see cref="T:System.Windows.Forms.Form" /> assigned to this property is located on a different thread. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x00065FF0 File Offset: 0x000641F0
		// (set) Token: 0x06001A66 RID: 6758 RVA: 0x00065FF8 File Offset: 0x000641F8
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Form MdiParent
		{
			get
			{
				return this.mdi_parent;
			}
			set
			{
				if (value == this.mdi_parent)
				{
					return;
				}
				if (value != null && !value.IsMdiContainer)
				{
					throw new ArgumentException("Form that was specified to be the MdiParent for this form is not an MdiContainer.");
				}
				if (this.mdi_parent != null)
				{
					this.mdi_parent.MdiContainer.Controls.Remove(this);
				}
				if (value != null)
				{
					this.mdi_parent = value;
					if (this.window_manager == null)
					{
						this.window_manager = new MdiWindowManager(this, this.mdi_parent.MdiContainer);
					}
					this.mdi_parent.MdiContainer.Controls.Add(this);
					this.mdi_parent.MdiContainer.Controls.SetChildIndex(this, 0);
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
				else if (this.mdi_parent != null)
				{
					this.mdi_parent = null;
					this.window_manager = null;
					this.FormBorderStyle = this.form_border_style;
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
				this.is_toplevel = this.mdi_parent == null;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x00066108 File Offset: 0x00064308
		internal MdiClient MdiContainer
		{
			get
			{
				return this.mdi_container;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x00066110 File Offset: 0x00064310
		internal InternalWindowManager WindowManager
		{
			get
			{
				return this.window_manager;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.MainMenu" /> that is displayed in the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the menu to display in the form.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00066118 File Offset: 0x00064318
		// (set) Token: 0x06001A6A RID: 6762 RVA: 0x00066120 File Offset: 0x00064320
		[Browsable(false)]
		[MWFCategory("Window Style")]
		[DefaultValue(null)]
		[TypeConverter(typeof(ReferenceConverter))]
		public MainMenu Menu
		{
			get
			{
				return this.menu;
			}
			set
			{
				if (this.menu != value)
				{
					this.menu = value;
					if (this.menu != null && !this.IsMdiChild)
					{
						this.menu.SetForm(this);
						if (base.IsHandleCreated)
						{
							XplatUI.SetMenu(this.window.Handle, this.menu);
						}
						if (this.clientsize_set != Size.Empty)
						{
							this.SetClientSizeCore(this.clientsize_set.Width, this.clientsize_set.Height);
						}
						else
						{
							base.UpdateBounds(this.bounds.X, this.bounds.Y, this.bounds.Width, this.bounds.Height, this.ClientSize.Width, this.ClientSize.Height - ThemeEngine.Current.CalcMenuBarSize(base.DeviceContext, this.menu, this.ClientSize.Width));
						}
					}
					else
					{
						base.UpdateBounds();
					}
					this.OnUIAMenuChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the merged menu for the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the merged menu of the form.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x00066244 File Offset: 0x00064444
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		public MainMenu MergedMenu
		{
			get
			{
				if (!this.IsMdiChild || this.window_manager == null)
				{
					return null;
				}
				return ((MdiWindowManager)this.window_manager).MergedMenu;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x0006627C File Offset: 0x0006447C
		internal MainMenu ActiveMenu
		{
			get
			{
				if (this.IsMdiChild)
				{
					return null;
				}
				if (this.IsMdiContainer && this.mdi_container.Controls.Count > 0 && ((Form)this.mdi_container.Controls[0]).WindowState == FormWindowState.Maximized)
				{
					MdiWindowManager mdiWindowManager = (MdiWindowManager)((Form)this.mdi_container.Controls[0]).WindowManager;
					return mdiWindowManager.MaximizedMenu;
				}
				Form activeMdiChild = this.ActiveMdiChild;
				if (activeMdiChild == null || activeMdiChild.Menu == null)
				{
					return this.menu;
				}
				return activeMdiChild.MergedMenu;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x00066328 File Offset: 0x00064528
		internal MdiWindowManager ActiveMaximizedMdiChild
		{
			get
			{
				Form activeMdiChild = this.ActiveMdiChild;
				if (activeMdiChild == null)
				{
					return null;
				}
				if (activeMdiChild.WindowManager == null || activeMdiChild.window_state != FormWindowState.Maximized)
				{
					return null;
				}
				return (MdiWindowManager)activeMdiChild.WindowManager;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Minimize button is displayed in the caption bar of the form.</summary>
		/// <returns>true to display a Minimize button for the form; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x00066368 File Offset: 0x00064568
		// (set) Token: 0x06001A6F RID: 6767 RVA: 0x00066370 File Offset: 0x00064570
		[MWFCategory("Window Style")]
		[DefaultValue(true)]
		public bool MinimizeBox
		{
			get
			{
				return this.minimize_box;
			}
			set
			{
				if (this.minimize_box != value)
				{
					this.minimize_box = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets the minimum size the form can be resized to.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum size for the form.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size" /> object are less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0006638C File Offset: 0x0006458C
		// (set) Token: 0x06001A71 RID: 6769 RVA: 0x00066394 File Offset: 0x00064594
		[MWFCategory("Layout")]
		[RefreshProperties(2)]
		[Localizable(true)]
		public override Size MinimumSize
		{
			get
			{
				return this.minimum_size;
			}
			set
			{
				if (this.minimum_size != value)
				{
					this.minimum_size = value;
					if (!this.maximum_size.IsEmpty)
					{
						if (this.minimum_size.Width >= this.maximum_size.Width)
						{
							this.maximum_size.Width = this.minimum_size.Width;
						}
						if (this.minimum_size.Height >= this.maximum_size.Height)
						{
							this.maximum_size.Height = this.minimum_size.Height;
						}
					}
					if (this.Size.Width < value.Width || this.Size.Height < value.Height)
					{
						this.Size = new Size(Math.Max(this.Size.Width, value.Width), Math.Max(this.Size.Height, value.Height));
					}
					this.OnMinimumSizeChanged(EventArgs.Empty);
					if (base.IsHandleCreated)
					{
						XplatUI.SetWindowMinMax(this.Handle, this.maximized_bounds, this.minimum_size, this.maximum_size);
					}
				}
			}
		}

		/// <summary>Gets a value indicating whether this form is displayed modally.</summary>
		/// <returns>true if the form is displayed modally; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x000664D4 File Offset: 0x000646D4
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool Modal
		{
			get
			{
				return this.is_modal;
			}
		}

		/// <summary>Gets or sets the opacity level of the form.</summary>
		/// <returns>The level of opacity for the form. The default is 1.00.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x000664DC File Offset: 0x000646DC
		// (set) Token: 0x06001A74 RID: 6772 RVA: 0x00066514 File Offset: 0x00064714
		[TypeConverter(typeof(OpacityConverter))]
		[MWFCategory("Window Style")]
		[DefaultValue(1.0)]
		public double Opacity
		{
			get
			{
				if (base.IsHandleCreated && (XplatUI.SupportsTransparency() & TransparencySupport.Get) != TransparencySupport.None)
				{
					return XplatUI.GetWindowTransparency(this.Handle);
				}
				return this.opacity;
			}
			set
			{
				this.opacity = value;
				if (this.opacity < 0.0)
				{
					this.opacity = 0.0;
				}
				if (this.opacity > 1.0)
				{
					this.opacity = 1.0;
				}
				this.AllowTransparency = true;
				if (base.IsHandleCreated)
				{
					base.UpdateStyles();
					if ((XplatUI.SupportsTransparency() & TransparencySupport.Set) != TransparencySupport.None)
					{
						XplatUI.SetWindowTransparency(this.Handle, this.opacity, this.TransparencyKey);
					}
				}
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Windows.Forms.Form" /> objects that represent all forms that are owned by this form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> array that represents the owned forms for this form.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x000665AC File Offset: 0x000647AC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public Form[] OwnedForms
		{
			get
			{
				Form[] array = new Form[this.owned_forms.Count];
				for (int i = 0; i < this.owned_forms.Count; i++)
				{
					array[i] = (Form)this.owned_forms[i];
				}
				return array;
			}
		}

		/// <summary>Gets or sets the form that owns this form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> that represents the form that is the owner of this form.</returns>
		/// <exception cref="T:System.Exception">A top-level window cannot have an owner. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x000665FC File Offset: 0x000647FC
		// (set) Token: 0x06001A77 RID: 6775 RVA: 0x00066604 File Offset: 0x00064804
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public Form Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				if (this.owner != value)
				{
					if (this.owner != null)
					{
						this.owner.RemoveOwnedForm(this);
					}
					this.owner = value;
					if (this.owner != null)
					{
						this.owner.AddOwnedForm(this);
					}
					if (base.IsHandleCreated)
					{
						if (this.owner != null && this.owner.IsHandleCreated)
						{
							XplatUI.SetOwner(this.window.Handle, this.owner.window.Handle);
						}
						else
						{
							XplatUI.SetOwner(this.window.Handle, IntPtr.Zero);
						}
					}
				}
			}
		}

		/// <summary>Gets the location and size of the form in its normal window state.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the location and size of the form in the normal window state.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x000666B4 File Offset: 0x000648B4
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public Rectangle RestoreBounds
		{
			get
			{
				return this.restore_bounds;
			}
		}

		/// <summary>Gets or sets a value indicating whether right-to-left mirror placement is turned on.</summary>
		/// <returns>true if right-to-left mirror placement is turned on; otherwise, false for standard child control placement. The default is false.</returns>
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x000666BC File Offset: 0x000648BC
		// (set) Token: 0x06001A7A RID: 6778 RVA: 0x000666C4 File Offset: 0x000648C4
		[Localizable(true)]
		[DefaultValue(false)]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.right_to_left_layout;
			}
			set
			{
				this.right_to_left_layout = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether an icon is displayed in the caption bar of the form.</summary>
		/// <returns>true if the form displays an icon in the caption bar; otherwise, false. The default is true.</returns>
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x000666D0 File Offset: 0x000648D0
		// (set) Token: 0x06001A7C RID: 6780 RVA: 0x000666D8 File Offset: 0x000648D8
		[DefaultValue(true)]
		public bool ShowIcon
		{
			get
			{
				return this.show_icon;
			}
			set
			{
				if (this.show_icon != value)
				{
					this.show_icon = value;
					base.UpdateStyles();
					if (base.IsHandleCreated)
					{
						XplatUI.SetIcon(this.Handle, (!value) ? null : this.Icon);
						XplatUI.InvalidateNC(this.Handle);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the form is displayed in the Windows taskbar.</summary>
		/// <returns>true to display the form in the Windows taskbar at run time; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x00066734 File Offset: 0x00064934
		// (set) Token: 0x06001A7E RID: 6782 RVA: 0x0006673C File Offset: 0x0006493C
		[MWFCategory("Window Style")]
		[DefaultValue(true)]
		public bool ShowInTaskbar
		{
			get
			{
				return this.show_in_taskbar;
			}
			set
			{
				if (this.show_in_taskbar != value)
				{
					this.show_in_taskbar = value;
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets the size of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the form.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x00066774 File Offset: 0x00064974
		// (set) Token: 0x06001A80 RID: 6784 RVA: 0x0006677C File Offset: 0x0006497C
		[DesignerSerializationVisibility(0)]
		[Localizable(false)]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		/// <summary>Gets or sets the style of the size grip to display in the lower-right corner of the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.SizeGripStyle" /> that represents the style of the size grip to display. The default is <see cref="F:System.Windows.Forms.SizeGripStyle.Auto" /></returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x00066788 File Offset: 0x00064988
		// (set) Token: 0x06001A82 RID: 6786 RVA: 0x00066790 File Offset: 0x00064990
		[DefaultValue(SizeGripStyle.Auto)]
		[MWFCategory("Window Style")]
		public SizeGripStyle SizeGripStyle
		{
			get
			{
				return this.size_grip_style;
			}
			set
			{
				this.size_grip_style = value;
				this.UpdateSizeGripVisible();
			}
		}

		/// <summary>Gets or sets the starting position of the form at run time.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormStartPosition" /> that represents the starting position of the form.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x000667A0 File Offset: 0x000649A0
		// (set) Token: 0x06001A84 RID: 6788 RVA: 0x000667A8 File Offset: 0x000649A8
		[MWFCategory("Layout")]
		[Localizable(true)]
		[DefaultValue(FormStartPosition.WindowsDefaultLocation)]
		public FormStartPosition StartPosition
		{
			get
			{
				return this.start_position;
			}
			set
			{
				this.start_position = value;
			}
		}

		/// <summary>Gets or sets the tab order of the control within its container.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the index of the control within the set of controls within its container that is included in the tab order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x000667B4 File Offset: 0x000649B4
		// (set) Token: 0x06001A86 RID: 6790 RVA: 0x000667BC File Offset: 0x000649BC
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new int TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x000667C8 File Offset: 0x000649C8
		// (set) Token: 0x06001A88 RID: 6792 RVA: 0x000667D0 File Offset: 0x000649D0
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DefaultValue(true)]
		[DispId(-516)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to display the form as a top-level window.</summary>
		/// <returns>true to display the form as a top-level window; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.Exception">A Multiple-document interface (MDI) parent form must be a top-level window. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x000667DC File Offset: 0x000649DC
		// (set) Token: 0x06001A8A RID: 6794 RVA: 0x000667E4 File Offset: 0x000649E4
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool TopLevel
		{
			get
			{
				return base.GetTopLevel();
			}
			set
			{
				if (!value && this.IsMdiContainer)
				{
					throw new ArgumentException("MDI Container forms must be top level.");
				}
				base.SetTopLevel(value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the form should be displayed as a topmost form.</summary>
		/// <returns>true to display the form as a topmost form; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x0006680C File Offset: 0x00064A0C
		// (set) Token: 0x06001A8C RID: 6796 RVA: 0x00066814 File Offset: 0x00064A14
		[DefaultValue(false)]
		[MWFCategory("Window Style")]
		public bool TopMost
		{
			get
			{
				return this.topmost;
			}
			set
			{
				if (this.topmost != value)
				{
					this.topmost = value;
					if (base.IsHandleCreated)
					{
						XplatUI.SetTopmost(this.window.Handle, value);
					}
					this.OnUIATopMostChanged();
				}
			}
		}

		/// <summary>Gets or sets the color that will represent transparent areas of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to display transparently on the form.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x00066858 File Offset: 0x00064A58
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x00066860 File Offset: 0x00064A60
		[MWFCategory("Window Style")]
		public Color TransparencyKey
		{
			get
			{
				return this.transparency_key;
			}
			set
			{
				this.transparency_key = value;
				this.AllowTransparency = true;
				base.UpdateStyles();
				if (base.IsHandleCreated && (XplatUI.SupportsTransparency() & TransparencySupport.Set) != TransparencySupport.None)
				{
					XplatUI.SetWindowTransparency(this.Handle, this.Opacity, this.transparency_key);
				}
			}
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000668B0 File Offset: 0x00064AB0
		internal bool ShouldSerializeTransparencyKey()
		{
			return this.TransparencyKey != Color.Empty;
		}

		/// <summary>Gets or sets a value that indicates whether form is minimized, maximized, or normal.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormWindowState" /> that represents whether form is minimized, maximized, or normal. The default is FormWindowState.Normal.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x000668C4 File Offset: 0x00064AC4
		// (set) Token: 0x06001A91 RID: 6801 RVA: 0x00066920 File Offset: 0x00064B20
		[MWFCategory("Layout")]
		[DefaultValue(FormWindowState.Normal)]
		public FormWindowState WindowState
		{
			get
			{
				if (base.IsHandleCreated && this.shown_raised)
				{
					if (this.window_manager != null)
					{
						return this.window_manager.GetWindowState();
					}
					FormWindowState windowState = XplatUI.GetWindowState(this.Handle);
					if (windowState != (FormWindowState)(-1))
					{
						this.window_state = windowState;
					}
				}
				return this.window_state;
			}
			set
			{
				FormWindowState formWindowState = this.window_state;
				this.window_state = value;
				if (base.IsHandleCreated && this.shown_raised)
				{
					if (this.window_manager != null)
					{
						this.window_manager.SetWindowState(formWindowState, value);
						return;
					}
					XplatUI.SetWindowState(this.Handle, value);
				}
				if (formWindowState != this.window_state)
				{
					this.OnUIAWindowStateChanged();
				}
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x00066988 File Offset: 0x00064B88
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = new CreateParams();
				if (this.Text != null)
				{
					createParams.Caption = this.Text.Replace(Environment.NewLine, string.Empty);
				}
				createParams.ClassName = XplatUI.DefaultClassName;
				createParams.ClassStyle = 0;
				createParams.Style = 0;
				createParams.ExStyle = 0;
				createParams.Param = 0;
				createParams.Parent = IntPtr.Zero;
				createParams.menu = this.ActiveMenu;
				createParams.control = this;
				if ((base.Parent != null || !this.TopLevel) && !this.IsMdiChild)
				{
					createParams.X = base.Left;
					createParams.Y = base.Top;
				}
				else
				{
					switch (this.start_position)
					{
					case FormStartPosition.Manual:
						createParams.X = base.Left;
						createParams.Y = base.Top;
						break;
					case FormStartPosition.CenterScreen:
						if (this.IsMdiChild)
						{
							createParams.X = Math.Max((this.MdiParent.mdi_container.ClientSize.Width - base.Width) / 2, 0);
							createParams.Y = Math.Max((this.MdiParent.mdi_container.ClientSize.Height - base.Height) / 2, 0);
						}
						else
						{
							createParams.X = Math.Max((Screen.PrimaryScreen.WorkingArea.Width - base.Width) / 2, 0);
							createParams.Y = Math.Max((Screen.PrimaryScreen.WorkingArea.Height - base.Height) / 2, 0);
						}
						break;
					case FormStartPosition.WindowsDefaultLocation:
					case FormStartPosition.WindowsDefaultBounds:
					case FormStartPosition.CenterParent:
						createParams.X = int.MinValue;
						createParams.Y = int.MinValue;
						break;
					}
				}
				createParams.Width = base.Width;
				createParams.Height = base.Height;
				createParams.Style = 33554432;
				if (!this.Modal)
				{
					createParams.WindowStyle |= WindowStyles.WS_CLIPSIBLINGS;
				}
				if (base.Parent != null && base.Parent.IsHandleCreated)
				{
					createParams.Parent = base.Parent.Handle;
					createParams.Style |= 1073741824;
				}
				if (this.IsMdiChild)
				{
					createParams.Style |= 1086324736;
					if (base.Parent != null)
					{
						createParams.Parent = base.Parent.Handle;
					}
					createParams.ExStyle |= 320;
					FormBorderStyle formBorderStyle = this.FormBorderStyle;
					if (formBorderStyle != FormBorderStyle.FixedToolWindow && formBorderStyle != FormBorderStyle.SizableToolWindow)
					{
						if (formBorderStyle == FormBorderStyle.None)
						{
							goto IL_02F3;
						}
					}
					else
					{
						createParams.ExStyle |= 128;
					}
					createParams.Style |= 13565952;
					IL_02F3:;
				}
				else
				{
					switch (this.FormBorderStyle)
					{
					case FormBorderStyle.FixedSingle:
						createParams.Style |= 12582912;
						break;
					case FormBorderStyle.Fixed3D:
						createParams.Style |= 12582912;
						createParams.ExStyle |= 512;
						break;
					case FormBorderStyle.FixedDialog:
						createParams.Style |= 12582912;
						createParams.ExStyle |= 65537;
						break;
					case FormBorderStyle.Sizable:
						createParams.Style |= 12845056;
						break;
					case FormBorderStyle.FixedToolWindow:
						createParams.Style |= 12582912;
						createParams.ExStyle |= 128;
						break;
					case FormBorderStyle.SizableToolWindow:
						createParams.Style |= 12845056;
						createParams.ExStyle |= 128;
						break;
					}
				}
				FormWindowState formWindowState = this.window_state;
				if (formWindowState != FormWindowState.Minimized)
				{
					if (formWindowState == FormWindowState.Maximized)
					{
						createParams.Style |= 16777216;
					}
				}
				else
				{
					createParams.Style |= 536870912;
				}
				if (this.TopMost)
				{
					createParams.ExStyle |= 8;
				}
				if (this.ShowInTaskbar)
				{
					createParams.ExStyle |= 262144;
				}
				if (this.MaximizeBox)
				{
					createParams.Style |= 65536;
				}
				if (this.MinimizeBox)
				{
					createParams.Style |= 131072;
				}
				if (this.ControlBox)
				{
					createParams.Style |= 524288;
				}
				if (!this.show_icon)
				{
					createParams.ExStyle |= 1;
				}
				createParams.ExStyle |= 65536;
				if (this.HelpButton && !this.MaximizeBox && !this.MinimizeBox)
				{
					createParams.ExStyle |= 1024;
				}
				int platform = Environment.OSVersion.Platform;
				bool flag = platform == 128 || platform == 4 || platform == 6;
				if ((base.VisibleInternal && (this.is_changing_visible_state == 0 || flag)) || base.IsRecreating)
				{
					createParams.Style |= 268435456;
				}
				if (this.opacity < 1.0 || this.TransparencyKey != Color.Empty)
				{
					createParams.ExStyle |= 524288;
				}
				if (!this.is_enabled && this.context == null)
				{
					createParams.Style |= 134217728;
				}
				if (!this.ControlBox && this.Text == string.Empty)
				{
					createParams.WindowStyle &= ~WindowStyles.WS_DLGFRAME;
				}
				return createParams;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x00066FC0 File Offset: 0x000651C0
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.NoControl;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x00066FC4 File Offset: 0x000651C4
		protected override Size DefaultSize
		{
			get
			{
				return new Size(300, 300);
			}
		}

		/// <summary>Gets and sets the size of the form when it is maximized.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the form when it is maximized.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Drawing.Rectangle.Top" /> property is greater than the height of the form.-or- The value of the <see cref="P:System.Drawing.Rectangle.Left" /> property is greater than the width of the form. </exception>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x00066FD8 File Offset: 0x000651D8
		// (set) Token: 0x06001A96 RID: 6806 RVA: 0x00067008 File Offset: 0x00065208
		protected Rectangle MaximizedBounds
		{
			get
			{
				if (this.maximized_bounds != Rectangle.Empty)
				{
					return this.maximized_bounds;
				}
				return this.default_maximized_bounds;
			}
			set
			{
				this.maximized_bounds = value;
				this.OnMaximizedBoundsChanged(EventArgs.Empty);
				if (base.IsHandleCreated)
				{
					XplatUI.SetWindowMinMax(this.Handle, this.maximized_bounds, this.minimum_size, this.maximum_size);
				}
			}
		}

		/// <summary>Gets a value indicating whether the window will be activated when it is shown.</summary>
		/// <returns>True if the window will not be activated when it is shown; otherwise, false. The default is false.</returns>
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x00067050 File Offset: 0x00065250
		[MonoTODO("Implemented for Win32, needs X11 implementation")]
		[Browsable(false)]
		protected virtual bool ShowWithoutActivation
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the size when autoscaling the form based on a specified font.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> representing the autoscaled size of the form.</returns>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> representing the font to determine the autoscaled base size of the form. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A98 RID: 6808 RVA: 0x00067054 File Offset: 0x00065254
		[Obsolete("This method has been deprecated.  Use AutoScaleDimensions instead")]
		[EditorBrowsable(1)]
		public static SizeF GetAutoScaleSize(Font font)
		{
			return XplatUI.GetAutoScaleSize(font);
		}

		/// <summary>Activates the form and gives it focus.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001A99 RID: 6809 RVA: 0x0006705C File Offset: 0x0006525C
		public void Activate()
		{
			if (base.IsHandleCreated)
			{
				if (this.IsMdiChild)
				{
					this.MdiParent.ActivateMdiChild(this);
				}
				else if (this.IsMdiContainer)
				{
					this.mdi_container.SendFocusToActiveChild();
				}
				else
				{
					XplatUI.Activate(this.window.Handle);
				}
			}
		}

		/// <summary>Adds an owned form to this form.</summary>
		/// <param name="ownedForm">The <see cref="T:System.Windows.Forms.Form" /> that this form will own. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001A9A RID: 6810 RVA: 0x000670BC File Offset: 0x000652BC
		public void AddOwnedForm(Form ownedForm)
		{
			if (!this.owned_forms.Contains(ownedForm))
			{
				this.owned_forms.Add(ownedForm);
			}
			ownedForm.Owner = this;
		}

		/// <summary>Closes the form.</summary>
		/// <exception cref="T:System.InvalidOperationException">The form was closed while a handle was being created. </exception>
		/// <exception cref="T:System.ObjectDisposedException">You cannot call this method from the <see cref="E:System.Windows.Forms.Form.Activated" /> event when <see cref="P:System.Windows.Forms.Form.WindowState" /> is set to <see cref="F:System.Windows.Forms.FormWindowState.Maximized" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001A9B RID: 6811 RVA: 0x000670F0 File Offset: 0x000652F0
		public void Close()
		{
			if (base.IsDisposed)
			{
				return;
			}
			if (!base.IsHandleCreated)
			{
				base.Dispose();
				return;
			}
			if (this.Menu != null)
			{
				XplatUI.SetMenu(this.window.Handle, null);
			}
			XplatUI.SendMessage(this.Handle, Msg.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
			this.closed = true;
		}

		/// <summary>Arranges the multiple-document interface (MDI) child forms within the MDI parent form.</summary>
		/// <param name="value">One of the <see cref="T:System.Windows.Forms.MdiLayout" /> values that defines the layout of MDI child forms. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001A9C RID: 6812 RVA: 0x00067158 File Offset: 0x00065358
		public void LayoutMdi(MdiLayout value)
		{
			if (this.mdi_container != null)
			{
				this.mdi_container.LayoutMdi(value);
			}
		}

		/// <summary>Removes an owned form from this form.</summary>
		/// <param name="ownedForm">A <see cref="T:System.Windows.Forms.Form" /> representing the form to remove from the list of owned forms for this form. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001A9D RID: 6813 RVA: 0x00067174 File Offset: 0x00065374
		public void RemoveOwnedForm(Form ownedForm)
		{
			this.owned_forms.Remove(ownedForm);
		}

		/// <summary>Sets the bounds of the form in desktop coordinates.</summary>
		/// <param name="x">The x-coordinate of the form's location. </param>
		/// <param name="y">The y-coordinate of the form's location. </param>
		/// <param name="width">The width of the form. </param>
		/// <param name="height">The height of the form. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001A9E RID: 6814 RVA: 0x00067184 File Offset: 0x00065384
		public void SetDesktopBounds(int x, int y, int width, int height)
		{
			this.DesktopBounds = new Rectangle(x, y, width, height);
		}

		/// <summary>Sets the location of the form in desktop coordinates.</summary>
		/// <param name="x">The x-coordinate of the form's location. </param>
		/// <param name="y">The y-coordinate of the form's location. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001A9F RID: 6815 RVA: 0x00067198 File Offset: 0x00065398
		public void SetDesktopLocation(int x, int y)
		{
			this.DesktopLocation = new Point(x, y);
		}

		/// <summary>Shows the form with the specified owner to the user.</summary>
		/// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> and represents the top-level window that will own this form. </param>
		/// <exception cref="T:System.InvalidOperationException">The form being shown is already visible.-or- The form specified in the <paramref name="owner" /> parameter is the same as the form being shown.-or- The form being shown is disabled.-or- The form being shown is not a top-level window.-or- The form being shown as a dialog box is already a modal form.-or-The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" />).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001AA0 RID: 6816 RVA: 0x000671A8 File Offset: 0x000653A8
		public void Show(IWin32Window owner)
		{
			if (owner == null)
			{
				this.Owner = null;
			}
			else
			{
				this.Owner = Control.FromHandle(owner.Handle).TopLevelControl as Form;
			}
			if (owner == this)
			{
				throw new InvalidOperationException("The 'owner' cannot be the form being shown.");
			}
			if (base.TopLevelControl != this)
			{
				throw new InvalidOperationException("Forms that are not top level forms cannot be displayed as a modal dialog. Remove the form from any parent form before calling Show.");
			}
			base.Show();
		}

		/// <summary>Shows the form as a modal dialog box.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.InvalidOperationException">The form being shown is already visible.-or- The form being shown is disabled.-or- The form being shown is not a top-level window.-or- The form being shown as a dialog box is already a modal form.-or-The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" />).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001AA1 RID: 6817 RVA: 0x00067214 File Offset: 0x00065414
		public DialogResult ShowDialog()
		{
			return this.ShowDialog(null);
		}

		/// <summary>Shows the form as a modal dialog box with the specified owner.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box. </param>
		/// <exception cref="T:System.ArgumentException">The form specified in the <paramref name="owner" /> parameter is the same as the form being shown.</exception>
		/// <exception cref="T:System.InvalidOperationException">The form being shown is already visible.-or- The form being shown is disabled.-or- The form being shown is not a top-level window.-or- The form being shown as a dialog box is already a modal form.-or-The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" />).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001AA2 RID: 6818 RVA: 0x00067220 File Offset: 0x00065420
		public DialogResult ShowDialog(IWin32Window owner)
		{
			Form form = null;
			if (owner == null && Application.MWFThread.Current.Context != null)
			{
				IntPtr active = XplatUI.GetActive();
				if (active != IntPtr.Zero)
				{
					owner = Control.FromHandle(active) as Form;
				}
			}
			if (owner != null)
			{
				Control control = Control.FromHandle(owner.Handle);
				if (control != null)
				{
					form = control.TopLevelControl as Form;
				}
			}
			if (form == this)
			{
				throw new ArgumentException("Forms cannot own themselves or their owners.", "owner");
			}
			if (this.is_modal)
			{
				throw new InvalidOperationException("The form is already displayed as a modal dialog.");
			}
			if (base.Visible)
			{
				throw new InvalidOperationException("Forms that are already  visible cannot be displayed as a modal dialog. Set the form's visible property to false before calling ShowDialog.");
			}
			if (!base.Enabled)
			{
				throw new InvalidOperationException("Forms that are not enabled cannot be displayed as a modal dialog. Set the form's enabled property to true before calling ShowDialog.");
			}
			if (base.TopLevelControl != this)
			{
				throw new InvalidOperationException("Forms that are not top level forms cannot be displayed as a modal dialog. Remove the form from any parent form before calling ShowDialog.");
			}
			if (form != null)
			{
				this.owner = form;
			}
			if (this.owner != null && this.owner.TopMost)
			{
				this.TopMost = true;
			}
			IntPtr intPtr;
			bool flag;
			Rectangle rectangle;
			XplatUI.GrabInfo(out intPtr, out flag, out rectangle);
			if (intPtr != IntPtr.Zero)
			{
				XplatUI.UngrabWindow(intPtr);
			}
			Application.RunLoop(true, new ApplicationContext(this));
			if (this.owner != null)
			{
				XplatUI.Activate(this.owner.window.Handle);
			}
			if (base.IsHandleCreated)
			{
				this.DestroyHandle();
			}
			if (this.DialogResult == DialogResult.None)
			{
				this.DialogResult = DialogResult.Cancel;
			}
			return this.DialogResult;
		}

		/// <summary>Gets a string representing the current instance of the form.</summary>
		/// <returns>A string consisting of the fully qualified name of the form object's class, with the <see cref="P:System.Windows.Forms.Form.Text" /> property of the form appended to the end. For example, if the form is derived from the class MyForm in the MyNamespace namespace, and the <see cref="P:System.Windows.Forms.Form.Text" /> property is set to Hello, World, this method will return MyNamespace.MyForm, Text: Hello, World.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AA3 RID: 6819 RVA: 0x000673A8 File Offset: 0x000655A8
		public override string ToString()
		{
			return base.GetType().FullName + ", Text: " + this.Text;
		}

		/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
		// Token: 0x06001AA4 RID: 6820 RVA: 0x000673D0 File Offset: 0x000655D0
		[Browsable(true)]
		[EditorBrowsable(0)]
		public override bool ValidateChildren()
		{
			return base.ValidateChildren();
		}

		/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
		/// <param name="validationConstraints">Places restrictions on which controls have their <see cref="E:System.Windows.Forms.Control.Validating" /> event raised.</param>
		// Token: 0x06001AA5 RID: 6821 RVA: 0x000673D8 File Offset: 0x000655D8
		[EditorBrowsable(0)]
		[Browsable(true)]
		public override bool ValidateChildren(ValidationConstraints validationConstraints)
		{
			return base.ValidateChildren(validationConstraints);
		}

		/// <summary>Activates the MDI child of a form.</summary>
		/// <param name="form">The child form to activate.</param>
		// Token: 0x06001AA6 RID: 6822 RVA: 0x000673E4 File Offset: 0x000655E4
		protected void ActivateMdiChild(Form form)
		{
			if (!this.IsMdiContainer)
			{
				return;
			}
			this.mdi_container.ActivateChild(form);
			this.OnMdiChildActivate(EventArgs.Empty);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0006740C File Offset: 0x0006560C
		[EditorBrowsable(2)]
		protected override void AdjustFormScrollbars(bool displayScrollbars)
		{
			base.AdjustFormScrollbars(displayScrollbars);
		}

		/// <summary>Resizes the form according to the current value of the <see cref="P:System.Windows.Forms.Form.AutoScaleBaseSize" /> property and the size of the current font.</summary>
		// Token: 0x06001AA8 RID: 6824 RVA: 0x00067418 File Offset: 0x00065618
		[EditorBrowsable(1)]
		[Obsolete("This method has been deprecated")]
		protected void ApplyAutoScaling()
		{
			SizeF autoScaleSize = Form.GetAutoScaleSize(this.Font);
			Size size;
			size..ctor((int)Math.Round((double)autoScaleSize.Width), (int)Math.Round((double)autoScaleSize.Height));
			if (size == this.autoscale_base_size)
			{
				return;
			}
			if (Environment.GetEnvironmentVariable("MONO_MWF_SCALING") == "disable")
			{
				return;
			}
			float num;
			if (size.Width != this.AutoScaleBaseSize.Width)
			{
				num = (float)size.Width / (float)this.AutoScaleBaseSize.Width + 0.08f;
			}
			else
			{
				num = 1f;
			}
			float num2;
			if (size.Height != this.AutoScaleBaseSize.Height)
			{
				num2 = (float)size.Height / (float)this.AutoScaleBaseSize.Height + 0.08f;
			}
			else
			{
				num2 = 1f;
			}
			base.Scale(num, num2);
			this.AutoScaleBaseSize = size;
		}

		/// <summary>Centers the position of the form within the bounds of the parent form.</summary>
		// Token: 0x06001AA9 RID: 6825 RVA: 0x0006751C File Offset: 0x0006571C
		protected void CenterToParent()
		{
			if (this.TopLevel && !base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			int num;
			if (base.Width > 0)
			{
				num = base.Width;
			}
			else
			{
				num = this.DefaultSize.Width;
			}
			int num2;
			if (base.Height > 0)
			{
				num2 = base.Height;
			}
			else
			{
				num2 = this.DefaultSize.Height;
			}
			Control control = null;
			if (base.Parent != null)
			{
				control = base.Parent;
			}
			else if (this.owner != null)
			{
				control = this.owner;
			}
			if (this.owner != null)
			{
				this.Location = new Point(control.Left + control.Width / 2 - num / 2, control.Top + control.Height / 2 - num2 / 2);
			}
		}

		/// <summary>Centers the form on the current screen.</summary>
		// Token: 0x06001AAA RID: 6826 RVA: 0x000675FC File Offset: 0x000657FC
		protected void CenterToScreen()
		{
			if (this.TopLevel && !base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			int num;
			if (base.Width > 0)
			{
				num = base.Width;
			}
			else
			{
				num = this.DefaultSize.Width;
			}
			int num2;
			if (base.Height > 0)
			{
				num2 = base.Height;
			}
			else
			{
				num2 = this.DefaultSize.Height;
			}
			Size size;
			XplatUI.GetDisplaySize(out size);
			this.Location = new Point(size.Width / 2 - num / 2, size.Height / 2 - num2 / 2);
		}

		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x06001AAB RID: 6827 RVA: 0x000676A0 File Offset: 0x000658A0
		[EditorBrowsable(2)]
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return base.CreateControlsInstance();
		}

		/// <summary>Creates the handle for the form. If a derived class overrides this function, it must call the base implementation.</summary>
		/// <exception cref="T:System.InvalidOperationException">A handle for this <see cref="T:System.Windows.Forms.Form" /> has already been created.</exception>
		// Token: 0x06001AAC RID: 6828 RVA: 0x000676A8 File Offset: 0x000658A8
		[EditorBrowsable(2)]
		protected override void CreateHandle()
		{
			base.CreateHandle();
			if (!base.IsHandleCreated)
			{
				return;
			}
			base.UpdateBounds();
			if ((XplatUI.SupportsTransparency() & TransparencySupport.Set) != TransparencySupport.None && this.allow_transparency)
			{
				XplatUI.SetWindowTransparency(this.Handle, this.opacity, this.TransparencyKey);
			}
			XplatUI.SetWindowMinMax(this.window.Handle, this.maximized_bounds, this.minimum_size, this.maximum_size);
			if (this.show_icon && this.FormBorderStyle != FormBorderStyle.FixedDialog && this.icon != null)
			{
				XplatUI.SetIcon(this.window.Handle, this.icon);
			}
			if (this.owner != null && this.owner.IsHandleCreated)
			{
				XplatUI.SetOwner(this.window.Handle, this.owner.window.Handle);
			}
			if (this.topmost)
			{
				XplatUI.SetTopmost(this.window.Handle, this.topmost);
			}
			for (int i = 0; i < this.owned_forms.Count; i++)
			{
				if (this.owned_forms[i].IsHandleCreated)
				{
					XplatUI.SetOwner(this.owned_forms[i].window.Handle, this.window.Handle);
				}
			}
			if (this.window_manager != null)
			{
				if (this.IsMdiChild && base.VisibleInternal)
				{
					MdiWindowManager mdiWindowManager;
					if (this.MdiParent != null)
					{
						foreach (Form form in this.MdiParent.MdiChildren)
						{
							mdiWindowManager = form.window_manager as MdiWindowManager;
							if (mdiWindowManager != null && form != this)
							{
								mdiWindowManager.RaiseDeactivate();
							}
						}
					}
					mdiWindowManager = this.window_manager as MdiWindowManager;
					mdiWindowManager.RaiseActivated();
					if (this.MdiParent != null)
					{
						foreach (Form form2 in this.MdiParent.MdiChildren)
						{
							if (form2 != this && form2.IsHandleCreated)
							{
								XplatUI.InvalidateNC(form2.Handle);
							}
						}
					}
				}
				if (this.window_state != FormWindowState.Normal)
				{
					this.window_manager.SetWindowState((FormWindowState)2147483647, this.window_state);
				}
				XplatUI.RequestNCRecalc(this.window.Handle);
			}
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06001AAD RID: 6829 RVA: 0x0006791C File Offset: 0x00065B1C
		[EditorBrowsable(2)]
		protected override void DefWndProc(ref Message m)
		{
			base.DefWndProc(ref m);
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.Form" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001AAE RID: 6830 RVA: 0x00067928 File Offset: 0x00065B28
		protected override void Dispose(bool disposing)
		{
			for (int i = 0; i < this.owned_forms.Count; i++)
			{
				((Form)this.owned_forms[i]).Owner = null;
			}
			this.owned_forms.Clear();
			this.Owner = null;
			base.Dispose(disposing);
			Application.RemoveForm(this);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Activated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AAF RID: 6831 RVA: 0x00067988 File Offset: 0x00065B88
		[EditorBrowsable(2)]
		protected virtual void OnActivated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.ActivatedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Closed" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AB0 RID: 6832 RVA: 0x000679BC File Offset: 0x00065BBC
		[EditorBrowsable(2)]
		protected virtual void OnClosed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.ClosedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Closing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		// Token: 0x06001AB1 RID: 6833 RVA: 0x000679F0 File Offset: 0x00065BF0
		[EditorBrowsable(2)]
		protected virtual void OnClosing(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[Form.ClosingEvent];
			if (cancelEventHandler != null)
			{
				cancelEventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the CreateControl event.</summary>
		// Token: 0x06001AB2 RID: 6834 RVA: 0x00067A24 File Offset: 0x00065C24
		[EditorBrowsable(2)]
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (this.menu != null)
			{
				XplatUI.SetMenu(this.window.Handle, this.menu);
			}
			this.OnLoadInternal(EventArgs.Empty);
			this.OnLocationChanged(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Deactivate" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AB3 RID: 6835 RVA: 0x00067A70 File Offset: 0x00065C70
		[EditorBrowsable(2)]
		protected virtual void OnDeactivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.DeactivateEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001AB4 RID: 6836 RVA: 0x00067AA4 File Offset: 0x00065CA4
		[EditorBrowsable(2)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (!this.autoscale_base_size_set)
			{
				SizeF autoScaleSize = Form.GetAutoScaleSize(this.Font);
				this.autoscale_base_size = new Size((int)Math.Round((double)autoScaleSize.Width), (int)Math.Round((double)autoScaleSize.Height));
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AB5 RID: 6837 RVA: 0x00067AF8 File Offset: 0x00065CF8
		[EditorBrowsable(2)]
		protected override void OnHandleCreated(EventArgs e)
		{
			XplatUI.SetBorderStyle(this.window.Handle, this.form_border_style);
			base.OnHandleCreated(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AB6 RID: 6838 RVA: 0x00067B18 File Offset: 0x00065D18
		[EditorBrowsable(2)]
		protected override void OnHandleDestroyed(EventArgs e)
		{
			Application.RemoveForm(this);
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.InputLanguageChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.InputLanguageChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06001AB7 RID: 6839 RVA: 0x00067B28 File Offset: 0x00065D28
		[EditorBrowsable(2)]
		protected virtual void OnInputLanguageChanged(InputLanguageChangedEventArgs e)
		{
			InputLanguageChangedEventHandler inputLanguageChangedEventHandler = (InputLanguageChangedEventHandler)base.Events[Form.InputLanguageChangedEvent];
			if (inputLanguageChangedEventHandler != null)
			{
				inputLanguageChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.InputLanguageChanging" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.InputLanguageChangingEventArgs" /> that contains the event data. </param>
		// Token: 0x06001AB8 RID: 6840 RVA: 0x00067B5C File Offset: 0x00065D5C
		[EditorBrowsable(2)]
		protected virtual void OnInputLanguageChanging(InputLanguageChangingEventArgs e)
		{
			InputLanguageChangingEventHandler inputLanguageChangingEventHandler = (InputLanguageChangingEventHandler)base.Events[Form.InputLanguageChangingEvent];
			if (inputLanguageChangingEventHandler != null)
			{
				inputLanguageChangingEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AB9 RID: 6841 RVA: 0x00067B90 File Offset: 0x00065D90
		[EditorBrowsable(2)]
		protected virtual void OnLoad(EventArgs e)
		{
			Application.AddForm(this);
			EventHandler eventHandler = (EventHandler)base.Events[Form.LoadEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MaximizedBoundsChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001ABA RID: 6842 RVA: 0x00067BC8 File Offset: 0x00065DC8
		[EditorBrowsable(2)]
		protected virtual void OnMaximizedBoundsChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.MaximizedBoundsChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MaximumSizeChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001ABB RID: 6843 RVA: 0x00067BFC File Offset: 0x00065DFC
		[EditorBrowsable(2)]
		protected virtual void OnMaximumSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.MaximumSizeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MdiChildActivate" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001ABC RID: 6844 RVA: 0x00067C30 File Offset: 0x00065E30
		[EditorBrowsable(2)]
		protected virtual void OnMdiChildActivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.MdiChildActivateEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MenuComplete" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001ABD RID: 6845 RVA: 0x00067C64 File Offset: 0x00065E64
		[EditorBrowsable(2)]
		protected internal virtual void OnMenuComplete(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.MenuCompleteEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MenuStart" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001ABE RID: 6846 RVA: 0x00067C98 File Offset: 0x00065E98
		[EditorBrowsable(2)]
		protected virtual void OnMenuStart(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.MenuStartEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MinimumSizeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001ABF RID: 6847 RVA: 0x00067CCC File Offset: 0x00065ECC
		[EditorBrowsable(2)]
		protected virtual void OnMinimumSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.MinimumSizeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06001AC0 RID: 6848 RVA: 0x00067D00 File Offset: 0x00065F00
		[EditorBrowsable(2)]
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.size_grip != null)
			{
				this.size_grip.HandlePaint(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AC1 RID: 6849 RVA: 0x00067D24 File Offset: 0x00065F24
		[EditorBrowsable(2)]
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AC2 RID: 6850 RVA: 0x00067D30 File Offset: 0x00065F30
		[EditorBrowsable(2)]
		protected override void OnStyleChanged(EventArgs e)
		{
			base.OnStyleChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AC3 RID: 6851 RVA: 0x00067D3C File Offset: 0x00065F3C
		[EditorBrowsable(2)]
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			if (this.mdi_container != null)
			{
				this.mdi_container.SetParentText(true);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AC4 RID: 6852 RVA: 0x00067D5C File Offset: 0x00065F5C
		[EditorBrowsable(2)]
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible && this.window_manager != null)
			{
				if (this.WindowState == FormWindowState.Normal)
				{
					this.window_manager.SetWindowState(this.WindowState, this.WindowState);
				}
				else
				{
					this.window_manager.SetWindowState((FormWindowState)(-1), this.WindowState);
				}
			}
		}

		/// <summary>Processes a command key. </summary>
		/// <returns>true if the keystroke was processed and consumed by the control; otherwise, false to allow further processing.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the Win32 message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06001AC5 RID: 6853 RVA: 0x00067DC0 File Offset: 0x00065FC0
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (base.ProcessCmdKey(ref msg, keyData))
			{
				return true;
			}
			if ((keyData & Keys.Alt) != Keys.None)
			{
				Control topLevelControl = base.TopLevelControl;
				if (topLevelControl != null)
				{
					IntPtr intPtr = Control.MakeParam(2, 2);
					XplatUI.SendMessage(topLevelControl.Handle, Msg.WM_CHANGEUISTATE, intPtr, IntPtr.Zero);
				}
			}
			if (this.ActiveMenu != null && this.ActiveMenu.ProcessCmdKey(ref msg, keyData))
			{
				return true;
			}
			if (base.ActiveTracker != null && base.ActiveTracker.TopMenu is ContextMenu)
			{
				ContextMenu contextMenu = base.ActiveTracker.TopMenu as ContextMenu;
				if (contextMenu.SourceControl != this && contextMenu.ProcessCmdKey(ref msg, keyData))
				{
					return true;
				}
			}
			if (this.IsMdiChild)
			{
				switch (keyData)
				{
				case (Keys)131187:
					break;
				default:
					switch (keyData)
					{
					case (Keys)196723:
						goto IL_0120;
					default:
						if (keyData == (Keys.LButton | Keys.Back | Keys.Control))
						{
							goto IL_0128;
						}
						if (keyData != (Keys.LButton | Keys.Back | Keys.Shift | Keys.Control))
						{
							if (keyData != (Keys)262253 && keyData != (Keys.LButton | Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Alt))
							{
								return false;
							}
							(this.WindowManager as MdiWindowManager).ShowPopup(Point.Empty);
							return true;
						}
						break;
					case (Keys)196725:
						break;
					}
					this.MdiParent.MdiContainer.ActivatePreviousChild();
					return true;
				case (Keys)131189:
					goto IL_0128;
				}
				IL_0120:
				this.Close();
				return true;
				IL_0128:
				this.MdiParent.MdiContainer.ActivateNextChild();
				return true;
			}
			return false;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00067F34 File Offset: 0x00066134
		[EditorBrowsable(2)]
		protected override bool ProcessDialogChar(char charCode)
		{
			return base.ProcessDialogChar(charCode);
		}

		/// <summary>Processes a dialog box key. </summary>
		/// <returns>true if the keystroke was processed and consumed by the control; otherwise, false to allow further processing.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06001AC7 RID: 6855 RVA: 0x00067F40 File Offset: 0x00066140
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & Keys.Modifiers) == Keys.None)
			{
				if (keyData == Keys.Return)
				{
					IntPtr focus = XplatUI.GetFocus();
					Control control = Control.FromHandle(focus);
					if (control is Button && control.FindForm() == this)
					{
						((Button)control).PerformClick();
						return true;
					}
					if (this.accept_button != null)
					{
						this.accept_button.PerformClick();
						return true;
					}
				}
				else if (keyData == Keys.Escape && this.cancel_button != null)
				{
					this.cancel_button.PerformClick();
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		// Token: 0x06001AC8 RID: 6856 RVA: 0x00067FD8 File Offset: 0x000661D8
		protected override bool ProcessKeyPreview(ref Message m)
		{
			return (this.key_preview && this.ProcessKeyEventArgs(ref m)) || base.ProcessKeyPreview(ref m);
		}

		/// <returns>true if a control is selected; otherwise, false.</returns>
		/// <param name="forward">true to cycle forward through the controls in the <see cref="T:System.Windows.Forms.ContainerControl" />; otherwise, false. </param>
		// Token: 0x06001AC9 RID: 6857 RVA: 0x00068008 File Offset: 0x00066208
		protected override bool ProcessTabKey(bool forward)
		{
			bool flag = !this.show_focus_cues;
			this.show_focus_cues = true;
			bool flag2 = base.SelectNextControl(this.ActiveControl, forward, true, true, true);
			if (flag && this.ActiveControl != null)
			{
				this.ActiveControl.Invalidate();
			}
			return flag2;
		}

		/// <summary>Performs scaling of the form.</summary>
		/// <param name="x">Percentage to scale the form horizontally </param>
		/// <param name="y">Percentage to scale the form vertically </param>
		// Token: 0x06001ACA RID: 6858 RVA: 0x00068054 File Offset: 0x00066254
		[EditorBrowsable(1)]
		protected override void ScaleCore(float x, float y)
		{
			base.ScaleCore(x, y);
		}

		/// <summary>Selects this form, and optionally selects the next or previous control.</summary>
		/// <param name="directed">If set to true that the active control is changed </param>
		/// <param name="forward">If directed is true, then this controls the direction in which focus is moved. If this is true, then the next control is selected; otherwise, the previous control is selected. </param>
		// Token: 0x06001ACB RID: 6859 RVA: 0x00068060 File Offset: 0x00066260
		protected override void Select(bool directed, bool forward)
		{
			if (!base.IsHandleCreated && !base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			if (directed)
			{
				base.SelectNextControl(null, forward, true, true, true);
			}
			Form parentForm = base.ParentForm;
			if (parentForm != null)
			{
				parentForm.ActiveControl = this;
			}
			this.Activate();
		}

		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		/// <param name="width">The bounds width.</param>
		/// <param name="height">The bounds height.</param>
		/// <param name="specified">A value from the BoundsSpecified enumeration.</param>
		// Token: 0x06001ACC RID: 6860 RVA: 0x000680B8 File Offset: 0x000662B8
		[EditorBrowsable(2)]
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			Size size;
			if (this.WindowState == FormWindowState.Minimized)
			{
				size = SystemInformation.MinimizedWindowSize;
			}
			else
			{
				FormBorderStyle formBorderStyle = this.FormBorderStyle;
				if (formBorderStyle != FormBorderStyle.FixedToolWindow)
				{
					if (formBorderStyle != FormBorderStyle.SizableToolWindow)
					{
						if (formBorderStyle != FormBorderStyle.None)
						{
							size = SystemInformation.MinimumWindowSize;
						}
						else
						{
							size = XplatUI.MinimumNoBorderWindowSize;
						}
					}
					else
					{
						size = XplatUI.MinimumSizeableToolWindowSize;
					}
				}
				else
				{
					size = XplatUI.MinimumFixedToolWindowSize;
				}
			}
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width)
			{
				width = Math.Max(width, size.Width);
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
			{
				height = Math.Max(height, size.Height);
			}
			base.SetBoundsCore(x, y, width, height, specified);
			int num = (((specified & BoundsSpecified.X) != BoundsSpecified.X) ? this.restore_bounds.X : x);
			int num2 = (((specified & BoundsSpecified.Y) != BoundsSpecified.Y) ? this.restore_bounds.Y : y);
			int num3 = (((specified & BoundsSpecified.Width) != BoundsSpecified.Width) ? this.restore_bounds.Width : width);
			int num4 = (((specified & BoundsSpecified.Height) != BoundsSpecified.Height) ? this.restore_bounds.Height : height);
			this.restore_bounds = new Rectangle(num, num2, num3, num4);
		}

		/// <summary>Sets the client size of the form. This will adjust the bounds of the form to make the client size the requested size.</summary>
		/// <param name="x">Requested width of the client region. </param>
		/// <param name="y">Requested height of the client region.</param>
		// Token: 0x06001ACD RID: 6861 RVA: 0x000681F0 File Offset: 0x000663F0
		[EditorBrowsable(2)]
		protected override void SetClientSizeCore(int x, int y)
		{
			if (this.minimum_size.Width != 0 && x < this.minimum_size.Width)
			{
				x = this.minimum_size.Width;
			}
			else if (this.maximum_size.Width != 0 && x > this.maximum_size.Width)
			{
				x = this.maximum_size.Width;
			}
			if (this.minimum_size.Height != 0 && y < this.minimum_size.Height)
			{
				y = this.minimum_size.Height;
			}
			else if (this.maximum_size.Height != 0 && y > this.maximum_size.Height)
			{
				y = this.maximum_size.Height;
			}
			Rectangle rectangle;
			rectangle..ctor(0, 0, x, y);
			CreateParams createParams = this.CreateParams;
			this.clientsize_set = new Size(x, y);
			Rectangle rectangle2;
			if (XplatUI.CalculateWindowRect(ref rectangle, createParams, createParams.menu, out rectangle2))
			{
				base.SetBounds(this.bounds.X, this.bounds.Y, rectangle2.Width, rectangle2.Height, BoundsSpecified.Size);
			}
		}

		/// <param name="value">true to make the control visible; otherwise, false. </param>
		// Token: 0x06001ACE RID: 6862 RVA: 0x00068320 File Offset: 0x00066520
		[EditorBrowsable(2)]
		protected override void SetVisibleCore(bool value)
		{
			if (value)
			{
				this.close_raised = false;
			}
			if (this.IsMdiChild && !this.MdiParent.Visible)
			{
				if (value != base.Visible)
				{
					MdiWindowManager mdiWindowManager = (MdiWindowManager)this.window_manager;
					mdiWindowManager.IsVisiblePending = value;
					this.OnVisibleChanged(EventArgs.Empty);
					return;
				}
			}
			else
			{
				this.is_changing_visible_state++;
				this.has_been_visible = value || this.has_been_visible;
				base.SetVisibleCore(value);
				if (value)
				{
					Application.AddForm(this);
				}
				if (value && this.WindowState != FormWindowState.Normal)
				{
					XplatUI.SendMessage(this.Handle, Msg.WM_SHOWWINDOW, (IntPtr)1, IntPtr.Zero);
				}
				this.is_changing_visible_state--;
			}
			if (value && this.IsMdiContainer)
			{
				foreach (Form form in this.MdiChildren)
				{
					MdiWindowManager mdiWindowManager2 = (MdiWindowManager)form.window_manager;
					if (!form.IsHandleCreated && mdiWindowManager2.IsVisiblePending)
					{
						mdiWindowManager2.IsVisiblePending = false;
						form.Visible = true;
					}
				}
			}
			if (value && this.IsMdiChild)
			{
				base.PerformLayout();
				ThemeEngine.Current.ManagedWindowSetButtonLocations(this.window_manager);
			}
			if (value && !this.shown_raised)
			{
				this.OnShown(EventArgs.Empty);
				this.shown_raised = true;
			}
			if (value && !this.IsMdiChild)
			{
				if (this.ActiveControl == null)
				{
					base.SelectNextControl(null, true, true, true, false);
				}
				if (this.ActiveControl != null)
				{
					base.SendControlFocus(this.ActiveControl);
				}
				else
				{
					base.Focus();
				}
			}
		}

		/// <summary>Updates which button is the default button.</summary>
		// Token: 0x06001ACF RID: 6863 RVA: 0x000684EC File Offset: 0x000666EC
		protected override void UpdateDefaultButton()
		{
			base.UpdateDefaultButton();
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000684F4 File Offset: 0x000666F4
		[EditorBrowsable(2)]
		protected override void WndProc(ref Message m)
		{
			if (this.window_manager != null && this.window_manager.WndProc(ref m))
			{
				return;
			}
			Msg msg = (Msg)m.Msg;
			switch (msg)
			{
			case Msg.WM_DESTROY:
				this.WmDestroy(ref m);
				return;
			default:
				switch (msg)
				{
				case Msg.WM_NCCALCSIZE:
					this.WmNcCalcSize(ref m);
					break;
				case Msg.WM_NCHITTEST:
					this.WmNcHitTest(ref m);
					return;
				case Msg.WM_NCPAINT:
					this.WmNcPaint(ref m);
					return;
				default:
					switch (msg)
					{
					case Msg.WM_NCMOUSEMOVE:
						this.WmNcMouseMove(ref m);
						return;
					case Msg.WM_NCLBUTTONDOWN:
						this.WmNcLButtonDown(ref m);
						return;
					case Msg.WM_NCLBUTTONUP:
						this.WmNcLButtonUp(ref m);
						return;
					default:
						if (msg != Msg.WM_ENTERSIZEMOVE)
						{
							if (msg != Msg.WM_EXITSIZEMOVE)
							{
								if (msg == Msg.WM_CLOSE)
								{
									this.WmClose(ref m);
									return;
								}
								if (msg != Msg.WM_GETMINMAXINFO)
								{
									if (msg == Msg.WM_WINDOWPOSCHANGED)
									{
										this.WmWindowPosChanged(ref m);
										return;
									}
									if (msg != Msg.WM_SYSCOMMAND)
									{
										if (msg == Msg.WM_NCMOUSELEAVE)
										{
											this.WmNcMouseLeave(ref m);
											return;
										}
										base.WndProc(ref m);
									}
									else
									{
										this.WmSysCommand(ref m);
									}
								}
								else
								{
									this.WmGetMinMaxInfo(ref m);
								}
							}
							else
							{
								this.OnResizeEnd(EventArgs.Empty);
							}
						}
						else
						{
							this.OnResizeBegin(EventArgs.Empty);
						}
						break;
					}
					break;
				}
				return;
			case Msg.WM_ACTIVATE:
				this.WmActivate(ref m);
				return;
			case Msg.WM_SETFOCUS:
				this.WmSetFocus(ref m);
				return;
			case Msg.WM_KILLFOCUS:
				this.WmKillFocus(ref m);
				return;
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00068674 File Offset: 0x00066874
		private void WmDestroy(ref Message m)
		{
			if (!base.RecreatingHandle)
			{
				this.closing = true;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00068690 File Offset: 0x00066890
		internal bool RaiseCloseEvents(bool last_check, bool cancel)
		{
			if (last_check && base.Visible)
			{
				base.Hide();
			}
			if (this.close_raised || (last_check && this.closed))
			{
				return false;
			}
			bool flag = this.FireClosingEvents(CloseReason.UserClosing, cancel);
			if (!flag)
			{
				if (!last_check || this.DialogResult != DialogResult.None)
				{
					if (this.mdi_container != null)
					{
						foreach (Form form in this.mdi_container.MdiChildren)
						{
							form.FireClosedEvents(CloseReason.UserClosing);
						}
					}
					this.FireClosedEvents(CloseReason.UserClosing);
				}
				this.closing = true;
				this.close_raised = true;
				this.shown_raised = false;
			}
			else
			{
				this.DialogResult = DialogResult.None;
				this.closing = false;
			}
			return flag;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00068758 File Offset: 0x00066958
		private void WmClose(ref Message m)
		{
			Form activeForm = Form.ActiveForm;
			if (activeForm != null && activeForm != this && activeForm.Modal)
			{
				Control control = this;
				while (control != null && control.Parent != activeForm)
				{
					control = control.Parent;
				}
				if (control == null || control.Parent != activeForm)
				{
					return;
				}
			}
			bool flag = false;
			if (this.mdi_container != null)
			{
				foreach (Form form in this.mdi_container.MdiChildren)
				{
					flag = form.FireClosingEvents(CloseReason.MdiFormClosing, flag);
				}
			}
			bool flag2 = false;
			if (!this.suppress_closing_events)
			{
				flag2 = !this.ValidateChildren();
			}
			if (this.suppress_closing_events || !this.RaiseCloseEvents(false, flag2 || flag))
			{
				if (this.is_modal)
				{
					base.Hide();
				}
				else
				{
					this.Dispose();
					if (activeForm != null && activeForm != this)
					{
						activeForm.SelectActiveControl();
					}
				}
				this.mdi_parent = null;
			}
			else
			{
				if (this.is_modal)
				{
					this.DialogResult = DialogResult.None;
				}
				this.closing = false;
			}
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00068888 File Offset: 0x00066A88
		private void WmWindowPosChanged(ref Message m)
		{
			if (this.window_state != FormWindowState.Minimized && this.WindowState != FormWindowState.Minimized)
			{
				base.WndProc(ref m);
			}
			else if (!this.is_minimizing)
			{
				this.is_minimizing = true;
				this.OnSizeChanged(EventArgs.Empty);
				this.is_minimizing = false;
			}
			if (this.WindowState == FormWindowState.Normal)
			{
				this.restore_bounds = this.Bounds;
			}
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x000688F4 File Offset: 0x00066AF4
		private void WmSysCommand(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle))
			{
				ToolStripManager.FireAppClicked();
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00068914 File Offset: 0x00066B14
		private void WmActivate(ref Message m)
		{
			if (m.WParam != (IntPtr)0)
			{
				if (this.is_loaded)
				{
					this.SelectActiveControl();
					if (this.ActiveControl != null && !this.ActiveControl.Focused)
					{
						base.SendControlFocus(this.ActiveControl);
					}
				}
				this.IsActive = true;
			}
			else
			{
				if (XplatUI.IsEnabled(this.Handle) && XplatUI.GetParent(m.LParam) != this.Handle)
				{
					ToolStripManager.FireAppFocusChanged(this);
				}
				this.IsActive = false;
			}
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000689B4 File Offset: 0x00066BB4
		private void WmKillFocus(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x000689C0 File Offset: 0x00066BC0
		private void WmSetFocus(ref Message m)
		{
			if (this.ActiveControl != null && this.ActiveControl != this)
			{
				this.ActiveControl.Focus();
				return;
			}
			if (this.IsMdiContainer)
			{
				this.mdi_container.SendFocusToActiveChild();
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00068A10 File Offset: 0x00066C10
		private void WmNcHitTest(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle) && this.ActiveMenu != null)
			{
				int num = Control.LowOrder(m.LParam.ToInt32());
				int num2 = Control.HighOrder((long)m.LParam.ToInt32());
				XplatUI.ScreenToMenu(this.ActiveMenu.Wnd.window.Handle, ref num, ref num2);
				if (num > 0 && num2 > 0 && num < this.ActiveMenu.Rect.Width && num2 < this.ActiveMenu.Rect.Height)
				{
					m.Result = new IntPtr(5);
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00068AD8 File Offset: 0x00066CD8
		private void WmNcLButtonDown(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle) && this.ActiveMenu != null)
			{
				this.ActiveMenu.OnMouseDown(this, new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.MousePosition.X, Control.MousePosition.Y, 0));
			}
			if (this.ActiveMaximizedMdiChild != null && this.ActiveMenu != null && this.ActiveMaximizedMdiChild.HandleMenuMouseDown(this.ActiveMenu, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32())))
			{
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00068BA4 File Offset: 0x00066DA4
		private void WmNcLButtonUp(ref Message m)
		{
			if (this.ActiveMaximizedMdiChild != null && this.ActiveMenu != null)
			{
				this.ActiveMaximizedMdiChild.HandleMenuMouseUp(this.ActiveMenu, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00068C08 File Offset: 0x00066E08
		private void WmNcMouseLeave(ref Message m)
		{
			if (this.ActiveMaximizedMdiChild != null && this.ActiveMenu != null)
			{
				this.ActiveMaximizedMdiChild.HandleMenuMouseLeave(this.ActiveMenu, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00068C6C File Offset: 0x00066E6C
		private void WmNcMouseMove(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle) && this.ActiveMenu != null)
			{
				this.ActiveMenu.OnMouseMove(this, new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
			}
			if (this.ActiveMaximizedMdiChild != null && this.ActiveMenu != null)
			{
				XplatUI.RequestAdditionalWM_NCMessages(this.Handle, false, true);
				this.ActiveMaximizedMdiChild.HandleMenuMouseMove(this.ActiveMenu, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00068D4C File Offset: 0x00066F4C
		private void WmNcPaint(ref Message m)
		{
			if (this.ActiveMenu != null)
			{
				PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref m, this.Handle, false);
				Point menuOrigin = XplatUI.GetMenuOrigin(this.window.Handle);
				Rectangle rectangle;
				rectangle..ctor(menuOrigin.X, menuOrigin.Y, this.ClientSize.Width, 0);
				rectangle = Rectangle.Union(rectangle, paintEventArgs.ClipRectangle);
				paintEventArgs.SetClip(rectangle);
				paintEventArgs.Graphics.SetClip(rectangle);
				this.ActiveMenu.Draw(paintEventArgs, new Rectangle(menuOrigin.X, menuOrigin.Y, this.ClientSize.Width, 0));
				if (this.ActiveMaximizedMdiChild != null)
				{
					this.ActiveMaximizedMdiChild.DrawMaximizedButtons(this.ActiveMenu, paintEventArgs);
				}
				XplatUI.PaintEventEnd(ref m, this.Handle, false);
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00068E28 File Offset: 0x00067028
		private void WmNcCalcSize(ref Message m)
		{
			if (this.ActiveMenu != null && m.WParam == (IntPtr)1)
			{
				XplatUIWin32.NCCALCSIZE_PARAMS nccalcsize_PARAMS = (XplatUIWin32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(XplatUIWin32.NCCALCSIZE_PARAMS));
				nccalcsize_PARAMS.rgrc1.top = nccalcsize_PARAMS.rgrc1.top + ThemeEngine.Current.CalcMenuBarSize(base.DeviceContext, this.ActiveMenu, this.ClientSize.Width);
				Marshal.StructureToPtr(nccalcsize_PARAMS, m.LParam, true);
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00068EC4 File Offset: 0x000670C4
		private void WmGetMinMaxInfo(ref Message m)
		{
			if (m.LParam != IntPtr.Zero)
			{
				MINMAXINFO minmaxinfo = (MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(MINMAXINFO));
				this.default_maximized_bounds = new Rectangle(minmaxinfo.ptMaxPosition.x, minmaxinfo.ptMaxPosition.y, minmaxinfo.ptMaxSize.x, minmaxinfo.ptMaxSize.y);
				if (this.maximized_bounds != Rectangle.Empty)
				{
					minmaxinfo.ptMaxPosition.x = this.maximized_bounds.Left;
					minmaxinfo.ptMaxPosition.y = this.maximized_bounds.Top;
					minmaxinfo.ptMaxSize.x = this.maximized_bounds.Width;
					minmaxinfo.ptMaxSize.y = this.maximized_bounds.Height;
				}
				if (this.minimum_size != Size.Empty)
				{
					minmaxinfo.ptMinTrackSize.x = this.minimum_size.Width;
					minmaxinfo.ptMinTrackSize.y = this.minimum_size.Height;
				}
				if (this.maximum_size != Size.Empty)
				{
					minmaxinfo.ptMaxTrackSize.x = this.maximum_size.Width;
					minmaxinfo.ptMaxTrackSize.y = this.maximum_size.Height;
				}
				Marshal.StructureToPtr(minmaxinfo, m.LParam, false);
			}
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00069048 File Offset: 0x00067248
		internal void ActivateFocusCues()
		{
			bool flag = !this.show_focus_cues;
			this.show_focus_cues = true;
			if (flag)
			{
				this.ActiveControl.Invalidate();
			}
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00069078 File Offset: 0x00067278
		internal override void FireEnter()
		{
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0006907C File Offset: 0x0006727C
		internal override void FireLeave()
		{
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00069080 File Offset: 0x00067280
		internal void RemoveWindowManager()
		{
			this.window_manager = null;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0006908C File Offset: 0x0006728C
		internal override void CheckAcceptButton()
		{
			if (this.accept_button != null)
			{
				Button button = this.accept_button as Button;
				if (this.ActiveControl == button)
				{
					return;
				}
				if (button == null)
				{
					return;
				}
				if (this.ActiveControl is Button)
				{
					button.paint_as_acceptbutton = false;
				}
				else
				{
					button.paint_as_acceptbutton = true;
				}
				button.Invalidate();
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x000690F0 File Offset: 0x000672F0
		internal override bool ActivateOnShow
		{
			get
			{
				return !this.ShowWithoutActivation;
			}
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x000690FC File Offset: 0x000672FC
		private void OnLoadInternal(EventArgs e)
		{
			if (this.AutoScale)
			{
				this.ApplyAutoScaling();
				this.AutoScale = false;
			}
			if (!base.IsDisposed)
			{
				base.OnSizeInitializedOrChanged();
				try
				{
					this.OnLoad(e);
				}
				catch (Exception ex)
				{
					Application.OnThreadException(ex);
				}
				if (!base.IsDisposed)
				{
					this.is_visible = true;
				}
			}
			if (!this.IsMdiChild && !base.IsDisposed)
			{
				switch (this.StartPosition)
				{
				case FormStartPosition.Manual:
					base.Left = this.CreateParams.X;
					base.Top = this.CreateParams.Y;
					break;
				case FormStartPosition.CenterScreen:
					this.CenterToScreen();
					break;
				case FormStartPosition.CenterParent:
					this.CenterToParent();
					break;
				}
			}
			this.is_loaded = true;
		}

		/// <returns>The text associated with this control.</returns>
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x000691F8 File Offset: 0x000673F8
		// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x00069200 File Offset: 0x00067400
		[SettingsBindable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the <see cref="T:System.Windows.Forms.Form" /> in screen coordinates.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the <see cref="T:System.Windows.Forms.Form" /> in screen coordinates.</returns>
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x0006920C File Offset: 0x0006740C
		// (set) Token: 0x06001AEB RID: 6891 RVA: 0x00069214 File Offset: 0x00067414
		[SettingsBindable(true)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the data.</param>
		// Token: 0x06001AEC RID: 6892 RVA: 0x00069220 File Offset: 0x00067420
		protected override void OnBackgroundImageChanged(EventArgs e)
		{
			base.OnBackgroundImageChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AED RID: 6893 RVA: 0x0006922C File Offset: 0x0006742C
		protected override void OnBackgroundImageLayoutChanged(EventArgs e)
		{
			base.OnBackgroundImageLayoutChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AEE RID: 6894 RVA: 0x00069238 File Offset: 0x00067438
		[EditorBrowsable(2)]
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AEF RID: 6895 RVA: 0x00069244 File Offset: 0x00067444
		[EditorBrowsable(2)]
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.FormClosed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.FormClosedEventArgs" /> that contains the event data. </param>
		// Token: 0x06001AF0 RID: 6896 RVA: 0x00069250 File Offset: 0x00067450
		[EditorBrowsable(2)]
		protected virtual void OnFormClosed(FormClosedEventArgs e)
		{
			Application.RemoveForm(this);
			FormClosedEventHandler formClosedEventHandler = (FormClosedEventHandler)base.Events[Form.FormClosedEvent];
			if (formClosedEventHandler != null)
			{
				formClosedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.FormClosing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> that contains the event data. </param>
		// Token: 0x06001AF1 RID: 6897 RVA: 0x00069288 File Offset: 0x00067488
		[EditorBrowsable(2)]
		protected virtual void OnFormClosing(FormClosingEventArgs e)
		{
			FormClosingEventHandler formClosingEventHandler = (FormClosingEventHandler)base.Events[Form.FormClosingEvent];
			if (formClosingEventHandler != null)
			{
				formClosingEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.HelpButtonClicked" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		// Token: 0x06001AF2 RID: 6898 RVA: 0x000692BC File Offset: 0x000674BC
		[MonoTODO("Will never be called")]
		[EditorBrowsable(2)]
		protected virtual void OnHelpButtonClicked(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[Form.HelpButtonClickedEvent];
			if (cancelEventHandler != null)
			{
				cancelEventHandler.Invoke(this, e);
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x000692F0 File Offset: 0x000674F0
		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
			if (this.AutoSize)
			{
				Size preferredSizeCore = this.GetPreferredSizeCore(Size.Empty);
				if (this.AutoSizeMode == AutoSizeMode.GrowOnly)
				{
					preferredSizeCore.Width = Math.Max(preferredSizeCore.Width, base.Width);
					preferredSizeCore.Height = Math.Max(preferredSizeCore.Height, base.Height);
				}
				if (preferredSizeCore == this.Size)
				{
					return;
				}
				base.SetBoundsInternal(this.bounds.X, this.bounds.Y, preferredSizeCore.Width, preferredSizeCore.Height, BoundsSpecified.None);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.ResizeBegin" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AF4 RID: 6900 RVA: 0x00069398 File Offset: 0x00067598
		[EditorBrowsable(2)]
		protected virtual void OnResizeBegin(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.ResizeBeginEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.ResizeEnd" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AF5 RID: 6901 RVA: 0x000693CC File Offset: 0x000675CC
		[EditorBrowsable(2)]
		protected virtual void OnResizeEnd(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.ResizeEndEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.RightToLeftLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001AF6 RID: 6902 RVA: 0x00069400 File Offset: 0x00067600
		[EditorBrowsable(2)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.RightToLeftLayoutChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001AF7 RID: 6903 RVA: 0x00069434 File Offset: 0x00067634
		[EditorBrowsable(2)]
		protected virtual void OnShown(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.ShownEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00069468 File Offset: 0x00067668
		internal void OnUIAMenuChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.UIAMenuChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0006949C File Offset: 0x0006769C
		internal void OnUIATopMostChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.UIATopMostChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000694D4 File Offset: 0x000676D4
		internal void OnUIAWindowStateChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.UIAWindowStateChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000E9D RID: 3741
		internal bool closing;

		// Token: 0x04000E9E RID: 3742
		private bool closed;

		// Token: 0x04000E9F RID: 3743
		private FormBorderStyle form_border_style;

		// Token: 0x04000EA0 RID: 3744
		private bool is_active;

		// Token: 0x04000EA1 RID: 3745
		private bool autoscale;

		// Token: 0x04000EA2 RID: 3746
		private Size clientsize_set;

		// Token: 0x04000EA3 RID: 3747
		private Size autoscale_base_size;

		// Token: 0x04000EA4 RID: 3748
		private bool allow_transparency;

		// Token: 0x04000EA5 RID: 3749
		private static Icon default_icon;

		// Token: 0x04000EA6 RID: 3750
		internal bool is_modal;

		// Token: 0x04000EA7 RID: 3751
		internal FormWindowState window_state;

		// Token: 0x04000EA8 RID: 3752
		private bool control_box;

		// Token: 0x04000EA9 RID: 3753
		private bool minimize_box;

		// Token: 0x04000EAA RID: 3754
		private bool maximize_box;

		// Token: 0x04000EAB RID: 3755
		private bool help_button;

		// Token: 0x04000EAC RID: 3756
		private bool show_in_taskbar;

		// Token: 0x04000EAD RID: 3757
		private bool topmost;

		// Token: 0x04000EAE RID: 3758
		private IButtonControl accept_button;

		// Token: 0x04000EAF RID: 3759
		private IButtonControl cancel_button;

		// Token: 0x04000EB0 RID: 3760
		private DialogResult dialog_result;

		// Token: 0x04000EB1 RID: 3761
		private FormStartPosition start_position;

		// Token: 0x04000EB2 RID: 3762
		private Form owner;

		// Token: 0x04000EB3 RID: 3763
		private Form.ControlCollection owned_forms;

		// Token: 0x04000EB4 RID: 3764
		private MdiClient mdi_container;

		// Token: 0x04000EB5 RID: 3765
		internal InternalWindowManager window_manager;

		// Token: 0x04000EB6 RID: 3766
		private Form mdi_parent;

		// Token: 0x04000EB7 RID: 3767
		private bool key_preview;

		// Token: 0x04000EB8 RID: 3768
		private MainMenu menu;

		// Token: 0x04000EB9 RID: 3769
		private Icon icon;

		// Token: 0x04000EBA RID: 3770
		private Size maximum_size;

		// Token: 0x04000EBB RID: 3771
		private Size minimum_size;

		// Token: 0x04000EBC RID: 3772
		private SizeGripStyle size_grip_style;

		// Token: 0x04000EBD RID: 3773
		private SizeGrip size_grip;

		// Token: 0x04000EBE RID: 3774
		private Rectangle maximized_bounds;

		// Token: 0x04000EBF RID: 3775
		private Rectangle default_maximized_bounds;

		// Token: 0x04000EC0 RID: 3776
		private double opacity;

		// Token: 0x04000EC1 RID: 3777
		internal ApplicationContext context;

		// Token: 0x04000EC2 RID: 3778
		private Color transparency_key;

		// Token: 0x04000EC3 RID: 3779
		private bool is_loaded;

		// Token: 0x04000EC4 RID: 3780
		internal int is_changing_visible_state;

		// Token: 0x04000EC5 RID: 3781
		internal bool has_been_visible;

		// Token: 0x04000EC6 RID: 3782
		private bool shown_raised;

		// Token: 0x04000EC7 RID: 3783
		private bool close_raised;

		// Token: 0x04000EC8 RID: 3784
		private bool is_clientsize_set;

		// Token: 0x04000EC9 RID: 3785
		internal bool suppress_closing_events;

		// Token: 0x04000ECA RID: 3786
		internal bool waiting_showwindow;

		// Token: 0x04000ECB RID: 3787
		private bool is_minimizing;

		// Token: 0x04000ECC RID: 3788
		private bool show_icon = true;

		// Token: 0x04000ECD RID: 3789
		private MenuStrip main_menu_strip;

		// Token: 0x04000ECE RID: 3790
		private bool right_to_left_layout;

		// Token: 0x04000ECF RID: 3791
		private Rectangle restore_bounds;

		// Token: 0x04000ED0 RID: 3792
		private bool autoscale_base_size_set;

		/// <summary>Represents a collection of controls on the form.</summary>
		// Token: 0x02000197 RID: 407
		[ComVisible(false)]
		public new class ControlCollection : Control.ControlCollection
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Form.ControlCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.Form" /> to contain the controls added to the control collection. </param>
			// Token: 0x06001AFB RID: 6907 RVA: 0x0006950C File Offset: 0x0006770C
			public ControlCollection(Form owner)
				: base(owner)
			{
				this.form_owner = owner;
			}

			/// <summary>Adds a control to the form.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to add to the form. </param>
			/// <exception cref="T:System.Exception">A multiple document interface (MDI) parent form cannot have controls added to it. </exception>
			// Token: 0x06001AFC RID: 6908 RVA: 0x0006951C File Offset: 0x0006771C
			public override void Add(Control value)
			{
				if (base.Contains(value))
				{
					return;
				}
				base.AddToList(value);
				((Form)value).owner = this.form_owner;
			}

			/// <summary>Removes a control from the form.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.Control" /> to remove from the form. </param>
			// Token: 0x06001AFD RID: 6909 RVA: 0x00069544 File Offset: 0x00067744
			public override void Remove(Control value)
			{
				((Form)value).owner = null;
				base.Remove(value);
			}

			// Token: 0x04000EE8 RID: 3816
			private Form form_owner;
		}
	}
}
