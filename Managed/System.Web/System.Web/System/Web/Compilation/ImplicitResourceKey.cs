using System;

namespace System.Web.Compilation
{
	/// <summary>Contains fields that identify an implicit resource key.</summary>
	// Token: 0x0200060A RID: 1546
	public sealed class ImplicitResourceKey
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ImplicitResourceKey" /> class. </summary>
		// Token: 0x060042AC RID: 17068 RVA: 0x00002050 File Offset: 0x00000250
		public ImplicitResourceKey()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ImplicitResourceKey" /> class with the specified values for the <see cref="P:System.Web.Compilation.ImplicitResourceKey.Filter" />, <see cref="P:System.Web.Compilation.ImplicitResourceKey.KeyPrefix" /> and <see cref="P:System.Web.Compilation.ImplicitResourceKey.Property" /> properties.</summary>
		/// <param name="filter">The filter value of an implicit resource key.</param>
		/// <param name="keyPrefix">The prefix for identifying a group of properties.</param>
		/// <param name="property">A property and subproperty, if provided, for an implicit resource key.</param>
		// Token: 0x060042AD RID: 17069 RVA: 0x000AFB82 File Offset: 0x000ADD82
		public ImplicitResourceKey(string filter, string keyPrefix, string property)
		{
			this._filter = filter;
			this._keyPrefix = keyPrefix;
			this._property = property;
		}

		/// <summary>Gets or sets the filter value of an implicit resource key.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the filter value for the implicit resource expression.</returns>
		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x060042AE RID: 17070 RVA: 0x000AFB9F File Offset: 0x000ADD9F
		// (set) Token: 0x060042AF RID: 17071 RVA: 0x000AFBA7 File Offset: 0x000ADDA7
		public string Filter
		{
			get
			{
				return this._filter;
			}
			set
			{
				this._filter = value;
			}
		}

		/// <summary>Gets or sets the prefix for identifying a group of properties.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the prefix for an implicit resource expression.</returns>
		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x000AFBB0 File Offset: 0x000ADDB0
		// (set) Token: 0x060042B1 RID: 17073 RVA: 0x000AFBB8 File Offset: 0x000ADDB8
		public string KeyPrefix
		{
			get
			{
				return this._keyPrefix;
			}
			set
			{
				this._keyPrefix = value;
			}
		}

		/// <summary>Gets or sets a property and subproperty, if provided, for an implicit resource key.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the property and subproperty for an implicit resource expression.</returns>
		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x060042B2 RID: 17074 RVA: 0x000AFBC1 File Offset: 0x000ADDC1
		// (set) Token: 0x060042B3 RID: 17075 RVA: 0x000AFBC9 File Offset: 0x000ADDC9
		public string Property
		{
			get
			{
				return this._property;
			}
			set
			{
				this._property = value;
			}
		}

		// Token: 0x040023B8 RID: 9144
		private string _filter;

		// Token: 0x040023B9 RID: 9145
		private string _keyPrefix;

		// Token: 0x040023BA RID: 9146
		private string _property;
	}
}
