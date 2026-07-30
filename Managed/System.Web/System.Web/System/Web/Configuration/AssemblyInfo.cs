using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>References an assembly to be linked to during compilation of a dynamic resource. This class cannot be inherited.</summary>
	// Token: 0x02000582 RID: 1410
	public sealed class AssemblyInfo : ConfigurationElement
	{
		// Token: 0x06003B8C RID: 15244 RVA: 0x0009F5D4 File Offset: 0x0009D7D4
		static AssemblyInfo()
		{
			AssemblyInfo.properties.Add(AssemblyInfo.assemblyProp);
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x0009F629 File Offset: 0x0009D829
		internal AssemblyInfo()
		{
		}

		/// <summary>Creates an instance of an <see cref="T:System.Web.Configuration.AssemblyInfo" /> class.</summary>
		/// <param name="assemblyName">Specifies a comma-separated assembly name combination consisting of version, culture, and public-key tokens.</param>
		// Token: 0x06003B8E RID: 15246 RVA: 0x0009F631 File Offset: 0x0009D831
		public AssemblyInfo(string assemblyName)
		{
			this.Assembly = assemblyName;
		}

		/// <summary>Gets or sets an assembly reference to use during compilation of a dynamic resource.</summary>
		/// <returns>A comma-separated string value specifying the version, culture, and public-key tokens of an assembly.</returns>
		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06003B8F RID: 15247 RVA: 0x0009F640 File Offset: 0x0009D840
		// (set) Token: 0x06003B90 RID: 15248 RVA: 0x0009F652 File Offset: 0x0009D852
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("assembly", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Assembly
		{
			get
			{
				return (string)base[AssemblyInfo.assemblyProp];
			}
			set
			{
				base[AssemblyInfo.assemblyProp] = value;
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06003B91 RID: 15249 RVA: 0x0009F660 File Offset: 0x0009D860
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AssemblyInfo.properties;
			}
		}

		// Token: 0x04002089 RID: 8329
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400208A RID: 8330
		private static ConfigurationProperty assemblyProp = new ConfigurationProperty("assembly", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
