using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Xml.Serialization;

namespace System.Diagnostics
{
	/// <summary>Provides an abstract base class to create new debugging and tracing switches.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001BB RID: 443
	public abstract class Switch
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0003ED58 File Offset: 0x0003CF58
		private object IntializedLock
		{
			get
			{
				if (this.m_intializedLock == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref this.m_intializedLock, obj, null);
				}
				return this.m_intializedLock;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Switch" /> class.</summary>
		/// <param name="displayName">The name of the switch. </param>
		/// <param name="description">The description for the switch. </param>
		// Token: 0x06000D0D RID: 3341 RVA: 0x0003ED87 File Offset: 0x0003CF87
		protected Switch(string displayName, string description)
			: this(displayName, description, "0")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Switch" /> class, specifying the display name, description, and default value for the switch. </summary>
		/// <param name="displayName">The name of the switch. </param>
		/// <param name="description">The description of the switch. </param>
		/// <param name="defaultSwitchValue">The default value for the switch.</param>
		// Token: 0x06000D0E RID: 3342 RVA: 0x0003ED98 File Offset: 0x0003CF98
		protected Switch(string displayName, string description, string defaultSwitchValue)
		{
			if (displayName == null)
			{
				displayName = string.Empty;
			}
			this.displayName = displayName;
			this.description = description;
			List<WeakReference> list = Switch.switches;
			lock (list)
			{
				Switch._pruneCachedSwitches();
				Switch.switches.Add(new WeakReference(this));
			}
			this.defaultValue = defaultSwitchValue;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003EE18 File Offset: 0x0003D018
		private static void _pruneCachedSwitches()
		{
			List<WeakReference> list = Switch.switches;
			lock (list)
			{
				if (Switch.s_LastCollectionCount != GC.CollectionCount(2))
				{
					List<WeakReference> list2 = new List<WeakReference>(Switch.switches.Count);
					for (int i = 0; i < Switch.switches.Count; i++)
					{
						if ((Switch)Switch.switches[i].Target != null)
						{
							list2.Add(Switch.switches[i]);
						}
					}
					if (list2.Count < Switch.switches.Count)
					{
						Switch.switches.Clear();
						Switch.switches.AddRange(list2);
						Switch.switches.TrimExcess();
					}
					Switch.s_LastCollectionCount = GC.CollectionCount(2);
				}
			}
		}

		/// <summary>Gets the custom switch attributes defined in the application configuration file.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> containing the case-insensitive custom attributes for the trace switch.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0003EEEC File Offset: 0x0003D0EC
		[XmlIgnore]
		public StringDictionary Attributes
		{
			get
			{
				this.Initialize();
				if (this.attributes == null)
				{
					this.attributes = new StringDictionary();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets a name used to identify the switch.</summary>
		/// <returns>The name used to identify the switch. The default value is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0003EF0D File Offset: 0x0003D10D
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		/// <summary>Gets a description of the switch.</summary>
		/// <returns>The description of the switch. The default value is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0003EF15 File Offset: 0x0003D115
		public string Description
		{
			get
			{
				if (this.description != null)
				{
					return this.description;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets or sets the current setting for this switch.</summary>
		/// <returns>The current setting for this switch. The default is zero.</returns>
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0003EF2B File Offset: 0x0003D12B
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x0003EF4C File Offset: 0x0003D14C
		protected int SwitchSetting
		{
			get
			{
				if (!this.initialized && this.InitializeWithStatus())
				{
					this.OnSwitchSettingChanged();
				}
				return this.switchSetting;
			}
			set
			{
				bool flag = false;
				object intializedLock = this.IntializedLock;
				lock (intializedLock)
				{
					this.initialized = true;
					if (this.switchSetting != value)
					{
						this.switchSetting = value;
						flag = true;
					}
				}
				if (flag)
				{
					this.OnSwitchSettingChanged();
				}
			}
		}

		/// <summary>Gets or sets the value of the switch.</summary>
		/// <returns>A string representing the value of the switch.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The value is null.-or-The value does not consist solely of an optional negative sign followed by a sequence of digits ranging from 0 to 9.-or-The value represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0003EFAC File Offset: 0x0003D1AC
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x0003EFBC File Offset: 0x0003D1BC
		protected string Value
		{
			get
			{
				this.Initialize();
				return this.switchValueString;
			}
			set
			{
				this.Initialize();
				this.switchValueString = value;
				try
				{
					this.OnValueChanged();
				}
				catch (ArgumentException ex)
				{
					throw new ConfigurationErrorsException(global::SR.GetString("The config value for Switch '{0}' was invalid.", new object[] { this.DisplayName }), ex);
				}
				catch (FormatException ex2)
				{
					throw new ConfigurationErrorsException(global::SR.GetString("The config value for Switch '{0}' was invalid.", new object[] { this.DisplayName }), ex2);
				}
				catch (OverflowException ex3)
				{
					throw new ConfigurationErrorsException(global::SR.GetString("The config value for Switch '{0}' was invalid.", new object[] { this.DisplayName }), ex3);
				}
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0003F06C File Offset: 0x0003D26C
		private void Initialize()
		{
			this.InitializeWithStatus();
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0003F078 File Offset: 0x0003D278
		private bool InitializeWithStatus()
		{
			if (!this.initialized)
			{
				object intializedLock = this.IntializedLock;
				lock (intializedLock)
				{
					if (this.initialized || this.initializing)
					{
						return false;
					}
					this.initializing = true;
					if (this.switchSettings == null && !this.InitializeConfigSettings())
					{
						this.initialized = true;
						this.initializing = false;
						return false;
					}
					if (this.switchSettings != null)
					{
						SwitchElement switchElement = this.switchSettings[this.displayName];
						if (switchElement != null)
						{
							string value = switchElement.Value;
							if (value != null)
							{
								this.Value = value;
							}
							else
							{
								this.Value = this.defaultValue;
							}
							try
							{
								TraceUtils.VerifyAttributes(switchElement.Attributes, this.GetSupportedAttributes(), this);
							}
							catch (ConfigurationException)
							{
								this.initialized = false;
								this.initializing = false;
								throw;
							}
							this.attributes = new StringDictionary();
							this.attributes.ReplaceHashtable(switchElement.Attributes);
						}
						else
						{
							this.switchValueString = this.defaultValue;
							this.OnValueChanged();
						}
					}
					else
					{
						this.switchValueString = this.defaultValue;
						this.OnValueChanged();
					}
					this.initialized = true;
					this.initializing = false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0003F1F0 File Offset: 0x0003D3F0
		private bool InitializeConfigSettings()
		{
			if (this.switchSettings != null)
			{
				return true;
			}
			if (!DiagnosticsConfiguration.CanInitialize())
			{
				return false;
			}
			this.switchSettings = DiagnosticsConfiguration.SwitchSettings;
			return true;
		}

		/// <summary>Gets the custom attributes supported by the switch.</summary>
		/// <returns>A string array that contains the names of the custom attributes supported by the switch, or null if there no custom attributes are supported.</returns>
		// Token: 0x06000D1A RID: 3354 RVA: 0x00009E57 File Offset: 0x00008057
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		/// <summary>Invoked when the <see cref="P:System.Diagnostics.Switch.SwitchSetting" /> property is changed.</summary>
		// Token: 0x06000D1B RID: 3355 RVA: 0x000027E8 File Offset: 0x000009E8
		protected virtual void OnSwitchSettingChanged()
		{
		}

		/// <summary>Invoked when the <see cref="P:System.Diagnostics.Switch.Value" /> property is changed.</summary>
		// Token: 0x06000D1C RID: 3356 RVA: 0x0003F211 File Offset: 0x0003D411
		protected virtual void OnValueChanged()
		{
			this.SwitchSetting = int.Parse(this.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0003F22C File Offset: 0x0003D42C
		internal static void RefreshAll()
		{
			List<WeakReference> list = Switch.switches;
			lock (list)
			{
				Switch._pruneCachedSwitches();
				for (int i = 0; i < Switch.switches.Count; i++)
				{
					Switch @switch = (Switch)Switch.switches[i].Target;
					if (@switch != null)
					{
						@switch.Refresh();
					}
				}
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0003F2A0 File Offset: 0x0003D4A0
		internal void Refresh()
		{
			object intializedLock = this.IntializedLock;
			lock (intializedLock)
			{
				this.initialized = false;
				this.switchSettings = null;
				this.Initialize();
			}
		}

		// Token: 0x04001032 RID: 4146
		private SwitchElementsCollection switchSettings;

		// Token: 0x04001033 RID: 4147
		private readonly string description;

		// Token: 0x04001034 RID: 4148
		private readonly string displayName;

		// Token: 0x04001035 RID: 4149
		private int switchSetting;

		// Token: 0x04001036 RID: 4150
		private volatile bool initialized;

		// Token: 0x04001037 RID: 4151
		private bool initializing;

		// Token: 0x04001038 RID: 4152
		private volatile string switchValueString = string.Empty;

		// Token: 0x04001039 RID: 4153
		private StringDictionary attributes;

		// Token: 0x0400103A RID: 4154
		private string defaultValue;

		// Token: 0x0400103B RID: 4155
		private object m_intializedLock;

		// Token: 0x0400103C RID: 4156
		private static List<WeakReference> switches = new List<WeakReference>();

		// Token: 0x0400103D RID: 4157
		private static int s_LastCollectionCount;
	}
}
