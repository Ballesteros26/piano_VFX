using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;

namespace System.Configuration
{
	/// <summary>Acts as a base class for deriving concrete wrapper classes to implement the application settings feature in Window Forms applications.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000162 RID: 354
	public abstract class ApplicationSettingsBase : SettingsBase, INotifyPropertyChanged
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class to its default state.</summary>
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0003894C File Offset: 0x00036B4C
		protected ApplicationSettingsBase()
		{
			base.Initialize(this.Context, this.Properties, this.Providers);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied owner component.</summary>
		/// <param name="owner">The component that will act as the owner of the application settings object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x06000AB1 RID: 2737 RVA: 0x0003896C File Offset: 0x00036B6C
		protected ApplicationSettingsBase(IComponent owner)
			: this(owner, string.Empty)
		{
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied settings key.</summary>
		/// <param name="settingsKey">A <see cref="T:System.String" /> that uniquely identifies separate instances of the wrapper class.</param>
		// Token: 0x06000AB2 RID: 2738 RVA: 0x0003897A File Offset: 0x00036B7A
		protected ApplicationSettingsBase(string settingsKey)
		{
			this.settingsKey = settingsKey;
			base.Initialize(this.Context, this.Properties, this.Providers);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied owner component and settings key.</summary>
		/// <param name="owner">The component that will act as the owner of the application settings object.</param>
		/// <param name="settingsKey">A <see cref="T:System.String" /> that uniquely identifies separate instances of the wrapper class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x06000AB3 RID: 2739 RVA: 0x000389A4 File Offset: 0x00036BA4
		protected ApplicationSettingsBase(IComponent owner, string settingsKey)
		{
			if (owner == null)
			{
				throw new ArgumentNullException();
			}
			this.providerService = (ISettingsProviderService)owner.Site.GetService(typeof(ISettingsProviderService));
			this.settingsKey = settingsKey;
			base.Initialize(this.Context, this.Properties, this.Providers);
		}

		/// <summary>Occurs after the value of an application settings property is changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000AB4 RID: 2740 RVA: 0x00038A00 File Offset: 0x00036C00
		// (remove) Token: 0x06000AB5 RID: 2741 RVA: 0x00038A38 File Offset: 0x00036C38
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Occurs before the value of an application settings property is changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000AB6 RID: 2742 RVA: 0x00038A70 File Offset: 0x00036C70
		// (remove) Token: 0x06000AB7 RID: 2743 RVA: 0x00038AA8 File Offset: 0x00036CA8
		public event SettingChangingEventHandler SettingChanging;

		/// <summary>Occurs after the application settings are retrieved from storage.</summary>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000AB8 RID: 2744 RVA: 0x00038AE0 File Offset: 0x00036CE0
		// (remove) Token: 0x06000AB9 RID: 2745 RVA: 0x00038B18 File Offset: 0x00036D18
		public event SettingsLoadedEventHandler SettingsLoaded;

		/// <summary>Occurs before values are saved to the data store.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000ABA RID: 2746 RVA: 0x00038B50 File Offset: 0x00036D50
		// (remove) Token: 0x06000ABB RID: 2747 RVA: 0x00038B88 File Offset: 0x00036D88
		public event SettingsSavingEventHandler SettingsSaving;

		/// <summary>Returns the value of the named settings property for the previous version of the same application.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the value of the specified <see cref="T:System.Configuration.SettingsProperty" /> if found; otherwise, null.</returns>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the settings property whose value is to be returned.</param>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">The property does not exist. The property count is zero or the property cannot be found in the data store.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06000ABC RID: 2748 RVA: 0x00004239 File Offset: 0x00002439
		public object GetPreviousVersion(string propertyName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Refreshes the application settings property values from persistent storage.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ABD RID: 2749 RVA: 0x00038BC0 File Offset: 0x00036DC0
		public void Reload()
		{
			foreach (object obj in this.Providers)
			{
				SettingsProvider settingsProvider = (SettingsProvider)obj;
				this.CacheValuesByProvider(settingsProvider);
			}
		}

		/// <summary>Restores the persisted application settings values to their corresponding default properties.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be parsed.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000ABE RID: 2750 RVA: 0x00038C1C File Offset: 0x00036E1C
		public void Reset()
		{
			this.Reload();
			foreach (object obj in this.PropertyValues)
			{
				SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
				settingsPropertyValue.PropertyValue = settingsPropertyValue.Reset();
			}
		}

		/// <summary>Stores the current values of the application settings properties.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ABF RID: 2751 RVA: 0x00038C80 File Offset: 0x00036E80
		public override void Save()
		{
			this.Context.CurrentSettings = this;
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
			this.Context.CurrentSettings = null;
		}

		/// <summary>Updates application settings to reflect a more recent installation of the application.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be parsed.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000AC0 RID: 2752 RVA: 0x000027E8 File Offset: 0x000009E8
		public virtual void Upgrade()
		{
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.PropertyChanged" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000AC1 RID: 2753 RVA: 0x00038D68 File Offset: 0x00036F68
		protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(sender, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Configuration.SettingChangingEventArgs" /> that contains the event data.</param>
		// Token: 0x06000AC2 RID: 2754 RVA: 0x00038D7F File Offset: 0x00036F7F
		protected virtual void OnSettingChanging(object sender, SettingChangingEventArgs e)
		{
			if (this.SettingChanging != null)
			{
				this.SettingChanging(sender, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsLoaded" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Configuration.SettingsLoadedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000AC3 RID: 2755 RVA: 0x00038D96 File Offset: 0x00036F96
		protected virtual void OnSettingsLoaded(object sender, SettingsLoadedEventArgs e)
		{
			if (this.SettingsLoaded != null)
			{
				this.SettingsLoaded(sender, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsSaving" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06000AC4 RID: 2756 RVA: 0x00038DAD File Offset: 0x00036FAD
		protected virtual void OnSettingsSaving(object sender, CancelEventArgs e)
		{
			if (this.SettingsSaving != null)
			{
				this.SettingsSaving(sender, e);
			}
		}

		/// <summary>Gets the application settings context associated with the settings group.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsContext" /> associated with the settings group.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00038DC4 File Offset: 0x00036FC4
		[Browsable(false)]
		public override SettingsContext Context
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsContext settingsContext;
				try
				{
					if (this.context == null)
					{
						this.context = new SettingsContext();
						this.context["SettingsKey"] = "";
						Type type = base.GetType();
						this.context["GroupName"] = type.FullName;
						this.context["SettingsClassType"] = type;
					}
					settingsContext = this.context;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return settingsContext;
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00038E60 File Offset: 0x00037060
		private void CacheValuesByProvider(SettingsProvider provider)
		{
			SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
			foreach (object obj in this.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (settingsProperty.Provider == provider)
				{
					settingsPropertyCollection.Add(settingsProperty);
				}
			}
			if (settingsPropertyCollection.Count > 0)
			{
				foreach (object obj2 in provider.GetPropertyValues(this.Context, settingsPropertyCollection))
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj2;
					if (this.PropertyValues[settingsPropertyValue.Name] != null)
					{
						this.PropertyValues[settingsPropertyValue.Name].PropertyValue = settingsPropertyValue.PropertyValue;
					}
					else
					{
						this.PropertyValues.Add(settingsPropertyValue);
					}
				}
			}
			this.OnSettingsLoaded(this, new SettingsLoadedEventArgs(provider));
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000027E8 File Offset: 0x000009E8
		private void InitializeSettings(SettingsPropertyCollection settings)
		{
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00038F68 File Offset: 0x00037168
		private object GetPropertyValue(string propertyName)
		{
			SettingsProperty settingsProperty = this.Properties[propertyName];
			if (settingsProperty == null)
			{
				throw new SettingsPropertyNotFoundException(propertyName);
			}
			if (this.propertyValues == null)
			{
				this.InitializeSettings(this.Properties);
			}
			if (this.PropertyValues[propertyName] == null)
			{
				this.CacheValuesByProvider(settingsProperty.Provider);
			}
			return this.PropertyValues[propertyName].PropertyValue;
		}

		/// <summary>Gets or sets the value of the specified application settings property.</summary>
		/// <returns>If found, the value of the named settings property; otherwise, null.</returns>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the property to access.</param>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties associated with the current wrapper or the specified property could not be found.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyIsReadOnlyException">An attempt was made to set a read-only property.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyWrongTypeException">The value supplied is of a type incompatible with the settings property, during a set operation.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be parsed.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001AA RID: 426
		[MonoTODO]
		public override object this[string propertyName]
		{
			get
			{
				if (base.IsSynchronized)
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
				SettingsProperty settingsProperty = this.Properties[propertyName];
				if (settingsProperty == null)
				{
					throw new SettingsPropertyNotFoundException(propertyName);
				}
				if (settingsProperty.IsReadOnly)
				{
					throw new SettingsPropertyIsReadOnlyException(propertyName);
				}
				if (value != null && !settingsProperty.PropertyType.IsAssignableFrom(value.GetType()))
				{
					throw new SettingsPropertyWrongTypeException(propertyName);
				}
				if (this.PropertyValues[propertyName] == null)
				{
					this.CacheValuesByProvider(settingsProperty.Provider);
				}
				SettingChangingEventArgs settingChangingEventArgs = new SettingChangingEventArgs(propertyName, base.GetType().FullName, this.settingsKey, value, false);
				this.OnSettingChanging(this, settingChangingEventArgs);
				if (!settingChangingEventArgs.Cancel)
				{
					this.PropertyValues[propertyName].PropertyValue = value;
					this.OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				}
			}
		}

		/// <summary>Gets the collection of settings properties in the wrapper.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing all the <see cref="T:System.Configuration.SettingsProperty" /> objects used in the current wrapper.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The associated settings provider could not be found or its instantiation failed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000390D0 File Offset: 0x000372D0
		[Browsable(false)]
		public override SettingsPropertyCollection Properties
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsPropertyCollection settingsPropertyCollection;
				try
				{
					if (this.properties == null)
					{
						SettingsProvider settingsProvider = null;
						this.properties = new SettingsPropertyCollection();
						Type type = base.GetType();
						SettingsProviderAttribute[] array = (SettingsProviderAttribute[])type.GetCustomAttributes(typeof(SettingsProviderAttribute), false);
						if (array != null && array.Length != 0)
						{
							SettingsProvider settingsProvider2 = (SettingsProvider)Activator.CreateInstance(Type.GetType(array[0].ProviderTypeName));
							settingsProvider2.Initialize(null, null);
							if (settingsProvider2 != null && this.Providers[settingsProvider2.Name] == null)
							{
								this.Providers.Add(settingsProvider2);
								settingsProvider = settingsProvider2;
							}
						}
						foreach (PropertyInfo propertyInfo in type.GetProperties())
						{
							SettingAttribute[] array3 = (SettingAttribute[])propertyInfo.GetCustomAttributes(typeof(SettingAttribute), false);
							if (array3 != null && array3.Length != 0)
							{
								this.CreateSettingsProperty(propertyInfo, this.properties, ref settingsProvider);
							}
						}
					}
					settingsPropertyCollection = this.properties;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return settingsPropertyCollection;
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000391E8 File Offset: 0x000373E8
		private void CreateSettingsProperty(PropertyInfo prop, SettingsPropertyCollection properties, ref SettingsProvider local_provider)
		{
			SettingsAttributeDictionary settingsAttributeDictionary = new SettingsAttributeDictionary();
			SettingsProvider settingsProvider = null;
			object obj = null;
			SettingsSerializeAs settingsSerializeAs = SettingsSerializeAs.String;
			bool flag = false;
			foreach (Attribute attribute in prop.GetCustomAttributes(false))
			{
				if (attribute is SettingsProviderAttribute)
				{
					settingsProvider = (SettingsProvider)Activator.CreateInstance(Type.GetType(((SettingsProviderAttribute)attribute).ProviderTypeName));
					settingsProvider.Initialize(null, null);
				}
				else if (attribute is DefaultSettingValueAttribute)
				{
					obj = ((DefaultSettingValueAttribute)attribute).Value;
				}
				else if (attribute is SettingsSerializeAsAttribute)
				{
					settingsSerializeAs = ((SettingsSerializeAsAttribute)attribute).SerializeAs;
					flag = true;
				}
				else if (attribute is ApplicationScopedSettingAttribute || attribute is UserScopedSettingAttribute)
				{
					settingsAttributeDictionary.Add(attribute.GetType(), attribute);
				}
				else
				{
					settingsAttributeDictionary.Add(attribute.GetType(), attribute);
				}
			}
			if (!flag)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(prop.PropertyType);
				if (converter != null && (!converter.CanConvertFrom(typeof(string)) || !converter.CanConvertTo(typeof(string))))
				{
					settingsSerializeAs = SettingsSerializeAs.Xml;
				}
			}
			SettingsProperty settingsProperty = new SettingsProperty(prop.Name, prop.PropertyType, settingsProvider, false, obj, settingsSerializeAs, settingsAttributeDictionary, false, false);
			if (this.providerService != null)
			{
				settingsProperty.Provider = this.providerService.GetSettingsProvider(settingsProperty);
			}
			if (settingsProvider == null)
			{
				if (local_provider == null)
				{
					local_provider = new LocalFileSettingsProvider();
					local_provider.Initialize(null, null);
				}
				settingsProperty.Provider = local_provider;
				settingsProvider = local_provider;
			}
			if (settingsProvider != null)
			{
				SettingsProvider settingsProvider2 = this.Providers[settingsProvider.Name];
				if (settingsProvider2 != null)
				{
					settingsProperty.Provider = settingsProvider2;
				}
			}
			properties.Add(settingsProperty);
			if (settingsProperty.Provider != null && this.Providers[settingsProperty.Provider.Name] == null)
			{
				this.Providers.Add(settingsProperty.Provider);
			}
		}

		/// <summary>Gets a collection of property values.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> of property values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000393BC File Offset: 0x000375BC
		[Browsable(false)]
		public override SettingsPropertyValueCollection PropertyValues
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsPropertyValueCollection settingsPropertyValueCollection;
				try
				{
					if (this.propertyValues == null)
					{
						this.propertyValues = new SettingsPropertyValueCollection();
					}
					settingsPropertyValueCollection = this.propertyValues;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return settingsPropertyValueCollection;
			}
		}

		/// <summary>Gets the collection of application settings providers used by the wrapper.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProviderCollection" /> containing all the <see cref="T:System.Configuration.SettingsProvider" /> objects used by the settings properties of the current settings wrapper.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00039414 File Offset: 0x00037614
		[Browsable(false)]
		public override SettingsProviderCollection Providers
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsProviderCollection settingsProviderCollection;
				try
				{
					if (this.providers == null)
					{
						this.providers = new SettingsProviderCollection();
					}
					settingsProviderCollection = this.providers;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return settingsProviderCollection;
			}
		}

		/// <summary>Gets or sets the settings key for the application settings group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the settings key for the current settings group.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0003946C File Offset: 0x0003766C
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x00039474 File Offset: 0x00037674
		[Browsable(false)]
		public string SettingsKey
		{
			get
			{
				return this.settingsKey;
			}
			set
			{
				this.settingsKey = value;
			}
		}

		// Token: 0x04000F70 RID: 3952
		private string settingsKey;

		// Token: 0x04000F71 RID: 3953
		private SettingsContext context;

		// Token: 0x04000F72 RID: 3954
		private SettingsPropertyCollection properties;

		// Token: 0x04000F73 RID: 3955
		private ISettingsProviderService providerService;

		// Token: 0x04000F74 RID: 3956
		private SettingsPropertyValueCollection propertyValues;

		// Token: 0x04000F75 RID: 3957
		private SettingsProviderCollection providers;
	}
}
