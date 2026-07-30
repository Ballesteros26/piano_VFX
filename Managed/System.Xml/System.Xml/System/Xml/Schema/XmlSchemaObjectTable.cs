using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	/// <summary>Provides the collections for contained elements in the <see cref="T:System.Xml.Schema.XmlSchema" /> class (for example, Attributes, AttributeGroups, Elements, and so on).</summary>
	// Token: 0x02000470 RID: 1136
	public class XmlSchemaObjectTable
	{
		// Token: 0x06002CC3 RID: 11459 RVA: 0x001074A9 File Offset: 0x001056A9
		internal XmlSchemaObjectTable()
		{
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x001074C7 File Offset: 0x001056C7
		internal void Add(XmlQualifiedName name, XmlSchemaObject value)
		{
			this.table.Add(name, value);
			this.entries.Add(new XmlSchemaObjectTable.XmlSchemaObjectEntry(name, value));
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x001074E8 File Offset: 0x001056E8
		internal void Insert(XmlQualifiedName name, XmlSchemaObject value)
		{
			XmlSchemaObject xmlSchemaObject = null;
			if (this.table.TryGetValue(name, out xmlSchemaObject))
			{
				this.table[name] = value;
				int num = this.FindIndexByValue(xmlSchemaObject);
				this.entries[num] = new XmlSchemaObjectTable.XmlSchemaObjectEntry(name, value);
				return;
			}
			this.Add(name, value);
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x00107538 File Offset: 0x00105738
		internal void Replace(XmlQualifiedName name, XmlSchemaObject value)
		{
			XmlSchemaObject xmlSchemaObject;
			if (this.table.TryGetValue(name, out xmlSchemaObject))
			{
				this.table[name] = value;
				int num = this.FindIndexByValue(xmlSchemaObject);
				this.entries[num] = new XmlSchemaObjectTable.XmlSchemaObjectEntry(name, value);
			}
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x0010757D File Offset: 0x0010577D
		internal void Clear()
		{
			this.table.Clear();
			this.entries.Clear();
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x00107598 File Offset: 0x00105798
		internal void Remove(XmlQualifiedName name)
		{
			XmlSchemaObject xmlSchemaObject;
			if (this.table.TryGetValue(name, out xmlSchemaObject))
			{
				this.table.Remove(name);
				int num = this.FindIndexByValue(xmlSchemaObject);
				this.entries.RemoveAt(num);
			}
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x001075D8 File Offset: 0x001057D8
		private int FindIndexByValue(XmlSchemaObject xso)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (this.entries[i].xso == xso)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets the number of items contained in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>The number of items contained in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x00107612 File Offset: 0x00105812
		public int Count
		{
			get
			{
				return this.table.Count;
			}
		}

		/// <summary>Determines if the qualified name specified exists in the collection.</summary>
		/// <returns>true if the qualified name specified exists in the collection; otherwise, false.</returns>
		/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" />.</param>
		// Token: 0x06002CCB RID: 11467 RVA: 0x0010761F File Offset: 0x0010581F
		public bool Contains(XmlQualifiedName name)
		{
			return this.table.ContainsKey(name);
		}

		/// <summary>Returns the element in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> specified by qualified name.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> of the element in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> specified by qualified name.</returns>
		/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" /> of the element to return.</param>
		// Token: 0x170009BF RID: 2495
		public XmlSchemaObject this[XmlQualifiedName name]
		{
			get
			{
				XmlSchemaObject xmlSchemaObject;
				if (this.table.TryGetValue(name, out xmlSchemaObject))
				{
					return xmlSchemaObject;
				}
				return null;
			}
		}

		/// <summary>Returns a collection of all the named elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>A collection of all the named elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002CCD RID: 11469 RVA: 0x00107650 File Offset: 0x00105850
		public ICollection Names
		{
			get
			{
				return new XmlSchemaObjectTable.NamesCollection(this.entries, this.table.Count);
			}
		}

		/// <summary>Returns a collection of all the values for all the elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>A collection of all the values for all the elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x00107668 File Offset: 0x00105868
		public ICollection Values
		{
			get
			{
				return new XmlSchemaObjectTable.ValuesCollection(this.entries, this.table.Count);
			}
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> that can iterate through <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x06002CCF RID: 11471 RVA: 0x00107680 File Offset: 0x00105880
		public IDictionaryEnumerator GetEnumerator()
		{
			return new XmlSchemaObjectTable.XSODictionaryEnumerator(this.entries, this.table.Count, XmlSchemaObjectTable.EnumeratorType.DictionaryEntry);
		}

		// Token: 0x04001DE0 RID: 7648
		private Dictionary<XmlQualifiedName, XmlSchemaObject> table = new Dictionary<XmlQualifiedName, XmlSchemaObject>();

		// Token: 0x04001DE1 RID: 7649
		private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries = new List<XmlSchemaObjectTable.XmlSchemaObjectEntry>();

		// Token: 0x02000471 RID: 1137
		internal enum EnumeratorType
		{
			// Token: 0x04001DE3 RID: 7651
			Keys,
			// Token: 0x04001DE4 RID: 7652
			Values,
			// Token: 0x04001DE5 RID: 7653
			DictionaryEntry
		}

		// Token: 0x02000472 RID: 1138
		internal struct XmlSchemaObjectEntry
		{
			// Token: 0x06002CD0 RID: 11472 RVA: 0x00107699 File Offset: 0x00105899
			public XmlSchemaObjectEntry(XmlQualifiedName name, XmlSchemaObject value)
			{
				this.qname = name;
				this.xso = value;
			}

			// Token: 0x06002CD1 RID: 11473 RVA: 0x001076A9 File Offset: 0x001058A9
			public XmlSchemaObject IsMatch(string localName, string ns)
			{
				if (localName == this.qname.Name && ns == this.qname.Namespace)
				{
					return this.xso;
				}
				return null;
			}

			// Token: 0x06002CD2 RID: 11474 RVA: 0x001076D9 File Offset: 0x001058D9
			public void Reset()
			{
				this.qname = null;
				this.xso = null;
			}

			// Token: 0x04001DE6 RID: 7654
			internal XmlQualifiedName qname;

			// Token: 0x04001DE7 RID: 7655
			internal XmlSchemaObject xso;
		}

		// Token: 0x02000473 RID: 1139
		internal class NamesCollection : ICollection, IEnumerable
		{
			// Token: 0x06002CD3 RID: 11475 RVA: 0x001076E9 File Offset: 0x001058E9
			internal NamesCollection(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size)
			{
				this.entries = entries;
				this.size = size;
			}

			// Token: 0x170009C2 RID: 2498
			// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x001076FF File Offset: 0x001058FF
			public int Count
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x170009C3 RID: 2499
			// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x00107707 File Offset: 0x00105907
			public object SyncRoot
			{
				get
				{
					return ((ICollection)this.entries).SyncRoot;
				}
			}

			// Token: 0x170009C4 RID: 2500
			// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x00107714 File Offset: 0x00105914
			public bool IsSynchronized
			{
				get
				{
					return ((ICollection)this.entries).IsSynchronized;
				}
			}

			// Token: 0x06002CD7 RID: 11479 RVA: 0x00107724 File Offset: 0x00105924
			public void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				for (int i = 0; i < this.size; i++)
				{
					array.SetValue(this.entries[i].qname, arrayIndex++);
				}
			}

			// Token: 0x06002CD8 RID: 11480 RVA: 0x0010777C File Offset: 0x0010597C
			public IEnumerator GetEnumerator()
			{
				return new XmlSchemaObjectTable.XSOEnumerator(this.entries, this.size, XmlSchemaObjectTable.EnumeratorType.Keys);
			}

			// Token: 0x04001DE8 RID: 7656
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x04001DE9 RID: 7657
			private int size;
		}

		// Token: 0x02000474 RID: 1140
		internal class ValuesCollection : ICollection, IEnumerable
		{
			// Token: 0x06002CD9 RID: 11481 RVA: 0x00107790 File Offset: 0x00105990
			internal ValuesCollection(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size)
			{
				this.entries = entries;
				this.size = size;
			}

			// Token: 0x170009C5 RID: 2501
			// (get) Token: 0x06002CDA RID: 11482 RVA: 0x001077A6 File Offset: 0x001059A6
			public int Count
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x170009C6 RID: 2502
			// (get) Token: 0x06002CDB RID: 11483 RVA: 0x001077AE File Offset: 0x001059AE
			public object SyncRoot
			{
				get
				{
					return ((ICollection)this.entries).SyncRoot;
				}
			}

			// Token: 0x170009C7 RID: 2503
			// (get) Token: 0x06002CDC RID: 11484 RVA: 0x001077BB File Offset: 0x001059BB
			public bool IsSynchronized
			{
				get
				{
					return ((ICollection)this.entries).IsSynchronized;
				}
			}

			// Token: 0x06002CDD RID: 11485 RVA: 0x001077C8 File Offset: 0x001059C8
			public void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				for (int i = 0; i < this.size; i++)
				{
					array.SetValue(this.entries[i].xso, arrayIndex++);
				}
			}

			// Token: 0x06002CDE RID: 11486 RVA: 0x00107820 File Offset: 0x00105A20
			public IEnumerator GetEnumerator()
			{
				return new XmlSchemaObjectTable.XSOEnumerator(this.entries, this.size, XmlSchemaObjectTable.EnumeratorType.Values);
			}

			// Token: 0x04001DEA RID: 7658
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x04001DEB RID: 7659
			private int size;
		}

		// Token: 0x02000475 RID: 1141
		internal class XSOEnumerator : IEnumerator
		{
			// Token: 0x06002CDF RID: 11487 RVA: 0x00107834 File Offset: 0x00105A34
			internal XSOEnumerator(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size, XmlSchemaObjectTable.EnumeratorType enumType)
			{
				this.entries = entries;
				this.size = size;
				this.enumType = enumType;
				this.currentIndex = -1;
			}

			// Token: 0x170009C8 RID: 2504
			// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x00107858 File Offset: 0x00105A58
			public object Current
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[] { string.Empty }));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[] { string.Empty }));
					}
					switch (this.enumType)
					{
					case XmlSchemaObjectTable.EnumeratorType.Keys:
						return this.currentKey;
					case XmlSchemaObjectTable.EnumeratorType.Values:
						return this.currentValue;
					case XmlSchemaObjectTable.EnumeratorType.DictionaryEntry:
						return new DictionaryEntry(this.currentKey, this.currentValue);
					default:
						return null;
					}
				}
			}

			// Token: 0x06002CE1 RID: 11489 RVA: 0x001078FC File Offset: 0x00105AFC
			public bool MoveNext()
			{
				if (this.currentIndex >= this.size - 1)
				{
					this.currentValue = null;
					this.currentKey = null;
					return false;
				}
				this.currentIndex++;
				this.currentValue = this.entries[this.currentIndex].xso;
				this.currentKey = this.entries[this.currentIndex].qname;
				return true;
			}

			// Token: 0x06002CE2 RID: 11490 RVA: 0x00107970 File Offset: 0x00105B70
			public void Reset()
			{
				this.currentIndex = -1;
				this.currentValue = null;
				this.currentKey = null;
			}

			// Token: 0x04001DEC RID: 7660
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x04001DED RID: 7661
			private XmlSchemaObjectTable.EnumeratorType enumType;

			// Token: 0x04001DEE RID: 7662
			protected int currentIndex;

			// Token: 0x04001DEF RID: 7663
			protected int size;

			// Token: 0x04001DF0 RID: 7664
			protected XmlQualifiedName currentKey;

			// Token: 0x04001DF1 RID: 7665
			protected XmlSchemaObject currentValue;
		}

		// Token: 0x02000476 RID: 1142
		internal class XSODictionaryEnumerator : XmlSchemaObjectTable.XSOEnumerator, IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06002CE3 RID: 11491 RVA: 0x00107987 File Offset: 0x00105B87
			internal XSODictionaryEnumerator(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size, XmlSchemaObjectTable.EnumeratorType enumType)
				: base(entries, size, enumType)
			{
			}

			// Token: 0x170009C9 RID: 2505
			// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x00107994 File Offset: 0x00105B94
			public DictionaryEntry Entry
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[] { string.Empty }));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[] { string.Empty }));
					}
					return new DictionaryEntry(this.currentKey, this.currentValue);
				}
			}

			// Token: 0x170009CA RID: 2506
			// (get) Token: 0x06002CE5 RID: 11493 RVA: 0x00107A08 File Offset: 0x00105C08
			public object Key
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[] { string.Empty }));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[] { string.Empty }));
					}
					return this.currentKey;
				}
			}

			// Token: 0x170009CB RID: 2507
			// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x00107A70 File Offset: 0x00105C70
			public object Value
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[] { string.Empty }));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[] { string.Empty }));
					}
					return this.currentValue;
				}
			}
		}
	}
}
