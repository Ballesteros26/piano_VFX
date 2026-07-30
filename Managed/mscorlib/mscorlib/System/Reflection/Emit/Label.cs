using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents a label in the instruction stream. Label is used in conjunction with the <see cref="T:System.Reflection.Emit.ILGenerator" /> class.</summary>
	// Token: 0x02000367 RID: 871
	[ComVisible(true)]
	[Serializable]
	public struct Label
	{
		// Token: 0x06002742 RID: 10050 RVA: 0x0008BA3C File Offset: 0x00089C3C
		internal Label(int val)
		{
			this.label = val;
		}

		/// <summary>Checks if the given object is an instance of Label and is equal to this instance.</summary>
		/// <returns>Returns true if <paramref name="obj" /> is an instance of Label and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this Label instance. </param>
		// Token: 0x06002743 RID: 10051 RVA: 0x0008BA48 File Offset: 0x00089C48
		public override bool Equals(object obj)
		{
			bool flag = obj is Label;
			if (flag)
			{
				Label label = (Label)obj;
				flag = this.label == label.label;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.Label" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.Label" /> to compare to the current instance.</param>
		// Token: 0x06002744 RID: 10052 RVA: 0x0008BA79 File Offset: 0x00089C79
		public bool Equals(Label obj)
		{
			return this.label == obj.label;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.Label" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.Label" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.Label" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06002745 RID: 10053 RVA: 0x0008BA89 File Offset: 0x00089C89
		public static bool operator ==(Label a, Label b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.Label" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.Label" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.Label" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06002746 RID: 10054 RVA: 0x0008BA93 File Offset: 0x00089C93
		public static bool operator !=(Label a, Label b)
		{
			return !(a == b);
		}

		/// <summary>Generates a hash code for this instance.</summary>
		/// <returns>Returns a hash code for this instance.</returns>
		// Token: 0x06002747 RID: 10055 RVA: 0x0008BA9F File Offset: 0x00089C9F
		public override int GetHashCode()
		{
			return this.label.GetHashCode();
		}

		// Token: 0x0400145D RID: 5213
		internal int label;
	}
}
