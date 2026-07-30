using System;
using System.Collections;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	/// <summary>Exposes the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method, which supports a simple iteration over a collection by a .NET Framework data provider.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000349 RID: 841
	public class DbEnumerator : IEnumerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbEnumerator" /> class using the specified DataReader.</summary>
		/// <param name="reader">The DataReader through which to iterate. </param>
		// Token: 0x060027F2 RID: 10226 RVA: 0x000B0F13 File Offset: 0x000AF113
		public DbEnumerator(IDataReader reader)
		{
			if (reader == null)
			{
				throw ADP.ArgumentNull("reader");
			}
			this._reader = reader;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbEnumerator" /> class using the specified DataReader, and indicates whether to automatically close the DataReader after iterating through its data.</summary>
		/// <param name="reader">The DataReader through which to iterate. </param>
		/// <param name="closeReader">true to automatically close the DataReader after iterating through its data; otherwise, false. </param>
		// Token: 0x060027F3 RID: 10227 RVA: 0x000B0F30 File Offset: 0x000AF130
		public DbEnumerator(IDataReader reader, bool closeReader)
		{
			if (reader == null)
			{
				throw ADP.ArgumentNull("reader");
			}
			this._reader = reader;
			this._closeReader = closeReader;
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000B0F54 File Offset: 0x000AF154
		public DbEnumerator(DbDataReader reader)
			: this(reader)
		{
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000B0F5D File Offset: 0x000AF15D
		public DbEnumerator(DbDataReader reader, bool closeReader)
			: this(reader, closeReader)
		{
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x000B0F67 File Offset: 0x000AF167
		public object Current
		{
			get
			{
				return this._current;
			}
		}

		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060027F7 RID: 10231 RVA: 0x000B0F70 File Offset: 0x000AF170
		public bool MoveNext()
		{
			if (this._schemaInfo == null)
			{
				this.BuildSchemaInfo();
			}
			this._current = null;
			if (this._reader.Read())
			{
				object[] array = new object[this._schemaInfo.Length];
				this._reader.GetValues(array);
				this._current = new DataRecordInternal(this._schemaInfo, array, this._descriptors, this._fieldNameLookup);
				return true;
			}
			if (this._closeReader)
			{
				this._reader.Close();
			}
			return false;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060027F8 RID: 10232 RVA: 0x000621D6 File Offset: 0x000603D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Reset()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000B0FF0 File Offset: 0x000AF1F0
		private void BuildSchemaInfo()
		{
			int fieldCount = this._reader.FieldCount;
			string[] array = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = this._reader.GetName(i);
			}
			ADP.BuildSchemaTableInfoTableNames(array);
			SchemaInfo[] array2 = new SchemaInfo[fieldCount];
			PropertyDescriptor[] array3 = new PropertyDescriptor[this._reader.FieldCount];
			for (int j = 0; j < array2.Length; j++)
			{
				SchemaInfo schemaInfo = default(SchemaInfo);
				schemaInfo.name = this._reader.GetName(j);
				schemaInfo.type = this._reader.GetFieldType(j);
				schemaInfo.typeName = this._reader.GetDataTypeName(j);
				array3[j] = new DbEnumerator.DbColumnDescriptor(j, array[j], schemaInfo.type);
				array2[j] = schemaInfo;
			}
			this._schemaInfo = array2;
			this._fieldNameLookup = new FieldNameLookup(this._reader, -1);
			this._descriptors = new PropertyDescriptorCollection(array3);
		}

		// Token: 0x040018CB RID: 6347
		internal IDataReader _reader;

		// Token: 0x040018CC RID: 6348
		internal DbDataRecord _current;

		// Token: 0x040018CD RID: 6349
		internal SchemaInfo[] _schemaInfo;

		// Token: 0x040018CE RID: 6350
		internal PropertyDescriptorCollection _descriptors;

		// Token: 0x040018CF RID: 6351
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x040018D0 RID: 6352
		private bool _closeReader;

		// Token: 0x0200034A RID: 842
		private sealed class DbColumnDescriptor : PropertyDescriptor
		{
			// Token: 0x060027FA RID: 10234 RVA: 0x000B10EA File Offset: 0x000AF2EA
			internal DbColumnDescriptor(int ordinal, string name, Type type)
				: base(name, null)
			{
				this._ordinal = ordinal;
				this._type = type;
			}

			// Token: 0x170006EB RID: 1771
			// (get) Token: 0x060027FB RID: 10235 RVA: 0x000B1102 File Offset: 0x000AF302
			public override Type ComponentType
			{
				get
				{
					return typeof(IDataRecord);
				}
			}

			// Token: 0x170006EC RID: 1772
			// (get) Token: 0x060027FC RID: 10236 RVA: 0x0000EF2B File Offset: 0x0000D12B
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170006ED RID: 1773
			// (get) Token: 0x060027FD RID: 10237 RVA: 0x000B110E File Offset: 0x000AF30E
			public override Type PropertyType
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x060027FE RID: 10238 RVA: 0x000061D5 File Offset: 0x000043D5
			public override bool CanResetValue(object component)
			{
				return false;
			}

			// Token: 0x060027FF RID: 10239 RVA: 0x000B1116 File Offset: 0x000AF316
			public override object GetValue(object component)
			{
				return ((IDataRecord)component)[this._ordinal];
			}

			// Token: 0x06002800 RID: 10240 RVA: 0x000621D6 File Offset: 0x000603D6
			public override void ResetValue(object component)
			{
				throw ADP.NotSupported();
			}

			// Token: 0x06002801 RID: 10241 RVA: 0x000621D6 File Offset: 0x000603D6
			public override void SetValue(object component, object value)
			{
				throw ADP.NotSupported();
			}

			// Token: 0x06002802 RID: 10242 RVA: 0x000061D5 File Offset: 0x000043D5
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x040018D1 RID: 6353
			private int _ordinal;

			// Token: 0x040018D2 RID: 6354
			private Type _type;
		}
	}
}
