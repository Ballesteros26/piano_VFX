using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its global assembly cache membership. This class cannot be inherited.</summary>
	// Token: 0x02000568 RID: 1384
	[ComVisible(true)]
	[Serializable]
	public sealed class GacMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		/// <summary>Indicates whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>true if the specified evidence satisfies the membership condition; otherwise, false.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> against which to make the test. </param>
		// Token: 0x06003E29 RID: 15913 RVA: 0x000DEDE8 File Offset: 0x000DCFE8
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				if (hostEnumerator.Current is GacInstalled)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new <see cref="T:System.Security.Policy.GacMembershipCondition" /> object.</returns>
		// Token: 0x06003E2A RID: 15914 RVA: 0x000DEE1B File Offset: 0x000DD01B
		public IMembershipCondition Copy()
		{
			return new GacMembershipCondition();
		}

		/// <summary>Indicates whether the current object is equivalent to the specified object.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.Security.Policy.GacMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current object. </param>
		// Token: 0x06003E2B RID: 15915 RVA: 0x000DEE22 File Offset: 0x000DD022
		public override bool Equals(object o)
		{
			return o != null && o is GacMembershipCondition;
		}

		/// <summary>Uses the specified XML encoding to reconstruct a security object.</summary>
		/// <param name="e">The <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid membership condition element. </exception>
		// Token: 0x06003E2C RID: 15916 RVA: 0x000DEE32 File Offset: 0x000DD032
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Uses the specified XML encoding to reconstruct a security object, using the specified policy level context.</summary>
		/// <param name="e">The <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for resolving <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid membership condition element. </exception>
		// Token: 0x06003E2D RID: 15917 RVA: 0x000DEE3C File Offset: 0x000DD03C
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
		}

		/// <summary>Gets a hash code for the current membership condition.</summary>
		/// <returns>0 (zero).</returns>
		// Token: 0x06003E2E RID: 15918 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override int GetHashCode()
		{
			return 0;
		}

		/// <summary>Returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the membership condition.</returns>
		// Token: 0x06003E2F RID: 15919 RVA: 0x000DEE56 File Offset: 0x000DD056
		public override string ToString()
		{
			return "GAC";
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding of the security object, including any state information.</returns>
		// Token: 0x06003E30 RID: 15920 RVA: 0x000DEE5D File Offset: 0x000DD05D
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state, using the specified policy level context.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for resolving <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		// Token: 0x06003E31 RID: 15921 RVA: 0x000DEE66 File Offset: 0x000DD066
		public SecurityElement ToXml(PolicyLevel level)
		{
			return MembershipConditionHelper.Element(typeof(GacMembershipCondition), this.version);
		}

		// Token: 0x04001FD9 RID: 8153
		private readonly int version = 1;
	}
}
