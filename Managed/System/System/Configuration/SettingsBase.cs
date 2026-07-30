using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Provides the base class used to support user property settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018C RID: 396
	public abstract class SettingsBase
	{
		/// <summary>Initializes internal properties used by <see cref="T:System.Configuration.SettingsBase" /> object.</summary>
		/// <param name="context">The settings context related to the settings properties.</param>
		/// <param name="properties">The settings properties that will be accessible from the <see cref="T:System.Configuration.SettingsBase" /> instance.</param>
		/// <param name="providers">The initialized providers that should be used when loading and saving property values.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BDE RID: 3038 RVA: 0x0003C4D9 File Offset: 0x0003A6D9
		public void Initialize(SettingsContext context, SettingsPropertyCollection properties, SettingsProviderCollection providers)
		{
			this.context = context;
			this.properties = properties;
			this.providers = providers;
		}

		/// <summary>Stores the current values of the settings properties.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000BDF RID: 3039 RVA: 0x0003C4F0 File Offset: 0x0003A6F0
		public virtual void Save()
		{
			if (this.sync)
			{
				lock (this)
				{
					this.SaveCore();
					return;
				}
			}
			this.SaveCore();
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0003C53C File Offset: 0x0003A73C
		private void SaveCore()
		{
			foreach (object obj in this.Providers)
			{
				SettingsProvider settingsProvider = (SettingsProvider)obj;
				SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
				foreach (object obj2 in this.PropertyValues)
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj2;
					if (settingsPropertyValue.Property.Provider == settingsProvider)
					{
						settingsPropertyValueCollection.Add(settingsPropertyValue);
					}
				}
				if (settingsPropertyValueCollection.Count > 0)
				{
					settingsProvider.SetPropertyValues(this.Context, settingsPropertyValueCollection);
				}
			}
		}

		/// <summary>Provides a <see cref="T:System.Configuration.SettingsBase" /> class that is synchronized (thread safe).</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsBase" /> class that is synchronized.</returns>
		/// <param name="settingsBase">The class used to support user property settings.</param>
		// Token: 0x06000BE1 RID: 3041 RVA: 0x0003C60C File Offset: 0x0003A80C
		public static SettingsBase Synchronized(SettingsBase settingsBase)
		{
			settingsBase.sync = true;
			return settingsBase;
		}

		/// <summary>Gets the associated settings context.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsContext" /> associated with the settings instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0003C616 File Offset: 0x0003A816
		public virtual SettingsContext Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets a value indicating whether access to the object is synchronized (thread safe). </summary>
		/// <returns>true if access to the <see cref="T:System.Configuration.SettingsBase" /> is synchronized; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0003C61E File Offset: 0x0003A81E
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return this.sync;
			}
		}

		/// <summary>Gets or sets the value of the specified settings property.</summary>
		/// <returns>If found, the value of the named settings property.</returns>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the property to access.</param>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties associated with the current object, or the specified property could not be found.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyIsReadOnlyException">An attempt was made to set a read-only property.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyWrongTypeException">The value supplied is of a type incompatible with the settings property, during a set operation.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x170001F6 RID: 502
		public virtual object this[string propertyName]
		{
			get
			{
				if (this.sync)
				{
					lock (this)
					{
						return this.GetPropertyValue(propertyName);
					}
				}
				return this.GetPropertyValue(propertyName);
			}
			set
			{
				if (this.sync)
				{
					lock (this)
					{
						this.SetPropertyValue(propertyName, value);
						return;
					}
				}
				this.SetPropertyValue(propertyName, value);
			}
		}

		/// <summary>Gets the collection of settings properties.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> collection containing all the <see cref="T:System.Configuration.SettingsProperty" /> objects.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0003C6C8 File Offset: 0x0003A8C8
		public virtual SettingsPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets a collection of settings property values.</summary>
		/// <returns>A collection of <see cref="T:System.Configuration.SettingsPropertyValue" /> objects representing the actual data values for the properties managed by the <see cref="T:System.Configuration.SettingsBase" /> instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0003C6D0 File Offset: 0x0003A8D0
		public virtual SettingsPropertyValueCollection PropertyValues
		{
			get
			{
				if (this.sync)
				{
					lock (this)
					{
						return this.values;
					}
				}
				return this.values;
			}
		}

		/// <summary>Gets a collection of settings providers.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProviderCollection" /> containing <see cref="T:System.Configuration.SettingsProvider" /> objects.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0003C71C File Offset: 0x0003A91C
		public virtual SettingsProviderCollection Providers
		{
			get
			{
				return this.providers;
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0003C724 File Offset: 0x0003A924
		private object GetPropertyValue(string propertyName)
		{
			SettingsProperty settingsProperty;
			if (this.Properties == null || (settingsProperty = this.Properties[propertyName]) == null)
			{
				throw new SettingsPropertyNotFoundException(string.Format("The settings property '{0}' was not found", propertyName));
			}
			if (this.values[propertyName] == null)
			{
				foreach (object obj in settingsProperty.Provider.GetPropertyValues(this.Context, this.Properties))
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
					this.values.Add(settingsPropertyValue);
				}
			}
			return this.PropertyValues[propertyName].PropertyValue;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0003C7E0 File Offset: 0x0003A9E0
		private void SetPropertyValue(string propertyName, object value)
		{
			SettingsProperty settingsProperty;
			if (this.Properties == null || (settingsProperty = this.Properties[propertyName]) == null)
			{
				throw new SettingsPropertyNotFoundException(string.Format("The settings property '{0}' was not found", propertyName));
			}
			if (settingsProperty.IsReadOnly)
			{
				throw new SettingsPropertyIsReadOnlyException(string.Format("The settings property '{0}' is read only", propertyName));
			}
			if (settingsProperty.PropertyType != value.GetType())
			{
				throw new SettingsPropertyWrongTypeException(string.Format("The value supplied is of a type incompatible with the settings property '{0}'", propertyName));
			}
			this.PropertyValues[propertyName].PropertyValue = value;
		}

		// Token: 0x04000FD9 RID: 4057
		private bool sync;

		// Token: 0x04000FDA RID: 4058
		private SettingsContext context;

		// Token: 0x04000FDB RID: 4059
		private SettingsPropertyCollection properties;

		// Token: 0x04000FDC RID: 4060
		private SettingsProviderCollection providers;

		// Token: 0x04000FDD RID: 4061
		private SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();
	}
}
