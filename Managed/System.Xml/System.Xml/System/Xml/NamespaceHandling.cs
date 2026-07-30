using System;

namespace System.Xml
{
	/// <summary>Specifies whether to remove duplicate namespace declarations in the <see cref="T:System.Xml.XmlWriter" />. </summary>
	// Token: 0x020000A6 RID: 166
	[Flags]
	public enum NamespaceHandling
	{
		/// <summary>Specifies that duplicate namespace declarations will not be removed.</summary>
		// Token: 0x0400033B RID: 827
		Default = 0,
		/// <summary>Specifies that duplicate namespace declarations will be removed. For the duplicate namespace to be removed, the prefix and the namespace must match.</summary>
		// Token: 0x0400033C RID: 828
		OmitDuplicates = 1
	}
}
