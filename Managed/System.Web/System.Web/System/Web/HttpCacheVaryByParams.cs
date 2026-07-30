using System;
using System.Collections;
using System.Security.Permissions;
using System.Text;
using Unity;

namespace System.Web
{
	/// <summary>Provides a type-safe way to set the <see cref="P:System.Web.HttpCachePolicy.VaryByParams" /> property.</summary>
	// Token: 0x02000086 RID: 134
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpCacheVaryByParams
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCacheVaryByParams" /> class.</summary>
		// Token: 0x060005F2 RID: 1522 RVA: 0x0000EC16 File Offset: 0x0000CE16
		public HttpCacheVaryByParams()
		{
			this.parms = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000EC30 File Offset: 0x0000CE30
		internal string[] GetParamNames()
		{
			string[] array = new string[this.parms.Count];
			this.parms.Keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000EC64 File Offset: 0x0000CE64
		internal string GetResponseHeaderValue()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this.parms.Keys)
			{
				string text = (string)obj;
				stringBuilder.Append(text);
				stringBuilder.Append("; ");
			}
			if (stringBuilder.Length == 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		/// <summary>Gets or sets a value indicating whether an HTTP response varies by Get or Post parameters. </summary>
		/// <returns>true if HTTP request parameters are ignored; otherwise, false.</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public bool IgnoreParams
		{
			get
			{
				return this.ignore_parms;
			}
			set
			{
				this.ignore_parms = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cache varies according to the specified HTTP request parameter.</summary>
		/// <returns>true if the cache should vary by the specified parameter value.</returns>
		/// <param name="header">The name of the custom parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="header" /> is null. </exception>
		// Token: 0x17000233 RID: 563
		public bool this[string header]
		{
			get
			{
				if (header == null)
				{
					throw new ArgumentNullException();
				}
				return this.parms.Contains(header);
			}
			set
			{
				if (header == null)
				{
					throw new ArgumentNullException();
				}
				this.ignore_parms = false;
				if (value)
				{
					if (!this.parms.Contains(header))
					{
						this.parms.Add(header, true);
						return;
					}
					this.parms.Remove(header);
				}
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string[] GetParams()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetParams(string[] parameters)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000F24 RID: 3876
		private bool ignore_parms;

		// Token: 0x04000F25 RID: 3877
		private Hashtable parms;
	}
}
