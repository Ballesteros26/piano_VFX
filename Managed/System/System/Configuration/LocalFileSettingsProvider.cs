using System;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Configuration
{
	/// <summary>Provides persistence for application settings classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000181 RID: 385
	public class LocalFileSettingsProvider : SettingsProvider, IApplicationSettingsProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.LocalFileSettingsProvider" /> class.</summary>
		// Token: 0x06000B9B RID: 2971 RVA: 0x0003BDBB File Offset: 0x00039FBB
		public LocalFileSettingsProvider()
		{
			this.impl = new CustomizableFileSettingsProvider();
		}

		/// <summary>Returns the value of the named settings property for the previous version of the same application. </summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValue" /> representing the application setting if found; otherwise, null.</returns>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> that describes where the application settings property is used.</param>
		/// <param name="property">The <see cref="T:System.Configuration.SettingsProperty" /> whose value is to be returned.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06000B9C RID: 2972 RVA: 0x0003BDCE File Offset: 0x00039FCE
		[MonoTODO]
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery)]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property)
		{
			return this.impl.GetPreviousVersion(context, property);
		}

		/// <summary>Returns the collection of setting property values for the specified application instance and settings property group.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> containing the values for the specified settings property group.</returns>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		/// <param name="properties">A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing the settings property group whose values are to be retrieved.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A user-scoped setting was encountered but the current configuration only supports application-scoped settings.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06000B9D RID: 2973 RVA: 0x0003BDDD File Offset: 0x00039FDD
		[MonoTODO]
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection properties)
		{
			return this.impl.GetPropertyValues(context, properties);
		}

		/// <param name="name">The name of the provider.</param>
		/// <param name="values">The values for initialization.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B9E RID: 2974 RVA: 0x0003BDEC File Offset: 0x00039FEC
		public override void Initialize(string name, NameValueCollection values)
		{
			if (name == null)
			{
				name = "LocalFileSettingsProvider";
			}
			if (values != null)
			{
				this.impl.ApplicationName = values["applicationName"];
			}
			base.Initialize(name, values);
		}

		/// <summary>Resets all application settings properties associated with the specified application to their default values.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A user-scoped setting was encountered but the current configuration only supports application-scoped settings.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000B9F RID: 2975 RVA: 0x0003BE19 File Offset: 0x0003A019
		[MonoTODO]
		public void Reset(SettingsContext context)
		{
			this.impl.Reset(context);
		}

		/// <summary>Sets the values of the specified group of property settings.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		/// <param name="values">A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> representing the group of property settings to set.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A user-scoped setting was encountered but the current configuration only supports application-scoped settings.-or-There was a general failure saving the settings to the configuration file.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06000BA0 RID: 2976 RVA: 0x0003BE27 File Offset: 0x0003A027
		[MonoTODO]
		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection values)
		{
			this.impl.SetPropertyValues(context, values);
		}

		/// <summary>Attempts to migrate previous user-scoped settings from a previous version of the same application.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage. </param>
		/// <param name="properties">A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing the settings property group whose values are to be retrieved. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A user-scoped setting was encountered but the current configuration only supports application-scoped settings.-or-The previous version of the configuration file could not be accessed.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06000BA1 RID: 2977 RVA: 0x0003BE36 File Offset: 0x0003A036
		[MonoTODO]
		public void Upgrade(SettingsContext context, SettingsPropertyCollection properties)
		{
			this.impl.Upgrade(context, properties);
		}

		/// <summary>Gets or sets the name of the currently running application.</summary>
		/// <returns>A string that contains the application's display name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0003BE45 File Offset: 0x0003A045
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x0003BE52 File Offset: 0x0003A052
		public override string ApplicationName
		{
			get
			{
				return this.impl.ApplicationName;
			}
			set
			{
				this.impl.ApplicationName = value;
			}
		}

		// Token: 0x04000FCE RID: 4046
		private CustomizableFileSettingsProvider impl;
	}
}
