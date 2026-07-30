using System;

namespace System.Collections.Specialized
{
	// Token: 0x020006F4 RID: 1780
	internal class CaseSensitiveStringDictionary : StringDictionary
	{
		// Token: 0x17000D73 RID: 3443
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

		// Token: 0x060037CF RID: 14287 RVA: 0x000CD879 File Offset: 0x000CBA79
		public override void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key, value);
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000CD896 File Offset: 0x000CBA96
		public override bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key);
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000CD8B2 File Offset: 0x000CBAB2
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
