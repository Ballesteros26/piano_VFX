using System;
using System.Collections;
using System.Security.Permissions;
using Unity;

namespace System.Web
{
	/// <summary>Provides a type-safe way to set the <see cref="P:System.Web.HttpCachePolicy.VaryByHeaders" /> property.</summary>
	// Token: 0x02000085 RID: 133
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpCacheVaryByHeaders
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCacheVaryByHeaders" /> class.</summary>
		// Token: 0x060005E3 RID: 1507 RVA: 0x0000EA13 File Offset: 0x0000CC13
		public HttpCacheVaryByHeaders()
		{
			this.fields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000EA2C File Offset: 0x0000CC2C
		internal string[] GetHeaderNames(bool omitVaryStar)
		{
			string[] array;
			if (this.vary_by_unspecified && !omitVaryStar)
			{
				array = new string[] { "*" };
			}
			else
			{
				int num = (this.vary_by_accept ? 1 : 0) + (this.vary_by_user_agent ? 1 : 0) + (this.vary_by_user_charset ? 1 : 0) + (this.vary_by_user_language ? 1 : 0);
				array = new string[this.fields.Count + num];
				int num2 = 0;
				if (this.vary_by_accept)
				{
					array[num2++] = "Accept";
				}
				if (this.vary_by_user_agent)
				{
					array[num2++] = "User-Agent";
				}
				if (this.vary_by_user_charset)
				{
					array[num2++] = "Accept-Charset";
				}
				if (this.vary_by_user_language)
				{
					array[num2++] = "Accept-Language";
				}
				this.fields.Keys.CopyTo(array, num);
			}
			return array;
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET output cache varies the cached responses by the Accept HTTP header, and appends it to the out-going Vary HTTP header.</summary>
		/// <returns>true when the ASP.NET output cache varies by the Accept header; otherwise, false. The default value is false.</returns>
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000EB04 File Offset: 0x0000CD04
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0000EB0C File Offset: 0x0000CD0C
		public bool AcceptTypes
		{
			get
			{
				return this.vary_by_accept;
			}
			set
			{
				this.vary_by_unspecified = false;
				this.vary_by_accept = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET output cache varies the cached responses by the User-Agent header, and appends it to the out-going Vary HTTP header.</summary>
		/// <returns>true when the ASP.NET output cache varies by the User-Agent header, and adds it to the Vary HTTP header sent to the client; otherwise, false. The default value is false.</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000EB1C File Offset: 0x0000CD1C
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0000EB24 File Offset: 0x0000CD24
		public bool UserAgent
		{
			get
			{
				return this.vary_by_user_agent;
			}
			set
			{
				this.vary_by_unspecified = false;
				this.vary_by_user_agent = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET output cache varies the cached responses by the Accept-Charset header, and appends it to the out-going Vary HTTP header.</summary>
		/// <returns>true when the ASP.NET output cache varies by the Accept-Charset header and adds it to the Vary HTTP header sent to the client; otherwise, false. The default value is false.</returns>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000EB34 File Offset: 0x0000CD34
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		public bool UserCharSet
		{
			get
			{
				return this.vary_by_user_charset;
			}
			set
			{
				this.vary_by_unspecified = false;
				this.vary_by_user_charset = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET output cache varies the cached responses by the Accept-Language header, and appends it to the out-going Vary HTTP header.</summary>
		/// <returns>true when ASP.NET output cache varies by the Accept-Language header and adds it to the Vary HTTP header sent to the client; otherwise, false. The default value is false.</returns>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0000EB54 File Offset: 0x0000CD54
		public bool UserLanguage
		{
			get
			{
				return this.vary_by_user_language;
			}
			set
			{
				this.vary_by_unspecified = false;
				this.vary_by_user_language = value;
			}
		}

		/// <summary>Gets or sets a custom header field that the ASP.NET output cache varies the cached responses by, and appends it to the out-going Vary HTTP header.</summary>
		/// <returns>true when the ASP.NET output cache varies by the specified custom field; otherwise, false. The default value is false. </returns>
		/// <param name="header">The name of the custom header. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="header" /> is null. </exception>
		// Token: 0x17000231 RID: 561
		public bool this[string header]
		{
			get
			{
				if (header == null)
				{
					throw new ArgumentNullException();
				}
				return this.fields.Contains(header);
			}
			set
			{
				if (header == null)
				{
					throw new ArgumentNullException();
				}
				this.vary_by_unspecified = false;
				if (value)
				{
					if (!this.fields.Contains(header))
					{
						this.fields.Add(header, true);
						return;
					}
					this.fields.Remove(header);
				}
			}
		}

		/// <summary>Causes ASP.NET to vary by all header values and sets the Vary HTTP header to the value * (an asterisk). All other Vary header information to be dropped.</summary>
		// Token: 0x060005EF RID: 1519 RVA: 0x0000EBCC File Offset: 0x0000CDCC
		public void VaryByUnspecifiedParameters()
		{
			this.fields.Clear();
			this.vary_by_unspecified = (this.vary_by_accept = (this.vary_by_user_agent = (this.vary_by_user_charset = (this.vary_by_user_language = false))));
			this.vary_by_unspecified = true;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string[] GetHeaders()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetHeaders(string[] headers)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000F1E RID: 3870
		private bool vary_by_unspecified;

		// Token: 0x04000F1F RID: 3871
		private bool vary_by_accept;

		// Token: 0x04000F20 RID: 3872
		private bool vary_by_user_agent;

		// Token: 0x04000F21 RID: 3873
		private bool vary_by_user_charset;

		// Token: 0x04000F22 RID: 3874
		private bool vary_by_user_language;

		// Token: 0x04000F23 RID: 3875
		private Hashtable fields;
	}
}
