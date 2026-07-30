using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The PropertyToken struct is an opaque representation of the Token returned by the metadata to represent a property.</summary>
	// Token: 0x0200037A RID: 890
	[ComVisible(true)]
	[Serializable]
	public struct PropertyToken
	{
		// Token: 0x060028A1 RID: 10401 RVA: 0x00090BEE File Offset: 0x0008EDEE
		internal PropertyToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of PropertyToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of PropertyToken and equals the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to this object. </param>
		// Token: 0x060028A2 RID: 10402 RVA: 0x00090BF8 File Offset: 0x0008EDF8
		public override bool Equals(object obj)
		{
			bool flag = obj is PropertyToken;
			if (flag)
			{
				PropertyToken propertyToken = (PropertyToken)obj;
				flag = this.tokValue == propertyToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.PropertyToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to the current instance.</param>
		// Token: 0x060028A3 RID: 10403 RVA: 0x00090C29 File Offset: 0x0008EE29
		public bool Equals(PropertyToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.PropertyToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060028A4 RID: 10404 RVA: 0x00090C39 File Offset: 0x0008EE39
		public static bool operator ==(PropertyToken a, PropertyToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.PropertyToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060028A5 RID: 10405 RVA: 0x00090C4C File Offset: 0x0008EE4C
		public static bool operator !=(PropertyToken a, PropertyToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Generates the hash code for this property.</summary>
		/// <returns>Returns the hash code for this property.</returns>
		// Token: 0x060028A6 RID: 10406 RVA: 0x00090C62 File Offset: 0x0008EE62
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this property.</summary>
		/// <returns>Read-only. Retrieves the metadata token for this instance.</returns>
		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x00090C62 File Offset: 0x0008EE62
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x040015D1 RID: 5585
		internal int tokValue;

		/// <summary>The default PropertyToken with <see cref="P:System.Reflection.Emit.PropertyToken.Token" /> value 0.</summary>
		// Token: 0x040015D2 RID: 5586
		public static readonly PropertyToken Empty;
	}
}
