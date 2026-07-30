using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a multilevel switch to control tracing and debug output without recompiling your code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CD RID: 461
	[SwitchLevel(typeof(TraceLevel))]
	public class TraceSwitch : Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSwitch" /> class, using the specified display name and description.</summary>
		/// <param name="displayName">The name to display on a user interface. </param>
		/// <param name="description">The description of the switch. </param>
		// Token: 0x06000E24 RID: 3620 RVA: 0x0003D377 File Offset: 0x0003B577
		public TraceSwitch(string displayName, string description)
			: base(displayName, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSwitch" /> class, using the specified display name, description, and default value for the switch. </summary>
		/// <param name="displayName">The name to display on a user interface. </param>
		/// <param name="description">The description of the switch. </param>
		/// <param name="defaultSwitchValue">The default value of the switch.</param>
		// Token: 0x06000E25 RID: 3621 RVA: 0x0003D381 File Offset: 0x0003B581
		public TraceSwitch(string displayName, string description, string defaultSwitchValue)
			: base(displayName, description, defaultSwitchValue)
		{
		}

		/// <summary>Gets or sets the trace level that determines the messages the switch allows.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.TraceLevel" /> values that that specifies the level of messages that are allowed by the switch.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Diagnostics.TraceSwitch.Level" /> is set to a value that is not one of the <see cref="T:System.Diagnostics.TraceLevel" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0003ED14 File Offset: 0x0003CF14
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x00042FD9 File Offset: 0x000411D9
		public TraceLevel Level
		{
			get
			{
				return (TraceLevel)base.SwitchSetting;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				if (value < TraceLevel.Off || value > TraceLevel.Verbose)
				{
					throw new ArgumentException(global::SR.GetString("The Level must be set to a value in the enumeration TraceLevel."));
				}
				base.SwitchSetting = (int)value;
			}
		}

		/// <summary>Gets a value indicating whether the switch allows error-handling messages.</summary>
		/// <returns>true if the <see cref="P:System.Diagnostics.TraceSwitch.Level" /> property is set to <see cref="F:System.Diagnostics.TraceLevel.Error" />, <see cref="F:System.Diagnostics.TraceLevel.Warning" />, <see cref="F:System.Diagnostics.TraceLevel.Info" />, or <see cref="F:System.Diagnostics.TraceLevel.Verbose" />; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00042FFA File Offset: 0x000411FA
		public bool TraceError
		{
			get
			{
				return this.Level >= TraceLevel.Error;
			}
		}

		/// <summary>Gets a value indicating whether the switch allows warning messages.</summary>
		/// <returns>true if the <see cref="P:System.Diagnostics.TraceSwitch.Level" /> property is set to <see cref="F:System.Diagnostics.TraceLevel.Warning" />, <see cref="F:System.Diagnostics.TraceLevel.Info" />, or <see cref="F:System.Diagnostics.TraceLevel.Verbose" />; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x00043008 File Offset: 0x00041208
		public bool TraceWarning
		{
			get
			{
				return this.Level >= TraceLevel.Warning;
			}
		}

		/// <summary>Gets a value indicating whether the switch allows informational messages.</summary>
		/// <returns>true if the <see cref="P:System.Diagnostics.TraceSwitch.Level" /> property is set to <see cref="F:System.Diagnostics.TraceLevel.Info" /> or <see cref="F:System.Diagnostics.TraceLevel.Verbose" />; otherwise, false.  </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00043016 File Offset: 0x00041216
		public bool TraceInfo
		{
			get
			{
				return this.Level >= TraceLevel.Info;
			}
		}

		/// <summary>Gets a value indicating whether the switch allows all messages.</summary>
		/// <returns>true if the <see cref="P:System.Diagnostics.TraceSwitch.Level" /> property is set to <see cref="F:System.Diagnostics.TraceLevel.Verbose" />; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00043024 File Offset: 0x00041224
		public bool TraceVerbose
		{
			get
			{
				return this.Level == TraceLevel.Verbose;
			}
		}

		/// <summary>Updates and corrects the level for this switch.</summary>
		// Token: 0x06000E2C RID: 3628 RVA: 0x00043030 File Offset: 0x00041230
		protected override void OnSwitchSettingChanged()
		{
			int switchSetting = base.SwitchSetting;
			if (switchSetting < 0)
			{
				base.SwitchSetting = 0;
				return;
			}
			if (switchSetting > 4)
			{
				base.SwitchSetting = 4;
			}
		}

		/// <summary>Sets the <see cref="P:System.Diagnostics.Switch.SwitchSetting" /> property to the integer equivalent of the <see cref="P:System.Diagnostics.Switch.Value" /> property.</summary>
		// Token: 0x06000E2D RID: 3629 RVA: 0x0004305B File Offset: 0x0004125B
		protected override void OnValueChanged()
		{
			base.SwitchSetting = (int)Enum.Parse(typeof(TraceLevel), base.Value, true);
		}
	}
}
