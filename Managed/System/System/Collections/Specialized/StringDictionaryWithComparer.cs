using System;

namespace System.Collections.Specialized
{
	// Token: 0x02000712 RID: 1810
	[Serializable]
	internal class StringDictionaryWithComparer : StringDictionary
	{
		// Token: 0x06003915 RID: 14613 RVA: 0x000D0D2E File Offset: 0x000CEF2E
		public StringDictionaryWithComparer()
			: this(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000D0D3B File Offset: 0x000CEF3B
		public StringDictionaryWithComparer(IEqualityComparer comparer)
		{
			base.ReplaceHashtable(new Hashtable(comparer));
		}

		// Token: 0x17000DD1 RID: 3537
		public override string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (string)this.contents[key];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.contents[key] = value;
			}
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000CD879 File Offset: 0x000CBA79
		public override void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key, value);
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000CD896 File Offset: 0x000CBA96
		public override bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key);
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000CD8B2 File Offset: 0x000CBAB2
		public override void Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Remove(key);
		}
	}
}
