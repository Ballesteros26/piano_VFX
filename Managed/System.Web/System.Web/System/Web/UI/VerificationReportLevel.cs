using System;

namespace System.Web.UI
{
	/// <summary>Specifies reporting levels for an accessibility rule defined by an <see cref="T:System.Web.UI.VerificationAttribute" /> instance.</summary>
	// Token: 0x02000249 RID: 585
	public enum VerificationReportLevel
	{
		/// <summary>The verification rule represented by the <see cref="T:System.Web.UI.VerificationAttribute" /> instance is an error.</summary>
		// Token: 0x04001606 RID: 5638
		Error,
		/// <summary>The verification rule represented by the <see cref="T:System.Web.UI.VerificationAttribute" /> instance is a warning.</summary>
		// Token: 0x04001607 RID: 5639
		Warning,
		/// <summary>The verification rule represented by the <see cref="T:System.Web.UI.VerificationAttribute" /> instance is a guideline.</summary>
		// Token: 0x04001608 RID: 5640
		Guideline
	}
}
