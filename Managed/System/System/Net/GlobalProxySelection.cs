using System;

namespace System.Net
{
	/// <summary>Contains a global default proxy instance for all HTTP requests.</summary>
	// Token: 0x02000424 RID: 1060
	[Obsolete("This class has been deprecated. Please use WebRequest.DefaultWebProxy instead to access and set the global default proxy. Use 'null' instead of GetEmptyWebProxy. http://go.microsoft.com/fwlink/?linkid=14202")]
	public class GlobalProxySelection
	{
		/// <summary>Gets or sets the global HTTP proxy.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that every call to <see cref="M:System.Net.HttpWebRequest.GetResponse" /> uses.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation was null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission for the requested operation. </exception>
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x0007D728 File Offset: 0x0007B928
		// (set) Token: 0x0600202A RID: 8234 RVA: 0x0007D756 File Offset: 0x0007B956
		public static IWebProxy Select
		{
			get
			{
				IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
				if (defaultWebProxy == null)
				{
					return GlobalProxySelection.GetEmptyWebProxy();
				}
				WebRequest.WebProxyWrapper webProxyWrapper = defaultWebProxy as WebRequest.WebProxyWrapper;
				if (webProxyWrapper != null)
				{
					return webProxyWrapper.WebProxy;
				}
				return defaultWebProxy;
			}
			set
			{
				WebRequest.DefaultWebProxy = value;
			}
		}

		/// <summary>Returns an empty proxy instance.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that contains no information.</returns>
		// Token: 0x0600202B RID: 8235 RVA: 0x0007D75E File Offset: 0x0007B95E
		public static IWebProxy GetEmptyWebProxy()
		{
			return new EmptyWebProxy();
		}
	}
}
