using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200059C RID: 1436
	internal class Keys : KeyedCollection<QilName, List<Key>>
	{
		// Token: 0x060038C1 RID: 14529 RVA: 0x0013F09F File Offset: 0x0013D29F
		protected override QilName GetKeyForItem(List<Key> list)
		{
			return list[0].Name;
		}
	}
}
