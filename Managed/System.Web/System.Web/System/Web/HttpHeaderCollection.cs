using System;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000097 RID: 151
	internal sealed class HttpHeaderCollection : NameValueCollection
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x000101F3 File Offset: 0x0000E3F3
		public HttpHeaderCollection()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00011238 File Offset: 0x0000F438
		private bool HeaderCheckingEnabled
		{
			get
			{
				if (this.headerCheckingEnabled == null)
				{
					this.headerCheckingEnabled = new bool?(HttpRuntime.Section.EnableHeaderChecking);
				}
				return this.headerCheckingEnabled.Value;
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00011267 File Offset: 0x0000F467
		public override void Add(string name, string value)
		{
			this.EncodeAndSetHeader(name, value, false);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00011272 File Offset: 0x0000F472
		public override void Set(string name, string value)
		{
			this.EncodeAndSetHeader(name, value, true);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00011280 File Offset: 0x0000F480
		private void EncodeAndSetHeader(string name, string value, bool replaceExisting)
		{
			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
			{
				return;
			}
			string text;
			string text2;
			if (this.HeaderCheckingEnabled)
			{
				HttpEncoder.Current.HeaderNameValueEncode(name, value, out text, out text2);
			}
			else
			{
				text = name;
				text2 = value;
			}
			if (replaceExisting)
			{
				base.Set(text, text2);
				return;
			}
			base.Add(text, text2);
		}

		// Token: 0x04000F67 RID: 3943
		private bool? headerCheckingEnabled;
	}
}
