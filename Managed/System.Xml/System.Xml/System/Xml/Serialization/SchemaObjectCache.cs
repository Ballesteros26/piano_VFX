using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002DE RID: 734
	internal class SchemaObjectCache
	{
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x00098E68 File Offset: 0x00097068
		private Hashtable Graph
		{
			get
			{
				if (this.graph == null)
				{
					this.graph = new Hashtable();
				}
				return this.graph;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x00098E83 File Offset: 0x00097083
		private Hashtable Hash
		{
			get
			{
				if (this.hash == null)
				{
					this.hash = new Hashtable();
				}
				return this.hash;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x00098E9E File Offset: 0x0009709E
		private Hashtable ObjectCache
		{
			get
			{
				if (this.objectCache == null)
				{
					this.objectCache = new Hashtable();
				}
				return this.objectCache;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x00098EB9 File Offset: 0x000970B9
		internal StringCollection Warnings
		{
			get
			{
				if (this.warnings == null)
				{
					this.warnings = new StringCollection();
				}
				return this.warnings;
			}
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x00098ED4 File Offset: 0x000970D4
		internal XmlSchemaObject AddItem(XmlSchemaObject item, XmlQualifiedName qname, XmlSchemas schemas)
		{
			if (item == null)
			{
				return null;
			}
			if (qname == null || qname.IsEmpty)
			{
				return null;
			}
			string text = item.GetType().Name + ":" + qname.ToString();
			ArrayList arrayList = (ArrayList)this.ObjectCache[text];
			if (arrayList == null)
			{
				arrayList = new ArrayList();
				this.ObjectCache[text] = arrayList;
			}
			for (int i = 0; i < arrayList.Count; i++)
			{
				XmlSchemaObject xmlSchemaObject = (XmlSchemaObject)arrayList[i];
				if (xmlSchemaObject == item)
				{
					return xmlSchemaObject;
				}
				if (this.Match(xmlSchemaObject, item, true))
				{
					return xmlSchemaObject;
				}
				this.Warnings.Add(Res.GetString("Warning: Cannot share {0} named '{1}' from '{2}' namespace. Several mismatched schema declarations were found.", new object[]
				{
					item.GetType().Name,
					qname.Name,
					qname.Namespace
				}));
				this.Warnings.Add("DEBUG:Cached item key:\r\n" + (string)this.looks[xmlSchemaObject] + "\r\nnew item key:\r\n" + (string)this.looks[item]);
			}
			arrayList.Add(item);
			return item;
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00098FF8 File Offset: 0x000971F8
		internal bool Match(XmlSchemaObject o1, XmlSchemaObject o2, bool shareTypes)
		{
			if (o1 == o2)
			{
				return true;
			}
			if (o1.GetType() != o2.GetType())
			{
				return false;
			}
			if (this.Hash[o1] == null)
			{
				this.Hash[o1] = this.GetHash(o1);
			}
			int num = (int)this.Hash[o1];
			int num2 = this.GetHash(o2);
			return num == num2 && (!shareTypes || this.CompositeHash(o1, num) == this.CompositeHash(o2, num2));
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00099080 File Offset: 0x00097280
		private ArrayList GetDependencies(XmlSchemaObject o, ArrayList deps, Hashtable refs)
		{
			if (refs[o] == null)
			{
				refs[o] = o;
				deps.Add(o);
				ArrayList arrayList = this.Graph[o] as ArrayList;
				if (arrayList != null)
				{
					for (int i = 0; i < arrayList.Count; i++)
					{
						this.GetDependencies((XmlSchemaObject)arrayList[i], deps, refs);
					}
				}
			}
			return deps;
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x000990E4 File Offset: 0x000972E4
		private int CompositeHash(XmlSchemaObject o, int hash)
		{
			ArrayList dependencies = this.GetDependencies(o, new ArrayList(), new Hashtable());
			double num = 0.0;
			for (int i = 0; i < dependencies.Count; i++)
			{
				object obj = this.Hash[dependencies[i]];
				if (obj is int)
				{
					num += (double)((int)obj / dependencies.Count);
				}
			}
			return (int)num;
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x0009914C File Offset: 0x0009734C
		internal void GenerateSchemaGraph(XmlSchemas schemas)
		{
			ArrayList items = new SchemaGraph(this.Graph, schemas).GetItems();
			for (int i = 0; i < items.Count; i++)
			{
				this.GetHash((XmlSchemaObject)items[i]);
			}
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00099190 File Offset: 0x00097390
		private int GetHash(XmlSchemaObject o)
		{
			object obj = this.Hash[o];
			if (obj != null && !(obj is XmlSchemaObject))
			{
				return (int)obj;
			}
			string text = this.ToString(o, new SchemaObjectWriter());
			this.looks[o] = text;
			int hashCode = text.GetHashCode();
			this.Hash[o] = hashCode;
			return hashCode;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x000991F0 File Offset: 0x000973F0
		private string ToString(XmlSchemaObject o, SchemaObjectWriter writer)
		{
			return writer.WriteXmlSchemaObject(o);
		}

		// Token: 0x040015EE RID: 5614
		private Hashtable graph;

		// Token: 0x040015EF RID: 5615
		private Hashtable hash;

		// Token: 0x040015F0 RID: 5616
		private Hashtable objectCache;

		// Token: 0x040015F1 RID: 5617
		private StringCollection warnings;

		// Token: 0x040015F2 RID: 5618
		internal Hashtable looks = new Hashtable();
	}
}
