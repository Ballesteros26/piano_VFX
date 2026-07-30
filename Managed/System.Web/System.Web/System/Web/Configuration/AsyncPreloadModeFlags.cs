using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the mode for asynchronous requests.</summary>
	// Token: 0x0200055C RID: 1372
	[Flags]
	public enum AsyncPreloadModeFlags
	{
		/// <summary>No asynchronous upload of the request entity body will occur.</summary>
		// Token: 0x04001FF9 RID: 8185
		None = 0,
		/// <summary>Asynchronous uploads of the request entity body will occur only for form posts. This flag enables asynchronous preloading for MIME types that are specifically set to application/x-www-form-urlencoded.</summary>
		// Token: 0x04001FFA RID: 8186
		Form = 1,
		/// <summary>Asynchronous uploads of the request entity body will occur only for multi-part form data. This flag enables asynchronous preloading for MIME types that are specifically set to multipart/form-data.</summary>
		// Token: 0x04001FFB RID: 8187
		FormMultiPart = 2,
		/// <summary>Asynchronous uploads of the request entity body will occur only for non-form posts.</summary>
		// Token: 0x04001FFC RID: 8188
		NonForm = 4,
		/// <summary>Asynchronous uploads of the request entity body will occur only for form posts.</summary>
		// Token: 0x04001FFD RID: 8189
		AllFormTypes = 3,
		/// <summary>Asynchronous uploads of the request entity body will occur for all posts.</summary>
		// Token: 0x04001FFE RID: 8190
		All = 7
	}
}
