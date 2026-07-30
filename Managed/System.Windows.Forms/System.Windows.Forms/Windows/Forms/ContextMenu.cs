using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents a shortcut menu. Although <see cref="T:System.Windows.Forms.ContextMenuStrip" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.ContextMenu" /> control of previous versions, <see cref="T:System.Windows.Forms.ContextMenu" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000A2 RID: 162
	[DefaultEvent("Popup")]
	public class ContextMenu : Menu
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenu" /> class with no menu items specified.</summary>
		// Token: 0x060007D3 RID: 2003 RVA: 0x00022B58 File Offset: 0x00020D58
		public ContextMenu()
			: base(null)
		{
			this.tracker = new MenuTracker(this);
			this.right_to_left = RightToLeft.Inherit;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenu" /> class with a specified set of <see cref="T:System.Windows.Forms.MenuItem" /> objects.</summary>
		/// <param name="menuItems">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that represent the menu items to add to the shortcut menu. </param>
		// Token: 0x060007D4 RID: 2004 RVA: 0x00022B74 File Offset: 0x00020D74
		public ContextMenu(MenuItem[] menuItems)
			: base(menuItems)
		{
			this.tracker = new MenuTracker(this);
			this.right_to_left = RightToLeft.Inherit;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00022B90 File Offset: 0x00020D90
		// Note: this type is marked as 'beforefieldinit'.
		static ContextMenu()
		{
			ContextMenu.CollapseEvent = new object();
			ContextMenu.PopupEvent = new object();
		}

		/// <summary>Occurs when the shortcut menu collapses.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400006C RID: 108
		// (add) Token: 0x060007D6 RID: 2006 RVA: 0x00022BA8 File Offset: 0x00020DA8
		// (remove) Token: 0x060007D7 RID: 2007 RVA: 0x00022BBC File Offset: 0x00020DBC
		public event EventHandler Collapse
		{
			add
			{
				base.Events.AddHandler(ContextMenu.CollapseEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ContextMenu.CollapseEvent, value);
			}
		}

		/// <summary>Occurs before the shortcut menu is displayed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400006D RID: 109
		// (add) Token: 0x060007D8 RID: 2008 RVA: 0x00022BD0 File Offset: 0x00020DD0
		// (remove) Token: 0x060007D9 RID: 2009 RVA: 0x00022BE4 File Offset: 0x00020DE4
		public event EventHandler Popup
		{
			add
			{
				base.Events.AddHandler(ContextMenu.PopupEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ContextMenu.PopupEvent, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether text displayed by the control is displayed from right to left.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a valid member of the <see cref="T:System.Windows.Forms.RightToLeft" /> enumeration. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00022BF8 File Offset: 0x00020DF8
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x00022C00 File Offset: 0x00020E00
		[DefaultValue(RightToLeft.No)]
		[Localizable(true)]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				return this.right_to_left;
			}
			set
			{
				this.right_to_left = value;
			}
		}

		/// <summary>Gets the control that is displaying the shortcut menu.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the control that is displaying the shortcut menu. If no control has displayed the shortcut menu, the property returns null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00022C0C File Offset: 0x00020E0C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Control SourceControl
		{
			get
			{
				return this.src_control;
			}
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		/// <param name="control">The control to which the command key applies.</param>
		// Token: 0x060007DD RID: 2013 RVA: 0x00022C14 File Offset: 0x00020E14
		protected internal virtual bool ProcessCmdKey(ref Message msg, Keys keyData, Control control)
		{
			this.src_control = control;
			return this.ProcessCmdKey(ref msg, keyData);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ContextMenu.Collapse" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060007DE RID: 2014 RVA: 0x00022C28 File Offset: 0x00020E28
		protected internal virtual void OnCollapse(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ContextMenu.CollapseEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ContextMenu.Popup" /> event </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060007DF RID: 2015 RVA: 0x00022C5C File Offset: 0x00020E5C
		protected internal virtual void OnPopup(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ContextMenu.PopupEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Displays the shortcut menu at the specified position.</summary>
		/// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> that specifies the control with which this shortcut menu is associated. </param>
		/// <param name="pos">A <see cref="T:System.Drawing.Point" /> that specifies the coordinates at which to display the menu. These coordinates are specified relative to the client coordinates of the control specified in the <paramref name="control" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="control" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The handle of the control does not exist or the control is not visible.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060007E0 RID: 2016 RVA: 0x00022C90 File Offset: 0x00020E90
		public void Show(Control control, Point pos)
		{
			if (control == null)
			{
				throw new ArgumentException();
			}
			this.src_control = control;
			this.OnPopup(EventArgs.Empty);
			pos = control.PointToScreen(pos);
			MenuTracker.TrackPopupMenu(this, pos);
			this.OnCollapse(EventArgs.Empty);
		}

		/// <summary>Displays the shortcut menu at the specified position and with the specified alignment.</summary>
		/// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> that specifies the control with which this shortcut menu is associated.</param>
		/// <param name="pos">A <see cref="T:System.Drawing.Point" /> that specifies the coordinates at which to display the menu. These coordinates are specified relative to the client coordinates of the control specified in the <paramref name="control" /> parameter.</param>
		/// <param name="alignment">A <see cref="T:System.Windows.Forms.LeftRightAlignment" /> that specifies the alignment of the control relative to the <paramref name="pos" /> parameter.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060007E1 RID: 2017 RVA: 0x00022CD8 File Offset: 0x00020ED8
		public void Show(Control control, Point pos, LeftRightAlignment alignment)
		{
			Point point;
			if (alignment == LeftRightAlignment.Left)
			{
				point..ctor(pos.X - control.Width, pos.Y);
			}
			else
			{
				point = pos;
			}
			this.Show(control, point);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00022D18 File Offset: 0x00020F18
		internal void Hide()
		{
			this.tracker.Deactivate();
		}

		// Token: 0x04000793 RID: 1939
		private RightToLeft right_to_left;

		// Token: 0x04000794 RID: 1940
		private Control src_control;
	}
}
