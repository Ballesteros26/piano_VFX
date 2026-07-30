using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the Token returned by the metadata to represent a signature.</summary>
	// Token: 0x0200037D RID: 893
	[ComVisible(true)]
	public struct SignatureToken
	{
		// Token: 0x060028CC RID: 10444 RVA: 0x000911F3 File Offset: 0x0008F3F3
		internal SignatureToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of SignatureToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of SignatureToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this SignatureToken. </param>
		// Token: 0x060028CD RID: 10445 RVA: 0x000911FC File Offset: 0x0008F3FC
		public override bool Equals(object obj)
		{
			bool flag = obj is SignatureToken;
			if (flag)
			{
				SignatureToken signatureToken = (SignatureToken)obj;
				flag = this.tokValue == signatureToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.SignatureToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to the current instance.</param>
		// Token: 0x060028CE RID: 10446 RVA: 0x0009122D File Offset: 0x0008F42D
		public bool Equals(SignatureToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.SignatureToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060028CF RID: 10447 RVA: 0x0009123D File Offset: 0x0008F43D
		public static bool operator ==(SignatureToken a, SignatureToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.SignatureToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060028D0 RID: 10448 RVA: 0x00091250 File Offset: 0x0008F450
		public static bool operator !=(SignatureToken a, SignatureToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Generates the hash code for this signature.</summary>
		/// <returns>Returns the hash code for this signature.</returns>
		// Token: 0x060028D1 RID: 10449 RVA: 0x00091266 File Offset: 0x0008F466
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for the local variable signature for this method.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this signature.</returns>
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060028D2 RID: 10450 RVA: 0x00091266 File Offset: 0x0008F466
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x040015E0 RID: 5600
		internal int tokValue;

		/// <summary>The default SignatureToken with <see cref="P:System.Reflection.Emit.SignatureToken.Token" /> value 0.</summary>
		// Token: 0x040015E1 RID: 5601
		public static readonly SignatureToken Empty;
	}
}
