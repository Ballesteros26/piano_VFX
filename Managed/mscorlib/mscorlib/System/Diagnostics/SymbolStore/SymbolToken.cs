using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>The <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure is an object representation of a token that represents symbolic information.</summary>
	// Token: 0x02000A7D RID: 2685
	[ComVisible(true)]
	public struct SymbolToken
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure when given a value.</summary>
		/// <param name="val">The value to be used for the token. </param>
		// Token: 0x0600620D RID: 25101 RVA: 0x00140B59 File Offset: 0x0013ED59
		public SymbolToken(int val)
		{
			this._val = val;
		}

		/// <summary>Determines whether <paramref name="obj" /> is an instance of <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> and is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">The object to check. </param>
		// Token: 0x0600620E RID: 25102 RVA: 0x00140B64 File Offset: 0x0013ED64
		public override bool Equals(object obj)
		{
			return obj is SymbolToken && ((SymbolToken)obj).GetToken() == this._val;
		}

		/// <summary>Determines whether <paramref name="obj" /> is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> to check.</param>
		// Token: 0x0600620F RID: 25103 RVA: 0x00140B91 File Offset: 0x0013ED91
		public bool Equals(SymbolToken obj)
		{
			return obj.GetToken() == this._val;
		}

		/// <summary>Returns a value indicating whether two <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> objects are equal.</summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> are equal; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure.</param>
		/// <param name="b">A <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure.</param>
		// Token: 0x06006210 RID: 25104 RVA: 0x00140BA2 File Offset: 0x0013EDA2
		public static bool operator ==(SymbolToken a, SymbolToken b)
		{
			return a.Equals(b);
		}

		/// <summary>Returns a value indicating whether two <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> are not equal; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure.</param>
		/// <param name="b">A <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure.</param>
		// Token: 0x06006211 RID: 25105 RVA: 0x00140BAC File Offset: 0x0013EDAC
		public static bool operator !=(SymbolToken a, SymbolToken b)
		{
			return !a.Equals(b);
		}

		/// <summary>Generates the hash code for the current token.</summary>
		/// <returns>The hash code for the current token.</returns>
		// Token: 0x06006212 RID: 25106 RVA: 0x00140BB9 File Offset: 0x0013EDB9
		public override int GetHashCode()
		{
			return this._val.GetHashCode();
		}

		/// <summary>Gets the value of the current token.</summary>
		/// <returns>The value of the current token.</returns>
		// Token: 0x06006213 RID: 25107 RVA: 0x00140BC6 File Offset: 0x0013EDC6
		public int GetToken()
		{
			return this._val;
		}

		// Token: 0x040030E9 RID: 12521
		private int _val;
	}
}
