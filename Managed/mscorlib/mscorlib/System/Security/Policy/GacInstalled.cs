using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Confirms that a code assembly originates in the global assembly cache (GAC) as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000567 RID: 1383
	[ComVisible(true)]
	[Serializable]
	public sealed class GacInstalled : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		/// <summary>Creates an equivalent copy of the current object.</summary>
		/// <returns>An equivalent copy of <see cref="T:System.Security.Policy.GacInstalled" />.</returns>
		// Token: 0x06003E20 RID: 15904 RVA: 0x000DED87 File Offset: 0x000DCF87
		public object Copy()
		{
			return new GacInstalled();
		}

		/// <summary>Creates a new identity permission that corresponds to the current object.</summary>
		/// <returns>A new identity permission that corresponds to the current object.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the identity permission. </param>
		// Token: 0x06003E21 RID: 15905 RVA: 0x000DED8E File Offset: 0x000DCF8E
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new GacIdentityPermission();
		}

		/// <summary>Indicates whether the current object is equivalent to the specified object.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.Security.Policy.GacInstalled" /> object; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current object. </param>
		// Token: 0x06003E22 RID: 15906 RVA: 0x000DED95 File Offset: 0x000DCF95
		public override bool Equals(object o)
		{
			return o != null && o is GacInstalled;
		}

		/// <summary>Returns a hash code for the current object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x06003E23 RID: 15907 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override int GetHashCode()
		{
			return 0;
		}

		/// <summary>Returns a string representation of the current  object.</summary>
		/// <returns>A string representation of the current object.</returns>
		// Token: 0x06003E24 RID: 15908 RVA: 0x000DEDA5 File Offset: 0x000DCFA5
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			return securityElement.ToString();
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x00003B29 File Offset: 0x00001D29
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return 1;
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x0006F1CD File Offset: 0x0006D3CD
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return position;
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x000DEDCC File Offset: 0x000DCFCC
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position] = '\t';
			return position + 1;
		}
	}
}
