using System;
using System.Collections;
using System.Globalization;
using System.Resources;

namespace System.Web.Compilation
{
	// Token: 0x0200060B RID: 1547
	internal class DefaultImplicitResourceProvider : IImplicitResourceProvider
	{
		// Token: 0x060042B4 RID: 17076 RVA: 0x000AFBD2 File Offset: 0x000ADDD2
		internal DefaultImplicitResourceProvider(IResourceProvider resourceProvider)
		{
			this._resourceProvider = resourceProvider;
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x000AFBE4 File Offset: 0x000ADDE4
		public virtual object GetObject(ImplicitResourceKey entry, CultureInfo culture)
		{
			string text = DefaultImplicitResourceProvider.ConstructFullKey(entry);
			return this._resourceProvider.GetObject(text, culture);
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x000AFC05 File Offset: 0x000ADE05
		public virtual ICollection GetImplicitResourceKeys(string keyPrefix)
		{
			this.EnsureGetPageResources();
			if (this._implicitResources == null)
			{
				return null;
			}
			return (ICollection)this._implicitResources[keyPrefix];
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x000AFC28 File Offset: 0x000ADE28
		internal void EnsureGetPageResources()
		{
			if (this._attemptedGetPageResources)
			{
				return;
			}
			this._attemptedGetPageResources = true;
			IResourceReader resourceReader = this._resourceProvider.ResourceReader;
			if (resourceReader == null)
			{
				return;
			}
			this._implicitResources = new Hashtable(StringComparer.OrdinalIgnoreCase);
			foreach (object obj in resourceReader)
			{
				ImplicitResourceKey implicitResourceKey = DefaultImplicitResourceProvider.ParseFullKey((string)((DictionaryEntry)obj).Key);
				if (implicitResourceKey != null)
				{
					ArrayList arrayList = (ArrayList)this._implicitResources[implicitResourceKey.KeyPrefix];
					if (arrayList == null)
					{
						arrayList = new ArrayList();
						this._implicitResources[implicitResourceKey.KeyPrefix] = arrayList;
					}
					arrayList.Add(implicitResourceKey);
				}
			}
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x000AFCFC File Offset: 0x000ADEFC
		private static ImplicitResourceKey ParseFullKey(string key)
		{
			string text = string.Empty;
			if (key.IndexOf(':') > 0)
			{
				string[] array = key.Split(new char[] { ':' });
				if (array.Length > 2)
				{
					return null;
				}
				text = array[0];
				key = array[1];
			}
			int num = key.IndexOf('.');
			if (num <= 0)
			{
				return null;
			}
			string text2 = key.Substring(0, num);
			string text3 = key.Substring(num + 1);
			return new ImplicitResourceKey
			{
				Filter = text,
				KeyPrefix = text2,
				Property = text3
			};
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x000AFD80 File Offset: 0x000ADF80
		private static string ConstructFullKey(ImplicitResourceKey entry)
		{
			string text = entry.KeyPrefix + "." + entry.Property;
			if (entry.Filter.Length > 0)
			{
				text = entry.Filter + ":" + text;
			}
			return text;
		}

		// Token: 0x040023BB RID: 9147
		private IResourceProvider _resourceProvider;

		// Token: 0x040023BC RID: 9148
		private IDictionary _implicitResources;

		// Token: 0x040023BD RID: 9149
		private bool _attemptedGetPageResources;
	}
}
