using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies behavior for file change notification (FCN) in the application.</summary>
	// Token: 0x020006A6 RID: 1702
	public enum FcnMode
	{
		/// <summary>For each subdirectory, the application creates an object that monitors the subdirectory. This is the default behavior.</summary>
		// Token: 0x040025CF RID: 9679
		Default = 1,
		/// <summary>File change notification is disabled.</summary>
		// Token: 0x040025D0 RID: 9680
		Disabled,
		/// <summary>File change notification is not set, so the application creates an object that monitors each subdirectory. This is the default behavior.</summary>
		// Token: 0x040025D1 RID: 9681
		NotSet = 0,
		/// <summary>The application creates one object to monitor the main directory and uses this object to monitor each subdirectory.</summary>
		// Token: 0x040025D2 RID: 9682
		Single = 3
	}
}
