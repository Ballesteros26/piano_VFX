using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines a configuration element containing tag-related information.</summary>
	// Token: 0x020005E1 RID: 1505
	public sealed class TagPrefixInfo : ConfigurationElement
	{
		// Token: 0x06004137 RID: 16695 RVA: 0x000AB100 File Offset: 0x000A9300
		static TagPrefixInfo()
		{
			TagPrefixInfo.properties.Add(TagPrefixInfo.tagPrefixProp);
			TagPrefixInfo.properties.Add(TagPrefixInfo.namespaceProp);
			TagPrefixInfo.properties.Add(TagPrefixInfo.assemblyProp);
			TagPrefixInfo.properties.Add(TagPrefixInfo.tagNameProp);
			TagPrefixInfo.properties.Add(TagPrefixInfo.sourceProp);
			TagPrefixInfo.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(TagPrefixInfo), new ValidatorCallback(TagPrefixInfo.ValidateElement)));
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x0009F629 File Offset: 0x0009D829
		internal TagPrefixInfo()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TagPrefixInfo" /> class using the passed values.</summary>
		/// <param name="tagPrefix">The tag prefix being mapped to a source file or namespace and assembly. </param>
		/// <param name="nameSpace">The namespace associated with the tag prefix.</param>
		/// <param name="assembly">The assembly where the namespace resides.</param>
		/// <param name="tagName">The name of the control to be used in the page.</param>
		/// <param name="source">The name of the file that contains the user control.</param>
		// Token: 0x06004139 RID: 16697 RVA: 0x000AB21E File Offset: 0x000A941E
		public TagPrefixInfo(string tagPrefix, string nameSpace, string assembly, string tagName, string source)
		{
			this.TagPrefix = tagPrefix;
			this.Namespace = nameSpace;
			this.Assembly = assembly;
			this.TagName = tagName;
			this.Source = source;
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x0000393A File Offset: 0x00001B3A
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x0600413B RID: 16699 RVA: 0x000AB24B File Offset: 0x000A944B
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return TagPrefixInfo.elementProperty;
			}
		}

		/// <summary>Compares this instance to another object.</summary>
		/// <returns>true if the objects are identical; otherwise, false.</returns>
		/// <param name="prefix">Object to compare.</param>
		// Token: 0x0600413C RID: 16700 RVA: 0x000AB254 File Offset: 0x000A9454
		public override bool Equals(object prefix)
		{
			TagPrefixInfo tagPrefixInfo = prefix as TagPrefixInfo;
			return tagPrefixInfo != null && (this.Namespace == tagPrefixInfo.Namespace && this.Source == tagPrefixInfo.Source && this.TagName == tagPrefixInfo.TagName) && this.TagPrefix == tagPrefixInfo.TagPrefix;
		}

		/// <summary>Returns a hash value for the current instance.</summary>
		/// <returns>A hash value for the current instance.</returns>
		// Token: 0x0600413D RID: 16701 RVA: 0x000AB2B9 File Offset: 0x000A94B9
		public override int GetHashCode()
		{
			return this.Namespace.GetHashCode() + this.Source.GetHashCode() + this.TagName.GetHashCode() + this.TagPrefix.GetHashCode();
		}

		/// <summary>Gets or sets the name of the assembly containing the control implementation.</summary>
		/// <returns>The name of the assembly (without an extension). The default is an empty string.</returns>
		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x0600413E RID: 16702 RVA: 0x000AB2EA File Offset: 0x000A94EA
		// (set) Token: 0x0600413F RID: 16703 RVA: 0x000AB2FC File Offset: 0x000A94FC
		[ConfigurationProperty("assembly")]
		public string Assembly
		{
			get
			{
				return (string)base[TagPrefixInfo.assemblyProp];
			}
			set
			{
				base[TagPrefixInfo.assemblyProp] = value;
			}
		}

		/// <summary>Gets or sets the namespace in which the control resides.</summary>
		/// <returns>The name of the namespace. The default is an empty string.</returns>
		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x06004140 RID: 16704 RVA: 0x000AB30A File Offset: 0x000A950A
		// (set) Token: 0x06004141 RID: 16705 RVA: 0x000AB31C File Offset: 0x000A951C
		[ConfigurationProperty("namespace")]
		public string Namespace
		{
			get
			{
				return (string)base[TagPrefixInfo.namespaceProp];
			}
			set
			{
				base[TagPrefixInfo.namespaceProp] = value;
			}
		}

		/// <summary>Gets or sets the name and path of the file containing the user control.</summary>
		/// <returns>The name and path of the file containing the user control.</returns>
		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x06004142 RID: 16706 RVA: 0x000AB32A File Offset: 0x000A952A
		// (set) Token: 0x06004143 RID: 16707 RVA: 0x000AB33C File Offset: 0x000A953C
		[ConfigurationProperty("src")]
		public string Source
		{
			get
			{
				return (string)base[TagPrefixInfo.sourceProp];
			}
			set
			{
				base[TagPrefixInfo.sourceProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the user control.</summary>
		/// <returns>The name of the user control.</returns>
		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06004144 RID: 16708 RVA: 0x000AB34A File Offset: 0x000A954A
		// (set) Token: 0x06004145 RID: 16709 RVA: 0x000AB35C File Offset: 0x000A955C
		[ConfigurationProperty("tagName")]
		public string TagName
		{
			get
			{
				return (string)base[TagPrefixInfo.tagNameProp];
			}
			set
			{
				base[TagPrefixInfo.tagNameProp] = value;
			}
		}

		/// <summary>Gets or sets the tag prefix that is being associated with a source file or namespace and assembly. </summary>
		/// <returns>The tag prefix. </returns>
		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x000AB36A File Offset: 0x000A956A
		// (set) Token: 0x06004147 RID: 16711 RVA: 0x000AB37C File Offset: 0x000A957C
		[ConfigurationProperty("tagPrefix", DefaultValue = "/", Options = ConfigurationPropertyOptions.IsRequired)]
		[StringValidator(MinLength = 1)]
		public string TagPrefix
		{
			get
			{
				return (string)base[TagPrefixInfo.tagPrefixProp];
			}
			set
			{
				base[TagPrefixInfo.tagPrefixProp] = value;
			}
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06004148 RID: 16712 RVA: 0x000AB38A File Offset: 0x000A958A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TagPrefixInfo.properties;
			}
		}

		// Token: 0x04002323 RID: 8995
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002324 RID: 8996
		private static ConfigurationProperty tagPrefixProp = new ConfigurationProperty("tagPrefix", typeof(string), "/", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002325 RID: 8997
		private static ConfigurationProperty namespaceProp = new ConfigurationProperty("namespace", typeof(string));

		// Token: 0x04002326 RID: 8998
		private static ConfigurationProperty assemblyProp = new ConfigurationProperty("assembly", typeof(string));

		// Token: 0x04002327 RID: 8999
		private static ConfigurationProperty tagNameProp = new ConfigurationProperty("tagName", typeof(string));

		// Token: 0x04002328 RID: 9000
		private static ConfigurationProperty sourceProp = new ConfigurationProperty("src", typeof(string));

		// Token: 0x04002329 RID: 9001
		private static ConfigurationElementProperty elementProperty;
	}
}
