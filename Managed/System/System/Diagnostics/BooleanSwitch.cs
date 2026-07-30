using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a simple on/off switch that controls debugging and tracing output.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A9 RID: 425
	[SwitchLevel(typeof(bool))]
	public class BooleanSwitch : Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.BooleanSwitch" /> class with the specified display name and description.</summary>
		/// <param name="displayName">The name to display on a user interface. </param>
		/// <param name="description">The description of the switch. </param>
		// Token: 0x06000C6C RID: 3180 RVA: 0x0003D377 File Offset: 0x0003B577
		public BooleanSwitch(string displayName, string description)
			: base(displayName, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.BooleanSwitch" /> class with the specified display name, description, and default switch value. </summary>
		/// <param name="displayName">The name to display on the user interface. </param>
		/// <param name="description">The description of the switch. </param>
		/// <param name="defaultSwitchValue">The default value of the switch.</param>
		// Token: 0x06000C6D RID: 3181 RVA: 0x0003D381 File Offset: 0x0003B581
		public BooleanSwitch(string displayName, string description, string defaultSwitchValue)
			: base(displayName, description, defaultSwitchValue)
		{
		}

		/// <summary>Gets or sets a value indicating whether the switch is enabled or disabled.</summary>
		/// <returns>true if the switch is enabled; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permission.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0003D38C File Offset: 0x0003B58C
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x0003D399 File Offset: 0x0003B599
		public bool Enabled
		{
			get
			{
				return base.SwitchSetting != 0;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				base.SwitchSetting = (value ? 1 : 0);
			}
		}

		/// <summary>Determines whether the new value of the <see cref="P:System.Diagnostics.Switch.Value" /> property can be parsed as a Boolean value.</summary>
		// Token: 0x06000C70 RID: 3184 RVA: 0x0003D3A8 File Offset: 0x0003B5A8
		protected override void OnValueChanged()
		{
			bool flag;
			if (bool.TryParse(base.Value, out flag))
			{
				base.SwitchSetting = (flag ? 1 : 0);
				return;
			}
			base.OnValueChanged();
		}
	}
}
