using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Contains the XML namespaces and prefixes that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> uses to generate qualified names in an XML-document instance.</summary>
	// Token: 0x02000366 RID: 870
	public class XmlSerializerNamespaces
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class.</summary>
		// Token: 0x0600239B RID: 9115 RVA: 0x000020FD File Offset: 0x000002FD
		public XmlSerializerNamespaces()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class, using the specified instance of XmlSerializerNamespaces containing the collection of prefix and namespace pairs.</summary>
		/// <param name="namespaces">An instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" />containing the namespace and prefix pairs. </param>
		// Token: 0x0600239C RID: 9116 RVA: 0x000DBE86 File Offset: 0x000DA086
		public XmlSerializerNamespaces(XmlSerializerNamespaces namespaces)
		{
			this.namespaces = (Hashtable)namespaces.Namespaces.Clone();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class.</summary>
		/// <param name="namespaces">An array of <see cref="T:System.Xml.XmlQualifiedName" /> objects. </param>
		// Token: 0x0600239D RID: 9117 RVA: 0x000DBEA4 File Offset: 0x000DA0A4
		public XmlSerializerNamespaces(XmlQualifiedName[] namespaces)
		{
			foreach (XmlQualifiedName xmlQualifiedName in namespaces)
			{
				this.Add(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
			}
		}

		/// <summary>Adds a prefix and namespace pair to an <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> object.</summary>
		/// <param name="prefix">The prefix associated with an XML namespace. </param>
		/// <param name="ns">An XML namespace. </param>
		// Token: 0x0600239E RID: 9118 RVA: 0x000DBEDB File Offset: 0x000DA0DB
		public void Add(string prefix, string ns)
		{
			if (prefix != null && prefix.Length > 0)
			{
				XmlConvert.VerifyNCName(prefix);
			}
			if (ns != null && ns.Length > 0)
			{
				XmlConvert.ToUri(ns);
			}
			this.AddInternal(prefix, ns);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000DBF0B File Offset: 0x000DA10B
		internal void AddInternal(string prefix, string ns)
		{
			this.Namespaces[prefix] = ns;
		}

		/// <summary>Gets the array of prefix and namespace pairs in an <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> object.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XmlQualifiedName" /> objects that are used as qualified names in an XML document.</returns>
		// Token: 0x060023A0 RID: 9120 RVA: 0x000DBF1A File Offset: 0x000DA11A
		public XmlQualifiedName[] ToArray()
		{
			if (this.NamespaceList == null)
			{
				return new XmlQualifiedName[0];
			}
			return (XmlQualifiedName[])this.NamespaceList.ToArray(typeof(XmlQualifiedName));
		}

		/// <summary>Gets the number of prefix and namespace pairs in the collection.</summary>
		/// <returns>The number of prefix and namespace pairs in the collection.</returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x000DBF45 File Offset: 0x000DA145
		public int Count
		{
			get
			{
				return this.Namespaces.Count;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x000DBF54 File Offset: 0x000DA154
		internal ArrayList NamespaceList
		{
			get
			{
				if (this.namespaces == null || this.namespaces.Count == 0)
				{
					return null;
				}
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.Namespaces.Keys)
				{
					string text = (string)obj;
					arrayList.Add(new XmlQualifiedName(text, (string)this.Namespaces[text]));
				}
				return arrayList;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x000DBFE8 File Offset: 0x000DA1E8
		// (set) Token: 0x060023A4 RID: 9124 RVA: 0x000DC003 File Offset: 0x000DA203
		internal Hashtable Namespaces
		{
			get
			{
				if (this.namespaces == null)
				{
					this.namespaces = new Hashtable();
				}
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000DC00C File Offset: 0x000DA20C
		internal string LookupPrefix(string ns)
		{
			if (string.IsNullOrEmpty(ns))
			{
				return null;
			}
			if (this.namespaces == null || this.namespaces.Count == 0)
			{
				return null;
			}
			foreach (object obj in this.namespaces.Keys)
			{
				string text = (string)obj;
				if (!string.IsNullOrEmpty(text) && (string)this.namespaces[text] == ns)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x04001868 RID: 6248
		private Hashtable namespaces;
	}
}
