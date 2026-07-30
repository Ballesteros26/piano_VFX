using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000301 RID: 769
	internal class NameTable : INameScope
	{
		// Token: 0x06001C93 RID: 7315 RVA: 0x0009BD6C File Offset: 0x00099F6C
		internal void Add(XmlQualifiedName qname, object value)
		{
			this.Add(qname.Name, qname.Namespace, value);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x0009BD84 File Offset: 0x00099F84
		internal void Add(string name, string ns, object value)
		{
			NameKey nameKey = new NameKey(name, ns);
			this.table.Add(nameKey, value);
		}

		// Token: 0x170005B6 RID: 1462
		internal object this[XmlQualifiedName qname]
		{
			get
			{
				return this.table[new NameKey(qname.Name, qname.Namespace)];
			}
			set
			{
				this.table[new NameKey(qname.Name, qname.Namespace)] = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		internal object this[string name, string ns]
		{
			get
			{
				return this.table[new NameKey(name, ns)];
			}
			set
			{
				this.table[new NameKey(name, ns)] = value;
			}
		}

		// Token: 0x170005B8 RID: 1464
		object INameScope.this[string name, string ns]
		{
			get
			{
				return this.table[new NameKey(name, ns)];
			}
			set
			{
				this.table[new NameKey(name, ns)] = value;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x0009BE0C File Offset: 0x0009A00C
		internal ICollection Values
		{
			get
			{
				return this.table.Values;
			}
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x0009BE1C File Offset: 0x0009A01C
		internal Array ToArray(Type type)
		{
			Array array = Array.CreateInstance(type, this.table.Count);
			this.table.Values.CopyTo(array, 0);
			return array;
		}

		// Token: 0x04001664 RID: 5732
		private Hashtable table = new Hashtable();
	}
}
