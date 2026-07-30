using System;
using System.Configuration;
using System.Globalization;
using System.Xml;

namespace System.Data.Common
{
	/// <summary>This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200038E RID: 910
	public class DbProviderFactoriesConfigurationHandler : IConfigurationSectionHandler
	{
		/// <summary>This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</returns>
		/// <param name="parent">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <param name="configContext">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <param name="section">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002AE7 RID: 10983 RVA: 0x000BE4A9 File Offset: 0x000BC6A9
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			return DbProviderFactoriesConfigurationHandler.CreateStatic(parent, configContext, section);
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000BE4B4 File Offset: 0x000BC6B4
		internal static object CreateStatic(object parent, object configContext, XmlNode section)
		{
			object obj = parent;
			if (section != null)
			{
				obj = HandlerBase.CloneParent(parent as DataSet, false);
				bool flag = false;
				HandlerBase.CheckForUnrecognizedAttributes(section);
				foreach (object obj2 in section.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						string name = xmlNode.Name;
						if (!(name == "DbProviderFactories"))
						{
							throw ADP.ConfigUnrecognizedElement(xmlNode);
						}
						if (flag)
						{
							throw ADP.ConfigSectionsUnique("DbProviderFactories");
						}
						flag = true;
						DbProviderFactoriesConfigurationHandler.HandleProviders(obj as DataSet, configContext, xmlNode, name);
					}
				}
			}
			return obj;
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000BE570 File Offset: 0x000BC770
		private static void HandleProviders(DataSet config, object configContext, XmlNode section, string sectionName)
		{
			DataTableCollection tables = config.Tables;
			DataTable dataTable = tables[sectionName];
			bool flag = dataTable != null;
			dataTable = DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.CreateStatic(dataTable, configContext, section);
			if (!flag)
			{
				tables.Add(dataTable);
			}
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000BE5A4 File Offset: 0x000BC7A4
		internal static DataTable CreateProviderDataTable()
		{
			DataColumn dataColumn = new DataColumn("Name", typeof(string));
			dataColumn.ReadOnly = true;
			DataColumn dataColumn2 = new DataColumn("Description", typeof(string));
			dataColumn2.ReadOnly = true;
			DataColumn dataColumn3 = new DataColumn("InvariantName", typeof(string));
			dataColumn3.ReadOnly = true;
			DataColumn dataColumn4 = new DataColumn("AssemblyQualifiedName", typeof(string));
			dataColumn4.ReadOnly = true;
			DataColumn[] array = new DataColumn[] { dataColumn3 };
			DataColumn[] array2 = new DataColumn[] { dataColumn, dataColumn2, dataColumn3, dataColumn4 };
			DataTable dataTable = new DataTable("DbProviderFactories");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.AddRange(array2);
			dataTable.PrimaryKey = array;
			return dataTable;
		}

		// Token: 0x04001A0C RID: 6668
		internal const string sectionName = "system.data";

		// Token: 0x04001A0D RID: 6669
		internal const string providerGroup = "DbProviderFactories";

		// Token: 0x04001A0E RID: 6670
		internal const string odbcProviderName = "Odbc Data Provider";

		// Token: 0x04001A0F RID: 6671
		internal const string odbcProviderDescription = ".Net Framework Data Provider for Odbc";

		// Token: 0x04001A10 RID: 6672
		internal const string oledbProviderName = "OleDb Data Provider";

		// Token: 0x04001A11 RID: 6673
		internal const string oledbProviderDescription = ".Net Framework Data Provider for OleDb";

		// Token: 0x04001A12 RID: 6674
		internal const string oracleclientProviderName = "OracleClient Data Provider";

		// Token: 0x04001A13 RID: 6675
		internal const string oracleclientProviderNamespace = "System.Data.OracleClient";

		// Token: 0x04001A14 RID: 6676
		internal const string oracleclientProviderDescription = ".Net Framework Data Provider for Oracle";

		// Token: 0x04001A15 RID: 6677
		internal const string sqlclientProviderName = "SqlClient Data Provider";

		// Token: 0x04001A16 RID: 6678
		internal const string sqlclientProviderDescription = ".Net Framework Data Provider for SqlServer";

		// Token: 0x04001A17 RID: 6679
		internal const string sqlclientPartialAssemblyQualifiedName = "System.Data.SqlClient.SqlClientFactory, System.Data,";

		// Token: 0x04001A18 RID: 6680
		internal const string oracleclientPartialAssemblyQualifiedName = "System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient,";

		// Token: 0x0200038F RID: 911
		private static class DbProviderDictionarySectionHandler
		{
			// Token: 0x06002AEB RID: 10987 RVA: 0x000BE670 File Offset: 0x000BC870
			internal static DataTable CreateStatic(DataTable config, object context, XmlNode section)
			{
				if (section != null)
				{
					HandlerBase.CheckForUnrecognizedAttributes(section);
					if (config == null)
					{
						config = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
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
									DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleClear(xmlNode, config);
								}
								else
								{
									DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleRemove(xmlNode, config);
								}
							}
							else
							{
								DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleAdd(xmlNode, config);
							}
						}
					}
					config.AcceptChanges();
				}
				return config;
			}

			// Token: 0x06002AEC RID: 10988 RVA: 0x000BE73C File Offset: 0x000BC93C
			private static void HandleAdd(XmlNode child, DataTable config)
			{
				HandlerBase.CheckForChildNodes(child);
				DataRow dataRow = config.NewRow();
				dataRow[0] = HandlerBase.RemoveAttribute(child, "name", true, false);
				dataRow[1] = HandlerBase.RemoveAttribute(child, "description", true, false);
				dataRow[2] = HandlerBase.RemoveAttribute(child, "invariant", true, false);
				dataRow[3] = HandlerBase.RemoveAttribute(child, "type", true, false);
				HandlerBase.RemoveAttribute(child, "support", false, false);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Rows.Add(dataRow);
			}

			// Token: 0x06002AED RID: 10989 RVA: 0x000BE7C8 File Offset: 0x000BC9C8
			private static void HandleRemove(XmlNode child, DataTable config)
			{
				HandlerBase.CheckForChildNodes(child);
				string text = HandlerBase.RemoveAttribute(child, "invariant", true, false);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				DataRow dataRow = config.Rows.Find(text);
				if (dataRow != null)
				{
					dataRow.Delete();
				}
			}

			// Token: 0x06002AEE RID: 10990 RVA: 0x000BE805 File Offset: 0x000BCA05
			private static void HandleClear(XmlNode child, DataTable config)
			{
				HandlerBase.CheckForChildNodes(child);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Clear();
			}
		}
	}
}
