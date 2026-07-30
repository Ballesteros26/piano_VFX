using System;

namespace System.Xml.Serialization
{
	/// <summary>Applied to a Web service client proxy, enables you to specify an assembly that contains custom-made serializers. </summary>
	// Token: 0x02000364 RID: 868
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class XmlSerializerAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerAssemblyAttribute" /> class. </summary>
		// Token: 0x06002388 RID: 9096 RVA: 0x000DBC60 File Offset: 0x000D9E60
		public XmlSerializerAssemblyAttribute()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerAssemblyAttribute" /> class with the specified assembly name.</summary>
		/// <param name="assemblyName">The simple, unencrypted name of the assembly. </param>
		// Token: 0x06002389 RID: 9097 RVA: 0x000DBC6A File Offset: 0x000D9E6A
		public XmlSerializerAssemblyAttribute(string assemblyName)
			: this(assemblyName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerAssemblyAttribute" /> class with the specified assembly name and location of the assembly.</summary>
		/// <param name="assemblyName">The simple, unencrypted name of the assembly. </param>
		/// <param name="codeBase">A string that is the URL location of the assembly.</param>
		// Token: 0x0600238A RID: 9098 RVA: 0x000DBC74 File Offset: 0x000D9E74
		public XmlSerializerAssemblyAttribute(string assemblyName, string codeBase)
		{
			this.assemblyName = assemblyName;
			this.codeBase = codeBase;
		}

		/// <summary>Gets or sets the location of the assembly that contains the serializers.</summary>
		/// <returns>A location, such as a path or URI, that points to the assembly.</returns>
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x000DBC8A File Offset: 0x000D9E8A
		// (set) Token: 0x0600238C RID: 9100 RVA: 0x000DBC92 File Offset: 0x000D9E92
		public string CodeBase
		{
			get
			{
				return this.codeBase;
			}
			set
			{
				this.codeBase = value;
			}
		}

		/// <summary>Gets or sets the name of the assembly that contains serializers for a specific set of types.</summary>
		/// <returns>The simple, unencrypted name of the assembly. </returns>
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x000DBC9B File Offset: 0x000D9E9B
		// (set) Token: 0x0600238E RID: 9102 RVA: 0x000DBCA3 File Offset: 0x000D9EA3
		public string AssemblyName
		{
			get
			{
				return this.assemblyName;
			}
			set
			{
				this.assemblyName = value;
			}
		}

		// Token: 0x04001865 RID: 6245
		private string assemblyName;

		// Token: 0x04001866 RID: 6246
		private string codeBase;
	}
}
