using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides support for type equivalence.</summary>
	// Token: 0x020008A4 RID: 2212
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class TypeIdentifierAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.InteropServices.TypeIdentifierAttribute" /> class. </summary>
		// Token: 0x060054D6 RID: 21718 RVA: 0x00002180 File Offset: 0x00000380
		public TypeIdentifierAttribute()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.InteropServices.TypeIdentifierAttribute" /> class with the specified scope and identifier. </summary>
		/// <param name="scope">The first type equivalence string.</param>
		/// <param name="identifier">The second type equivalence string.</param>
		// Token: 0x060054D7 RID: 21719 RVA: 0x0012832D File Offset: 0x0012652D
		public TypeIdentifierAttribute(string scope, string identifier)
		{
			this.Scope_ = scope;
			this.Identifier_ = identifier;
		}

		/// <summary>Gets the value of the <paramref name="scope" /> parameter that was passed to the <see cref="M:System.Runtime.InteropServices.TypeIdentifierAttribute.#ctor(System.String,System.String)" /> constructor.</summary>
		/// <returns>The value of the constructor's <paramref name="scope" /> parameter.</returns>
		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x060054D8 RID: 21720 RVA: 0x00128343 File Offset: 0x00126543
		public string Scope
		{
			get
			{
				return this.Scope_;
			}
		}

		/// <summary>Gets the value of the <paramref name="identifier" /> parameter that was passed to the <see cref="M:System.Runtime.InteropServices.TypeIdentifierAttribute.#ctor(System.String,System.String)" /> constructor.</summary>
		/// <returns>The value of the constructor's <paramref name="identifier" /> parameter.</returns>
		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x060054D9 RID: 21721 RVA: 0x0012834B File Offset: 0x0012654B
		public string Identifier
		{
			get
			{
				return this.Identifier_;
			}
		}

		// Token: 0x04002BF3 RID: 11251
		internal string Scope_;

		// Token: 0x04002BF4 RID: 11252
		internal string Identifier_;
	}
}
