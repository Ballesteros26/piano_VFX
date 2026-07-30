using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000220 RID: 544
	internal class XmlDOMTextWriter : XmlTextWriter
	{
		// Token: 0x06001466 RID: 5222 RVA: 0x00075018 File Offset: 0x00073218
		public XmlDOMTextWriter(Stream w, Encoding encoding)
			: base(w, encoding)
		{
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00075022 File Offset: 0x00073222
		public XmlDOMTextWriter(string filename, Encoding encoding)
			: base(filename, encoding)
		{
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0007502C File Offset: 0x0007322C
		public XmlDOMTextWriter(TextWriter w)
			: base(w)
		{
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00075035 File Offset: 0x00073235
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0 && prefix.Length != 0)
			{
				prefix = "";
			}
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00075057 File Offset: 0x00073257
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (ns.Length == 0 && prefix.Length != 0)
			{
				prefix = "";
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}
	}
}
