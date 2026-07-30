using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200006A RID: 106
	internal class NamespaceFrame
	{
		// Token: 0x060002AE RID: 686 RVA: 0x00009EDE File Offset: 0x000080DE
		internal NamespaceFrame()
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00009EFC File Offset: 0x000080FC
		internal void AddRendered(XmlAttribute attr)
		{
			this._rendered.Add(Utils.GetNamespacePrefix(attr), attr);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00009F10 File Offset: 0x00008110
		internal XmlAttribute GetRendered(string nsPrefix)
		{
			return (XmlAttribute)this._rendered[nsPrefix];
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00009F23 File Offset: 0x00008123
		internal void AddUnrendered(XmlAttribute attr)
		{
			this._unrendered.Add(Utils.GetNamespacePrefix(attr), attr);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00009F37 File Offset: 0x00008137
		internal XmlAttribute GetUnrendered(string nsPrefix)
		{
			return (XmlAttribute)this._unrendered[nsPrefix];
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00009F4A File Offset: 0x0000814A
		internal Hashtable GetUnrendered()
		{
			return this._unrendered;
		}

		// Token: 0x04000173 RID: 371
		private Hashtable _rendered = new Hashtable();

		// Token: 0x04000174 RID: 372
		private Hashtable _unrendered = new Hashtable();
	}
}
