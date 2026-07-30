using System;
using System.Globalization;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000390 RID: 912
	internal static class HandlerBase
	{
		// Token: 0x06002AEF RID: 10991 RVA: 0x000BE819 File Offset: 0x000BCA19
		internal static void CheckForChildNodes(XmlNode node)
		{
			if (node.HasChildNodes)
			{
				throw ADP.ConfigBaseNoChildNodes(node.FirstChild);
			}
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x000BE82F File Offset: 0x000BCA2F
		private static void CheckForNonElement(XmlNode node)
		{
			if (XmlNodeType.Element != node.NodeType)
			{
				throw ADP.ConfigBaseElementsOnly(node);
			}
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000BE841 File Offset: 0x000BCA41
		internal static void CheckForUnrecognizedAttributes(XmlNode node)
		{
			if (node.Attributes.Count != 0)
			{
				throw ADP.ConfigUnrecognizedAttributes(node);
			}
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000BE857 File Offset: 0x000BCA57
		internal static bool IsIgnorableAlsoCheckForNonElement(XmlNode node)
		{
			if (XmlNodeType.Comment == node.NodeType || XmlNodeType.Whitespace == node.NodeType)
			{
				return true;
			}
			HandlerBase.CheckForNonElement(node);
			return false;
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000BE878 File Offset: 0x000BCA78
		internal static string RemoveAttribute(XmlNode node, string name, bool required, bool allowEmpty)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(name);
			if (xmlNode == null)
			{
				if (required)
				{
					throw ADP.ConfigRequiredAttributeMissing(name, node);
				}
				return null;
			}
			else
			{
				string value = xmlNode.Value;
				if (!allowEmpty && value.Length == 0)
				{
					throw ADP.ConfigRequiredAttributeEmpty(name, node);
				}
				return value;
			}
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000BE8BD File Offset: 0x000BCABD
		internal static DataSet CloneParent(DataSet parentConfig, bool insenstive)
		{
			if (parentConfig == null)
			{
				parentConfig = new DataSet("system.data");
				parentConfig.CaseSensitive = !insenstive;
				parentConfig.Locale = CultureInfo.InvariantCulture;
			}
			else
			{
				parentConfig = parentConfig.Copy();
			}
			return parentConfig;
		}
	}
}
