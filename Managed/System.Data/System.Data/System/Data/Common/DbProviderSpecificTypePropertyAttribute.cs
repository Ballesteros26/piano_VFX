using System;

namespace System.Data.Common
{
	/// <summary>Identifies which provider-specific property in the strongly typed parameter classes is to be used when setting a provider-specific type.</summary>
	// Token: 0x02000351 RID: 849
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	[Serializable]
	public sealed class DbProviderSpecificTypePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DbProviderSpecificTypePropertyAttribute" /> class.</summary>
		/// <param name="isProviderSpecificTypeProperty">Specifies whether this property is a provider-specific property.</param>
		// Token: 0x0600284F RID: 10319 RVA: 0x000B13A8 File Offset: 0x000AF5A8
		public DbProviderSpecificTypePropertyAttribute(bool isProviderSpecificTypeProperty)
		{
			this.IsProviderSpecificTypeProperty = isProviderSpecificTypeProperty;
		}

		/// <summary>Indicates whether the attributed property is a provider-specific type.</summary>
		/// <returns>true if the property that this attribute is applied to is a provider-specific type property; otherwise false.</returns>
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002850 RID: 10320 RVA: 0x000B13B7 File Offset: 0x000AF5B7
		public bool IsProviderSpecificTypeProperty { get; }
	}
}
