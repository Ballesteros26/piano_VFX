using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x02000223 RID: 547
	internal class XmlElementListEnumerator : IEnumerator
	{
		// Token: 0x060014B4 RID: 5300 RVA: 0x00075D8A File Offset: 0x00073F8A
		public XmlElementListEnumerator(XmlElementList list)
		{
			this.list = list;
			this.curElem = null;
			this.changeCount = list.ChangeCount;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00075DAC File Offset: 0x00073FAC
		public bool MoveNext()
		{
			if (this.list.ChangeCount != this.changeCount)
			{
				throw new InvalidOperationException(Res.GetString("The element list has changed. The enumeration operation failed to continue."));
			}
			this.curElem = this.list.GetNextNode(this.curElem);
			return this.curElem != null;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00075DFC File Offset: 0x00073FFC
		public void Reset()
		{
			this.curElem = null;
			this.changeCount = this.list.ChangeCount;
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x00075E16 File Offset: 0x00074016
		public object Current
		{
			get
			{
				return this.curElem;
			}
		}

		// Token: 0x04000DD0 RID: 3536
		private XmlElementList list;

		// Token: 0x04000DD1 RID: 3537
		private XmlNode curElem;

		// Token: 0x04000DD2 RID: 3538
		private int changeCount;
	}
}
