using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The MethodToken struct is an object representation of a token that represents a method.</summary>
	// Token: 0x0200036C RID: 876
	[ComVisible(true)]
	[Serializable]
	public struct MethodToken
	{
		// Token: 0x060027C6 RID: 10182 RVA: 0x0008CC49 File Offset: 0x0008AE49
		internal MethodToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Tests whether the given object is equal to this MethodToken object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of MethodToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this object. </param>
		// Token: 0x060027C7 RID: 10183 RVA: 0x0008CC54 File Offset: 0x0008AE54
		public override bool Equals(object obj)
		{
			bool flag = obj is MethodToken;
			if (flag)
			{
				MethodToken methodToken = (MethodToken)obj;
				flag = this.tokValue == methodToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.MethodToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to the current instance.</param>
		// Token: 0x060027C8 RID: 10184 RVA: 0x0008CC85 File Offset: 0x0008AE85
		public bool Equals(MethodToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.MethodToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060027C9 RID: 10185 RVA: 0x0008CC95 File Offset: 0x0008AE95
		public static bool operator ==(MethodToken a, MethodToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.MethodToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060027CA RID: 10186 RVA: 0x0008CCA8 File Offset: 0x0008AEA8
		public static bool operator !=(MethodToken a, MethodToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Returns the generated hash code for this method.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x060027CB RID: 10187 RVA: 0x0008CCBE File Offset: 0x0008AEBE
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Returns the metadata token for this method.</summary>
		/// <returns>Read-only. Returns the metadata token for this method.</returns>
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x0008CCBE File Offset: 0x0008AEBE
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x04001483 RID: 5251
		internal int tokValue;

		/// <summary>The default MethodToken with <see cref="P:System.Reflection.Emit.MethodToken.Token" /> value 0.</summary>
		// Token: 0x04001484 RID: 5252
		public static readonly MethodToken Empty;
	}
}
