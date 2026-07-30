using System;

namespace System.Xml.Serialization
{
	/// <summary>Establishes a <see cref="P:System.Xml.Serialization.IXmlTextParser.Normalized" /> property for use by the .NET Framework infrastructure.</summary>
	// Token: 0x020002DC RID: 732
	public interface IXmlTextParser
	{
		/// <summary>Gets or sets whether white space and attribute values are normalized.</summary>
		/// <returns>true if white space attributes values are normalized; otherwise, false.</returns>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001B73 RID: 7027
		// (set) Token: 0x06001B74 RID: 7028
		bool Normalized { get; set; }

		/// <summary>Gets or sets how white space is handled when parsing XML.</summary>
		/// <returns>A member of the <see cref="T:System.Xml.WhitespaceHandling" /> enumeration that describes how whites pace is handled when parsing XML.</returns>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001B75 RID: 7029
		// (set) Token: 0x06001B76 RID: 7030
		WhitespaceHandling WhitespaceHandling { get; set; }
	}
}
