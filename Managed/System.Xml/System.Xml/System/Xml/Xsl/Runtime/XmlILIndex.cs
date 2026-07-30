using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000601 RID: 1537
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlILIndex
	{
		// Token: 0x06003BE8 RID: 15336 RVA: 0x0014FB72 File Offset: 0x0014DD72
		internal XmlILIndex()
		{
			this.table = new Dictionary<string, XmlQueryNodeSequence>();
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x0014FB88 File Offset: 0x0014DD88
		public void Add(string key, XPathNavigator navigator)
		{
			XmlQueryNodeSequence xmlQueryNodeSequence;
			if (!this.table.TryGetValue(key, out xmlQueryNodeSequence))
			{
				xmlQueryNodeSequence = new XmlQueryNodeSequence();
				xmlQueryNodeSequence.AddClone(navigator);
				this.table.Add(key, xmlQueryNodeSequence);
				return;
			}
			if (!navigator.IsSamePosition(xmlQueryNodeSequence[xmlQueryNodeSequence.Count - 1]))
			{
				xmlQueryNodeSequence.AddClone(navigator);
			}
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x0014FBE0 File Offset: 0x0014DDE0
		public XmlQueryNodeSequence Lookup(string key)
		{
			XmlQueryNodeSequence xmlQueryNodeSequence;
			if (!this.table.TryGetValue(key, out xmlQueryNodeSequence))
			{
				xmlQueryNodeSequence = new XmlQueryNodeSequence();
			}
			return xmlQueryNodeSequence;
		}

		// Token: 0x0400276D RID: 10093
		private Dictionary<string, XmlQueryNodeSequence> table;
	}
}
