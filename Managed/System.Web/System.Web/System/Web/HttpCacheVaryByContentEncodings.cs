using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Unity;

namespace System.Web
{
	/// <summary>Provides a type-safe way to set the <see cref="P:System.Web.HttpCachePolicy.VaryByContentEncodings" /> property of the <see cref="T:System.Web.HttpCachePolicy" /> class.</summary>
	// Token: 0x02000084 RID: 132
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpCacheVaryByContentEncodings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCacheVaryByContentEncodings" /> class.</summary>
		// Token: 0x060005DE RID: 1502 RVA: 0x0000E9B7 File Offset: 0x0000CBB7
		public HttpCacheVaryByContentEncodings()
		{
			this.encodings = new Dictionary<string, bool>();
		}

		/// <summary>Gets or sets a value that indicates whether the cache varies according to the specified content encoding.</summary>
		/// <returns>true if the cache should vary by the specified content encoding; otherwise, false.</returns>
		/// <param name="contentEncoding">The name of the content encoding.</param>
		/// <exception cref="T:System.ArgumentNullException">The content encoding is null.</exception>
		// Token: 0x1700022C RID: 556
		public bool this[string contentEncoding]
		{
			get
			{
				if (contentEncoding == null)
				{
					throw new ArgumentNullException("contentEncoding");
				}
				return this.encodings.ContainsKey(contentEncoding) && this.encodings[contentEncoding];
			}
			set
			{
				if (contentEncoding == null)
				{
					throw new ArgumentNullException("contentEncoding");
				}
				this.encodings[contentEncoding] = value;
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string[] GetContentEncodings()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetContentEncodings(string[] contentEncodings)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000F1D RID: 3869
		private Dictionary<string, bool> encodings;
	}
}
