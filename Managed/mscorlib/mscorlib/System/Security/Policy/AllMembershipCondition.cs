using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Represents a membership condition that matches all code. This class cannot be inherited.</summary>
	// Token: 0x02000554 RID: 1364
	[ComVisible(true)]
	[Serializable]
	public sealed class AllMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>Always true.</returns>
		/// <param name="evidence">The evidence set against which to make the test. </param>
		// Token: 0x06003D5D RID: 15709 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool Check(Evidence evidence)
		{
			return true;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		// Token: 0x06003D5E RID: 15710 RVA: 0x000DCB56 File Offset: 0x000DAD56
		public IMembershipCondition Copy()
		{
			return new AllMembershipCondition();
		}

		/// <summary>Determines whether the specified membership condition is an <see cref="T:System.Security.Policy.AllMembershipCondition" />.</summary>
		/// <returns>true if the specified membership condition is an <see cref="T:System.Security.Policy.AllMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The object to compare to <see cref="T:System.Security.Policy.AllMembershipCondition" />. </param>
		// Token: 0x06003D5F RID: 15711 RVA: 0x000DCB5D File Offset: 0x000DAD5D
		public override bool Equals(object o)
		{
			return o is AllMembershipCondition;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		// Token: 0x06003D60 RID: 15712 RVA: 0x000DCB68 File Offset: 0x000DAD68
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The policy level context used to resolve named permission set references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06003D61 RID: 15713 RVA: 0x000DCB72 File Offset: 0x000DAD72
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		// Token: 0x06003D62 RID: 15714 RVA: 0x000DCB8C File Offset: 0x000DAD8C
		public override int GetHashCode()
		{
			return typeof(AllMembershipCondition).GetHashCode();
		}

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A representation of the membership condition.</returns>
		// Token: 0x06003D63 RID: 15715 RVA: 0x000DCB9D File Offset: 0x000DAD9D
		public override string ToString()
		{
			return "All code";
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06003D64 RID: 15716 RVA: 0x000DCBA4 File Offset: 0x000DADA4
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The policy level context for resolving named permission set references. </param>
		// Token: 0x06003D65 RID: 15717 RVA: 0x000DCBAD File Offset: 0x000DADAD
		public SecurityElement ToXml(PolicyLevel level)
		{
			return MembershipConditionHelper.Element(typeof(AllMembershipCondition), this.version);
		}

		// Token: 0x04001F92 RID: 8082
		private readonly int version = 1;
	}
}
