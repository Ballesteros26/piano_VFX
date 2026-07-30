using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace System.Web.Configuration
{
	// Token: 0x0200058D RID: 1421
	internal class CapabilitiesResult : HttpBrowserCapabilities
	{
		// Token: 0x06003C14 RID: 15380 RVA: 0x000A0713 File Offset: 0x0009E913
		internal CapabilitiesResult(IDictionary items)
		{
			base.Capabilities = items;
			base.Capabilities["browsers"] = new ArrayList();
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x000A0737 File Offset: 0x0009E937
		internal void AddCapabilities(string name, string value)
		{
			base.Capabilities[name] = value;
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x000A0748 File Offset: 0x0009E948
		internal void AddMatchingBrowserId(string id)
		{
			ArrayList arrayList = base.Capabilities["browsers"] as ArrayList;
			if (arrayList != null && !arrayList.Contains(id))
			{
				arrayList.Add(id);
			}
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x000A0780 File Offset: 0x0009E980
		internal virtual string Replace(string item)
		{
			if (item.IndexOf('$') > -1)
			{
				MatchCollection matchCollection = Regex.Matches(item, "\\$\\{(?'Capability'\\w*)\\}");
				if (matchCollection.Count == 0)
				{
					return item;
				}
				for (int i = 0; i <= matchCollection.Count - 1; i++)
				{
					if (matchCollection[i].Success)
					{
						string text = matchCollection[i].Result("${Capability}");
						item = item.Replace("${" + text + "}", this[text]);
					}
				}
			}
			if (item.IndexOf('%') > -1)
			{
				MatchCollection matchCollection2 = Regex.Matches(item, "\\%\\{(?'Capability'\\w*)\\}");
				if (matchCollection2.Count == 0)
				{
					return item;
				}
				for (int j = 0; j <= matchCollection2.Count - 1; j++)
				{
					if (matchCollection2[j].Success)
					{
						string text2 = matchCollection2[j].Result("${Capability}");
						item = item.Replace("%{" + text2 + "}", this[text2]);
					}
				}
			}
			return item;
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06003C18 RID: 15384 RVA: 0x000A0880 File Offset: 0x0009EA80
		public StringCollection Keys
		{
			get
			{
				string[] array = new string[base.Capabilities.Keys.Count];
				base.Capabilities.Keys.CopyTo(array, 0);
				Array.Sort<string>(array);
				StringCollection stringCollection = new StringCollection();
				stringCollection.AddRange(array);
				return stringCollection;
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06003C19 RID: 15385 RVA: 0x000A08C7 File Offset: 0x0009EAC7
		public string UserAgent
		{
			get
			{
				return this[""];
			}
		}
	}
}
