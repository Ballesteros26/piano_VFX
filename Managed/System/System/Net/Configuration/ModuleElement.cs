using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the type information for a custom <see cref="T:System.Net.IWebProxy" /> module. This class cannot be inherited.</summary>
	// Token: 0x020006A3 RID: 1699
	public sealed class ModuleElement : ConfigurationElement
	{
		// Token: 0x06003544 RID: 13636 RVA: 0x000C4B1D File Offset: 0x000C2D1D
		static ModuleElement()
		{
			ModuleElement.properties.Add(ModuleElement.typeProp);
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06003546 RID: 13638 RVA: 0x000C4B52 File Offset: 0x000C2D52
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ModuleElement.properties;
			}
		}

		/// <summary>Gets or sets the type and assembly information for the current instance.</summary>
		/// <returns>A string that identifies a type that implements the <see cref="T:System.Net.IWebProxy" /> interface or null if no value has been specified.</returns>
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x000C4B59 File Offset: 0x000C2D59
		// (set) Token: 0x06003548 RID: 13640 RVA: 0x000C4B6B File Offset: 0x000C2D6B
		[ConfigurationProperty("type")]
		public string Type
		{
			get
			{
				return (string)base[ModuleElement.typeProp];
			}
			set
			{
				base[ModuleElement.typeProp] = value;
			}
		}

		// Token: 0x04002A73 RID: 10867
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002A74 RID: 10868
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null);
	}
}
