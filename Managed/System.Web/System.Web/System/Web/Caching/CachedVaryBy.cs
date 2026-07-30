using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Web.Caching
{
	// Token: 0x0200068C RID: 1676
	[Serializable]
	internal sealed class CachedVaryBy
	{
		// Token: 0x0600476F RID: 18287 RVA: 0x000C8BF0 File Offset: 0x000C6DF0
		internal CachedVaryBy(HttpCachePolicy policy, string key)
		{
			this.prms = policy.VaryByParams.GetParamNames();
			this.headers = policy.VaryByHeaders.GetHeaderNames(policy.OmitVaryStar);
			this.custom = policy.GetVaryByCustom();
			this.key = key;
			this.item_list = new List<string>();
			this.wildCardParams = policy.VaryByParams["*"];
		}

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x000C8C5F File Offset: 0x000C6E5F
		internal List<string> ItemList
		{
			get
			{
				return this.item_list;
			}
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06004771 RID: 18289 RVA: 0x000C8C67 File Offset: 0x000C6E67
		internal string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x000C8C70 File Offset: 0x000C6E70
		internal string CreateKey(string file_path, HttpContext context)
		{
			if (string.IsNullOrEmpty(file_path))
			{
				throw new ArgumentNullException("file_path");
			}
			StringBuilder stringBuilder = new StringBuilder("vbk");
			HttpRequest httpRequest = ((context != null) ? context.Request : null);
			stringBuilder.Append(file_path);
			if (httpRequest == null)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append(httpRequest.HttpMethod);
			if (this.wildCardParams)
			{
				stringBuilder.Append("WQ");
				foreach (object obj in httpRequest.QueryString)
				{
					string text = (string)obj;
					if (text != null)
					{
						stringBuilder.Append('N');
						stringBuilder.Append(text.ToLowerInvariant());
						string text2 = httpRequest.QueryString[text];
						if (!string.IsNullOrEmpty(text2))
						{
							stringBuilder.Append('V');
							stringBuilder.Append(text2);
						}
					}
				}
				stringBuilder.Append('F');
				using (IEnumerator enumerator = httpRequest.Form.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						string text3 = (string)obj2;
						if (text3 != null)
						{
							stringBuilder.Append('N');
							stringBuilder.Append(text3.ToLowerInvariant());
							string text2 = httpRequest.Form[text3];
							if (!string.IsNullOrEmpty(text2))
							{
								stringBuilder.Append('V');
								stringBuilder.Append(text2);
							}
						}
					}
					goto IL_0256;
				}
			}
			if (this.prms != null)
			{
				StringBuilder stringBuilder2 = null;
				stringBuilder.Append("SQ");
				for (int i = 0; i < this.prms.Length; i++)
				{
					string text4 = this.prms[i];
					if (!string.IsNullOrEmpty(text4))
					{
						string text2 = httpRequest.QueryString[text4];
						if (text2 != null)
						{
							stringBuilder.Append('N');
							stringBuilder.Append(text4.ToLowerInvariant());
							if (text2.Length > 0)
							{
								stringBuilder.Append('V');
								stringBuilder.Append(text2);
							}
						}
						text2 = httpRequest.Form[text4];
						if (text2 != null)
						{
							if (stringBuilder2 == null)
							{
								stringBuilder2 = new StringBuilder(70);
							}
							stringBuilder.Append('N');
							stringBuilder.Append(text4.ToLowerInvariant());
							if (text2.Length > 0)
							{
								stringBuilder.Append('V');
								stringBuilder.Append(text2);
							}
						}
					}
				}
				if (stringBuilder2 != null)
				{
					stringBuilder.Append(stringBuilder2.ToString());
				}
			}
			IL_0256:
			if (this.headers != null)
			{
				stringBuilder.Append('H');
				for (int j = 0; j < this.headers.Length; j++)
				{
					stringBuilder.Append('N');
					string text4 = this.headers[j];
					stringBuilder.Append(text4.ToLowerInvariant());
					string text2 = httpRequest.Headers[text4];
					if (!string.IsNullOrEmpty(text2))
					{
						stringBuilder.Append('V');
						stringBuilder.Append(text2);
					}
				}
			}
			if (this.custom != null)
			{
				stringBuilder.Append('C');
				string varyByCustomString = context.ApplicationInstance.GetVaryByCustomString(context, this.custom);
				stringBuilder.Append('N');
				stringBuilder.Append(this.custom);
				stringBuilder.Append('V');
				stringBuilder.Append((varyByCustomString != null) ? varyByCustomString : "__null__");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040025AB RID: 9643
		private string[] prms;

		// Token: 0x040025AC RID: 9644
		private string[] headers;

		// Token: 0x040025AD RID: 9645
		private string custom;

		// Token: 0x040025AE RID: 9646
		private string key;

		// Token: 0x040025AF RID: 9647
		private List<string> item_list;

		// Token: 0x040025B0 RID: 9648
		private bool wildCardParams;
	}
}
