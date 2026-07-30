using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000046 RID: 70
	internal class CanonicalXmlNodeList : XmlNodeList, IList, ICollection, IEnumerable
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00005D48 File Offset: 0x00003F48
		internal CanonicalXmlNodeList()
		{
			this._nodeArray = new ArrayList();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005D5B File Offset: 0x00003F5B
		public override XmlNode Item(int index)
		{
			return (XmlNode)this._nodeArray[index];
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005D6E File Offset: 0x00003F6E
		public override IEnumerator GetEnumerator()
		{
			return this._nodeArray.GetEnumerator();
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00005D7B File Offset: 0x00003F7B
		public override int Count
		{
			get
			{
				return this._nodeArray.Count;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005D88 File Offset: 0x00003F88
		public int Add(object value)
		{
			if (!(value is XmlNode))
			{
				throw new ArgumentException("Type of input object is invalid.", "node");
			}
			return this._nodeArray.Add(value);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005DAE File Offset: 0x00003FAE
		public void Clear()
		{
			this._nodeArray.Clear();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005DBB File Offset: 0x00003FBB
		public bool Contains(object value)
		{
			return this._nodeArray.Contains(value);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005DC9 File Offset: 0x00003FC9
		public int IndexOf(object value)
		{
			return this._nodeArray.IndexOf(value);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005DD7 File Offset: 0x00003FD7
		public void Insert(int index, object value)
		{
			if (!(value is XmlNode))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			this._nodeArray.Insert(index, value);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005DFE File Offset: 0x00003FFE
		public void Remove(object value)
		{
			this._nodeArray.Remove(value);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005E0C File Offset: 0x0000400C
		public void RemoveAt(int index)
		{
			this._nodeArray.RemoveAt(index);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00005E1A File Offset: 0x0000401A
		public bool IsFixedSize
		{
			get
			{
				return this._nodeArray.IsFixedSize;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005E27 File Offset: 0x00004027
		public bool IsReadOnly
		{
			get
			{
				return this._nodeArray.IsReadOnly;
			}
		}

		// Token: 0x17000068 RID: 104
		object IList.this[int index]
		{
			get
			{
				return this._nodeArray[index];
			}
			set
			{
				if (!(value is XmlNode))
				{
					throw new ArgumentException("Type of input object is invalid.", "value");
				}
				this._nodeArray[index] = value;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005E69 File Offset: 0x00004069
		public void CopyTo(Array array, int index)
		{
			this._nodeArray.CopyTo(array, index);
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00005E78 File Offset: 0x00004078
		public object SyncRoot
		{
			get
			{
				return this._nodeArray.SyncRoot;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00005E85 File Offset: 0x00004085
		public bool IsSynchronized
		{
			get
			{
				return this._nodeArray.IsSynchronized;
			}
		}

		// Token: 0x04000115 RID: 277
		private ArrayList _nodeArray;
	}
}
