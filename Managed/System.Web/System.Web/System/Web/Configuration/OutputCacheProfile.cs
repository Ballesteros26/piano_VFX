using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.UI;

namespace System.Web.Configuration
{
	/// <summary>Configures the output cache profile that can be used by the application pages. This class cannot be inherited.</summary>
	// Token: 0x020005BF RID: 1471
	public sealed class OutputCacheProfile : ConfigurationElement
	{
		// Token: 0x06003F07 RID: 16135 RVA: 0x000A6DF4 File Offset: 0x000A4FF4
		static OutputCacheProfile()
		{
			OutputCacheProfile.properties.Add(OutputCacheProfile.durationProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.enabledProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.locationProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.nameProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.noStoreProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.sqlDependencyProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.varyByContentEncodingProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.varyByControlProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.varyByCustomProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.varyByHeaderProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.varyByParamProp);
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x0009F629 File Offset: 0x0009D829
		internal OutputCacheProfile()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> class.</summary>
		/// <param name="name">The name value to use.</param>
		// Token: 0x06003F09 RID: 16137 RVA: 0x000A6FFB File Offset: 0x000A51FB
		public OutputCacheProfile(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets or sets the time duration during which the page or control is cached.</summary>
		/// <returns>The time duration in seconds.</returns>
		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06003F0A RID: 16138 RVA: 0x000A700A File Offset: 0x000A520A
		// (set) Token: 0x06003F0B RID: 16139 RVA: 0x000A701C File Offset: 0x000A521C
		[ConfigurationProperty("duration", DefaultValue = "-1")]
		public int Duration
		{
			get
			{
				return (int)base[OutputCacheProfile.durationProp];
			}
			set
			{
				base[OutputCacheProfile.durationProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether caching is enabled.</summary>
		/// <returns>true if caching is enabled; otherwise, false. The default value is false. </returns>
		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x06003F0C RID: 16140 RVA: 0x000A702F File Offset: 0x000A522F
		// (set) Token: 0x06003F0D RID: 16141 RVA: 0x000A7041 File Offset: 0x000A5241
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool Enabled
		{
			get
			{
				return (bool)base[OutputCacheProfile.enabledProp];
			}
			set
			{
				base[OutputCacheProfile.enabledProp] = value;
			}
		}

		/// <summary>Gets or sets the output cache location.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.OutputCacheLocation" /> enumeration values. The default is Any.</returns>
		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06003F0E RID: 16142 RVA: 0x000A7054 File Offset: 0x000A5254
		// (set) Token: 0x06003F0F RID: 16143 RVA: 0x000A7066 File Offset: 0x000A5266
		[ConfigurationProperty("location")]
		public OutputCacheLocation Location
		{
			get
			{
				return (OutputCacheLocation)base[OutputCacheProfile.locationProp];
			}
			set
			{
				base[OutputCacheProfile.locationProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> name.</returns>
		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06003F10 RID: 16144 RVA: 0x000A7079 File Offset: 0x000A5279
		// (set) Token: 0x06003F11 RID: 16145 RVA: 0x000A708B File Offset: 0x000A528B
		[StringValidator(MinLength = 1)]
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[OutputCacheProfile.nameProp];
			}
			set
			{
				base[OutputCacheProfile.nameProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether secondary storage is enabled. </summary>
		/// <returns>true if secondary storage is enabled; otherwise, false. The default value is false. </returns>
		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06003F12 RID: 16146 RVA: 0x000A7099 File Offset: 0x000A5299
		// (set) Token: 0x06003F13 RID: 16147 RVA: 0x000A70AB File Offset: 0x000A52AB
		[ConfigurationProperty("noStore", DefaultValue = "False")]
		public bool NoStore
		{
			get
			{
				return (bool)base[OutputCacheProfile.noStoreProp];
			}
			set
			{
				base[OutputCacheProfile.noStoreProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.SqlDependency" /> property. </summary>
		/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.SqlDependency" /> value.</returns>
		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06003F14 RID: 16148 RVA: 0x000A70BE File Offset: 0x000A52BE
		// (set) Token: 0x06003F15 RID: 16149 RVA: 0x000A70D0 File Offset: 0x000A52D0
		[ConfigurationProperty("sqlDependency")]
		public string SqlDependency
		{
			get
			{
				return (string)base[OutputCacheProfile.sqlDependencyProp];
			}
			set
			{
				base[OutputCacheProfile.sqlDependencyProp] = value;
			}
		}

		/// <summary>Gets or sets the semicolon-delimited set of content encodings to be cached.</summary>
		/// <returns>The list of content encodings.</returns>
		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x06003F16 RID: 16150 RVA: 0x000A70DE File Offset: 0x000A52DE
		// (set) Token: 0x06003F17 RID: 16151 RVA: 0x000A70F0 File Offset: 0x000A52F0
		[ConfigurationProperty("varyByContentEncoding")]
		public string VaryByContentEncoding
		{
			get
			{
				return (string)base[OutputCacheProfile.varyByContentEncodingProp];
			}
			set
			{
				base[OutputCacheProfile.varyByContentEncodingProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByControl" /> property.</summary>
		/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByControl" /> value.</returns>
		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x000A70FE File Offset: 0x000A52FE
		// (set) Token: 0x06003F19 RID: 16153 RVA: 0x000A7110 File Offset: 0x000A5310
		[ConfigurationProperty("varyByControl")]
		public string VaryByControl
		{
			get
			{
				return (string)base[OutputCacheProfile.varyByControlProp];
			}
			set
			{
				base[OutputCacheProfile.varyByControlProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByCustom" /> property.</summary>
		/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByCustom" /> value.</returns>
		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06003F1A RID: 16154 RVA: 0x000A711E File Offset: 0x000A531E
		// (set) Token: 0x06003F1B RID: 16155 RVA: 0x000A7130 File Offset: 0x000A5330
		[ConfigurationProperty("varyByCustom")]
		public string VaryByCustom
		{
			get
			{
				return (string)base[OutputCacheProfile.varyByCustomProp];
			}
			set
			{
				base[OutputCacheProfile.varyByCustomProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByHeader" /> property.</summary>
		/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByHeader" /> value.</returns>
		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x06003F1C RID: 16156 RVA: 0x000A713E File Offset: 0x000A533E
		// (set) Token: 0x06003F1D RID: 16157 RVA: 0x000A7150 File Offset: 0x000A5350
		[ConfigurationProperty("varyByHeader")]
		public string VaryByHeader
		{
			get
			{
				return (string)base[OutputCacheProfile.varyByHeaderProp];
			}
			set
			{
				base[OutputCacheProfile.varyByHeaderProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByParam" /> property.</summary>
		/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByParam" /> value.</returns>
		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x000A715E File Offset: 0x000A535E
		// (set) Token: 0x06003F1F RID: 16159 RVA: 0x000A7170 File Offset: 0x000A5370
		[ConfigurationProperty("varyByParam")]
		public string VaryByParam
		{
			get
			{
				return (string)base[OutputCacheProfile.varyByParamProp];
			}
			set
			{
				base[OutputCacheProfile.varyByParamProp] = value;
			}
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06003F20 RID: 16160 RVA: 0x000A717E File Offset: 0x000A537E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return OutputCacheProfile.properties;
			}
		}

		// Token: 0x04002262 RID: 8802
		private static ConfigurationProperty durationProp = new ConfigurationProperty("duration", typeof(int), -1);

		// Token: 0x04002263 RID: 8803
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x04002264 RID: 8804
		private static ConfigurationProperty locationProp = new ConfigurationProperty("location", typeof(OutputCacheLocation), null, new GenericEnumConverter(typeof(OutputCacheLocation)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002265 RID: 8805
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002266 RID: 8806
		private static ConfigurationProperty noStoreProp = new ConfigurationProperty("noStore", typeof(bool), false);

		// Token: 0x04002267 RID: 8807
		private static ConfigurationProperty sqlDependencyProp = new ConfigurationProperty("sqlDependency", typeof(string));

		// Token: 0x04002268 RID: 8808
		private static ConfigurationProperty varyByContentEncodingProp = new ConfigurationProperty("varyByContentEncoding", typeof(string));

		// Token: 0x04002269 RID: 8809
		private static ConfigurationProperty varyByControlProp = new ConfigurationProperty("varyByControl", typeof(string));

		// Token: 0x0400226A RID: 8810
		private static ConfigurationProperty varyByCustomProp = new ConfigurationProperty("varyByCustom", typeof(string));

		// Token: 0x0400226B RID: 8811
		private static ConfigurationProperty varyByHeaderProp = new ConfigurationProperty("varyByHeader", typeof(string));

		// Token: 0x0400226C RID: 8812
		private static ConfigurationProperty varyByParamProp = new ConfigurationProperty("varyByParam", typeof(string));

		// Token: 0x0400226D RID: 8813
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
