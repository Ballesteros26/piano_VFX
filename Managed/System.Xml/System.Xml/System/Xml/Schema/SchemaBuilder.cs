using System;

namespace System.Xml.Schema
{
	// Token: 0x02000410 RID: 1040
	internal abstract class SchemaBuilder
	{
		// Token: 0x06002867 RID: 10343
		internal abstract bool ProcessElement(string prefix, string name, string ns);

		// Token: 0x06002868 RID: 10344
		internal abstract void ProcessAttribute(string prefix, string name, string ns, string value);

		// Token: 0x06002869 RID: 10345
		internal abstract bool IsContentParsed();

		// Token: 0x0600286A RID: 10346
		internal abstract void ProcessMarkup(XmlNode[] markup);

		// Token: 0x0600286B RID: 10347
		internal abstract void ProcessCData(string value);

		// Token: 0x0600286C RID: 10348
		internal abstract void StartChildren();

		// Token: 0x0600286D RID: 10349
		internal abstract void EndChildren();
	}
}
