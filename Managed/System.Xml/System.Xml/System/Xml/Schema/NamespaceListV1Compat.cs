using System;

namespace System.Xml.Schema
{
	// Token: 0x02000407 RID: 1031
	internal class NamespaceListV1Compat : NamespaceList
	{
		// Token: 0x060027FA RID: 10234 RVA: 0x000ECEB9 File Offset: 0x000EB0B9
		public NamespaceListV1Compat(string namespaces, string targetNamespace)
			: base(namespaces, targetNamespace)
		{
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000ECEC3 File Offset: 0x000EB0C3
		public override bool Allows(string ns)
		{
			if (base.Type == NamespaceList.ListType.Other)
			{
				return ns != base.Excluded;
			}
			return base.Allows(ns);
		}
	}
}
