using System;

namespace System.Web.Compilation
{
	/// <summary>Specifies the type of resource provider factory for design time. This class cannot be inherited.</summary>
	// Token: 0x02000603 RID: 1539
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DesignTimeResourceProviderFactoryAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.DesignTimeResourceProviderFactoryAttribute" /> class with the attribute set to the qualified name of the specified factory type. </summary>
		/// <param name="factoryType">The type of the resource provider factory.</param>
		// Token: 0x0600429C RID: 17052 RVA: 0x000AFA88 File Offset: 0x000ADC88
		public DesignTimeResourceProviderFactoryAttribute(Type factoryType)
		{
			this._factoryTypeName = factoryType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.DesignTimeResourceProviderFactoryAttribute" /> class with the attribute set to the specified factory type name. </summary>
		/// <param name="factoryTypeName">The name of the resource provider factory type.</param>
		// Token: 0x0600429D RID: 17053 RVA: 0x000AFA9C File Offset: 0x000ADC9C
		public DesignTimeResourceProviderFactoryAttribute(string factoryTypeName)
		{
			this._factoryTypeName = factoryTypeName;
		}

		/// <summary>Gets the value of the factory type name.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the factory type.</returns>
		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x0600429E RID: 17054 RVA: 0x000AFAAB File Offset: 0x000ADCAB
		public string FactoryTypeName
		{
			get
			{
				return this._factoryTypeName;
			}
		}

		/// <summary>Determines whether the default provider is used.</summary>
		/// <returns>true if <see cref="P:System.Web.Compilation.DesignTimeResourceProviderFactoryAttribute.FactoryTypeName" /> equals null; otherwise, false.</returns>
		// Token: 0x0600429F RID: 17055 RVA: 0x000AFAB3 File Offset: 0x000ADCB3
		public override bool IsDefaultAttribute()
		{
			return this._factoryTypeName == null;
		}

		// Token: 0x040023AE RID: 9134
		private string _factoryTypeName;
	}
}
