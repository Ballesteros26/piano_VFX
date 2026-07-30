using System;

namespace System.Xml.Xsl
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Xsl.XsltArgumentList.XsltMessageEncountered" /> event.</summary>
	// Token: 0x020004DA RID: 1242
	public abstract class XsltMessageEncounteredEventArgs : EventArgs
	{
		/// <summary>Gets the contents of the xsl:message element.</summary>
		/// <returns>The contents of the xsl:message element.</returns>
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060032B3 RID: 12979
		public abstract string Message { get; }
	}
}
