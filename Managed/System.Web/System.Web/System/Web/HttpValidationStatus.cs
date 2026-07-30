using System;

namespace System.Web
{
	/// <summary>Provides enumerated values that indicate cache validation status.</summary>
	// Token: 0x020000BC RID: 188
	public enum HttpValidationStatus
	{
		/// <summary>Indicates that the cache is invalid. The item is evicted from the cache and the request is handled as a cache miss. </summary>
		// Token: 0x0400101F RID: 4127
		Invalid = 1,
		/// <summary>Indicates that the request is treated as a cache miss and the page is executed. The cache is not invalidated. </summary>
		// Token: 0x04001020 RID: 4128
		IgnoreThisRequest,
		/// <summary>Indicates that the cache is valid. </summary>
		// Token: 0x04001021 RID: 4129
		Valid
	}
}
