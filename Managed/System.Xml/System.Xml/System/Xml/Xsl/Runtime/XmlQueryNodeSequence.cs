using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000612 RID: 1554
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlQueryNodeSequence : XmlQuerySequence<XPathNavigator>, IList<XPathItem>, ICollection<XPathItem>, IEnumerable<XPathItem>, IEnumerable
	{
		// Token: 0x06003CF9 RID: 15609 RVA: 0x00152909 File Offset: 0x00150B09
		public static XmlQueryNodeSequence CreateOrReuse(XmlQueryNodeSequence seq)
		{
			if (seq != null)
			{
				seq.Clear();
				return seq;
			}
			return new XmlQueryNodeSequence();
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x0015291B File Offset: 0x00150B1B
		public static XmlQueryNodeSequence CreateOrReuse(XmlQueryNodeSequence seq, XPathNavigator navigator)
		{
			if (seq != null)
			{
				seq.Clear();
				seq.Add(navigator);
				return seq;
			}
			return new XmlQueryNodeSequence(navigator);
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x00152935 File Offset: 0x00150B35
		public XmlQueryNodeSequence()
		{
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x0015293D File Offset: 0x00150B3D
		public XmlQueryNodeSequence(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x00152948 File Offset: 0x00150B48
		public XmlQueryNodeSequence(IList<XPathNavigator> list)
			: base(list.Count)
		{
			for (int i = 0; i < list.Count; i++)
			{
				this.AddClone(list[i]);
			}
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x0015297F File Offset: 0x00150B7F
		public XmlQueryNodeSequence(XPathNavigator[] array, int size)
			: base(array, size)
		{
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x00152989 File Offset: 0x00150B89
		public XmlQueryNodeSequence(XPathNavigator navigator)
			: base(1)
		{
			this.AddClone(navigator);
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06003D00 RID: 15616 RVA: 0x00152999 File Offset: 0x00150B99
		// (set) Token: 0x06003D01 RID: 15617 RVA: 0x001529B2 File Offset: 0x00150BB2
		public bool IsDocOrderDistinct
		{
			get
			{
				return this.docOrderDistinct == this || base.Count <= 1;
			}
			set
			{
				this.docOrderDistinct = (value ? this : null);
			}
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x001529C4 File Offset: 0x00150BC4
		public XmlQueryNodeSequence DocOrderDistinct(IComparer<XPathNavigator> comparer)
		{
			if (this.docOrderDistinct != null)
			{
				return this.docOrderDistinct;
			}
			if (base.Count <= 1)
			{
				return this;
			}
			XPathNavigator[] array = new XPathNavigator[base.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = base[i];
			}
			Array.Sort<XPathNavigator>(array, 0, base.Count, comparer);
			int num = 0;
			for (int i = 1; i < array.Length; i++)
			{
				if (!array[num].IsSamePosition(array[i]))
				{
					num++;
					if (num != i)
					{
						array[num] = array[i];
					}
				}
			}
			this.docOrderDistinct = new XmlQueryNodeSequence(array, num + 1);
			this.docOrderDistinct.docOrderDistinct = this.docOrderDistinct;
			return this.docOrderDistinct;
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x00152A6E File Offset: 0x00150C6E
		public void AddClone(XPathNavigator navigator)
		{
			base.Add(navigator.Clone());
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x00152A7C File Offset: 0x00150C7C
		protected override void OnItemsChanged()
		{
			this.docOrderDistinct = null;
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x00152A85 File Offset: 0x00150C85
		IEnumerator<XPathItem> IEnumerable<XPathItem>.GetEnumerator()
		{
			return new IListEnumerator<XPathItem>(this);
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06003D06 RID: 15622 RVA: 0x00003242 File Offset: 0x00001442
		bool ICollection<XPathItem>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x00010C4A File Offset: 0x0000EE4A
		void ICollection<XPathItem>.Add(XPathItem value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x00010C4A File Offset: 0x0000EE4A
		void ICollection<XPathItem>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x00152A92 File Offset: 0x00150C92
		bool ICollection<XPathItem>.Contains(XPathItem value)
		{
			return base.IndexOf((XPathNavigator)value) != -1;
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x00152AA8 File Offset: 0x00150CA8
		void ICollection<XPathItem>.CopyTo(XPathItem[] array, int index)
		{
			for (int i = 0; i < base.Count; i++)
			{
				array[index + i] = base[i];
			}
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x00010C4A File Offset: 0x0000EE4A
		bool ICollection<XPathItem>.Remove(XPathItem value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C5D RID: 3165
		XPathItem IList<XPathItem>.this[int index]
		{
			get
			{
				if (index >= base.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return base[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x00152AEF File Offset: 0x00150CEF
		int IList<XPathItem>.IndexOf(XPathItem value)
		{
			return base.IndexOf((XPathNavigator)value);
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x00010C4A File Offset: 0x0000EE4A
		void IList<XPathItem>.Insert(int index, XPathItem value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x00010C4A File Offset: 0x0000EE4A
		void IList<XPathItem>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040027B7 RID: 10167
		public new static readonly XmlQueryNodeSequence Empty = new XmlQueryNodeSequence();

		// Token: 0x040027B8 RID: 10168
		private XmlQueryNodeSequence docOrderDistinct;
	}
}
