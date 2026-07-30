using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200041D RID: 1053
	internal class SchemaNamespaceManager : XmlNamespaceManager
	{
		// Token: 0x0600296F RID: 10607 RVA: 0x000FAEF1 File Offset: 0x000F90F1
		public SchemaNamespaceManager(XmlSchemaObject node)
		{
			this.node = node;
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000FAF00 File Offset: 0x000F9100
		public override string LookupNamespace(string prefix)
		{
			if (prefix == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			for (XmlSchemaObject parent = this.node; parent != null; parent = parent.Parent)
			{
				Hashtable namespaces = parent.Namespaces.Namespaces;
				if (namespaces != null && namespaces.Count > 0)
				{
					object obj = namespaces[prefix];
					if (obj != null)
					{
						return (string)obj;
					}
				}
			}
			if (prefix.Length != 0)
			{
				return null;
			}
			return string.Empty;
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000FAF6C File Offset: 0x000F916C
		public override string LookupPrefix(string ns)
		{
			if (ns == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			for (XmlSchemaObject parent = this.node; parent != null; parent = parent.Parent)
			{
				Hashtable namespaces = parent.Namespaces.Namespaces;
				if (namespaces != null && namespaces.Count > 0)
				{
					foreach (object obj in namespaces)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (dictionaryEntry.Value.Equals(ns))
						{
							return (string)dictionaryEntry.Key;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04001C46 RID: 7238
		private XmlSchemaObject node;
	}
}
