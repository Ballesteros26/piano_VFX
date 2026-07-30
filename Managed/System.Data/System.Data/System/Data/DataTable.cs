using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data
{
	/// <summary>Represents one table of in-memory data.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200008C RID: 140
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("TableName")]
	[DefaultEvent("RowChanging")]
	[XmlSchemaProvider("GetDataTableSchema")]
	[Serializable]
	public class DataTable : MarshalByValueComponent, IListSource, ISupportInitializeNotification, ISupportInitialize, ISerializable, IXmlSerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTable" /> class with no arguments.</summary>
		// Token: 0x0600074B RID: 1867 RVA: 0x0001E844 File Offset: 0x0001CA44
		public DataTable()
		{
			GC.SuppressFinalize(this);
			DataCommonEventSource.Log.Trace<int>("<ds.DataTable.DataTable|API> {0}", this.ObjectID);
			this._nextRowID = 1L;
			this._recordManager = new RecordManager(this);
			this._culture = CultureInfo.CurrentCulture;
			this._columnCollection = new DataColumnCollection(this);
			this._constraintCollection = new ConstraintCollection(this);
			this._rowCollection = new DataRowCollection(this);
			this._indexes = new List<Index>();
			this._rowBuilder = new DataRowBuilder(this, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTable" /> class with the specified table name.</summary>
		/// <param name="tableName">The name to give the table. If <paramref name="tableName" /> is null or an empty string, a default name is given when added to the <see cref="T:System.Data.DataTableCollection" />. </param>
		// Token: 0x0600074C RID: 1868 RVA: 0x0001E95D File Offset: 0x0001CB5D
		public DataTable(string tableName)
			: this()
		{
			this._tableName = ((tableName == null) ? "" : tableName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTable" /> class using the specified table name and namespace.</summary>
		/// <param name="tableName">The name to give the table. If <paramref name="tableName" /> is null or an empty string, a default name is given when added to the <see cref="T:System.Data.DataTableCollection" />. </param>
		/// <param name="tableNamespace">The namespace for the XML representation of the data stored in the DataTable. </param>
		// Token: 0x0600074D RID: 1869 RVA: 0x0001E976 File Offset: 0x0001CB76
		public DataTable(string tableName, string tableNamespace)
			: this(tableName)
		{
			this.Namespace = tableNamespace;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTable" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object.</param>
		/// <param name="context">The source and destination of a given serialized stream. </param>
		// Token: 0x0600074E RID: 1870 RVA: 0x0001E988 File Offset: 0x0001CB88
		protected DataTable(SerializationInfo info, StreamingContext context)
			: this()
		{
			bool flag = context.Context == null || Convert.ToBoolean(context.Context, CultureInfo.InvariantCulture);
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (name == "DataTable.RemotingFormat")
				{
					serializationFormat = (SerializationFormat)enumerator.Value;
				}
			}
			this.DeserializeDataTable(info, context, flag, serializationFormat);
		}

		/// <summary>Populates a serialization information object with the data needed to serialize the <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized data associated with the <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Data.DataTable" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x0600074F RID: 1871 RVA: 0x0001E9F8 File Offset: 0x0001CBF8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat remotingFormat = this.RemotingFormat;
			bool flag = context.Context == null || Convert.ToBoolean(context.Context, CultureInfo.InvariantCulture);
			this.SerializeDataTable(info, context, flag, remotingFormat);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001EA34 File Offset: 0x0001CC34
		private void SerializeDataTable(SerializationInfo info, StreamingContext context, bool isSingleTable, SerializationFormat remotingFormat)
		{
			info.AddValue("DataTable.RemotingVersion", new Version(2, 0));
			if (remotingFormat != SerializationFormat.Xml)
			{
				info.AddValue("DataTable.RemotingFormat", remotingFormat);
			}
			if (remotingFormat != SerializationFormat.Xml)
			{
				this.SerializeTableSchema(info, context, isSingleTable);
				if (isSingleTable)
				{
					this.SerializeTableData(info, context, 0);
					return;
				}
			}
			else
			{
				string text = string.Empty;
				bool flag = false;
				if (this._dataSet == null)
				{
					DataSet dataSet = new DataSet("tmpDataSet");
					dataSet.SetLocaleValue(this._culture, this._cultureUserSet);
					dataSet.CaseSensitive = this.CaseSensitive;
					dataSet._namespaceURI = this.Namespace;
					dataSet.Tables.Add(this);
					flag = true;
				}
				else
				{
					text = this.DataSet.Namespace;
					this.DataSet._namespaceURI = this.Namespace;
				}
				info.AddValue("XmlSchema", this._dataSet.GetXmlSchemaForRemoting(this));
				info.AddValue("XmlDiffGram", this._dataSet.GetRemotingDiffGram(this));
				if (flag)
				{
					this._dataSet.Tables.Remove(this);
					return;
				}
				this._dataSet._namespaceURI = text;
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001EB48 File Offset: 0x0001CD48
		internal void DeserializeDataTable(SerializationInfo info, StreamingContext context, bool isSingleTable, SerializationFormat remotingFormat)
		{
			if (remotingFormat != SerializationFormat.Xml)
			{
				this.DeserializeTableSchema(info, context, isSingleTable);
				if (isSingleTable)
				{
					this.DeserializeTableData(info, context, 0);
					this.ResetIndexes();
					return;
				}
			}
			else
			{
				string text = (string)info.GetValue("XmlSchema", typeof(string));
				string text2 = (string)info.GetValue("XmlDiffGram", typeof(string));
				if (text != null)
				{
					DataSet dataSet = new DataSet();
					dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(text)));
					DataTable dataTable = dataSet.Tables[0];
					dataTable.CloneTo(this, null, false);
					this.Namespace = dataTable.Namespace;
					if (text2 != null)
					{
						dataSet.Tables.Remove(dataSet.Tables[0]);
						dataSet.Tables.Add(this);
						dataSet.ReadXml(new XmlTextReader(new StringReader(text2)), XmlReadMode.DiffGram);
						dataSet.Tables.Remove(this);
					}
				}
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001EC30 File Offset: 0x0001CE30
		internal void SerializeTableSchema(SerializationInfo info, StreamingContext context, bool isSingleTable)
		{
			info.AddValue("DataTable.TableName", this.TableName);
			info.AddValue("DataTable.Namespace", this.Namespace);
			info.AddValue("DataTable.Prefix", this.Prefix);
			info.AddValue("DataTable.CaseSensitive", this._caseSensitive);
			info.AddValue("DataTable.caseSensitiveAmbient", !this._caseSensitiveUserSet);
			info.AddValue("DataTable.LocaleLCID", this.Locale.LCID);
			info.AddValue("DataTable.MinimumCapacity", this._recordManager.MinimumCapacity);
			info.AddValue("DataTable.NestedInDataSet", this._fNestedInDataset);
			info.AddValue("DataTable.TypeName", this.TypeName.ToString());
			info.AddValue("DataTable.RepeatableElement", this._repeatableElement);
			info.AddValue("DataTable.ExtendedProperties", this.ExtendedProperties);
			info.AddValue("DataTable.Columns.Count", this.Columns.Count);
			if (isSingleTable && !this.CheckForClosureOnExpressionTables(new List<DataTable> { this }))
			{
				throw ExceptionBuilder.CanNotRemoteDataTable();
			}
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			for (int i = 0; i < this.Columns.Count; i++)
			{
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ColumnName", i), this.Columns[i].ColumnName);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Namespace", i), this.Columns[i]._columnUri);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Prefix", i), this.Columns[i].Prefix);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ColumnMapping", i), this.Columns[i].ColumnMapping);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AllowDBNull", i), this.Columns[i].AllowDBNull);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrement", i), this.Columns[i].AutoIncrement);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrementStep", i), this.Columns[i].AutoIncrementStep);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrementSeed", i), this.Columns[i].AutoIncrementSeed);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Caption", i), this.Columns[i].Caption);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.DefaultValue", i), this.Columns[i].DefaultValue);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ReadOnly", i), this.Columns[i].ReadOnly);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.MaxLength", i), this.Columns[i].MaxLength);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.DataType", i), this.Columns[i].DataType);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.XmlDataType", i), this.Columns[i].XmlDataType);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.SimpleType", i), this.Columns[i].SimpleType);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.DateTimeMode", i), this.Columns[i].DateTimeMode);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrementCurrent", i), this.Columns[i].AutoIncrementCurrent);
				if (isSingleTable)
				{
					info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Expression", i), this.Columns[i].Expression);
				}
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ExtendedProperties", i), this.Columns[i]._extendedProperties);
			}
			if (isSingleTable)
			{
				this.SerializeConstraints(info, context, 0, false);
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001F074 File Offset: 0x0001D274
		internal void DeserializeTableSchema(SerializationInfo info, StreamingContext context, bool isSingleTable)
		{
			this._tableName = info.GetString("DataTable.TableName");
			this._tableNamespace = info.GetString("DataTable.Namespace");
			this._tablePrefix = info.GetString("DataTable.Prefix");
			bool boolean = info.GetBoolean("DataTable.CaseSensitive");
			this.SetCaseSensitiveValue(boolean, true, false);
			this._caseSensitiveUserSet = !info.GetBoolean("DataTable.caseSensitiveAmbient");
			CultureInfo cultureInfo = new CultureInfo((int)info.GetValue("DataTable.LocaleLCID", typeof(int)));
			this.SetLocaleValue(cultureInfo, true, false);
			this._cultureUserSet = true;
			this.MinimumCapacity = info.GetInt32("DataTable.MinimumCapacity");
			this._fNestedInDataset = info.GetBoolean("DataTable.NestedInDataSet");
			string @string = info.GetString("DataTable.TypeName");
			this._typeName = new XmlQualifiedName(@string);
			this._repeatableElement = info.GetBoolean("DataTable.RepeatableElement");
			this._extendedProperties = (PropertyCollection)info.GetValue("DataTable.ExtendedProperties", typeof(PropertyCollection));
			int @int = info.GetInt32("DataTable.Columns.Count");
			string[] array = new string[@int];
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			for (int i = 0; i < @int; i++)
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.ColumnName = info.GetString(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ColumnName", i));
				dataColumn._columnUri = info.GetString(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Namespace", i));
				dataColumn.Prefix = info.GetString(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Prefix", i));
				dataColumn.DataType = (Type)info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.DataType", i), typeof(Type));
				dataColumn.XmlDataType = (string)info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.XmlDataType", i), typeof(string));
				dataColumn.SimpleType = (SimpleType)info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.SimpleType", i), typeof(SimpleType));
				dataColumn.ColumnMapping = (MappingType)info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ColumnMapping", i), typeof(MappingType));
				dataColumn.DateTimeMode = (DataSetDateTime)info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.DateTimeMode", i), typeof(DataSetDateTime));
				dataColumn.AllowDBNull = info.GetBoolean(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AllowDBNull", i));
				dataColumn.AutoIncrement = info.GetBoolean(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrement", i));
				dataColumn.AutoIncrementStep = info.GetInt64(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrementStep", i));
				dataColumn.AutoIncrementSeed = info.GetInt64(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrementSeed", i));
				dataColumn.Caption = info.GetString(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Caption", i));
				dataColumn.DefaultValue = info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.DefaultValue", i), typeof(object));
				dataColumn.ReadOnly = info.GetBoolean(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ReadOnly", i));
				dataColumn.MaxLength = info.GetInt32(string.Format(invariantCulture, "DataTable.DataColumn_{0}.MaxLength", i));
				dataColumn.AutoIncrementCurrent = info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrementCurrent", i), typeof(object));
				if (isSingleTable)
				{
					array[i] = info.GetString(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Expression", i));
				}
				dataColumn._extendedProperties = (PropertyCollection)info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ExtendedProperties", i), typeof(PropertyCollection));
				this.Columns.Add(dataColumn);
			}
			if (isSingleTable)
			{
				for (int j = 0; j < @int; j++)
				{
					if (array[j] != null)
					{
						this.Columns[j].Expression = array[j];
					}
				}
			}
			if (isSingleTable)
			{
				this.DeserializeConstraints(info, context, 0, false);
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001F4CC File Offset: 0x0001D6CC
		internal void SerializeConstraints(SerializationInfo info, StreamingContext context, int serIndex, bool allConstraints)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.Constraints.Count; i++)
			{
				Constraint constraint = this.Constraints[i];
				UniqueConstraint uniqueConstraint = constraint as UniqueConstraint;
				if (uniqueConstraint != null)
				{
					int[] array = new int[uniqueConstraint.Columns.Length];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = uniqueConstraint.Columns[j].Ordinal;
					}
					arrayList.Add(new ArrayList { "U", uniqueConstraint.ConstraintName, array, uniqueConstraint.IsPrimaryKey, uniqueConstraint.ExtendedProperties });
				}
				else
				{
					ForeignKeyConstraint foreignKeyConstraint = constraint as ForeignKeyConstraint;
					if (allConstraints || (foreignKeyConstraint.Table == this && foreignKeyConstraint.RelatedTable == this))
					{
						int[] array2 = new int[foreignKeyConstraint.RelatedColumns.Length + 1];
						array2[0] = (allConstraints ? this.DataSet.Tables.IndexOf(foreignKeyConstraint.RelatedTable) : 0);
						for (int k = 1; k < array2.Length; k++)
						{
							array2[k] = foreignKeyConstraint.RelatedColumns[k - 1].Ordinal;
						}
						int[] array3 = new int[foreignKeyConstraint.Columns.Length + 1];
						array3[0] = (allConstraints ? this.DataSet.Tables.IndexOf(foreignKeyConstraint.Table) : 0);
						for (int l = 1; l < array3.Length; l++)
						{
							array3[l] = foreignKeyConstraint.Columns[l - 1].Ordinal;
						}
						arrayList.Add(new ArrayList
						{
							"F",
							foreignKeyConstraint.ConstraintName,
							array2,
							array3,
							new int[]
							{
								(int)foreignKeyConstraint.AcceptRejectRule,
								(int)foreignKeyConstraint.UpdateRule,
								(int)foreignKeyConstraint.DeleteRule
							},
							foreignKeyConstraint.ExtendedProperties
						});
					}
				}
			}
			info.AddValue(string.Format(CultureInfo.InvariantCulture, "DataTable_{0}.Constraints", serIndex), arrayList);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001F724 File Offset: 0x0001D924
		internal void DeserializeConstraints(SerializationInfo info, StreamingContext context, int serIndex, bool allConstraints)
		{
			foreach (object obj in ((ArrayList)info.GetValue(string.Format(CultureInfo.InvariantCulture, "DataTable_{0}.Constraints", serIndex), typeof(ArrayList))))
			{
				ArrayList arrayList = (ArrayList)obj;
				if (((string)arrayList[0]).Equals("U"))
				{
					string text = (string)arrayList[1];
					int[] array = (int[])arrayList[2];
					bool flag = (bool)arrayList[3];
					PropertyCollection propertyCollection = (PropertyCollection)arrayList[4];
					DataColumn[] array2 = new DataColumn[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = this.Columns[array[i]];
					}
					UniqueConstraint uniqueConstraint = new UniqueConstraint(text, array2, flag);
					uniqueConstraint._extendedProperties = propertyCollection;
					this.Constraints.Add(uniqueConstraint);
				}
				else
				{
					string text2 = (string)arrayList[1];
					int[] array3 = (int[])arrayList[2];
					int[] array4 = (int[])arrayList[3];
					int[] array5 = (int[])arrayList[4];
					PropertyCollection propertyCollection2 = (PropertyCollection)arrayList[5];
					DataTable dataTable = ((!allConstraints) ? this : this.DataSet.Tables[array3[0]]);
					DataColumn[] array6 = new DataColumn[array3.Length - 1];
					for (int j = 0; j < array6.Length; j++)
					{
						array6[j] = dataTable.Columns[array3[j + 1]];
					}
					DataTable dataTable2 = ((!allConstraints) ? this : this.DataSet.Tables[array4[0]]);
					DataColumn[] array7 = new DataColumn[array4.Length - 1];
					for (int k = 0; k < array7.Length; k++)
					{
						array7[k] = dataTable2.Columns[array4[k + 1]];
					}
					ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(text2, array6, array7);
					foreignKeyConstraint.AcceptRejectRule = (AcceptRejectRule)array5[0];
					foreignKeyConstraint.UpdateRule = (Rule)array5[1];
					foreignKeyConstraint.DeleteRule = (Rule)array5[2];
					foreignKeyConstraint._extendedProperties = propertyCollection2;
					this.Constraints.Add(foreignKeyConstraint, false);
				}
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001F98C File Offset: 0x0001DB8C
		internal void SerializeExpressionColumns(SerializationInfo info, StreamingContext context, int serIndex)
		{
			int count = this.Columns.Count;
			for (int i = 0; i < count; i++)
			{
				info.AddValue(string.Format(CultureInfo.InvariantCulture, "DataTable_{0}.DataColumn_{1}.Expression", serIndex, i), this.Columns[i].Expression);
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001F9E4 File Offset: 0x0001DBE4
		internal void DeserializeExpressionColumns(SerializationInfo info, StreamingContext context, int serIndex)
		{
			int count = this.Columns.Count;
			for (int i = 0; i < count; i++)
			{
				string @string = info.GetString(string.Format(CultureInfo.InvariantCulture, "DataTable_{0}.DataColumn_{1}.Expression", serIndex, i));
				if (@string.Length != 0)
				{
					this.Columns[i].Expression = @string;
				}
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001FA48 File Offset: 0x0001DC48
		internal void SerializeTableData(SerializationInfo info, StreamingContext context, int serIndex)
		{
			int count = this.Columns.Count;
			int count2 = this.Rows.Count;
			int num = 0;
			int num2 = 0;
			BitArray bitArray = new BitArray(count2 * 3, false);
			int i = 0;
			while (i < count2)
			{
				int num3 = i * 3;
				DataRow dataRow = this.Rows[i];
				DataRowState rowState = dataRow.RowState;
				if (rowState <= DataRowState.Added)
				{
					if (rowState != DataRowState.Unchanged)
					{
						if (rowState != DataRowState.Added)
						{
							goto IL_00A1;
						}
						bitArray[num3 + 1] = true;
					}
				}
				else if (rowState != DataRowState.Deleted)
				{
					if (rowState != DataRowState.Modified)
					{
						goto IL_00A1;
					}
					bitArray[num3] = true;
					num++;
				}
				else
				{
					bitArray[num3] = true;
					bitArray[num3 + 1] = true;
				}
				if (-1 != dataRow._tempRecord)
				{
					bitArray[num3 + 2] = true;
					num2++;
				}
				i++;
				continue;
				IL_00A1:
				throw ExceptionBuilder.InvalidRowState(rowState);
			}
			int num4 = count2 + num + num2;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			if (num4 > 0)
			{
				for (int j = 0; j < count; j++)
				{
					object emptyColumnStore = this.Columns[j].GetEmptyColumnStore(num4);
					arrayList.Add(emptyColumnStore);
					BitArray bitArray2 = new BitArray(num4);
					arrayList2.Add(bitArray2);
				}
			}
			int num5 = 0;
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			for (int k = 0; k < count2; k++)
			{
				int num6 = this.Rows[k].CopyValuesIntoStore(arrayList, arrayList2, num5);
				this.GetRowAndColumnErrors(k, hashtable, hashtable2);
				num5 += num6;
			}
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.Rows.Count", serIndex), count2);
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.Records.Count", serIndex), num4);
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.RowStates", serIndex), bitArray);
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.Records", serIndex), arrayList);
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.NullBits", serIndex), arrayList2);
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.RowErrors", serIndex), hashtable);
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.ColumnErrors", serIndex), hashtable2);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001FC90 File Offset: 0x0001DE90
		internal void DeserializeTableData(SerializationInfo info, StreamingContext context, int serIndex)
		{
			bool enforceConstraints = this._enforceConstraints;
			bool inDataLoad = this._inDataLoad;
			try
			{
				this._enforceConstraints = false;
				this._inDataLoad = true;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				int @int = info.GetInt32(string.Format(invariantCulture, "DataTable_{0}.Rows.Count", serIndex));
				int int2 = info.GetInt32(string.Format(invariantCulture, "DataTable_{0}.Records.Count", serIndex));
				BitArray bitArray = (BitArray)info.GetValue(string.Format(invariantCulture, "DataTable_{0}.RowStates", serIndex), typeof(BitArray));
				ArrayList arrayList = (ArrayList)info.GetValue(string.Format(invariantCulture, "DataTable_{0}.Records", serIndex), typeof(ArrayList));
				ArrayList arrayList2 = (ArrayList)info.GetValue(string.Format(invariantCulture, "DataTable_{0}.NullBits", serIndex), typeof(ArrayList));
				Hashtable hashtable = (Hashtable)info.GetValue(string.Format(invariantCulture, "DataTable_{0}.RowErrors", serIndex), typeof(Hashtable));
				hashtable.OnDeserialization(this);
				Hashtable hashtable2 = (Hashtable)info.GetValue(string.Format(invariantCulture, "DataTable_{0}.ColumnErrors", serIndex), typeof(Hashtable));
				hashtable2.OnDeserialization(this);
				if (int2 > 0)
				{
					for (int i = 0; i < this.Columns.Count; i++)
					{
						this.Columns[i].SetStorage(arrayList[i], (BitArray)arrayList2[i]);
					}
					int num = 0;
					DataRow[] array = new DataRow[int2];
					for (int j = 0; j < @int; j++)
					{
						DataRow dataRow = this.NewEmptyRow();
						array[num] = dataRow;
						int num2 = j * 3;
						DataRowState dataRowState = this.ConvertToRowState(bitArray, num2);
						if (dataRowState <= DataRowState.Added)
						{
							if (dataRowState != DataRowState.Unchanged)
							{
								if (dataRowState == DataRowState.Added)
								{
									dataRow._oldRecord = -1;
									dataRow._newRecord = num;
									num++;
								}
							}
							else
							{
								dataRow._oldRecord = num;
								dataRow._newRecord = num;
								num++;
							}
						}
						else if (dataRowState != DataRowState.Deleted)
						{
							if (dataRowState == DataRowState.Modified)
							{
								dataRow._oldRecord = num;
								dataRow._newRecord = num + 1;
								array[num + 1] = dataRow;
								num += 2;
							}
						}
						else
						{
							dataRow._oldRecord = num;
							dataRow._newRecord = -1;
							num++;
						}
						if (bitArray[num2 + 2])
						{
							dataRow._tempRecord = num;
							array[num] = dataRow;
							num++;
						}
						else
						{
							dataRow._tempRecord = -1;
						}
						this.Rows.ArrayAdd(dataRow);
						dataRow.rowID = this._nextRowID;
						this._nextRowID += 1L;
						this.ConvertToRowError(j, hashtable, hashtable2);
					}
					this._recordManager.SetRowCache(array);
					this.ResetIndexes();
				}
			}
			finally
			{
				this._enforceConstraints = enforceConstraints;
				this._inDataLoad = inDataLoad;
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001FF88 File Offset: 0x0001E188
		private DataRowState ConvertToRowState(BitArray bitStates, int bitIndex)
		{
			bool flag = bitStates[bitIndex];
			bool flag2 = bitStates[bitIndex + 1];
			if (!flag && !flag2)
			{
				return DataRowState.Unchanged;
			}
			if (!flag && flag2)
			{
				return DataRowState.Added;
			}
			if (flag && !flag2)
			{
				return DataRowState.Modified;
			}
			if (flag && flag2)
			{
				return DataRowState.Deleted;
			}
			throw ExceptionBuilder.InvalidRowBitPattern();
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001FFD0 File Offset: 0x0001E1D0
		internal void GetRowAndColumnErrors(int rowIndex, Hashtable rowErrors, Hashtable colErrors)
		{
			DataRow dataRow = this.Rows[rowIndex];
			if (dataRow.HasErrors)
			{
				rowErrors.Add(rowIndex, dataRow.RowError);
				DataColumn[] columnsInError = dataRow.GetColumnsInError();
				if (columnsInError.Length != 0)
				{
					int[] array = new int[columnsInError.Length];
					string[] array2 = new string[columnsInError.Length];
					for (int i = 0; i < columnsInError.Length; i++)
					{
						array[i] = columnsInError[i].Ordinal;
						array2[i] = dataRow.GetColumnError(columnsInError[i]);
					}
					ArrayList arrayList = new ArrayList();
					arrayList.Add(array);
					arrayList.Add(array2);
					colErrors.Add(rowIndex, arrayList);
				}
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00020078 File Offset: 0x0001E278
		private void ConvertToRowError(int rowIndex, Hashtable rowErrors, Hashtable colErrors)
		{
			DataRow dataRow = this.Rows[rowIndex];
			if (rowErrors.ContainsKey(rowIndex))
			{
				dataRow.RowError = (string)rowErrors[rowIndex];
			}
			if (colErrors.ContainsKey(rowIndex))
			{
				ArrayList arrayList = (ArrayList)colErrors[rowIndex];
				int[] array = (int[])arrayList[0];
				string[] array2 = (string[])arrayList[1];
				for (int i = 0; i < array.Length; i++)
				{
					dataRow.SetColumnError(array[i], array2[i]);
				}
			}
		}

		/// <summary>Indicates whether string comparisons within the table are case-sensitive.</summary>
		/// <returns>true if the comparison is case-sensitive; otherwise false. The default is set to the parent <see cref="T:System.Data.DataSet" /> object's <see cref="P:System.Data.DataSet.CaseSensitive" /> property, or false if the <see cref="T:System.Data.DataTable" /> was created independently of a <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00020109 File Offset: 0x0001E309
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x00020114 File Offset: 0x0001E314
		public bool CaseSensitive
		{
			get
			{
				return this._caseSensitive;
			}
			set
			{
				if (this._caseSensitive != value)
				{
					bool caseSensitive = this._caseSensitive;
					bool caseSensitiveUserSet = this._caseSensitiveUserSet;
					this._caseSensitive = value;
					this._caseSensitiveUserSet = true;
					if (this.DataSet != null && !this.DataSet.ValidateCaseConstraint())
					{
						this._caseSensitive = caseSensitive;
						this._caseSensitiveUserSet = caseSensitiveUserSet;
						throw ExceptionBuilder.CannotChangeCaseLocale();
					}
					this.SetCaseSensitiveValue(value, true, true);
				}
				this._caseSensitiveUserSet = true;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00020180 File Offset: 0x0001E380
		internal bool AreIndexEventsSuspended
		{
			get
			{
				return 0 < this._suspendIndexEvents;
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0002018C File Offset: 0x0001E38C
		internal void RestoreIndexEvents(bool forceReset)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataTable.RestoreIndexEvents|Info> {0}, {1}", this.ObjectID, this._suspendIndexEvents);
			if (0 < this._suspendIndexEvents)
			{
				this._suspendIndexEvents--;
				if (this._suspendIndexEvents == 0)
				{
					Exception ex = null;
					this.SetShadowIndexes();
					try
					{
						int count = this._shadowIndexes.Count;
						for (int i = 0; i < count; i++)
						{
							Index index = this._shadowIndexes[i];
							try
							{
								if (forceReset || index.HasRemoteAggregate)
								{
									index.Reset();
								}
								else
								{
									index.FireResetEvent();
								}
							}
							catch (Exception ex2) when (ADP.IsCatchableExceptionType(ex2))
							{
								ExceptionBuilder.TraceExceptionWithoutRethrow(ex2);
								if (ex == null)
								{
									ex = ex2;
								}
							}
						}
						if (ex != null)
						{
							throw ex;
						}
					}
					finally
					{
						this.RestoreShadowIndexes();
					}
				}
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00020278 File Offset: 0x0001E478
		internal void SuspendIndexEvents()
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataTable.SuspendIndexEvents|Info> {0}, {1}", this.ObjectID, this._suspendIndexEvents);
			this._suspendIndexEvents++;
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataTable" /> is initialized.</summary>
		/// <returns>true to indicate the component has completed initialization; otherwise false. </returns>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x000202A3 File Offset: 0x0001E4A3
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return !this.fInitInProgress;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x000202B0 File Offset: 0x0001E4B0
		private bool IsTypedDataTable
		{
			get
			{
				byte isTypedDataTable = this._isTypedDataTable;
				if (isTypedDataTable != 0)
				{
					return isTypedDataTable == 1;
				}
				this._isTypedDataTable = ((base.GetType() != typeof(DataTable)) ? 1 : 2);
				return 1 == this._isTypedDataTable;
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000202FC File Offset: 0x0001E4FC
		internal bool SetCaseSensitiveValue(bool isCaseSensitive, bool userSet, bool resetIndexes)
		{
			if (userSet || (!this._caseSensitiveUserSet && this._caseSensitive != isCaseSensitive))
			{
				this._caseSensitive = isCaseSensitive;
				if (isCaseSensitive)
				{
					this._compareFlags = CompareOptions.None;
				}
				else
				{
					this._compareFlags = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;
				}
				if (resetIndexes)
				{
					this.ResetIndexes();
					foreach (object obj in this.Constraints)
					{
						((Constraint)obj).CheckConstraint();
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00020390 File Offset: 0x0001E590
		private void ResetCaseSensitive()
		{
			this.SetCaseSensitiveValue(this._dataSet != null && this._dataSet.CaseSensitive, true, true);
			this._caseSensitiveUserSet = false;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000203B8 File Offset: 0x0001E5B8
		internal bool ShouldSerializeCaseSensitive()
		{
			return this._caseSensitiveUserSet;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x000203C0 File Offset: 0x0001E5C0
		internal bool SelfNested
		{
			get
			{
				foreach (object obj in this.ParentRelations)
				{
					DataRelation dataRelation = (DataRelation)obj;
					if (dataRelation.Nested && dataRelation.ParentTable == this)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0002042C File Offset: 0x0001E62C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal List<Index> LiveIndexes
		{
			get
			{
				if (!this.AreIndexEventsSuspended)
				{
					int num = this._indexes.Count - 1;
					while (0 <= num)
					{
						Index index = this._indexes[num];
						if (index.RefCount <= 1)
						{
							index.RemoveRef();
						}
						num--;
					}
				}
				return this._indexes;
			}
		}

		/// <summary>Gets or sets the serialization format.</summary>
		/// <returns>A <see cref="T:System.Data.SerializationFormat" /> enumeration specifying either Binary or Xml serialization.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0002047C File Offset: 0x0001E67C
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x00020484 File Offset: 0x0001E684
		[DefaultValue(SerializationFormat.Xml)]
		public SerializationFormat RemotingFormat
		{
			get
			{
				return this._remotingFormat;
			}
			set
			{
				if (value != SerializationFormat.Binary && value != SerializationFormat.Xml)
				{
					throw ExceptionBuilder.InvalidRemotingFormat(value);
				}
				if (this.DataSet != null && value != this.DataSet.RemotingFormat)
				{
					throw ExceptionBuilder.CanNotSetRemotingFormat();
				}
				this._remotingFormat = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x000204B7 File Offset: 0x0001E6B7
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x000204BF File Offset: 0x0001E6BF
		internal int UKColumnPositionForInference
		{
			get
			{
				return this._ukColumnPositionForInference;
			}
			set
			{
				this._ukColumnPositionForInference = value;
			}
		}

		/// <summary>Gets the collection of child relations for this <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataRelationCollection" /> that contains the child relations for the table. An empty collection is returned if no <see cref="T:System.Data.DataRelation" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x000204C8 File Offset: 0x0001E6C8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataRelationCollection ChildRelations
		{
			get
			{
				DataRelationCollection dataRelationCollection;
				if ((dataRelationCollection = this._childRelationsCollection) == null)
				{
					dataRelationCollection = (this._childRelationsCollection = new DataRelationCollection.DataTableRelationCollection(this, false));
				}
				return dataRelationCollection;
			}
		}

		/// <summary>Gets the collection of columns that belong to this table.</summary>
		/// <returns>A <see cref="T:System.Data.DataColumnCollection" /> that contains the collection of <see cref="T:System.Data.DataColumn" /> objects for the table. An empty collection is returned if no <see cref="T:System.Data.DataColumn" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x000204EF File Offset: 0x0001E6EF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataColumnCollection Columns
		{
			get
			{
				return this._columnCollection;
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000204F7 File Offset: 0x0001E6F7
		private void ResetColumns()
		{
			this.Columns.Clear();
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00020504 File Offset: 0x0001E704
		private CompareInfo CompareInfo
		{
			get
			{
				CompareInfo compareInfo;
				if ((compareInfo = this._compareInfo) == null)
				{
					compareInfo = (this._compareInfo = this.Locale.CompareInfo);
				}
				return compareInfo;
			}
		}

		/// <summary>Gets the collection of constraints maintained by this table.</summary>
		/// <returns>A <see cref="T:System.Data.ConstraintCollection" /> that contains the collection of <see cref="T:System.Data.Constraint" /> objects for the table. An empty collection is returned if no <see cref="T:System.Data.Constraint" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0002052F File Offset: 0x0001E72F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ConstraintCollection Constraints
		{
			get
			{
				return this._constraintCollection;
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00020537 File Offset: 0x0001E737
		private void ResetConstraints()
		{
			this.Constraints.Clear();
		}

		/// <summary>Gets the <see cref="T:System.Data.DataSet" /> to which this table belongs.</summary>
		/// <returns>The <see cref="T:System.Data.DataSet" /> to which this table belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00020544 File Offset: 0x0001E744
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataSet DataSet
		{
			get
			{
				return this._dataSet;
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0002054C File Offset: 0x0001E74C
		internal void SetDataSet(DataSet dataSet)
		{
			if (this._dataSet != dataSet)
			{
				this._dataSet = dataSet;
				DataColumnCollection columns = this.Columns;
				for (int i = 0; i < columns.Count; i++)
				{
					columns[i].OnSetDataSet();
				}
				if (this.DataSet != null)
				{
					this._defaultView = null;
				}
				if (dataSet != null)
				{
					this._remotingFormat = dataSet.RemotingFormat;
				}
			}
		}

		/// <summary>Gets a customized view of the table that may include a filtered view, or a cursor position.</summary>
		/// <returns>The <see cref="T:System.Data.DataView" /> associated with the <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x000205AC File Offset: 0x0001E7AC
		[Browsable(false)]
		public DataView DefaultView
		{
			get
			{
				DataView dataView = this._defaultView;
				if (dataView == null)
				{
					if (this._dataSet != null)
					{
						dataView = this._dataSet.DefaultViewManager.CreateDataView(this);
					}
					else
					{
						dataView = new DataView(this, true);
						dataView.SetIndex2("", DataViewRowState.CurrentRows, null, true);
					}
					dataView = Interlocked.CompareExchange<DataView>(ref this._defaultView, dataView, null);
					if (dataView == null)
					{
						dataView = this._defaultView;
					}
				}
				return dataView;
			}
		}

		/// <summary>Gets or sets the expression that returns a value used to represent this table in the user interface. The DisplayExpression property lets you display the name of this table in a user interface.</summary>
		/// <returns>A display string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0002060F File Offset: 0x0001E80F
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x00020617 File Offset: 0x0001E817
		[DefaultValue("")]
		public string DisplayExpression
		{
			get
			{
				return this.DisplayExpressionInternal;
			}
			set
			{
				this._displayExpression = ((!string.IsNullOrEmpty(value)) ? new DataExpression(this, value) : null);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00020631 File Offset: 0x0001E831
		internal string DisplayExpressionInternal
		{
			get
			{
				if (this._displayExpression == null)
				{
					return string.Empty;
				}
				return this._displayExpression.Expression;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0002064C File Offset: 0x0001E84C
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x00020672 File Offset: 0x0001E872
		internal bool EnforceConstraints
		{
			get
			{
				if (this.SuspendEnforceConstraints)
				{
					return false;
				}
				if (this._dataSet != null)
				{
					return this._dataSet.EnforceConstraints;
				}
				return this._enforceConstraints;
			}
			set
			{
				if (this._dataSet == null && this._enforceConstraints != value)
				{
					if (value)
					{
						this.EnableConstraints();
					}
					this._enforceConstraints = value;
				}
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00020695 File Offset: 0x0001E895
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0002069D File Offset: 0x0001E89D
		internal bool SuspendEnforceConstraints
		{
			get
			{
				return this._suspendEnforceConstraints;
			}
			set
			{
				this._suspendEnforceConstraints = value;
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000206A8 File Offset: 0x0001E8A8
		internal void EnableConstraints()
		{
			bool flag = false;
			foreach (object obj in this.Constraints)
			{
				Constraint constraint = (Constraint)obj;
				if (constraint is UniqueConstraint)
				{
					flag |= constraint.IsConstraintViolated();
				}
			}
			foreach (object obj2 in this.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj2;
				if (!dataColumn.AllowDBNull)
				{
					flag |= dataColumn.IsNotAllowDBNullViolated();
				}
				if (dataColumn.MaxLength >= 0)
				{
					flag |= dataColumn.IsMaxLengthViolated();
				}
			}
			if (flag)
			{
				this.EnforceConstraints = false;
				throw ExceptionBuilder.EnforceConstraint();
			}
		}

		/// <summary>Gets the collection of customized user information.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> that contains custom user information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00020788 File Offset: 0x0001E988
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				PropertyCollection propertyCollection;
				if ((propertyCollection = this._extendedProperties) == null)
				{
					propertyCollection = (this._extendedProperties = new PropertyCollection());
				}
				return propertyCollection;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x000207B0 File Offset: 0x0001E9B0
		internal IFormatProvider FormatProvider
		{
			get
			{
				if (this._formatProvider == null)
				{
					CultureInfo cultureInfo = this.Locale;
					if (cultureInfo.IsNeutralCulture)
					{
						cultureInfo = CultureInfo.InvariantCulture;
					}
					this._formatProvider = cultureInfo;
				}
				return this._formatProvider;
			}
		}

		/// <summary>Gets a value indicating whether there are errors in any of the rows in any of the tables of the <see cref="T:System.Data.DataSet" /> to which the table belongs.</summary>
		/// <returns>true if errors exist; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x000207E8 File Offset: 0x0001E9E8
		[Browsable(false)]
		public bool HasErrors
		{
			get
			{
				for (int i = 0; i < this.Rows.Count; i++)
				{
					if (this.Rows[i].HasErrors)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Gets or sets the locale information used to compare strings within the table.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that contains data about the user's machine locale. The default is the <see cref="T:System.Data.DataSet" /> object's <see cref="T:System.Globalization.CultureInfo" /> (returned by the <see cref="P:System.Data.DataSet.Locale" /> property) to which the <see cref="T:System.Data.DataTable" /> belongs; if the table doesn't belong to a <see cref="T:System.Data.DataSet" />, the default is the current system <see cref="T:System.Globalization.CultureInfo" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00020821 File Offset: 0x0001EA21
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x0002082C File Offset: 0x0001EA2C
		public CultureInfo Locale
		{
			get
			{
				return this._culture;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTable.set_Locale|API> {0}", this.ObjectID);
				try
				{
					bool flag = true;
					if (value == null)
					{
						flag = false;
						value = ((this._dataSet != null) ? this._dataSet.Locale : this._culture);
					}
					if (this._culture != value && !this._culture.Equals(value))
					{
						bool flag2 = false;
						bool flag3 = false;
						CultureInfo culture = this._culture;
						bool cultureUserSet = this._cultureUserSet;
						try
						{
							this._cultureUserSet = true;
							this.SetLocaleValue(value, true, false);
							if (this.DataSet == null || this.DataSet.ValidateLocaleConstraint())
							{
								flag2 = false;
								this.SetLocaleValue(value, true, true);
								flag2 = true;
							}
						}
						catch
						{
							flag3 = true;
							throw;
						}
						finally
						{
							if (!flag2)
							{
								try
								{
									this.SetLocaleValue(culture, true, true);
								}
								catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
								{
									ADP.TraceExceptionWithoutRethrow(ex);
								}
								this._cultureUserSet = cultureUserSet;
								if (!flag3)
								{
									throw ExceptionBuilder.CannotChangeCaseLocale(null);
								}
							}
						}
						this.SetLocaleValue(value, true, true);
					}
					this._cultureUserSet = flag;
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0002097C File Offset: 0x0001EB7C
		internal bool SetLocaleValue(CultureInfo culture, bool userSet, bool resetIndexes)
		{
			if (userSet || resetIndexes || (!this._cultureUserSet && !this._culture.Equals(culture)))
			{
				this._culture = culture;
				this._compareInfo = null;
				this._formatProvider = null;
				this._hashCodeProvider = null;
				foreach (object obj in this.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					dataColumn._hashCode = this.GetSpecialHashCode(dataColumn.ColumnName);
				}
				if (resetIndexes)
				{
					this.ResetIndexes();
					foreach (object obj2 in this.Constraints)
					{
						((Constraint)obj2).CheckConstraint();
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00020A70 File Offset: 0x0001EC70
		internal bool ShouldSerializeLocale()
		{
			return this._cultureUserSet;
		}

		/// <summary>Gets or sets the initial starting size for this table.</summary>
		/// <returns>The initial starting size in rows of this table. The default is 50.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00020A78 File Offset: 0x0001EC78
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x00020A85 File Offset: 0x0001EC85
		[DefaultValue(50)]
		public int MinimumCapacity
		{
			get
			{
				return this._recordManager.MinimumCapacity;
			}
			set
			{
				if (value != this._recordManager.MinimumCapacity)
				{
					this._recordManager.MinimumCapacity = value;
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00020AA1 File Offset: 0x0001ECA1
		internal int RecordCapacity
		{
			get
			{
				return this._recordManager.RecordCapacity;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00020AAE File Offset: 0x0001ECAE
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x00020AB6 File Offset: 0x0001ECB6
		internal int ElementColumnCount
		{
			get
			{
				return this._elementColumnCount;
			}
			set
			{
				if (value > 0 && this._xmlText != null)
				{
					throw ExceptionBuilder.TableCannotAddToSimpleContent();
				}
				this._elementColumnCount = value;
			}
		}

		/// <summary>Gets the collection of parent relations for this <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataRelationCollection" /> that contains the parent relations for the table. An empty collection is returned if no <see cref="T:System.Data.DataRelation" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00020AD4 File Offset: 0x0001ECD4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataRelationCollection ParentRelations
		{
			get
			{
				DataRelationCollection dataRelationCollection;
				if ((dataRelationCollection = this._parentRelationsCollection) == null)
				{
					dataRelationCollection = (this._parentRelationsCollection = new DataRelationCollection.DataTableRelationCollection(this, true));
				}
				return dataRelationCollection;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00020AFB File Offset: 0x0001ECFB
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x00020B03 File Offset: 0x0001ED03
		internal bool MergingData
		{
			get
			{
				return this._mergingData;
			}
			set
			{
				this._mergingData = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00020B0C File Offset: 0x0001ED0C
		internal DataRelation[] NestedParentRelations
		{
			get
			{
				return this._nestedParentRelations;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00020B14 File Offset: 0x0001ED14
		internal bool SchemaLoading
		{
			get
			{
				return this._schemaLoading;
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00020B1C File Offset: 0x0001ED1C
		internal void CacheNestedParent()
		{
			this._nestedParentRelations = this.FindNestedParentRelations();
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00020B2C File Offset: 0x0001ED2C
		private DataRelation[] FindNestedParentRelations()
		{
			List<DataRelation> list = null;
			foreach (object obj in this.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Nested)
				{
					if (list == null)
					{
						list = new List<DataRelation>();
					}
					list.Add(dataRelation);
				}
			}
			if (list != null && list.Count != 0)
			{
				return list.ToArray();
			}
			return Array.Empty<DataRelation>();
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x00020BB0 File Offset: 0x0001EDB0
		internal int NestedParentsCount
		{
			get
			{
				int num = 0;
				using (IEnumerator enumerator = this.ParentRelations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((DataRelation)enumerator.Current).Nested)
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		/// <summary>Gets or sets an array of columns that function as primary keys for the data table.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects.</returns>
		/// <exception cref="T:System.Data.DataException">The key is a foreign key. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00020C10 File Offset: 0x0001EE10
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x00020C3C File Offset: 0x0001EE3C
		[TypeConverter(typeof(PrimaryKeyTypeConverter))]
		public DataColumn[] PrimaryKey
		{
			get
			{
				UniqueConstraint primaryKey = this._primaryKey;
				if (primaryKey != null)
				{
					return primaryKey.Key.ToArray();
				}
				return Array.Empty<DataColumn>();
			}
			set
			{
				UniqueConstraint uniqueConstraint = null;
				if (this.fInitInProgress && value != null)
				{
					this._delayedSetPrimaryKey = value;
					return;
				}
				if (value != null && value.Length != 0)
				{
					int num = 0;
					int num2 = 0;
					while (num2 < value.Length && value[num2] != null)
					{
						num++;
						num2++;
					}
					if (num != 0)
					{
						DataColumn[] array = value;
						if (num != value.Length)
						{
							array = new DataColumn[num];
							for (int i = 0; i < num; i++)
							{
								array[i] = value[i];
							}
						}
						uniqueConstraint = new UniqueConstraint(array);
						if (uniqueConstraint.Table != this)
						{
							throw ExceptionBuilder.TableForeignPrimaryKey();
						}
					}
				}
				if (uniqueConstraint == this._primaryKey || (uniqueConstraint != null && uniqueConstraint.Equals(this._primaryKey)))
				{
					return;
				}
				UniqueConstraint uniqueConstraint2;
				if ((uniqueConstraint2 = (UniqueConstraint)this.Constraints.FindConstraint(uniqueConstraint)) != null)
				{
					uniqueConstraint.ColumnsReference.CopyTo(uniqueConstraint2.Key.ColumnsReference, 0);
					uniqueConstraint = uniqueConstraint2;
				}
				UniqueConstraint primaryKey = this._primaryKey;
				this._primaryKey = null;
				if (primaryKey != null)
				{
					primaryKey.ConstraintIndex.RemoveRef();
					if (this._loadIndex != null)
					{
						this._loadIndex.RemoveRef();
						this._loadIndex = null;
					}
					if (this._loadIndexwithOriginalAdded != null)
					{
						this._loadIndexwithOriginalAdded.RemoveRef();
						this._loadIndexwithOriginalAdded = null;
					}
					if (this._loadIndexwithCurrentDeleted != null)
					{
						this._loadIndexwithCurrentDeleted.RemoveRef();
						this._loadIndexwithCurrentDeleted = null;
					}
					this.Constraints.Remove(primaryKey);
				}
				if (uniqueConstraint != null && uniqueConstraint2 == null)
				{
					this.Constraints.Add(uniqueConstraint);
				}
				this._primaryKey = uniqueConstraint;
				this._primaryIndex = ((uniqueConstraint != null) ? uniqueConstraint.Key.GetIndexDesc() : Array.Empty<IndexField>());
				if (this._primaryKey != null)
				{
					uniqueConstraint.ConstraintIndex.AddRef();
					for (int j = 0; j < uniqueConstraint.ColumnsReference.Length; j++)
					{
						uniqueConstraint.ColumnsReference[j].AllowDBNull = false;
					}
				}
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00020E01 File Offset: 0x0001F001
		private bool ShouldSerializePrimaryKey()
		{
			return this._primaryKey != null;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00020E0C File Offset: 0x0001F00C
		private void ResetPrimaryKey()
		{
			this.PrimaryKey = null;
		}

		/// <summary>Gets the collection of rows that belong to this table.</summary>
		/// <returns>A <see cref="T:System.Data.DataRowCollection" /> that contains <see cref="T:System.Data.DataRow" /> objects; otherwise a null value if no <see cref="T:System.Data.DataRow" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00020E15 File Offset: 0x0001F015
		[Browsable(false)]
		public DataRowCollection Rows
		{
			get
			{
				return this._rowCollection;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.DataTable" />.</returns>
		/// <exception cref="T:System.ArgumentException">null or empty string ("") is passed in and this table belongs to a collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The table belongs to a collection that already has a table with the same name. (Comparison is case-sensitive).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00020E1D File Offset: 0x0001F01D
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x00020E28 File Offset: 0x0001F028
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		public string TableName
		{
			get
			{
				return this._tableName;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, string>("<ds.DataTable.set_TableName|API> {0}, value='{1}'", this.ObjectID, value);
				try
				{
					if (value == null)
					{
						value = string.Empty;
					}
					CultureInfo locale = this.Locale;
					if (string.Compare(this._tableName, value, true, locale) != 0)
					{
						if (this._dataSet != null)
						{
							if (value.Length == 0)
							{
								throw ExceptionBuilder.NoTableName();
							}
							if (string.Compare(value, this._dataSet.DataSetName, true, this._dataSet.Locale) == 0 && !this._fNestedInDataset)
							{
								throw ExceptionBuilder.DatasetConflictingName(this._dataSet.DataSetName);
							}
							DataRelation[] nestedParentRelations = this.NestedParentRelations;
							if (nestedParentRelations.Length == 0)
							{
								this._dataSet.Tables.RegisterName(value, this.Namespace);
							}
							else
							{
								DataRelation[] array = nestedParentRelations;
								for (int i = 0; i < array.Length; i++)
								{
									if (!array[i].ParentTable.Columns.CanRegisterName(value))
									{
										throw ExceptionBuilder.CannotAddDuplicate2(value);
									}
								}
								this._dataSet.Tables.RegisterName(value, this.Namespace);
								foreach (DataRelation dataRelation in nestedParentRelations)
								{
									dataRelation.ParentTable.Columns.RegisterColumnName(value, null);
									dataRelation.ParentTable.Columns.UnregisterName(this.TableName);
								}
							}
							if (this._tableName.Length != 0)
							{
								this._dataSet.Tables.UnregisterName(this._tableName);
							}
						}
						this.RaisePropertyChanging("TableName");
						this._tableName = value;
						this._encodedTableName = null;
					}
					else if (string.Compare(this._tableName, value, false, locale) != 0)
					{
						this.RaisePropertyChanging("TableName");
						this._tableName = value;
						this._encodedTableName = null;
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00021000 File Offset: 0x0001F200
		internal string EncodedTableName
		{
			get
			{
				string text = this._encodedTableName;
				if (text == null)
				{
					text = XmlConvert.EncodeLocalName(this.TableName);
					this._encodedTableName = text;
				}
				return text;
			}
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0002102C File Offset: 0x0001F22C
		private string GetInheritedNamespace(List<DataTable> visitedTables)
		{
			DataRelation[] nestedParentRelations = this.NestedParentRelations;
			if (nestedParentRelations.Length != 0)
			{
				foreach (DataRelation dataRelation in nestedParentRelations)
				{
					if (dataRelation.ParentTable._tableNamespace != null)
					{
						return dataRelation.ParentTable._tableNamespace;
					}
				}
				int num = 0;
				while (num < nestedParentRelations.Length && (nestedParentRelations[num].ParentTable == this || visitedTables.Contains(nestedParentRelations[num].ParentTable)))
				{
					num++;
				}
				if (num < nestedParentRelations.Length)
				{
					DataTable parentTable = nestedParentRelations[num].ParentTable;
					if (!visitedTables.Contains(parentTable))
					{
						visitedTables.Add(parentTable);
					}
					return parentTable.GetInheritedNamespace(visitedTables);
				}
			}
			if (this.DataSet != null)
			{
				return this.DataSet.Namespace;
			}
			return string.Empty;
		}

		/// <summary>Gets or sets the namespace for the XML representation of the data stored in the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x000210DC File Offset: 0x0001F2DC
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x000210F4 File Offset: 0x0001F2F4
		public string Namespace
		{
			get
			{
				return this._tableNamespace ?? this.GetInheritedNamespace(new List<DataTable>());
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, string>("<ds.DataTable.set_Namespace|API> {0}, value='{1}'", this.ObjectID, value);
				try
				{
					if (value != this._tableNamespace)
					{
						if (this._dataSet != null)
						{
							string text = ((value == null) ? this.GetInheritedNamespace(new List<DataTable>()) : value);
							if (text != this.Namespace)
							{
								if (this._dataSet.Tables.Contains(this.TableName, text, true, true))
								{
									throw ExceptionBuilder.DuplicateTableName2(this.TableName, text);
								}
								this.CheckCascadingNamespaceConflict(text);
							}
						}
						this.CheckNamespaceValidityForNestedRelations(value);
						this.DoRaiseNamespaceChange();
					}
					this._tableNamespace = value;
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x000211B0 File Offset: 0x0001F3B0
		internal bool IsNamespaceInherited()
		{
			return this._tableNamespace == null;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000211BC File Offset: 0x0001F3BC
		internal void CheckCascadingNamespaceConflict(string realNamespace)
		{
			foreach (object obj in this.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Nested && dataRelation.ChildTable != this && dataRelation.ChildTable._tableNamespace == null)
				{
					DataTable childTable = dataRelation.ChildTable;
					if (this._dataSet.Tables.Contains(childTable.TableName, realNamespace, false, true))
					{
						throw ExceptionBuilder.DuplicateTableName2(this.TableName, realNamespace);
					}
					childTable.CheckCascadingNamespaceConflict(realNamespace);
				}
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00021264 File Offset: 0x0001F464
		internal void CheckNamespaceValidityForNestedRelations(string realNamespace)
		{
			foreach (object obj in this.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Nested)
				{
					if (realNamespace != null)
					{
						dataRelation.ChildTable.CheckNamespaceValidityForNestedParentRelations(realNamespace, this);
					}
					else
					{
						dataRelation.ChildTable.CheckNamespaceValidityForNestedParentRelations(this.GetInheritedNamespace(new List<DataTable>()), this);
					}
				}
			}
			if (realNamespace == null)
			{
				this.CheckNamespaceValidityForNestedParentRelations(this.GetInheritedNamespace(new List<DataTable>()), this);
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000212FC File Offset: 0x0001F4FC
		internal void CheckNamespaceValidityForNestedParentRelations(string ns, DataTable parentTable)
		{
			foreach (object obj in this.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Nested && dataRelation.ParentTable != parentTable && dataRelation.ParentTable.Namespace != ns)
				{
					throw ExceptionBuilder.InValidNestedRelation(this.TableName);
				}
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00021380 File Offset: 0x0001F580
		internal void DoRaiseNamespaceChange()
		{
			this.RaisePropertyChanging("Namespace");
			foreach (object obj in this.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn._columnUri == null)
				{
					dataColumn.RaisePropertyChanging("Namespace");
				}
			}
			foreach (object obj2 in this.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj2;
				if (dataRelation.Nested && dataRelation.ChildTable != this)
				{
					DataTable childTable = dataRelation.ChildTable;
					dataRelation.ChildTable.DoRaiseNamespaceChange();
				}
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00021454 File Offset: 0x0001F654
		private bool ShouldSerializeNamespace()
		{
			return this._tableNamespace != null;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0002145F File Offset: 0x0001F65F
		private void ResetNamespace()
		{
			this.Namespace = null;
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Data.DataTable" /> that is used on a form or used by another component. The initialization occurs at run time. </summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060007A4 RID: 1956 RVA: 0x00021468 File Offset: 0x0001F668
		public virtual void BeginInit()
		{
			this.fInitInProgress = true;
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Data.DataTable" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060007A5 RID: 1957 RVA: 0x00021474 File Offset: 0x0001F674
		public virtual void EndInit()
		{
			if (this._dataSet == null || !this._dataSet._fInitInProgress)
			{
				this.Columns.FinishInitCollection();
				this.Constraints.FinishInitConstraints();
				foreach (object obj in this.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.Computed)
					{
						dataColumn.Expression = dataColumn.Expression;
					}
				}
			}
			this.fInitInProgress = false;
			if (this._delayedSetPrimaryKey != null)
			{
				this.PrimaryKey = this._delayedSetPrimaryKey;
				this._delayedSetPrimaryKey = null;
			}
			if (this._delayedViews.Count > 0)
			{
				foreach (DataView dataView in this._delayedViews)
				{
					dataView.EndInit();
				}
				this._delayedViews.Clear();
			}
			this.OnInitialized();
		}

		/// <summary>Gets or sets the namespace for the XML representation of the data stored in the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>The prefix of the <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00021588 File Offset: 0x0001F788
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x00021590 File Offset: 0x0001F790
		[DefaultValue("")]
		public string Prefix
		{
			get
			{
				return this._tablePrefix;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataTable.set_Prefix|API> {0}, value='{1}'", this.ObjectID, value);
				if (XmlConvert.DecodeName(value) == value && XmlConvert.EncodeName(value) != value)
				{
					throw ExceptionBuilder.InvalidPrefix(value);
				}
				this._tablePrefix = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x000215E7 File Offset: 0x0001F7E7
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x000215F0 File Offset: 0x0001F7F0
		internal DataColumn XmlText
		{
			get
			{
				return this._xmlText;
			}
			set
			{
				if (this._xmlText != value)
				{
					if (this._xmlText != null)
					{
						if (value != null)
						{
							throw ExceptionBuilder.MultipleTextOnlyColumns();
						}
						this.Columns.Remove(this._xmlText);
					}
					else if (value != this.Columns[value.ColumnName])
					{
						this.Columns.Add(value);
					}
					this._xmlText = value;
				}
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00021651 File Offset: 0x0001F851
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x00021659 File Offset: 0x0001F859
		internal decimal MaxOccurs
		{
			get
			{
				return this._maxOccurs;
			}
			set
			{
				this._maxOccurs = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00021662 File Offset: 0x0001F862
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x0002166A File Offset: 0x0001F86A
		internal decimal MinOccurs
		{
			get
			{
				return this._minOccurs;
			}
			set
			{
				this._minOccurs = value;
			}
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00021674 File Offset: 0x0001F874
		internal void SetKeyValues(DataKey key, object[] keyValues, int record)
		{
			for (int i = 0; i < keyValues.Length; i++)
			{
				key.ColumnsReference[i][record] = keyValues[i];
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x000216A4 File Offset: 0x0001F8A4
		internal DataRow FindByIndex(Index ndx, object[] key)
		{
			Range range = ndx.FindRecords(key);
			if (!range.IsNull)
			{
				return this._recordManager[ndx.GetRecord(range.Min)];
			}
			return null;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000216DC File Offset: 0x0001F8DC
		internal DataRow FindMergeTarget(DataRow row, DataKey key, Index ndx)
		{
			DataRow dataRow = null;
			if (key.HasValue)
			{
				int num = ((row._oldRecord == -1) ? row._newRecord : row._oldRecord);
				object[] keyValues = key.GetKeyValues(num);
				dataRow = this.FindByIndex(ndx, keyValues);
			}
			return dataRow;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0002171F File Offset: 0x0001F91F
		private void SetMergeRecords(DataRow row, int newRecord, int oldRecord, DataRowAction action)
		{
			if (newRecord != -1)
			{
				this.SetNewRecord(row, newRecord, action, true, true, false);
				this.SetOldRecord(row, oldRecord);
				return;
			}
			this.SetOldRecord(row, oldRecord);
			if (row._newRecord != -1)
			{
				this.SetNewRecord(row, newRecord, action, true, true, false);
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0002175C File Offset: 0x0001F95C
		internal DataRow MergeRow(DataRow row, DataRow targetRow, bool preserveChanges, Index idxSearch)
		{
			if (targetRow == null)
			{
				targetRow = this.NewEmptyRow();
				targetRow._oldRecord = this._recordManager.ImportRecord(row.Table, row._oldRecord);
				targetRow._newRecord = targetRow._oldRecord;
				if (row._oldRecord != row._newRecord)
				{
					targetRow._newRecord = this._recordManager.ImportRecord(row.Table, row._newRecord);
				}
				this.InsertRow(targetRow, -1L);
			}
			else
			{
				int tempRecord = targetRow._tempRecord;
				targetRow._tempRecord = -1;
				try
				{
					DataRowState rowState = targetRow.RowState;
					int num = ((rowState == DataRowState.Added) ? targetRow._newRecord : targetRow._oldRecord);
					if (targetRow.RowState == DataRowState.Unchanged && row.RowState == DataRowState.Unchanged)
					{
						int num2 = targetRow._oldRecord;
						int num3 = (preserveChanges ? this._recordManager.CopyRecord(this, num2, -1) : targetRow._newRecord);
						num2 = this._recordManager.CopyRecord(row.Table, row._oldRecord, targetRow._oldRecord);
						this.SetMergeRecords(targetRow, num3, num2, DataRowAction.Change);
					}
					else if (row._newRecord == -1)
					{
						int num2 = targetRow._oldRecord;
						int num3;
						if (preserveChanges)
						{
							num3 = ((targetRow.RowState == DataRowState.Unchanged) ? this._recordManager.CopyRecord(this, num2, -1) : targetRow._newRecord);
						}
						else
						{
							num3 = -1;
						}
						num2 = this._recordManager.CopyRecord(row.Table, row._oldRecord, num2);
						if (num != ((rowState == DataRowState.Added) ? num3 : num2))
						{
							this.SetMergeRecords(targetRow, num3, num2, (num3 == -1) ? DataRowAction.Delete : DataRowAction.Change);
							idxSearch.Reset();
							int num4 = ((rowState == DataRowState.Added) ? num3 : num2);
						}
						else
						{
							this.SetMergeRecords(targetRow, num3, num2, (num3 == -1) ? DataRowAction.Delete : DataRowAction.Change);
						}
					}
					else
					{
						int num2 = targetRow._oldRecord;
						int num3 = targetRow._newRecord;
						if (targetRow.RowState == DataRowState.Unchanged)
						{
							num3 = this._recordManager.CopyRecord(this, num2, -1);
						}
						num2 = this._recordManager.CopyRecord(row.Table, row._oldRecord, num2);
						if (!preserveChanges)
						{
							num3 = this._recordManager.CopyRecord(row.Table, row._newRecord, num3);
						}
						this.SetMergeRecords(targetRow, num3, num2, DataRowAction.Change);
					}
					if (rowState == DataRowState.Added && targetRow._oldRecord != -1)
					{
						idxSearch.Reset();
					}
				}
				finally
				{
					targetRow._tempRecord = tempRecord;
				}
			}
			if (row.HasErrors)
			{
				if (targetRow.RowError.Length == 0)
				{
					targetRow.RowError = row.RowError;
				}
				else
				{
					DataRow dataRow = targetRow;
					dataRow.RowError = dataRow.RowError + " ]:[ " + row.RowError;
				}
				DataColumn[] columnsInError = row.GetColumnsInError();
				for (int i = 0; i < columnsInError.Length; i++)
				{
					DataColumn dataColumn = targetRow.Table.Columns[columnsInError[i].ColumnName];
					targetRow.SetColumnError(dataColumn, row.GetColumnError(columnsInError[i]));
				}
			}
			else if (!preserveChanges)
			{
				targetRow.ClearErrors();
			}
			return targetRow;
		}

		/// <summary>Commits all the changes made to this table since the last time <see cref="M:System.Data.DataTable.AcceptChanges" /> was called.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007B3 RID: 1971 RVA: 0x00021A3C File Offset: 0x0001FC3C
		public void AcceptChanges()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTable.AcceptChanges|API> {0}", this.ObjectID);
			try
			{
				DataRow[] array = new DataRow[this.Rows.Count];
				this.Rows.CopyTo(array, 0);
				this.SuspendIndexEvents();
				try
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].rowID != -1L)
						{
							array[i].AcceptChanges();
						}
					}
				}
				finally
				{
					this.RestoreIndexEvents(false);
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Creates a new instance of <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>The new expression.</returns>
		// Token: 0x060007B4 RID: 1972 RVA: 0x00021AD8 File Offset: 0x0001FCD8
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual DataTable CreateInstance()
		{
			return (DataTable)Activator.CreateInstance(base.GetType(), true);
		}

		/// <summary>Clones the structure of the <see cref="T:System.Data.DataTable" />, including all <see cref="T:System.Data.DataTable" /> schemas and constraints.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> with the same schema as the current <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060007B5 RID: 1973 RVA: 0x00021AEB File Offset: 0x0001FCEB
		public virtual DataTable Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00021AF4 File Offset: 0x0001FCF4
		internal DataTable Clone(DataSet cloneDS)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataTable.Clone|INFO> {0}, cloneDS={1}", this.ObjectID, (cloneDS != null) ? cloneDS.ObjectID : 0);
			DataTable dataTable2;
			try
			{
				DataTable dataTable = this.CreateInstance();
				if (dataTable.Columns.Count > 0)
				{
					dataTable.Reset();
				}
				dataTable2 = this.CloneTo(dataTable, cloneDS, false);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataTable2;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00021B68 File Offset: 0x0001FD68
		private DataTable IncrementalCloneTo(DataTable sourceTable, DataTable targetTable)
		{
			foreach (object obj in sourceTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (targetTable.Columns[dataColumn.ColumnName] == null)
				{
					targetTable.Columns.Add(dataColumn.Clone());
				}
			}
			return targetTable;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00021BE0 File Offset: 0x0001FDE0
		private DataTable CloneHierarchy(DataTable sourceTable, DataSet ds, Hashtable visitedMap)
		{
			if (visitedMap == null)
			{
				visitedMap = new Hashtable();
			}
			if (visitedMap.Contains(sourceTable))
			{
				return (DataTable)visitedMap[sourceTable];
			}
			DataTable dataTable = ds.Tables[sourceTable.TableName, sourceTable.Namespace];
			if (dataTable != null && dataTable.Columns.Count > 0)
			{
				dataTable = this.IncrementalCloneTo(sourceTable, dataTable);
			}
			else
			{
				if (dataTable == null)
				{
					dataTable = new DataTable();
					ds.Tables.Add(dataTable);
				}
				dataTable = sourceTable.CloneTo(dataTable, ds, true);
			}
			visitedMap[sourceTable] = dataTable;
			foreach (object obj in sourceTable.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				this.CloneHierarchy(dataRelation.ChildTable, ds, visitedMap);
			}
			return dataTable;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00021CC0 File Offset: 0x0001FEC0
		private DataTable CloneTo(DataTable clone, DataSet cloneDS, bool skipExpressionColumns)
		{
			clone._tableName = this._tableName;
			clone._tableNamespace = this._tableNamespace;
			clone._tablePrefix = this._tablePrefix;
			clone._fNestedInDataset = this._fNestedInDataset;
			clone._culture = this._culture;
			clone._cultureUserSet = this._cultureUserSet;
			clone._compareInfo = this._compareInfo;
			clone._compareFlags = this._compareFlags;
			clone._formatProvider = this._formatProvider;
			clone._hashCodeProvider = this._hashCodeProvider;
			clone._caseSensitive = this._caseSensitive;
			clone._caseSensitiveUserSet = this._caseSensitiveUserSet;
			clone._displayExpression = this._displayExpression;
			clone._typeName = this._typeName;
			clone._repeatableElement = this._repeatableElement;
			clone.MinimumCapacity = this.MinimumCapacity;
			clone.RemotingFormat = this.RemotingFormat;
			DataColumnCollection columns = this.Columns;
			for (int i = 0; i < columns.Count; i++)
			{
				clone.Columns.Add(columns[i].Clone());
			}
			if (!skipExpressionColumns && cloneDS == null)
			{
				for (int j = 0; j < columns.Count; j++)
				{
					clone.Columns[columns[j].ColumnName].Expression = columns[j].Expression;
				}
			}
			DataColumn[] primaryKey = this.PrimaryKey;
			if (primaryKey.Length != 0)
			{
				DataColumn[] array = new DataColumn[primaryKey.Length];
				for (int k = 0; k < primaryKey.Length; k++)
				{
					array[k] = clone.Columns[primaryKey[k].Ordinal];
				}
				clone.PrimaryKey = array;
			}
			for (int l = 0; l < this.Constraints.Count; l++)
			{
				ForeignKeyConstraint foreignKeyConstraint = this.Constraints[l] as ForeignKeyConstraint;
				UniqueConstraint uniqueConstraint = this.Constraints[l] as UniqueConstraint;
				if (foreignKeyConstraint != null)
				{
					if (foreignKeyConstraint.Table == foreignKeyConstraint.RelatedTable)
					{
						ForeignKeyConstraint foreignKeyConstraint2 = foreignKeyConstraint.Clone(clone);
						Constraint constraint = clone.Constraints.FindConstraint(foreignKeyConstraint2);
						if (constraint != null)
						{
							constraint.ConstraintName = this.Constraints[l].ConstraintName;
						}
					}
				}
				else if (uniqueConstraint != null)
				{
					UniqueConstraint uniqueConstraint2 = uniqueConstraint.Clone(clone);
					Constraint constraint2 = clone.Constraints.FindConstraint(uniqueConstraint2);
					if (constraint2 != null)
					{
						constraint2.ConstraintName = this.Constraints[l].ConstraintName;
						foreach (object obj in uniqueConstraint2.ExtendedProperties.Keys)
						{
							constraint2.ExtendedProperties[obj] = uniqueConstraint2.ExtendedProperties[obj];
						}
					}
				}
			}
			for (int m = 0; m < this.Constraints.Count; m++)
			{
				if (!clone.Constraints.Contains(this.Constraints[m].ConstraintName, true))
				{
					ForeignKeyConstraint foreignKeyConstraint3 = this.Constraints[m] as ForeignKeyConstraint;
					UniqueConstraint uniqueConstraint3 = this.Constraints[m] as UniqueConstraint;
					if (foreignKeyConstraint3 != null)
					{
						if (foreignKeyConstraint3.Table == foreignKeyConstraint3.RelatedTable)
						{
							ForeignKeyConstraint foreignKeyConstraint4 = foreignKeyConstraint3.Clone(clone);
							if (foreignKeyConstraint4 != null)
							{
								clone.Constraints.Add(foreignKeyConstraint4);
							}
						}
					}
					else if (uniqueConstraint3 != null)
					{
						clone.Constraints.Add(uniqueConstraint3.Clone(clone));
					}
				}
			}
			if (this._extendedProperties != null)
			{
				foreach (object obj2 in this._extendedProperties.Keys)
				{
					clone.ExtendedProperties[obj2] = this._extendedProperties[obj2];
				}
			}
			return clone;
		}

		/// <summary>Copies both the structure and data for this <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> with the same structure (table schemas and constraints) and data as this <see cref="T:System.Data.DataTable" />.If these classes have been derived, the copy will also be of the same derived classes.<see cref="M:System.Data.DataTable.Copy" /> creates a new <see cref="T:System.Data.DataTable" /> with the same structure and data as the original <see cref="T:System.Data.DataTable" />. To copy the structure to a new <see cref="T:System.Data.DataTable" />, but not the data, use <see cref="M:System.Data.DataTable.Clone" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060007BA RID: 1978 RVA: 0x000220B0 File Offset: 0x000202B0
		public DataTable Copy()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTable.Copy|API> {0}", this.ObjectID);
			DataTable dataTable2;
			try
			{
				DataTable dataTable = this.Clone();
				foreach (object obj in this.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					this.CopyRow(dataTable, dataRow);
				}
				dataTable2 = dataTable;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataTable2;
		}

		/// <summary>Occurs when a value is being changed for the specified <see cref="T:System.Data.DataColumn" /> in a <see cref="T:System.Data.DataRow" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060007BB RID: 1979 RVA: 0x0002214C File Offset: 0x0002034C
		// (remove) Token: 0x060007BC RID: 1980 RVA: 0x0002217A File Offset: 0x0002037A
		public event DataColumnChangeEventHandler ColumnChanging
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.add_ColumnChanging|API> {0}", this.ObjectID);
				this._onColumnChangingDelegate = (DataColumnChangeEventHandler)Delegate.Combine(this._onColumnChangingDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.remove_ColumnChanging|API> {0}", this.ObjectID);
				this._onColumnChangingDelegate = (DataColumnChangeEventHandler)Delegate.Remove(this._onColumnChangingDelegate, value);
			}
		}

		/// <summary>Occurs after a value has been changed for the specified <see cref="T:System.Data.DataColumn" /> in a <see cref="T:System.Data.DataRow" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060007BD RID: 1981 RVA: 0x000221A8 File Offset: 0x000203A8
		// (remove) Token: 0x060007BE RID: 1982 RVA: 0x000221D6 File Offset: 0x000203D6
		public event DataColumnChangeEventHandler ColumnChanged
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.add_ColumnChanged|API> {0}", this.ObjectID);
				this._onColumnChangedDelegate = (DataColumnChangeEventHandler)Delegate.Combine(this._onColumnChangedDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.remove_ColumnChanged|API> {0}", this.ObjectID);
				this._onColumnChangedDelegate = (DataColumnChangeEventHandler)Delegate.Remove(this._onColumnChangedDelegate, value);
			}
		}

		/// <summary>Occurs after the <see cref="T:System.Data.DataTable" /> is initialized.</summary>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060007BF RID: 1983 RVA: 0x00022204 File Offset: 0x00020404
		// (remove) Token: 0x060007C0 RID: 1984 RVA: 0x0002221D File Offset: 0x0002041D
		public event EventHandler Initialized
		{
			add
			{
				this._onInitialized = (EventHandler)Delegate.Combine(this._onInitialized, value);
			}
			remove
			{
				this._onInitialized = (EventHandler)Delegate.Remove(this._onInitialized, value);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060007C1 RID: 1985 RVA: 0x00022236 File Offset: 0x00020436
		// (remove) Token: 0x060007C2 RID: 1986 RVA: 0x00022264 File Offset: 0x00020464
		internal event PropertyChangedEventHandler PropertyChanging
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.add_PropertyChanging|INFO> {0}", this.ObjectID);
				this._onPropertyChangingDelegate = (PropertyChangedEventHandler)Delegate.Combine(this._onPropertyChangingDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.remove_PropertyChanging|INFO> {0}", this.ObjectID);
				this._onPropertyChangingDelegate = (PropertyChangedEventHandler)Delegate.Remove(this._onPropertyChangingDelegate, value);
			}
		}

		/// <summary>Occurs after a <see cref="T:System.Data.DataRow" /> has been changed successfully.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060007C3 RID: 1987 RVA: 0x00022292 File Offset: 0x00020492
		// (remove) Token: 0x060007C4 RID: 1988 RVA: 0x000222C0 File Offset: 0x000204C0
		public event DataRowChangeEventHandler RowChanged
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.add_RowChanged|API> {0}", this.ObjectID);
				this._onRowChangedDelegate = (DataRowChangeEventHandler)Delegate.Combine(this._onRowChangedDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.remove_RowChanged|API> {0}", this.ObjectID);
				this._onRowChangedDelegate = (DataRowChangeEventHandler)Delegate.Remove(this._onRowChangedDelegate, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Data.DataRow" /> is changing.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060007C5 RID: 1989 RVA: 0x000222EE File Offset: 0x000204EE
		// (remove) Token: 0x060007C6 RID: 1990 RVA: 0x0002231C File Offset: 0x0002051C
		public event DataRowChangeEventHandler RowChanging
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.add_RowChanging|API> {0}", this.ObjectID);
				this._onRowChangingDelegate = (DataRowChangeEventHandler)Delegate.Combine(this._onRowChangingDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.remove_RowChanging|API> {0}", this.ObjectID);
				this._onRowChangingDelegate = (DataRowChangeEventHandler)Delegate.Remove(this._onRowChangingDelegate, value);
			}
		}

		/// <summary>Occurs before a row in the table is about to be deleted.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060007C7 RID: 1991 RVA: 0x0002234A File Offset: 0x0002054A
		// (remove) Token: 0x060007C8 RID: 1992 RVA: 0x00022378 File Offset: 0x00020578
		public event DataRowChangeEventHandler RowDeleting
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.add_RowDeleting|API> {0}", this.ObjectID);
				this._onRowDeletingDelegate = (DataRowChangeEventHandler)Delegate.Combine(this._onRowDeletingDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.remove_RowDeleting|API> {0}", this.ObjectID);
				this._onRowDeletingDelegate = (DataRowChangeEventHandler)Delegate.Remove(this._onRowDeletingDelegate, value);
			}
		}

		/// <summary>Occurs after a row in the table has been deleted.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060007C9 RID: 1993 RVA: 0x000223A6 File Offset: 0x000205A6
		// (remove) Token: 0x060007CA RID: 1994 RVA: 0x000223D4 File Offset: 0x000205D4
		public event DataRowChangeEventHandler RowDeleted
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.add_RowDeleted|API> {0}", this.ObjectID);
				this._onRowDeletedDelegate = (DataRowChangeEventHandler)Delegate.Combine(this._onRowDeletedDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.remove_RowDeleted|API> {0}", this.ObjectID);
				this._onRowDeletedDelegate = (DataRowChangeEventHandler)Delegate.Remove(this._onRowDeletedDelegate, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Data.DataTable" /> is cleared.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060007CB RID: 1995 RVA: 0x00022402 File Offset: 0x00020602
		// (remove) Token: 0x060007CC RID: 1996 RVA: 0x00022430 File Offset: 0x00020630
		public event DataTableClearEventHandler TableClearing
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.add_TableClearing|API> {0}", this.ObjectID);
				this._onTableClearingDelegate = (DataTableClearEventHandler)Delegate.Combine(this._onTableClearingDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.remove_TableClearing|API> {0}", this.ObjectID);
				this._onTableClearingDelegate = (DataTableClearEventHandler)Delegate.Remove(this._onTableClearingDelegate, value);
			}
		}

		/// <summary>Occurs after a <see cref="T:System.Data.DataTable" /> is cleared.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060007CD RID: 1997 RVA: 0x0002245E File Offset: 0x0002065E
		// (remove) Token: 0x060007CE RID: 1998 RVA: 0x0002248C File Offset: 0x0002068C
		public event DataTableClearEventHandler TableCleared
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.add_TableCleared|API> {0}", this.ObjectID);
				this._onTableClearedDelegate = (DataTableClearEventHandler)Delegate.Combine(this._onTableClearedDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.remove_TableCleared|API> {0}", this.ObjectID);
				this._onTableClearedDelegate = (DataTableClearEventHandler)Delegate.Remove(this._onTableClearedDelegate, value);
			}
		}

		/// <summary>Occurs when a new <see cref="T:System.Data.DataRow" /> is inserted.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060007CF RID: 1999 RVA: 0x000224BA File Offset: 0x000206BA
		// (remove) Token: 0x060007D0 RID: 2000 RVA: 0x000224D3 File Offset: 0x000206D3
		public event DataTableNewRowEventHandler TableNewRow
		{
			add
			{
				this._onTableNewRowDelegate = (DataTableNewRowEventHandler)Delegate.Combine(this._onTableNewRowDelegate, value);
			}
			remove
			{
				this._onTableNewRowDelegate = (DataTableNewRowEventHandler)Delegate.Remove(this._onTableNewRowDelegate, value);
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0001B29C File Offset: 0x0001949C
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x000224EC File Offset: 0x000206EC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				ISite site = this.Site;
				if (value == null && site != null)
				{
					IContainer container = site.Container;
					if (container != null)
					{
						for (int i = 0; i < this.Columns.Count; i++)
						{
							if (this.Columns[i].Site != null)
							{
								container.Remove(this.Columns[i]);
							}
						}
					}
				}
				base.Site = value;
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00022554 File Offset: 0x00020754
		internal DataRow AddRecords(int oldRecord, int newRecord)
		{
			DataRow dataRow;
			if (oldRecord == -1 && newRecord == -1)
			{
				dataRow = this.NewRow(-1);
				this.AddRow(dataRow);
			}
			else
			{
				dataRow = this.NewEmptyRow();
				dataRow._oldRecord = oldRecord;
				dataRow._newRecord = newRecord;
				this.InsertRow(dataRow, -1L);
			}
			return dataRow;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00022599 File Offset: 0x00020799
		internal void AddRow(DataRow row)
		{
			this.AddRow(row, -1);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000225A3 File Offset: 0x000207A3
		internal void AddRow(DataRow row, int proposedID)
		{
			this.InsertRow(row, proposedID, -1);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000225AE File Offset: 0x000207AE
		internal void InsertRow(DataRow row, int proposedID, int pos)
		{
			this.InsertRow(row, (long)proposedID, pos, true);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000225BC File Offset: 0x000207BC
		internal void InsertRow(DataRow row, long proposedID, int pos, bool fireEvent)
		{
			Exception ex = null;
			if (row == null)
			{
				throw ExceptionBuilder.ArgumentNull("row");
			}
			if (row.Table != this)
			{
				throw ExceptionBuilder.RowAlreadyInOtherCollection();
			}
			if (row.rowID != -1L)
			{
				throw ExceptionBuilder.RowAlreadyInTheCollection();
			}
			row.BeginEdit();
			int tempRecord = row._tempRecord;
			row._tempRecord = -1;
			if (proposedID == -1L)
			{
				proposedID = this._nextRowID;
			}
			bool flag;
			if (flag = this._nextRowID <= proposedID)
			{
				this._nextRowID = checked(proposedID + 1L);
			}
			try
			{
				try
				{
					row.rowID = proposedID;
					this.SetNewRecordWorker(row, tempRecord, DataRowAction.Add, false, false, pos, fireEvent, out ex);
				}
				catch
				{
					if (flag && this._nextRowID == proposedID + 1L)
					{
						this._nextRowID = proposedID;
					}
					row.rowID = -1L;
					row._tempRecord = tempRecord;
					throw;
				}
				if (ex != null)
				{
					throw ex;
				}
				if (this.EnforceConstraints && !this._inLoad)
				{
					int count = this._columnCollection.Count;
					for (int i = 0; i < count; i++)
					{
						DataColumn dataColumn = this._columnCollection[i];
						if (dataColumn.Computed)
						{
							dataColumn.CheckColumnConstraint(row, DataRowAction.Add);
						}
					}
				}
			}
			finally
			{
				row.ResetLastChangedColumn();
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000226F0 File Offset: 0x000208F0
		internal void CheckNotModifying(DataRow row)
		{
			if (row._tempRecord != -1)
			{
				row.EndEdit();
			}
		}

		/// <summary>Clears the <see cref="T:System.Data.DataTable" /> of all data.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007D9 RID: 2009 RVA: 0x00022701 File Offset: 0x00020901
		public void Clear()
		{
			this.Clear(true);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0002270C File Offset: 0x0002090C
		internal void Clear(bool clearAll)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataTable.Clear|INFO> {0}, clearAll={1}", this.ObjectID, clearAll);
			try
			{
				this._rowDiffId = null;
				if (this._dataSet != null)
				{
					this._dataSet.OnClearFunctionCalled(this);
				}
				bool flag = this.Rows.Count != 0;
				DataTableClearEventArgs dataTableClearEventArgs = null;
				if (flag)
				{
					dataTableClearEventArgs = new DataTableClearEventArgs(this);
					this.OnTableClearing(dataTableClearEventArgs);
				}
				if (this._dataSet != null && this._dataSet.EnforceConstraints)
				{
					ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this._dataSet, this);
					while (parentForeignKeyConstraintEnumerator.GetNext())
					{
						parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint().CheckCanClearParentTable(this);
					}
				}
				this._recordManager.Clear(clearAll);
				foreach (object obj in this.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataRow._oldRecord = -1;
					dataRow._newRecord = -1;
					dataRow._tempRecord = -1;
					dataRow.rowID = -1L;
					dataRow.RBTreeNodeId = 0;
				}
				this.Rows.ArrayClear();
				this.ResetIndexes();
				if (flag)
				{
					this.OnTableCleared(dataTableClearEventArgs);
				}
				foreach (object obj2 in this.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj2;
					this.EvaluateDependentExpressions(dataColumn);
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000228C8 File Offset: 0x00020AC8
		internal void CascadeAll(DataRow row, DataRowAction action)
		{
			if (this.DataSet != null && this.DataSet._fEnableCascading)
			{
				ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this._dataSet, this);
				while (parentForeignKeyConstraintEnumerator.GetNext())
				{
					parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint().CheckCascade(row, action);
				}
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00022910 File Offset: 0x00020B10
		internal void CommitRow(DataRow row)
		{
			DataRowChangeEventArgs dataRowChangeEventArgs = this.OnRowChanging(null, row, DataRowAction.Commit);
			if (!this._inDataLoad)
			{
				this.CascadeAll(row, DataRowAction.Commit);
			}
			this.SetOldRecord(row, row._newRecord);
			this.OnRowChanged(dataRowChangeEventArgs, row, DataRowAction.Commit);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0002294E File Offset: 0x00020B4E
		internal int Compare(string s1, string s2)
		{
			return this.Compare(s1, s2, null);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0002295C File Offset: 0x00020B5C
		internal int Compare(string s1, string s2, CompareInfo comparer)
		{
			if (s1 == s2)
			{
				return 0;
			}
			if (s1 == null)
			{
				return -1;
			}
			if (s2 == null)
			{
				return 1;
			}
			int i = s1.Length;
			int num = s2.Length;
			while (i > 0)
			{
				if (s1[i - 1] != ' ' && s1[i - 1] != '\u3000')
				{
					IL_006C:
					while (num > 0 && (s2[num - 1] == ' ' || s2[num - 1] == '\u3000'))
					{
						num--;
					}
					return (comparer ?? this.CompareInfo).Compare(s1, 0, i, s2, 0, num, this._compareFlags);
				}
				i--;
			}
			goto IL_006C;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000229F5 File Offset: 0x00020BF5
		internal int IndexOf(string s1, string s2)
		{
			return this.CompareInfo.IndexOf(s1, s2, this._compareFlags);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00022A0A File Offset: 0x00020C0A
		internal bool IsSuffix(string s1, string s2)
		{
			return this.CompareInfo.IsSuffix(s1, s2, this._compareFlags);
		}

		/// <summary>Computes the given expression on the current rows that pass the filter criteria.</summary>
		/// <returns>An <see cref="T:System.Object" />, set to the result of the computation. If the expression evaluates to null, the return value will be <see cref="F:System.DBNull.Value" />.</returns>
		/// <param name="expression">The expression to compute. </param>
		/// <param name="filter">The filter to limit the rows that evaluate in the expression. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007E1 RID: 2017 RVA: 0x00022A20 File Offset: 0x00020C20
		public object Compute(string expression, string filter)
		{
			DataRow[] array = this.Select(filter, "", DataViewRowState.CurrentRows);
			return new DataExpression(this, expression).Evaluate(array);
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IListSource.ContainsListCollection" />.</summary>
		/// <returns>true if the collection is a collection of <see cref="T:System.Collections.IList" /> objects; otherwise, false.</returns>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x000061D5 File Offset: 0x000043D5
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00022A4C File Offset: 0x00020C4C
		internal void CopyRow(DataTable table, DataRow row)
		{
			int num = -1;
			int num2 = -1;
			if (row == null)
			{
				return;
			}
			if (row._oldRecord != -1)
			{
				num = table._recordManager.ImportRecord(row.Table, row._oldRecord);
			}
			if (row._newRecord != -1)
			{
				if (row._newRecord != row._oldRecord)
				{
					num2 = table._recordManager.ImportRecord(row.Table, row._newRecord);
				}
				else
				{
					num2 = num;
				}
			}
			DataRow dataRow = table.AddRecords(num, num2);
			if (row.HasErrors)
			{
				dataRow.RowError = row.RowError;
				DataColumn[] columnsInError = row.GetColumnsInError();
				for (int i = 0; i < columnsInError.Length; i++)
				{
					DataColumn dataColumn = dataRow.Table.Columns[columnsInError[i].ColumnName];
					dataRow.SetColumnError(dataColumn, row.GetColumnError(columnsInError[i]));
				}
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00022B18 File Offset: 0x00020D18
		internal void DeleteRow(DataRow row)
		{
			if (row._newRecord == -1)
			{
				throw ExceptionBuilder.RowAlreadyDeleted();
			}
			this.SetNewRecord(row, -1, DataRowAction.Delete, false, true, false);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00022B35 File Offset: 0x00020D35
		private void CheckPrimaryKey()
		{
			if (this._primaryKey == null)
			{
				throw ExceptionBuilder.TableMissingPrimaryKey();
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00022B45 File Offset: 0x00020D45
		internal DataRow FindByPrimaryKey(object[] values)
		{
			this.CheckPrimaryKey();
			return this.FindRow(this._primaryKey.Key, values);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00022B5F File Offset: 0x00020D5F
		internal DataRow FindByPrimaryKey(object value)
		{
			this.CheckPrimaryKey();
			return this.FindRow(this._primaryKey.Key, value);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00022B7C File Offset: 0x00020D7C
		private DataRow FindRow(DataKey key, object[] values)
		{
			Index index = this.GetIndex(this.NewIndexDesc(key));
			Range range = index.FindRecords(values);
			if (range.IsNull)
			{
				return null;
			}
			return this._recordManager[index.GetRecord(range.Min)];
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00022BC4 File Offset: 0x00020DC4
		private DataRow FindRow(DataKey key, object value)
		{
			Index index = this.GetIndex(this.NewIndexDesc(key));
			Range range = index.FindRecords(value);
			if (range.IsNull)
			{
				return null;
			}
			return this._recordManager[index.GetRecord(range.Min)];
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00022C0C File Offset: 0x00020E0C
		internal string FormatSortString(IndexField[] indexDesc)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (IndexField indexField in indexDesc)
			{
				if (0 < stringBuilder.Length)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(indexField.Column.ColumnName);
				if (indexField.IsDescending)
				{
					stringBuilder.Append(" DESC");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00022C78 File Offset: 0x00020E78
		internal void FreeRecord(ref int record)
		{
			this._recordManager.FreeRecord(ref record);
		}

		/// <summary>Gets a copy of the <see cref="T:System.Data.DataTable" /> that contains all changes made to it since it was loaded or <see cref="M:System.Data.DataTable.AcceptChanges" /> was last called.</summary>
		/// <returns>A copy of the changes from this <see cref="T:System.Data.DataTable" />, or null if no changes are found.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060007EC RID: 2028 RVA: 0x00022C88 File Offset: 0x00020E88
		public DataTable GetChanges()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTable.GetChanges|API> {0}", this.ObjectID);
			DataTable dataTable2;
			try
			{
				DataTable dataTable = this.Clone();
				for (int i = 0; i < this.Rows.Count; i++)
				{
					DataRow dataRow = this.Rows[i];
					if (dataRow._oldRecord != dataRow._newRecord)
					{
						dataTable.ImportRow(dataRow);
					}
				}
				if (dataTable.Rows.Count == 0)
				{
					dataTable2 = null;
				}
				else
				{
					dataTable2 = dataTable;
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataTable2;
		}

		/// <summary>Gets a copy of the <see cref="T:System.Data.DataTable" /> containing all changes made to it since it was last loaded, or since <see cref="M:System.Data.DataTable.AcceptChanges" /> was called, filtered by <see cref="T:System.Data.DataRowState" />.</summary>
		/// <returns>A filtered copy of the <see cref="T:System.Data.DataTable" /> that can have actions performed on it, and later be merged back in the <see cref="T:System.Data.DataTable" /> using <see cref="M:System.Data.DataSet.Merge(System.Data.DataSet)" />. If no rows of the desired <see cref="T:System.Data.DataRowState" /> are found, the method returns null.</returns>
		/// <param name="rowStates">One of the <see cref="T:System.Data.DataRowState" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060007ED RID: 2029 RVA: 0x00022D24 File Offset: 0x00020F24
		public DataTable GetChanges(DataRowState rowStates)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, DataRowState>("<ds.DataTable.GetChanges|API> {0}, rowStates={1}", this.ObjectID, rowStates);
			DataTable dataTable2;
			try
			{
				DataTable dataTable = this.Clone();
				for (int i = 0; i < this.Rows.Count; i++)
				{
					DataRow dataRow = this.Rows[i];
					if ((dataRow.RowState & rowStates) != (DataRowState)0)
					{
						dataTable.ImportRow(dataRow);
					}
				}
				if (dataTable.Rows.Count == 0)
				{
					dataTable2 = null;
				}
				else
				{
					dataTable2 = dataTable;
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataTable2;
		}

		/// <summary>Gets an array of <see cref="T:System.Data.DataRow" /> objects that contain errors.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects that have errors.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007EE RID: 2030 RVA: 0x00022DBC File Offset: 0x00020FBC
		public DataRow[] GetErrors()
		{
			List<DataRow> list = new List<DataRow>();
			for (int i = 0; i < this.Rows.Count; i++)
			{
				DataRow dataRow = this.Rows[i];
				if (dataRow.HasErrors)
				{
					list.Add(dataRow);
				}
			}
			DataRow[] array = this.NewRowArray(list.Count);
			list.CopyTo(array);
			return array;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00022E16 File Offset: 0x00021016
		internal Index GetIndex(IndexField[] indexDesc)
		{
			return this.GetIndex(indexDesc, DataViewRowState.CurrentRows, null);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00022E22 File Offset: 0x00021022
		internal Index GetIndex(string sort, DataViewRowState recordStates, IFilter rowFilter)
		{
			return this.GetIndex(this.ParseSortString(sort), recordStates, rowFilter);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00022E34 File Offset: 0x00021034
		internal Index GetIndex(IndexField[] indexDesc, DataViewRowState recordStates, IFilter rowFilter)
		{
			this._indexesLock.EnterUpgradeableReadLock();
			try
			{
				for (int i = 0; i < this._indexes.Count; i++)
				{
					Index index = this._indexes[i];
					if (index != null && index.Equal(indexDesc, recordStates, rowFilter))
					{
						return index;
					}
				}
			}
			finally
			{
				this._indexesLock.ExitUpgradeableReadLock();
			}
			Index index2 = new Index(this, indexDesc, recordStates, rowFilter);
			index2.AddRef();
			return index2;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that can be bound to a data source from the object.</returns>
		// Token: 0x060007F2 RID: 2034 RVA: 0x00022EB0 File Offset: 0x000210B0
		IList IListSource.GetList()
		{
			return this.DefaultView;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00022EB8 File Offset: 0x000210B8
		internal List<DataViewListener> GetListeners()
		{
			return this._dataViewListeners;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00022EC0 File Offset: 0x000210C0
		internal int GetSpecialHashCode(string name)
		{
			int num = 0;
			while (num < name.Length && '\u3000' > name[num])
			{
				num++;
			}
			if (name.Length == num)
			{
				if (this._hashCodeProvider == null)
				{
					this._hashCodeProvider = StringComparer.Create(this.Locale, true);
				}
				return this._hashCodeProvider.GetHashCode(name);
			}
			return 0;
		}

		/// <summary>Copies a <see cref="T:System.Data.DataRow" /> into a <see cref="T:System.Data.DataTable" />, preserving any property settings, as well as original and current values.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to be imported. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060007F5 RID: 2037 RVA: 0x00022F20 File Offset: 0x00021120
		public void ImportRow(DataRow row)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTable.ImportRow|API> {0}", this.ObjectID);
			try
			{
				int num2 = -1;
				int num3 = -1;
				if (row != null)
				{
					if (row._oldRecord != -1)
					{
						num2 = this._recordManager.ImportRecord(row.Table, row._oldRecord);
					}
					if (row._newRecord != -1)
					{
						if (row.RowState != DataRowState.Unchanged)
						{
							num3 = this._recordManager.ImportRecord(row.Table, row._newRecord);
						}
						else
						{
							num3 = num2;
						}
					}
					if (num2 != -1 || num3 != -1)
					{
						DataRow dataRow = this.AddRecords(num2, num3);
						if (row.HasErrors)
						{
							dataRow.RowError = row.RowError;
							DataColumn[] columnsInError = row.GetColumnsInError();
							for (int i = 0; i < columnsInError.Length; i++)
							{
								DataColumn dataColumn = dataRow.Table.Columns[columnsInError[i].ColumnName];
								dataRow.SetColumnError(dataColumn, row.GetColumnError(columnsInError[i]));
							}
						}
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002302C File Offset: 0x0002122C
		internal void InsertRow(DataRow row, long proposedID)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataTable.InsertRow|INFO> {0}, row={1}", this.ObjectID, row._objectID);
			try
			{
				if (row.Table != this)
				{
					throw ExceptionBuilder.RowAlreadyInOtherCollection();
				}
				if (row.rowID != -1L)
				{
					throw ExceptionBuilder.RowAlreadyInTheCollection();
				}
				if (row._oldRecord == -1 && row._newRecord == -1)
				{
					throw ExceptionBuilder.RowEmpty();
				}
				if (proposedID == -1L)
				{
					proposedID = this._nextRowID;
				}
				row.rowID = proposedID;
				if (this._nextRowID <= proposedID)
				{
					this._nextRowID = checked(proposedID + 1L);
				}
				DataRowChangeEventArgs dataRowChangeEventArgs = null;
				if (row._newRecord != -1)
				{
					row._tempRecord = row._newRecord;
					row._newRecord = -1;
					try
					{
						dataRowChangeEventArgs = this.RaiseRowChanging(null, row, DataRowAction.Add, true);
					}
					catch
					{
						row._tempRecord = -1;
						throw;
					}
					row._newRecord = row._tempRecord;
					row._tempRecord = -1;
				}
				if (row._oldRecord != -1)
				{
					this._recordManager[row._oldRecord] = row;
				}
				if (row._newRecord != -1)
				{
					this._recordManager[row._newRecord] = row;
				}
				this.Rows.ArrayAdd(row);
				if (row.RowState == DataRowState.Unchanged)
				{
					this.RecordStateChanged(row._oldRecord, DataViewRowState.None, DataViewRowState.Unchanged);
				}
				else
				{
					this.RecordStateChanged(row._oldRecord, DataViewRowState.None, row.GetRecordState(row._oldRecord), row._newRecord, DataViewRowState.None, row.GetRecordState(row._newRecord));
				}
				if (this._dependentColumns != null && this._dependentColumns.Count > 0)
				{
					this.EvaluateExpressions(row, DataRowAction.Add, null);
				}
				this.RaiseRowChanged(dataRowChangeEventArgs, row, DataRowAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000231F0 File Offset: 0x000213F0
		private IndexField[] NewIndexDesc(DataKey key)
		{
			IndexField[] indexDesc = key.GetIndexDesc();
			IndexField[] array = new IndexField[indexDesc.Length];
			Array.Copy(indexDesc, 0, array, 0, indexDesc.Length);
			return array;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0002321B File Offset: 0x0002141B
		internal int NewRecord()
		{
			return this.NewRecord(-1);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00023224 File Offset: 0x00021424
		internal int NewUninitializedRecord()
		{
			return this._recordManager.NewRecordBase();
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00023234 File Offset: 0x00021434
		internal int NewRecordFromArray(object[] value)
		{
			int count = this._columnCollection.Count;
			if (count < value.Length)
			{
				throw ExceptionBuilder.ValueArrayLength();
			}
			int num = this._recordManager.NewRecordBase();
			int num2;
			try
			{
				for (int i = 0; i < value.Length; i++)
				{
					if (value[i] != null)
					{
						this._columnCollection[i][num] = value[i];
					}
					else
					{
						this._columnCollection[i].Init(num);
					}
				}
				for (int j = value.Length; j < count; j++)
				{
					this._columnCollection[j].Init(num);
				}
				num2 = num;
			}
			catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
			{
				this.FreeRecord(ref num);
				throw;
			}
			return num2;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000232FC File Offset: 0x000214FC
		internal int NewRecord(int sourceRecord)
		{
			int num = this._recordManager.NewRecordBase();
			int count = this._columnCollection.Count;
			if (-1 == sourceRecord)
			{
				for (int i = 0; i < count; i++)
				{
					this._columnCollection[i].Init(num);
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					this._columnCollection[j].Copy(sourceRecord, num);
				}
			}
			return num;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00023368 File Offset: 0x00021568
		internal DataRow NewEmptyRow()
		{
			this._rowBuilder._record = -1;
			DataRow dataRow = this.NewRowFromBuilder(this._rowBuilder);
			if (this._dataSet != null)
			{
				this.DataSet.OnDataRowCreated(dataRow);
			}
			return dataRow;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000233A3 File Offset: 0x000215A3
		private DataRow NewUninitializedRow()
		{
			return this.NewRow(this.NewUninitializedRecord());
		}

		/// <summary>Creates a new <see cref="T:System.Data.DataRow" /> with the same schema as the table.</summary>
		/// <returns>A <see cref="T:System.Data.DataRow" /> with the same schema as the <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060007FE RID: 2046 RVA: 0x000233B4 File Offset: 0x000215B4
		public DataRow NewRow()
		{
			DataRow dataRow = this.NewRow(-1);
			this.NewRowCreated(dataRow);
			return dataRow;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000233D4 File Offset: 0x000215D4
		internal DataRow CreateEmptyRow()
		{
			DataRow dataRow = this.NewUninitializedRow();
			foreach (object obj in this.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (!XmlToDatasetMap.IsMappedColumn(dataColumn))
				{
					if (!dataColumn.AutoIncrement)
					{
						if (dataColumn.AllowDBNull)
						{
							dataRow[dataColumn] = DBNull.Value;
						}
						else if (dataColumn.DefaultValue != null)
						{
							dataRow[dataColumn] = dataColumn.DefaultValue;
						}
					}
					else
					{
						dataColumn.Init(dataRow._tempRecord);
					}
				}
			}
			return dataRow;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00023478 File Offset: 0x00021678
		private void NewRowCreated(DataRow row)
		{
			if (this._onTableNewRowDelegate != null)
			{
				DataTableNewRowEventArgs dataTableNewRowEventArgs = new DataTableNewRowEventArgs(row);
				this.OnTableNewRow(dataTableNewRowEventArgs);
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0002349C File Offset: 0x0002169C
		internal DataRow NewRow(int record)
		{
			if (-1 == record)
			{
				record = this.NewRecord(-1);
			}
			this._rowBuilder._record = record;
			DataRow dataRow = this.NewRowFromBuilder(this._rowBuilder);
			this._recordManager[record] = dataRow;
			if (this._dataSet != null)
			{
				this.DataSet.OnDataRowCreated(dataRow);
			}
			return dataRow;
		}

		/// <summary>Creates a new row from an existing row.</summary>
		/// <returns>A <see cref="T:System.Data.DataRow" /> derived class.</returns>
		/// <param name="builder">A <see cref="T:System.Data.DataRowBuilder" /> object. </param>
		// Token: 0x06000802 RID: 2050 RVA: 0x000234F1 File Offset: 0x000216F1
		protected virtual DataRow NewRowFromBuilder(DataRowBuilder builder)
		{
			return new DataRow(builder);
		}

		/// <summary>Gets the row type.</summary>
		/// <returns>Returns the type of the <see cref="T:System.Data.DataRow" />.</returns>
		// Token: 0x06000803 RID: 2051 RVA: 0x000234F9 File Offset: 0x000216F9
		protected virtual Type GetRowType()
		{
			return typeof(DataRow);
		}

		/// <summary>Returns an array of <see cref="T:System.Data.DataRow" />.</summary>
		/// <returns>The new array.</returns>
		/// <param name="size">A <see cref="T:System.Int32" /> value that describes the size of the array.</param>
		// Token: 0x06000804 RID: 2052 RVA: 0x00023508 File Offset: 0x00021708
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected internal DataRow[] NewRowArray(int size)
		{
			if (this.IsTypedDataTable)
			{
				if (size == 0)
				{
					if (this._emptyDataRowArray == null)
					{
						this._emptyDataRowArray = (DataRow[])Array.CreateInstance(this.GetRowType(), 0);
					}
					return this._emptyDataRowArray;
				}
				return (DataRow[])Array.CreateInstance(this.GetRowType(), size);
			}
			else
			{
				if (size != 0)
				{
					return new DataRow[size];
				}
				return Array.Empty<DataRow>();
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00023567 File Offset: 0x00021767
		internal bool NeedColumnChangeEvents
		{
			get
			{
				return this.IsTypedDataTable || this._onColumnChangingDelegate != null || this._onColumnChangedDelegate != null;
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.ColumnChanging" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataColumnChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x06000806 RID: 2054 RVA: 0x00023584 File Offset: 0x00021784
		protected internal virtual void OnColumnChanging(DataColumnChangeEventArgs e)
		{
			if (this._onColumnChangingDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.OnColumnChanging|INFO> {0}", this.ObjectID);
				this._onColumnChangingDelegate(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.ColumnChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataColumnChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x06000807 RID: 2055 RVA: 0x000235B0 File Offset: 0x000217B0
		protected internal virtual void OnColumnChanged(DataColumnChangeEventArgs e)
		{
			if (this._onColumnChangedDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.OnColumnChanged|INFO> {0}", this.ObjectID);
				this._onColumnChangedDelegate(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" /> event.</summary>
		/// <param name="pcevent">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06000808 RID: 2056 RVA: 0x000235DC File Offset: 0x000217DC
		protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			if (this._onPropertyChangingDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.OnPropertyChanging|INFO> {0}", this.ObjectID);
				this._onPropertyChangingDelegate(this, pcevent);
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00023608 File Offset: 0x00021808
		internal void OnRemoveColumnInternal(DataColumn column)
		{
			this.OnRemoveColumn(column);
		}

		/// <summary>Notifies the <see cref="T:System.Data.DataTable" /> that a <see cref="T:System.Data.DataColumn" /> is being removed.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> being removed. </param>
		// Token: 0x0600080A RID: 2058 RVA: 0x00005E03 File Offset: 0x00004003
		protected virtual void OnRemoveColumn(DataColumn column)
		{
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00023611 File Offset: 0x00021811
		private DataRowChangeEventArgs OnRowChanged(DataRowChangeEventArgs args, DataRow eRow, DataRowAction eAction)
		{
			if (this._onRowChangedDelegate != null || this.IsTypedDataTable)
			{
				if (args == null)
				{
					args = new DataRowChangeEventArgs(eRow, eAction);
				}
				this.OnRowChanged(args);
			}
			return args;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00023637 File Offset: 0x00021837
		private DataRowChangeEventArgs OnRowChanging(DataRowChangeEventArgs args, DataRow eRow, DataRowAction eAction)
		{
			if (this._onRowChangingDelegate != null || this.IsTypedDataTable)
			{
				if (args == null)
				{
					args = new DataRowChangeEventArgs(eRow, eAction);
				}
				this.OnRowChanging(args);
			}
			return args;
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x0600080D RID: 2061 RVA: 0x0002365D File Offset: 0x0002185D
		protected virtual void OnRowChanged(DataRowChangeEventArgs e)
		{
			if (this._onRowChangedDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.OnRowChanged|INFO> {0}", this.ObjectID);
				this._onRowChangedDelegate(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.RowChanging" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x0600080E RID: 2062 RVA: 0x00023689 File Offset: 0x00021889
		protected virtual void OnRowChanging(DataRowChangeEventArgs e)
		{
			if (this._onRowChangingDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.OnRowChanging|INFO> {0}", this.ObjectID);
				this._onRowChangingDelegate(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.RowDeleting" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x0600080F RID: 2063 RVA: 0x000236B5 File Offset: 0x000218B5
		protected virtual void OnRowDeleting(DataRowChangeEventArgs e)
		{
			if (this._onRowDeletingDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.OnRowDeleting|INFO> {0}", this.ObjectID);
				this._onRowDeletingDelegate(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x06000810 RID: 2064 RVA: 0x000236E1 File Offset: 0x000218E1
		protected virtual void OnRowDeleted(DataRowChangeEventArgs e)
		{
			if (this._onRowDeletedDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.OnRowDeleted|INFO> {0}", this.ObjectID);
				this._onRowDeletedDelegate(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.TableCleared" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataTableClearEventArgs" /> that contains the event data. </param>
		// Token: 0x06000811 RID: 2065 RVA: 0x0002370D File Offset: 0x0002190D
		protected virtual void OnTableCleared(DataTableClearEventArgs e)
		{
			if (this._onTableClearedDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.OnTableCleared|INFO> {0}", this.ObjectID);
				this._onTableClearedDelegate(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.TableClearing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataTableClearEventArgs" /> that contains the event data. </param>
		// Token: 0x06000812 RID: 2066 RVA: 0x00023739 File Offset: 0x00021939
		protected virtual void OnTableClearing(DataTableClearEventArgs e)
		{
			if (this._onTableClearingDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.OnTableClearing|INFO> {0}", this.ObjectID);
				this._onTableClearingDelegate(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.TableNewRow" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataTableNewRowEventArgs" /> that contains the event data. </param>
		// Token: 0x06000813 RID: 2067 RVA: 0x00023765 File Offset: 0x00021965
		protected virtual void OnTableNewRow(DataTableNewRowEventArgs e)
		{
			if (this._onTableNewRowDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.OnTableNewRow|INFO> {0}", this.ObjectID);
				this._onTableNewRowDelegate(this, e);
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00023791 File Offset: 0x00021991
		private void OnInitialized()
		{
			if (this._onInitialized != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTable.OnInitialized|INFO> {0}", this.ObjectID);
				this._onInitialized(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x000237C4 File Offset: 0x000219C4
		internal IndexField[] ParseSortString(string sortString)
		{
			IndexField[] array = Array.Empty<IndexField>();
			if (sortString != null && 0 < sortString.Length)
			{
				string[] array2 = sortString.Split(new char[] { ',' });
				array = new IndexField[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i].Trim();
					int length = text.Length;
					bool flag = false;
					if (length >= 5 && string.Compare(text, length - 4, " ASC", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
					{
						text = text.Substring(0, length - 4).Trim();
					}
					else if (length >= 6 && string.Compare(text, length - 5, " DESC", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag = true;
						text = text.Substring(0, length - 5).Trim();
					}
					if (text.StartsWith("[", StringComparison.Ordinal))
					{
						if (!text.EndsWith("]", StringComparison.Ordinal))
						{
							throw ExceptionBuilder.InvalidSortString(array2[i]);
						}
						text = text.Substring(1, text.Length - 2);
					}
					DataColumn dataColumn = this.Columns[text];
					if (dataColumn == null)
					{
						throw ExceptionBuilder.ColumnOutOfRange(text);
					}
					array[i] = new IndexField(dataColumn, flag);
				}
			}
			return array;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000238E6 File Offset: 0x00021AE6
		internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000238F4 File Offset: 0x00021AF4
		internal void RecordChanged(int record)
		{
			this.SetShadowIndexes();
			try
			{
				int count = this._shadowIndexes.Count;
				for (int i = 0; i < count; i++)
				{
					Index index = this._shadowIndexes[i];
					if (0 < index.RefCount)
					{
						index.RecordChanged(record);
					}
				}
			}
			finally
			{
				this.RestoreShadowIndexes();
			}
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00023958 File Offset: 0x00021B58
		internal void RecordChanged(int[] oldIndex, int[] newIndex)
		{
			this.SetShadowIndexes();
			try
			{
				int count = this._shadowIndexes.Count;
				for (int i = 0; i < count; i++)
				{
					Index index = this._shadowIndexes[i];
					if (0 < index.RefCount)
					{
						index.RecordChanged(oldIndex[i], newIndex[i]);
					}
				}
			}
			finally
			{
				this.RestoreShadowIndexes();
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000239C0 File Offset: 0x00021BC0
		internal void RecordStateChanged(int record, DataViewRowState oldState, DataViewRowState newState)
		{
			this.SetShadowIndexes();
			try
			{
				int count = this._shadowIndexes.Count;
				for (int i = 0; i < count; i++)
				{
					Index index = this._shadowIndexes[i];
					if (0 < index.RefCount)
					{
						index.RecordStateChanged(record, oldState, newState);
					}
				}
			}
			finally
			{
				this.RestoreShadowIndexes();
			}
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00023A24 File Offset: 0x00021C24
		internal void RecordStateChanged(int record1, DataViewRowState oldState1, DataViewRowState newState1, int record2, DataViewRowState oldState2, DataViewRowState newState2)
		{
			this.SetShadowIndexes();
			try
			{
				int count = this._shadowIndexes.Count;
				for (int i = 0; i < count; i++)
				{
					Index index = this._shadowIndexes[i];
					if (0 < index.RefCount)
					{
						if (record1 != -1 && record2 != -1)
						{
							index.RecordStateChanged(record1, oldState1, newState1, record2, oldState2, newState2);
						}
						else if (record1 != -1)
						{
							index.RecordStateChanged(record1, oldState1, newState1);
						}
						else if (record2 != -1)
						{
							index.RecordStateChanged(record2, oldState2, newState2);
						}
					}
				}
			}
			finally
			{
				this.RestoreShadowIndexes();
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00023AB8 File Offset: 0x00021CB8
		internal int[] RemoveRecordFromIndexes(DataRow row, DataRowVersion version)
		{
			int num = this.LiveIndexes.Count;
			int[] array = new int[num];
			int recordFromVersion = row.GetRecordFromVersion(version);
			DataViewRowState recordState = row.GetRecordState(recordFromVersion);
			while (--num >= 0)
			{
				if (row.HasVersion(version) && (recordState & this._indexes[num].RecordStates) != DataViewRowState.None)
				{
					int index = this._indexes[num].GetIndex(recordFromVersion);
					if (index > -1)
					{
						array[num] = index;
						this._indexes[num].DeleteRecordFromIndex(index);
					}
					else
					{
						array[num] = -1;
					}
				}
				else
				{
					array[num] = -1;
				}
			}
			return array;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00023B50 File Offset: 0x00021D50
		internal int[] InsertRecordToIndexes(DataRow row, DataRowVersion version)
		{
			int num = this.LiveIndexes.Count;
			int[] array = new int[num];
			int recordFromVersion = row.GetRecordFromVersion(version);
			DataViewRowState recordState = row.GetRecordState(recordFromVersion);
			while (--num >= 0)
			{
				if (row.HasVersion(version))
				{
					if ((recordState & this._indexes[num].RecordStates) != DataViewRowState.None)
					{
						array[num] = this._indexes[num].InsertRecordToIndex(recordFromVersion);
					}
					else
					{
						array[num] = -1;
					}
				}
			}
			return array;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00023BC4 File Offset: 0x00021DC4
		internal void SilentlySetValue(DataRow dr, DataColumn dc, DataRowVersion version, object newValue)
		{
			int recordFromVersion = dr.GetRecordFromVersion(version);
			if ((DataStorage.IsTypeCustomType(dc.DataType) && newValue != dc[recordFromVersion]) || !dc.CompareValueTo(recordFromVersion, newValue, true))
			{
				int[] array = dr.Table.RemoveRecordFromIndexes(dr, version);
				dc.SetValue(recordFromVersion, newValue);
				int[] array2 = dr.Table.InsertRecordToIndexes(dr, version);
				if (dr.HasVersion(version))
				{
					if (version != DataRowVersion.Original)
					{
						dr.Table.RecordChanged(array, array2);
					}
					if (dc._dependentColumns != null)
					{
						dc.Table.EvaluateDependentExpressions(dc._dependentColumns, dr, version, null);
					}
				}
			}
			dr.ResetLastChangedColumn();
		}

		/// <summary>Rolls back all changes that have been made to the table since it was loaded, or the last time <see cref="M:System.Data.DataTable.AcceptChanges" /> was called.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600081E RID: 2078 RVA: 0x00023C6C File Offset: 0x00021E6C
		public void RejectChanges()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTable.RejectChanges|API> {0}", this.ObjectID);
			try
			{
				DataRow[] array = new DataRow[this.Rows.Count];
				this.Rows.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					this.RollbackRow(array[i]);
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00023CE4 File Offset: 0x00021EE4
		internal void RemoveRow(DataRow row, bool check)
		{
			if (row.rowID == -1L)
			{
				throw ExceptionBuilder.RowAlreadyRemoved();
			}
			if (check && this._dataSet != null)
			{
				ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this._dataSet, this);
				while (parentForeignKeyConstraintEnumerator.GetNext())
				{
					parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint().CheckCanRemoveParentRow(row);
				}
			}
			int num = row._oldRecord;
			int newRecord = row._newRecord;
			DataViewRowState recordState = row.GetRecordState(num);
			DataViewRowState recordState2 = row.GetRecordState(newRecord);
			row._oldRecord = -1;
			row._newRecord = -1;
			if (num == newRecord)
			{
				num = -1;
			}
			this.RecordStateChanged(num, recordState, DataViewRowState.None, newRecord, recordState2, DataViewRowState.None);
			this.FreeRecord(ref num);
			this.FreeRecord(ref newRecord);
			row.rowID = -1L;
			this.Rows.ArrayRemove(row);
		}

		/// <summary>Resets the <see cref="T:System.Data.DataTable" /> to its original state. Reset removes all data, indexes, relations, and columns of the table. If a DataSet includes a DataTable, the table will still be part of the DataSet after the table is reset.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000820 RID: 2080 RVA: 0x00023D94 File Offset: 0x00021F94
		public virtual void Reset()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTable.Reset|API> {0}", this.ObjectID);
			try
			{
				this.Clear();
				this.ResetConstraints();
				DataRelationCollection dataRelationCollection = this.ParentRelations;
				int i = dataRelationCollection.Count;
				while (i > 0)
				{
					i--;
					dataRelationCollection.RemoveAt(i);
				}
				dataRelationCollection = this.ChildRelations;
				i = dataRelationCollection.Count;
				while (i > 0)
				{
					i--;
					dataRelationCollection.RemoveAt(i);
				}
				this.Columns.Clear();
				this._indexes.Clear();
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00023E38 File Offset: 0x00022038
		internal void ResetIndexes()
		{
			this.ResetInternalIndexes(null);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00023E44 File Offset: 0x00022044
		internal void ResetInternalIndexes(DataColumn column)
		{
			this.SetShadowIndexes();
			try
			{
				int count = this._shadowIndexes.Count;
				for (int i = 0; i < count; i++)
				{
					Index index = this._shadowIndexes[i];
					if (0 < index.RefCount)
					{
						if (column == null)
						{
							index.Reset();
						}
						else
						{
							bool flag = false;
							foreach (IndexField indexField in index._indexFields)
							{
								if (column == indexField.Column)
								{
									flag = true;
									break;
								}
							}
							if (flag)
							{
								index.Reset();
							}
						}
					}
				}
			}
			finally
			{
				this.RestoreShadowIndexes();
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00023EE8 File Offset: 0x000220E8
		internal void RollbackRow(DataRow row)
		{
			row.CancelEdit();
			this.SetNewRecord(row, row._oldRecord, DataRowAction.Rollback, false, true, false);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00023F04 File Offset: 0x00022104
		private DataRowChangeEventArgs RaiseRowChanged(DataRowChangeEventArgs args, DataRow eRow, DataRowAction eAction)
		{
			try
			{
				if (this.UpdatingCurrent(eRow, eAction) && (this.IsTypedDataTable || this._onRowChangedDelegate != null))
				{
					args = this.OnRowChanged(args, eRow, eAction);
				}
				else if (DataRowAction.Delete == eAction && eRow._newRecord == -1 && (this.IsTypedDataTable || this._onRowDeletedDelegate != null))
				{
					if (args == null)
					{
						args = new DataRowChangeEventArgs(eRow, eAction);
					}
					this.OnRowDeleted(args);
				}
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
			}
			return args;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00023FA0 File Offset: 0x000221A0
		private DataRowChangeEventArgs RaiseRowChanging(DataRowChangeEventArgs args, DataRow eRow, DataRowAction eAction)
		{
			if (this.UpdatingCurrent(eRow, eAction) && (this.IsTypedDataTable || this._onRowChangingDelegate != null))
			{
				eRow._inChangingEvent = true;
				try
				{
					return this.OnRowChanging(args, eRow, eAction);
				}
				finally
				{
					eRow._inChangingEvent = false;
				}
			}
			if (DataRowAction.Delete == eAction && eRow._newRecord != -1 && (this.IsTypedDataTable || this._onRowDeletingDelegate != null))
			{
				eRow._inDeletingEvent = true;
				try
				{
					if (args == null)
					{
						args = new DataRowChangeEventArgs(eRow, eAction);
					}
					this.OnRowDeleting(args);
				}
				finally
				{
					eRow._inDeletingEvent = false;
				}
			}
			return args;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00024044 File Offset: 0x00022244
		private DataRowChangeEventArgs RaiseRowChanging(DataRowChangeEventArgs args, DataRow eRow, DataRowAction eAction, bool fireEvent)
		{
			if (this.EnforceConstraints && !this._inLoad)
			{
				int count = this._columnCollection.Count;
				for (int i = 0; i < count; i++)
				{
					DataColumn dataColumn = this._columnCollection[i];
					if (!dataColumn.Computed || eAction != DataRowAction.Add)
					{
						dataColumn.CheckColumnConstraint(eRow, eAction);
					}
				}
				int count2 = this._constraintCollection.Count;
				for (int j = 0; j < count2; j++)
				{
					this._constraintCollection[j].CheckConstraint(eRow, eAction);
				}
			}
			if (fireEvent)
			{
				args = this.RaiseRowChanging(args, eRow, eAction);
			}
			if (!this._inDataLoad && !this.MergingData && eAction != DataRowAction.Nothing && eAction != DataRowAction.ChangeOriginal)
			{
				this.CascadeAll(eRow, eAction);
			}
			return args;
		}

		/// <summary>Gets an array of all <see cref="T:System.Data.DataRow" /> objects.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000827 RID: 2087 RVA: 0x000240FB File Offset: 0x000222FB
		public DataRow[] Select()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.DataTable.Select|API> {0}", this.ObjectID);
			return new Select(this, "", "", DataViewRowState.CurrentRows).SelectRows();
		}

		/// <summary>Gets an array of all <see cref="T:System.Data.DataRow" /> objects that match the filter criteria.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects.</returns>
		/// <param name="filterExpression">The criteria to use to filter the rows. For examples on how to filter rows, see DataView RowFilter Syntax [C#].</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000828 RID: 2088 RVA: 0x00024129 File Offset: 0x00022329
		public DataRow[] Select(string filterExpression)
		{
			DataCommonEventSource.Log.Trace<int, string>("<ds.DataTable.Select|API> {0}, filterExpression='{1}'", this.ObjectID, filterExpression);
			return new Select(this, filterExpression, "", DataViewRowState.CurrentRows).SelectRows();
		}

		/// <summary>Gets an array of all <see cref="T:System.Data.DataRow" /> objects that match the filter criteria, in the specified sort order.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects matching the filter expression.</returns>
		/// <param name="filterExpression">The criteria to use to filter the rows. For examples on how to filter rows, see DataView RowFilter Syntax [C#].</param>
		/// <param name="sort">A string specifying the column and sort direction. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000829 RID: 2089 RVA: 0x00024154 File Offset: 0x00022354
		public DataRow[] Select(string filterExpression, string sort)
		{
			DataCommonEventSource.Log.Trace<int, string, string>("<ds.DataTable.Select|API> {0}, filterExpression='{1}', sort='{2}'", this.ObjectID, filterExpression, sort);
			return new Select(this, filterExpression, sort, DataViewRowState.CurrentRows).SelectRows();
		}

		/// <summary>Gets an array of all <see cref="T:System.Data.DataRow" /> objects that match the filter in the order of the sort that match the specified state.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects.</returns>
		/// <param name="filterExpression">The criteria to use to filter the rows. For examples on how to filter rows, see DataView RowFilter Syntax [C#].</param>
		/// <param name="sort">A string specifying the column and sort direction. </param>
		/// <param name="recordStates">One of the <see cref="T:System.Data.DataViewRowState" /> values.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600082A RID: 2090 RVA: 0x0002417C File Offset: 0x0002237C
		public DataRow[] Select(string filterExpression, string sort, DataViewRowState recordStates)
		{
			DataCommonEventSource.Log.Trace<int, string, string, DataViewRowState>("<ds.DataTable.Select|API> {0}, filterExpression='{1}', sort='{2}', recordStates={3}", this.ObjectID, filterExpression, sort, recordStates);
			return new Select(this, filterExpression, sort, recordStates).SelectRows();
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000241A4 File Offset: 0x000223A4
		internal void SetNewRecord(DataRow row, int proposedRecord, DataRowAction action = DataRowAction.Change, bool isInMerge = false, bool fireEvent = true, bool suppressEnsurePropertyChanged = false)
		{
			Exception ex = null;
			this.SetNewRecordWorker(row, proposedRecord, action, isInMerge, suppressEnsurePropertyChanged, -1, fireEvent, out ex);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000241CC File Offset: 0x000223CC
		private void SetNewRecordWorker(DataRow row, int proposedRecord, DataRowAction action, bool isInMerge, bool suppressEnsurePropertyChanged, int position, bool fireEvent, out Exception deferredException)
		{
			deferredException = null;
			if (row._tempRecord != proposedRecord)
			{
				if (!this._inDataLoad)
				{
					row.CheckInTable();
					this.CheckNotModifying(row);
				}
				if (proposedRecord == row._newRecord)
				{
					if (isInMerge)
					{
						this.RaiseRowChanged(null, row, action);
					}
					return;
				}
				row._tempRecord = proposedRecord;
			}
			DataRowChangeEventArgs dataRowChangeEventArgs = null;
			try
			{
				row._action = action;
				dataRowChangeEventArgs = this.RaiseRowChanging(null, row, action, fireEvent);
			}
			catch
			{
				row._tempRecord = -1;
				throw;
			}
			finally
			{
				row._action = DataRowAction.Nothing;
			}
			row._tempRecord = -1;
			int num = row._newRecord;
			int num2 = ((proposedRecord != -1) ? proposedRecord : ((row.RowState != DataRowState.Unchanged) ? row._oldRecord : (-1)));
			if (action == DataRowAction.Add)
			{
				if (position == -1)
				{
					this.Rows.ArrayAdd(row);
				}
				else
				{
					this.Rows.ArrayInsert(row, position);
				}
			}
			List<DataRow> list = null;
			if ((action == DataRowAction.Delete || action == DataRowAction.Change) && this._dependentColumns != null && this._dependentColumns.Count > 0)
			{
				list = new List<DataRow>();
				for (int i = 0; i < this.ParentRelations.Count; i++)
				{
					DataRelation dataRelation = this.ParentRelations[i];
					if (dataRelation.ChildTable == row.Table)
					{
						list.InsertRange(list.Count, row.GetParentRows(dataRelation));
					}
				}
				for (int j = 0; j < this.ChildRelations.Count; j++)
				{
					DataRelation dataRelation2 = this.ChildRelations[j];
					if (dataRelation2.ParentTable == row.Table)
					{
						list.InsertRange(list.Count, row.GetChildRows(dataRelation2));
					}
				}
			}
			if (!suppressEnsurePropertyChanged && !row.HasPropertyChanged && row._newRecord != proposedRecord && -1 != proposedRecord && -1 != row._newRecord)
			{
				row.LastChangedColumn = null;
				row.LastChangedColumn = null;
			}
			if (this.LiveIndexes.Count != 0)
			{
				if (-1 == num && -1 != proposedRecord && -1 != row._oldRecord && proposedRecord != row._oldRecord)
				{
					num = row._oldRecord;
				}
				DataViewRowState recordState = row.GetRecordState(num);
				DataViewRowState recordState2 = row.GetRecordState(num2);
				row._newRecord = proposedRecord;
				if (proposedRecord != -1)
				{
					this._recordManager[proposedRecord] = row;
				}
				DataViewRowState recordState3 = row.GetRecordState(num);
				DataViewRowState recordState4 = row.GetRecordState(num2);
				this.RecordStateChanged(num, recordState, recordState3, num2, recordState2, recordState4);
			}
			else
			{
				row._newRecord = proposedRecord;
				if (proposedRecord != -1)
				{
					this._recordManager[proposedRecord] = row;
				}
			}
			row.ResetLastChangedColumn();
			if (-1 != num && num != row._oldRecord && num != row._tempRecord && num != row._newRecord && row == this._recordManager[num])
			{
				this.FreeRecord(ref num);
			}
			if (row.RowState == DataRowState.Detached && row.rowID != -1L)
			{
				this.RemoveRow(row, false);
			}
			if (this._dependentColumns != null && this._dependentColumns.Count > 0)
			{
				try
				{
					this.EvaluateExpressions(row, action, list);
				}
				catch (Exception ex)
				{
					if (action != DataRowAction.Add)
					{
						throw ex;
					}
					deferredException = ex;
				}
			}
			try
			{
				if (fireEvent)
				{
					this.RaiseRowChanged(dataRowChangeEventArgs, row, action);
				}
			}
			catch (Exception ex2) when (ADP.IsCatchableExceptionType(ex2))
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex2);
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00024510 File Offset: 0x00022710
		internal void SetOldRecord(DataRow row, int proposedRecord)
		{
			if (!this._inDataLoad)
			{
				row.CheckInTable();
				this.CheckNotModifying(row);
			}
			if (proposedRecord == row._oldRecord)
			{
				return;
			}
			int num = row._oldRecord;
			try
			{
				if (this.LiveIndexes.Count != 0)
				{
					if (-1 == num && -1 != proposedRecord && -1 != row._newRecord && proposedRecord != row._newRecord)
					{
						num = row._newRecord;
					}
					DataViewRowState recordState = row.GetRecordState(num);
					DataViewRowState recordState2 = row.GetRecordState(proposedRecord);
					row._oldRecord = proposedRecord;
					if (proposedRecord != -1)
					{
						this._recordManager[proposedRecord] = row;
					}
					DataViewRowState recordState3 = row.GetRecordState(num);
					DataViewRowState recordState4 = row.GetRecordState(proposedRecord);
					this.RecordStateChanged(num, recordState, recordState3, proposedRecord, recordState2, recordState4);
				}
				else
				{
					row._oldRecord = proposedRecord;
					if (proposedRecord != -1)
					{
						this._recordManager[proposedRecord] = row;
					}
				}
			}
			finally
			{
				if (num != -1 && num != row._tempRecord && num != row._oldRecord && num != row._newRecord)
				{
					this.FreeRecord(ref num);
				}
				if (row.RowState == DataRowState.Detached && row.rowID != -1L)
				{
					this.RemoveRow(row, false);
				}
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00024628 File Offset: 0x00022828
		private void RestoreShadowIndexes()
		{
			this._shadowCount--;
			if (this._shadowCount == 0)
			{
				this._shadowIndexes = null;
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00024647 File Offset: 0x00022847
		private void SetShadowIndexes()
		{
			if (this._shadowIndexes == null)
			{
				this._shadowIndexes = this.LiveIndexes;
				this._shadowCount = 1;
				return;
			}
			this._shadowCount++;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00024673 File Offset: 0x00022873
		internal void ShadowIndexCopy()
		{
			if (this._shadowIndexes == this._indexes)
			{
				this._shadowIndexes = new List<Index>(this._indexes);
			}
		}

		/// <summary>Gets the <see cref="P:System.Data.DataTable.TableName" /> and <see cref="P:System.Data.DataTable.DisplayExpression" />, if there is one as a concatenated string.</summary>
		/// <returns>A string consisting of the <see cref="P:System.Data.DataTable.TableName" /> and the <see cref="P:System.Data.DataTable.DisplayExpression" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000831 RID: 2097 RVA: 0x00024694 File Offset: 0x00022894
		public override string ToString()
		{
			if (this._displayExpression != null)
			{
				return this.TableName + " + " + this.DisplayExpressionInternal;
			}
			return this.TableName;
		}

		/// <summary>Turns off notifications, index maintenance, and constraints while loading data.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000832 RID: 2098 RVA: 0x000246BC File Offset: 0x000228BC
		public void BeginLoadData()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTable.BeginLoadData|API> {0}", this.ObjectID);
			try
			{
				if (!this._inDataLoad)
				{
					this._inDataLoad = true;
					this._loadIndex = null;
					this._initialLoad = this.Rows.Count == 0;
					if (this._initialLoad)
					{
						this.SuspendIndexEvents();
					}
					else
					{
						if (this._primaryKey != null)
						{
							this._loadIndex = this._primaryKey.Key.GetSortIndex(DataViewRowState.OriginalRows);
						}
						if (this._loadIndex != null)
						{
							this._loadIndex.AddRef();
						}
					}
					if (this.DataSet != null)
					{
						this._savedEnforceConstraints = this.DataSet.EnforceConstraints;
						this.DataSet.EnforceConstraints = false;
					}
					else
					{
						this.EnforceConstraints = false;
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Turns on notifications, index maintenance, and constraints after loading data.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000833 RID: 2099 RVA: 0x000247A0 File Offset: 0x000229A0
		public void EndLoadData()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTable.EndLoadData|API> {0}", this.ObjectID);
			try
			{
				if (this._inDataLoad)
				{
					if (this._loadIndex != null)
					{
						this._loadIndex.RemoveRef();
					}
					if (this._loadIndexwithOriginalAdded != null)
					{
						this._loadIndexwithOriginalAdded.RemoveRef();
					}
					if (this._loadIndexwithCurrentDeleted != null)
					{
						this._loadIndexwithCurrentDeleted.RemoveRef();
					}
					this._loadIndex = null;
					this._loadIndexwithOriginalAdded = null;
					this._loadIndexwithCurrentDeleted = null;
					this._inDataLoad = false;
					this.RestoreIndexEvents(false);
					if (this.DataSet != null)
					{
						this.DataSet.EnforceConstraints = this._savedEnforceConstraints;
					}
					else
					{
						this.EnforceConstraints = true;
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Finds and updates a specific row. If no matching row is found, a new row is created using the given values.</summary>
		/// <returns>The new <see cref="T:System.Data.DataRow" />.</returns>
		/// <param name="values">An array of values used to create the new row. </param>
		/// <param name="fAcceptChanges">true to accept changes; otherwise false. </param>
		/// <exception cref="T:System.ArgumentException">The array is larger than the number of columns in the table. </exception>
		/// <exception cref="T:System.InvalidCastException">A value doesn't match its respective column type. </exception>
		/// <exception cref="T:System.Data.ConstraintException">Adding the row invalidates a constraint. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">Attempting to put a null in a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is false. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000834 RID: 2100 RVA: 0x00024870 File Offset: 0x00022A70
		public DataRow LoadDataRow(object[] values, bool fAcceptChanges)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataTable.LoadDataRow|API> {0}, fAcceptChanges={1}", this.ObjectID, fAcceptChanges);
			DataRow dataRow2;
			try
			{
				if (this._inDataLoad)
				{
					int num2 = this.NewRecordFromArray(values);
					DataRow dataRow;
					if (this._loadIndex != null)
					{
						int num3 = this._loadIndex.FindRecord(num2);
						if (num3 != -1)
						{
							int record = this._loadIndex.GetRecord(num3);
							dataRow = this._recordManager[record];
							dataRow.CancelEdit();
							if (dataRow.RowState == DataRowState.Deleted)
							{
								this.SetNewRecord(dataRow, dataRow._oldRecord, DataRowAction.Rollback, false, true, false);
							}
							this.SetNewRecord(dataRow, num2, DataRowAction.Change, false, true, false);
							if (fAcceptChanges)
							{
								dataRow.AcceptChanges();
							}
							return dataRow;
						}
					}
					dataRow = this.NewRow(num2);
					this.AddRow(dataRow);
					if (fAcceptChanges)
					{
						dataRow.AcceptChanges();
					}
					dataRow2 = dataRow;
				}
				else
				{
					DataRow dataRow = this.UpdatingAdd(values);
					if (fAcceptChanges)
					{
						dataRow.AcceptChanges();
					}
					dataRow2 = dataRow;
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataRow2;
		}

		/// <summary>Finds and updates a specific row. If no matching row is found, a new row is created using the given values.</summary>
		/// <returns>The new <see cref="T:System.Data.DataRow" />.</returns>
		/// <param name="values">An array of values used to create the new row. </param>
		/// <param name="loadOption">Used to determine how the array values are applied to the corresponding values in an existing row. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000835 RID: 2101 RVA: 0x00024968 File Offset: 0x00022B68
		public DataRow LoadDataRow(object[] values, LoadOption loadOption)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, LoadOption>("<ds.DataTable.LoadDataRow|API> {0}, loadOption={1}", this.ObjectID, loadOption);
			DataRow dataRow;
			try
			{
				Index index = null;
				if (this._primaryKey != null)
				{
					if (loadOption == LoadOption.Upsert)
					{
						if (this._loadIndexwithCurrentDeleted == null)
						{
							this._loadIndexwithCurrentDeleted = this._primaryKey.Key.GetSortIndex(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent);
							if (this._loadIndexwithCurrentDeleted != null)
							{
								this._loadIndexwithCurrentDeleted.AddRef();
							}
						}
						index = this._loadIndexwithCurrentDeleted;
					}
					else
					{
						if (this._loadIndexwithOriginalAdded == null)
						{
							this._loadIndexwithOriginalAdded = this._primaryKey.Key.GetSortIndex(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedOriginal);
							if (this._loadIndexwithOriginalAdded != null)
							{
								this._loadIndexwithOriginalAdded.AddRef();
							}
						}
						index = this._loadIndexwithOriginalAdded;
					}
				}
				if (this._inDataLoad && !this.AreIndexEventsSuspended)
				{
					this.SuspendIndexEvents();
				}
				dataRow = this.LoadRow(values, loadOption, index);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataRow;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00024A58 File Offset: 0x00022C58
		internal DataRow UpdatingAdd(object[] values)
		{
			Index index = null;
			if (this._primaryKey != null)
			{
				index = this._primaryKey.Key.GetSortIndex(DataViewRowState.OriginalRows);
			}
			if (index == null)
			{
				return this.Rows.Add(values);
			}
			int num = this.NewRecordFromArray(values);
			int num2 = index.FindRecord(num);
			if (num2 != -1)
			{
				int record = index.GetRecord(num2);
				DataRow dataRow = this._recordManager[record];
				dataRow.RejectChanges();
				this.SetNewRecord(dataRow, num, DataRowAction.Change, false, true, false);
				return dataRow;
			}
			DataRow dataRow2 = this.NewRow(num);
			this.Rows.Add(dataRow2);
			return dataRow2;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00024AF0 File Offset: 0x00022CF0
		internal bool UpdatingCurrent(DataRow row, DataRowAction action)
		{
			return action == DataRowAction.Add || action == DataRowAction.Change || action == DataRowAction.Rollback || action == DataRowAction.ChangeOriginal || action == DataRowAction.ChangeCurrentAndOriginal;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00024B0C File Offset: 0x00022D0C
		internal DataColumn AddUniqueKey(int position)
		{
			if (this._colUnique != null)
			{
				return this._colUnique;
			}
			DataColumn[] primaryKey = this.PrimaryKey;
			if (primaryKey.Length == 1)
			{
				return primaryKey[0];
			}
			DataColumn dataColumn = new DataColumn(XMLSchema.GenUniqueColumnName(this.TableName + "_Id", this), typeof(int), null, MappingType.Hidden);
			dataColumn.Prefix = this._tablePrefix;
			dataColumn.AutoIncrement = true;
			dataColumn.AllowDBNull = false;
			dataColumn.Unique = true;
			if (position == -1)
			{
				this.Columns.Add(dataColumn);
			}
			else
			{
				for (int i = this.Columns.Count - 1; i >= position; i--)
				{
					this.Columns[i].SetOrdinalInternal(i + 1);
				}
				this.Columns.AddAt(position, dataColumn);
				dataColumn.SetOrdinalInternal(position);
			}
			if (primaryKey.Length == 0)
			{
				this.PrimaryKey = new DataColumn[] { dataColumn };
			}
			this._colUnique = dataColumn;
			return this._colUnique;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00024BF5 File Offset: 0x00022DF5
		internal DataColumn AddUniqueKey()
		{
			return this.AddUniqueKey(-1);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00024C00 File Offset: 0x00022E00
		internal DataColumn AddForeignKey(DataColumn parentKey)
		{
			DataColumn dataColumn = new DataColumn(XMLSchema.GenUniqueColumnName(parentKey.ColumnName, this), parentKey.DataType, null, MappingType.Hidden);
			this.Columns.Add(dataColumn);
			return dataColumn;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00024C34 File Offset: 0x00022E34
		internal void UpdatePropertyDescriptorCollectionCache()
		{
			this._propertyDescriptorCollectionCache = null;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00024C40 File Offset: 0x00022E40
		internal PropertyDescriptorCollection GetPropertyDescriptorCollection(Attribute[] attributes)
		{
			if (this._propertyDescriptorCollectionCache == null)
			{
				int count = this.Columns.Count;
				int count2 = this.ChildRelations.Count;
				PropertyDescriptor[] array = new PropertyDescriptor[count + count2];
				for (int i = 0; i < count; i++)
				{
					array[i] = new DataColumnPropertyDescriptor(this.Columns[i]);
				}
				for (int j = 0; j < count2; j++)
				{
					array[count + j] = new DataRelationPropertyDescriptor(this.ChildRelations[j]);
				}
				this._propertyDescriptorCollectionCache = new PropertyDescriptorCollection(array);
			}
			return this._propertyDescriptorCollectionCache;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00024CD0 File Offset: 0x00022ED0
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x00024CEB File Offset: 0x00022EEB
		internal XmlQualifiedName TypeName
		{
			get
			{
				if (this._typeName != null)
				{
					return (XmlQualifiedName)this._typeName;
				}
				return XmlQualifiedName.Empty;
			}
			set
			{
				this._typeName = value;
			}
		}

		/// <summary>Merge the specified <see cref="T:System.Data.DataTable" /> with the current <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> to be merged with the current <see cref="T:System.Data.DataTable" />.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600083F RID: 2111 RVA: 0x00024CF4 File Offset: 0x00022EF4
		public void Merge(DataTable table)
		{
			this.Merge(table, false, MissingSchemaAction.Add);
		}

		/// <summary>Merge the specified <see cref="T:System.Data.DataTable" /> with the current DataTable, indicating whether to preserve changes in the current DataTable.</summary>
		/// <param name="table">The DataTable to be merged with the current DataTable.</param>
		/// <param name="preserveChanges">true, to preserve changes in the current DataTable; otherwise false. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000840 RID: 2112 RVA: 0x00024CFF File Offset: 0x00022EFF
		public void Merge(DataTable table, bool preserveChanges)
		{
			this.Merge(table, preserveChanges, MissingSchemaAction.Add);
		}

		/// <summary>Merge the specified <see cref="T:System.Data.DataTable" /> with the current DataTable, indicating whether to preserve changes and how to handle missing schema in the current DataTable.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> to be merged with the current <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="preserveChanges">true, to preserve changes in the current <see cref="T:System.Data.DataTable" />; otherwise false.</param>
		/// <param name="missingSchemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000841 RID: 2113 RVA: 0x00024D0C File Offset: 0x00022F0C
		public void Merge(DataTable table, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, bool, MissingSchemaAction>("<ds.DataTable.Merge|API> {0}, table={1}, preserveChanges={2}, missingSchemaAction={3}", this.ObjectID, (table != null) ? table.ObjectID : 0, preserveChanges, missingSchemaAction);
			try
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				new Merger(this, preserveChanges, missingSchemaAction).MergeTable(table);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Fills a <see cref="T:System.Data.DataTable" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />. If the <see cref="T:System.Data.DataTable" /> already contains rows, the incoming data from the data source is merged with the existing rows.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides a result set.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000842 RID: 2114 RVA: 0x00024D84 File Offset: 0x00022F84
		public void Load(IDataReader reader)
		{
			this.Load(reader, LoadOption.PreserveChanges, null);
		}

		/// <summary>Fills a <see cref="T:System.Data.DataTable" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />. If the DataTable already contains rows, the incoming data from the data source is merged with the existing rows according to the value of the <paramref name="loadOption" /> parameter.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets.</param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> are combined with incoming rows that share the same primary key. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000843 RID: 2115 RVA: 0x00024D8F File Offset: 0x00022F8F
		public void Load(IDataReader reader, LoadOption loadOption)
		{
			this.Load(reader, loadOption, null);
		}

		/// <summary>Fills a <see cref="T:System.Data.DataTable" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" /> using an error-handling delegate.</summary>
		/// <param name="reader">A <see cref="T:System.Data.IDataReader" /> that provides a result set.</param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> are combined with incoming rows that share the same primary key. </param>
		/// <param name="errorHandler">A <see cref="T:System.Data.FillErrorEventHandler" /> delegate to call when an error occurs while loading data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000844 RID: 2116 RVA: 0x00024D9C File Offset: 0x00022F9C
		public virtual void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler errorHandler)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, LoadOption>("<ds.DataTable.Load|API> {0}, loadOption={1}", this.ObjectID, loadOption);
			try
			{
				if (this.PrimaryKey.Length == 0)
				{
					DataTableReader dataTableReader = reader as DataTableReader;
					if (dataTableReader != null && dataTableReader.CurrentDataTable == this)
					{
						return;
					}
				}
				LoadAdapter loadAdapter = new LoadAdapter();
				loadAdapter.FillLoadOption = loadOption;
				loadAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				if (errorHandler != null)
				{
					loadAdapter.FillError += errorHandler;
				}
				loadAdapter.FillFromReader(new DataTable[] { this }, reader, 0, 0);
				if (!reader.IsClosed && !reader.NextResult())
				{
					reader.Close();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00024E44 File Offset: 0x00023044
		private DataRow LoadRow(object[] values, LoadOption loadOption, Index searchIndex)
		{
			DataRow dataRow = null;
			int num2;
			if (searchIndex != null)
			{
				int[] array = Array.Empty<int>();
				if (this._primaryKey != null)
				{
					array = new int[this._primaryKey.ColumnsReference.Length];
					for (int i = 0; i < this._primaryKey.ColumnsReference.Length; i++)
					{
						array[i] = this._primaryKey.ColumnsReference[i].Ordinal;
					}
				}
				object[] array2 = new object[array.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = values[array[j]];
				}
				Range range = searchIndex.FindRecords(array2);
				if (!range.IsNull)
				{
					int num = 0;
					for (int k = range.Min; k <= range.Max; k++)
					{
						int record = searchIndex.GetRecord(k);
						dataRow = this._recordManager[record];
						num2 = this.NewRecordFromArray(values);
						for (int l = 0; l < values.Length; l++)
						{
							if (values[l] == null)
							{
								this._columnCollection[l].Copy(record, num2);
							}
						}
						for (int m = values.Length; m < this._columnCollection.Count; m++)
						{
							this._columnCollection[m].Copy(record, num2);
						}
						if (loadOption != LoadOption.Upsert || dataRow.RowState != DataRowState.Deleted)
						{
							this.SetDataRowWithLoadOption(dataRow, num2, loadOption, true);
						}
						else
						{
							num++;
						}
					}
					if (num == 0)
					{
						return dataRow;
					}
				}
			}
			num2 = this.NewRecordFromArray(values);
			dataRow = this.NewRow(num2);
			DataRowAction dataRowAction;
			if (loadOption - LoadOption.OverwriteChanges > 1)
			{
				if (loadOption != LoadOption.Upsert)
				{
					throw ExceptionBuilder.ArgumentOutOfRange("LoadOption");
				}
				dataRowAction = DataRowAction.Add;
			}
			else
			{
				dataRowAction = DataRowAction.ChangeCurrentAndOriginal;
			}
			DataRowChangeEventArgs dataRowChangeEventArgs = this.RaiseRowChanging(null, dataRow, dataRowAction);
			this.InsertRow(dataRow, -1L, -1, false);
			if (loadOption - LoadOption.OverwriteChanges > 1)
			{
				if (loadOption != LoadOption.Upsert)
				{
					throw ExceptionBuilder.ArgumentOutOfRange("LoadOption");
				}
			}
			else
			{
				this.SetOldRecord(dataRow, num2);
			}
			this.RaiseRowChanged(dataRowChangeEventArgs, dataRow, dataRowAction);
			return dataRow;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002502C File Offset: 0x0002322C
		private void SetDataRowWithLoadOption(DataRow dataRow, int recordNo, LoadOption loadOption, bool checkReadOnly)
		{
			bool flag = false;
			if (checkReadOnly)
			{
				foreach (object obj in this.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.ReadOnly && !dataColumn.Computed)
					{
						switch (loadOption)
						{
						case LoadOption.OverwriteChanges:
							if (dataRow[dataColumn, DataRowVersion.Current] != dataColumn[recordNo] || dataRow[dataColumn, DataRowVersion.Original] != dataColumn[recordNo])
							{
								flag = true;
							}
							break;
						case LoadOption.PreserveChanges:
							if (dataRow[dataColumn, DataRowVersion.Original] != dataColumn[recordNo])
							{
								flag = true;
							}
							break;
						case LoadOption.Upsert:
							if (dataRow[dataColumn, DataRowVersion.Current] != dataColumn[recordNo])
							{
								flag = true;
							}
							break;
						}
					}
				}
			}
			DataRowChangeEventArgs dataRowChangeEventArgs = null;
			DataRowAction dataRowAction = DataRowAction.Nothing;
			int tempRecord = dataRow._tempRecord;
			dataRow._tempRecord = recordNo;
			switch (loadOption)
			{
			case LoadOption.OverwriteChanges:
				dataRowAction = DataRowAction.ChangeCurrentAndOriginal;
				break;
			case LoadOption.PreserveChanges:
			{
				DataRowState dataRowState = dataRow.RowState;
				if (dataRowState == DataRowState.Unchanged)
				{
					dataRowAction = DataRowAction.ChangeCurrentAndOriginal;
				}
				else
				{
					dataRowAction = DataRowAction.ChangeOriginal;
				}
				break;
			}
			case LoadOption.Upsert:
			{
				DataRowState dataRowState = dataRow.RowState;
				if (dataRowState != DataRowState.Unchanged)
				{
					if (dataRowState == DataRowState.Deleted)
					{
						break;
					}
				}
				else
				{
					using (IEnumerator enumerator = dataRow.Table.Columns.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							DataColumn dataColumn2 = (DataColumn)obj2;
							if (dataColumn2.Compare(dataRow._newRecord, recordNo) != 0)
							{
								dataRowAction = DataRowAction.Change;
								break;
							}
						}
						break;
					}
				}
				dataRowAction = DataRowAction.Change;
				break;
			}
			default:
				throw ExceptionBuilder.ArgumentOutOfRange("LoadOption");
			}
			try
			{
				dataRowChangeEventArgs = this.RaiseRowChanging(null, dataRow, dataRowAction);
				if (dataRowAction == DataRowAction.Nothing)
				{
					dataRow._inChangingEvent = true;
					try
					{
						dataRowChangeEventArgs = this.OnRowChanging(dataRowChangeEventArgs, dataRow, dataRowAction);
					}
					finally
					{
						dataRow._inChangingEvent = false;
					}
				}
			}
			finally
			{
				if (DataRowState.Detached == dataRow.RowState)
				{
					if (-1 != tempRecord)
					{
						this.FreeRecord(ref tempRecord);
					}
				}
				else if (dataRow._tempRecord != recordNo)
				{
					if (-1 != tempRecord)
					{
						this.FreeRecord(ref tempRecord);
					}
					if (-1 != recordNo)
					{
						this.FreeRecord(ref recordNo);
					}
					recordNo = dataRow._tempRecord;
				}
				else
				{
					dataRow._tempRecord = tempRecord;
				}
			}
			if (dataRow._tempRecord != -1)
			{
				dataRow.CancelEdit();
			}
			switch (loadOption)
			{
			case LoadOption.OverwriteChanges:
				this.SetNewRecord(dataRow, recordNo, DataRowAction.Change, false, false, false);
				this.SetOldRecord(dataRow, recordNo);
				break;
			case LoadOption.PreserveChanges:
				if (dataRow.RowState == DataRowState.Unchanged)
				{
					this.SetOldRecord(dataRow, recordNo);
					this.SetNewRecord(dataRow, recordNo, DataRowAction.Change, false, false, false);
				}
				else
				{
					this.SetOldRecord(dataRow, recordNo);
				}
				break;
			case LoadOption.Upsert:
				if (dataRow.RowState == DataRowState.Unchanged)
				{
					this.SetNewRecord(dataRow, recordNo, DataRowAction.Change, false, false, false);
					if (!dataRow.HasChanges())
					{
						this.SetOldRecord(dataRow, recordNo);
					}
				}
				else
				{
					if (dataRow.RowState == DataRowState.Deleted)
					{
						dataRow.RejectChanges();
					}
					this.SetNewRecord(dataRow, recordNo, DataRowAction.Change, false, false, false);
				}
				break;
			default:
				throw ExceptionBuilder.ArgumentOutOfRange("LoadOption");
			}
			if (flag)
			{
				string text = "ReadOnly Data is Modified.";
				if (dataRow.RowError.Length == 0)
				{
					dataRow.RowError = text;
				}
				else
				{
					dataRow.RowError = dataRow.RowError + " ]:[ " + text;
				}
				foreach (object obj3 in this.Columns)
				{
					DataColumn dataColumn3 = (DataColumn)obj3;
					if (dataColumn3.ReadOnly && !dataColumn3.Computed)
					{
						dataRow.SetColumnError(dataColumn3, text);
					}
				}
			}
			dataRowChangeEventArgs = this.RaiseRowChanged(dataRowChangeEventArgs, dataRow, dataRowAction);
			if (dataRowAction == DataRowAction.Nothing)
			{
				dataRow._inChangingEvent = true;
				try
				{
					this.OnRowChanged(dataRowChangeEventArgs, dataRow, dataRowAction);
				}
				finally
				{
					dataRow._inChangingEvent = false;
				}
			}
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTableReader" /> corresponding to the data within this <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTableReader" /> containing one result set, corresponding to the source <see cref="T:System.Data.DataTable" /> instance.</returns>
		// Token: 0x06000847 RID: 2119 RVA: 0x00025410 File Offset: 0x00023610
		public DataTableReader CreateDataReader()
		{
			return new DataTableReader(this);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The stream to which the data will be written. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000848 RID: 2120 RVA: 0x00025418 File Offset: 0x00023618
		public void WriteXml(Stream stream)
		{
			this.WriteXml(stream, XmlWriteMode.IgnoreSchema, false);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified <see cref="T:System.IO.Stream" />. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="stream">The stream to which the data will be written. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000849 RID: 2121 RVA: 0x00025423 File Offset: 0x00023623
		public void WriteXml(Stream stream, bool writeHierarchy)
		{
			this.WriteXml(stream, XmlWriteMode.IgnoreSchema, writeHierarchy);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> with which to write the content. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600084A RID: 2122 RVA: 0x0002542E File Offset: 0x0002362E
		public void WriteXml(TextWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema, false);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified <see cref="T:System.IO.TextWriter" />. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> with which to write the content. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600084B RID: 2123 RVA: 0x00025439 File Offset: 0x00023639
		public void WriteXml(TextWriter writer, bool writeHierarchy)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema, writeHierarchy);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write the contents. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600084C RID: 2124 RVA: 0x00025444 File Offset: 0x00023644
		public void WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema, false);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified <see cref="T:System.Xml.XmlWriter" />. </summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write the contents. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600084D RID: 2125 RVA: 0x0002544F File Offset: 0x0002364F
		public void WriteXml(XmlWriter writer, bool writeHierarchy)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema, writeHierarchy);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified file.</summary>
		/// <param name="fileName">The file to which to write the XML data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600084E RID: 2126 RVA: 0x0002545A File Offset: 0x0002365A
		public void WriteXml(string fileName)
		{
			this.WriteXml(fileName, XmlWriteMode.IgnoreSchema, false);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified file. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="fileName">The file to which to write the XML data.</param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600084F RID: 2127 RVA: 0x00025465 File Offset: 0x00023665
		public void WriteXml(string fileName, bool writeHierarchy)
		{
			this.WriteXml(fileName, XmlWriteMode.IgnoreSchema, writeHierarchy);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> to the specified file using the specified <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="stream">The stream to which the data will be written. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000850 RID: 2128 RVA: 0x00025470 File Offset: 0x00023670
		public void WriteXml(Stream stream, XmlWriteMode mode)
		{
			this.WriteXml(stream, mode, false);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> to the specified file using the specified <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="stream">The stream to which the data will be written. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000851 RID: 2129 RVA: 0x0002547C File Offset: 0x0002367C
		public void WriteXml(Stream stream, XmlWriteMode mode, bool writeHierarchy)
		{
			if (stream != null)
			{
				this.WriteXml(new XmlTextWriter(stream, null)
				{
					Formatting = Formatting.Indented
				}, mode, writeHierarchy);
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> used to write the document. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000852 RID: 2130 RVA: 0x000254A4 File Offset: 0x000236A4
		public void WriteXml(TextWriter writer, XmlWriteMode mode)
		{
			this.WriteXml(writer, mode, false);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> used to write the document. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000853 RID: 2131 RVA: 0x000254B0 File Offset: 0x000236B0
		public void WriteXml(TextWriter writer, XmlWriteMode mode, bool writeHierarchy)
		{
			if (writer != null)
			{
				this.WriteXml(new XmlTextWriter(writer)
				{
					Formatting = Formatting.Indented
				}, mode, writeHierarchy);
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.Xml.XmlWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the document. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000854 RID: 2132 RVA: 0x000254D7 File Offset: 0x000236D7
		public void WriteXml(XmlWriter writer, XmlWriteMode mode)
		{
			this.WriteXml(writer, mode, false);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.Xml.XmlWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the document. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000855 RID: 2133 RVA: 0x000254E4 File Offset: 0x000236E4
		public void WriteXml(XmlWriter writer, XmlWriteMode mode, bool writeHierarchy)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, XmlWriteMode>("<ds.DataTable.WriteXml|API> {0}, mode={1}", this.ObjectID, mode);
			try
			{
				if (this._tableName.Length == 0)
				{
					throw ExceptionBuilder.CanNotSerializeDataTableWithEmptyName();
				}
				if (writer != null)
				{
					if (mode == XmlWriteMode.DiffGram)
					{
						new NewDiffgramGen(this, writeHierarchy).Save(writer, this);
					}
					else if (mode == XmlWriteMode.WriteSchema)
					{
						DataSet dataSet = null;
						string tableNamespace = this._tableNamespace;
						if (this.DataSet == null)
						{
							dataSet = new DataSet();
							dataSet.SetLocaleValue(this._culture, this._cultureUserSet);
							dataSet.CaseSensitive = this.CaseSensitive;
							dataSet.Namespace = this.Namespace;
							dataSet.RemotingFormat = this.RemotingFormat;
							dataSet.Tables.Add(this);
						}
						if (writer != null)
						{
							new XmlDataTreeWriter(this, writeHierarchy).Save(writer, true);
						}
						if (dataSet != null)
						{
							dataSet.Tables.Remove(this);
							this._tableNamespace = tableNamespace;
						}
					}
					else
					{
						new XmlDataTreeWriter(this, writeHierarchy).Save(writer, false);
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> using the specified file and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="fileName">The name of the file to which the data will be written. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000856 RID: 2134 RVA: 0x000255EC File Offset: 0x000237EC
		public void WriteXml(string fileName, XmlWriteMode mode)
		{
			this.WriteXml(fileName, mode, false);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> using the specified file and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="fileName">The name of the file to which the data will be written. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000857 RID: 2135 RVA: 0x000255F8 File Offset: 0x000237F8
		public void WriteXml(string fileName, XmlWriteMode mode, bool writeHierarchy)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, string, XmlWriteMode>("<ds.DataTable.WriteXml|API> {0}, fileName='{1}', mode={2}", this.ObjectID, fileName, mode);
			try
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null))
				{
					xmlTextWriter.Formatting = Formatting.Indented;
					xmlTextWriter.WriteStartDocument(true);
					this.WriteXml(xmlTextWriter, mode, writeHierarchy);
					xmlTextWriter.WriteEndDocument();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema to the specified stream.</summary>
		/// <param name="stream">The stream to which the XML schema will be written. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000858 RID: 2136 RVA: 0x00025678 File Offset: 0x00023878
		public void WriteXmlSchema(Stream stream)
		{
			this.WriteXmlSchema(stream, false);
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema to the specified stream. To save the schema for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="stream">The stream to which the XML schema will be written. </param>
		/// <param name="writeHierarchy">If true, write the schema of the current table and all its descendants. If false (the default value), write the schema for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000859 RID: 2137 RVA: 0x00025684 File Offset: 0x00023884
		public void WriteXmlSchema(Stream stream, bool writeHierarchy)
		{
			if (stream == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented
			}, writeHierarchy);
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> with which to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600085A RID: 2138 RVA: 0x000256AC File Offset: 0x000238AC
		public void WriteXmlSchema(TextWriter writer)
		{
			this.WriteXmlSchema(writer, false);
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema using the specified <see cref="T:System.IO.TextWriter" />. To save the schema for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> with which to write. </param>
		/// <param name="writeHierarchy">If true, write the schema of the current table and all its descendants. If false (the default value), write the schema for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600085B RID: 2139 RVA: 0x000256B8 File Offset: 0x000238B8
		public void WriteXmlSchema(TextWriter writer, bool writeHierarchy)
		{
			if (writer == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented
			}, writeHierarchy);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000256E0 File Offset: 0x000238E0
		private bool CheckForClosureOnExpressions(DataTable dt, bool writeHierarchy)
		{
			List<DataTable> list = new List<DataTable>();
			list.Add(dt);
			if (writeHierarchy)
			{
				this.CreateTableList(dt, list);
			}
			return this.CheckForClosureOnExpressionTables(list);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0002570C File Offset: 0x0002390C
		private bool CheckForClosureOnExpressionTables(List<DataTable> tableList)
		{
			foreach (DataTable dataTable in tableList)
			{
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.Expression.Length != 0)
					{
						DataColumn[] dependency = dataColumn.DataExpression.GetDependency();
						for (int i = 0; i < dependency.Length; i++)
						{
							if (!tableList.Contains(dependency[i].Table))
							{
								return false;
							}
						}
					}
				}
			}
			return true;
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema using the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to use. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600085E RID: 2142 RVA: 0x000257E0 File Offset: 0x000239E0
		public void WriteXmlSchema(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, false);
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema using the specified <see cref="T:System.Xml.XmlWriter" />. To save the schema for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the document. </param>
		/// <param name="writeHierarchy">If true, write the schema of the current table and all its descendants. If false (the default value), write the schema for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600085F RID: 2143 RVA: 0x000257EC File Offset: 0x000239EC
		public void WriteXmlSchema(XmlWriter writer, bool writeHierarchy)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTable.WriteXmlSchema|API> {0}", this.ObjectID);
			try
			{
				if (this._tableName.Length == 0)
				{
					throw ExceptionBuilder.CanNotSerializeDataTableWithEmptyName();
				}
				if (!this.CheckForClosureOnExpressions(this, writeHierarchy))
				{
					throw ExceptionBuilder.CanNotSerializeDataTableHierarchy();
				}
				DataSet dataSet = null;
				string tableNamespace = this._tableNamespace;
				if (this.DataSet == null)
				{
					dataSet = new DataSet();
					dataSet.SetLocaleValue(this._culture, this._cultureUserSet);
					dataSet.CaseSensitive = this.CaseSensitive;
					dataSet.Namespace = this.Namespace;
					dataSet.RemotingFormat = this.RemotingFormat;
					dataSet.Tables.Add(this);
				}
				if (writer != null)
				{
					new XmlTreeGen(SchemaFormat.Public).Save(null, this, writer, writeHierarchy);
				}
				if (dataSet != null)
				{
					dataSet.Tables.Remove(this);
					this._tableNamespace = tableNamespace;
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema to the specified file.</summary>
		/// <param name="fileName">The name of the file to use. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000860 RID: 2144 RVA: 0x000258D4 File Offset: 0x00023AD4
		public void WriteXmlSchema(string fileName)
		{
			this.WriteXmlSchema(fileName, false);
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema to the specified file. To save the schema for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="fileName">The name of the file to use. </param>
		/// <param name="writeHierarchy">If true, write the schema of the current table and all its descendants. If false (the default value), write the schema for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000861 RID: 2145 RVA: 0x000258E0 File Offset: 0x00023AE0
		public void WriteXmlSchema(string fileName, bool writeHierarchy)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			try
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument(true);
				this.WriteXmlSchema(xmlTextWriter, writeHierarchy);
				xmlTextWriter.WriteEndDocument();
			}
			finally
			{
				xmlTextWriter.Close();
			}
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="stream">An object that derives from <see cref="T:System.IO.Stream" /></param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000862 RID: 2146 RVA: 0x0002592C File Offset: 0x00023B2C
		public XmlReadMode ReadXml(Stream stream)
		{
			if (stream == null)
			{
				return XmlReadMode.Auto;
			}
			return this.ReadXml(new XmlTextReader(stream)
			{
				XmlResolver = null
			}, false);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> that will be used to read the data.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000863 RID: 2147 RVA: 0x00025954 File Offset: 0x00023B54
		public XmlReadMode ReadXml(TextReader reader)
		{
			if (reader == null)
			{
				return XmlReadMode.Auto;
			}
			return this.ReadXml(new XmlTextReader(reader)
			{
				XmlResolver = null
			}, false);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataTable" /> from the specified file.</summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="fileName">The name of the file from which to read the data. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000864 RID: 2148 RVA: 0x0002597C File Offset: 0x00023B7C
		public XmlReadMode ReadXml(string fileName)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			xmlTextReader.XmlResolver = null;
			XmlReadMode xmlReadMode;
			try
			{
				xmlReadMode = this.ReadXml(xmlTextReader, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
			return xmlReadMode;
		}

		/// <summary>Reads XML Schema and Data into the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> that will be used to read the data. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000865 RID: 2149 RVA: 0x000259BC File Offset: 0x00023BBC
		public XmlReadMode ReadXml(XmlReader reader)
		{
			return this.ReadXml(reader, false);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000259C6 File Offset: 0x00023BC6
		private void RestoreConstraint(bool originalEnforceConstraint)
		{
			if (this.DataSet != null)
			{
				this.DataSet.EnforceConstraints = originalEnforceConstraint;
				return;
			}
			this.EnforceConstraints = originalEnforceConstraint;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000259E4 File Offset: 0x00023BE4
		private bool IsEmptyXml(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				if (reader.AttributeCount == 0 || (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1"))
				{
					return true;
				}
				if (reader.AttributeCount == 1)
				{
					reader.MoveToAttribute(0);
					if (this.Namespace == reader.Value && this.Prefix == reader.LocalName && reader.Prefix == "xmlns" && reader.NamespaceURI == "http://www.w3.org/2000/xmlns/")
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00025A88 File Offset: 0x00023C88
		internal XmlReadMode ReadXml(XmlReader reader, bool denyResolving)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataTable.ReadXml|INFO> {0}, denyResolving={1}", this.ObjectID, denyResolving);
			XmlReadMode xmlReadMode2;
			try
			{
				DataTable.RowDiffIdUsageSection rowDiffIdUsageSection = default(DataTable.RowDiffIdUsageSection);
				try
				{
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					XmlReadMode xmlReadMode = XmlReadMode.Auto;
					rowDiffIdUsageSection.Prepare(this);
					if (reader == null)
					{
						xmlReadMode2 = xmlReadMode;
					}
					else
					{
						bool flag4;
						if (this.DataSet != null)
						{
							flag4 = this.DataSet.EnforceConstraints;
							this.DataSet.EnforceConstraints = false;
						}
						else
						{
							flag4 = this.EnforceConstraints;
							this.EnforceConstraints = false;
						}
						if (reader is XmlTextReader)
						{
							((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
						}
						XmlDocument xmlDocument = new XmlDocument();
						XmlDataLoader xmlDataLoader = null;
						reader.MoveToContent();
						if (this.Columns.Count == 0 && this.IsEmptyXml(reader))
						{
							reader.Read();
							xmlReadMode2 = xmlReadMode;
						}
						else
						{
							if (reader.NodeType == XmlNodeType.Element)
							{
								int depth = reader.Depth;
								if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (this.Columns.Count != 0)
									{
										this.ReadXmlDiffgram(reader);
										this.ReadEndElement(reader);
										this.RestoreConstraint(flag4);
										return XmlReadMode.DiffGram;
									}
									if (reader.IsEmptyElement)
									{
										reader.Read();
										return XmlReadMode.DiffGram;
									}
									throw ExceptionBuilder.DataTableInferenceNotSupported();
								}
								else
								{
									if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
									{
										this.ReadXDRSchema(reader);
										this.RestoreConstraint(flag4);
										return XmlReadMode.ReadSchema;
									}
									if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
									{
										this.ReadXmlSchema(reader, denyResolving);
										this.RestoreConstraint(flag4);
										return XmlReadMode.ReadSchema;
									}
									if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
									{
										if (this.DataSet != null)
										{
											this.DataSet.RestoreEnforceConstraints(flag4);
										}
										else
										{
											this._enforceConstraints = flag4;
										}
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									XmlElement xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
									if (reader.HasAttributes)
									{
										int attributeCount = reader.AttributeCount;
										for (int i = 0; i < attributeCount; i++)
										{
											reader.MoveToAttribute(i);
											if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
											{
												xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
											}
											else
											{
												XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
												xmlAttribute.Prefix = reader.Prefix;
												xmlAttribute.Value = reader.GetAttribute(i);
											}
										}
									}
									reader.Read();
									while (this.MoveToElement(reader, depth))
									{
										if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
										{
											this.ReadXmlDiffgram(reader);
											this.ReadEndElement(reader);
											this.RestoreConstraint(flag4);
											return XmlReadMode.DiffGram;
										}
										if (!flag2 && !flag && reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
										{
											this.ReadXDRSchema(reader);
											flag2 = true;
											flag3 = true;
										}
										else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
										{
											this.ReadXmlSchema(reader, denyResolving);
											flag2 = true;
										}
										else
										{
											if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
											{
												if (this.DataSet != null)
												{
													this.DataSet.RestoreEnforceConstraints(flag4);
												}
												else
												{
													this._enforceConstraints = flag4;
												}
												throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
											}
											if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
											{
												this.ReadXmlDiffgram(reader);
												xmlReadMode = XmlReadMode.DiffGram;
											}
											else
											{
												flag = true;
												if (!flag2 && this.Columns.Count == 0)
												{
													XmlNode xmlNode = xmlDocument.ReadNode(reader);
													xmlElement.AppendChild(xmlNode);
												}
												else
												{
													if (xmlDataLoader == null)
													{
														xmlDataLoader = new XmlDataLoader(this, flag3, xmlElement, false);
													}
													xmlDataLoader.LoadData(reader);
													xmlReadMode = (flag2 ? XmlReadMode.ReadSchema : XmlReadMode.IgnoreSchema);
												}
											}
										}
									}
									this.ReadEndElement(reader);
									xmlDocument.AppendChild(xmlElement);
									if (!flag2 && this.Columns.Count == 0)
									{
										if (this.IsEmptyXml(reader))
										{
											reader.Read();
											return xmlReadMode;
										}
										throw ExceptionBuilder.DataTableInferenceNotSupported();
									}
									else if (xmlDataLoader == null)
									{
										xmlDataLoader = new XmlDataLoader(this, flag3, false);
									}
								}
							}
							this.RestoreConstraint(flag4);
							xmlReadMode2 = xmlReadMode;
						}
					}
				}
				finally
				{
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return xmlReadMode2;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00025F70 File Offset: 0x00024170
		internal XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode, bool denyResolving)
		{
			DataTable.RowDiffIdUsageSection rowDiffIdUsageSection = default(DataTable.RowDiffIdUsageSection);
			XmlReadMode xmlReadMode2;
			try
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				int num = -1;
				XmlReadMode xmlReadMode = mode;
				rowDiffIdUsageSection.Prepare(this);
				if (reader == null)
				{
					xmlReadMode2 = xmlReadMode;
				}
				else
				{
					bool flag4;
					if (this.DataSet != null)
					{
						flag4 = this.DataSet.EnforceConstraints;
						this.DataSet.EnforceConstraints = false;
					}
					else
					{
						flag4 = this.EnforceConstraints;
						this.EnforceConstraints = false;
					}
					if (reader is XmlTextReader)
					{
						((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
					}
					XmlDocument xmlDocument = new XmlDocument();
					if (mode != XmlReadMode.Fragment && reader.NodeType == XmlNodeType.Element)
					{
						num = reader.Depth;
					}
					reader.MoveToContent();
					if (this.Columns.Count == 0 && this.IsEmptyXml(reader))
					{
						reader.Read();
						xmlReadMode2 = xmlReadMode;
					}
					else
					{
						XmlDataLoader xmlDataLoader = null;
						if (reader.NodeType == XmlNodeType.Element)
						{
							XmlElement xmlElement;
							if (mode == XmlReadMode.Fragment)
							{
								xmlDocument.AppendChild(xmlDocument.CreateElement("ds_sqlXmlWraPPeR"));
								xmlElement = xmlDocument.DocumentElement;
							}
							else
							{
								if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (mode == XmlReadMode.DiffGram || mode == XmlReadMode.IgnoreSchema)
									{
										if (this.Columns.Count == 0)
										{
											if (reader.IsEmptyElement)
											{
												reader.Read();
												return XmlReadMode.DiffGram;
											}
											throw ExceptionBuilder.DataTableInferenceNotSupported();
										}
										else
										{
											this.ReadXmlDiffgram(reader);
											this.ReadEndElement(reader);
										}
									}
									else
									{
										reader.Skip();
									}
									this.RestoreConstraint(flag4);
									return xmlReadMode;
								}
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema)
									{
										this.ReadXDRSchema(reader);
									}
									else
									{
										reader.Skip();
									}
									this.RestoreConstraint(flag4);
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema)
									{
										this.ReadXmlSchema(reader, denyResolving);
									}
									else
									{
										reader.Skip();
									}
									this.RestoreConstraint(flag4);
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
								{
									if (this.DataSet != null)
									{
										this.DataSet.RestoreEnforceConstraints(flag4);
									}
									else
									{
										this._enforceConstraints = flag4;
									}
									throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
								}
								xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
								if (reader.HasAttributes)
								{
									int attributeCount = reader.AttributeCount;
									for (int i = 0; i < attributeCount; i++)
									{
										reader.MoveToAttribute(i);
										if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
										{
											xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
										}
										else
										{
											XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
											xmlAttribute.Prefix = reader.Prefix;
											xmlAttribute.Value = reader.GetAttribute(i);
										}
									}
								}
								reader.Read();
							}
							while (this.MoveToElement(reader, num))
							{
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (!flag && !flag2 && mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema)
									{
										this.ReadXDRSchema(reader);
										flag = true;
										flag3 = true;
									}
									else
									{
										reader.Skip();
									}
								}
								else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema)
									{
										this.ReadXmlSchema(reader, denyResolving);
										flag = true;
									}
									else
									{
										reader.Skip();
									}
								}
								else if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (mode == XmlReadMode.DiffGram || mode == XmlReadMode.IgnoreSchema)
									{
										if (this.Columns.Count == 0)
										{
											if (reader.IsEmptyElement)
											{
												reader.Read();
												return XmlReadMode.DiffGram;
											}
											throw ExceptionBuilder.DataTableInferenceNotSupported();
										}
										else
										{
											this.ReadXmlDiffgram(reader);
											xmlReadMode = XmlReadMode.DiffGram;
										}
									}
									else
									{
										reader.Skip();
									}
								}
								else
								{
									if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
									{
										if (this.DataSet != null)
										{
											this.DataSet.RestoreEnforceConstraints(flag4);
										}
										else
										{
											this._enforceConstraints = flag4;
										}
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									if (mode == XmlReadMode.DiffGram)
									{
										reader.Skip();
									}
									else
									{
										flag2 = true;
										if (mode == XmlReadMode.InferSchema)
										{
											XmlNode xmlNode = xmlDocument.ReadNode(reader);
											xmlElement.AppendChild(xmlNode);
										}
										else
										{
											if (this.Columns.Count == 0)
											{
												throw ExceptionBuilder.DataTableInferenceNotSupported();
											}
											if (xmlDataLoader == null)
											{
												xmlDataLoader = new XmlDataLoader(this, flag3, xmlElement, mode == XmlReadMode.IgnoreSchema);
											}
											xmlDataLoader.LoadData(reader);
										}
									}
								}
							}
							this.ReadEndElement(reader);
							xmlDocument.AppendChild(xmlElement);
							if (xmlDataLoader == null)
							{
								xmlDataLoader = new XmlDataLoader(this, flag3, mode == XmlReadMode.IgnoreSchema);
							}
							if (mode == XmlReadMode.DiffGram)
							{
								this.RestoreConstraint(flag4);
								return xmlReadMode;
							}
							if (mode == XmlReadMode.InferSchema && this.Columns.Count == 0)
							{
								throw ExceptionBuilder.DataTableInferenceNotSupported();
							}
						}
						this.RestoreConstraint(flag4);
						xmlReadMode2 = xmlReadMode;
					}
				}
			}
			finally
			{
			}
			return xmlReadMode2;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001C55A File Offset: 0x0001A75A
		internal void ReadEndElement(XmlReader reader)
		{
			while (reader.NodeType == XmlNodeType.Whitespace)
			{
				reader.Skip();
			}
			if (reader.NodeType == XmlNodeType.None)
			{
				reader.Skip();
				return;
			}
			if (reader.NodeType == XmlNodeType.EndElement)
			{
				reader.ReadEndElement();
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000264A8 File Offset: 0x000246A8
		internal void ReadXDRSchema(XmlReader reader)
		{
			new XmlDocument().ReadNode(reader);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001C4FC File Offset: 0x0001A6FC
		internal bool MoveToElement(XmlReader reader, int depth)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element && reader.Depth > depth)
			{
				reader.Read();
			}
			return reader.NodeType == XmlNodeType.Element;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000264B8 File Offset: 0x000246B8
		private void ReadXmlDiffgram(XmlReader reader)
		{
			int depth = reader.Depth;
			bool enforceConstraints = this.EnforceConstraints;
			this.EnforceConstraints = false;
			bool flag;
			DataTable dataTable;
			if (this.Rows.Count == 0)
			{
				flag = true;
				dataTable = this;
			}
			else
			{
				flag = false;
				dataTable = this.Clone();
				dataTable.EnforceConstraints = false;
			}
			dataTable.Rows._nullInList = 0;
			reader.MoveToContent();
			if (reader.LocalName != "diffgram" && reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1")
			{
				return;
			}
			reader.Read();
			if (reader.NodeType == XmlNodeType.Whitespace)
			{
				this.MoveToElement(reader, reader.Depth - 1);
			}
			dataTable._fInLoadDiffgram = true;
			if (reader.Depth > depth)
			{
				if (reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1" && reader.NamespaceURI != "urn:schemas-microsoft-com:xml-msdata")
				{
					XmlElement xmlElement = new XmlDocument().CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
					reader.Read();
					if (reader.Depth - 1 > depth)
					{
						new XmlDataLoader(dataTable, false, xmlElement, false)
						{
							_isDiffgram = true
						}.LoadData(reader);
					}
					this.ReadEndElement(reader);
				}
				if ((reader.LocalName == "before" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1") || (reader.LocalName == "errors" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1"))
				{
					new XMLDiffLoader().LoadDiffGram(dataTable, reader);
				}
				while (reader.Depth > depth)
				{
					reader.Read();
				}
				this.ReadEndElement(reader);
			}
			if (dataTable.Rows._nullInList > 0)
			{
				throw ExceptionBuilder.RowInsertMissing(dataTable.TableName);
			}
			dataTable._fInLoadDiffgram = false;
			List<DataTable> list = new List<DataTable>();
			list.Add(this);
			this.CreateTableList(this, list);
			for (int i = 0; i < list.Count; i++)
			{
				DataRelation[] nestedParentRelations = list[i].NestedParentRelations;
				foreach (DataRelation dataRelation in nestedParentRelations)
				{
					if (dataRelation != null && dataRelation.ParentTable == list[i])
					{
						foreach (object obj in list[i].Rows)
						{
							DataRow dataRow = (DataRow)obj;
							foreach (DataRelation dataRelation2 in nestedParentRelations)
							{
								dataRow.CheckForLoops(dataRelation2);
							}
						}
					}
				}
			}
			if (!flag)
			{
				this.Merge(dataTable);
			}
			this.EnforceConstraints = enforceConstraints;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00026774 File Offset: 0x00024974
		internal void ReadXSDSchema(XmlReader reader, bool denyResolving)
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			while (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
			{
				XmlSchema xmlSchema = XmlSchema.Read(reader, null);
				xmlSchemaSet.Add(xmlSchema);
				this.ReadEndElement(reader);
			}
			xmlSchemaSet.Compile();
			new XSDSchema().LoadSchema(xmlSchemaSet, this);
		}

		/// <summary>Reads an XML schema into the <see cref="T:System.Data.DataTable" /> using the specified stream.</summary>
		/// <param name="stream">The stream used to read the schema. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600086F RID: 2159 RVA: 0x000267D6 File Offset: 0x000249D6
		public void ReadXmlSchema(Stream stream)
		{
			if (stream == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(stream), false);
		}

		/// <summary>Reads an XML schema into the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> used to read the schema information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000870 RID: 2160 RVA: 0x000267E9 File Offset: 0x000249E9
		public void ReadXmlSchema(TextReader reader)
		{
			if (reader == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(reader), false);
		}

		/// <summary>Reads an XML schema into the <see cref="T:System.Data.DataTable" /> from the specified file.</summary>
		/// <param name="fileName">The name of the file from which to read the schema information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000871 RID: 2161 RVA: 0x000267FC File Offset: 0x000249FC
		public void ReadXmlSchema(string fileName)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			try
			{
				this.ReadXmlSchema(xmlTextReader, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		/// <summary>Reads an XML schema into the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> used to read the schema information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000872 RID: 2162 RVA: 0x00026834 File Offset: 0x00024A34
		public void ReadXmlSchema(XmlReader reader)
		{
			this.ReadXmlSchema(reader, false);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00026840 File Offset: 0x00024A40
		internal void ReadXmlSchema(XmlReader reader, bool denyResolving)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataTable.ReadXmlSchema|INFO> {0}, denyResolving={1}", this.ObjectID, denyResolving);
			try
			{
				DataSet dataSet = new DataSet();
				SerializationFormat remotingFormat = this.RemotingFormat;
				dataSet.ReadXmlSchema(reader, denyResolving);
				string mainTableName = dataSet.MainTableName;
				if (!string.IsNullOrEmpty(this._tableName) || !string.IsNullOrEmpty(mainTableName))
				{
					DataTable dataTable = null;
					if (!string.IsNullOrEmpty(this._tableName))
					{
						if (!string.IsNullOrEmpty(this.Namespace))
						{
							dataTable = dataSet.Tables[this._tableName, this.Namespace];
						}
						else
						{
							int num2 = dataSet.Tables.InternalIndexOf(this._tableName);
							if (num2 > -1)
							{
								dataTable = dataSet.Tables[num2];
							}
						}
					}
					else
					{
						string text = string.Empty;
						int num3 = mainTableName.IndexOf(':');
						if (num3 > -1)
						{
							text = mainTableName.Substring(0, num3);
						}
						string text2 = mainTableName.Substring(num3 + 1, mainTableName.Length - num3 - 1);
						dataTable = dataSet.Tables[text2, text];
					}
					if (dataTable == null)
					{
						string text3 = string.Empty;
						if (!string.IsNullOrEmpty(this._tableName))
						{
							text3 = ((this.Namespace.Length > 0) ? (this.Namespace + ":" + this._tableName) : this._tableName);
						}
						else
						{
							text3 = mainTableName;
						}
						throw ExceptionBuilder.TableNotFound(text3);
					}
					dataTable._remotingFormat = remotingFormat;
					List<DataTable> list = new List<DataTable>();
					list.Add(dataTable);
					this.CreateTableList(dataTable, list);
					List<DataRelation> list2 = new List<DataRelation>();
					this.CreateRelationList(list, list2);
					if (list2.Count == 0)
					{
						if (this.Columns.Count == 0)
						{
							DataTable dataTable2 = dataTable;
							if (dataTable2 != null)
							{
								dataTable2.CloneTo(this, null, false);
							}
							if (this.DataSet == null && this._tableNamespace == null)
							{
								this._tableNamespace = dataTable2.Namespace;
							}
						}
					}
					else
					{
						if (string.IsNullOrEmpty(this.TableName))
						{
							this.TableName = dataTable.TableName;
							if (!string.IsNullOrEmpty(dataTable.Namespace))
							{
								this.Namespace = dataTable.Namespace;
							}
						}
						if (this.DataSet == null)
						{
							DataSet dataSet2 = new DataSet(dataSet.DataSetName);
							dataSet2.SetLocaleValue(dataSet.Locale, dataSet.ShouldSerializeLocale());
							dataSet2.CaseSensitive = dataSet.CaseSensitive;
							dataSet2.Namespace = dataSet.Namespace;
							dataSet2._mainTableName = dataSet._mainTableName;
							dataSet2.RemotingFormat = dataSet.RemotingFormat;
							dataSet2.Tables.Add(this);
						}
						this.CloneHierarchy(dataTable, this.DataSet, null);
						foreach (DataTable dataTable3 in list)
						{
							DataTable dataTable4 = this.DataSet.Tables[dataTable3._tableName, dataTable3.Namespace];
							foreach (object obj in dataSet.Tables[dataTable3._tableName, dataTable3.Namespace].Constraints)
							{
								ForeignKeyConstraint foreignKeyConstraint = ((Constraint)obj) as ForeignKeyConstraint;
								if (foreignKeyConstraint != null && foreignKeyConstraint.Table != foreignKeyConstraint.RelatedTable && list.Contains(foreignKeyConstraint.Table) && list.Contains(foreignKeyConstraint.RelatedTable))
								{
									ForeignKeyConstraint foreignKeyConstraint2 = (ForeignKeyConstraint)foreignKeyConstraint.Clone(dataTable4.DataSet);
									if (!dataTable4.Constraints.Contains(foreignKeyConstraint2.ConstraintName))
									{
										dataTable4.Constraints.Add(foreignKeyConstraint2);
									}
								}
							}
						}
						foreach (DataRelation dataRelation in list2)
						{
							if (!this.DataSet.Relations.Contains(dataRelation.RelationName))
							{
								this.DataSet.Relations.Add(dataRelation.Clone(this.DataSet));
							}
						}
						foreach (DataTable dataTable5 in list)
						{
							foreach (object obj2 in dataTable5.Columns)
							{
								DataColumn dataColumn = (DataColumn)obj2;
								bool flag = false;
								if (dataColumn.Expression.Length != 0)
								{
									DataColumn[] dependency = dataColumn.DataExpression.GetDependency();
									for (int i = 0; i < dependency.Length; i++)
									{
										if (!list.Contains(dependency[i].Table))
										{
											flag = true;
											break;
										}
									}
								}
								if (!flag)
								{
									this.DataSet.Tables[dataTable5.TableName, dataTable5.Namespace].Columns[dataColumn.ColumnName].Expression = dataColumn.Expression;
								}
							}
						}
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00026DE0 File Offset: 0x00024FE0
		private void CreateTableList(DataTable currentTable, List<DataTable> tableList)
		{
			foreach (object obj in currentTable.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (!tableList.Contains(dataRelation.ChildTable))
				{
					tableList.Add(dataRelation.ChildTable);
					this.CreateTableList(dataRelation.ChildTable, tableList);
				}
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00026E5C File Offset: 0x0002505C
		private void CreateRelationList(List<DataTable> tableList, List<DataRelation> relationList)
		{
			foreach (DataTable dataTable in tableList)
			{
				foreach (object obj in dataTable.ChildRelations)
				{
					DataRelation dataRelation = (DataRelation)obj;
					if (tableList.Contains(dataRelation.ChildTable) && tableList.Contains(dataRelation.ParentTable))
					{
						relationList.Add(dataRelation);
					}
				}
			}
		}

		/// <summary>This method returns an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> instance containing the Web Services Description Language (WSDL) that describes the <see cref="T:System.Data.DataTable" /> for Web Services.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> instance.</returns>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> instance.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000876 RID: 2166 RVA: 0x00026F08 File Offset: 0x00025108
		public static XmlSchemaComplexType GetDataTableSchema(XmlSchemaSet schemaSet)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.MaxOccurs = decimal.MaxValue;
			xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
			xmlSchemaAny.MinOccurs = 1m;
			xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			return xmlSchemaComplexType;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.GetSchema" />.</summary>
		/// <returns> An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.</returns>
		// Token: 0x06000877 RID: 2167 RVA: 0x00026F97 File Offset: 0x00025197
		XmlSchema IXmlSerializable.GetSchema()
		{
			return this.GetSchema();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.GetSchema" />.</summary>
		/// <returns> An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.</returns>
		// Token: 0x06000878 RID: 2168 RVA: 0x00026FA0 File Offset: 0x000251A0
		protected virtual XmlSchema GetSchema()
		{
			if (base.GetType() == typeof(DataTable))
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = new XmlTextWriter(memoryStream, null);
			if (xmlWriter != null)
			{
				new XmlTreeGen(SchemaFormat.WebService).Save(this, xmlWriter);
			}
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" />.</summary>
		/// <param name="reader">An XmlReader.</param>
		// Token: 0x06000879 RID: 2169 RVA: 0x00026FF8 File Offset: 0x000251F8
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			IXmlTextParser xmlTextParser = reader as IXmlTextParser;
			bool flag = true;
			if (xmlTextParser != null)
			{
				flag = xmlTextParser.Normalized;
				xmlTextParser.Normalized = false;
			}
			this.ReadXmlSerializable(reader);
			if (xmlTextParser != null)
			{
				xmlTextParser.Normalized = flag;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" />.</summary>
		/// <param name="writer">An XmlWriter.</param>
		// Token: 0x0600087A RID: 2170 RVA: 0x00027030 File Offset: 0x00025230
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, false);
			this.WriteXml(writer, XmlWriteMode.DiffGram, false);
		}

		/// <summary>Reads from an XML stream.</summary>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" /> object.</param>
		// Token: 0x0600087B RID: 2171 RVA: 0x00027043 File Offset: 0x00025243
		protected virtual void ReadXmlSerializable(XmlReader reader)
		{
			this.ReadXml(reader, XmlReadMode.DiffGram, true);
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0002704F File Offset: 0x0002524F
		internal Hashtable RowDiffId
		{
			get
			{
				if (this._rowDiffId == null)
				{
					this._rowDiffId = new Hashtable();
				}
				return this._rowDiffId;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0002706A File Offset: 0x0002526A
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00027072 File Offset: 0x00025272
		internal void AddDependentColumn(DataColumn expressionColumn)
		{
			if (this._dependentColumns == null)
			{
				this._dependentColumns = new List<DataColumn>();
			}
			if (!this._dependentColumns.Contains(expressionColumn))
			{
				this._dependentColumns.Add(expressionColumn);
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000270A1 File Offset: 0x000252A1
		internal void RemoveDependentColumn(DataColumn expressionColumn)
		{
			if (this._dependentColumns != null && this._dependentColumns.Contains(expressionColumn))
			{
				this._dependentColumns.Remove(expressionColumn);
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000270C8 File Offset: 0x000252C8
		internal void EvaluateExpressions()
		{
			if (this._dependentColumns != null && 0 < this._dependentColumns.Count)
			{
				foreach (object obj in this.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow._oldRecord != -1 && dataRow._oldRecord != dataRow._newRecord)
					{
						this.EvaluateDependentExpressions(this._dependentColumns, dataRow, DataRowVersion.Original, null);
					}
					if (dataRow._newRecord != -1)
					{
						this.EvaluateDependentExpressions(this._dependentColumns, dataRow, DataRowVersion.Current, null);
					}
					if (dataRow._tempRecord != -1)
					{
						this.EvaluateDependentExpressions(this._dependentColumns, dataRow, DataRowVersion.Proposed, null);
					}
				}
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00027198 File Offset: 0x00025398
		internal void EvaluateExpressions(DataRow row, DataRowAction action, List<DataRow> cachedRows)
		{
			if (action == DataRowAction.Add || action == DataRowAction.Change || (action == DataRowAction.Rollback && (row._oldRecord != -1 || row._newRecord != -1)))
			{
				if (row._oldRecord != -1 && row._oldRecord != row._newRecord)
				{
					this.EvaluateDependentExpressions(this._dependentColumns, row, DataRowVersion.Original, cachedRows);
				}
				if (row._newRecord != -1)
				{
					this.EvaluateDependentExpressions(this._dependentColumns, row, DataRowVersion.Current, cachedRows);
				}
				if (row._tempRecord != -1)
				{
					this.EvaluateDependentExpressions(this._dependentColumns, row, DataRowVersion.Proposed, cachedRows);
				}
				return;
			}
			if ((action == DataRowAction.Delete || (action == DataRowAction.Rollback && row._oldRecord == -1 && row._newRecord == -1)) && this._dependentColumns != null)
			{
				foreach (DataColumn dataColumn in this._dependentColumns)
				{
					if (dataColumn.DataExpression != null && dataColumn.DataExpression.HasLocalAggregate() && dataColumn.Table == this)
					{
						for (int i = 0; i < this.Rows.Count; i++)
						{
							DataRow dataRow = this.Rows[i];
							if (dataRow._oldRecord != -1 && dataRow._oldRecord != dataRow._newRecord)
							{
								this.EvaluateDependentExpressions(this._dependentColumns, dataRow, DataRowVersion.Original, null);
							}
						}
						for (int j = 0; j < this.Rows.Count; j++)
						{
							DataRow dataRow2 = this.Rows[j];
							if (dataRow2._tempRecord != -1)
							{
								this.EvaluateDependentExpressions(this._dependentColumns, dataRow2, DataRowVersion.Proposed, null);
							}
						}
						for (int k = 0; k < this.Rows.Count; k++)
						{
							DataRow dataRow3 = this.Rows[k];
							if (dataRow3._newRecord != -1)
							{
								this.EvaluateDependentExpressions(this._dependentColumns, dataRow3, DataRowVersion.Current, null);
							}
						}
						break;
					}
				}
				if (cachedRows != null)
				{
					foreach (DataRow dataRow4 in cachedRows)
					{
						if (dataRow4._oldRecord != -1 && dataRow4._oldRecord != dataRow4._newRecord)
						{
							dataRow4.Table.EvaluateDependentExpressions(dataRow4.Table._dependentColumns, dataRow4, DataRowVersion.Original, null);
						}
						if (dataRow4._newRecord != -1)
						{
							dataRow4.Table.EvaluateDependentExpressions(dataRow4.Table._dependentColumns, dataRow4, DataRowVersion.Current, null);
						}
						if (dataRow4._tempRecord != -1)
						{
							dataRow4.Table.EvaluateDependentExpressions(dataRow4.Table._dependentColumns, dataRow4, DataRowVersion.Proposed, null);
						}
					}
				}
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00027488 File Offset: 0x00025688
		internal void EvaluateExpressions(DataColumn column)
		{
			int count = column._table.Rows.Count;
			if (column.DataExpression.IsTableAggregate() && count > 0)
			{
				object obj = column.DataExpression.Evaluate();
				for (int i = 0; i < count; i++)
				{
					DataRow dataRow = column._table.Rows[i];
					if (dataRow._oldRecord != -1 && dataRow._oldRecord != dataRow._newRecord)
					{
						column[dataRow._oldRecord] = obj;
					}
					if (dataRow._newRecord != -1)
					{
						column[dataRow._newRecord] = obj;
					}
					if (dataRow._tempRecord != -1)
					{
						column[dataRow._tempRecord] = obj;
					}
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					DataRow dataRow2 = column._table.Rows[j];
					if (dataRow2._oldRecord != -1 && dataRow2._oldRecord != dataRow2._newRecord)
					{
						column[dataRow2._oldRecord] = column.DataExpression.Evaluate(dataRow2, DataRowVersion.Original);
					}
					if (dataRow2._newRecord != -1)
					{
						column[dataRow2._newRecord] = column.DataExpression.Evaluate(dataRow2, DataRowVersion.Current);
					}
					if (dataRow2._tempRecord != -1)
					{
						column[dataRow2._tempRecord] = column.DataExpression.Evaluate(dataRow2, DataRowVersion.Proposed);
					}
				}
			}
			column.Table.ResetInternalIndexes(column);
			this.EvaluateDependentExpressions(column);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00027604 File Offset: 0x00025804
		internal void EvaluateDependentExpressions(DataColumn column)
		{
			if (column._dependentColumns != null)
			{
				foreach (DataColumn dataColumn in column._dependentColumns)
				{
					if (dataColumn._table != null && column != dataColumn)
					{
						this.EvaluateExpressions(dataColumn);
					}
				}
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0002766C File Offset: 0x0002586C
		internal void EvaluateDependentExpressions(List<DataColumn> columns, DataRow row, DataRowVersion version, List<DataRow> cachedRows)
		{
			if (columns == null)
			{
				return;
			}
			int num = columns.Count;
			for (int i = 0; i < num; i++)
			{
				if (columns[i].Table == this)
				{
					DataColumn dataColumn = columns[i];
					if (dataColumn.DataExpression != null && dataColumn.DataExpression.HasLocalAggregate())
					{
						DataRowVersion dataRowVersion = ((version == DataRowVersion.Proposed) ? DataRowVersion.Default : version);
						bool flag = dataColumn.DataExpression.IsTableAggregate();
						object obj = null;
						if (flag)
						{
							obj = dataColumn.DataExpression.Evaluate(row, dataRowVersion);
						}
						for (int j = 0; j < this.Rows.Count; j++)
						{
							DataRow dataRow = this.Rows[j];
							if (dataRow.RowState != DataRowState.Deleted && (dataRowVersion != DataRowVersion.Original || (dataRow._oldRecord != -1 && dataRow._oldRecord != dataRow._newRecord)))
							{
								if (!flag)
								{
									obj = dataColumn.DataExpression.Evaluate(dataRow, dataRowVersion);
								}
								this.SilentlySetValue(dataRow, dataColumn, dataRowVersion, obj);
							}
						}
					}
					else if (row.RowState != DataRowState.Deleted && (version != DataRowVersion.Original || (row._oldRecord != -1 && row._oldRecord != row._newRecord)))
					{
						this.SilentlySetValue(row, dataColumn, version, (dataColumn.DataExpression == null) ? dataColumn.DefaultValue : dataColumn.DataExpression.Evaluate(row, version));
					}
				}
			}
			num = columns.Count;
			for (int k = 0; k < num; k++)
			{
				DataColumn dataColumn2 = columns[k];
				if (dataColumn2.Table != this || (dataColumn2.DataExpression != null && !dataColumn2.DataExpression.HasLocalAggregate()))
				{
					DataRowVersion dataRowVersion2 = ((version == DataRowVersion.Proposed) ? DataRowVersion.Default : version);
					if (cachedRows != null)
					{
						foreach (DataRow dataRow2 in cachedRows)
						{
							if (dataRow2.Table == dataColumn2.Table && (dataRowVersion2 != DataRowVersion.Original || dataRow2._newRecord != dataRow2._oldRecord) && dataRow2 != null && dataRow2.RowState != DataRowState.Deleted && (version != DataRowVersion.Original || dataRow2._oldRecord != -1))
							{
								object obj2 = dataColumn2.DataExpression.Evaluate(dataRow2, dataRowVersion2);
								this.SilentlySetValue(dataRow2, dataColumn2, dataRowVersion2, obj2);
							}
						}
					}
					for (int l = 0; l < this.ParentRelations.Count; l++)
					{
						DataRelation dataRelation = this.ParentRelations[l];
						if (dataRelation.ParentTable == dataColumn2.Table)
						{
							foreach (DataRow dataRow3 in row.GetParentRows(dataRelation, version))
							{
								if ((cachedRows == null || !cachedRows.Contains(dataRow3)) && (dataRowVersion2 != DataRowVersion.Original || dataRow3._newRecord != dataRow3._oldRecord) && dataRow3 != null && dataRow3.RowState != DataRowState.Deleted && (version != DataRowVersion.Original || dataRow3._oldRecord != -1))
								{
									object obj3 = dataColumn2.DataExpression.Evaluate(dataRow3, dataRowVersion2);
									this.SilentlySetValue(dataRow3, dataColumn2, dataRowVersion2, obj3);
								}
							}
						}
					}
					for (int n = 0; n < this.ChildRelations.Count; n++)
					{
						DataRelation dataRelation2 = this.ChildRelations[n];
						if (dataRelation2.ChildTable == dataColumn2.Table)
						{
							foreach (DataRow dataRow4 in row.GetChildRows(dataRelation2, version))
							{
								if ((cachedRows == null || !cachedRows.Contains(dataRow4)) && (dataRowVersion2 != DataRowVersion.Original || dataRow4._newRecord != dataRow4._oldRecord) && dataRow4 != null && dataRow4.RowState != DataRowState.Deleted && (version != DataRowVersion.Original || dataRow4._oldRecord != -1))
								{
									object obj4 = dataColumn2.DataExpression.Evaluate(dataRow4, dataRowVersion2);
									this.SilentlySetValue(dataRow4, dataColumn2, dataRowVersion2, obj4);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x040005D6 RID: 1494
		private DataSet _dataSet;

		// Token: 0x040005D7 RID: 1495
		private DataView _defaultView;

		// Token: 0x040005D8 RID: 1496
		internal long _nextRowID;

		// Token: 0x040005D9 RID: 1497
		internal readonly DataRowCollection _rowCollection;

		// Token: 0x040005DA RID: 1498
		internal readonly DataColumnCollection _columnCollection;

		// Token: 0x040005DB RID: 1499
		private readonly ConstraintCollection _constraintCollection;

		// Token: 0x040005DC RID: 1500
		private int _elementColumnCount;

		// Token: 0x040005DD RID: 1501
		internal DataRelationCollection _parentRelationsCollection;

		// Token: 0x040005DE RID: 1502
		internal DataRelationCollection _childRelationsCollection;

		// Token: 0x040005DF RID: 1503
		internal readonly RecordManager _recordManager;

		// Token: 0x040005E0 RID: 1504
		internal readonly List<Index> _indexes;

		// Token: 0x040005E1 RID: 1505
		private List<Index> _shadowIndexes;

		// Token: 0x040005E2 RID: 1506
		private int _shadowCount;

		// Token: 0x040005E3 RID: 1507
		internal PropertyCollection _extendedProperties;

		// Token: 0x040005E4 RID: 1508
		private string _tableName = string.Empty;

		// Token: 0x040005E5 RID: 1509
		internal string _tableNamespace;

		// Token: 0x040005E6 RID: 1510
		private string _tablePrefix = string.Empty;

		// Token: 0x040005E7 RID: 1511
		internal DataExpression _displayExpression;

		// Token: 0x040005E8 RID: 1512
		internal bool _fNestedInDataset = true;

		// Token: 0x040005E9 RID: 1513
		private CultureInfo _culture;

		// Token: 0x040005EA RID: 1514
		private bool _cultureUserSet;

		// Token: 0x040005EB RID: 1515
		private CompareInfo _compareInfo;

		// Token: 0x040005EC RID: 1516
		private CompareOptions _compareFlags = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x040005ED RID: 1517
		private IFormatProvider _formatProvider;

		// Token: 0x040005EE RID: 1518
		private StringComparer _hashCodeProvider;

		// Token: 0x040005EF RID: 1519
		private bool _caseSensitive;

		// Token: 0x040005F0 RID: 1520
		private bool _caseSensitiveUserSet;

		// Token: 0x040005F1 RID: 1521
		internal string _encodedTableName;

		// Token: 0x040005F2 RID: 1522
		internal DataColumn _xmlText;

		// Token: 0x040005F3 RID: 1523
		internal DataColumn _colUnique;

		// Token: 0x040005F4 RID: 1524
		internal bool _textOnly;

		// Token: 0x040005F5 RID: 1525
		internal decimal _minOccurs = 1m;

		// Token: 0x040005F6 RID: 1526
		internal decimal _maxOccurs = 1m;

		// Token: 0x040005F7 RID: 1527
		internal bool _repeatableElement;

		// Token: 0x040005F8 RID: 1528
		private object _typeName;

		// Token: 0x040005F9 RID: 1529
		internal UniqueConstraint _primaryKey;

		// Token: 0x040005FA RID: 1530
		internal IndexField[] _primaryIndex = Array.Empty<IndexField>();

		// Token: 0x040005FB RID: 1531
		private DataColumn[] _delayedSetPrimaryKey;

		// Token: 0x040005FC RID: 1532
		private Index _loadIndex;

		// Token: 0x040005FD RID: 1533
		private Index _loadIndexwithOriginalAdded;

		// Token: 0x040005FE RID: 1534
		private Index _loadIndexwithCurrentDeleted;

		// Token: 0x040005FF RID: 1535
		private int _suspendIndexEvents;

		// Token: 0x04000600 RID: 1536
		private bool _savedEnforceConstraints;

		// Token: 0x04000601 RID: 1537
		private bool _inDataLoad;

		// Token: 0x04000602 RID: 1538
		private bool _initialLoad;

		// Token: 0x04000603 RID: 1539
		private bool _schemaLoading;

		// Token: 0x04000604 RID: 1540
		private bool _enforceConstraints = true;

		// Token: 0x04000605 RID: 1541
		internal bool _suspendEnforceConstraints;

		/// <summary>Checks whether initialization is in progress. The initialization occurs at run time.</summary>
		// Token: 0x04000606 RID: 1542
		protected internal bool fInitInProgress;

		// Token: 0x04000607 RID: 1543
		private bool _inLoad;

		// Token: 0x04000608 RID: 1544
		internal bool _fInLoadDiffgram;

		// Token: 0x04000609 RID: 1545
		private byte _isTypedDataTable;

		// Token: 0x0400060A RID: 1546
		private DataRow[] _emptyDataRowArray;

		// Token: 0x0400060B RID: 1547
		private PropertyDescriptorCollection _propertyDescriptorCollectionCache;

		// Token: 0x0400060C RID: 1548
		private DataRelation[] _nestedParentRelations = Array.Empty<DataRelation>();

		// Token: 0x0400060D RID: 1549
		internal List<DataColumn> _dependentColumns;

		// Token: 0x0400060E RID: 1550
		private bool _mergingData;

		// Token: 0x0400060F RID: 1551
		private DataRowChangeEventHandler _onRowChangedDelegate;

		// Token: 0x04000610 RID: 1552
		private DataRowChangeEventHandler _onRowChangingDelegate;

		// Token: 0x04000611 RID: 1553
		private DataRowChangeEventHandler _onRowDeletingDelegate;

		// Token: 0x04000612 RID: 1554
		private DataRowChangeEventHandler _onRowDeletedDelegate;

		// Token: 0x04000613 RID: 1555
		private DataColumnChangeEventHandler _onColumnChangedDelegate;

		// Token: 0x04000614 RID: 1556
		private DataColumnChangeEventHandler _onColumnChangingDelegate;

		// Token: 0x04000615 RID: 1557
		private DataTableClearEventHandler _onTableClearingDelegate;

		// Token: 0x04000616 RID: 1558
		private DataTableClearEventHandler _onTableClearedDelegate;

		// Token: 0x04000617 RID: 1559
		private DataTableNewRowEventHandler _onTableNewRowDelegate;

		// Token: 0x04000618 RID: 1560
		private PropertyChangedEventHandler _onPropertyChangingDelegate;

		// Token: 0x04000619 RID: 1561
		private EventHandler _onInitialized;

		// Token: 0x0400061A RID: 1562
		private readonly DataRowBuilder _rowBuilder;

		// Token: 0x0400061B RID: 1563
		private const string KEY_XMLSCHEMA = "XmlSchema";

		// Token: 0x0400061C RID: 1564
		private const string KEY_XMLDIFFGRAM = "XmlDiffGram";

		// Token: 0x0400061D RID: 1565
		private const string KEY_NAME = "TableName";

		// Token: 0x0400061E RID: 1566
		internal readonly List<DataView> _delayedViews = new List<DataView>();

		// Token: 0x0400061F RID: 1567
		private readonly List<DataViewListener> _dataViewListeners = new List<DataViewListener>();

		// Token: 0x04000620 RID: 1568
		internal Hashtable _rowDiffId;

		// Token: 0x04000621 RID: 1569
		internal readonly ReaderWriterLockSlim _indexesLock = new ReaderWriterLockSlim();

		// Token: 0x04000622 RID: 1570
		internal int _ukColumnPositionForInference = -1;

		// Token: 0x04000623 RID: 1571
		private SerializationFormat _remotingFormat;

		// Token: 0x04000624 RID: 1572
		private static int s_objectTypeCount;

		// Token: 0x04000625 RID: 1573
		private readonly int _objectID = Interlocked.Increment(ref DataTable.s_objectTypeCount);

		// Token: 0x0200008D RID: 141
		internal struct RowDiffIdUsageSection
		{
			// Token: 0x06000885 RID: 2181 RVA: 0x00027A70 File Offset: 0x00025C70
			internal void Prepare(DataTable table)
			{
				this._targetTable = table;
				table._rowDiffId = null;
			}

			// Token: 0x06000886 RID: 2182 RVA: 0x00027A80 File Offset: 0x00025C80
			[Conditional("DEBUG")]
			internal void Cleanup()
			{
				if (this._targetTable != null)
				{
					this._targetTable._rowDiffId = null;
				}
			}

			// Token: 0x06000887 RID: 2183 RVA: 0x00005E03 File Offset: 0x00004003
			[Conditional("DEBUG")]
			internal static void Assert(string message)
			{
			}

			// Token: 0x04000626 RID: 1574
			private DataTable _targetTable;
		}

		// Token: 0x0200008E RID: 142
		internal struct DSRowDiffIdUsageSection
		{
			// Token: 0x06000888 RID: 2184 RVA: 0x00027A98 File Offset: 0x00025C98
			internal void Prepare(DataSet ds)
			{
				this._targetDS = ds;
				for (int i = 0; i < ds.Tables.Count; i++)
				{
					ds.Tables[i]._rowDiffId = null;
				}
			}

			// Token: 0x06000889 RID: 2185 RVA: 0x00027AD4 File Offset: 0x00025CD4
			[Conditional("DEBUG")]
			internal void Cleanup()
			{
				if (this._targetDS != null)
				{
					for (int i = 0; i < this._targetDS.Tables.Count; i++)
					{
						this._targetDS.Tables[i]._rowDiffId = null;
					}
				}
			}

			// Token: 0x04000627 RID: 1575
			private DataSet _targetDS;
		}
	}
}
