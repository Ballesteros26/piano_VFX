using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000398 RID: 920
	internal class SymbolsDictionary
	{
		// Token: 0x06002514 RID: 9492 RVA: 0x000E003E File Offset: 0x000DE23E
		public SymbolsDictionary()
		{
			this.names = new Hashtable();
			this.particles = new ArrayList();
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002515 RID: 9493 RVA: 0x000E0063 File Offset: 0x000DE263
		public int Count
		{
			get
			{
				return this.last + 1;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x000E006D File Offset: 0x000DE26D
		public int CountOfNames
		{
			get
			{
				return this.names.Count;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x000E007A File Offset: 0x000DE27A
		// (set) Token: 0x06002518 RID: 9496 RVA: 0x000E0082 File Offset: 0x000DE282
		public bool IsUpaEnforced
		{
			get
			{
				return this.isUpaEnforced;
			}
			set
			{
				this.isUpaEnforced = value;
			}
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x000E008C File Offset: 0x000DE28C
		public int AddName(XmlQualifiedName name, object particle)
		{
			object obj = this.names[name];
			if (obj != null)
			{
				int num = (int)obj;
				if (this.particles[num] != particle)
				{
					this.isUpaEnforced = false;
				}
				return num;
			}
			this.names.Add(name, this.last);
			this.particles.Add(particle);
			int num2 = this.last;
			this.last = num2 + 1;
			return num2;
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000E0100 File Offset: 0x000DE300
		public void AddNamespaceList(NamespaceList list, object particle, bool allowLocal)
		{
			switch (list.Type)
			{
			case NamespaceList.ListType.Any:
				this.particleLast = particle;
				return;
			case NamespaceList.ListType.Other:
				this.AddWildcard(list.Excluded, null);
				if (!allowLocal)
				{
					this.AddWildcard(string.Empty, null);
					return;
				}
				break;
			case NamespaceList.ListType.Set:
				foreach (object obj in list.Enumerate)
				{
					string text = (string)obj;
					this.AddWildcard(text, particle);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x000E019C File Offset: 0x000DE39C
		private void AddWildcard(string wildcard, object particle)
		{
			if (this.wildcards == null)
			{
				this.wildcards = new Hashtable();
			}
			object obj = this.wildcards[wildcard];
			if (obj == null)
			{
				this.wildcards.Add(wildcard, this.last);
				this.particles.Add(particle);
				this.last++;
				return;
			}
			if (particle != null)
			{
				this.particles[(int)obj] = particle;
			}
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000E0214 File Offset: 0x000DE414
		public ICollection GetNamespaceListSymbols(NamespaceList list)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.names.Keys)
			{
				XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
				if (xmlQualifiedName != XmlQualifiedName.Empty && list.Allows(xmlQualifiedName))
				{
					arrayList.Add(this.names[xmlQualifiedName]);
				}
			}
			if (this.wildcards != null)
			{
				foreach (object obj2 in this.wildcards.Keys)
				{
					string text = (string)obj2;
					if (list.Allows(text))
					{
						arrayList.Add(this.wildcards[text]);
					}
				}
			}
			if (list.Type == NamespaceList.ListType.Any || list.Type == NamespaceList.ListType.Other)
			{
				arrayList.Add(this.last);
			}
			return arrayList;
		}

		// Token: 0x1700076B RID: 1899
		public int this[XmlQualifiedName name]
		{
			get
			{
				object obj = this.names[name];
				if (obj != null)
				{
					return (int)obj;
				}
				if (this.wildcards != null)
				{
					obj = this.wildcards[name.Namespace];
					if (obj != null)
					{
						return (int)obj;
					}
				}
				return this.last;
			}
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000E037A File Offset: 0x000DE57A
		public bool Exists(XmlQualifiedName name)
		{
			return this.names[name] != null;
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x000E038D File Offset: 0x000DE58D
		public object GetParticle(int symbol)
		{
			if (symbol != this.last)
			{
				return this.particles[symbol];
			}
			return this.particleLast;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000E03AC File Offset: 0x000DE5AC
		public string NameOf(int symbol)
		{
			foreach (object obj in this.names)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if ((int)dictionaryEntry.Value == symbol)
				{
					return ((XmlQualifiedName)dictionaryEntry.Key).ToString();
				}
			}
			if (this.wildcards != null)
			{
				foreach (object obj2 in this.wildcards)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if ((int)dictionaryEntry2.Value == symbol)
					{
						return (string)dictionaryEntry2.Key + ":*";
					}
				}
			}
			return "##other:*";
		}

		// Token: 0x04001922 RID: 6434
		private int last;

		// Token: 0x04001923 RID: 6435
		private Hashtable names;

		// Token: 0x04001924 RID: 6436
		private Hashtable wildcards;

		// Token: 0x04001925 RID: 6437
		private ArrayList particles;

		// Token: 0x04001926 RID: 6438
		private object particleLast;

		// Token: 0x04001927 RID: 6439
		private bool isUpaEnforced = true;
	}
}
