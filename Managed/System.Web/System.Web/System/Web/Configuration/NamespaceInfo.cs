using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a single configuration namespace reference, similar to the Import directive. This class cannot be inherited.</summary>
	// Token: 0x020005BE RID: 1470
	public sealed class NamespaceInfo : ConfigurationElement
	{
		// Token: 0x06003F00 RID: 16128 RVA: 0x000A6D30 File Offset: 0x000A4F30
		static NamespaceInfo()
		{
			NamespaceInfo.properties.Add(NamespaceInfo.namespaceProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.NamespaceInfo" /> class with the specified namespace reference.</summary>
		/// <param name="name">A namespace reference for the new <see cref="T:System.Web.Configuration.NamespaceInfo" /> object.</param>
		// Token: 0x06003F01 RID: 16129 RVA: 0x000A6D85 File Offset: 0x000A4F85
		public NamespaceInfo(string name)
		{
			this.Namespace = name;
		}

		/// <summary>Compares the current instance to the passed <see cref="T:System.Web.Configuration.NamespaceInfo" /> object.</summary>
		/// <returns>true if the two objects are identical. </returns>
		/// <param name="namespaceInformation">A <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to compare to.</param>
		// Token: 0x06003F02 RID: 16130 RVA: 0x000A6D94 File Offset: 0x000A4F94
		public override bool Equals(object namespaceInformation)
		{
			NamespaceInfo namespaceInfo = namespaceInformation as NamespaceInfo;
			return namespaceInfo != null && this.Namespace == namespaceInfo.Namespace;
		}

		/// <summary>Returns a hash value for the current instance.</summary>
		/// <returns>A hash value for the current instance.</returns>
		// Token: 0x06003F03 RID: 16131 RVA: 0x000A6DBE File Offset: 0x000A4FBE
		public override int GetHashCode()
		{
			return this.Namespace.GetHashCode();
		}

		/// <summary>Gets or sets the namespace reference.</summary>
		/// <returns>A string that specifies the name of the namespace.</returns>
		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06003F04 RID: 16132 RVA: 0x000A6DCB File Offset: 0x000A4FCB
		// (set) Token: 0x06003F05 RID: 16133 RVA: 0x000A6DDD File Offset: 0x000A4FDD
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("namespace", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Namespace
		{
			get
			{
				return (string)base[NamespaceInfo.namespaceProp];
			}
			set
			{
				base[NamespaceInfo.namespaceProp] = value;
			}
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x06003F06 RID: 16134 RVA: 0x000A6DEB File Offset: 0x000A4FEB
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return NamespaceInfo.properties;
			}
		}

		// Token: 0x04002260 RID: 8800
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002261 RID: 8801
		private static ConfigurationProperty namespaceProp = new ConfigurationProperty("namespace", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
	}
}
