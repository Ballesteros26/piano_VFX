using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml
{
	/// <summary>Resolves, adds, and removes namespaces to a collection and provides scope management for these namespaces. </summary>
	// Token: 0x0200029F RID: 671
	public class XmlNamespaceManager : IXmlNamespaceResolver, IEnumerable
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0008F5D3 File Offset: 0x0008D7D3
		internal static IXmlNamespaceResolver EmptyResolver
		{
			get
			{
				if (XmlNamespaceManager.s_EmptyResolver == null)
				{
					XmlNamespaceManager.s_EmptyResolver = new XmlNamespaceManager(new NameTable());
				}
				return XmlNamespaceManager.s_EmptyResolver;
			}
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000020FD File Offset: 0x000002FD
		internal XmlNamespaceManager()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlNamespaceManager" /> class with the specified <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="nameTable">The <see cref="T:System.Xml.XmlNameTable" /> to use. </param>
		/// <exception cref="T:System.NullReferenceException">null is passed to the constructor </exception>
		// Token: 0x060018DD RID: 6365 RVA: 0x0008F5F8 File Offset: 0x0008D7F8
		public XmlNamespaceManager(XmlNameTable nameTable)
		{
			this.nameTable = nameTable;
			this.xml = nameTable.Add("xml");
			this.xmlNs = nameTable.Add("xmlns");
			this.nsdecls = new XmlNamespaceManager.NamespaceDeclaration[8];
			string text = nameTable.Add(string.Empty);
			this.nsdecls[0].Set(text, text, -1, -1);
			this.nsdecls[1].Set(this.xmlNs, nameTable.Add("http://www.w3.org/2000/xmlns/"), -1, -1);
			this.nsdecls[2].Set(this.xml, nameTable.Add("http://www.w3.org/XML/1998/namespace"), 0, -1);
			this.lastDecl = 2;
			this.scopeId = 1;
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this object.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNameTable" /> used by this object.</returns>
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x0008F6B7 File Offset: 0x0008D8B7
		public virtual XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		/// <summary>Gets the namespace URI for the default namespace.</summary>
		/// <returns>Returns the namespace URI for the default namespace, or String.Empty if there is no default namespace.</returns>
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x0008F6C0 File Offset: 0x0008D8C0
		public virtual string DefaultNamespace
		{
			get
			{
				string text = this.LookupNamespace(string.Empty);
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
		}

		/// <summary>Pushes a namespace scope onto the stack.</summary>
		// Token: 0x060018E0 RID: 6368 RVA: 0x0008F6E3 File Offset: 0x0008D8E3
		public virtual void PushScope()
		{
			this.scopeId++;
		}

		/// <summary>Pops a namespace scope off the stack.</summary>
		/// <returns>true if there are namespace scopes left on the stack; false if there are no more namespaces to pop.</returns>
		// Token: 0x060018E1 RID: 6369 RVA: 0x0008F6F4 File Offset: 0x0008D8F4
		public virtual bool PopScope()
		{
			int num = this.lastDecl;
			if (this.scopeId == 1)
			{
				return false;
			}
			while (this.nsdecls[num].scopeId == this.scopeId)
			{
				if (this.useHashtable)
				{
					this.hashTable[this.nsdecls[num].prefix] = this.nsdecls[num].previousNsIndex;
				}
				num--;
			}
			this.lastDecl = num;
			this.scopeId--;
			return true;
		}

		/// <summary>Adds the given namespace to the collection.</summary>
		/// <param name="prefix">The prefix to associate with the namespace being added. Use String.Empty to add a default namespace.Note   If the <see cref="T:System.Xml.XmlNamespaceManager" /> will be used for resolving namespaces in an XML Path Language (XPath) expression, a prefix must be specified. If an XPath expression does not include a prefix, it is assumed that the namespace Uniform Resource Identifier (URI) is the empty namespace. For more information about XPath expressions and the <see cref="T:System.Xml.XmlNamespaceManager" />, refer to the <see cref="M:System.Xml.XmlNode.SelectNodes(System.String)" /> and <see cref="M:System.Xml.XPath.XPathExpression.SetContext(System.Xml.XmlNamespaceManager)" /> methods.</param>
		/// <param name="uri">The namespace to add. </param>
		/// <exception cref="T:System.ArgumentException">The value for <paramref name="prefix" /> is "xml" or "xmlns". </exception>
		/// <exception cref="T:System.ArgumentNullException">The value for <paramref name="prefix" /> or <paramref name="uri" /> is null. </exception>
		// Token: 0x060018E2 RID: 6370 RVA: 0x0008F77C File Offset: 0x0008D97C
		public virtual void AddNamespace(string prefix, string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			prefix = this.nameTable.Add(prefix);
			uri = this.nameTable.Add(uri);
			if (Ref.Equal(this.xml, prefix) && !uri.Equals("http://www.w3.org/XML/1998/namespace"))
			{
				throw new ArgumentException(Res.GetString("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\"."));
			}
			if (Ref.Equal(this.xmlNs, prefix))
			{
				throw new ArgumentException(Res.GetString("Prefix \"xmlns\" is reserved for use by XML."));
			}
			int num = this.LookupNamespaceDecl(prefix);
			int num2 = -1;
			if (num != -1)
			{
				if (this.nsdecls[num].scopeId == this.scopeId)
				{
					this.nsdecls[num].uri = uri;
					return;
				}
				num2 = num;
			}
			if (this.lastDecl == this.nsdecls.Length - 1)
			{
				XmlNamespaceManager.NamespaceDeclaration[] array = new XmlNamespaceManager.NamespaceDeclaration[this.nsdecls.Length * 2];
				Array.Copy(this.nsdecls, 0, array, 0, this.nsdecls.Length);
				this.nsdecls = array;
			}
			XmlNamespaceManager.NamespaceDeclaration[] array2 = this.nsdecls;
			int num3 = this.lastDecl + 1;
			this.lastDecl = num3;
			array2[num3].Set(prefix, uri, this.scopeId, num2);
			if (this.useHashtable)
			{
				this.hashTable[prefix] = this.lastDecl;
				return;
			}
			if (this.lastDecl >= 16)
			{
				this.hashTable = new Dictionary<string, int>(this.lastDecl);
				for (int i = 0; i <= this.lastDecl; i++)
				{
					this.hashTable[this.nsdecls[i].prefix] = i;
				}
				this.useHashtable = true;
			}
		}

		/// <summary>Removes the given namespace for the given prefix.</summary>
		/// <param name="prefix">The prefix for the namespace </param>
		/// <param name="uri">The namespace to remove for the given prefix. The namespace removed is from the current namespace scope. Namespaces outside the current scope are ignored. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="prefix" /> or <paramref name="uri" /> is null. </exception>
		// Token: 0x060018E3 RID: 6371 RVA: 0x0008F920 File Offset: 0x0008DB20
		public virtual void RemoveNamespace(string prefix, string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			for (int num = this.LookupNamespaceDecl(prefix); num != -1; num = this.nsdecls[num].previousNsIndex)
			{
				if (string.Equals(this.nsdecls[num].uri, uri) && this.nsdecls[num].scopeId == this.scopeId)
				{
					this.nsdecls[num].uri = null;
				}
			}
		}

		/// <summary>Returns an enumerator to use to iterate through the namespaces in the <see cref="T:System.Xml.XmlNamespaceManager" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> containing the prefixes stored by the <see cref="T:System.Xml.XmlNamespaceManager" />.</returns>
		// Token: 0x060018E4 RID: 6372 RVA: 0x0008F9B0 File Offset: 0x0008DBB0
		public virtual IEnumerator GetEnumerator()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(this.lastDecl + 1);
			for (int i = 0; i <= this.lastDecl; i++)
			{
				if (this.nsdecls[i].uri != null)
				{
					dictionary[this.nsdecls[i].prefix] = this.nsdecls[i].prefix;
				}
			}
			return dictionary.Keys.GetEnumerator();
		}

		/// <summary>Gets a collection of namespace names keyed by prefix which can be used to enumerate the namespaces currently in scope.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> object containing a collection of namespace and prefix pairs currently in scope.</returns>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value that specifies the type of namespace nodes to return.</param>
		// Token: 0x060018E5 RID: 6373 RVA: 0x0008FA28 File Offset: 0x0008DC28
		public virtual IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			int i = 0;
			switch (scope)
			{
			case XmlNamespaceScope.All:
				i = 2;
				break;
			case XmlNamespaceScope.ExcludeXml:
				i = 3;
				break;
			case XmlNamespaceScope.Local:
				i = this.lastDecl;
				while (this.nsdecls[i].scopeId == this.scopeId)
				{
					i--;
				}
				i++;
				break;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>(this.lastDecl - i + 1);
			while (i <= this.lastDecl)
			{
				string prefix = this.nsdecls[i].prefix;
				string uri = this.nsdecls[i].uri;
				if (uri != null)
				{
					if (uri.Length > 0 || prefix.Length > 0 || scope == XmlNamespaceScope.Local)
					{
						dictionary[prefix] = uri;
					}
					else
					{
						dictionary.Remove(prefix);
					}
				}
				i++;
			}
			return dictionary;
		}

		/// <summary>Gets the namespace URI for the specified prefix.</summary>
		/// <returns>Returns the namespace URI for <paramref name="prefix" /> or null if there is no mapped namespace. The returned string is atomized.For more information on atomized strings, see <see cref="T:System.Xml.XmlNameTable" />.</returns>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass String.Empty. </param>
		// Token: 0x060018E6 RID: 6374 RVA: 0x0008FAEC File Offset: 0x0008DCEC
		public virtual string LookupNamespace(string prefix)
		{
			int num = this.LookupNamespaceDecl(prefix);
			if (num != -1)
			{
				return this.nsdecls[num].uri;
			}
			return null;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0008FB18 File Offset: 0x0008DD18
		private int LookupNamespaceDecl(string prefix)
		{
			if (!this.useHashtable)
			{
				for (int i = this.lastDecl; i >= 0; i--)
				{
					if (this.nsdecls[i].prefix == prefix && this.nsdecls[i].uri != null)
					{
						return i;
					}
				}
				for (int j = this.lastDecl; j >= 0; j--)
				{
					if (string.Equals(this.nsdecls[j].prefix, prefix) && this.nsdecls[j].uri != null)
					{
						return j;
					}
				}
				return -1;
			}
			int previousNsIndex;
			if (this.hashTable.TryGetValue(prefix, out previousNsIndex))
			{
				while (previousNsIndex != -1 && this.nsdecls[previousNsIndex].uri == null)
				{
					previousNsIndex = this.nsdecls[previousNsIndex].previousNsIndex;
				}
				return previousNsIndex;
			}
			return -1;
		}

		/// <summary>Finds the prefix declared for the given namespace URI.</summary>
		/// <returns>The matching prefix. If there is no mapped prefix, the method returns String.Empty. If a null value is supplied, then null is returned.</returns>
		/// <param name="uri">The namespace to resolve for the prefix. </param>
		// Token: 0x060018E8 RID: 6376 RVA: 0x0008FBE8 File Offset: 0x0008DDE8
		public virtual string LookupPrefix(string uri)
		{
			for (int i = this.lastDecl; i >= 0; i--)
			{
				if (string.Equals(this.nsdecls[i].uri, uri))
				{
					string prefix = this.nsdecls[i].prefix;
					if (string.Equals(this.LookupNamespace(prefix), uri))
					{
						return prefix;
					}
				}
			}
			return null;
		}

		/// <summary>Gets a value indicating whether the supplied prefix has a namespace defined for the current pushed scope.</summary>
		/// <returns>true if there is a namespace defined; otherwise, false.</returns>
		/// <param name="prefix">The prefix of the namespace you want to find. </param>
		// Token: 0x060018E9 RID: 6377 RVA: 0x0008FC44 File Offset: 0x0008DE44
		public virtual bool HasNamespace(string prefix)
		{
			int num = this.lastDecl;
			while (this.nsdecls[num].scopeId == this.scopeId)
			{
				if (string.Equals(this.nsdecls[num].prefix, prefix) && this.nsdecls[num].uri != null)
				{
					return prefix.Length > 0 || this.nsdecls[num].uri.Length > 0;
				}
				num--;
			}
			return false;
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0008FCCC File Offset: 0x0008DECC
		internal bool GetNamespaceDeclaration(int idx, out string prefix, out string uri)
		{
			idx = this.lastDecl - idx;
			if (idx < 0)
			{
				string text;
				uri = (text = null);
				prefix = text;
				return false;
			}
			prefix = this.nsdecls[idx].prefix;
			uri = this.nsdecls[idx].uri;
			return true;
		}

		// Token: 0x04001027 RID: 4135
		private static volatile IXmlNamespaceResolver s_EmptyResolver;

		// Token: 0x04001028 RID: 4136
		private XmlNamespaceManager.NamespaceDeclaration[] nsdecls;

		// Token: 0x04001029 RID: 4137
		private int lastDecl;

		// Token: 0x0400102A RID: 4138
		private XmlNameTable nameTable;

		// Token: 0x0400102B RID: 4139
		private int scopeId;

		// Token: 0x0400102C RID: 4140
		private Dictionary<string, int> hashTable;

		// Token: 0x0400102D RID: 4141
		private bool useHashtable;

		// Token: 0x0400102E RID: 4142
		private string xml;

		// Token: 0x0400102F RID: 4143
		private string xmlNs;

		// Token: 0x04001030 RID: 4144
		private const int MinDeclsCountForHashtable = 16;

		// Token: 0x020002A0 RID: 672
		private struct NamespaceDeclaration
		{
			// Token: 0x060018EB RID: 6379 RVA: 0x0008FD18 File Offset: 0x0008DF18
			public void Set(string prefix, string uri, int scopeId, int previousNsIndex)
			{
				this.prefix = prefix;
				this.uri = uri;
				this.scopeId = scopeId;
				this.previousNsIndex = previousNsIndex;
			}

			// Token: 0x04001031 RID: 4145
			public string prefix;

			// Token: 0x04001032 RID: 4146
			public string uri;

			// Token: 0x04001033 RID: 4147
			public int scopeId;

			// Token: 0x04001034 RID: 4148
			public int previousNsIndex;
		}
	}
}
