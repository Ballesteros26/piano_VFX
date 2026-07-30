using System;

namespace System.Web.Configuration
{
	/// <summary>Used internally at run time by <see cref="T:System.Web.Configuration.BrowserCapabilitiesFactory" /> and <see cref="T:System.Web.Configuration.BrowserCapabilitiesCodeGenerator" /> to parse request data and identify the browser.</summary>
	// Token: 0x020005D1 RID: 1489
	public class RegexWorker
	{
		/// <summary>Creates a new instance of <see cref="T:System.Web.Configuration.RegexWorker" />.</summary>
		/// <param name="browserCaps">The <see cref="T:System.Web.Configuration.HttpCapabilitiesBase" /> object to be configured.</param>
		// Token: 0x0600403B RID: 16443 RVA: 0x00002050 File Offset: 0x00000250
		public RegexWorker(HttpBrowserCapabilities browserCaps)
		{
		}

		/// <summary>Used internally at run time to determine whether the specified request-header value matches any of the capabilities of an internal collection of browsers.</summary>
		/// <returns>true if the specified request-header value matches any of the capabilities of an internal collection of browsers; otherwise, false. The default is false.</returns>
		/// <param name="target">The capabilities value from an internal collection of browsers.</param>
		/// <param name="regexExpression">The specified request-header value.</param>
		// Token: 0x0600403C RID: 16444 RVA: 0x00008A69 File Offset: 0x00006C69
		[global::System.MonoTODO("Mono does not currently need this routine.  Always returns false.")]
		public bool ProcessRegex(string target, string regexExpression)
		{
			return false;
		}

		/// <summary>Accessor to this class.</summary>
		/// <returns>The internal value associated with the specified<paramref name="key" />.</returns>
		/// <param name="key">The specified key.</param>
		// Token: 0x17001444 RID: 5188
		[global::System.MonoTODO("Mono does not currently need this routine. Not implemented.")]
		public string this[string key]
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
