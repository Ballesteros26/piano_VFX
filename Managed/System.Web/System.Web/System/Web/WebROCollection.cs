using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000EC RID: 236
	internal class WebROCollection : NameValueCollection
	{
		// Token: 0x06000CA0 RID: 3232 RVA: 0x00022205 File Offset: 0x00020405
		public WebROCollection()
			: base(SecureHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant)
		{
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00022217 File Offset: 0x00020417
		public bool GotID
		{
			get
			{
				return this.got_id;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0002221F File Offset: 0x0002041F
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x00022227 File Offset: 0x00020427
		public int ID
		{
			get
			{
				return this.id;
			}
			set
			{
				this.got_id = true;
				this.id = value;
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00022237 File Offset: 0x00020437
		public void Protect()
		{
			base.IsReadOnly = true;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00022240 File Offset: 0x00020440
		public void Unprotect()
		{
			base.IsReadOnly = false;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0002224C File Offset: 0x0002044C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in this.AllKeys)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append('&');
				}
				if (text != null && text.Length > 0)
				{
					stringBuilder.Append(text);
					stringBuilder.Append('=');
				}
				stringBuilder.Append(this.Get(text));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001118 RID: 4376
		private bool got_id;

		// Token: 0x04001119 RID: 4377
		private int id;
	}
}
