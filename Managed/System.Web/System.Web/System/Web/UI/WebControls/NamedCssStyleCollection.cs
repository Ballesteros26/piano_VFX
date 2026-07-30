using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003DD RID: 989
	internal sealed class NamedCssStyleCollection
	{
		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x000710EA File Offset: 0x0006F2EA
		public CssStyleCollection Collection
		{
			get
			{
				if (this.collection == null)
				{
					this.collection = new CssStyleCollection();
				}
				return this.collection;
			}
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x00071105 File Offset: 0x0006F305
		// (set) Token: 0x06002A99 RID: 10905 RVA: 0x0007110D File Offset: 0x0006F30D
		public string Name { get; private set; }

		// Token: 0x06002A9A RID: 10906 RVA: 0x00071116 File Offset: 0x0006F316
		public NamedCssStyleCollection(string name)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			this.Name = name;
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x00071130 File Offset: 0x0006F330
		public NamedCssStyleCollection CopyFrom(CssStyleCollection coll)
		{
			if (coll == null)
			{
				return this;
			}
			CssStyleCollection cssStyleCollection = this.Collection;
			foreach (object obj in coll.Keys)
			{
				string text = (string)obj;
				cssStyleCollection.Add(text, coll[text]);
			}
			return this;
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000711A0 File Offset: 0x0006F3A0
		public NamedCssStyleCollection Add(HtmlTextWriterStyle key, string value)
		{
			this.Collection.Add(key, value);
			return this;
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000711B0 File Offset: 0x0006F3B0
		public NamedCssStyleCollection Add(string key, string value)
		{
			this.Collection.Add(key, value);
			return this;
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000711C0 File Offset: 0x0006F3C0
		public NamedCssStyleCollection Add(Style style)
		{
			if (style != null)
			{
				this.CopyFrom(style.GetStyleAttributes(null));
			}
			return this;
		}

		// Token: 0x04001AE6 RID: 6886
		private CssStyleCollection collection;
	}
}
