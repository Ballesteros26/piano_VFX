using System;

namespace System.Xml
{
	/// <summary>Provides an interface to enable a class to return line and position information.</summary>
	// Token: 0x02000243 RID: 579
	public interface IXmlLineInfo
	{
		/// <summary>Gets a value indicating whether the class can return line information.</summary>
		/// <returns>true if <see cref="P:System.Xml.IXmlLineInfo.LineNumber" /> and <see cref="P:System.Xml.IXmlLineInfo.LinePosition" /> can be provided; otherwise, false.</returns>
		// Token: 0x0600167F RID: 5759
		bool HasLineInfo();

		/// <summary>Gets the current line number.</summary>
		/// <returns>The current line number or 0 if no line information is available (for example, <see cref="M:System.Xml.IXmlLineInfo.HasLineInfo" /> returns false).</returns>
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001680 RID: 5760
		int LineNumber { get; }

		/// <summary>Gets the current line position.</summary>
		/// <returns>The current line position or 0 if no line information is available (for example, <see cref="M:System.Xml.IXmlLineInfo.HasLineInfo" /> returns false).</returns>
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001681 RID: 5761
		int LinePosition { get; }
	}
}
