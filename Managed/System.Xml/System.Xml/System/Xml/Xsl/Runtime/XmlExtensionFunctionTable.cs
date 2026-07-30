using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005FF RID: 1535
	internal class XmlExtensionFunctionTable
	{
		// Token: 0x06003BD5 RID: 15317 RVA: 0x0014F5AD File Offset: 0x0014D7AD
		public XmlExtensionFunctionTable()
		{
			this.table = new Dictionary<XmlExtensionFunction, XmlExtensionFunction>();
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x0014F5C0 File Offset: 0x0014D7C0
		public XmlExtensionFunction Bind(string name, string namespaceUri, int numArgs, Type objectType, BindingFlags flags)
		{
			if (this.funcCached == null)
			{
				this.funcCached = new XmlExtensionFunction();
			}
			this.funcCached.Init(name, namespaceUri, numArgs, objectType, flags);
			XmlExtensionFunction xmlExtensionFunction;
			if (!this.table.TryGetValue(this.funcCached, out xmlExtensionFunction))
			{
				xmlExtensionFunction = this.funcCached;
				this.funcCached = null;
				xmlExtensionFunction.Bind();
				this.table.Add(xmlExtensionFunction, xmlExtensionFunction);
			}
			return xmlExtensionFunction;
		}

		// Token: 0x04002760 RID: 10080
		private Dictionary<XmlExtensionFunction, XmlExtensionFunction> table;

		// Token: 0x04002761 RID: 10081
		private XmlExtensionFunction funcCached;
	}
}
