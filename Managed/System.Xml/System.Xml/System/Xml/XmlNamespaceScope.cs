using System;

namespace System.Xml
{
	/// <summary>Defines the namespace scope.</summary>
	// Token: 0x0200029E RID: 670
	public enum XmlNamespaceScope
	{
		/// <summary>All namespaces defined in the scope of the current node. This includes the xmlns:xml namespace which is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x04001024 RID: 4132
		All,
		/// <summary>All namespaces defined in the scope of the current node, excluding the xmlns:xml namespace, which is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x04001025 RID: 4133
		ExcludeXml,
		/// <summary>All namespaces that are defined locally at the current node.</summary>
		// Token: 0x04001026 RID: 4134
		Local
	}
}
