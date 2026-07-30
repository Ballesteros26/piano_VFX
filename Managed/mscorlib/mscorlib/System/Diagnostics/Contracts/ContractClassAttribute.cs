using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies that a separate type contains the code contracts for this type.</summary>
	// Token: 0x02000A7F RID: 2687
	[Conditional("DEBUG")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractClassAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractClassAttribute" /> class. </summary>
		/// <param name="typeContainingContracts">The type that contains the code contracts for this type.</param>
		// Token: 0x06006215 RID: 25109 RVA: 0x00140BCE File Offset: 0x0013EDCE
		public ContractClassAttribute(Type typeContainingContracts)
		{
			this._typeWithContracts = typeContainingContracts;
		}

		/// <summary>Gets the type that contains the code contracts for this type.</summary>
		/// <returns>The type that contains the code contracts for this type. </returns>
		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x06006216 RID: 25110 RVA: 0x00140BDD File Offset: 0x0013EDDD
		public Type TypeContainingContracts
		{
			get
			{
				return this._typeWithContracts;
			}
		}

		// Token: 0x040030EA RID: 12522
		private Type _typeWithContracts;
	}
}
