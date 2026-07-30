using System;

namespace System.Xml.XPath
{
	/// <summary>Provides an accessor to the <see cref="T:System.Xml.XPath.XPathNavigator" /> class.</summary>
	// Token: 0x020002AE RID: 686
	public interface IXPathNavigable
	{
		/// <summary>Returns a new <see cref="T:System.Xml.XPath.XPathNavigator" /> object. </summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		// Token: 0x0600192A RID: 6442
		XPathNavigator CreateNavigator();
	}
}
