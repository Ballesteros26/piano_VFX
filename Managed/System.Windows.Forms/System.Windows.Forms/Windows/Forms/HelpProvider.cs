using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides pop-up or online Help for controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B2 RID: 434
	[ProvideProperty("ShowHelp", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[ToolboxItemFilter("System.Windows.Forms")]
	[ProvideProperty("HelpString", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[ProvideProperty("HelpKeyword", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[ProvideProperty("HelpNavigator", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public class HelpProvider : Component, IExtenderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HelpProvider" /> class.</summary>
		// Token: 0x06001C12 RID: 7186 RVA: 0x0006C260 File Offset: 0x0006A460
		public HelpProvider()
		{
			this.controls = new Hashtable();
			this.tooltip = new ToolTip.ToolTipWindow();
			this.tooltip.VisibleChanged += delegate(object sender, EventArgs args)
			{
				if (this.tooltip.Visible)
				{
					HelpProvider.OnUIAHelpRequested(this, new ControlEventArgs(this.UIAControl));
				}
				else
				{
					HelpProvider.OnUIAHelpUnRequested(this, new ControlEventArgs(this.UIAControl));
				}
			};
			this.HideToolTipHandler = new EventHandler(this.HideToolTip);
			this.HideToolTipKeyHandler = new KeyPressEventHandler(this.HideToolTipKey);
			this.HideToolTipMouseHandler = new MouseEventHandler(this.HideToolTipMouse);
			this.HelpRequestHandler = new HelpEventHandler(this.HelpRequested);
		}

		// Token: 0x140001C4 RID: 452
		// (add) Token: 0x06001C13 RID: 7187 RVA: 0x0006C2E8 File Offset: 0x0006A4E8
		// (remove) Token: 0x06001C14 RID: 7188 RVA: 0x0006C300 File Offset: 0x0006A500
		internal static event ControlEventHandler UIAHelpRequested;

		// Token: 0x140001C5 RID: 453
		// (add) Token: 0x06001C15 RID: 7189 RVA: 0x0006C318 File Offset: 0x0006A518
		// (remove) Token: 0x06001C16 RID: 7190 RVA: 0x0006C330 File Offset: 0x0006A530
		internal static event ControlEventHandler UIAHelpUnRequested;

		/// <summary>Gets or sets a value specifying the name of the Help file associated with this <see cref="T:System.Windows.Forms.HelpProvider" /> object.</summary>
		/// <returns>The name of the Help file. This can be of the form C:\path\sample.chm or /folder/file.htm.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0006C348 File Offset: 0x0006A548
		// (set) Token: 0x06001C18 RID: 7192 RVA: 0x0006C350 File Offset: 0x0006A550
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.HelpNamespaceEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		public virtual string HelpNamespace
		{
			get
			{
				return this.helpnamespace;
			}
			set
			{
				this.helpnamespace = value;
			}
		}

		/// <summary>Gets or sets the object that contains supplemental data about the <see cref="T:System.Windows.Forms.HelpProvider" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.HelpProvider" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x0006C35C File Offset: 0x0006A55C
		// (set) Token: 0x06001C1A RID: 7194 RVA: 0x0006C364 File Offset: 0x0006A564
		[Bindable(true)]
		[MWFCategory("Data")]
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

		/// <summary>Specifies whether this object can provide its extender properties to the specified object.</summary>
		/// <param name="target">The object </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C1B RID: 7195 RVA: 0x0006C370 File Offset: 0x0006A570
		public virtual bool CanExtend(object target)
		{
			return target is Control && !(target is Form) && !(target is ToolBar);
		}

		/// <summary>Returns the Help keyword for the specified control.</summary>
		/// <returns>The Help keyword associated with this control, or null if the <see cref="T:System.Windows.Forms.HelpProvider" /> is currently configured to display the entire Help file or is configured to provide a Help string.</returns>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> from which to retrieve the Help topic. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C1C RID: 7196 RVA: 0x0006C3A4 File Offset: 0x0006A5A4
		[Localizable(true)]
		[DefaultValue(null)]
		public virtual string GetHelpKeyword(Control ctl)
		{
			return this.GetHelpProperty(ctl).Keyword;
		}

		/// <summary>Returns the current <see cref="T:System.Windows.Forms.HelpNavigator" /> setting for the specified control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HelpNavigator" /> setting for the specified control. The default is <see cref="F:System.Windows.Forms.HelpNavigator.AssociateIndex" />.</returns>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> from which to retrieve the Help navigator. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C1D RID: 7197 RVA: 0x0006C3B4 File Offset: 0x0006A5B4
		[DefaultValue(HelpNavigator.AssociateIndex)]
		[Localizable(true)]
		public virtual HelpNavigator GetHelpNavigator(Control ctl)
		{
			return this.GetHelpProperty(ctl).Navigator;
		}

		/// <summary>Returns the contents of the pop-up Help window for the specified control.</summary>
		/// <returns>The Help string associated with this control. The default is null.</returns>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> from which to retrieve the Help string. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C1E RID: 7198 RVA: 0x0006C3C4 File Offset: 0x0006A5C4
		[DefaultValue(null)]
		[Localizable(true)]
		public virtual string GetHelpString(Control ctl)
		{
			return this.GetHelpProperty(ctl).Text;
		}

		/// <summary>Returns a value indicating whether the specified control's Help should be displayed.</summary>
		/// <returns>true if Help will be displayed for the control; otherwise, false.</returns>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> for which Help will be displayed. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C1F RID: 7199 RVA: 0x0006C3D4 File Offset: 0x0006A5D4
		[Localizable(true)]
		public virtual bool GetShowHelp(Control ctl)
		{
			return this.GetHelpProperty(ctl).Show;
		}

		/// <summary>Removes the Help associated with the specified control.</summary>
		/// <param name="ctl">The control to remove Help from.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C20 RID: 7200 RVA: 0x0006C3E4 File Offset: 0x0006A5E4
		public virtual void ResetShowHelp(Control ctl)
		{
			HelpProvider.HelpProperty helpProperty = this.GetHelpProperty(ctl);
			if (helpProperty.Keyword != null || helpProperty.Text != null)
			{
				helpProperty.Show = true;
			}
			else
			{
				helpProperty.Show = false;
			}
		}

		/// <summary>Specifies the keyword used to retrieve Help when the user invokes Help for the specified control.</summary>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> that specifies the control for which to set the Help topic. </param>
		/// <param name="keyword">The Help keyword to associate with the control. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001C21 RID: 7201 RVA: 0x0006C424 File Offset: 0x0006A624
		public virtual void SetHelpKeyword(Control ctl, string keyword)
		{
			this.GetHelpProperty(ctl).Keyword = keyword;
		}

		/// <summary>Specifies the Help command to use when retrieving Help from the Help file for the specified control.</summary>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> for which to set the Help keyword. </param>
		/// <param name="navigator">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of <paramref name="navigator" /> is not one of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001C22 RID: 7202 RVA: 0x0006C434 File Offset: 0x0006A634
		public virtual void SetHelpNavigator(Control ctl, HelpNavigator navigator)
		{
			this.GetHelpProperty(ctl).Navigator = navigator;
		}

		/// <summary>Specifies the Help string associated with the specified control.</summary>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> with which to associate the Help string. </param>
		/// <param name="helpString">The Help string associated with the control. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001C23 RID: 7203 RVA: 0x0006C444 File Offset: 0x0006A644
		public virtual void SetHelpString(Control ctl, string helpString)
		{
			this.GetHelpProperty(ctl).Text = helpString;
		}

		/// <summary>Specifies whether Help is displayed for the specified control.</summary>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> for which Help is turned on or off. </param>
		/// <param name="value">true if Help displays for the control; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001C24 RID: 7204 RVA: 0x0006C454 File Offset: 0x0006A654
		public virtual void SetShowHelp(Control ctl, bool value)
		{
			this.GetHelpProperty(ctl).Show = value;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.HelpProvider" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.HelpProvider" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C25 RID: 7205 RVA: 0x0006C464 File Offset: 0x0006A664
		public override string ToString()
		{
			return base.ToString() + ", HelpNameSpace: " + this.helpnamespace;
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0006C47C File Offset: 0x0006A67C
		private HelpProvider.HelpProperty GetHelpProperty(Control control)
		{
			HelpProvider.HelpProperty helpProperty = (HelpProvider.HelpProperty)this.controls[control];
			if (helpProperty == null)
			{
				helpProperty = new HelpProvider.HelpProperty(this, control);
				this.controls[control] = helpProperty;
			}
			return helpProperty;
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0006C4B8 File Offset: 0x0006A6B8
		private void HideToolTip(object Sender, EventArgs e)
		{
			Control control = (Control)Sender;
			control.LostFocus -= this.HideToolTipHandler;
			this.tooltip.Visible = false;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0006C4E4 File Offset: 0x0006A6E4
		private void HideToolTipKey(object Sender, KeyPressEventArgs e)
		{
			Control control = (Control)Sender;
			control.KeyPress -= this.HideToolTipKeyHandler;
			this.tooltip.Visible = false;
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0006C510 File Offset: 0x0006A710
		private void HideToolTipMouse(object Sender, MouseEventArgs e)
		{
			Control control = (Control)Sender;
			control.MouseDown -= this.HideToolTipMouseHandler;
			this.tooltip.Visible = false;
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0006C53C File Offset: 0x0006A73C
		private void HelpRequested(object sender, HelpEventArgs e)
		{
			Control control = (Control)sender;
			this.UIAControl = control;
			if (this.GetHelpProperty(control).Text == null)
			{
				return;
			}
			Point mousePos = e.MousePos;
			this.tooltip.Text = this.GetHelpProperty(control).Text;
			Size size = ThemeEngine.Current.ToolTipSize(this.tooltip, this.tooltip.Text);
			this.tooltip.Width = size.Width;
			this.tooltip.Height = size.Height;
			mousePos.X -= size.Width / 2;
			if (mousePos.X < 0)
			{
				mousePos.X += size.Width / 2;
			}
			if (mousePos.X + size.Width < SystemInformation.WorkingArea.Width)
			{
				this.tooltip.Left = mousePos.X;
			}
			else
			{
				this.tooltip.Left = mousePos.X - size.Width;
			}
			if (mousePos.Y + size.Height < SystemInformation.WorkingArea.Height - 16)
			{
				this.tooltip.Top = mousePos.Y;
			}
			else
			{
				this.tooltip.Top = mousePos.Y - size.Height;
			}
			this.tooltip.Visible = true;
			control.KeyPress += this.HideToolTipKeyHandler;
			control.MouseDown += this.HideToolTipMouseHandler;
			control.LostFocus += this.HideToolTipHandler;
			e.Handled = true;
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x0006C6E4 File Offset: 0x0006A8E4
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x0006C6EC File Offset: 0x0006A8EC
		private Control UIAControl
		{
			get
			{
				return this.uia_control;
			}
			set
			{
				this.uia_control = value;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x0006C6F8 File Offset: 0x0006A8F8
		internal Rectangle UIAToolTipRectangle
		{
			get
			{
				return this.tooltip.Bounds;
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0006C708 File Offset: 0x0006A908
		internal static void OnUIAHelpRequested(HelpProvider provider, ControlEventArgs args)
		{
			if (HelpProvider.UIAHelpRequested != null)
			{
				HelpProvider.UIAHelpRequested(provider, args);
			}
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0006C720 File Offset: 0x0006A920
		internal static void OnUIAHelpUnRequested(HelpProvider provider, ControlEventArgs args)
		{
			if (HelpProvider.UIAHelpUnRequested != null)
			{
				HelpProvider.UIAHelpUnRequested(provider, args);
			}
		}

		// Token: 0x04000F30 RID: 3888
		private string helpnamespace;

		// Token: 0x04000F31 RID: 3889
		private Hashtable controls;

		// Token: 0x04000F32 RID: 3890
		private ToolTip.ToolTipWindow tooltip;

		// Token: 0x04000F33 RID: 3891
		private EventHandler HideToolTipHandler;

		// Token: 0x04000F34 RID: 3892
		private KeyPressEventHandler HideToolTipKeyHandler;

		// Token: 0x04000F35 RID: 3893
		private MouseEventHandler HideToolTipMouseHandler;

		// Token: 0x04000F36 RID: 3894
		private HelpEventHandler HelpRequestHandler;

		// Token: 0x04000F37 RID: 3895
		private object tag;

		// Token: 0x04000F38 RID: 3896
		private Control uia_control;

		// Token: 0x020001B3 RID: 435
		private class HelpProperty
		{
			// Token: 0x06001C31 RID: 7217 RVA: 0x0006C77C File Offset: 0x0006A97C
			public HelpProperty(HelpProvider hp, Control control)
			{
				this.control = control;
				this.hp = hp;
				this.keyword = null;
				this.navigator = HelpNavigator.AssociateIndex;
				this.text = null;
				this.show = false;
				control.HelpRequested += hp.HelpRequestHandler;
			}

			// Token: 0x170006C7 RID: 1735
			// (get) Token: 0x06001C32 RID: 7218 RVA: 0x0006C7CC File Offset: 0x0006A9CC
			// (set) Token: 0x06001C33 RID: 7219 RVA: 0x0006C7D4 File Offset: 0x0006A9D4
			public string Keyword
			{
				get
				{
					return this.keyword;
				}
				set
				{
					this.keyword = value;
				}
			}

			// Token: 0x170006C8 RID: 1736
			// (get) Token: 0x06001C34 RID: 7220 RVA: 0x0006C7E0 File Offset: 0x0006A9E0
			// (set) Token: 0x06001C35 RID: 7221 RVA: 0x0006C7E8 File Offset: 0x0006A9E8
			public HelpNavigator Navigator
			{
				get
				{
					return this.navigator;
				}
				set
				{
					this.navigator = value;
				}
			}

			// Token: 0x170006C9 RID: 1737
			// (get) Token: 0x06001C36 RID: 7222 RVA: 0x0006C7F4 File Offset: 0x0006A9F4
			// (set) Token: 0x06001C37 RID: 7223 RVA: 0x0006C7FC File Offset: 0x0006A9FC
			public string Text
			{
				get
				{
					return this.text;
				}
				set
				{
					this.text = value;
				}
			}

			// Token: 0x170006CA RID: 1738
			// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0006C808 File Offset: 0x0006AA08
			// (set) Token: 0x06001C39 RID: 7225 RVA: 0x0006C810 File Offset: 0x0006AA10
			public bool Show
			{
				get
				{
					return this.show;
				}
				set
				{
					this.show = value;
				}
			}

			// Token: 0x04000F3B RID: 3899
			internal string keyword;

			// Token: 0x04000F3C RID: 3900
			internal HelpNavigator navigator;

			// Token: 0x04000F3D RID: 3901
			internal string text;

			// Token: 0x04000F3E RID: 3902
			internal bool show;

			// Token: 0x04000F3F RID: 3903
			internal Control control;

			// Token: 0x04000F40 RID: 3904
			internal HelpProvider hp;
		}
	}
}
