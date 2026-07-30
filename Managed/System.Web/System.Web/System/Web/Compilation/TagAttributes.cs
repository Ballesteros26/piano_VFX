using System;
using System.Collections;
using System.Text;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200066C RID: 1644
	internal sealed class TagAttributes
	{
		// Token: 0x06004646 RID: 17990 RVA: 0x000C1734 File Offset: 0x000BF934
		public TagAttributes()
		{
			this.got_hashed = false;
			this.keys = new ArrayList();
			this.values = new ArrayList();
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x000C175C File Offset: 0x000BF95C
		private void MakeHash()
		{
			this.atts_hash = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
			for (int i = 0; i < this.keys.Count; i++)
			{
				this.CheckServerKey(this.keys[i]);
				this.atts_hash.Add(this.keys[i], this.values[i]);
			}
			this.got_hashed = true;
			this.keys = null;
			this.values = null;
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x000C17D9 File Offset: 0x000BF9D9
		public bool IsRunAtServer()
		{
			return this.got_hashed;
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x000C17E4 File Offset: 0x000BF9E4
		public void Add(object key, object value)
		{
			if (key != null && value != null && string.Compare((string)key, "runat", true, Helpers.InvariantCulture) == 0)
			{
				if (string.Compare((string)value, "server", true) != 0)
				{
					throw new HttpException("runat attribute must have a 'server' value");
				}
				if (this.got_hashed)
				{
					return;
				}
				this.MakeHash();
			}
			if (value != null)
			{
				value = HttpUtility.HtmlDecode(value.ToString());
			}
			if (!this.got_hashed)
			{
				this.keys.Add(key);
				this.values.Add(value);
				return;
			}
			this.CheckServerKey(key);
			if (this.atts_hash.ContainsKey(key))
			{
				throw new HttpException("Tag contains duplicated '" + key + "' attributes.");
			}
			this.atts_hash.Add(key, value);
		}

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x0600464A RID: 17994 RVA: 0x000C18A8 File Offset: 0x000BFAA8
		public ICollection Keys
		{
			get
			{
				if (!this.got_hashed)
				{
					return this.keys;
				}
				return this.atts_hash.Keys;
			}
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x0600464B RID: 17995 RVA: 0x000C18D4 File Offset: 0x000BFAD4
		public ICollection Values
		{
			get
			{
				if (!this.got_hashed)
				{
					return this.values;
				}
				return this.atts_hash.Values;
			}
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x000C1900 File Offset: 0x000BFB00
		private int CaseInsensitiveSearch(string key)
		{
			for (int i = 0; i < this.keys.Count; i++)
			{
				if (string.Compare((string)this.keys[i], key, true, Helpers.InvariantCulture) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x170015E5 RID: 5605
		public object this[object key]
		{
			get
			{
				if (this.got_hashed)
				{
					return this.atts_hash[key];
				}
				int num = this.CaseInsensitiveSearch((string)key);
				if (num == -1)
				{
					return null;
				}
				return this.values[num];
			}
			set
			{
				if (this.got_hashed)
				{
					this.CheckServerKey(key);
					this.atts_hash[key] = value;
					return;
				}
				int num = this.CaseInsensitiveSearch((string)key);
				this.keys[num] = value;
			}
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x0600464F RID: 17999 RVA: 0x000C19D0 File Offset: 0x000BFBD0
		public int Count
		{
			get
			{
				if (!this.got_hashed)
				{
					return this.keys.Count;
				}
				return this.atts_hash.Count;
			}
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x000C19F1 File Offset: 0x000BFBF1
		public bool IsDataBound(string att)
		{
			return att != null && this.got_hashed && StrUtils.StartsWith(att, "<%#") && StrUtils.EndsWith(att, "%>");
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x000C1A1C File Offset: 0x000BFC1C
		public IDictionary GetDictionary(string key)
		{
			if (this.got_hashed)
			{
				return this.atts_hash;
			}
			if (this.tmp_hash == null)
			{
				this.tmp_hash = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
			}
			this.tmp_hash.Clear();
			for (int i = this.keys.Count - 1; i >= 0; i--)
			{
				if (key == null || string.Compare(key, (string)this.keys[i], true, Helpers.InvariantCulture) == 0)
				{
					this.tmp_hash[this.keys[i]] = this.values[i];
				}
			}
			return this.tmp_hash;
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x000C1AC0 File Offset: 0x000BFCC0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("TagAttributes {");
			foreach (object obj in this.Keys)
			{
				string text = (string)obj;
				stringBuilder.Append('[');
				stringBuilder.Append(text);
				string text2 = this[text] as string;
				if (text2 != null)
				{
					stringBuilder.AppendFormat("=\"{0}\"", text2);
				}
				stringBuilder.Append("] ");
			}
			if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == ' ')
			{
				StringBuilder stringBuilder2 = stringBuilder;
				int length = stringBuilder2.Length;
				stringBuilder2.Length = length - 1;
			}
			stringBuilder.Append('}');
			if (this.IsRunAtServer())
			{
				stringBuilder.Append(" @Server");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x000C1BAC File Offset: 0x000BFDAC
		private void CheckServerKey(object key)
		{
			if (key == null || ((string)key).Length == 0)
			{
				throw new HttpException("The server tag is not well formed.");
			}
		}

		// Token: 0x04002531 RID: 9521
		private Hashtable atts_hash;

		// Token: 0x04002532 RID: 9522
		private Hashtable tmp_hash;

		// Token: 0x04002533 RID: 9523
		private ArrayList keys;

		// Token: 0x04002534 RID: 9524
		private ArrayList values;

		// Token: 0x04002535 RID: 9525
		private bool got_hashed;
	}
}
