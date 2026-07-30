using System;
using System.Data.Common;
using System.Globalization;
using System.IO;

namespace System.Data.ProviderBase
{
	// Token: 0x02000316 RID: 790
	internal class DbMetaDataFactory
	{
		// Token: 0x0600230E RID: 8974 RVA: 0x000A2CE6 File Offset: 0x000A0EE6
		public DbMetaDataFactory(Stream xmlStream, string serverVersion, string normalizedServerVersion)
		{
			ADP.CheckArgumentNull(xmlStream, "xmlStream");
			ADP.CheckArgumentNull(serverVersion, "serverVersion");
			ADP.CheckArgumentNull(normalizedServerVersion, "normalizedServerVersion");
			this.LoadDataSetFromXml(xmlStream);
			this._serverVersionString = serverVersion;
			this._normalizedServerVersion = normalizedServerVersion;
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x000A2D24 File Offset: 0x000A0F24
		protected DataSet CollectionDataSet
		{
			get
			{
				return this._metaDataCollectionsDataSet;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x000A2D2C File Offset: 0x000A0F2C
		protected string ServerVersion
		{
			get
			{
				return this._serverVersionString;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x000A2D34 File Offset: 0x000A0F34
		protected string ServerVersionNormalized
		{
			get
			{
				return this._normalizedServerVersion;
			}
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000A2D3C File Offset: 0x000A0F3C
		protected DataTable CloneAndFilterCollection(string collectionName, string[] hiddenColumnNames)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[collectionName];
			if (dataTable == null || collectionName != dataTable.TableName)
			{
				throw ADP.DataTableDoesNotExist(collectionName);
			}
			DataTable dataTable2 = new DataTable(collectionName)
			{
				Locale = CultureInfo.InvariantCulture
			};
			DataColumnCollection columns = dataTable2.Columns;
			DataColumn[] array = this.FilterColumns(dataTable, hiddenColumnNames, columns);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (this.SupportedByCurrentVersion(dataRow))
				{
					DataRow dataRow2 = dataTable2.NewRow();
					for (int i = 0; i < columns.Count; i++)
					{
						dataRow2[columns[i]] = dataRow[array[i], DataRowVersion.Current];
					}
					dataTable2.Rows.Add(dataRow2);
					dataRow2.AcceptChanges();
				}
			}
			return dataTable2;
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000A2E40 File Offset: 0x000A1040
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000A2E49 File Offset: 0x000A1049
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._normalizedServerVersion = null;
				this._serverVersionString = null;
				this._metaDataCollectionsDataSet.Dispose();
			}
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000A2E68 File Offset: 0x000A1068
		private DataTable ExecuteCommand(DataRow requestedCollectionRow, string[] restrictions, DbConnection connection)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			DataColumn dataColumn = dataTable.Columns["PopulationString"];
			DataColumn dataColumn2 = dataTable.Columns["NumberOfRestrictions"];
			DataColumn dataColumn3 = dataTable.Columns["CollectionName"];
			DataTable dataTable2 = null;
			string text = requestedCollectionRow[dataColumn, DataRowVersion.Current] as string;
			int num = (int)requestedCollectionRow[dataColumn2, DataRowVersion.Current];
			string text2 = requestedCollectionRow[dataColumn3, DataRowVersion.Current] as string;
			if (restrictions != null && restrictions.Length > num)
			{
				throw ADP.TooManyRestrictions(text2);
			}
			DbCommand dbCommand = connection.CreateCommand();
			dbCommand.CommandText = text;
			dbCommand.CommandTimeout = Math.Max(dbCommand.CommandTimeout, 180);
			for (int i = 0; i < num; i++)
			{
				DbParameter dbParameter = dbCommand.CreateParameter();
				if (restrictions != null && restrictions.Length > i && restrictions[i] != null)
				{
					dbParameter.Value = restrictions[i];
				}
				else
				{
					dbParameter.Value = DBNull.Value;
				}
				dbParameter.ParameterName = this.GetParameterName(text2, i + 1);
				dbParameter.Direction = ParameterDirection.Input;
				dbCommand.Parameters.Add(dbParameter);
			}
			DbDataReader dbDataReader = null;
			try
			{
				try
				{
					dbDataReader = dbCommand.ExecuteReader();
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					throw ADP.QueryFailed(text2, ex);
				}
				dataTable2 = new DataTable(text2)
				{
					Locale = CultureInfo.InvariantCulture
				};
				foreach (object obj in dbDataReader.GetSchemaTable().Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataTable2.Columns.Add(dataRow["ColumnName"] as string, (Type)dataRow["DataType"]);
				}
				object[] array = new object[dataTable2.Columns.Count];
				while (dbDataReader.Read())
				{
					dbDataReader.GetValues(array);
					dataTable2.Rows.Add(array);
				}
			}
			finally
			{
				if (dbDataReader != null)
				{
					dbDataReader.Dispose();
					dbDataReader = null;
				}
			}
			return dataTable2;
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x000A30BC File Offset: 0x000A12BC
		private DataColumn[] FilterColumns(DataTable sourceTable, string[] hiddenColumnNames, DataColumnCollection destinationColumns)
		{
			DataColumn[] array = null;
			int num = 0;
			foreach (object obj in sourceTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (this.IncludeThisColumn(dataColumn, hiddenColumnNames))
				{
					num++;
				}
			}
			if (num == 0)
			{
				throw ADP.NoColumns();
			}
			int num2 = 0;
			array = new DataColumn[num];
			foreach (object obj2 in sourceTable.Columns)
			{
				DataColumn dataColumn2 = (DataColumn)obj2;
				if (this.IncludeThisColumn(dataColumn2, hiddenColumnNames))
				{
					DataColumn dataColumn3 = new DataColumn(dataColumn2.ColumnName, dataColumn2.DataType);
					destinationColumns.Add(dataColumn3);
					array[num2] = dataColumn2;
					num2++;
				}
			}
			return array;
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000A31B4 File Offset: 0x000A13B4
		internal DataRow FindMetaDataCollectionRow(string collectionName)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			if (dataTable == null)
			{
				throw ADP.InvalidXml();
			}
			DataColumn dataColumn = dataTable.Columns[DbMetaDataColumnNames.CollectionName];
			if (dataColumn == null || typeof(string) != dataColumn.DataType)
			{
				throw ADP.InvalidXmlMissingColumn(DbMetaDataCollectionNames.MetaDataCollections, DbMetaDataColumnNames.CollectionName);
			}
			DataRow dataRow = null;
			string text = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow2 = (DataRow)obj;
				string text2 = dataRow2[dataColumn, DataRowVersion.Current] as string;
				if (string.IsNullOrEmpty(text2))
				{
					throw ADP.InvalidXmlInvalidValue(DbMetaDataCollectionNames.MetaDataCollections, DbMetaDataColumnNames.CollectionName);
				}
				if (ADP.CompareInsensitiveInvariant(text2, collectionName))
				{
					if (!this.SupportedByCurrentVersion(dataRow2))
					{
						flag = true;
					}
					else if (collectionName == text2)
					{
						if (flag2)
						{
							throw ADP.CollectionNameIsNotUnique(collectionName);
						}
						dataRow = dataRow2;
						text = text2;
						flag2 = true;
					}
					else
					{
						if (text != null)
						{
							flag3 = true;
						}
						dataRow = dataRow2;
						text = text2;
					}
				}
			}
			if (dataRow == null)
			{
				if (!flag)
				{
					throw ADP.UndefinedCollection(collectionName);
				}
				throw ADP.UnsupportedVersion(collectionName);
			}
			else
			{
				if (!flag2 && flag3)
				{
					throw ADP.AmbigousCollectionName(collectionName);
				}
				return dataRow;
			}
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000A330C File Offset: 0x000A150C
		private void FixUpVersion(DataTable dataSourceInfoTable)
		{
			DataColumn dataColumn = dataSourceInfoTable.Columns["DataSourceProductVersion"];
			DataColumn dataColumn2 = dataSourceInfoTable.Columns["DataSourceProductVersionNormalized"];
			if (dataColumn == null || dataColumn2 == null)
			{
				throw ADP.MissingDataSourceInformationColumn();
			}
			if (dataSourceInfoTable.Rows.Count != 1)
			{
				throw ADP.IncorrectNumberOfDataSourceInformationRows();
			}
			DataRow dataRow = dataSourceInfoTable.Rows[0];
			dataRow[dataColumn] = this._serverVersionString;
			dataRow[dataColumn2] = this._normalizedServerVersion;
			dataRow.AcceptChanges();
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x000A3388 File Offset: 0x000A1588
		private string GetParameterName(string neededCollectionName, int neededRestrictionNumber)
		{
			DataColumn dataColumn = null;
			DataColumn dataColumn2 = null;
			DataColumn dataColumn3 = null;
			DataColumn dataColumn4 = null;
			string text = null;
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.Restrictions];
			if (dataTable != null)
			{
				DataColumnCollection columns = dataTable.Columns;
				if (columns != null)
				{
					dataColumn = columns["CollectionName"];
					dataColumn2 = columns["ParameterName"];
					dataColumn3 = columns["RestrictionName"];
					dataColumn4 = columns["RestrictionNumber"];
				}
			}
			if (dataColumn2 == null || dataColumn == null || dataColumn3 == null || dataColumn4 == null)
			{
				throw ADP.MissingRestrictionColumn();
			}
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if ((string)dataRow[dataColumn] == neededCollectionName && (int)dataRow[dataColumn4] == neededRestrictionNumber && this.SupportedByCurrentVersion(dataRow))
				{
					text = (string)dataRow[dataColumn2];
					break;
				}
			}
			if (text == null)
			{
				throw ADP.MissingRestrictionRow();
			}
			return text;
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x000A34AC File Offset: 0x000A16AC
		public virtual DataTable GetSchema(DbConnection connection, string collectionName, string[] restrictions)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			DataColumn dataColumn = dataTable.Columns["PopulationMechanism"];
			DataColumn dataColumn2 = dataTable.Columns[DbMetaDataColumnNames.CollectionName];
			DataRow dataRow = this.FindMetaDataCollectionRow(collectionName);
			string text = dataRow[dataColumn2, DataRowVersion.Current] as string;
			if (!ADP.IsEmptyArray(restrictions))
			{
				for (int i = 0; i < restrictions.Length; i++)
				{
					if (restrictions[i] != null && restrictions[i].Length > 4096)
					{
						throw ADP.NotSupported();
					}
				}
			}
			string text2 = dataRow[dataColumn, DataRowVersion.Current] as string;
			DataTable dataTable2;
			if (!(text2 == "DataTable"))
			{
				if (!(text2 == "SQLCommand"))
				{
					if (!(text2 == "PrepareCollection"))
					{
						throw ADP.UndefinedPopulationMechanism(text2);
					}
					dataTable2 = this.PrepareCollection(text, restrictions, connection);
				}
				else
				{
					dataTable2 = this.ExecuteCommand(dataRow, restrictions, connection);
				}
			}
			else
			{
				string[] array;
				if (text == DbMetaDataCollectionNames.MetaDataCollections)
				{
					array = new string[] { "PopulationMechanism", "PopulationString" };
				}
				else
				{
					array = null;
				}
				if (!ADP.IsEmptyArray(restrictions))
				{
					throw ADP.TooManyRestrictions(text);
				}
				dataTable2 = this.CloneAndFilterCollection(text, array);
				if (text == DbMetaDataCollectionNames.DataSourceInformation)
				{
					this.FixUpVersion(dataTable2);
				}
			}
			return dataTable2;
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x000A3608 File Offset: 0x000A1808
		private bool IncludeThisColumn(DataColumn sourceColumn, string[] hiddenColumnNames)
		{
			bool flag = true;
			string columnName = sourceColumn.ColumnName;
			if (columnName == "MinimumVersion" || columnName == "MaximumVersion")
			{
				flag = false;
			}
			else if (hiddenColumnNames != null)
			{
				for (int i = 0; i < hiddenColumnNames.Length; i++)
				{
					if (hiddenColumnNames[i] == columnName)
					{
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000A365D File Offset: 0x000A185D
		private void LoadDataSetFromXml(Stream XmlStream)
		{
			this._metaDataCollectionsDataSet = new DataSet();
			this._metaDataCollectionsDataSet.Locale = CultureInfo.InvariantCulture;
			this._metaDataCollectionsDataSet.ReadXml(XmlStream);
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x000621D6 File Offset: 0x000603D6
		protected virtual DataTable PrepareCollection(string collectionName, string[] restrictions, DbConnection connection)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x000A3688 File Offset: 0x000A1888
		private bool SupportedByCurrentVersion(DataRow requestedCollectionRow)
		{
			bool flag = true;
			DataColumnCollection columns = requestedCollectionRow.Table.Columns;
			DataColumn dataColumn = columns["MinimumVersion"];
			if (dataColumn != null)
			{
				object obj = requestedCollectionRow[dataColumn];
				if (obj != null && obj != DBNull.Value && 0 > string.Compare(this._normalizedServerVersion, (string)obj, StringComparison.OrdinalIgnoreCase))
				{
					flag = false;
				}
			}
			if (flag)
			{
				dataColumn = columns["MaximumVersion"];
				if (dataColumn != null)
				{
					object obj = requestedCollectionRow[dataColumn];
					if (obj != null && obj != DBNull.Value && 0 < string.Compare(this._normalizedServerVersion, (string)obj, StringComparison.OrdinalIgnoreCase))
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x04001739 RID: 5945
		private DataSet _metaDataCollectionsDataSet;

		// Token: 0x0400173A RID: 5946
		private string _normalizedServerVersion;

		// Token: 0x0400173B RID: 5947
		private string _serverVersionString;

		// Token: 0x0400173C RID: 5948
		private const string _collectionName = "CollectionName";

		// Token: 0x0400173D RID: 5949
		private const string _populationMechanism = "PopulationMechanism";

		// Token: 0x0400173E RID: 5950
		private const string _populationString = "PopulationString";

		// Token: 0x0400173F RID: 5951
		private const string _maximumVersion = "MaximumVersion";

		// Token: 0x04001740 RID: 5952
		private const string _minimumVersion = "MinimumVersion";

		// Token: 0x04001741 RID: 5953
		private const string _dataSourceProductVersionNormalized = "DataSourceProductVersionNormalized";

		// Token: 0x04001742 RID: 5954
		private const string _dataSourceProductVersion = "DataSourceProductVersion";

		// Token: 0x04001743 RID: 5955
		private const string _restrictionDefault = "RestrictionDefault";

		// Token: 0x04001744 RID: 5956
		private const string _restrictionNumber = "RestrictionNumber";

		// Token: 0x04001745 RID: 5957
		private const string _numberOfRestrictions = "NumberOfRestrictions";

		// Token: 0x04001746 RID: 5958
		private const string _restrictionName = "RestrictionName";

		// Token: 0x04001747 RID: 5959
		private const string _parameterName = "ParameterName";

		// Token: 0x04001748 RID: 5960
		private const string _dataTable = "DataTable";

		// Token: 0x04001749 RID: 5961
		private const string _sqlCommand = "SQLCommand";

		// Token: 0x0400174A RID: 5962
		private const string _prepareCollection = "PrepareCollection";
	}
}
