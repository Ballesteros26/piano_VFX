using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Provides functionality to parse a particular file type and generate code during compilation of a dynamic resource. This class cannot be inherited.</summary>
	// Token: 0x02000589 RID: 1417
	public sealed class BuildProvider : ConfigurationElement
	{
		// Token: 0x06003BE7 RID: 15335 RVA: 0x000A02D0 File Offset: 0x0009E4D0
		static BuildProvider()
		{
			BuildProvider.properties.Add(BuildProvider.extensionProp);
			BuildProvider.properties.Add(BuildProvider.typeProp);
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x0009F629 File Offset: 0x0009D829
		internal BuildProvider()
		{
		}

		/// <summary>Creates an instance of a <see cref="T:System.Web.Configuration.BuildProvider" /> class, initialized to the provided values.</summary>
		/// <param name="extension">The file extension of the dynamic resource used during compilation.</param>
		/// <param name="type">The type that represents the <see cref="T:System.Web.Configuration.BuildProvider" /> instance to use when parsing and compiling the given extension.</param>
		// Token: 0x06003BE9 RID: 15337 RVA: 0x000A036B File Offset: 0x0009E56B
		public BuildProvider(string extension, string type)
		{
			this.Extension = extension;
			this.Type = type;
		}

		/// <summary>Gets or sets the file extension to map to during compilation of a dynamic resource.</summary>
		/// <returns>A string specifying the file extension to map to during compilation of a dynamic resource.</returns>
		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06003BEA RID: 15338 RVA: 0x000A0381 File Offset: 0x0009E581
		// (set) Token: 0x06003BEB RID: 15339 RVA: 0x000A0394 File Offset: 0x0009E594
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("extension", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Extension
		{
			get
			{
				return (string)base[BuildProvider.extensionProp];
			}
			set
			{
				string text;
				if (!string.IsNullOrEmpty(value))
				{
					text = value.ToLowerInvariant();
				}
				else
				{
					text = value;
				}
				base[BuildProvider.extensionProp] = text;
			}
		}

		/// <summary>Gets or set the comma-separated class and assembly combination that indicates the <see cref="T:System.Web.Configuration.BuildProvider" /> instance to use.</summary>
		/// <returns>A comma-separated class and assembly combination that indicates the <see cref="T:System.Web.Configuration.BuildProvider" /> instance to use.</returns>
		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06003BEC RID: 15340 RVA: 0x000A03C0 File Offset: 0x0009E5C0
		// (set) Token: 0x06003BED RID: 15341 RVA: 0x000A03D2 File Offset: 0x0009E5D2
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[BuildProvider.typeProp];
			}
			set
			{
				base[BuildProvider.typeProp] = value;
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06003BEE RID: 15342 RVA: 0x000A03E0 File Offset: 0x0009E5E0
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BuildProvider.properties;
			}
		}

		/// <summary>Determines whether the specified build provider object is equal to the current object.</summary>
		/// <returns>true if the objects are equal; otherwise, false.</returns>
		/// <param name="provider">The build provider object to compare with the current object.</param>
		// Token: 0x06003BEF RID: 15343 RVA: 0x000A03E8 File Offset: 0x0009E5E8
		public override bool Equals(object provider)
		{
			BuildProvider buildProvider = provider as BuildProvider;
			return buildProvider != null && this.Extension == buildProvider.Extension && this.Type == buildProvider.Type;
		}

		/// <summary>Generates the hash code for the build provider.</summary>
		/// <returns>An integer representing the hash code for the build provider.</returns>
		// Token: 0x06003BF0 RID: 15344 RVA: 0x000A0427 File Offset: 0x0009E627
		public override int GetHashCode()
		{
			return this.Extension.GetHashCode() + this.Type.GetHashCode();
		}

		// Token: 0x040020A2 RID: 8354
		private static ConfigurationProperty extensionProp = new ConfigurationProperty("extension", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020A3 RID: 8355
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020A4 RID: 8356
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
