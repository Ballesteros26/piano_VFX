using System;
using System.Data;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Represents a column of a table or view in the data store accessed through a data connection. This class cannot be inherited.</summary>
	// Token: 0x02000167 RID: 359
	public sealed class DesignerDataColumn
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataColumn" /> class with the specified name and data type. </summary>
		/// <param name="name">The name identifying the column in the data store.</param>
		/// <param name="dataType">One of the <see cref="T:System.Data.DbType" /> values.</param>
		// Token: 0x06000AC8 RID: 2760 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		public DesignerDataColumn(string name, DbType dataType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataColumn" /> class with the specified name, data type, and default value. </summary>
		/// <param name="name">The name identifying the column in the data store.</param>
		/// <param name="dataType">One of the <see cref="T:System.Data.DbType" /> values.</param>
		/// <param name="defaultValue">The default value of the column.</param>
		// Token: 0x06000AC9 RID: 2761 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		public DesignerDataColumn(string name, DbType dataType, object defaultValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataColumn" /> class with the specified values. </summary>
		/// <param name="name">The name identifying the column in the data store.</param>
		/// <param name="dataType">One of the <see cref="T:System.Data.DbType" /> values.</param>
		/// <param name="defaultValue">The default value of the column</param>
		/// <param name="identity">true if the field is the identity field of the data row; otherwise, false.</param>
		/// <param name="nullable">true if the field can be null in the data store; otherwise, false.</param>
		/// <param name="primaryKey">true if the field is the primary key of the data row; otherwise, false.</param>
		/// <param name="precision">The maximum number of digits used by a numeric data field.</param>
		/// <param name="scale">The maximum number of digits to the right of the decimal point in a numeric data field.</param>
		/// <param name="length">The length of the data field, in bytes.</param>
		// Token: 0x06000ACA RID: 2762 RVA: 0x00016448 File Offset: 0x00014648
		[MonoTODO]
		public DesignerDataColumn(string name, DbType dataType, object defaultValue, bool identity, bool nullable, bool primaryKey, int precision, int scale, int length)
		{
			this.name = name;
			this.data_type = dataType;
			this.default_value = defaultValue;
			this.identity = identity;
			this.nullable = nullable;
			this.primary_key = primaryKey;
			this.precision = precision;
			this.scale = scale;
			this.length = length;
		}

		/// <summary>Gets the name of the column in the data store.</summary>
		/// <returns>The name of the column in the data store.</returns>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000164A0 File Offset: 0x000146A0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the data type of the data column.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values.</returns>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x000164A8 File Offset: 0x000146A8
		public DbType DataType
		{
			get
			{
				return this.data_type;
			}
		}

		/// <summary>Gets the default value of the data column.</summary>
		/// <returns>The default value of the data column. The default is null.</returns>
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000164B0 File Offset: 0x000146B0
		public object DefaultValue
		{
			get
			{
				return this.default_value;
			}
		}

		/// <summary>Gets a value indicating whether the data column is an identity column for the data row.</summary>
		/// <returns>true of the column is an identity column; otherwise, false.</returns>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x000164B8 File Offset: 0x000146B8
		public bool Identity
		{
			get
			{
				return this.identity;
			}
		}

		/// <summary>Gets a value indicating whether the column can be null in the data store.</summary>
		/// <returns>true if the column can be null in the data store; otherwise, false.</returns>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x000164C0 File Offset: 0x000146C0
		public bool Nullable
		{
			get
			{
				return this.nullable;
			}
		}

		/// <summary>Gets a value indicating whether the column is part of the table's primary key.</summary>
		/// <returns>true if the column is part of the table's primary key; otherwise, false.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x000164C8 File Offset: 0x000146C8
		public bool PrimaryKey
		{
			get
			{
				return this.primary_key;
			}
		}

		/// <summary>Gets the number of digits in a numeric data column.</summary>
		/// <returns>The number of digits in a numeric data column. </returns>
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x000164D0 File Offset: 0x000146D0
		public int Precision
		{
			get
			{
				return this.precision;
			}
		}

		/// <summary>Gets the number of digits to the right of the decimal point in a numeric column.</summary>
		/// <returns>The number of digits to the right of the decimal point in a numeric column.</returns>
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x000164D8 File Offset: 0x000146D8
		public int Scale
		{
			get
			{
				return this.scale;
			}
		}

		/// <summary>Gets the length in bytes of the data column.</summary>
		/// <returns>The length of the data column, in bytes.</returns>
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x000164E0 File Offset: 0x000146E0
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x04000279 RID: 633
		private string name;

		// Token: 0x0400027A RID: 634
		private DbType data_type;

		// Token: 0x0400027B RID: 635
		private object default_value;

		// Token: 0x0400027C RID: 636
		private bool identity;

		// Token: 0x0400027D RID: 637
		private bool nullable;

		// Token: 0x0400027E RID: 638
		private bool primary_key;

		// Token: 0x0400027F RID: 639
		private int precision;

		// Token: 0x04000280 RID: 640
		private int scale;

		// Token: 0x04000281 RID: 641
		private int length;
	}
}
