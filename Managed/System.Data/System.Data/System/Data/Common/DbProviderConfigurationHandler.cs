using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;

namespace System.Data.Common
{
	/// <summary>This class can be used by any provider to support a provider-specific configuration section.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000389 RID: 905
	public class DbProviderConfigurationHandler : IConfigurationSectionHandler
	{
		// Token: 0x06002ACE RID: 10958 RVA: 0x000BDCC1 File Offset: 0x000BBEC1
		internal static NameValueCollection CloneParent(NameValueCollection parentConfig)
		{
			if (parentConfig == null)
			{
				parentConfig = new NameValueCollection();
			}
			else
			{
				parentConfig = new NameValueCollection(parentConfig);
			}
			return parentConfig;
		}

		/// <summary>Creates a new <see cref="System.Collections.Specialized.NameValueCollection" /> expression.</summary>
		/// <returns>The new expression.</returns>
		/// <param name="parent">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <param name="configContext">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <param name="section">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002ACF RID: 10959 RVA: 0x000BDCD8 File Offset: 0x000BBED8
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			return DbProviderConfigurationHandler.CreateStatic(parent, configContext, section);
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000BDCE4 File Offset: 0x000BBEE4
		internal static object CreateStatic(object parent, object configContext, XmlNode section)
		{
			object obj = parent;
			if (section != null)
			{
				obj = DbProviderConfigurationHandler.CloneParent(parent as NameValueCollection);
				bool flag = false;
				HandlerBase.CheckForUnrecognizedAttributes(section);
				foreach (object obj2 in section.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						string name = xmlNode.Name;
						if (!(name == "settings"))
						{
							throw ADP.ConfigUnrecognizedElement(xmlNode);
						}
						if (flag)
						{
							throw ADP.ConfigSectionsUnique("settings");
						}
						flag = true;
						DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.CreateStatic(obj as NameValueCollection, configContext, xmlNode);
					}
				}
			}
			return obj;
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000BDD9C File Offset: 0x000BBF9C
		internal static string RemoveAttribute(XmlNode node, string name)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(name);
			if (xmlNode == null)
			{
				throw ADP.ConfigRequiredAttributeMissing(name, node);
			}
			string value = xmlNode.Value;
			if (value.Length == 0)
			{
				throw ADP.ConfigRequiredAttributeEmpty(name, node);
			}
			return value;
		}

		// Token: 0x040019F8 RID: 6648
		internal const string settings = "settings";

		// Token: 0x0200038A RID: 906
		private sealed class DbProviderDictionarySectionHandler
		{
			// Token: 0x06002AD2 RID: 10962 RVA: 0x000BDDDC File Offset: 0x000BBFDC
			internal static NameValueCollection CreateStatic(NameValueCollection config, object context, XmlNode section)
			{
				if (section != null)
				{
					HandlerBase.CheckForUnrecognizedAttributes(section);
				}
				foreach (object obj in section.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						string name = xmlNode.Name;
						if (!(name == "add"))
						{
							if (!(name == "remove"))
							{
								if (!(name == "clear"))
								{
									throw ADP.ConfigUnrecognizedElement(xmlNode);
								}
								DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.HandleClear(xmlNode, config);
							}
							else
							{
								DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.HandleRemove(xmlNode, config);
							}
						}
						else
						{
							DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.HandleAdd(xmlNode, config);
						}
					}
				}
				return config;
			}

			// Token: 0x06002AD3 RID: 10963 RVA: 0x000BDE94 File Offset: 0x000BC094
			private static void HandleAdd(XmlNode child, NameValueCollection config)
			{
				HandlerBase.CheckForChildNodes(child);
				string text = DbProviderConfigurationHandler.RemoveAttribute(child, "name");
				string text2 = DbProviderConfigurationHandler.RemoveAttribute(child, "value");
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Add(text, text2);
			}

			// Token: 0x06002AD4 RID: 10964 RVA: 0x000BDED0 File Offset: 0x000BC0D0
			private static void HandleRemove(XmlNode child, NameValueCollection config)
			{
				HandlerBase.CheckForChildNodes(child);
				string text = DbProviderConfigurationHandler.RemoveAttribute(child, "name");
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Remove(text);
			}

			// Token: 0x06002AD5 RID: 10965 RVA: 0x000BDEFC File Offset: 0x000BC0FC
			private static void HandleClear(XmlNode child, NameValueCollection config)
			{
				HandlerBase.CheckForChildNodes(child);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Clear();
			}
		}
	}
}
