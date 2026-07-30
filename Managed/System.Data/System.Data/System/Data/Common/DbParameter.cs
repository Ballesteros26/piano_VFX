using System;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Represents a parameter to a <see cref="T:System.Data.Common.DbCommand" /> and optionally, its mapping to a <see cref="T:System.Data.DataSet" /> column. For more information on parameters, see Configuring Parameters and Parameter Data Types (ADO.NET).</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200034E RID: 846
	public abstract class DbParameter : MarshalByRefObject, IDbDataParameter, IDataParameter
	{
		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="F:System.Data.DbType.String" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to a valid <see cref="T:System.Data.DbType" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x0600280B RID: 10251
		// (set) Token: 0x0600280C RID: 10252
		[Browsable(false)]
		[RefreshProperties(RefreshProperties.All)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public abstract DbType DbType { get; set; }

		/// <summary>Resets the DbType property to its original settings.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600280D RID: 10253
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public abstract void ResetDbType();

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600280E RID: 10254
		// (set) Token: 0x0600280F RID: 10255
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(ParameterDirection.Input)]
		public abstract ParameterDirection Direction { get; set; }

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
		/// <returns>true if null values are accepted; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06002810 RID: 10256
		// (set) Token: 0x06002811 RID: 10257
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignOnly(true)]
		[Browsable(false)]
		public abstract bool IsNullable { get; set; }

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.Common.DbParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.Common.DbParameter" />. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06002812 RID: 10258
		// (set) Token: 0x06002813 RID: 10259
		[DefaultValue("")]
		public abstract string ParameterName { get; set; }

		/// <summary>Indicates the precision of numeric parameters.</summary>
		/// <returns>The maximum number of digits used to represent the Value property of a data provider Parameter object. The default value is 0, which indicates that a data provider sets the precision for Value.</returns>
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x000061D5 File Offset: 0x000043D5
		// (set) Token: 0x06002815 RID: 10261 RVA: 0x00005E03 File Offset: 0x00004003
		byte IDbDataParameter.Precision
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataParameter.Scale" />.</summary>
		/// <returns>The number of decimal places to which <see cref="T:System.Data.OleDb.OleDbParameter.Value" /> is resolved. The default is 0.</returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x000061D5 File Offset: 0x000043D5
		// (set) Token: 0x06002817 RID: 10263 RVA: 0x00005E03 File Offset: 0x00004003
		byte IDbDataParameter.Scale
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x000B134F File Offset: 0x000AF54F
		// (set) Token: 0x06002819 RID: 10265 RVA: 0x000B1357 File Offset: 0x000AF557
		public virtual byte Precision
		{
			get
			{
				return ((IDbDataParameter)this).Precision;
			}
			set
			{
				((IDbDataParameter)this).Precision = value;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x0600281A RID: 10266 RVA: 0x000B1360 File Offset: 0x000AF560
		// (set) Token: 0x0600281B RID: 10267 RVA: 0x000B1368 File Offset: 0x000AF568
		public virtual byte Scale
		{
			get
			{
				return ((IDbDataParameter)this).Scale;
			}
			set
			{
				((IDbDataParameter)this).Scale = value;
			}
		}

		/// <summary>Gets or sets the maximum size, in bytes, of the data within the column.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x0600281C RID: 10268
		// (set) Token: 0x0600281D RID: 10269
		public abstract int Size { get; set; }

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.Common.DbParameter.Value" />.</summary>
		/// <returns>The name of the source column mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x0600281E RID: 10270
		// (set) Token: 0x0600281F RID: 10271
		[DefaultValue("")]
		public abstract string SourceColumn { get; set; }

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This allows <see cref="T:System.Data.Common.DbCommandBuilder" /> to correctly generate Update statements for nullable columns.</summary>
		/// <returns>true if the source column is nullable; false if it is not.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002820 RID: 10272
		// (set) Token: 0x06002821 RID: 10273
		[RefreshProperties(RefreshProperties.All)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue(false)]
		public abstract bool SourceColumnNullMapping { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.Common.DbParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to one of the <see cref="T:System.Data.DataRowVersion" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x000B1371 File Offset: 0x000AF571
		// (set) Token: 0x06002823 RID: 10275 RVA: 0x00005E03 File Offset: 0x00004003
		[DefaultValue(DataRowVersion.Current)]
		public virtual DataRowVersion SourceVersion
		{
			get
			{
				return DataRowVersion.Default;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002824 RID: 10276
		// (set) Token: 0x06002825 RID: 10277
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.All)]
		public abstract object Value { get; set; }
	}
}
