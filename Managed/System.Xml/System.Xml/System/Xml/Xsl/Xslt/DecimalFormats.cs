using System;
using System.Collections.ObjectModel;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200056C RID: 1388
	internal class DecimalFormats : KeyedCollection<XmlQualifiedName, DecimalFormatDecl>
	{
		// Token: 0x0600375B RID: 14171 RVA: 0x00134B01 File Offset: 0x00132D01
		protected override XmlQualifiedName GetKeyForItem(DecimalFormatDecl format)
		{
			return format.Name;
		}
	}
}
