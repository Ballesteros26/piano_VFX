using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000050 RID: 80
	internal class XPathMultyIterator : ResetableIterator
	{
		// Token: 0x06000236 RID: 566 RVA: 0x00008120 File Offset: 0x00006320
		public XPathMultyIterator(ArrayList inputArray)
		{
			this.arr = new ResetableIterator[inputArray.Count];
			for (int i = 0; i < this.arr.Length; i++)
			{
				this.arr[i] = new XPathArrayIterator((ArrayList)inputArray[i]);
			}
			this.Init();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00008178 File Offset: 0x00006378
		private void Init()
		{
			for (int i = 0; i < this.arr.Length; i++)
			{
				this.Advance(i);
			}
			int num = this.arr.Length - 2;
			while (this.firstNotEmpty <= num)
			{
				if (this.SiftItem(num))
				{
					num--;
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000081C4 File Offset: 0x000063C4
		private bool Advance(int pos)
		{
			if (!this.arr[pos].MoveNext())
			{
				if (this.firstNotEmpty != pos)
				{
					ResetableIterator resetableIterator = this.arr[pos];
					Array.Copy(this.arr, this.firstNotEmpty, this.arr, this.firstNotEmpty + 1, pos - this.firstNotEmpty);
					this.arr[this.firstNotEmpty] = resetableIterator;
				}
				this.firstNotEmpty++;
				return false;
			}
			return true;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00008238 File Offset: 0x00006438
		private bool SiftItem(int item)
		{
			ResetableIterator resetableIterator = this.arr[item];
			while (item + 1 < this.arr.Length)
			{
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(resetableIterator.Current, this.arr[item + 1].Current);
				if (xmlNodeOrder == XmlNodeOrder.Before)
				{
					break;
				}
				if (xmlNodeOrder == XmlNodeOrder.After)
				{
					this.arr[item] = this.arr[item + 1];
					item++;
				}
				else
				{
					this.arr[item] = resetableIterator;
					if (!this.Advance(item))
					{
						return false;
					}
					resetableIterator = this.arr[item];
				}
			}
			this.arr[item] = resetableIterator;
			return true;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000082C0 File Offset: 0x000064C0
		public override void Reset()
		{
			this.firstNotEmpty = 0;
			this.position = 0;
			for (int i = 0; i < this.arr.Length; i++)
			{
				this.arr[i].Reset();
			}
			this.Init();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00008301 File Offset: 0x00006501
		public XPathMultyIterator(XPathMultyIterator it)
		{
			this.arr = (ResetableIterator[])it.arr.Clone();
			this.firstNotEmpty = it.firstNotEmpty;
			this.position = it.position;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00008337 File Offset: 0x00006537
		public override XPathNodeIterator Clone()
		{
			return new XPathMultyIterator(this);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000833F File Offset: 0x0000653F
		public override XPathNavigator Current
		{
			get
			{
				return this.arr[this.firstNotEmpty].Current;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00008353 File Offset: 0x00006553
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000835C File Offset: 0x0000655C
		public override bool MoveNext()
		{
			if (this.firstNotEmpty >= this.arr.Length)
			{
				return false;
			}
			if (this.position != 0)
			{
				if (this.Advance(this.firstNotEmpty))
				{
					this.SiftItem(this.firstNotEmpty);
				}
				if (this.firstNotEmpty >= this.arr.Length)
				{
					return false;
				}
			}
			this.position++;
			return true;
		}

		// Token: 0x04000119 RID: 281
		protected ResetableIterator[] arr;

		// Token: 0x0400011A RID: 282
		protected int firstNotEmpty;

		// Token: 0x0400011B RID: 283
		protected int position;
	}
}
