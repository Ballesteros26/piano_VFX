using System;

namespace System.Web.UI
{
	/// <summary>Specifies how conditional expressions defined by an <see cref="T:System.Web.UI.VerificationAttribute" /> instance are used in verification.</summary>
	// Token: 0x0200024A RID: 586
	public enum VerificationRule
	{
		/// <summary>The conditional expression specified in an <see cref="T:System.Web.UI.VerificationAttribute" /> instance is required.</summary>
		// Token: 0x0400160A RID: 5642
		Required,
		/// <summary>The conditional expression specified in an <see cref="T:System.Web.UI.VerificationAttribute" /> instance is prohibited.</summary>
		// Token: 0x0400160B RID: 5643
		Prohibited,
		/// <summary>The conditional expression specified in an <see cref="T:System.Web.UI.VerificationAttribute" /> instance must have a left hand side that is not an empty string ("").</summary>
		// Token: 0x0400160C RID: 5644
		NotEmptyString
	}
}
