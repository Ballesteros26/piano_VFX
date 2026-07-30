using System;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x020005A3 RID: 1443
	internal sealed class HandlersUtil
	{
		// Token: 0x06003D52 RID: 15698 RVA: 0x00002050 File Offset: 0x00000250
		private HandlersUtil()
		{
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x000A2B7C File Offset: 0x000A0D7C
		public static string ExtractAttributeValue(string attKey, XmlNode node)
		{
			return HandlersUtil.ExtractAttributeValue(attKey, node, false);
		}

		// Token: 0x06003D54 RID: 15700 RVA: 0x000A2B86 File Offset: 0x000A0D86
		public static string ExtractAttributeValue(string attKey, XmlNode node, bool optional)
		{
			return HandlersUtil.ExtractAttributeValue(attKey, node, optional, false);
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x000A2B94 File Offset: 0x000A0D94
		public static string ExtractAttributeValue(string attKey, XmlNode node, bool optional, bool allowEmpty)
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
			if (!allowEmpty && value == string.Empty)
			{
				HandlersUtil.ThrowException((optional ? "Optional" : "Required") + " attribute is empty: " + attKey, node);
			}
			return value;
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x000A2C1D File Offset: 0x000A0E1D
		public static void ThrowException(string msg, XmlNode node)
		{
			if (node != null && node.Name != string.Empty)
			{
				msg = msg + " (node name: " + node.Name + ") ";
			}
			throw new ConfigurationException(msg, node);
		}
	}
}
