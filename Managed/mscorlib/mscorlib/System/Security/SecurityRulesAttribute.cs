using System;

namespace System.Security
{
	/// <summary>Indicates the set of security rules the common language runtime should enforce for an assembly.  </summary>
	// Token: 0x02000536 RID: 1334
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class SecurityRulesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityRulesAttribute" /> class using the specified rule set value. </summary>
		/// <param name="ruleSet">One of the enumeration values that specifies the transparency rules set. </param>
		// Token: 0x06003C32 RID: 15410 RVA: 0x000D8F21 File Offset: 0x000D7121
		public SecurityRulesAttribute(SecurityRuleSet ruleSet)
		{
			this.m_ruleSet = ruleSet;
		}

		/// <summary>Determines whether fully trusted transparent code should skip Microsoft intermediate language (MSIL) verification.</summary>
		/// <returns>true if MSIL verification should be skipped; otherwise, false. The default is false.</returns>
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x000D8F30 File Offset: 0x000D7130
		// (set) Token: 0x06003C34 RID: 15412 RVA: 0x000D8F38 File Offset: 0x000D7138
		public bool SkipVerificationInFullTrust
		{
			get
			{
				return this.m_skipVerificationInFullTrust;
			}
			set
			{
				this.m_skipVerificationInFullTrust = value;
			}
		}

		/// <summary>Gets the rule set to be applied.</summary>
		/// <returns>One of the enumeration values that specifies the transparency rules to be applied.</returns>
		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x000D8F41 File Offset: 0x000D7141
		public SecurityRuleSet RuleSet
		{
			get
			{
				return this.m_ruleSet;
			}
		}

		// Token: 0x04001F31 RID: 7985
		private SecurityRuleSet m_ruleSet;

		// Token: 0x04001F32 RID: 7986
		private bool m_skipVerificationInFullTrust;
	}
}
