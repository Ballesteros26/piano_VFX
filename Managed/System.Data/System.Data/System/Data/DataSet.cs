using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data
{
	/// <summary>Represents an in-memory cache of data.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000088 RID: 136
	[XmlSchemaProvider("GetDataSetSchema")]
	[DefaultProperty("DataSetName")]
	[XmlRoot("DataSet")]
	[Serializable]
	public class DataSet : MarshalByValueComponent, IListSource, IXmlSerializable, ISupportInitializeNotification, ISupportInitialize, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataSet" /> class.</summary>
		// Token: 0x06000699 RID: 1689 RVA: 0x0001A06C File Offset: 0x0001826C
		public DataSet()
		{
			GC.SuppressFinalize(this);
			DataCommonEventSource.Log.Trace<int>("<ds.DataSet.DataSet|API> {0}", this.ObjectID);
			this._tableCollection = new DataTableCollection(this);
			this._relationCollection = new DataRelationCollection.DataSetRelationCollection(this);
			this._culture = CultureInfo.CurrentCulture;
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.DataSet" /> class with the given name.</summary>
		/// <param name="dataSetName">The name of the <see cref="T:System.Data.DataSet" />.</param>
		// Token: 0x0600069A RID: 1690 RVA: 0x0001A112 File Offset: 0x00018312
		public DataSet(string dataSetName)
			: this()
		{
			this.DataSetName = dataSetName;
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.SerializationFormat" /> for the <see cref="T:System.Data.DataSet" /> used during remoting.</summary>
		/// <returns>A <see cref="T:System.Data.SerializationFormat" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001A121 File Offset: 0x00018321
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0001A12C File Offset: 0x0001832C
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
				this._remotingFormat = value;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].RemotingFormat = value;
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>Gets or sets a <see cref="T:System.Data.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0000EF2B File Offset: 0x0000D12B
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x0001A176 File Offset: 0x00018376
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual SchemaSerializationMode SchemaSerializationMode
		{
			get
			{
				return SchemaSerializationMode.IncludeSchema;
			}
			set
			{
				if (value != SchemaSerializationMode.IncludeSchema)
				{
					throw ExceptionBuilder.CannotChangeSchemaSerializationMode();
				}
			}
		}

		/// <summary>Inspects the format of the serialized representation of the DataSet.</summary>
		/// <returns>true if the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> represents a DataSet serialized in its binary format, false otherwise.</returns>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x0600069F RID: 1695 RVA: 0x0001A184 File Offset: 0x00018384
		protected bool IsBinarySerialized(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "DataSet.RemotingFormat")
				{
					serializationFormat = (SerializationFormat)enumerator.Value;
					break;
				}
			}
			return serializationFormat == SerializationFormat.Binary;
		}

		/// <summary>Determines the <see cref="P:System.Data.DataSet.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>An <see cref="T:System.Data.SchemaSerializationMode" /> enumeration indicating whether schema information has been omitted from the payload.</returns>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that a DataSet’s protected constructor <see cref="M:System.Data.DataSet.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> is invoked with during deserialization in remoting scenarios. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that a DataSet’s protected constructor <see cref="M:System.Data.DataSet.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> is invoked with during deserialization in remoting scenarios.</param>
		// Token: 0x060006A0 RID: 1696 RVA: 0x0001A1C8 File Offset: 0x000183C8
		protected SchemaSerializationMode DetermineSchemaSerializationMode(SerializationInfo info, StreamingContext context)
		{
			SchemaSerializationMode schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "SchemaSerializationMode.DataSet")
				{
					schemaSerializationMode = (SchemaSerializationMode)enumerator.Value;
					break;
				}
			}
			return schemaSerializationMode;
		}

		/// <summary>Determines the <see cref="P:System.Data.DataSet.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>An <see cref="T:System.Data.SchemaSerializationMode" /> enumeration indicating whether schema information has been omitted from the payload.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> instance that is passed during deserialization of the <see cref="T:System.Data.DataSet" />.</param>
		// Token: 0x060006A1 RID: 1697 RVA: 0x0001A20C File Offset: 0x0001840C
		protected SchemaSerializationMode DetermineSchemaSerializationMode(XmlReader reader)
		{
			SchemaSerializationMode schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			reader.MoveToContent();
			if (reader.NodeType == XmlNodeType.Element && reader.HasAttributes)
			{
				string attribute = reader.GetAttribute("SchemaSerializationMode", "urn:schemas-microsoft-com:xml-msdata");
				if (string.Equals(attribute, "ExcludeSchema", StringComparison.OrdinalIgnoreCase))
				{
					schemaSerializationMode = SchemaSerializationMode.ExcludeSchema;
				}
				else if (string.Equals(attribute, "IncludeSchema", StringComparison.OrdinalIgnoreCase))
				{
					schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
				}
				else if (attribute != null)
				{
					throw ExceptionBuilder.InvalidSchemaSerializationMode(typeof(SchemaSerializationMode), attribute);
				}
			}
			return schemaSerializationMode;
		}

		/// <summary>Deserializes the table data from the binary or XML stream.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance. </param>
		/// <param name="context">The streaming context. </param>
		// Token: 0x060006A2 RID: 1698 RVA: 0x0001A280 File Offset: 0x00018480
		protected void GetSerializationData(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "DataSet.RemotingFormat")
				{
					serializationFormat = (SerializationFormat)enumerator.Value;
					break;
				}
			}
			this.DeserializeDataSetData(info, context, serializationFormat);
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.DataSet" /> class that has the given serialization information and context.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object. </param>
		/// <param name="context">The source and destination of a given serialized stream.</param>
		// Token: 0x060006A3 RID: 1699 RVA: 0x0001A2C9 File Offset: 0x000184C9
		protected DataSet(SerializationInfo info, StreamingContext context)
			: this(info, context, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataSet" /> class.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		/// <param name="ConstructSchema">The boolean value.</param>
		// Token: 0x060006A4 RID: 1700 RVA: 0x0001A2D4 File Offset: 0x000184D4
		protected DataSet(SerializationInfo info, StreamingContext context, bool ConstructSchema)
			: this()
		{
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			SchemaSerializationMode schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "DataSet.RemotingFormat"))
				{
					if (name == "SchemaSerializationMode.DataSet")
					{
						schemaSerializationMode = (SchemaSerializationMode)enumerator.Value;
					}
				}
				else
				{
					serializationFormat = (SerializationFormat)enumerator.Value;
				}
			}
			if (schemaSerializationMode == SchemaSerializationMode.ExcludeSchema)
			{
				this.InitializeDerivedDataSet();
			}
			if (serializationFormat == SerializationFormat.Xml && !ConstructSchema)
			{
				return;
			}
			this.DeserializeDataSet(info, context, serializationFormat, schemaSerializationMode);
		}

		/// <summary>Populates a serialization information object with the data needed to serialize the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized data associated with the <see cref="T:System.Data.DataSet" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream associated with the <see cref="T:System.Data.DataSet" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
		// Token: 0x060006A5 RID: 1701 RVA: 0x0001A354 File Offset: 0x00018554
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat remotingFormat = this.RemotingFormat;
			this.SerializeDataSet(info, context, remotingFormat);
		}

		/// <summary>Deserialize all of the tables data of the DataSet from the binary or XML stream.</summary>
		// Token: 0x060006A6 RID: 1702 RVA: 0x00005E03 File Offset: 0x00004003
		protected virtual void InitializeDerivedDataSet()
		{
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001A374 File Offset: 0x00018574
		private void SerializeDataSet(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat)
		{
			info.AddValue("DataSet.RemotingVersion", new Version(2, 0));
			if (remotingFormat != SerializationFormat.Xml)
			{
				info.AddValue("DataSet.RemotingFormat", remotingFormat);
			}
			if (SchemaSerializationMode.IncludeSchema != this.SchemaSerializationMode)
			{
				info.AddValue("SchemaSerializationMode.DataSet", this.SchemaSerializationMode);
			}
			if (remotingFormat != SerializationFormat.Xml)
			{
				if (this.SchemaSerializationMode == SchemaSerializationMode.IncludeSchema)
				{
					this.SerializeDataSetProperties(info, context);
					info.AddValue("DataSet.Tables.Count", this.Tables.Count);
					for (int i = 0; i < this.Tables.Count; i++)
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(context.State, false));
						MemoryStream memoryStream = new MemoryStream();
						binaryFormatter.Serialize(memoryStream, this.Tables[i]);
						memoryStream.Position = 0L;
						info.AddValue(string.Format(CultureInfo.InvariantCulture, "DataSet.Tables_{0}", i), memoryStream.GetBuffer());
					}
					for (int j = 0; j < this.Tables.Count; j++)
					{
						this.Tables[j].SerializeConstraints(info, context, j, true);
					}
					this.SerializeRelations(info, context);
					for (int k = 0; k < this.Tables.Count; k++)
					{
						this.Tables[k].SerializeExpressionColumns(info, context, k);
					}
				}
				else
				{
					this.SerializeDataSetProperties(info, context);
				}
				for (int l = 0; l < this.Tables.Count; l++)
				{
					this.Tables[l].SerializeTableData(info, context, l);
				}
				return;
			}
			string xmlSchemaForRemoting = this.GetXmlSchemaForRemoting(null);
			info.AddValue("XmlSchema", xmlSchemaForRemoting);
			StringWriter stringWriter = new StringWriter(new StringBuilder(this.EstimatedXmlStringSize() * 2), CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			this.WriteXml(xmlTextWriter, XmlWriteMode.DiffGram);
			string text = stringWriter.ToString();
			info.AddValue("XmlDiffGram", text);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001A552 File Offset: 0x00018752
		internal void DeserializeDataSet(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat, SchemaSerializationMode schemaSerializationMode)
		{
			this.DeserializeDataSetSchema(info, context, remotingFormat, schemaSerializationMode);
			this.DeserializeDataSetData(info, context, remotingFormat);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001A568 File Offset: 0x00018768
		private void DeserializeDataSetSchema(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat, SchemaSerializationMode schemaSerializationMode)
		{
			if (remotingFormat == SerializationFormat.Xml)
			{
				string text = (string)info.GetValue("XmlSchema", typeof(string));
				if (text != null)
				{
					this.ReadXmlSchema(new XmlTextReader(new StringReader(text)), true);
				}
				return;
			}
			if (schemaSerializationMode == SchemaSerializationMode.IncludeSchema)
			{
				this.DeserializeDataSetProperties(info, context);
				int @int = info.GetInt32("DataSet.Tables.Count");
				for (int i = 0; i < @int; i++)
				{
					MemoryStream memoryStream = new MemoryStream((byte[])info.GetValue(string.Format(CultureInfo.InvariantCulture, "DataSet.Tables_{0}", i), typeof(byte[])));
					memoryStream.Position = 0L;
					DataTable dataTable = (DataTable)new BinaryFormatter(null, new StreamingContext(context.State, false)).Deserialize(memoryStream);
					this.Tables.Add(dataTable);
				}
				for (int j = 0; j < @int; j++)
				{
					this.Tables[j].DeserializeConstraints(info, context, j, true);
				}
				this.DeserializeRelations(info, context);
				for (int k = 0; k < @int; k++)
				{
					this.Tables[k].DeserializeExpressionColumns(info, context, k);
				}
				return;
			}
			this.DeserializeDataSetProperties(info, context);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001A6A0 File Offset: 0x000188A0
		private void DeserializeDataSetData(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat)
		{
			if (remotingFormat != SerializationFormat.Xml)
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].DeserializeTableData(info, context, i);
				}
				return;
			}
			string text = (string)info.GetValue("XmlDiffGram", typeof(string));
			if (text != null)
			{
				this.ReadXml(new XmlTextReader(new StringReader(text)), XmlReadMode.DiffGram);
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001A70C File Offset: 0x0001890C
		private void SerializeDataSetProperties(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("DataSet.DataSetName", this.DataSetName);
			info.AddValue("DataSet.Namespace", this.Namespace);
			info.AddValue("DataSet.Prefix", this.Prefix);
			info.AddValue("DataSet.CaseSensitive", this.CaseSensitive);
			info.AddValue("DataSet.LocaleLCID", this.Locale.LCID);
			info.AddValue("DataSet.EnforceConstraints", this.EnforceConstraints);
			info.AddValue("DataSet.ExtendedProperties", this.ExtendedProperties);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001A798 File Offset: 0x00018998
		private void DeserializeDataSetProperties(SerializationInfo info, StreamingContext context)
		{
			this._dataSetName = info.GetString("DataSet.DataSetName");
			this._namespaceURI = info.GetString("DataSet.Namespace");
			this._datasetPrefix = info.GetString("DataSet.Prefix");
			this._caseSensitive = info.GetBoolean("DataSet.CaseSensitive");
			int num = (int)info.GetValue("DataSet.LocaleLCID", typeof(int));
			this._culture = new CultureInfo(num);
			this._cultureUserSet = true;
			this._enforceConstraints = info.GetBoolean("DataSet.EnforceConstraints");
			this._extendedProperties = (PropertyCollection)info.GetValue("DataSet.ExtendedProperties", typeof(PropertyCollection));
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001A848 File Offset: 0x00018A48
		private void SerializeRelations(SerializationInfo info, StreamingContext context)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.Relations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				int[] array = new int[dataRelation.ParentColumns.Length + 1];
				array[0] = this.Tables.IndexOf(dataRelation.ParentTable);
				for (int i = 1; i < array.Length; i++)
				{
					array[i] = dataRelation.ParentColumns[i - 1].Ordinal;
				}
				int[] array2 = new int[dataRelation.ChildColumns.Length + 1];
				array2[0] = this.Tables.IndexOf(dataRelation.ChildTable);
				for (int j = 1; j < array2.Length; j++)
				{
					array2[j] = dataRelation.ChildColumns[j - 1].Ordinal;
				}
				arrayList.Add(new ArrayList { dataRelation.RelationName, array, array2, dataRelation.Nested, dataRelation._extendedProperties });
			}
			info.AddValue("DataSet.Relations", arrayList);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001A9AC File Offset: 0x00018BAC
		private void DeserializeRelations(SerializationInfo info, StreamingContext context)
		{
			foreach (object obj in ((ArrayList)info.GetValue("DataSet.Relations", typeof(ArrayList))))
			{
				ArrayList arrayList = (ArrayList)obj;
				string text = (string)arrayList[0];
				int[] array = (int[])arrayList[1];
				int[] array2 = (int[])arrayList[2];
				bool flag = (bool)arrayList[3];
				PropertyCollection propertyCollection = (PropertyCollection)arrayList[4];
				DataColumn[] array3 = new DataColumn[array.Length - 1];
				for (int i = 0; i < array3.Length; i++)
				{
					array3[i] = this.Tables[array[0]].Columns[array[i + 1]];
				}
				DataColumn[] array4 = new DataColumn[array2.Length - 1];
				for (int j = 0; j < array4.Length; j++)
				{
					array4[j] = this.Tables[array2[0]].Columns[array2[j + 1]];
				}
				DataRelation dataRelation = new DataRelation(text, array3, array4, false);
				dataRelation.CheckMultipleNested = false;
				dataRelation.Nested = flag;
				dataRelation._extendedProperties = propertyCollection;
				this.Relations.Add(dataRelation);
				dataRelation.CheckMultipleNested = true;
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001AB28 File Offset: 0x00018D28
		internal void FailedEnableConstraints()
		{
			this.EnforceConstraints = false;
			throw ExceptionBuilder.EnforceConstraint();
		}

		/// <summary>Gets or sets a value indicating whether string comparisons within <see cref="T:System.Data.DataTable" /> objects are case-sensitive.</summary>
		/// <returns>true if string comparisons are case-sensitive; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001AB36 File Offset: 0x00018D36
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x0001AB40 File Offset: 0x00018D40
		[DefaultValue(false)]
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
					this._caseSensitive = value;
					if (!this.ValidateCaseConstraint())
					{
						this._caseSensitive = caseSensitive;
						throw ExceptionBuilder.CannotChangeCaseLocale();
					}
					foreach (object obj in this.Tables)
					{
						((DataTable)obj).SetCaseSensitiveValue(value, false, true);
					}
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IListSource.ContainsListCollection" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IListSource.ContainsListCollection" />.</returns>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0000EF2B File Offset: 0x0000D12B
		bool IListSource.ContainsListCollection
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a custom view of the data contained in the <see cref="T:System.Data.DataSet" /> to allow filtering, searching, and navigating using a custom <see cref="T:System.Data.DataViewManager" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewManager" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001ABC8 File Offset: 0x00018DC8
		[Browsable(false)]
		public DataViewManager DefaultViewManager
		{
			get
			{
				if (this._defaultViewManager == null)
				{
					object defaultViewManagerLock = this._defaultViewManagerLock;
					lock (defaultViewManagerLock)
					{
						if (this._defaultViewManager == null)
						{
							this._defaultViewManager = new DataViewManager(this, true);
						}
					}
				}
				return this._defaultViewManager;
			}
		}

		/// <summary>Gets or sets a value indicating whether constraint rules are followed when attempting any update operation.</summary>
		/// <returns>true if rules are enforced; otherwise false. The default is true.</returns>
		/// <exception cref="T:System.Data.ConstraintException">One or more constraints cannot be enforced. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0001AC28 File Offset: 0x00018E28
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x0001AC30 File Offset: 0x00018E30
		[DefaultValue(true)]
		public bool EnforceConstraints
		{
			get
			{
				return this._enforceConstraints;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataSet.set_EnforceConstraints|API> {0}, {1}", this.ObjectID, value);
				try
				{
					if (this._enforceConstraints != value)
					{
						if (value)
						{
							this.EnableConstraints();
						}
						this._enforceConstraints = value;
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001AC8C File Offset: 0x00018E8C
		internal void RestoreEnforceConstraints(bool value)
		{
			this._enforceConstraints = value;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001AC98 File Offset: 0x00018E98
		internal void EnableConstraints()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.EnableConstraints|INFO> {0}", this.ObjectID);
			try
			{
				bool flag = false;
				ConstraintEnumerator constraintEnumerator = new ConstraintEnumerator(this);
				while (constraintEnumerator.GetNext())
				{
					Constraint constraint = constraintEnumerator.GetConstraint();
					flag |= constraint.IsConstraintViolated();
				}
				foreach (object obj in this.Tables)
				{
					foreach (object obj2 in ((DataTable)obj).Columns)
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
				}
				if (flag)
				{
					this.FailedEnableConstraints();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Gets or sets the name of the current <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001ADBC File Offset: 0x00018FBC
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0001ADC4 File Offset: 0x00018FC4
		[DefaultValue("")]
		public string DataSetName
		{
			get
			{
				return this._dataSetName;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataSet.set_DataSetName|API> {0}, '{1}'", this.ObjectID, value);
				if (value != this._dataSetName)
				{
					if (value == null || value.Length == 0)
					{
						throw ExceptionBuilder.SetDataSetNameToEmpty();
					}
					DataTable dataTable = this.Tables[value, this.Namespace];
					if (dataTable != null && !dataTable._fNestedInDataset)
					{
						throw ExceptionBuilder.SetDataSetNameConflicting(value);
					}
					this.RaisePropertyChanging("DataSetName");
					this._dataSetName = value;
				}
			}
		}

		/// <summary>Gets or sets the namespace of the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Data.DataSet" />.</returns>
		/// <exception cref="T:System.ArgumentException">The namespace already has data. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001AE3D File Offset: 0x0001903D
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x0001AE48 File Offset: 0x00019048
		[DefaultValue("")]
		public string Namespace
		{
			get
			{
				return this._namespaceURI;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataSet.set_Namespace|API> {0}, '{1}'", this.ObjectID, value);
				if (value == null)
				{
					value = string.Empty;
				}
				if (value != this._namespaceURI)
				{
					this.RaisePropertyChanging("Namespace");
					foreach (object obj in this.Tables)
					{
						DataTable dataTable = (DataTable)obj;
						if (dataTable._tableNamespace == null && (dataTable.NestedParentRelations.Length == 0 || (dataTable.NestedParentRelations.Length == 1 && dataTable.NestedParentRelations[0].ChildTable == dataTable)))
						{
							if (this.Tables.Contains(dataTable.TableName, value, false, true))
							{
								throw ExceptionBuilder.DuplicateTableName2(dataTable.TableName, value);
							}
							dataTable.CheckCascadingNamespaceConflict(value);
							dataTable.DoRaiseNamespaceChange();
						}
					}
					this._namespaceURI = value;
					if (string.IsNullOrEmpty(value))
					{
						this._datasetPrefix = string.Empty;
					}
				}
			}
		}

		/// <summary>Gets or sets an XML prefix that aliases the namespace of the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The XML prefix for the <see cref="T:System.Data.DataSet" /> namespace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001AF4C File Offset: 0x0001914C
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x0001AF54 File Offset: 0x00019154
		[DefaultValue("")]
		public string Prefix
		{
			get
			{
				return this._datasetPrefix;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (XmlConvert.DecodeName(value) == value && XmlConvert.EncodeName(value) != value)
				{
					throw ExceptionBuilder.InvalidPrefix(value);
				}
				if (value != this._datasetPrefix)
				{
					this.RaisePropertyChanging("Prefix");
					this._datasetPrefix = value;
				}
			}
		}

		/// <summary>Gets the collection of customized user information associated with the DataSet.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> with all custom user information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001AFB0 File Offset: 0x000191B0
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

		/// <summary>Gets a value indicating whether there are errors in any of the <see cref="T:System.Data.DataTable" /> objects within this <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>true if any table contains an error;otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001AFD8 File Offset: 0x000191D8
		[Browsable(false)]
		public bool HasErrors
		{
			get
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					if (this.Tables[i].HasErrors)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataSet" /> is initialized.</summary>
		/// <returns>true to indicate the component has completed initialization; otherwise false.</returns>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001B011 File Offset: 0x00019211
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return !this._fInitInProgress;
			}
		}

		/// <summary>Gets or sets the locale information used to compare strings within the table.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that contains data about the user's machine locale. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001B01C File Offset: 0x0001921C
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001B024 File Offset: 0x00019224
		public CultureInfo Locale
		{
			get
			{
				return this._culture;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.set_Locale|API> {0}", this.ObjectID);
				try
				{
					if (value != null)
					{
						if (!this._culture.Equals(value))
						{
							this.SetLocaleValue(value, true);
						}
						this._cultureUserSet = true;
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001B088 File Offset: 0x00019288
		internal void SetLocaleValue(CultureInfo value, bool userSet)
		{
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			CultureInfo culture = this._culture;
			bool cultureUserSet = this._cultureUserSet;
			try
			{
				this._culture = value;
				this._cultureUserSet = userSet;
				foreach (object obj in this.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					if (!dataTable.ShouldSerializeLocale())
					{
						dataTable.SetLocaleValue(value, false, false);
					}
				}
				flag = this.ValidateLocaleConstraint();
				if (flag)
				{
					flag = false;
					foreach (object obj2 in this.Tables)
					{
						DataTable dataTable2 = (DataTable)obj2;
						num++;
						if (!dataTable2.ShouldSerializeLocale())
						{
							dataTable2.SetLocaleValue(value, false, true);
						}
					}
					flag = true;
				}
			}
			catch
			{
				flag2 = true;
				throw;
			}
			finally
			{
				if (!flag)
				{
					this._culture = culture;
					this._cultureUserSet = cultureUserSet;
					foreach (object obj3 in this.Tables)
					{
						DataTable dataTable3 = (DataTable)obj3;
						if (!dataTable3.ShouldSerializeLocale())
						{
							dataTable3.SetLocaleValue(culture, false, false);
						}
					}
					try
					{
						for (int i = 0; i < num; i++)
						{
							if (!this.Tables[i].ShouldSerializeLocale())
							{
								this.Tables[i].SetLocaleValue(culture, false, true);
							}
						}
					}
					catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
					{
						ADP.TraceExceptionWithoutRethrow(ex);
					}
					if (!flag2)
					{
						throw ExceptionBuilder.CannotChangeCaseLocale(null);
					}
				}
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001B294 File Offset: 0x00019494
		internal bool ShouldSerializeLocale()
		{
			return this._cultureUserSet;
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001B29C File Offset: 0x0001949C
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0001B2A4 File Offset: 0x000194A4
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
						for (int i = 0; i < this.Tables.Count; i++)
						{
							if (this.Tables[i].Site != null)
							{
								container.Remove(this.Tables[i]);
							}
						}
					}
				}
				base.Site = value;
			}
		}

		/// <summary>Get the collection of relations that link tables and allow navigation from parent tables to child tables.</summary>
		/// <returns>A <see cref="T:System.Data.DataRelationCollection" /> that contains a collection of <see cref="T:System.Data.DataRelation" /> objects. An empty collection is returned if no <see cref="T:System.Data.DataRelation" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001B30A File Offset: 0x0001950A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataRelationCollection Relations
		{
			get
			{
				return this._relationCollection;
			}
		}

		/// <summary>Gets a value indicating whether <see cref="P:System.Data.DataSet.Relations" /> property should be persisted.</summary>
		/// <returns>true if the property value has been changed from its default; otherwise false.</returns>
		// Token: 0x060006C8 RID: 1736 RVA: 0x0000EF2B File Offset: 0x0000D12B
		protected virtual bool ShouldSerializeRelations()
		{
			return true;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001B312 File Offset: 0x00019512
		private void ResetRelations()
		{
			this.Relations.Clear();
		}

		/// <summary>Gets the collection of tables contained in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The <see cref="T:System.Data.DataTableCollection" /> contained by this <see cref="T:System.Data.DataSet" />. An empty collection is returned if no <see cref="T:System.Data.DataTable" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001B31F File Offset: 0x0001951F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataTableCollection Tables
		{
			get
			{
				return this._tableCollection;
			}
		}

		/// <summary>Gets a value indicating whether <see cref="P:System.Data.DataSet.Tables" /> property should be persisted.</summary>
		/// <returns>true if the property value has been changed from its default; otherwise false.</returns>
		// Token: 0x060006CB RID: 1739 RVA: 0x0000EF2B File Offset: 0x0000D12B
		protected virtual bool ShouldSerializeTables()
		{
			return true;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001B327 File Offset: 0x00019527
		private void ResetTables()
		{
			this.Tables.Clear();
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001B334 File Offset: 0x00019534
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0001B33C File Offset: 0x0001953C
		internal bool FBoundToDocument
		{
			get
			{
				return this._fBoundToDocument;
			}
			set
			{
				this._fBoundToDocument = value;
			}
		}

		/// <summary>Commits all the changes made to this <see cref="T:System.Data.DataSet" /> since it was loaded or since the last time <see cref="M:System.Data.DataSet.AcceptChanges" /> was called.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060006CF RID: 1743 RVA: 0x0001B348 File Offset: 0x00019548
		public void AcceptChanges()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.AcceptChanges|API> {0}", this.ObjectID);
			try
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].AcceptChanges();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060006D0 RID: 1744 RVA: 0x0001B3B0 File Offset: 0x000195B0
		// (remove) Token: 0x060006D1 RID: 1745 RVA: 0x0001B3E8 File Offset: 0x000195E8
		internal event PropertyChangedEventHandler PropertyChanging;

		/// <summary>Occurs when a target and source <see cref="T:System.Data.DataRow" /> have the same primary key value, and <see cref="P:System.Data.DataSet.EnforceConstraints" /> is set to true.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060006D2 RID: 1746 RVA: 0x0001B420 File Offset: 0x00019620
		// (remove) Token: 0x060006D3 RID: 1747 RVA: 0x0001B458 File Offset: 0x00019658
		public event MergeFailedEventHandler MergeFailed;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060006D4 RID: 1748 RVA: 0x0001B490 File Offset: 0x00019690
		// (remove) Token: 0x060006D5 RID: 1749 RVA: 0x0001B4C8 File Offset: 0x000196C8
		internal event DataRowCreatedEventHandler DataRowCreated;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060006D6 RID: 1750 RVA: 0x0001B500 File Offset: 0x00019700
		// (remove) Token: 0x060006D7 RID: 1751 RVA: 0x0001B538 File Offset: 0x00019738
		internal event DataSetClearEventhandler ClearFunctionCalled;

		/// <summary>Occurs after the <see cref="T:System.Data.DataSet" /> is initialized.</summary>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060006D8 RID: 1752 RVA: 0x0001B570 File Offset: 0x00019770
		// (remove) Token: 0x060006D9 RID: 1753 RVA: 0x0001B5A8 File Offset: 0x000197A8
		public event EventHandler Initialized;

		/// <summary>Begins the initialization of a <see cref="T:System.Data.DataSet" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006DA RID: 1754 RVA: 0x0001B5DD File Offset: 0x000197DD
		public void BeginInit()
		{
			this._fInitInProgress = true;
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Data.DataSet" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006DB RID: 1755 RVA: 0x0001B5E8 File Offset: 0x000197E8
		public void EndInit()
		{
			this.Tables.FinishInitCollection();
			for (int i = 0; i < this.Tables.Count; i++)
			{
				this.Tables[i].Columns.FinishInitCollection();
			}
			for (int j = 0; j < this.Tables.Count; j++)
			{
				this.Tables[j].Constraints.FinishInitConstraints();
			}
			((DataRelationCollection.DataSetRelationCollection)this.Relations).FinishInitRelations();
			this._fInitInProgress = false;
			this.OnInitialized();
		}

		/// <summary>Clears the <see cref="T:System.Data.DataSet" /> of any data by removing all rows in all tables.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060006DC RID: 1756 RVA: 0x0001B678 File Offset: 0x00019878
		public void Clear()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Clear|API> {0}", this.ObjectID);
			try
			{
				this.OnClearFunctionCalled(null);
				bool enforceConstraints = this.EnforceConstraints;
				this.EnforceConstraints = false;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].Clear();
				}
				this.EnforceConstraints = enforceConstraints;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Copies the structure of the <see cref="T:System.Data.DataSet" />, including all <see cref="T:System.Data.DataTable" /> schemas, relations, and constraints. Does not copy any data.</summary>
		/// <returns>A new <see cref="T:System.Data.DataSet" /> with the same schema as the current <see cref="T:System.Data.DataSet" />, but none of the data.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006DD RID: 1757 RVA: 0x0001B6FC File Offset: 0x000198FC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual DataSet Clone()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Clone|API> {0}", this.ObjectID);
			DataSet dataSet2;
			try
			{
				DataSet dataSet = (DataSet)Activator.CreateInstance(base.GetType(), true);
				if (dataSet.Tables.Count > 0)
				{
					dataSet.Reset();
				}
				dataSet.DataSetName = this.DataSetName;
				dataSet.CaseSensitive = this.CaseSensitive;
				dataSet._culture = this._culture;
				dataSet._cultureUserSet = this._cultureUserSet;
				dataSet.EnforceConstraints = this.EnforceConstraints;
				dataSet.Namespace = this.Namespace;
				dataSet.Prefix = this.Prefix;
				dataSet.RemotingFormat = this.RemotingFormat;
				dataSet._fIsSchemaLoading = true;
				DataTableCollection tables = this.Tables;
				for (int i = 0; i < tables.Count; i++)
				{
					DataTable dataTable = tables[i].Clone(dataSet);
					dataTable._tableNamespace = tables[i].Namespace;
					dataSet.Tables.Add(dataTable);
				}
				for (int j = 0; j < tables.Count; j++)
				{
					ConstraintCollection constraints = tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (!(constraints[k] is UniqueConstraint))
						{
							ForeignKeyConstraint foreignKeyConstraint = constraints[k] as ForeignKeyConstraint;
							if (foreignKeyConstraint.Table != foreignKeyConstraint.RelatedTable)
							{
								dataSet.Tables[j].Constraints.Add(constraints[k].Clone(dataSet));
							}
						}
					}
				}
				DataRelationCollection relations = this.Relations;
				for (int l = 0; l < relations.Count; l++)
				{
					DataRelation dataRelation = relations[l].Clone(dataSet);
					dataRelation.CheckMultipleNested = false;
					dataSet.Relations.Add(dataRelation);
					dataRelation.CheckMultipleNested = true;
				}
				if (this._extendedProperties != null)
				{
					foreach (object obj in this._extendedProperties.Keys)
					{
						dataSet.ExtendedProperties[obj] = this._extendedProperties[obj];
					}
				}
				foreach (object obj2 in this.Tables)
				{
					DataTable dataTable2 = (DataTable)obj2;
					foreach (object obj3 in dataTable2.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj3;
						if (dataColumn.Expression.Length != 0)
						{
							dataSet.Tables[dataTable2.TableName, dataTable2.Namespace].Columns[dataColumn.ColumnName].Expression = dataColumn.Expression;
						}
					}
				}
				for (int m = 0; m < tables.Count; m++)
				{
					dataSet.Tables[m]._tableNamespace = tables[m]._tableNamespace;
				}
				dataSet._fIsSchemaLoading = false;
				dataSet2 = dataSet;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataSet2;
		}

		/// <summary>Copies both the structure and data for this <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataSet" /> with the same structure (table schemas, relations, and constraints) and data as this <see cref="T:System.Data.DataSet" />.NoteIf these classes have been subclassed, the copy will also be of the same subclasses.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006DE RID: 1758 RVA: 0x0001BAB0 File Offset: 0x00019CB0
		public DataSet Copy()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Copy|API> {0}", this.ObjectID);
			DataSet dataSet2;
			try
			{
				DataSet dataSet = this.Clone();
				bool enforceConstraints = dataSet.EnforceConstraints;
				dataSet.EnforceConstraints = false;
				foreach (object obj in this.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					DataTable dataTable2 = dataSet.Tables[dataTable.TableName, dataTable.Namespace];
					foreach (object obj2 in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						dataTable.CopyRow(dataTable2, dataRow);
					}
				}
				dataSet.EnforceConstraints = enforceConstraints;
				dataSet2 = dataSet;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataSet2;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001BBC8 File Offset: 0x00019DC8
		internal int EstimatedXmlStringSize()
		{
			int num = 100;
			for (int i = 0; i < this.Tables.Count; i++)
			{
				int num2 = this.Tables[i].TableName.Length + 4 << 2;
				DataTable dataTable = this.Tables[i];
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					num2 += dataTable.Columns[j].ColumnName.Length + 4 << 2;
					num2 += 20;
				}
				num += dataTable.Rows.Count * num2;
			}
			return num;
		}

		/// <summary>Gets a copy of the <see cref="T:System.Data.DataSet" /> that contains all changes made to it since it was loaded or since <see cref="M:System.Data.DataSet.AcceptChanges" /> was last called.</summary>
		/// <returns>A copy of the changes from this <see cref="T:System.Data.DataSet" /> that can have actions performed on it and later be merged back in using <see cref="M:System.Data.DataSet.Merge(System.Data.DataSet)" />. If no changed rows are found, the method returns null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006E0 RID: 1760 RVA: 0x0001BC67 File Offset: 0x00019E67
		public DataSet GetChanges()
		{
			return this.GetChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
		}

		/// <summary>Gets a copy of the <see cref="T:System.Data.DataSet" /> containing all changes made to it since it was last loaded, or since <see cref="M:System.Data.DataSet.AcceptChanges" /> was called, filtered by <see cref="T:System.Data.DataRowState" />.</summary>
		/// <returns>A filtered copy of the <see cref="T:System.Data.DataSet" /> that can have actions performed on it, and subsequently be merged back in using <see cref="M:System.Data.DataSet.Merge(System.Data.DataSet)" />. If no rows of the desired <see cref="T:System.Data.DataRowState" /> are found, the method returns null.</returns>
		/// <param name="rowStates">One of the <see cref="T:System.Data.DataRowState" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060006E1 RID: 1761 RVA: 0x0001BC74 File Offset: 0x00019E74
		public DataSet GetChanges(DataRowState rowStates)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, DataRowState>("<ds.DataSet.GetChanges|API> {0}, rowStates={1}", this.ObjectID, rowStates);
			DataSet dataSet2;
			try
			{
				DataSet dataSet = null;
				bool flag = false;
				if ((rowStates & ~(DataRowState.Unchanged | DataRowState.Added | DataRowState.Deleted | DataRowState.Modified)) != (DataRowState)0)
				{
					throw ExceptionBuilder.InvalidRowState(rowStates);
				}
				DataSet.TableChanges[] array = new DataSet.TableChanges[this.Tables.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new DataSet.TableChanges(this.Tables[i].Rows.Count);
				}
				this.MarkModifiedRows(array, rowStates);
				for (int j = 0; j < array.Length; j++)
				{
					if (0 < array[j].HasChanges)
					{
						if (dataSet == null)
						{
							dataSet = this.Clone();
							flag = dataSet.EnforceConstraints;
							dataSet.EnforceConstraints = false;
						}
						DataTable dataTable = this.Tables[j];
						DataTable dataTable2 = dataSet.Tables[dataTable.TableName, dataTable.Namespace];
						int num2 = 0;
						while (0 < array[j].HasChanges)
						{
							if (array[j][num2])
							{
								dataTable.CopyRow(dataTable2, dataTable.Rows[num2]);
								DataSet.TableChanges[] array2 = array;
								int num3 = j;
								int hasChanges = array2[num3].HasChanges;
								array2[num3].HasChanges = hasChanges - 1;
							}
							num2++;
						}
					}
				}
				if (dataSet != null)
				{
					dataSet.EnforceConstraints = flag;
				}
				dataSet2 = dataSet;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataSet2;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001BDFC File Offset: 0x00019FFC
		private void MarkModifiedRows(DataSet.TableChanges[] bitMatrix, DataRowState rowStates)
		{
			for (int i = 0; i < bitMatrix.Length; i++)
			{
				DataRowCollection rows = this.Tables[i].Rows;
				int count = rows.Count;
				for (int j = 0; j < count; j++)
				{
					DataRow dataRow = rows[j];
					DataRowState rowState = dataRow.RowState;
					if ((rowStates & rowState) != (DataRowState)0 && !bitMatrix[i][j])
					{
						bitMatrix[i][j] = true;
						if (DataRowState.Deleted != rowState)
						{
							this.MarkRelatedRowsAsModified(bitMatrix, dataRow);
						}
					}
				}
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001BE80 File Offset: 0x0001A080
		private void MarkRelatedRowsAsModified(DataSet.TableChanges[] bitMatrix, DataRow row)
		{
			DataRelationCollection parentRelations = row.Table.ParentRelations;
			int count = parentRelations.Count;
			for (int i = 0; i < count; i++)
			{
				foreach (DataRow dataRow in row.GetParentRows(parentRelations[i], DataRowVersion.Current))
				{
					int num = this.Tables.IndexOf(dataRow.Table);
					int num2 = dataRow.Table.Rows.IndexOf(dataRow);
					if (!bitMatrix[num][num2])
					{
						bitMatrix[num][num2] = true;
						if (DataRowState.Deleted != dataRow.RowState)
						{
							this.MarkRelatedRowsAsModified(bitMatrix, dataRow);
						}
					}
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</returns>
		// Token: 0x060006E4 RID: 1764 RVA: 0x0001BF3A File Offset: 0x0001A13A
		IList IListSource.GetList()
		{
			return this.DefaultViewManager;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001BF44 File Offset: 0x0001A144
		internal string GetRemotingDiffGram(DataTable table)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			if (stringWriter != null)
			{
				new NewDiffgramGen(table, false).Save(xmlTextWriter, table);
			}
			return stringWriter.ToString();
		}

		/// <summary>Returns the XML representation of the data stored in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A string that is a representation of the data stored in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006E6 RID: 1766 RVA: 0x0001BF80 File Offset: 0x0001A180
		public string GetXml()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.GetXml|API> {0}", this.ObjectID);
			string text;
			try
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				if (stringWriter != null)
				{
					XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
					xmlTextWriter.Formatting = Formatting.Indented;
					new XmlDataTreeWriter(this).Save(xmlTextWriter, false);
				}
				text = stringWriter.ToString();
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return text;
		}

		/// <summary>Returns the XML Schema for the XML representation of the data stored in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>String that is the XML Schema for the XML representation of the data stored in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006E7 RID: 1767 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
		public string GetXmlSchema()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.GetXmlSchema|API> {0}", this.ObjectID);
			string text;
			try
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				xmlTextWriter.Formatting = Formatting.Indented;
				if (stringWriter != null)
				{
					new XmlTreeGen(SchemaFormat.Public).Save(this, xmlTextWriter);
				}
				text = stringWriter.ToString();
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return text;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001C064 File Offset: 0x0001A264
		internal string GetXmlSchemaForRemoting(DataTable table)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			if (stringWriter != null)
			{
				if (table == null)
				{
					if (this.SchemaSerializationMode == SchemaSerializationMode.ExcludeSchema)
					{
						new XmlTreeGen(SchemaFormat.RemotingSkipSchema).Save(this, xmlTextWriter);
					}
					else
					{
						new XmlTreeGen(SchemaFormat.Remoting).Save(this, xmlTextWriter);
					}
				}
				else
				{
					new XmlTreeGen(SchemaFormat.Remoting).Save(table, xmlTextWriter);
				}
			}
			return stringWriter.ToString();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.DataSet" /> has changes, including new, deleted, or modified rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataSet" /> has changes; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060006E9 RID: 1769 RVA: 0x0001C0C8 File Offset: 0x0001A2C8
		public bool HasChanges()
		{
			return this.HasChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.DataSet" /> has changes, including new, deleted, or modified rows, filtered by <see cref="T:System.Data.DataRowState" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataSet" /> has changes; otherwise false.</returns>
		/// <param name="rowStates">One of the <see cref="T:System.Data.DataRowState" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060006EA RID: 1770 RVA: 0x0001C0D4 File Offset: 0x0001A2D4
		public bool HasChanges(DataRowState rowStates)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataSet.HasChanges|API> {0}, rowStates={1}", this.ObjectID, (int)rowStates);
			bool flag;
			try
			{
				if ((rowStates & ~(DataRowState.Detached | DataRowState.Unchanged | DataRowState.Added | DataRowState.Deleted | DataRowState.Modified)) != (DataRowState)0)
				{
					throw ExceptionBuilder.ArgumentOutOfRange("rowState");
				}
				for (int i = 0; i < this.Tables.Count; i++)
				{
					DataTable dataTable = this.Tables[i];
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						if ((dataTable.Rows[j].RowState & rowStates) != (DataRowState)0)
						{
							return true;
						}
					}
				}
				flag = false;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return flag;
		}

		/// <summary>Applies the XML schema from the specified <see cref="T:System.Xml.XmlReader" /> to the <see cref="T:System.Data.DataSet" />. </summary>
		/// <param name="reader">The XMLReader from which to read the schema. </param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006EB RID: 1771 RVA: 0x0001C180 File Offset: 0x0001A380
		public void InferXmlSchema(XmlReader reader, string[] nsArray)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.InferXmlSchema|API> {0}", this.ObjectID);
			try
			{
				if (reader != null)
				{
					XmlDocument xmlDocument = new XmlDocument();
					if (reader.NodeType == XmlNodeType.Element)
					{
						XmlNode xmlNode = xmlDocument.ReadNode(reader);
						xmlDocument.AppendChild(xmlNode);
					}
					else
					{
						xmlDocument.Load(reader);
					}
					if (xmlDocument.DocumentElement != null)
					{
						this.InferSchema(xmlDocument, nsArray, XmlReadMode.InferSchema);
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Applies the XML schema from the specified <see cref="T:System.IO.Stream" /> to the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="stream">The Stream from which to read the schema. </param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006EC RID: 1772 RVA: 0x0001C204 File Offset: 0x0001A404
		public void InferXmlSchema(Stream stream, string[] nsArray)
		{
			if (stream == null)
			{
				return;
			}
			this.InferXmlSchema(new XmlTextReader(stream), nsArray);
		}

		/// <summary>Applies the XML schema from the specified <see cref="T:System.IO.TextReader" /> to the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The TextReader from which to read the schema. </param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006ED RID: 1773 RVA: 0x0001C217 File Offset: 0x0001A417
		public void InferXmlSchema(TextReader reader, string[] nsArray)
		{
			if (reader == null)
			{
				return;
			}
			this.InferXmlSchema(new XmlTextReader(reader), nsArray);
		}

		/// <summary>Applies the XML schema from the specified file to the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="fileName">The name of the file (including the path) from which to read the schema. </param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006EE RID: 1774 RVA: 0x0001C22C File Offset: 0x0001A42C
		public void InferXmlSchema(string fileName, string[] nsArray)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			try
			{
				this.InferXmlSchema(xmlTextReader, nsArray);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		/// <summary>Reads the XML schema from the specified <see cref="T:System.Xml.XmlReader" /> into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006EF RID: 1775 RVA: 0x0001C264 File Offset: 0x0001A464
		public void ReadXmlSchema(XmlReader reader)
		{
			this.ReadXmlSchema(reader, false);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001C270 File Offset: 0x0001A470
		internal void ReadXmlSchema(XmlReader reader, bool denyResolving)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataSet.ReadXmlSchema|INFO> {0}, reader, denyResolving={1}", this.ObjectID, denyResolving);
			try
			{
				int num2 = -1;
				if (reader != null)
				{
					if (reader is XmlTextReader)
					{
						((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.None;
					}
					XmlDocument xmlDocument = new XmlDocument();
					if (reader.NodeType == XmlNodeType.Element)
					{
						num2 = reader.Depth;
					}
					reader.MoveToContent();
					if (reader.NodeType == XmlNodeType.Element)
					{
						if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
						{
							this.ReadXDRSchema(reader);
						}
						else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
						{
							this.ReadXSDSchema(reader, denyResolving);
						}
						else
						{
							if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
							{
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
							while (this.MoveToElement(reader, num2))
							{
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									this.ReadXDRSchema(reader);
									return;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									this.ReadXSDSchema(reader, denyResolving);
									return;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
								{
									throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
								}
								XmlNode xmlNode = xmlDocument.ReadNode(reader);
								xmlElement.AppendChild(xmlNode);
							}
							this.ReadEndElement(reader);
							xmlDocument.AppendChild(xmlElement);
							this.InferSchema(xmlDocument, null, XmlReadMode.Auto);
						}
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001C4FC File Offset: 0x0001A6FC
		internal bool MoveToElement(XmlReader reader, int depth)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element && reader.Depth > depth)
			{
				reader.Read();
			}
			return reader.NodeType == XmlNodeType.Element;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001C534 File Offset: 0x0001A734
		private static void MoveToElement(XmlReader reader)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element)
			{
				reader.Read();
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001C55A File Offset: 0x0001A75A
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

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001C590 File Offset: 0x0001A790
		internal void ReadXSDSchema(XmlReader reader, bool denyResolving)
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			int num = 1;
			if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema" && reader.HasAttributes)
			{
				string attribute = reader.GetAttribute("schemafragmentcount", "urn:schemas-microsoft-com:xml-msdata");
				if (!string.IsNullOrEmpty(attribute))
				{
					num = int.Parse(attribute, null);
				}
			}
			while (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
			{
				XmlSchema xmlSchema = XmlSchema.Read(reader, null);
				xmlSchemaSet.Add(xmlSchema);
				this.ReadEndElement(reader);
				if (--num > 0)
				{
					DataSet.MoveToElement(reader);
				}
				while (reader.NodeType == XmlNodeType.Whitespace)
				{
					reader.Skip();
				}
			}
			xmlSchemaSet.Compile();
			new XSDSchema().LoadSchema(xmlSchemaSet, this);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001C664 File Offset: 0x0001A864
		internal void ReadXDRSchema(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNode = xmlDocument.ReadNode(reader);
			xmlDocument.AppendChild(xmlNode);
			XDRSchema xdrschema = new XDRSchema(this, false);
			this.DataSetName = xmlDocument.DocumentElement.LocalName;
			xdrschema.LoadSchema((XmlElement)xmlNode, this);
		}

		/// <summary>Reads the XML schema from the specified <see cref="T:System.IO.Stream" /> into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F6 RID: 1782 RVA: 0x0001C6AB File Offset: 0x0001A8AB
		public void ReadXmlSchema(Stream stream)
		{
			if (stream == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(stream), false);
		}

		/// <summary>Reads the XML schema from the specified <see cref="T:System.IO.TextReader" /> into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F7 RID: 1783 RVA: 0x0001C6BE File Offset: 0x0001A8BE
		public void ReadXmlSchema(TextReader reader)
		{
			if (reader == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(reader), false);
		}

		/// <summary>Reads the XML schema from the specified file into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="fileName">The file name (including the path) from which to read. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F8 RID: 1784 RVA: 0x0001C6D4 File Offset: 0x0001A8D4
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

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object used to write to a file. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006F9 RID: 1785 RVA: 0x0001C70C File Offset: 0x0001A90C
		public void WriteXmlSchema(Stream stream)
		{
			this.WriteXmlSchema(stream, SchemaFormat.Public, null);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object to write to. </param>
		/// <param name="multipleTargetConverter">A delegate used to convert <see cref="T:System.Type" /> to string.</param>
		// Token: 0x060006FA RID: 1786 RVA: 0x0001C717 File Offset: 0x0001A917
		public void WriteXmlSchema(Stream stream, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(stream, SchemaFormat.Public, multipleTargetConverter);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to a file.</summary>
		/// <param name="fileName">The file name (including the path) to which to write. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006FB RID: 1787 RVA: 0x0001C72D File Offset: 0x0001A92D
		public void WriteXmlSchema(string fileName)
		{
			this.WriteXmlSchema(fileName, SchemaFormat.Public, null);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to a file.</summary>
		/// <param name="fileName">The name of the file to write to. </param>
		/// <param name="multipleTargetConverter">A delegate used to convert <see cref="T:System.Type" /> to string.</param>
		// Token: 0x060006FC RID: 1788 RVA: 0x0001C738 File Offset: 0x0001A938
		public void WriteXmlSchema(string fileName, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(fileName, SchemaFormat.Public, multipleTargetConverter);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> object with which to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006FD RID: 1789 RVA: 0x0001C74E File Offset: 0x0001A94E
		public void WriteXmlSchema(TextWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.Public, null);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> object to write to. </param>
		/// <param name="multipleTargetConverter">A delegate used to convert <see cref="T:System.Type" /> to string.</param>
		// Token: 0x060006FE RID: 1790 RVA: 0x0001C759 File Offset: 0x0001A959
		public void WriteXmlSchema(TextWriter writer, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(writer, SchemaFormat.Public, multipleTargetConverter);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to an <see cref="T:System.Xml.XmlWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to write to. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006FF RID: 1791 RVA: 0x0001C76F File Offset: 0x0001A96F
		public void WriteXmlSchema(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.Public, null);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" /> object to write to. </param>
		/// <param name="multipleTargetConverter">A delegate used to convert <see cref="T:System.Type" /> to string.</param>
		// Token: 0x06000700 RID: 1792 RVA: 0x0001C77A File Offset: 0x0001A97A
		public void WriteXmlSchema(XmlWriter writer, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(writer, SchemaFormat.Public, multipleTargetConverter);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001C790 File Offset: 0x0001A990
		private void WriteXmlSchema(string fileName, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			try
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument(true);
				this.WriteXmlSchema(xmlTextWriter, schemaFormat, multipleTargetConverter);
				xmlTextWriter.WriteEndDocument();
			}
			finally
			{
				xmlTextWriter.Close();
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001C7DC File Offset: 0x0001A9DC
		private void WriteXmlSchema(Stream stream, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			if (stream == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented
			}, schemaFormat, multipleTargetConverter);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001C808 File Offset: 0x0001AA08
		private void WriteXmlSchema(TextWriter writer, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			if (writer == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented
			}, schemaFormat, multipleTargetConverter);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001C830 File Offset: 0x0001AA30
		private void WriteXmlSchema(XmlWriter writer, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, SchemaFormat>("<ds.DataSet.WriteXmlSchema|INFO> {0}, schemaFormat={1}", this.ObjectID, schemaFormat);
			try
			{
				if (writer != null)
				{
					XmlTreeGen xmlTreeGen;
					if (schemaFormat == SchemaFormat.WebService && this.SchemaSerializationMode == SchemaSerializationMode.ExcludeSchema && writer.WriteState == WriteState.Element)
					{
						xmlTreeGen = new XmlTreeGen(SchemaFormat.WebServiceSkipSchema);
					}
					else
					{
						xmlTreeGen = new XmlTreeGen(schemaFormat);
					}
					xmlTreeGen.Save(this, null, writer, false, multipleTargetConverter);
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000705 RID: 1797 RVA: 0x0001C8A8 File Offset: 0x0001AAA8
		public XmlReadMode ReadXml(XmlReader reader)
		{
			return this.ReadXml(reader, false);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001C8B4 File Offset: 0x0001AAB4
		internal XmlReadMode ReadXml(XmlReader reader, bool denyResolving)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataSet.ReadXml|INFO> {0}, denyResolving={1}", this.ObjectID, denyResolving);
			XmlReadMode xmlReadMode2;
			try
			{
				DataTable.DSRowDiffIdUsageSection dsrowDiffIdUsageSection = default(DataTable.DSRowDiffIdUsageSection);
				try
				{
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					int num2 = -1;
					XmlReadMode xmlReadMode = XmlReadMode.Auto;
					bool flag5 = false;
					bool flag6 = false;
					dsrowDiffIdUsageSection.Prepare(this);
					if (reader == null)
					{
						xmlReadMode2 = xmlReadMode;
					}
					else
					{
						if (this.Tables.Count == 0)
						{
							flag5 = true;
						}
						if (reader is XmlTextReader)
						{
							((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
						}
						XmlDocument xmlDocument = new XmlDocument();
						XmlDataLoader xmlDataLoader = null;
						reader.MoveToContent();
						if (reader.NodeType == XmlNodeType.Element)
						{
							num2 = reader.Depth;
						}
						if (reader.NodeType == XmlNodeType.Element)
						{
							if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
							{
								this.ReadXmlDiffgram(reader);
								this.ReadEndElement(reader);
								return XmlReadMode.DiffGram;
							}
							if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
							{
								this.ReadXDRSchema(reader);
								return XmlReadMode.ReadSchema;
							}
							if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
							{
								this.ReadXSDSchema(reader, denyResolving);
								return XmlReadMode.ReadSchema;
							}
							if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
							{
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
							string value = reader.Value;
							while (this.MoveToElement(reader, num2))
							{
								if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									this.ReadXmlDiffgram(reader);
									xmlReadMode = XmlReadMode.DiffGram;
								}
								if (!flag2 && !flag && reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									this.ReadXDRSchema(reader);
									flag2 = true;
									flag4 = true;
								}
								else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									this.ReadXSDSchema(reader, denyResolving);
									flag2 = true;
								}
								else
								{
									if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
									{
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
									{
										this.ReadXmlDiffgram(reader);
										flag3 = true;
										xmlReadMode = XmlReadMode.DiffGram;
									}
									else
									{
										while (!reader.EOF && reader.NodeType == XmlNodeType.Whitespace)
										{
											reader.Read();
										}
										if (reader.NodeType == XmlNodeType.Element)
										{
											flag = true;
											if (!flag2 && this.Tables.Count == 0)
											{
												XmlNode xmlNode = xmlDocument.ReadNode(reader);
												xmlElement.AppendChild(xmlNode);
											}
											else
											{
												if (xmlDataLoader == null)
												{
													xmlDataLoader = new XmlDataLoader(this, flag4, xmlElement, false);
												}
												xmlDataLoader.LoadData(reader);
												flag6 = true;
												if (flag2)
												{
													xmlReadMode = XmlReadMode.ReadSchema;
												}
												else
												{
													xmlReadMode = XmlReadMode.IgnoreSchema;
												}
											}
										}
									}
								}
							}
							this.ReadEndElement(reader);
							bool flag7 = false;
							bool fTopLevelTable = this._fTopLevelTable;
							if (!flag2 && this.Tables.Count == 0 && !xmlElement.HasChildNodes)
							{
								this._fTopLevelTable = true;
								flag7 = true;
								if (value != null && value.Length > 0)
								{
									xmlElement.InnerText = value;
								}
							}
							if (!flag5 && value != null && value.Length > 0)
							{
								xmlElement.InnerText = value;
							}
							xmlDocument.AppendChild(xmlElement);
							if (xmlDataLoader == null)
							{
								xmlDataLoader = new XmlDataLoader(this, flag4, xmlElement, false);
							}
							if (!flag5 && !flag6)
							{
								XmlElement documentElement = xmlDocument.DocumentElement;
								if (documentElement.ChildNodes.Count == 0 || (documentElement.ChildNodes.Count == 1 && documentElement.FirstChild.GetType() == typeof(XmlText)))
								{
									bool fTopLevelTable2 = this._fTopLevelTable;
									if (this.DataSetName != documentElement.Name && this._namespaceURI != documentElement.NamespaceURI && this.Tables.Contains(documentElement.Name, (documentElement.NamespaceURI.Length == 0) ? null : documentElement.NamespaceURI, false, true))
									{
										this._fTopLevelTable = true;
									}
									try
									{
										xmlDataLoader.LoadData(xmlDocument);
									}
									finally
									{
										this._fTopLevelTable = fTopLevelTable2;
									}
								}
							}
							if (!flag3)
							{
								if (!flag2 && this.Tables.Count == 0)
								{
									this.InferSchema(xmlDocument, null, XmlReadMode.Auto);
									xmlReadMode = XmlReadMode.InferSchema;
									xmlDataLoader.FromInference = true;
									try
									{
										xmlDataLoader.LoadData(xmlDocument);
									}
									finally
									{
										xmlDataLoader.FromInference = false;
									}
								}
								if (flag7)
								{
									this._fTopLevelTable = fTopLevelTable;
								}
							}
						}
						xmlReadMode2 = xmlReadMode;
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

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="stream">An object that derives from <see cref="T:System.IO.Stream" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000707 RID: 1799 RVA: 0x0001CE6C File Offset: 0x0001B06C
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

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="reader">The TextReader from which to read the schema and data. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000708 RID: 1800 RVA: 0x0001CE94 File Offset: 0x0001B094
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

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified file.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="fileName">The filename (including the path) from which to read. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000709 RID: 1801 RVA: 0x0001CEBC File Offset: 0x0001B0BC
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

		// Token: 0x0600070A RID: 1802 RVA: 0x0001CEFC File Offset: 0x0001B0FC
		internal void InferSchema(XmlDocument xdoc, string[] excludedNamespaces, XmlReadMode mode)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, XmlReadMode>("<ds.DataSet.InferSchema|INFO> {0}, mode={1}", this.ObjectID, mode);
			try
			{
				string namespaceURI = xdoc.DocumentElement.NamespaceURI;
				if (excludedNamespaces == null)
				{
					excludedNamespaces = Array.Empty<string>();
				}
				XmlNodeReader xmlNodeReader = new XmlIgnoreNamespaceReader(xdoc, excludedNamespaces);
				XmlSchemaSet xmlSchemaSet = new XmlSchemaInference
				{
					Occurrence = XmlSchemaInference.InferenceOption.Relaxed,
					TypeInference = ((mode == XmlReadMode.InferTypedSchema) ? XmlSchemaInference.InferenceOption.Restricted : XmlSchemaInference.InferenceOption.Relaxed)
				}.InferSchema(xmlNodeReader);
				xmlSchemaSet.Compile();
				XSDSchema xsdschema = new XSDSchema();
				xsdschema.FromInference = true;
				try
				{
					xsdschema.LoadSchema(xmlSchemaSet, this);
				}
				finally
				{
					xsdschema.FromInference = false;
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001CFAC File Offset: 0x0001B1AC
		private bool IsEmpty()
		{
			using (IEnumerator enumerator = this.Tables.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((DataTable)enumerator.Current).Rows.Count > 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001D014 File Offset: 0x0001B214
		private void ReadXmlDiffgram(XmlReader reader)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.ReadXmlDiffgram|INFO> {0}", this.ObjectID);
			try
			{
				int depth = reader.Depth;
				bool enforceConstraints = this.EnforceConstraints;
				this.EnforceConstraints = false;
				bool flag = this.IsEmpty();
				DataSet dataSet;
				if (flag)
				{
					dataSet = this;
				}
				else
				{
					dataSet = this.Clone();
					dataSet.EnforceConstraints = false;
				}
				foreach (object obj in dataSet.Tables)
				{
					((DataTable)obj).Rows._nullInList = 0;
				}
				reader.MoveToContent();
				if (!(reader.LocalName != "diffgram") || !(reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1"))
				{
					reader.Read();
					if (reader.NodeType == XmlNodeType.Whitespace)
					{
						this.MoveToElement(reader, reader.Depth - 1);
					}
					dataSet._fInLoadDiffgram = true;
					if (reader.Depth > depth)
					{
						if (reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1" && reader.NamespaceURI != "urn:schemas-microsoft-com:xml-msdata")
						{
							XmlElement xmlElement = new XmlDocument().CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							reader.Read();
							if (reader.NodeType == XmlNodeType.Whitespace)
							{
								this.MoveToElement(reader, reader.Depth - 1);
							}
							if (reader.Depth - 1 > depth)
							{
								new XmlDataLoader(dataSet, false, xmlElement, false)
								{
									_isDiffgram = true
								}.LoadData(reader);
							}
							this.ReadEndElement(reader);
							if (reader.NodeType == XmlNodeType.Whitespace)
							{
								this.MoveToElement(reader, reader.Depth - 1);
							}
						}
						if ((reader.LocalName == "before" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1") || (reader.LocalName == "errors" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1"))
						{
							new XMLDiffLoader().LoadDiffGram(dataSet, reader);
						}
						while (reader.Depth > depth)
						{
							reader.Read();
						}
						this.ReadEndElement(reader);
					}
					foreach (object obj2 in dataSet.Tables)
					{
						DataTable dataTable = (DataTable)obj2;
						if (dataTable.Rows._nullInList > 0)
						{
							throw ExceptionBuilder.RowInsertMissing(dataTable.TableName);
						}
					}
					dataSet._fInLoadDiffgram = false;
					foreach (object obj3 in dataSet.Tables)
					{
						DataTable dataTable2 = (DataTable)obj3;
						DataRelation[] nestedParentRelations = dataTable2.NestedParentRelations;
						DataRelation[] array = nestedParentRelations;
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i].ParentTable == dataTable2)
							{
								foreach (object obj4 in dataTable2.Rows)
								{
									DataRow dataRow = (DataRow)obj4;
									foreach (DataRelation dataRelation in nestedParentRelations)
									{
										dataRow.CheckForLoops(dataRelation);
									}
								}
							}
						}
					}
					if (!flag)
					{
						this.Merge(dataSet);
						if (this._dataSetName == "NewDataSet")
						{
							this._dataSetName = dataSet._dataSetName;
						}
						dataSet.EnforceConstraints = enforceConstraints;
					}
					this.EnforceConstraints = enforceConstraints;
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlReader" /> and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600070D RID: 1805 RVA: 0x0001D428 File Offset: 0x0001B628
		public XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode)
		{
			return this.ReadXml(reader, mode, false);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001D434 File Offset: 0x0001B634
		internal XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode, bool denyResolving)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, XmlReadMode, bool>("<ds.DataSet.ReadXml|INFO> {0}, mode={1}, denyResolving={2}", this.ObjectID, mode, denyResolving);
			XmlReadMode xmlReadMode2;
			try
			{
				XmlReadMode xmlReadMode = mode;
				if (reader == null)
				{
					xmlReadMode2 = xmlReadMode;
				}
				else if (mode == XmlReadMode.Auto)
				{
					xmlReadMode2 = this.ReadXml(reader);
				}
				else
				{
					DataTable.DSRowDiffIdUsageSection dsrowDiffIdUsageSection = default(DataTable.DSRowDiffIdUsageSection);
					try
					{
						bool flag = false;
						bool flag2 = false;
						bool flag3 = false;
						int num2 = -1;
						dsrowDiffIdUsageSection.Prepare(this);
						if (reader is XmlTextReader)
						{
							((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
						}
						XmlDocument xmlDocument = new XmlDocument();
						if (mode != XmlReadMode.Fragment && reader.NodeType == XmlNodeType.Element)
						{
							num2 = reader.Depth;
						}
						reader.MoveToContent();
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
										this.ReadXmlDiffgram(reader);
										this.ReadEndElement(reader);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXDRSchema(reader);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXSDSchema(reader, denyResolving);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
								{
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
							while (this.MoveToElement(reader, num2))
							{
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (!flag && !flag2 && mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
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
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXSDSchema(reader, denyResolving);
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
										this.ReadXmlDiffgram(reader);
										xmlReadMode = XmlReadMode.DiffGram;
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
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									if (mode == XmlReadMode.DiffGram)
									{
										reader.Skip();
									}
									else
									{
										flag2 = true;
										if (mode == XmlReadMode.InferSchema || mode == XmlReadMode.InferTypedSchema)
										{
											XmlNode xmlNode = xmlDocument.ReadNode(reader);
											xmlElement.AppendChild(xmlNode);
										}
										else
										{
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
								return xmlReadMode;
							}
							if (mode == XmlReadMode.InferSchema || mode == XmlReadMode.InferTypedSchema)
							{
								this.InferSchema(xmlDocument, null, mode);
								xmlReadMode = XmlReadMode.InferSchema;
								xmlDataLoader.FromInference = true;
								try
								{
									xmlDataLoader.LoadData(xmlDocument);
								}
								finally
								{
									xmlDataLoader.FromInference = false;
								}
							}
						}
						xmlReadMode2 = xmlReadMode;
					}
					finally
					{
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return xmlReadMode2;
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" /> and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which to read. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600070F RID: 1807 RVA: 0x0001D8D0 File Offset: 0x0001BAD0
		public XmlReadMode ReadXml(Stream stream, XmlReadMode mode)
		{
			if (stream == null)
			{
				return XmlReadMode.Auto;
			}
			XmlTextReader xmlTextReader = ((mode == XmlReadMode.Fragment) ? new XmlTextReader(stream, XmlNodeType.Element, null) : new XmlTextReader(stream));
			xmlTextReader.XmlResolver = null;
			return this.ReadXml(xmlTextReader, mode, false);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextReader" /> and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> from which to read. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000710 RID: 1808 RVA: 0x0001D908 File Offset: 0x0001BB08
		public XmlReadMode ReadXml(TextReader reader, XmlReadMode mode)
		{
			if (reader == null)
			{
				return XmlReadMode.Auto;
			}
			XmlTextReader xmlTextReader = ((mode == XmlReadMode.Fragment) ? new XmlTextReader(reader.ReadToEnd(), XmlNodeType.Element, null) : new XmlTextReader(reader));
			xmlTextReader.XmlResolver = null;
			return this.ReadXml(xmlTextReader, mode, false);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified file and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="fileName">The filename (including the path) from which to read. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000711 RID: 1809 RVA: 0x0001D944 File Offset: 0x0001BB44
		public XmlReadMode ReadXml(string fileName, XmlReadMode mode)
		{
			XmlTextReader xmlTextReader = null;
			if (mode == XmlReadMode.Fragment)
			{
				xmlTextReader = new XmlTextReader(new FileStream(fileName, FileMode.Open), XmlNodeType.Element, null);
			}
			else
			{
				xmlTextReader = new XmlTextReader(fileName);
			}
			xmlTextReader.XmlResolver = null;
			XmlReadMode xmlReadMode;
			try
			{
				xmlReadMode = this.ReadXml(xmlTextReader, mode, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
			return xmlReadMode;
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object used to write to a file. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000712 RID: 1810 RVA: 0x0001D99C File Offset: 0x0001BB9C
		public void WriteXml(Stream stream)
		{
			this.WriteXml(stream, XmlWriteMode.IgnoreSchema);
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> object with which to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000713 RID: 1811 RVA: 0x0001D9A6 File Offset: 0x0001BBA6
		public void WriteXml(TextWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema);
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000714 RID: 1812 RVA: 0x0001D9B0 File Offset: 0x0001BBB0
		public void WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema);
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> to the specified file.</summary>
		/// <param name="fileName">The file name (including the path) to which to write. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000715 RID: 1813 RVA: 0x0001D9BA File Offset: 0x0001BBBA
		public void WriteXml(string fileName)
		{
			this.WriteXml(fileName, XmlWriteMode.IgnoreSchema);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object used to write to a file. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000716 RID: 1814 RVA: 0x0001D9C4 File Offset: 0x0001BBC4
		public void WriteXml(Stream stream, XmlWriteMode mode)
		{
			if (stream != null)
			{
				this.WriteXml(new XmlTextWriter(stream, null)
				{
					Formatting = Formatting.Indented
				}, mode);
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> object used to write the document. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000717 RID: 1815 RVA: 0x0001D9EC File Offset: 0x0001BBEC
		public void WriteXml(TextWriter writer, XmlWriteMode mode)
		{
			if (writer != null)
			{
				this.WriteXml(new XmlTextWriter(writer)
				{
					Formatting = Formatting.Indented
				}, mode);
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000718 RID: 1816 RVA: 0x0001DA14 File Offset: 0x0001BC14
		public void WriteXml(XmlWriter writer, XmlWriteMode mode)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, XmlWriteMode>("<ds.DataSet.WriteXml|API> {0}, mode={1}", this.ObjectID, mode);
			try
			{
				if (writer != null)
				{
					if (mode == XmlWriteMode.DiffGram)
					{
						new NewDiffgramGen(this).Save(writer);
					}
					else
					{
						new XmlDataTreeWriter(this).Save(writer, mode == XmlWriteMode.WriteSchema);
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> to the specified file using the specified <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="fileName">The file name (including the path) to which to write. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000719 RID: 1817 RVA: 0x0001DA7C File Offset: 0x0001BC7C
		public void WriteXml(string fileName, XmlWriteMode mode)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, string, int>("<ds.DataSet.WriteXml|API> {0}, fileName='{1}', mode={2}", this.ObjectID, fileName, (int)mode);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			try
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument(true);
				if (xmlTextWriter != null)
				{
					if (mode == XmlWriteMode.DiffGram)
					{
						new NewDiffgramGen(this).Save(xmlTextWriter);
					}
					else
					{
						new XmlDataTreeWriter(this).Save(xmlTextWriter, mode == XmlWriteMode.WriteSchema);
					}
				}
				xmlTextWriter.WriteEndDocument();
			}
			finally
			{
				xmlTextWriter.Close();
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001DB08 File Offset: 0x0001BD08
		internal DataRelationCollection GetParentRelations(DataTable table)
		{
			return table.ParentRelations;
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema into the current DataSet.</summary>
		/// <param name="dataSet">The DataSet whose data and schema will be merged. </param>
		/// <exception cref="T:System.Data.ConstraintException">One or more constraints cannot be enabled. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600071B RID: 1819 RVA: 0x0001DB10 File Offset: 0x0001BD10
		public void Merge(DataSet dataSet)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataSet.Merge|API> {0}, dataSet={1}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0);
			try
			{
				this.Merge(dataSet, false, MissingSchemaAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema into the current DataSet, preserving or discarding any changes in this DataSet according to the given argument.</summary>
		/// <param name="dataSet">The DataSet whose data and schema will be merged. </param>
		/// <param name="preserveChanges">true to preserve changes in the current DataSet; otherwise false. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600071C RID: 1820 RVA: 0x0001DB68 File Offset: 0x0001BD68
		public void Merge(DataSet dataSet, bool preserveChanges)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, bool>("<ds.DataSet.Merge|API> {0}, dataSet={1}, preserveChanges={2}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0, preserveChanges);
			try
			{
				this.Merge(dataSet, preserveChanges, MissingSchemaAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema with the current DataSet, preserving or discarding changes in the current DataSet and handling an incompatible schema according to the given arguments.</summary>
		/// <param name="dataSet">The DataSet whose data and schema will be merged. </param>
		/// <param name="preserveChanges">true to preserve changes in the current DataSet; otherwise false. </param>
		/// <param name="missingSchemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600071D RID: 1821 RVA: 0x0001DBC0 File Offset: 0x0001BDC0
		public void Merge(DataSet dataSet, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, bool, MissingSchemaAction>("<ds.DataSet.Merge|API> {0}, dataSet={1}, preserveChanges={2}, missingSchemaAction={3}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0, preserveChanges, missingSchemaAction);
			try
			{
				if (dataSet == null)
				{
					throw ExceptionBuilder.ArgumentNull("dataSet");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				new Merger(this, preserveChanges, missingSchemaAction).MergeDataSet(dataSet);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataTable" /> and its schema into the current <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> whose data and schema will be merged. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="table" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600071E RID: 1822 RVA: 0x0001DC38 File Offset: 0x0001BE38
		public void Merge(DataTable table)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataSet.Merge|API> {0}, table={1}", this.ObjectID, (table != null) ? table.ObjectID : 0);
			try
			{
				this.Merge(table, false, MissingSchemaAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataTable" /> and its schema into the current DataSet, preserving or discarding changes in the DataSet and handling an incompatible schema according to the given arguments.</summary>
		/// <param name="table">The DataTable whose data and schema will be merged. </param>
		/// <param name="preserveChanges">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <param name="missingSchemaAction">true to preserve changes in the DataSet; otherwise false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600071F RID: 1823 RVA: 0x0001DC90 File Offset: 0x0001BE90
		public void Merge(DataTable table, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, bool, MissingSchemaAction>("<ds.DataSet.Merge|API> {0}, table={1}, preserveChanges={2}, missingSchemaAction={3}", this.ObjectID, (table != null) ? table.ObjectID : 0, preserveChanges, missingSchemaAction);
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

		/// <summary>Merges an array of <see cref="T:System.Data.DataRow" /> objects into the current <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="rows">The array of DataRow objects to be merged into the DataSet. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000720 RID: 1824 RVA: 0x0001DD08 File Offset: 0x0001BF08
		public void Merge(DataRow[] rows)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Merge|API> {0}, rows", this.ObjectID);
			try
			{
				this.Merge(rows, false, MissingSchemaAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Merges an array of <see cref="T:System.Data.DataRow" /> objects into the current <see cref="T:System.Data.DataSet" />, preserving or discarding changes in the DataSet and handling an incompatible schema according to the given arguments.</summary>
		/// <param name="rows">The array of <see cref="T:System.Data.DataRow" /> objects to be merged into the DataSet. </param>
		/// <param name="preserveChanges">true to preserve changes in the DataSet; otherwise false. </param>
		/// <param name="missingSchemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000721 RID: 1825 RVA: 0x0001DD54 File Offset: 0x0001BF54
		public void Merge(DataRow[] rows, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, bool, MissingSchemaAction>("<ds.DataSet.Merge|API> {0}, preserveChanges={1}, missingSchemaAction={2}", this.ObjectID, preserveChanges, missingSchemaAction);
			try
			{
				if (rows == null)
				{
					throw ExceptionBuilder.ArgumentNull("rows");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				new Merger(this, preserveChanges, missingSchemaAction).MergeRows(rows);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Raises the <see cref="M:System.Data.DataSet.OnPropertyChanging(System.ComponentModel.PropertyChangedEventArgs)" /> event.</summary>
		/// <param name="pcevent">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06000722 RID: 1826 RVA: 0x0001DDC0 File Offset: 0x0001BFC0
		protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			PropertyChangedEventHandler propertyChanging = this.PropertyChanging;
			if (propertyChanging == null)
			{
				return;
			}
			propertyChanging(this, pcevent);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001DDD4 File Offset: 0x0001BFD4
		internal void OnMergeFailed(MergeFailedEventArgs mfevent)
		{
			if (this.MergeFailed != null)
			{
				this.MergeFailed(this, mfevent);
				return;
			}
			throw ExceptionBuilder.MergeFailed(mfevent.Conflict);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001DDF7 File Offset: 0x0001BFF7
		internal void RaiseMergeFailed(DataTable table, string conflict, MissingSchemaAction missingSchemaAction)
		{
			if (MissingSchemaAction.Error == missingSchemaAction)
			{
				throw ExceptionBuilder.MergeFailed(conflict);
			}
			this.OnMergeFailed(new MergeFailedEventArgs(table, conflict));
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001DE11 File Offset: 0x0001C011
		internal void OnDataRowCreated(DataRow row)
		{
			DataRowCreatedEventHandler dataRowCreated = this.DataRowCreated;
			if (dataRowCreated == null)
			{
				return;
			}
			dataRowCreated(this, row);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001DE25 File Offset: 0x0001C025
		internal void OnClearFunctionCalled(DataTable table)
		{
			DataSetClearEventhandler clearFunctionCalled = this.ClearFunctionCalled;
			if (clearFunctionCalled == null)
			{
				return;
			}
			clearFunctionCalled(this, table);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001DE39 File Offset: 0x0001C039
		private void OnInitialized()
		{
			EventHandler initialized = this.Initialized;
			if (initialized == null)
			{
				return;
			}
			initialized(this, EventArgs.Empty);
		}

		/// <summary>Occurs when a <see cref="T:System.Data.DataTable" /> is removed from a <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> being removed. </param>
		// Token: 0x06000728 RID: 1832 RVA: 0x00005E03 File Offset: 0x00004003
		protected internal virtual void OnRemoveTable(DataTable table)
		{
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001DE54 File Offset: 0x0001C054
		internal void OnRemovedTable(DataTable table)
		{
			DataViewManager defaultViewManager = this._defaultViewManager;
			if (defaultViewManager != null)
			{
				defaultViewManager.DataViewSettings.Remove(table);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Data.DataRelation" /> object is removed from a <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> being removed. </param>
		// Token: 0x0600072A RID: 1834 RVA: 0x00005E03 File Offset: 0x00004003
		protected virtual void OnRemoveRelation(DataRelation relation)
		{
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001DE77 File Offset: 0x0001C077
		internal void OnRemoveRelationHack(DataRelation relation)
		{
			this.OnRemoveRelation(relation);
		}

		/// <summary>Sends a notification that the specified <see cref="T:System.Data.DataSet" /> property is about to change.</summary>
		/// <param name="name">The name of the property that is about to change. </param>
		// Token: 0x0600072C RID: 1836 RVA: 0x0001DE80 File Offset: 0x0001C080
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001DE8E File Offset: 0x0001C08E
		internal DataTable[] TopLevelTables()
		{
			return this.TopLevelTables(false);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001DE98 File Offset: 0x0001C098
		internal DataTable[] TopLevelTables(bool forSchema)
		{
			List<DataTable> list = new List<DataTable>();
			if (forSchema)
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					DataTable dataTable = this.Tables[i];
					if (dataTable.NestedParentsCount > 1 || dataTable.SelfNested)
					{
						list.Add(dataTable);
					}
				}
			}
			for (int j = 0; j < this.Tables.Count; j++)
			{
				DataTable dataTable2 = this.Tables[j];
				if (dataTable2.NestedParentsCount == 0 && !list.Contains(dataTable2))
				{
					list.Add(dataTable2);
				}
			}
			if (list.Count != 0)
			{
				return list.ToArray();
			}
			return Array.Empty<DataTable>();
		}

		/// <summary>Rolls back all the changes made to the <see cref="T:System.Data.DataSet" /> since it was created, or since the last time <see cref="M:System.Data.DataSet.AcceptChanges" /> was called.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600072F RID: 1839 RVA: 0x0001DF3C File Offset: 0x0001C13C
		public virtual void RejectChanges()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.RejectChanges|API> {0}", this.ObjectID);
			try
			{
				bool enforceConstraints = this.EnforceConstraints;
				this.EnforceConstraints = false;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].RejectChanges();
				}
				this.EnforceConstraints = enforceConstraints;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Clears all tables and removes all relations, foreign constraints, and tables from the <see cref="T:System.Data.DataSet" />. Subclasses should override <see cref="M:System.Data.DataSet.Reset" /> to restore a <see cref="T:System.Data.DataSet" /> to its original state.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000730 RID: 1840 RVA: 0x0001DFBC File Offset: 0x0001C1BC
		public virtual void Reset()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Reset|API> {0}", this.ObjectID);
			try
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					ConstraintCollection constraints = this.Tables[i].Constraints;
					int j = 0;
					while (j < constraints.Count)
					{
						if (constraints[j] is ForeignKeyConstraint)
						{
							constraints.Remove(constraints[j]);
						}
						else
						{
							j++;
						}
					}
				}
				this.Clear();
				this.Relations.Clear();
				this.Tables.Clear();
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001E070 File Offset: 0x0001C270
		internal bool ValidateCaseConstraint()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.ValidateCaseConstraint|INFO> {0}", this.ObjectID);
			bool flag;
			try
			{
				for (int i = 0; i < this.Relations.Count; i++)
				{
					DataRelation dataRelation = this.Relations[i];
					if (dataRelation.ChildTable.CaseSensitive != dataRelation.ParentTable.CaseSensitive)
					{
						return false;
					}
				}
				for (int j = 0; j < this.Tables.Count; j++)
				{
					ConstraintCollection constraints = this.Tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (constraints[k] is ForeignKeyConstraint)
						{
							ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraints[k];
							if (foreignKeyConstraint.Table.CaseSensitive != foreignKeyConstraint.RelatedTable.CaseSensitive)
							{
								return false;
							}
						}
					}
				}
				flag = true;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return flag;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001E180 File Offset: 0x0001C380
		internal bool ValidateLocaleConstraint()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.ValidateLocaleConstraint|INFO> {0}", this.ObjectID);
			bool flag;
			try
			{
				for (int i = 0; i < this.Relations.Count; i++)
				{
					DataRelation dataRelation = this.Relations[i];
					if (dataRelation.ChildTable.Locale.LCID != dataRelation.ParentTable.Locale.LCID)
					{
						return false;
					}
				}
				for (int j = 0; j < this.Tables.Count; j++)
				{
					ConstraintCollection constraints = this.Tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (constraints[k] is ForeignKeyConstraint)
						{
							ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraints[k];
							if (foreignKeyConstraint.Table.Locale.LCID != foreignKeyConstraint.RelatedTable.Locale.LCID)
							{
								return false;
							}
						}
					}
				}
				flag = true;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return flag;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001E2A8 File Offset: 0x0001C4A8
		internal DataTable FindTable(DataTable baseTable, PropertyDescriptor[] props, int propStart)
		{
			if (props.Length < propStart + 1)
			{
				return baseTable;
			}
			PropertyDescriptor propertyDescriptor = props[propStart];
			if (baseTable == null)
			{
				if (propertyDescriptor is DataTablePropertyDescriptor)
				{
					return this.FindTable(((DataTablePropertyDescriptor)propertyDescriptor).Table, props, propStart + 1);
				}
				return null;
			}
			else
			{
				if (propertyDescriptor is DataRelationPropertyDescriptor)
				{
					return this.FindTable(((DataRelationPropertyDescriptor)propertyDescriptor).Relation.ChildTable, props, propStart + 1);
				}
				return null;
			}
		}

		/// <summary>Ignores attributes and returns an empty DataSet.</summary>
		/// <param name="reader">The specified XML reader. </param>
		// Token: 0x06000734 RID: 1844 RVA: 0x0001E30C File Offset: 0x0001C50C
		protected virtual void ReadXmlSerializable(XmlReader reader)
		{
			this._useDataSetSchemaOnly = false;
			this._udtIsWrapped = false;
			if (reader.HasAttributes)
			{
				if (reader.MoveToAttribute("xsi:nil") && string.Equals(reader.GetAttribute("xsi:nil"), "true", StringComparison.Ordinal))
				{
					this.MoveToElement(reader, 1);
					return;
				}
				if (reader.MoveToAttribute("msdata:UseDataSetSchemaOnly"))
				{
					string attribute = reader.GetAttribute("msdata:UseDataSetSchemaOnly");
					if (string.Equals(attribute, "true", StringComparison.Ordinal) || string.Equals(attribute, "1", StringComparison.Ordinal))
					{
						this._useDataSetSchemaOnly = true;
					}
					else if (!string.Equals(attribute, "false", StringComparison.Ordinal) && !string.Equals(attribute, "0", StringComparison.Ordinal))
					{
						throw ExceptionBuilder.InvalidAttributeValue("UseDataSetSchemaOnly", attribute);
					}
				}
				if (reader.MoveToAttribute("msdata:UDTColumnValueWrapped"))
				{
					string attribute2 = reader.GetAttribute("msdata:UDTColumnValueWrapped");
					if (string.Equals(attribute2, "true", StringComparison.Ordinal) || string.Equals(attribute2, "1", StringComparison.Ordinal))
					{
						this._udtIsWrapped = true;
					}
					else if (!string.Equals(attribute2, "false", StringComparison.Ordinal) && !string.Equals(attribute2, "0", StringComparison.Ordinal))
					{
						throw ExceptionBuilder.InvalidAttributeValue("UDTColumnValueWrapped", attribute2);
					}
				}
			}
			this.ReadXml(reader, XmlReadMode.DiffGram, true);
		}

		/// <summary>Returns a serializable <see cref="T:System.Xml.Schema.XMLSchema" /> instance.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XMLSchema" /> instance.</returns>
		// Token: 0x06000735 RID: 1845 RVA: 0x00004526 File Offset: 0x00002726
		protected virtual XmlSchema GetSchemaSerializable()
		{
			return null;
		}

		/// <summary>Gets a copy of <see cref="T:System.Xml.Schema.XmlSchemaSet" /> for the DataSet.</summary>
		/// <returns>A copy of <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">The specified schema set. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000736 RID: 1846 RVA: 0x0001E438 File Offset: 0x0001C638
		public static XmlSchemaComplexType GetDataSetSchema(XmlSchemaSet schemaSet)
		{
			if (DataSet.s_schemaTypeForWSDL == null)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				xmlSchemaSequence.MaxOccurs = decimal.MaxValue;
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				DataSet.s_schemaTypeForWSDL = xmlSchemaComplexType;
			}
			return DataSet.s_schemaTypeForWSDL;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000061D5 File Offset: 0x000043D5
		private static bool PublishLegacyWSDL()
		{
			return false;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.GetSchema" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.GetSchema" />.</returns>
		// Token: 0x06000738 RID: 1848 RVA: 0x0001E4DC File Offset: 0x0001C6DC
		XmlSchema IXmlSerializable.GetSchema()
		{
			if (base.GetType() == typeof(DataSet))
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
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" />.</param>
		// Token: 0x06000739 RID: 1849 RVA: 0x0001E534 File Offset: 0x0001C734
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			bool flag = true;
			XmlTextReader xmlTextReader = null;
			IXmlTextParser xmlTextParser = reader as IXmlTextParser;
			if (xmlTextParser != null)
			{
				flag = xmlTextParser.Normalized;
				xmlTextParser.Normalized = false;
			}
			else
			{
				xmlTextReader = reader as XmlTextReader;
				if (xmlTextReader != null)
				{
					flag = xmlTextReader.Normalization;
					xmlTextReader.Normalization = false;
				}
			}
			this.ReadXmlSerializable(reader);
			if (xmlTextParser != null)
			{
				xmlTextParser.Normalized = flag;
				return;
			}
			if (xmlTextReader != null)
			{
				xmlTextReader.Normalization = flag;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" />.</param>
		// Token: 0x0600073A RID: 1850 RVA: 0x0001E593 File Offset: 0x0001C793
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.WebService, null);
			this.WriteXml(writer, XmlWriteMode.DiffGram);
		}

		/// <summary>Fills a <see cref="T:System.Data.DataSet" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />, using an array of <see cref="T:System.Data.DataTable" /> instances to supply the schema and namespace information.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets.</param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> instances within the <see cref="T:System.Data.DataSet" /> will be combined with incoming rows that share the same primary key. </param>
		/// <param name="errorHandler">A <see cref="T:System.Data.FillErrorEventHandler" /> delegate to call when an error occurs while loading data.</param>
		/// <param name="tables">An array of <see cref="T:System.Data.DataTable" /> instances, from which the <see cref="M:System.Data.DataSet.Load(System.Data.IDataReader,System.Data.LoadOption,System.Data.FillErrorEventHandler,System.Data.DataTable[])" /> method retrieves name and namespace information.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600073B RID: 1851 RVA: 0x0001E5A8 File Offset: 0x0001C7A8
		public virtual void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler errorHandler, params DataTable[] tables)
		{
			long num = DataCommonEventSource.Log.EnterScope<LoadOption>("<ds.DataSet.Load|API> reader, loadOption={0}", loadOption);
			try
			{
				foreach (DataTable dataTable in tables)
				{
					ADP.CheckArgumentNull(dataTable, "tables");
					if (dataTable.DataSet != this)
					{
						throw ExceptionBuilder.TableNotInTheDataSet(dataTable.TableName);
					}
				}
				LoadAdapter loadAdapter = new LoadAdapter();
				loadAdapter.FillLoadOption = loadOption;
				loadAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				if (errorHandler != null)
				{
					loadAdapter.FillError += errorHandler;
				}
				loadAdapter.FillFromReader(tables, reader, 0, 0);
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

		/// <summary>Fills a <see cref="T:System.Data.DataSet" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />, using an array of <see cref="T:System.Data.DataTable" /> instances to supply the schema and namespace information.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets. </param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> instances within the <see cref="T:System.Data.DataSet" /> will be combined with incoming rows that share the same primary key. </param>
		/// <param name="tables">An array of <see cref="T:System.Data.DataTable" /> instances, from which the <see cref="M:System.Data.DataSet.Load(System.Data.IDataReader,System.Data.LoadOption,System.Data.DataTable[])" /> method retrieves name and namespace information. Each of these tables must be a member of the <see cref="T:System.Data.DataTableCollection" /> contained by this <see cref="T:System.Data.DataSet" />.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600073C RID: 1852 RVA: 0x0001E660 File Offset: 0x0001C860
		public void Load(IDataReader reader, LoadOption loadOption, params DataTable[] tables)
		{
			this.Load(reader, loadOption, null, tables);
		}

		/// <summary>Fills a <see cref="T:System.Data.DataSet" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />, using an array of strings to supply the names for the tables within the DataSet.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets.</param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> instances within the DataSet will be combined with incoming rows that share the same primary key. </param>
		/// <param name="tables">An array of strings, from which the Load method retrieves table name information.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600073D RID: 1853 RVA: 0x0001E66C File Offset: 0x0001C86C
		public void Load(IDataReader reader, LoadOption loadOption, params string[] tables)
		{
			ADP.CheckArgumentNull(tables, "tables");
			DataTable[] array = new DataTable[tables.Length];
			for (int i = 0; i < tables.Length; i++)
			{
				DataTable dataTable = this.Tables[tables[i]];
				if (dataTable == null)
				{
					dataTable = new DataTable(tables[i]);
					this.Tables.Add(dataTable);
				}
				array[i] = dataTable;
			}
			this.Load(reader, loadOption, null, array);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTableReader" /> with one result set per <see cref="T:System.Data.DataTable" />, in the same sequence as the tables appear in the <see cref="P:System.Data.DataSet.Tables" /> collection.</summary>
		/// <returns>A <see cref="T:System.Data.DataTableReader" /> containing one or more result sets, corresponding to the <see cref="T:System.Data.DataTable" /> instances contained within the source <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x0600073E RID: 1854 RVA: 0x0001E6D0 File Offset: 0x0001C8D0
		public DataTableReader CreateDataReader()
		{
			if (this.Tables.Count == 0)
			{
				throw ExceptionBuilder.CannotCreateDataReaderOnEmptyDataSet();
			}
			DataTable[] array = new DataTable[this.Tables.Count];
			for (int i = 0; i < this.Tables.Count; i++)
			{
				array[i] = this.Tables[i];
			}
			return this.CreateDataReader(array);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTableReader" /> with one result set per <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTableReader" /> containing one or more result sets, corresponding to the <see cref="T:System.Data.DataTable" /> instances contained within the source <see cref="T:System.Data.DataSet" />. The returned result sets are in the order specified by the <paramref name="dataTables" /> parameter.</returns>
		/// <param name="dataTables">An array of DataTables providing the order of the result sets to be returned in the <see cref="T:System.Data.DataTableReader" />.</param>
		// Token: 0x0600073F RID: 1855 RVA: 0x0001E730 File Offset: 0x0001C930
		public DataTableReader CreateDataReader(params DataTable[] dataTables)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.GetDataReader|API> {0}", this.ObjectID);
			DataTableReader dataTableReader;
			try
			{
				if (dataTables.Length == 0)
				{
					throw ExceptionBuilder.DataTableReaderArgumentIsEmpty();
				}
				for (int i = 0; i < dataTables.Length; i++)
				{
					if (dataTables[i] == null)
					{
						throw ExceptionBuilder.ArgumentContainsNullValue();
					}
				}
				dataTableReader = new DataTableReader(dataTables);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataTableReader;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001E79C File Offset: 0x0001C99C
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0001E7A4 File Offset: 0x0001C9A4
		internal string MainTableName
		{
			get
			{
				return this._mainTableName;
			}
			set
			{
				this._mainTableName = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001E7AD File Offset: 0x0001C9AD
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x040005AD RID: 1453
		private const string KEY_XMLSCHEMA = "XmlSchema";

		// Token: 0x040005AE RID: 1454
		private const string KEY_XMLDIFFGRAM = "XmlDiffGram";

		// Token: 0x040005AF RID: 1455
		private DataViewManager _defaultViewManager;

		// Token: 0x040005B0 RID: 1456
		private readonly DataTableCollection _tableCollection;

		// Token: 0x040005B1 RID: 1457
		private readonly DataRelationCollection _relationCollection;

		// Token: 0x040005B2 RID: 1458
		internal PropertyCollection _extendedProperties;

		// Token: 0x040005B3 RID: 1459
		private string _dataSetName = "NewDataSet";

		// Token: 0x040005B4 RID: 1460
		private string _datasetPrefix = string.Empty;

		// Token: 0x040005B5 RID: 1461
		internal string _namespaceURI = string.Empty;

		// Token: 0x040005B6 RID: 1462
		private bool _enforceConstraints = true;

		// Token: 0x040005B7 RID: 1463
		private bool _caseSensitive;

		// Token: 0x040005B8 RID: 1464
		private CultureInfo _culture;

		// Token: 0x040005B9 RID: 1465
		private bool _cultureUserSet;

		// Token: 0x040005BA RID: 1466
		internal bool _fInReadXml;

		// Token: 0x040005BB RID: 1467
		internal bool _fInLoadDiffgram;

		// Token: 0x040005BC RID: 1468
		internal bool _fTopLevelTable;

		// Token: 0x040005BD RID: 1469
		internal bool _fInitInProgress;

		// Token: 0x040005BE RID: 1470
		internal bool _fEnableCascading = true;

		// Token: 0x040005BF RID: 1471
		internal bool _fIsSchemaLoading;

		// Token: 0x040005C0 RID: 1472
		private bool _fBoundToDocument;

		// Token: 0x040005C1 RID: 1473
		internal string _mainTableName = string.Empty;

		// Token: 0x040005C2 RID: 1474
		private SerializationFormat _remotingFormat;

		// Token: 0x040005C3 RID: 1475
		private object _defaultViewManagerLock = new object();

		// Token: 0x040005C4 RID: 1476
		private static int s_objectTypeCount;

		// Token: 0x040005C5 RID: 1477
		private readonly int _objectID = Interlocked.Increment(ref DataSet.s_objectTypeCount);

		// Token: 0x040005C6 RID: 1478
		private static XmlSchemaComplexType s_schemaTypeForWSDL;

		// Token: 0x040005C7 RID: 1479
		internal bool _useDataSetSchemaOnly;

		// Token: 0x040005C8 RID: 1480
		internal bool _udtIsWrapped;

		// Token: 0x02000089 RID: 137
		private struct TableChanges
		{
			// Token: 0x06000744 RID: 1860 RVA: 0x0001E7B5 File Offset: 0x0001C9B5
			internal TableChanges(int rowCount)
			{
				this._rowChanges = new BitArray(rowCount);
				this.HasChanges = 0;
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001E7CA File Offset: 0x0001C9CA
			// (set) Token: 0x06000746 RID: 1862 RVA: 0x0001E7D2 File Offset: 0x0001C9D2
			internal int HasChanges { get; set; }

			// Token: 0x17000152 RID: 338
			internal bool this[int index]
			{
				get
				{
					return this._rowChanges[index];
				}
				set
				{
					this._rowChanges[index] = value;
					int hasChanges = this.HasChanges;
					this.HasChanges = hasChanges + 1;
				}
			}

			// Token: 0x040005CE RID: 1486
			private BitArray _rowChanges;
		}
	}
}
