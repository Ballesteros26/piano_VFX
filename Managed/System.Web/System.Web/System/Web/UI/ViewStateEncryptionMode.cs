using System;

namespace System.Web.UI
{
	/// <summary>Specifies whether view-state information is encrypted.</summary>
	// Token: 0x02000199 RID: 409
	public enum ViewStateEncryptionMode
	{
		/// <summary>The view-state information is encrypted if a control requests encryption by calling the <see cref="M:System.Web.UI.Page.RegisterRequiresViewStateEncryption" /> method. This is the default.</summary>
		// Token: 0x04001340 RID: 4928
		Auto,
		/// <summary>The view-state information is always encrypted.</summary>
		// Token: 0x04001341 RID: 4929
		Always,
		/// <summary>The view-state information is never encrypted, even if a control requests it.</summary>
		// Token: 0x04001342 RID: 4930
		Never
	}
}
