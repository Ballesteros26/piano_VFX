using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents a token that represents a string.</summary>
	// Token: 0x0200037F RID: 895
	[ComVisible(true)]
	[Serializable]
	public struct StringToken
	{
		// Token: 0x060028D4 RID: 10452 RVA: 0x0009126E File Offset: 0x0008F46E
		internal StringToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of StringToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of StringToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this StringToken. </param>
		// Token: 0x060028D5 RID: 10453 RVA: 0x00091278 File Offset: 0x0008F478
		public override bool Equals(object obj)
		{
			bool flag = obj is StringToken;
			if (flag)
			{
				StringToken stringToken = (StringToken)obj;
				flag = this.tokValue == stringToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.StringToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to the current instance.</param>
		// Token: 0x060028D6 RID: 10454 RVA: 0x000912A9 File Offset: 0x0008F4A9
		public bool Equals(StringToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.StringToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060028D7 RID: 10455 RVA: 0x000912B9 File Offset: 0x0008F4B9
		public static bool operator ==(StringToken a, StringToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.StringToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060028D8 RID: 10456 RVA: 0x000912CC File Offset: 0x0008F4CC
		public static bool operator !=(StringToken a, StringToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Returns the hash code for this string.</summary>
		/// <returns>Returns the underlying string token.</returns>
		// Token: 0x060028D9 RID: 10457 RVA: 0x000912E2 File Offset: 0x0008F4E2
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this string.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this string.</returns>
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060028DA RID: 10458 RVA: 0x000912E2 File Offset: 0x0008F4E2
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x04001600 RID: 5632
		internal int tokValue;
	}
}
