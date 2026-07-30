using System;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x020007BB RID: 1979
	internal sealed class CompilerProviderOption : ConfigurationElement
	{
		// Token: 0x06003FE9 RID: 16361 RVA: 0x000E0844 File Offset: 0x000DEA44
		static CompilerProviderOption()
		{
			CompilerProviderOption.properties.Add(CompilerProviderOption.nameProp);
			CompilerProviderOption.properties.Add(CompilerProviderOption.valueProp);
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06003FEA RID: 16362 RVA: 0x000E08B7 File Offset: 0x000DEAB7
		// (set) Token: 0x06003FEB RID: 16363 RVA: 0x000E08C9 File Offset: 0x000DEAC9
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[CompilerProviderOption.nameProp];
			}
			set
			{
				base[CompilerProviderOption.nameProp] = value;
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06003FEC RID: 16364 RVA: 0x000E08D7 File Offset: 0x000DEAD7
		// (set) Token: 0x06003FED RID: 16365 RVA: 0x000E08E9 File Offset: 0x000DEAE9
		[ConfigurationProperty("value", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Value
		{
			get
			{
				return (string)base[CompilerProviderOption.valueProp];
			}
			set
			{
				base[CompilerProviderOption.valueProp] = value;
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06003FEE RID: 16366 RVA: 0x000E08F7 File Offset: 0x000DEAF7
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerProviderOption.properties;
			}
		}

		// Token: 0x04002E8D RID: 11917
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002E8E RID: 11918
		private static ConfigurationProperty valueProp = new ConfigurationProperty("value", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002E8F RID: 11919
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
