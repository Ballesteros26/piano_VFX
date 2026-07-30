using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a variable within a symbol store.</summary>
	// Token: 0x02000A77 RID: 2679
	[ComVisible(true)]
	public interface ISymbolVariable
	{
		/// <summary>Gets the first address of a variable.</summary>
		/// <returns>The first address of the variable.</returns>
		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x060061ED RID: 25069
		int AddressField1 { get; }

		/// <summary>Gets the second address of a variable.</summary>
		/// <returns>The second address of the variable.</returns>
		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x060061EE RID: 25070
		int AddressField2 { get; }

		/// <summary>Gets the third address of a variable.</summary>
		/// <returns>The third address of the variable.</returns>
		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x060061EF RID: 25071
		int AddressField3 { get; }

		/// <summary>Gets the <see cref="T:System.Diagnostics.SymbolStore.SymAddressKind" /> value describing the type of the address.</summary>
		/// <returns>The type of the address. One of the <see cref="T:System.Diagnostics.SymbolStore.SymAddressKind" /> values.</returns>
		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x060061F0 RID: 25072
		SymAddressKind AddressKind { get; }

		/// <summary>Gets the attributes of the variable.</summary>
		/// <returns>The variable attributes.</returns>
		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x060061F1 RID: 25073
		object Attributes { get; }

		/// <summary>Gets the end offset of a variable within the scope of the variable.</summary>
		/// <returns>The end offset of the variable.</returns>
		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x060061F2 RID: 25074
		int EndOffset { get; }

		/// <summary>Gets the name of the variable.</summary>
		/// <returns>The name of the variable.</returns>
		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x060061F3 RID: 25075
		string Name { get; }

		/// <summary>Gets the start offset of the variable within the scope of the variable.</summary>
		/// <returns>The start offset of the variable.</returns>
		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x060061F4 RID: 25076
		int StartOffset { get; }

		/// <summary>Gets the variable signature.</summary>
		/// <returns>The variable signature as an opaque blob.</returns>
		// Token: 0x060061F5 RID: 25077
		byte[] GetSignature();
	}
}
