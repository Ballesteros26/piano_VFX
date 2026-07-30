using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies that a class is a contract for a type.</summary>
	// Token: 0x02000A80 RID: 2688
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractClassForAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractClassForAttribute" /> class, specifying the type the current class is a contract for. </summary>
		/// <param name="typeContractsAreFor">The type the current class is a contract for.</param>
		// Token: 0x06006217 RID: 25111 RVA: 0x00140BE5 File Offset: 0x0013EDE5
		public ContractClassForAttribute(Type typeContractsAreFor)
		{
			this._typeIAmAContractFor = typeContractsAreFor;
		}

		/// <summary>Gets the type that this code contract applies to. </summary>
		/// <returns>The type that this contract applies to.</returns>
		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x06006218 RID: 25112 RVA: 0x00140BF4 File Offset: 0x0013EDF4
		public Type TypeContractsAreFor
		{
			get
			{
				return this._typeIAmAContractFor;
			}
		}

		// Token: 0x040030EB RID: 12523
		private Type _typeIAmAContractFor;
	}
}
