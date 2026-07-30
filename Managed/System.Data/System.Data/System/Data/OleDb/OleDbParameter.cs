using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents a parameter to an <see cref="T:System.Data.OleDb.OleDbCommand" /> and optionally its mapping to a <see cref="T:System.Data.DataSet" /> column. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000132 RID: 306
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbParameter : DbParameter, IDataParameter, IDbDataParameter, ICloneable
	{
		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="F:System.Data.DbType.String" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property was not set to a valid <see cref="T:System.Data.DbType" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x00005E03 File Offset: 0x00004003
		public override DbType DbType
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return-value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x00005E03 File Offset: 0x00004003
		public override ParameterDirection Direction
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
		/// <returns>true if null values are accepted; otherwise false. The default is false.</returns>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x00005E03 File Offset: 0x00004003
		public override bool IsNullable
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x00005E03 File Offset: 0x00004003
		public int Offset
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbType" /> of the parameter.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbType" /> of the parameter. The default is <see cref="F:System.Data.OleDb.OleDbType.VarWChar" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x00005E03 File Offset: 0x00004003
		public OleDbType OleDbType
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.OleDb.OleDbParameter" />. The default is an empty string ("").</returns>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x00005E03 File Offset: 0x00004003
		public override string ParameterName
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the maximum number of digits used to represent the <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> property. The default value is 0, which indicates that the data provider sets the precision for <see cref="P:System.Data.OleDb.OleDbParameter.Value" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x00005E03 File Offset: 0x00004003
		public new byte Precision
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x00005E03 File Offset: 0x00004003
		public new byte Scale
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the maximum size, in bytes, of the data within the column.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x00005E03 File Offset: 0x00004003
		public override int Size
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.OleDb.OleDbParameter.Value" />.</summary>
		/// <returns>The name of the source column mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x00005E03 File Offset: 0x00004003
		public override string SourceColumn
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This allows <see cref="T:System.Data.Common.DbCommandBuilder" /> to correctly generate Update statements for nullable columns.</summary>
		/// <returns>true if the source column is nullable; false if it is not.</returns>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x00005E03 File Offset: 0x00004003
		public override bool SourceColumnNullMapping
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.OleDb.OleDbParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the <see cref="T:System.Data.DataRowVersion" /> values.</exception>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x00005E03 File Offset: 0x00004003
		public override DataRowVersion SourceVersion
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x00005E03 File Offset: 0x00004003
		public override object Value
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class.</summary>
		// Token: 0x06000FBB RID: 4027 RVA: 0x00050F57 File Offset: 0x0004F157
		public OleDbParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name and data type.</summary>
		/// <param name="name">The name of the parameter to map. </param>
		/// <param name="dataType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000FBC RID: 4028 RVA: 0x00050F5F File Offset: 0x0004F15F
		public OleDbParameter(string name, OleDbType dataType)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, and length.</summary>
		/// <param name="name">The name of the parameter to map. </param>
		/// <param name="dataType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000FBD RID: 4029 RVA: 0x00050F5F File Offset: 0x0004F15F
		public OleDbParameter(string name, OleDbType dataType, int size)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values. </param>
		/// <param name="isNullable">true if the value of the field can be null; otherwise false. </param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved. </param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved. </param>
		/// <param name="srcColumn">The name of the source column. </param>
		/// <param name="srcVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.OleDb.OleDbParameter" />. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000FBE RID: 4030 RVA: 0x00050F5F File Offset: 0x0004F15F
		public OleDbParameter(string parameterName, OleDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values. </param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		/// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values.</param>
		/// <param name="sourceColumnNullMapping">true if the source column is nullable; false if it is not.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.OleDb.OleDbParameter" />. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000FBF RID: 4031 RVA: 0x00050F5F File Offset: 0x0004F15F
		public OleDbParameter(string parameterName, OleDbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, length, and source column name.</summary>
		/// <param name="name">The name of the parameter to map. </param>
		/// <param name="dataType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="srcColumn">The name of the source column. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000FC0 RID: 4032 RVA: 0x00050F5F File Offset: 0x0004F15F
		public OleDbParameter(string name, OleDbType dataType, int size, string srcColumn)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name and the value of the new <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <param name="name">The name of the parameter to map. </param>
		/// <param name="value">The value of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object. </param>
		// Token: 0x06000FC1 RID: 4033 RVA: 0x00050F5F File Offset: 0x0004F15F
		public OleDbParameter(string name, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000FC2 RID: 4034 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void ResetDbType()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets a string that contains the <see cref="P:System.Data.OleDb.OleDbParameter.ParameterName" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.OleDb.OleDbParameter.ParameterName" />.</returns>
		// Token: 0x06000FC3 RID: 4035 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override string ToString()
		{
			throw ADP.OleDb();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06000FC4 RID: 4036 RVA: 0x00050D50 File Offset: 0x0004EF50
		object ICloneable.Clone()
		{
			throw ADP.OleDb();
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000FC5 RID: 4037 RVA: 0x00050D50 File Offset: 0x0004EF50
		public void ResetOleDbType()
		{
			throw ADP.OleDb();
		}
	}
}
