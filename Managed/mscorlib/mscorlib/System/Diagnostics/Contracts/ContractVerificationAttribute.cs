using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Instructs analysis tools to assume the correctness of an assembly, type, or member without performing static verification.</summary>
	// Token: 0x02000A84 RID: 2692
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class ContractVerificationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractVerificationAttribute" /> class. </summary>
		/// <param name="value">true to require verification; otherwise, false. </param>
		// Token: 0x0600621C RID: 25116 RVA: 0x00140BFC File Offset: 0x0013EDFC
		public ContractVerificationAttribute(bool value)
		{
			this._value = value;
		}

		/// <summary>Gets the value that indicates whether to verify the contract of the target. </summary>
		/// <returns>true if verification is required; otherwise, false.</returns>
		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x0600621D RID: 25117 RVA: 0x00140C0B File Offset: 0x0013EE0B
		public bool Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040030EC RID: 12524
		private bool _value;
	}
}
