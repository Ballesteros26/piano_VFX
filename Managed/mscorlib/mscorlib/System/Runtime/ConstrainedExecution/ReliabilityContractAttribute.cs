using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Defines a contract for reliability between the author of some code, and the developers who have a dependency on that code.</summary>
	// Token: 0x02000830 RID: 2096
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Interface, Inherited = false)]
	public sealed class ReliabilityContractAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.ConstrainedExecution.ReliabilityContractAttribute" /> class with the specified <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> guarantee and <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> value.</summary>
		/// <param name="consistencyGuarantee">One of the <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> values. </param>
		/// <param name="cer">One of the <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> values. </param>
		// Token: 0x0600538A RID: 21386 RVA: 0x001259A0 File Offset: 0x00123BA0
		public ReliabilityContractAttribute(Consistency consistencyGuarantee, Cer cer)
		{
			this._consistency = consistencyGuarantee;
			this._cer = cer;
		}

		/// <summary>Gets the value of the <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> reliability contract. </summary>
		/// <returns>One of the <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> values.</returns>
		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x0600538B RID: 21387 RVA: 0x001259B6 File Offset: 0x00123BB6
		public Consistency ConsistencyGuarantee
		{
			get
			{
				return this._consistency;
			}
		}

		/// <summary>Gets the value that determines the behavior of a method, type, or assembly when called under a Constrained Execution Region (CER). </summary>
		/// <returns>One of the <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> values.</returns>
		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x0600538C RID: 21388 RVA: 0x001259BE File Offset: 0x00123BBE
		public Cer Cer
		{
			get
			{
				return this._cer;
			}
		}

		// Token: 0x04002B73 RID: 11123
		private Consistency _consistency;

		// Token: 0x04002B74 RID: 11124
		private Cer _cer;
	}
}
