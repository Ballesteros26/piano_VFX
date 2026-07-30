using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>References a directory location that is used during compilation of a dynamic resource. This class cannot be inherited.</summary>
	// Token: 0x02000592 RID: 1426
	public sealed class CodeSubDirectory : ConfigurationElement
	{
		// Token: 0x06003C41 RID: 15425 RVA: 0x000A0AEC File Offset: 0x0009ECEC
		static CodeSubDirectory()
		{
			CodeSubDirectory.properties.Add(CodeSubDirectory.directoryNameProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CodeSubDirectory" /> class.</summary>
		/// <param name="directoryName">A string value specifying the <see cref="T:System.Web.Configuration.CodeSubDirectory" /> reference.</param>
		// Token: 0x06003C42 RID: 15426 RVA: 0x000A0B3B File Offset: 0x0009ED3B
		public CodeSubDirectory(string directoryName)
		{
			this.DirectoryName = directoryName;
		}

		/// <summary>Gets or sets the name of the directory that contains files compiled at run time.</summary>
		/// <returns>A string value specifying the name of the directory reference used during compilation.</returns>
		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x000A0B4A File Offset: 0x0009ED4A
		// (set) Token: 0x06003C44 RID: 15428 RVA: 0x000A0B5C File Offset: 0x0009ED5C
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[ConfigurationProperty("directoryName", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string DirectoryName
		{
			get
			{
				return (string)base[CodeSubDirectory.directoryNameProp];
			}
			set
			{
				base[CodeSubDirectory.directoryNameProp] = value;
			}
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x000A0B6A File Offset: 0x0009ED6A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CodeSubDirectory.properties;
			}
		}

		// Token: 0x040020B3 RID: 8371
		private static ConfigurationProperty directoryNameProp = new ConfigurationProperty("directoryName", typeof(string), "", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020B4 RID: 8372
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
