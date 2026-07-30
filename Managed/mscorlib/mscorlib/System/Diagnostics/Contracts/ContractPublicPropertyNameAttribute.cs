using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies that a field can be used in method contracts when the field has less visibility than the method. </summary>
	// Token: 0x02000A85 RID: 2693
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractPublicPropertyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractPublicPropertyNameAttribute" /> class. </summary>
		/// <param name="name">The property name to apply to the field.</param>
		// Token: 0x0600621E RID: 25118 RVA: 0x00140C13 File Offset: 0x0013EE13
		public ContractPublicPropertyNameAttribute(string name)
		{
			this._publicName = name;
		}

		/// <summary>Gets the property name to be applied to the field.</summary>
		/// <returns>The property name to be applied to the field.</returns>
		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x0600621F RID: 25119 RVA: 0x00140C22 File Offset: 0x0013EE22
		public string Name
		{
			get
			{
				return this._publicName;
			}
		}

		// Token: 0x040030ED RID: 12525
		private string _publicName;
	}
}
