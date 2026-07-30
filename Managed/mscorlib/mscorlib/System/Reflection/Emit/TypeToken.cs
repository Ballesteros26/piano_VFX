using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the Token returned by the metadata to represent a type.</summary>
	// Token: 0x02000382 RID: 898
	[ComVisible(true)]
	[Serializable]
	public struct TypeToken
	{
		// Token: 0x060029A5 RID: 10661 RVA: 0x00093E50 File Offset: 0x00092050
		internal TypeToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of TypeToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of TypeToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this TypeToken. </param>
		// Token: 0x060029A6 RID: 10662 RVA: 0x00093E5C File Offset: 0x0009205C
		public override bool Equals(object obj)
		{
			bool flag = obj is TypeToken;
			if (flag)
			{
				TypeToken typeToken = (TypeToken)obj;
				flag = this.tokValue == typeToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.TypeToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to the current instance.</param>
		// Token: 0x060029A7 RID: 10663 RVA: 0x00093E8D File Offset: 0x0009208D
		public bool Equals(TypeToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.TypeToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060029A8 RID: 10664 RVA: 0x00093E9D File Offset: 0x0009209D
		public static bool operator ==(TypeToken a, TypeToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.TypeToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060029A9 RID: 10665 RVA: 0x00093EB0 File Offset: 0x000920B0
		public static bool operator !=(TypeToken a, TypeToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Generates the hash code for this type.</summary>
		/// <returns>Returns the hash code for this type.</returns>
		// Token: 0x060029AA RID: 10666 RVA: 0x00093EC6 File Offset: 0x000920C6
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this class.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this type.</returns>
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x00093EC6 File Offset: 0x000920C6
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x04001623 RID: 5667
		internal int tokValue;

		/// <summary>The default TypeToken with <see cref="P:System.Reflection.Emit.TypeToken.Token" /> value 0.</summary>
		// Token: 0x04001624 RID: 5668
		public static readonly TypeToken Empty;
	}
}
