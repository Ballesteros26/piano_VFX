using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x02000224 RID: 548
	internal class XmlEmptyElementListEnumerator : IEnumerator
	{
		// Token: 0x060014B8 RID: 5304 RVA: 0x000020FD File Offset: 0x000002FD
		public XmlEmptyElementListEnumerator(XmlElementList list)
		{
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0000226C File Offset: 0x0000046C
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00002F50 File Offset: 0x00001150
		public void Reset()
		{
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x0000365F File Offset: 0x0000185F
		public object Current
		{
			get
			{
				return null;
			}
		}
	}
}
