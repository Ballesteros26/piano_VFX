using System;

namespace System.Xml.XPath
{
	/// <summary>Defines the namespace scope.</summary>
	// Token: 0x020002B8 RID: 696
	public enum XPathNamespaceScope
	{
		/// <summary>Returns all namespaces defined in the scope of the current node. This includes the xmlns:xml namespace which is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x04001546 RID: 5446
		All,
		/// <summary>Returns all namespaces defined in the scope of the current node, excluding the xmlns:xml namespace. The xmlns:xml namespace is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x04001547 RID: 5447
		ExcludeXml,
		/// <summary>Returns all namespaces that are defined locally at the current node. </summary>
		// Token: 0x04001548 RID: 5448
		Local
	}
}
