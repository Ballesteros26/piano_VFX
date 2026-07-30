using System;
using System.Configuration;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x0200069A RID: 1690
	internal class HandlersUtil
	{
		// Token: 0x06003506 RID: 13574 RVA: 0x000020EB File Offset: 0x000002EB
		private HandlersUtil()
		{
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000C4098 File Offset: 0x000C2298
		internal static string ExtractAttributeValue(string attKey, XmlNode node)
		{
			return HandlersUtil.ExtractAttributeValue(attKey, node, false);
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000C40A4 File Offset: 0x000C22A4
		internal static string ExtractAttributeValue(string attKey, XmlNode node, bool optional)
		{
			if (node.Attributes == null)
			{
				if (optional)
				{
					return null;
				}
				HandlersUtil.ThrowException("Required attribute not found: " + attKey, node);
			}
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(attKey);
			if (xmlNode == null)
			{
				if (optional)
				{
					return null;
				}
				HandlersUtil.ThrowException("Required attribute not found: " + attKey, node);
			}
			string value = xmlNode.Value;
			if (value == string.Empty)
			{
				HandlersUtil.ThrowException((optional ? "Optional" : "Required") + " attribute is empty: " + attKey, node);
			}
			return value;
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000C4128 File Offset: 0x000C2328
		internal static void ThrowException(string msg, XmlNode node)
		{
			if (node != null && node.Name != string.Empty)
			{
				msg = msg + " (node name: " + node.Name + ") ";
			}
			throw new ConfigurationException(msg, node);
		}
	}
}
