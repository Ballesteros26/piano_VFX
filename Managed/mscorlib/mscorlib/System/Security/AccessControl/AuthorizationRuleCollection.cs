using System;
using System.Collections;

namespace System.Security.AccessControl
{
	/// <summary>Represents a collection of <see cref="T:System.Security.AccessControl.AuthorizationRule" /> objects.</summary>
	// Token: 0x020005D0 RID: 1488
	public sealed class AuthorizationRuleCollection : ReadOnlyCollectionBase
	{
		// Token: 0x0600418A RID: 16778 RVA: 0x000E8E27 File Offset: 0x000E7027
		public AuthorizationRuleCollection()
		{
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x000E8E2F File Offset: 0x000E702F
		internal AuthorizationRuleCollection(AuthorizationRule[] rules)
		{
			base.InnerList.AddRange(rules);
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x000E8E43 File Offset: 0x000E7043
		public void AddRule(AuthorizationRule rule)
		{
			base.InnerList.Add(rule);
		}

		/// <summary>Gets the <see cref="T:System.Security.AccessControl.AuthorizationRule" /> object at the specified index of the collection.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AuthorizationRule" /> object at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.AccessControl.AuthorizationRule" /> object to get.</param>
		// Token: 0x17000AE2 RID: 2786
		public AuthorizationRule this[int index]
		{
			get
			{
				return (AuthorizationRule)base.InnerList[index];
			}
		}

		/// <summary>Copies the contents of the collection to an array.</summary>
		/// <param name="rules">An array to which to copy the contents of the collection.</param>
		/// <param name="index">The zero-based index from which to begin copying.</param>
		// Token: 0x0600418E RID: 16782 RVA: 0x000E8E65 File Offset: 0x000E7065
		public void CopyTo(AuthorizationRule[] rules, int index)
		{
			base.InnerList.CopyTo(rules, index);
		}
	}
}
