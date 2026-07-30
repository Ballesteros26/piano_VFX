using System;

namespace System.Web.Security
{
	/// <summary>Provides data for the AnonymousIdentification_Creating event. This class cannot be inherited.</summary>
	// Token: 0x020004B6 RID: 1206
	public sealed class AnonymousIdentificationEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.AnonymousIdentificationEventArgs" /> class.</summary>
		/// <param name="context">The context for the event.</param>
		// Token: 0x06003668 RID: 13928 RVA: 0x0008E681 File Offset: 0x0008C881
		public AnonymousIdentificationEventArgs(HttpContext context)
		{
			this.context = context;
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</returns>
		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06003669 RID: 13929 RVA: 0x0008E690 File Offset: 0x0008C890
		public HttpContext Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets or sets the anonymous identifier for the user.</summary>
		/// <returns>The anonymous identifier for the user.</returns>
		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x0600366A RID: 13930 RVA: 0x0008E698 File Offset: 0x0008C898
		// (set) Token: 0x0600366B RID: 13931 RVA: 0x0008E6A0 File Offset: 0x0008C8A0
		public string AnonymousID
		{
			get
			{
				return this.anonymousId;
			}
			set
			{
				this.anonymousId = value;
			}
		}

		// Token: 0x04001DAD RID: 7597
		private HttpContext context;

		// Token: 0x04001DAE RID: 7598
		private string anonymousId;
	}
}
