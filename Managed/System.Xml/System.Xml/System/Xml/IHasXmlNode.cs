using System;

namespace System.Xml
{
	/// <summary>Enables a class to return an <see cref="T:System.Xml.XmlNode" /> from the current context or position.</summary>
	// Token: 0x02000242 RID: 578
	public interface IHasXmlNode
	{
		/// <summary>Returns the <see cref="T:System.Xml.XmlNode" /> for the current position.</summary>
		/// <returns>The XmlNode for the current position.</returns>
		// Token: 0x0600167E RID: 5758
		XmlNode GetNode();
	}
}
