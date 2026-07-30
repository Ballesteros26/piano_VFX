using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a URI prefix and the associated class that handles creating Web requests for the prefix. This class cannot be inherited.</summary>
	// Token: 0x020006B4 RID: 1716
	public sealed class WebRequestModuleElement : ConfigurationElement
	{
		// Token: 0x060035C6 RID: 13766 RVA: 0x000C5A78 File Offset: 0x000C3C78
		static WebRequestModuleElement()
		{
			WebRequestModuleElement.properties.Add(WebRequestModuleElement.prefixProp);
			WebRequestModuleElement.properties.Add(WebRequestModuleElement.typeProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class. </summary>
		// Token: 0x060035C7 RID: 13767 RVA: 0x0003BCB4 File Offset: 0x00039EB4
		public WebRequestModuleElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class using the specified URI prefix and type information. </summary>
		/// <param name="prefix">A string containing a URI prefix.</param>
		/// <param name="type">A string containing the type and assembly information for the class that handles creating requests for resources that use the <paramref name="prefix" /> URI prefix. For more information, see the Remarks section.</param>
		// Token: 0x060035C8 RID: 13768 RVA: 0x000C5AE1 File Offset: 0x000C3CE1
		public WebRequestModuleElement(string prefix, string type)
		{
			base[WebRequestModuleElement.typeProp] = type;
			this.Prefix = prefix;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class using the specified URI prefix and type identifier.</summary>
		/// <param name="prefix">A string containing a URI prefix.</param>
		/// <param name="type">A <see cref="T:System.Type" /> that identifies the class that handles creating requests for resources that use the <paramref name="prefix" /> URI prefix. </param>
		// Token: 0x060035C9 RID: 13769 RVA: 0x000C5AFC File Offset: 0x000C3CFC
		public WebRequestModuleElement(string prefix, Type type)
			: this(prefix, type.FullName)
		{
		}

		/// <summary>Gets or sets the URI prefix for the current Web request module.</summary>
		/// <returns>A string that contains a URI prefix.</returns>
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x060035CA RID: 13770 RVA: 0x000C5B0B File Offset: 0x000C3D0B
		// (set) Token: 0x060035CB RID: 13771 RVA: 0x000C5B1D File Offset: 0x000C3D1D
		[ConfigurationProperty("prefix", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Prefix
		{
			get
			{
				return (string)base[WebRequestModuleElement.prefixProp];
			}
			set
			{
				base[WebRequestModuleElement.prefixProp] = value;
			}
		}

		/// <summary>Gets or sets a class that creates Web requests.</summary>
		/// <returns>A <see cref="T:System.Type" /> instance that identifies a Web request module.</returns>
		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x060035CC RID: 13772 RVA: 0x000C5B2B File Offset: 0x000C3D2B
		// (set) Token: 0x060035CD RID: 13773 RVA: 0x000C5B42 File Offset: 0x000C3D42
		[ConfigurationProperty("type")]
		[TypeConverter(typeof(TypeConverter))]
		public Type Type
		{
			get
			{
				return Type.GetType((string)base[WebRequestModuleElement.typeProp]);
			}
			set
			{
				base[WebRequestModuleElement.typeProp] = value.FullName;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060035CE RID: 13774 RVA: 0x000C5B55 File Offset: 0x000C3D55
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebRequestModuleElement.properties;
			}
		}

		// Token: 0x04002AA5 RID: 10917
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002AA6 RID: 10918
		private static ConfigurationProperty prefixProp = new ConfigurationProperty("prefix", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002AA7 RID: 10919
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string));
	}
}
