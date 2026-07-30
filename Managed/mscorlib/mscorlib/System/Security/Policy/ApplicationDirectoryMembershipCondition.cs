using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its application directory. This class cannot be inherited.</summary>
	// Token: 0x02000556 RID: 1366
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationDirectoryMembershipCondition : IConstantMembershipCondition, IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable
	{
		/// <summary>Determines whether the membership condition is satisfied by the specified evidence.</summary>
		/// <returns>true if the specified evidence satisfies the membership condition; otherwise, false.</returns>
		/// <param name="evidence">The evidence set against which to make the test. </param>
		// Token: 0x06003D71 RID: 15729 RVA: 0x000DCCF4 File Offset: 0x000DAEF4
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			string codeBase = Assembly.GetCallingAssembly().CodeBase;
			Uri uri = new Uri(codeBase);
			Url url = new Url(codeBase);
			bool flag = false;
			bool flag2 = false;
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				if (!flag && obj is ApplicationDirectory)
				{
					string directory = (obj as ApplicationDirectory).Directory;
					flag = string.Compare(directory, 0, uri.ToString(), 0, directory.Length, true, CultureInfo.InvariantCulture) == 0;
				}
				else if (!flag2 && obj is Url)
				{
					flag2 = url.Equals(obj);
				}
				if (flag && flag2)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		// Token: 0x06003D72 RID: 15730 RVA: 0x000DCD96 File Offset: 0x000DAF96
		public IMembershipCondition Copy()
		{
			return new ApplicationDirectoryMembershipCondition();
		}

		/// <summary>Determines whether the specified membership condition is an <see cref="T:System.Security.Policy.ApplicationDirectoryMembershipCondition" />.</summary>
		/// <returns>true if the specified membership condition is an <see cref="T:System.Security.Policy.ApplicationDirectoryMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The object to compare to <see cref="T:System.Security.Policy.ApplicationDirectoryMembershipCondition" />. </param>
		// Token: 0x06003D73 RID: 15731 RVA: 0x000DCD9D File Offset: 0x000DAF9D
		public override bool Equals(object o)
		{
			return o is ApplicationDirectoryMembershipCondition;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid application directory membership condition element. </exception>
		// Token: 0x06003D74 RID: 15732 RVA: 0x000DCDA8 File Offset: 0x000DAFA8
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The policy level context, used to resolve named permission set references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid application directory membership condition element. </exception>
		// Token: 0x06003D75 RID: 15733 RVA: 0x000DCDB2 File Offset: 0x000DAFB2
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		// Token: 0x06003D76 RID: 15734 RVA: 0x000DCDCC File Offset: 0x000DAFCC
		public override int GetHashCode()
		{
			return typeof(ApplicationDirectoryMembershipCondition).GetHashCode();
		}

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the state of the membership condition.</returns>
		// Token: 0x06003D77 RID: 15735 RVA: 0x000DCDDD File Offset: 0x000DAFDD
		public override string ToString()
		{
			return "ApplicationDirectory";
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06003D78 RID: 15736 RVA: 0x000DCDE4 File Offset: 0x000DAFE4
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The policy level context for resolving named permission set references. </param>
		// Token: 0x06003D79 RID: 15737 RVA: 0x000DCDED File Offset: 0x000DAFED
		public SecurityElement ToXml(PolicyLevel level)
		{
			return MembershipConditionHelper.Element(typeof(ApplicationDirectoryMembershipCondition), this.version);
		}

		// Token: 0x04001F94 RID: 8084
		private readonly int version = 1;
	}
}
