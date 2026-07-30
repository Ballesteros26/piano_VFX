using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines the mapping of specific security levels to named policy files. This class cannot be inherited.</summary>
	// Token: 0x020005E5 RID: 1509
	public sealed class TrustLevel : ConfigurationElement
	{
		// Token: 0x0600416E RID: 16750 RVA: 0x000AB7B0 File Offset: 0x000A99B0
		static TrustLevel()
		{
			TrustLevel.properties.Add(TrustLevel.nameProp);
			TrustLevel.properties.Add(TrustLevel.policyFileProp);
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x0009F629 File Offset: 0x0009D829
		internal TrustLevel()
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.Configuration.TrustLevel" /> class that is initialized based on the provided values, which define the mapping of specific security levels to named policy files.</summary>
		/// <param name="name">A named security level that is mapped to a policy file.</param>
		/// <param name="policyFile">The configuration file that contains security policy settings for the named security level.</param>
		// Token: 0x06004170 RID: 16752 RVA: 0x000AB837 File Offset: 0x000A9A37
		public TrustLevel(string name, string policyFile)
		{
			this.Name = name;
			this.PolicyFile = policyFile;
		}

		/// <summary>Gets or sets a named security level that is mapped to a policy file.</summary>
		/// <returns>The <see cref="P:System.Web.Configuration.TrustLevel.Name" /> that is mapped to a policy file.</returns>
		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x06004171 RID: 16753 RVA: 0x000AB84D File Offset: 0x000A9A4D
		// (set) Token: 0x06004172 RID: 16754 RVA: 0x000AB85F File Offset: 0x000A9A5F
		[ConfigurationProperty("name", DefaultValue = "Full", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[TrustLevel.nameProp];
			}
			set
			{
				base[TrustLevel.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the configuration file reference that contains the security policy settings for the named security level.</summary>
		/// <returns>The configuration file reference that contains the security policy settings for the associated security level.</returns>
		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x06004173 RID: 16755 RVA: 0x000AB86D File Offset: 0x000A9A6D
		// (set) Token: 0x06004174 RID: 16756 RVA: 0x000AB87F File Offset: 0x000A9A7F
		[ConfigurationProperty("policyFile", DefaultValue = "internal", Options = ConfigurationPropertyOptions.IsRequired)]
		public string PolicyFile
		{
			get
			{
				return (string)base[TrustLevel.policyFileProp];
			}
			set
			{
				base[TrustLevel.policyFileProp] = value;
			}
		}

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x06004175 RID: 16757 RVA: 0x000AB88D File Offset: 0x000A9A8D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TrustLevel.properties;
			}
		}

		// Token: 0x04002336 RID: 9014
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "Full", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002337 RID: 9015
		private static ConfigurationProperty policyFileProp = new ConfigurationProperty("policyFile", typeof(string), "internal", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002338 RID: 9016
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
