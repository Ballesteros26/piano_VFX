using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The FieldToken struct is an object representation of a token that represents a field.</summary>
	// Token: 0x0200035B RID: 859
	[ComVisible(true)]
	[Serializable]
	public struct FieldToken
	{
		// Token: 0x06002693 RID: 9875 RVA: 0x00089172 File Offset: 0x00087372
		internal FieldToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Determines if an object is an instance of FieldToken and is equal to this instance.</summary>
		/// <returns>Returns true if <paramref name="obj" /> is an instance of FieldToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this FieldToken. </param>
		// Token: 0x06002694 RID: 9876 RVA: 0x0008917C File Offset: 0x0008737C
		public override bool Equals(object obj)
		{
			bool flag = obj is FieldToken;
			if (flag)
			{
				FieldToken fieldToken = (FieldToken)obj;
				flag = this.tokValue == fieldToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.FieldToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to the current instance.</param>
		// Token: 0x06002695 RID: 9877 RVA: 0x000891AD File Offset: 0x000873AD
		public bool Equals(FieldToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.FieldToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06002696 RID: 9878 RVA: 0x000891BD File Offset: 0x000873BD
		public static bool operator ==(FieldToken a, FieldToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.FieldToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06002697 RID: 9879 RVA: 0x000891D0 File Offset: 0x000873D0
		public static bool operator !=(FieldToken a, FieldToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Generates the hash code for this field.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x06002698 RID: 9880 RVA: 0x000891E6 File Offset: 0x000873E6
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this field.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this field.</returns>
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x000891E6 File Offset: 0x000873E6
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x04001416 RID: 5142
		internal int tokValue;

		/// <summary>The default FieldToken with <see cref="P:System.Reflection.Emit.FieldToken.Token" /> value 0.</summary>
		// Token: 0x04001417 RID: 5143
		public static readonly FieldToken Empty;
	}
}
