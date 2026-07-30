using System;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace System.Web
{
	// Token: 0x0200009C RID: 156
	internal class HttpParamsCollection : WebROCollection
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x0001137D File Offset: 0x0000F57D
		public HttpParamsCollection(NameValueCollection queryString, NameValueCollection form, NameValueCollection serverVariables, HttpCookieCollection cookies)
		{
			this._queryString = queryString;
			this._form = form;
			this._serverVariables = serverVariables;
			this._cookies = cookies;
			this._merged = false;
			base.Protect();
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000113AF File Offset: 0x0000F5AF
		public override string Get(string name)
		{
			this.MergeCollections();
			return base.Get(name);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000113C0 File Offset: 0x0000F5C0
		private void MergeCollections()
		{
			if (this._merged)
			{
				return;
			}
			base.Unprotect();
			base.Add(this._queryString);
			base.Add(this._form);
			base.Add(this._serverVariables);
			for (int i = 0; i < this._cookies.Count; i++)
			{
				HttpCookie httpCookie = this._cookies[i];
				this.Add(httpCookie.Name, httpCookie.Value);
			}
			this._merged = true;
			base.Protect();
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00011442 File Offset: 0x0000F642
		public override string Get(int index)
		{
			this.MergeCollections();
			return base.Get(index);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00011451 File Offset: 0x0000F651
		public override string GetKey(int index)
		{
			this.MergeCollections();
			return base.GetKey(index);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00011460 File Offset: 0x0000F660
		public override string[] GetValues(int index)
		{
			this.MergeCollections();
			return base.GetValues(index);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001146F File Offset: 0x0000F66F
		public override string[] GetValues(string name)
		{
			this.MergeCollections();
			return base.GetValues(name);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00007654 File Offset: 0x00005854
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new SerializationException();
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001147E File Offset: 0x0000F67E
		public override string[] AllKeys
		{
			get
			{
				this.MergeCollections();
				return base.AllKeys;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001148C File Offset: 0x0000F68C
		public override int Count
		{
			get
			{
				this.MergeCollections();
				return base.Count;
			}
		}

		// Token: 0x04000F68 RID: 3944
		private NameValueCollection _queryString;

		// Token: 0x04000F69 RID: 3945
		private NameValueCollection _form;

		// Token: 0x04000F6A RID: 3946
		private NameValueCollection _serverVariables;

		// Token: 0x04000F6B RID: 3947
		private HttpCookieCollection _cookies;

		// Token: 0x04000F6C RID: 3948
		private bool _merged;
	}
}
