using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides a user interface for indicating that a control on a form has an error associated with it.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015F RID: 351
	[ProvideProperty("Error", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[ToolboxItemFilter("System.Windows.Forms")]
	[ProvideProperty("IconAlignment", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[ProvideProperty("IconPadding", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[ComplexBindingProperties("DataSource", "DataMember")]
	public class ErrorProvider : Component, ISupportInitialize, IExtenderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ErrorProvider" /> class and initializes the default settings for <see cref="P:System.Windows.Forms.ErrorProvider.BlinkRate" />, <see cref="P:System.Windows.Forms.ErrorProvider.BlinkStyle" />, and the <see cref="P:System.Windows.Forms.ErrorProvider.Icon" />.</summary>
		// Token: 0x06001789 RID: 6025 RVA: 0x000565C4 File Offset: 0x000547C4
		public ErrorProvider()
		{
			this.controls = new Hashtable();
			this.blinkrate = 250;
			this.blinkstyle = ErrorBlinkStyle.BlinkIfDifferentError;
			this.icon = ResourceImageLoader.GetIcon("errorProvider.ico");
			this.tooltip = new ToolTip.ToolTipWindow();
			this.tooltip.VisibleChanged += delegate(object sender, EventArgs args)
			{
				if (this.tooltip.Visible)
				{
					ErrorProvider.OnUIAPopup(this, new PopupEventArgs(this.UIAControl, this.UIAControl, false, Size.Empty));
				}
				else if (!this.tooltip.Visible)
				{
					ErrorProvider.OnUIAUnPopup(this, new PopupEventArgs(this.UIAControl, this.UIAControl, false, Size.Empty));
				}
			};
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ErrorProvider" /> class attached to a container.</summary>
		/// <param name="parentControl">The container of the control to monitor for errors. </param>
		// Token: 0x0600178A RID: 6026 RVA: 0x00056628 File Offset: 0x00054828
		public ErrorProvider(ContainerControl parentControl)
			: this()
		{
			this.container = parentControl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ErrorProvider" /> class attached to an <see cref="T:System.ComponentModel.IContainer" /> implementation.</summary>
		/// <param name="container">The <see cref="T:System.ComponentModel.IContainer" /> to monitor for errors.</param>
		// Token: 0x0600178B RID: 6027 RVA: 0x00056638 File Offset: 0x00054838
		public ErrorProvider(IContainer container)
			: this()
		{
			container.Add(this);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00056648 File Offset: 0x00054848
		// Note: this type is marked as 'beforefieldinit'.
		static ErrorProvider()
		{
			ErrorProvider.RightToLeftChangedEvent = new object();
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ErrorProvider.RightToLeft" /> property changes value. </summary>
		// Token: 0x14000187 RID: 391
		// (add) Token: 0x0600178D RID: 6029 RVA: 0x00056654 File Offset: 0x00054854
		// (remove) Token: 0x0600178E RID: 6030 RVA: 0x00056668 File Offset: 0x00054868
		public event EventHandler RightToLeftChanged
		{
			add
			{
				base.Events.AddHandler(ErrorProvider.RightToLeftChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ErrorProvider.RightToLeftChangedEvent, value);
			}
		}

		// Token: 0x14000188 RID: 392
		// (add) Token: 0x0600178F RID: 6031 RVA: 0x0005667C File Offset: 0x0005487C
		// (remove) Token: 0x06001790 RID: 6032 RVA: 0x00056694 File Offset: 0x00054894
		internal static event ControlEventHandler UIAControlHookUp;

		// Token: 0x14000189 RID: 393
		// (add) Token: 0x06001791 RID: 6033 RVA: 0x000566AC File Offset: 0x000548AC
		// (remove) Token: 0x06001792 RID: 6034 RVA: 0x000566C4 File Offset: 0x000548C4
		internal static event ControlEventHandler UIAControlUnhookUp;

		// Token: 0x1400018A RID: 394
		// (add) Token: 0x06001793 RID: 6035 RVA: 0x000566DC File Offset: 0x000548DC
		// (remove) Token: 0x06001794 RID: 6036 RVA: 0x000566F4 File Offset: 0x000548F4
		internal static event ControlEventHandler UIAErrorProviderHookUp;

		// Token: 0x1400018B RID: 395
		// (add) Token: 0x06001795 RID: 6037 RVA: 0x0005670C File Offset: 0x0005490C
		// (remove) Token: 0x06001796 RID: 6038 RVA: 0x00056724 File Offset: 0x00054924
		internal static event ControlEventHandler UIAErrorProviderUnhookUp;

		// Token: 0x1400018C RID: 396
		// (add) Token: 0x06001797 RID: 6039 RVA: 0x0005673C File Offset: 0x0005493C
		// (remove) Token: 0x06001798 RID: 6040 RVA: 0x00056754 File Offset: 0x00054954
		internal static event PopupEventHandler UIAPopup;

		// Token: 0x1400018D RID: 397
		// (add) Token: 0x06001799 RID: 6041 RVA: 0x0005676C File Offset: 0x0005496C
		// (remove) Token: 0x0600179A RID: 6042 RVA: 0x00056784 File Offset: 0x00054984
		internal static event PopupEventHandler UIAUnPopup;

		/// <summary>Signals the object that initialization is starting.</summary>
		// Token: 0x0600179B RID: 6043 RVA: 0x0005679C File Offset: 0x0005499C
		void ISupportInitialize.BeginInit()
		{
		}

		/// <summary>Signals the object that initialization is complete.</summary>
		// Token: 0x0600179C RID: 6044 RVA: 0x000567A0 File Offset: 0x000549A0
		void ISupportInitialize.EndInit()
		{
		}

		/// <summary>Gets or sets the rate at which the error icon flashes.</summary>
		/// <returns>The rate, in milliseconds, at which the error icon should flash. The default is 250 milliseconds.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x000567A4 File Offset: 0x000549A4
		// (set) Token: 0x0600179E RID: 6046 RVA: 0x000567AC File Offset: 0x000549AC
		[DefaultValue(250)]
		[RefreshProperties(2)]
		public int BlinkRate
		{
			get
			{
				return this.blinkrate;
			}
			set
			{
				this.blinkrate = value;
			}
		}

		/// <summary>Gets or sets a value indicating when the error icon flashes.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ErrorBlinkStyle" /> values. The default is <see cref="F:System.Windows.Forms.ErrorBlinkStyle.BlinkIfDifferentError" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ErrorBlinkStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x000567B8 File Offset: 0x000549B8
		// (set) Token: 0x060017A0 RID: 6048 RVA: 0x000567C0 File Offset: 0x000549C0
		[DefaultValue(ErrorBlinkStyle.BlinkIfDifferentError)]
		public ErrorBlinkStyle BlinkStyle
		{
			get
			{
				return this.blinkstyle;
			}
			set
			{
				this.blinkstyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating the parent control for this <see cref="T:System.Windows.Forms.ErrorProvider" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContainerControl" /> that contains the controls that the <see cref="T:System.Windows.Forms.ErrorProvider" /> is attached to.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x000567CC File Offset: 0x000549CC
		// (set) Token: 0x060017A2 RID: 6050 RVA: 0x000567D4 File Offset: 0x000549D4
		[DefaultValue(null)]
		public ContainerControl ContainerControl
		{
			get
			{
				return this.container;
			}
			set
			{
				this.container = value;
			}
		}

		/// <summary>Gets or sets the list within a data source to monitor.</summary>
		/// <returns>The string that represents a list within the data source specified by the <see cref="P:System.Windows.Forms.ErrorProvider.DataSource" /> to be monitored. Typically, this will be a <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x000567E0 File Offset: 0x000549E0
		// (set) Token: 0x060017A4 RID: 6052 RVA: 0x000567E8 File Offset: 0x000549E8
		[DefaultValue(null)]
		[MonoTODO("Stub, does nothing")]
		[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DataMember
		{
			get
			{
				return this.datamember;
			}
			set
			{
				this.datamember = value;
			}
		}

		/// <summary>Gets or sets the data source that the <see cref="T:System.Windows.Forms.ErrorProvider" /> monitors.</summary>
		/// <returns>A data source based on the <see cref="T:System.Collections.IList" /> interface to be monitored for errors. Typically, this is a <see cref="T:System.Data.DataSet" /> to be monitored for errors.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x000567F4 File Offset: 0x000549F4
		// (set) Token: 0x060017A6 RID: 6054 RVA: 0x000567FC File Offset: 0x000549FC
		[DefaultValue(null)]
		[AttributeProvider(typeof(IListSource))]
		[MonoTODO("Stub, does nothing")]
		public object DataSource
		{
			get
			{
				return this.datasource;
			}
			set
			{
				this.datasource = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Icon" /> that is displayed next to a control when an error description string has been set for the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> that signals an error has occurred. The default icon consists of an exclamation point in a circle with a red background.</returns>
		/// <exception cref="T:System.ArgumentNullException">The assigned value of the <see cref="T:System.Drawing.Icon" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x00056808 File Offset: 0x00054A08
		// (set) Token: 0x060017A8 RID: 6056 RVA: 0x00056810 File Offset: 0x00054A10
		[Localizable(true)]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (value != null && (value.Height != 16 || value.Width != 16))
				{
					this.icon = new Icon(value, 16, 16);
				}
				else
				{
					this.icon = value;
				}
			}
		}

		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.ComponentModel.Component" />, or null if the <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer" />, the <see cref="T:System.ComponentModel.Component" /> does not have an <see cref="T:System.ComponentModel.ISite" /> associated with it, or the <see cref="T:System.ComponentModel.Component" /> is removed from its <see cref="T:System.ComponentModel.IContainer" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005AE RID: 1454
		// (set) Token: 0x060017A9 RID: 6057 RVA: 0x0005685C File Offset: 0x00054A5C
		public override ISite Site
		{
			set
			{
				base.Site = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the component is used in a locale that supports right-to-left fonts.</summary>
		/// <returns>true if the component is used in a right-to-left locale; otherwise, false.</returns>
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x00056868 File Offset: 0x00054A68
		// (set) Token: 0x060017AB RID: 6059 RVA: 0x00056870 File Offset: 0x00054A70
		[MonoTODO("RTL not supported")]
		[Localizable(true)]
		[DefaultValue(false)]
		public virtual bool RightToLeft
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

		/// <summary>Gets or sets an object that contains data about the component.</summary>
		/// <returns>An object that contains data about the control. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x0005687C File Offset: 0x00054A7C
		// (set) Token: 0x060017AD RID: 6061 RVA: 0x00056884 File Offset: 0x00054A84
		[TypeConverter(typeof(StringConverter))]
		[Localizable(false)]
		[Bindable(true)]
		[MWFCategory("Data")]
		[DefaultValue(null)]
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

		/// <summary>Provides a method to set both the <see cref="P:System.Windows.Forms.ErrorProvider.DataSource" /> and <see cref="P:System.Windows.Forms.ErrorProvider.DataMember" /> at run time.</summary>
		/// <param name="newDataSource">A data set based on the <see cref="T:System.Collections.IList" /> interface to be monitored for errors. Typically, this is a <see cref="T:System.Data.DataSet" /> to be monitored for errors. </param>
		/// <param name="newDataMember">A collection within the <paramref name="newDataSource" /> to monitor for errors. Typically, this will be a <see cref="T:System.Data.DataTable" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060017AE RID: 6062 RVA: 0x00056890 File Offset: 0x00054A90
		[MonoTODO("Stub, does nothing")]
		public void BindToDataAndErrors(object newDataSource, string newDataMember)
		{
			this.datasource = newDataSource;
			this.datamember = newDataMember;
		}

		/// <summary>Gets a value indicating whether a control can be extended.</summary>
		/// <returns>true if the control can be extended; otherwise, false.This property will be true if the object is a <see cref="T:System.Windows.Forms.Control" /> and is not a <see cref="T:System.Windows.Forms.Form" /> or <see cref="T:System.Windows.Forms.ToolBar" />.</returns>
		/// <param name="extendee">The control to be extended. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017AF RID: 6063 RVA: 0x000568A0 File Offset: 0x00054AA0
		public bool CanExtend(object extendee)
		{
			return extendee is Control && !(extendee is Form) && !(extendee is ToolBar);
		}

		/// <summary>Clears all settings associated with this component.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060017B0 RID: 6064 RVA: 0x000568D4 File Offset: 0x00054AD4
		public void Clear()
		{
			foreach (object obj in this.controls.Values)
			{
				ErrorProvider.ErrorProperty errorProperty = (ErrorProvider.ErrorProperty)obj;
				errorProperty.Text = string.Empty;
			}
		}

		/// <summary>Returns the current error description string for the specified control.</summary>
		/// <returns>The error description string for the specified control.</returns>
		/// <param name="control">The item to get the error description string for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060017B1 RID: 6065 RVA: 0x0005694C File Offset: 0x00054B4C
		[DefaultValue("")]
		[Localizable(true)]
		public string GetError(Control control)
		{
			return this.GetErrorProperty(control).Text;
		}

		/// <summary>Gets a value indicating where the error icon should be placed in relation to the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ErrorIconAlignment" /> values. The default icon alignment is <see cref="F:System.Windows.Forms.ErrorIconAlignment.MiddleRight" />.</returns>
		/// <param name="control">The control to get the icon location for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060017B2 RID: 6066 RVA: 0x0005695C File Offset: 0x00054B5C
		[DefaultValue(ErrorIconAlignment.MiddleRight)]
		[Localizable(true)]
		public ErrorIconAlignment GetIconAlignment(Control control)
		{
			return this.GetErrorProperty(control).Alignment;
		}

		/// <summary>Returns the amount of extra space to leave next to the error icon.</summary>
		/// <returns>The number of pixels to leave between the icon and the control. </returns>
		/// <param name="control">The control to get the padding for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060017B3 RID: 6067 RVA: 0x0005696C File Offset: 0x00054B6C
		[DefaultValue(0)]
		[Localizable(true)]
		public int GetIconPadding(Control control)
		{
			return this.GetErrorProperty(control).padding;
		}

		/// <summary>Sets the error description string for the specified control.</summary>
		/// <param name="control">The control to set the error description string for. </param>
		/// <param name="value">The error description string, or null or <see cref="F:System.String.Empty" /> to remove the error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060017B4 RID: 6068 RVA: 0x0005697C File Offset: 0x00054B7C
		public void SetError(Control control, string value)
		{
			this.GetErrorProperty(control).Text = value;
		}

		/// <summary>Sets the location where the error icon should be placed in relation to the control.</summary>
		/// <param name="control">The control to set the icon location for. </param>
		/// <param name="value">One of the <see cref="T:System.Windows.Forms.ErrorIconAlignment" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060017B5 RID: 6069 RVA: 0x0005698C File Offset: 0x00054B8C
		public void SetIconAlignment(Control control, ErrorIconAlignment value)
		{
			this.GetErrorProperty(control).Alignment = value;
		}

		/// <summary>Sets the amount of extra space to leave between the specified control and the error icon.</summary>
		/// <param name="control">The <paramref name="control" /> to set the padding for. </param>
		/// <param name="padding">The number of pixels to add between the icon and the <paramref name="control" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060017B6 RID: 6070 RVA: 0x0005699C File Offset: 0x00054B9C
		public void SetIconPadding(Control control, int padding)
		{
			this.GetErrorProperty(control).Padding = padding;
		}

		/// <summary>Provides a method to update the bindings of the <see cref="P:System.Windows.Forms.ErrorProvider.DataSource" />, <see cref="P:System.Windows.Forms.ErrorProvider.DataMember" />, and the error text.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060017B7 RID: 6071 RVA: 0x000569AC File Offset: 0x00054BAC
		[MonoTODO("Stub, does nothing")]
		public void UpdateBinding()
		{
		}

		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060017B8 RID: 6072 RVA: 0x000569B0 File Offset: 0x00054BB0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ErrorProvider.RightToLeftChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060017B9 RID: 6073 RVA: 0x000569BC File Offset: 0x00054BBC
		[EditorBrowsable(2)]
		protected virtual void OnRightToLeftChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ErrorProvider.RightToLeftChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x000569F0 File Offset: 0x00054BF0
		private ErrorProvider.ErrorProperty GetErrorProperty(Control control)
		{
			ErrorProvider.ErrorProperty errorProperty = (ErrorProvider.ErrorProperty)this.controls[control];
			if (errorProperty == null)
			{
				errorProperty = new ErrorProvider.ErrorProperty(this, control);
				this.controls[control] = errorProperty;
			}
			return errorProperty;
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x00056A2C File Offset: 0x00054C2C
		// (set) Token: 0x060017BC RID: 6076 RVA: 0x00056A34 File Offset: 0x00054C34
		internal Control UIAControl
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

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x00056A40 File Offset: 0x00054C40
		internal Rectangle UIAToolTipRectangle
		{
			get
			{
				return this.tooltip.Bounds;
			}
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00056A50 File Offset: 0x00054C50
		internal static void OnUIAPopup(ErrorProvider sender, PopupEventArgs args)
		{
			if (ErrorProvider.UIAPopup != null)
			{
				ErrorProvider.UIAPopup(sender, args);
			}
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00056A68 File Offset: 0x00054C68
		internal static void OnUIAUnPopup(ErrorProvider sender, PopupEventArgs args)
		{
			if (ErrorProvider.UIAUnPopup != null)
			{
				ErrorProvider.UIAUnPopup(sender, args);
			}
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00056A80 File Offset: 0x00054C80
		internal static void OnUIAControlHookUp(object sender, ControlEventArgs args)
		{
			if (ErrorProvider.UIAControlHookUp != null)
			{
				ErrorProvider.UIAControlHookUp(sender, args);
			}
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00056A98 File Offset: 0x00054C98
		internal static void OnUIAControlUnhookUp(object sender, ControlEventArgs args)
		{
			if (ErrorProvider.UIAControlUnhookUp != null)
			{
				ErrorProvider.UIAControlUnhookUp(sender, args);
			}
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00056AB0 File Offset: 0x00054CB0
		internal static void OnUIAErrorProviderHookUp(object sender, ControlEventArgs args)
		{
			if (ErrorProvider.UIAErrorProviderHookUp != null)
			{
				ErrorProvider.UIAErrorProviderHookUp(sender, args);
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00056AC8 File Offset: 0x00054CC8
		internal static void OnUIAErrorProviderUnhookUp(object sender, ControlEventArgs args)
		{
			if (ErrorProvider.UIAErrorProviderUnhookUp != null)
			{
				ErrorProvider.UIAErrorProviderUnhookUp(sender, args);
			}
		}

		// Token: 0x04000D0F RID: 3343
		private int blinkrate;

		// Token: 0x04000D10 RID: 3344
		private ErrorBlinkStyle blinkstyle;

		// Token: 0x04000D11 RID: 3345
		private string datamember;

		// Token: 0x04000D12 RID: 3346
		private object datasource;

		// Token: 0x04000D13 RID: 3347
		private ContainerControl container;

		// Token: 0x04000D14 RID: 3348
		private Icon icon;

		// Token: 0x04000D15 RID: 3349
		private Hashtable controls;

		// Token: 0x04000D16 RID: 3350
		private ToolTip.ToolTipWindow tooltip;

		// Token: 0x04000D17 RID: 3351
		private bool right_to_left;

		// Token: 0x04000D18 RID: 3352
		private object tag;

		// Token: 0x04000D1A RID: 3354
		private Control uia_control;

		// Token: 0x02000160 RID: 352
		private class ErrorWindow : UserControl
		{
			// Token: 0x060017C5 RID: 6085 RVA: 0x00056B4C File Offset: 0x00054D4C
			public ErrorWindow()
			{
				base.SetStyle(ControlStyles.Selectable, false);
			}
		}

		// Token: 0x02000161 RID: 353
		private class ErrorProperty
		{
			// Token: 0x060017C6 RID: 6086 RVA: 0x00056B60 File Offset: 0x00054D60
			public ErrorProperty(ErrorProvider ep, Control control)
			{
				ErrorProvider.ErrorProperty <>f__this = this;
				this.ep = ep;
				this.control = control;
				this.alignment = ErrorIconAlignment.MiddleRight;
				this.padding = 0;
				this.text = string.Empty;
				this.blink_count = 0;
				this.tick = new EventHandler(this.window_Tick);
				this.window = new ErrorProvider.ErrorWindow();
				this.window.Visible = false;
				this.window.Width = ep.icon.Width;
				this.window.Height = ep.icon.Height;
				ErrorProvider.OnUIAErrorProviderHookUp(ep, new ControlEventArgs(control));
				this.window.VisibleChanged += delegate(object sender, EventArgs args)
				{
					if (<>f__this.window.Visible)
					{
						ErrorProvider.OnUIAControlHookUp(control, new ControlEventArgs(<>f__this.window));
					}
					else
					{
						ErrorProvider.OnUIAControlUnhookUp(control, new ControlEventArgs(<>f__this.window));
					}
				};
				if (control.Parent != null)
				{
					ErrorProvider.OnUIAControlHookUp(control, new ControlEventArgs(this.window));
					control.Parent.Controls.Add(this.window);
					control.Parent.Controls.SetChildIndex(this.window, control.Parent.Controls.IndexOf(control) + 1);
				}
				this.window.Paint += this.window_Paint;
				this.window.MouseEnter += new EventHandler(this.window_MouseEnter);
				this.window.MouseLeave += new EventHandler(this.window_MouseLeave);
				control.SizeChanged += new EventHandler(this.control_SizeLocationChanged);
				control.LocationChanged += new EventHandler(this.control_SizeLocationChanged);
				control.ParentChanged += new EventHandler(this.control_ParentChanged);
				this.CalculateAlignment();
			}

			// Token: 0x170005B3 RID: 1459
			// (get) Token: 0x060017C7 RID: 6087 RVA: 0x00056D40 File Offset: 0x00054F40
			// (set) Token: 0x060017C8 RID: 6088 RVA: 0x00056D48 File Offset: 0x00054F48
			public string Text
			{
				get
				{
					return this.text;
				}
				set
				{
					if (value == null)
					{
						value = string.Empty;
					}
					bool flag = this.text != value;
					this.text = value;
					if (this.text != string.Empty)
					{
						this.window.Visible = true;
						if (flag || this.ep.blinkstyle == ErrorBlinkStyle.AlwaysBlink)
						{
							if (this.timer == null)
							{
								this.timer = new Timer();
								this.timer.Tick += this.tick;
							}
							this.timer.Interval = this.ep.blinkrate;
							this.blink_count = 0;
							this.timer.Enabled = true;
						}
						return;
					}
					this.window.Visible = false;
				}
			}

			// Token: 0x170005B4 RID: 1460
			// (get) Token: 0x060017C9 RID: 6089 RVA: 0x00056E10 File Offset: 0x00055010
			// (set) Token: 0x060017CA RID: 6090 RVA: 0x00056E18 File Offset: 0x00055018
			public ErrorIconAlignment Alignment
			{
				get
				{
					return this.alignment;
				}
				set
				{
					if (this.alignment != value)
					{
						this.alignment = value;
						this.CalculateAlignment();
					}
				}
			}

			// Token: 0x170005B5 RID: 1461
			// (get) Token: 0x060017CB RID: 6091 RVA: 0x00056E34 File Offset: 0x00055034
			// (set) Token: 0x060017CC RID: 6092 RVA: 0x00056E3C File Offset: 0x0005503C
			public int Padding
			{
				get
				{
					return this.padding;
				}
				set
				{
					if (this.padding != value)
					{
						this.padding = value;
						this.CalculateAlignment();
					}
				}
			}

			// Token: 0x060017CD RID: 6093 RVA: 0x00056E58 File Offset: 0x00055058
			private void CalculateAlignment()
			{
				if (this.visible)
				{
					this.visible = false;
					this.ep.tooltip.Visible = false;
				}
				switch (this.alignment)
				{
				case ErrorIconAlignment.TopLeft:
					this.window.Left = this.control.Left - this.ep.icon.Width - this.padding;
					this.window.Top = this.control.Top;
					break;
				case ErrorIconAlignment.TopRight:
					this.window.Left = this.control.Left + this.control.Width + this.padding;
					this.window.Top = this.control.Top;
					break;
				case ErrorIconAlignment.MiddleLeft:
					this.window.Left = this.control.Left - this.ep.icon.Width - this.padding;
					this.window.Top = this.control.Top + (this.control.Height - this.ep.icon.Height) / 2;
					break;
				case ErrorIconAlignment.MiddleRight:
					this.window.Left = this.control.Left + this.control.Width + this.padding;
					this.window.Top = this.control.Top + (this.control.Height - this.ep.icon.Height) / 2;
					break;
				case ErrorIconAlignment.BottomLeft:
					this.window.Left = this.control.Left - this.ep.icon.Width - this.padding;
					this.window.Top = this.control.Top + this.control.Height - this.ep.icon.Height;
					break;
				case ErrorIconAlignment.BottomRight:
					this.window.Left = this.control.Left + this.control.Width + this.padding;
					this.window.Top = this.control.Top + this.control.Height - this.ep.icon.Height;
					break;
				}
			}

			// Token: 0x060017CE RID: 6094 RVA: 0x000570D4 File Offset: 0x000552D4
			private void window_Paint(object sender, PaintEventArgs e)
			{
				if (this.text != string.Empty)
				{
					e.Graphics.DrawIcon(this.ep.icon, 0, 0);
				}
			}

			// Token: 0x060017CF RID: 6095 RVA: 0x00057104 File Offset: 0x00055304
			private void window_MouseEnter(object sender, EventArgs e)
			{
				if (!this.visible)
				{
					this.visible = true;
					Point mousePosition = Control.MousePosition;
					Size size = ThemeEngine.Current.ToolTipSize(this.ep.tooltip, this.text);
					this.ep.tooltip.Width = size.Width;
					this.ep.tooltip.Height = size.Height;
					this.ep.tooltip.Text = this.text;
					if (mousePosition.X + size.Width < SystemInformation.WorkingArea.Width)
					{
						this.ep.tooltip.Left = mousePosition.X;
					}
					else
					{
						this.ep.tooltip.Left = mousePosition.X - size.Width;
					}
					if (mousePosition.Y + size.Height < SystemInformation.WorkingArea.Height - 16)
					{
						this.ep.tooltip.Top = mousePosition.Y + 16;
					}
					else
					{
						this.ep.tooltip.Top = mousePosition.Y - size.Height;
					}
					this.ep.UIAControl = this.control;
					this.ep.tooltip.Visible = true;
				}
			}

			// Token: 0x060017D0 RID: 6096 RVA: 0x0005726C File Offset: 0x0005546C
			private void window_MouseLeave(object sender, EventArgs e)
			{
				if (this.visible)
				{
					this.visible = false;
					this.ep.tooltip.Visible = false;
				}
			}

			// Token: 0x060017D1 RID: 6097 RVA: 0x00057294 File Offset: 0x00055494
			private void control_SizeLocationChanged(object sender, EventArgs e)
			{
				if (this.visible)
				{
					this.visible = false;
					this.ep.tooltip.Visible = false;
				}
				this.CalculateAlignment();
			}

			// Token: 0x060017D2 RID: 6098 RVA: 0x000572C0 File Offset: 0x000554C0
			private void control_ParentChanged(object sender, EventArgs e)
			{
				if (this.control.Parent != null)
				{
					ErrorProvider.OnUIAControlUnhookUp(this.control, new ControlEventArgs(this.window));
					this.control.Parent.Controls.Add(this.window);
					this.control.Parent.Controls.SetChildIndex(this.window, this.control.Parent.Controls.IndexOf(this.control) + 1);
					ErrorProvider.OnUIAControlHookUp(this.control, new ControlEventArgs(this.window));
				}
			}

			// Token: 0x060017D3 RID: 6099 RVA: 0x0005735C File Offset: 0x0005555C
			private void window_Tick(object sender, EventArgs e)
			{
				if (this.timer.Enabled && this.control.IsHandleCreated && this.control.Visible)
				{
					this.blink_count++;
					Graphics graphics = this.window.CreateGraphics();
					if (this.blink_count % 2 == 0)
					{
						graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.window.Parent.BackColor), this.window.ClientRectangle);
					}
					else
					{
						graphics.DrawIcon(this.ep.icon, 0, 0);
					}
					graphics.Dispose();
					switch (this.ep.blinkstyle)
					{
					case ErrorBlinkStyle.BlinkIfDifferentError:
						if (this.blink_count > 10)
						{
							this.timer.Stop();
						}
						break;
					case ErrorBlinkStyle.NeverBlink:
						this.timer.Stop();
						break;
					}
					if (this.blink_count == 11)
					{
						this.blink_count = 1;
					}
				}
			}

			// Token: 0x04000D21 RID: 3361
			public ErrorIconAlignment alignment;

			// Token: 0x04000D22 RID: 3362
			public int padding;

			// Token: 0x04000D23 RID: 3363
			public string text;

			// Token: 0x04000D24 RID: 3364
			public Control control;

			// Token: 0x04000D25 RID: 3365
			public ErrorProvider ep;

			// Token: 0x04000D26 RID: 3366
			private ErrorProvider.ErrorWindow window;

			// Token: 0x04000D27 RID: 3367
			private bool visible;

			// Token: 0x04000D28 RID: 3368
			private int blink_count;

			// Token: 0x04000D29 RID: 3369
			private EventHandler tick;

			// Token: 0x04000D2A RID: 3370
			private Timer timer;
		}
	}
}
