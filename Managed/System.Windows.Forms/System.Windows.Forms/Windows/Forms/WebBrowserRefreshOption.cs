using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants that define how the <see cref="T:System.Windows.Forms.WebBrowser" /> control can refresh its contents.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003B3 RID: 947
	public enum WebBrowserRefreshOption
	{
		/// <summary>A refresh that requests a copy of the current Web page that has been cached on the server.</summary>
		// Token: 0x04001CCB RID: 7371
		Normal,
		/// <summary>A refresh that requests an update only if the current Web page has expired.</summary>
		// Token: 0x04001CCC RID: 7372
		IfExpired,
		/// <summary>For internal use only; do not use.</summary>
		// Token: 0x04001CCD RID: 7373
		Continue,
		/// <summary>A refresh that requests the latest version of the current Web page.</summary>
		// Token: 0x04001CCE RID: 7374
		Completely
	}
}
