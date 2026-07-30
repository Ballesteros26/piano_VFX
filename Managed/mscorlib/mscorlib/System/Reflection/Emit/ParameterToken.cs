using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The ParameterToken struct is an opaque representation of the token returned by the metadata to represent a parameter.</summary>
	// Token: 0x02000377 RID: 887
	[ComVisible(true)]
	[Serializable]
	public struct ParameterToken
	{
		// Token: 0x06002866 RID: 10342 RVA: 0x0009081E File Offset: 0x0008EA1E
		internal ParameterToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of ParameterToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of ParameterToken and equals the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this object. </param>
		// Token: 0x06002867 RID: 10343 RVA: 0x00090828 File Offset: 0x0008EA28
		public override bool Equals(object obj)
		{
			bool flag = obj is ParameterToken;
			if (flag)
			{
				ParameterToken parameterToken = (ParameterToken)obj;
				flag = this.tokValue == parameterToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.ParameterToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to the current instance.</param>
		// Token: 0x06002868 RID: 10344 RVA: 0x00090859 File Offset: 0x0008EA59
		public bool Equals(ParameterToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.ParameterToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06002869 RID: 10345 RVA: 0x00090869 File Offset: 0x0008EA69
		public static bool operator ==(ParameterToken a, ParameterToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.ParameterToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x0600286A RID: 10346 RVA: 0x0009087C File Offset: 0x0008EA7C
		public static bool operator !=(ParameterToken a, ParameterToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Generates the hash code for this parameter.</summary>
		/// <returns>Returns the hash code for this parameter.</returns>
		// Token: 0x0600286B RID: 10347 RVA: 0x00090892 File Offset: 0x0008EA92
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this parameter.</summary>
		/// <returns>Read-only. Retrieves the metadata token for this parameter.</returns>
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x00090892 File Offset: 0x0008EA92
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x040015BE RID: 5566
		internal int tokValue;

		/// <summary>The default ParameterToken with <see cref="P:System.Reflection.Emit.ParameterToken.Token" /> value 0.</summary>
		// Token: 0x040015BF RID: 5567
		public static readonly ParameterToken Empty;
	}
}
