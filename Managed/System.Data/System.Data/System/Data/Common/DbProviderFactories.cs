using System;
using System.Configuration;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Reflection;

namespace System.Data.Common
{
	/// <summary>Represents a set of static methods for creating one or more instances of <see cref="T:System.Data.Common.DbProviderFactory" /> classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200038B RID: 907
	public static class DbProviderFactories
	{
		/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified provider name.</returns>
		/// <param name="providerInvariantName">Invariant name of a provider.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002AD7 RID: 10967 RVA: 0x000BDF10 File Offset: 0x000BC110
		public static DbProviderFactory GetFactory(string providerInvariantName)
		{
			ADP.CheckArgumentLength(providerInvariantName, "providerInvariantName");
			DataTable providerTable = DbProviderFactories.GetProviderTable();
			if (providerTable != null)
			{
				DataRow dataRow = providerTable.Rows.Find(providerInvariantName);
				if (dataRow != null)
				{
					return DbProviderFactories.GetFactory(dataRow);
				}
			}
			throw ADP.ConfigProviderNotFound();
		}

		/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified <see cref="T:System.Data.DataRow" />.</returns>
		/// <param name="providerRow">
		///   <see cref="T:System.Data.DataRow" /> containing the provider's configuration information.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002AD8 RID: 10968 RVA: 0x000BDF50 File Offset: 0x000BC150
		public static DbProviderFactory GetFactory(DataRow providerRow)
		{
			ADP.CheckArgumentNull(providerRow, "providerRow");
			DataColumn dataColumn = providerRow.Table.Columns["AssemblyQualifiedName"];
			if (dataColumn != null)
			{
				string text = providerRow[dataColumn] as string;
				if (!ADP.IsEmpty(text))
				{
					Type type = Type.GetType(text);
					if (null != type)
					{
						FieldInfo field = type.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
						if (null != field && field.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
						{
							object value = field.GetValue(null);
							if (value != null)
							{
								return (DbProviderFactory)value;
							}
						}
						throw ADP.ConfigProviderInvalid();
					}
					throw ADP.ConfigProviderNotInstalled();
				}
			}
			throw ADP.ConfigProviderMissing();
		}

		/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified connection.</returns>
		/// <param name="connection">The connection used.</param>
		// Token: 0x06002AD9 RID: 10969 RVA: 0x000BDFFA File Offset: 0x000BC1FA
		public static DbProviderFactory GetFactory(DbConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			return connection.ProviderFactory;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that contains information about all installed providers that implement <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <returns>Returns a <see cref="T:System.Data.DataTable" /> containing <see cref="T:System.Data.DataRow" /> objects that contain the following data. Column ordinalColumn nameDescription0NameHuman-readable name for the data provider.1DescriptionHuman-readable description of the data provider.2InvariantNameName that can be used programmatically to refer to the data provider.3AssemblyQualifiedNameFully qualified name of the factory class, which contains enough information to instantiate the object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002ADA RID: 10970 RVA: 0x000BE010 File Offset: 0x000BC210
		public static DataTable GetFactoryClasses()
		{
			DataTable dataTable = DbProviderFactories.GetProviderTable();
			if (dataTable != null)
			{
				dataTable = dataTable.Copy();
			}
			else
			{
				dataTable = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
			}
			return dataTable;
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000BE038 File Offset: 0x000BC238
		private static DataTable IncludeFrameworkFactoryClasses(DataTable configDataTable)
		{
			DataTable dataTable = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
			string text = typeof(SqlClientFactory).AssemblyQualifiedName.ToString().Replace("System.Data.SqlClient.SqlClientFactory, System.Data,", "System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient,");
			DbProviderFactoryConfigSection[] array = new DbProviderFactoryConfigSection[]
			{
				new DbProviderFactoryConfigSection(typeof(OdbcFactory), "Odbc Data Provider", ".Net Framework Data Provider for Odbc"),
				new DbProviderFactoryConfigSection(typeof(OleDbFactory), "OleDb Data Provider", ".Net Framework Data Provider for OleDb"),
				new DbProviderFactoryConfigSection("OracleClient Data Provider", "System.Data.OracleClient", ".Net Framework Data Provider for Oracle", text),
				new DbProviderFactoryConfigSection(typeof(SqlClientFactory), "SqlClient Data Provider", ".Net Framework Data Provider for SqlServer")
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsNull())
				{
					bool flag = false;
					if (i == 2)
					{
						Type type = Type.GetType(array[i].AssemblyQualifiedName);
						if (type != null)
						{
							FieldInfo field = type.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
							if (null != field && field.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
							{
								object value = field.GetValue(null);
								if (value != null)
								{
									flag = true;
								}
							}
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["Name"] = array[i].Name;
						dataRow["InvariantName"] = array[i].InvariantName;
						dataRow["Description"] = array[i].Description;
						dataRow["AssemblyQualifiedName"] = array[i].AssemblyQualifiedName;
						dataTable.Rows.Add(dataRow);
					}
				}
			}
			int num = 0;
			while (configDataTable != null && num < configDataTable.Rows.Count)
			{
				try
				{
					bool flag2 = false;
					if (configDataTable.Rows[num]["AssemblyQualifiedName"].ToString().ToLowerInvariant().Contains("System.Data.OracleClient".ToString().ToLowerInvariant()))
					{
						Type type2 = Type.GetType(configDataTable.Rows[num]["AssemblyQualifiedName"].ToString());
						if (type2 != null)
						{
							FieldInfo field2 = type2.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
							if (null != field2 && field2.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
							{
								object value2 = field2.GetValue(null);
								if (value2 != null)
								{
									flag2 = true;
								}
							}
						}
					}
					else
					{
						flag2 = true;
					}
					if (flag2)
					{
						dataTable.Rows.Add(configDataTable.Rows[num].ItemArray);
					}
				}
				catch (ConstraintException)
				{
				}
				num++;
			}
			return dataTable;
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000BE2E0 File Offset: 0x000BC4E0
		private static DataTable GetProviderTable()
		{
			DbProviderFactories.Initialize();
			return DbProviderFactories._providerTable;
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000BE2EC File Offset: 0x000BC4EC
		private static void Initialize()
		{
			if (ConnectionState.Open != DbProviderFactories._initState)
			{
				object lockobj = DbProviderFactories._lockobj;
				lock (lockobj)
				{
					ConnectionState initState = DbProviderFactories._initState;
					if (initState != ConnectionState.Closed)
					{
						if (initState - ConnectionState.Open > 1)
						{
						}
					}
					else
					{
						DbProviderFactories._initState = ConnectionState.Connecting;
						try
						{
							DataSet dataSet = PrivilegedConfigurationManager.GetSection("system.data") as DataSet;
							DbProviderFactories._providerTable = ((dataSet != null) ? DbProviderFactories.IncludeFrameworkFactoryClasses(dataSet.Tables["DbProviderFactories"]) : DbProviderFactories.IncludeFrameworkFactoryClasses(null));
						}
						finally
						{
							DbProviderFactories._initState = ConnectionState.Open;
						}
					}
				}
			}
		}

		// Token: 0x040019F9 RID: 6649
		private const string AssemblyQualifiedName = "AssemblyQualifiedName";

		// Token: 0x040019FA RID: 6650
		private const string Instance = "Instance";

		// Token: 0x040019FB RID: 6651
		private const string InvariantName = "InvariantName";

		// Token: 0x040019FC RID: 6652
		private const string Name = "Name";

		// Token: 0x040019FD RID: 6653
		private const string Description = "Description";

		// Token: 0x040019FE RID: 6654
		private static ConnectionState _initState;

		// Token: 0x040019FF RID: 6655
		private static DataTable _providerTable;

		// Token: 0x04001A00 RID: 6656
		private static object _lockobj = new object();
	}
}
