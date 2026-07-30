using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the type information for an authentication module. This class cannot be inherited.</summary>
	// Token: 0x02000691 RID: 1681
	public sealed class AuthenticationModuleElement : ConfigurationElement
	{
		// Token: 0x060034BB RID: 13499 RVA: 0x000C3B08 File Offset: 0x000C1D08
		static AuthenticationModuleElement()
		{
			AuthenticationModuleElement.properties.Add(AuthenticationModuleElement.typeProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> class. </summary>
		// Token: 0x060034BC RID: 13500 RVA: 0x0003BCB4 File Offset: 0x00039EB4
		public AuthenticationModuleElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> class with the specified type information.</summary>
		/// <param name="typeName">A string that identifies the type and the assembly that contains it.</param>
		// Token: 0x060034BD RID: 13501 RVA: 0x000C3B3E File Offset: 0x000C1D3E
		public AuthenticationModuleElement(string typeName)
		{
			this.Type = typeName;
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x060034BE RID: 13502 RVA: 0x000C3B4D File Offset: 0x000C1D4D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthenticationModuleElement.properties;
			}
		}

		/// <summary>Gets or sets the type and assembly information for the current instance.</summary>
		/// <returns>A string that identifies a type that implements an authentication module or null if no value has been specified.</returns>
		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x060034BF RID: 13503 RVA: 0x000C3B54 File Offset: 0x000C1D54
		// (set) Token: 0x060034C0 RID: 13504 RVA: 0x000C3B66 File Offset: 0x000C1D66
		[ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Type
		{
			get
			{
				return (string)base[AuthenticationModuleElement.typeProp];
			}
			set
			{
				base[AuthenticationModuleElement.typeProp] = value;
			}
		}

		// Token: 0x04002A52 RID: 10834
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002A53 RID: 10835
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
