using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	/// <summary>Specifies the resource consumed by the member of a class. This class cannot be inherited.</summary>
	// Token: 0x020006C0 RID: 1728
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	public sealed class ResourceConsumptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ResourceConsumptionAttribute" /> class specifying the scope of the consumed resource. </summary>
		/// <param name="resourceScope">The <see cref="T:System.Runtime.Versioning.ResourceScope" /> for the consumed resource.</param>
		// Token: 0x06004980 RID: 18816 RVA: 0x00107C82 File Offset: 0x00105E82
		public ResourceConsumptionAttribute(ResourceScope resourceScope)
		{
			this._resourceScope = resourceScope;
			this._consumptionScope = this._resourceScope;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ResourceConsumptionAttribute" /> class specifying the scope of the consumed resource and the scope of how it is consumed.</summary>
		/// <param name="resourceScope">The <see cref="T:System.Runtime.Versioning.ResourceScope" /> for the consumed resource.</param>
		/// <param name="consumptionScope">The <see cref="T:System.Runtime.Versioning.ResourceScope" /> used by this member.</param>
		// Token: 0x06004981 RID: 18817 RVA: 0x00107C9D File Offset: 0x00105E9D
		public ResourceConsumptionAttribute(ResourceScope resourceScope, ResourceScope consumptionScope)
		{
			this._resourceScope = resourceScope;
			this._consumptionScope = consumptionScope;
		}

		/// <summary>Gets the resource scope for the consumed resource.</summary>
		/// <returns>A <see cref="T:System.Runtime.Versioning.ResourceScope" /> object specifying the resource scope of the consumed member.</returns>
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06004982 RID: 18818 RVA: 0x00107CB3 File Offset: 0x00105EB3
		public ResourceScope ResourceScope
		{
			get
			{
				return this._resourceScope;
			}
		}

		/// <summary>Gets the consumption scope for this member.</summary>
		/// <returns>A <see cref="T:System.Runtime.Versioning.ResourceScope" /> object specifying the resource scope used by this member.</returns>
		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06004983 RID: 18819 RVA: 0x00107CBB File Offset: 0x00105EBB
		public ResourceScope ConsumptionScope
		{
			get
			{
				return this._consumptionScope;
			}
		}

		// Token: 0x04002687 RID: 9863
		private ResourceScope _consumptionScope;

		// Token: 0x04002688 RID: 9864
		private ResourceScope _resourceScope;
	}
}
