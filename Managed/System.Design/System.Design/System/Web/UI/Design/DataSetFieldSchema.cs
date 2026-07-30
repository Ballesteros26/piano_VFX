using System;
using System.Data;

namespace System.Web.UI.Design
{
	/// <summary>Represents the structure, or schema, of a data field.</summary>
	// Token: 0x02000066 RID: 102
	public sealed class DataSetFieldSchema : IDataSourceFieldSchema
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DataSetFieldSchema" /> class using a specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" /> object that the <see cref="T:System.Web.UI.Design.DataSetFieldSchema" /> object  describes.</param>
		// Token: 0x0600032F RID: 815 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		public DataSetFieldSchema(DataColumn column)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the type of data stored in the data field.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of data the data field contains.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Type DataType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the value of the data field automatically increments for each new row added to the table or view.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Design.DataSetFieldSchema.DataType" /> is numeric and the value of the column increments automatically as new rows are added to the <see cref="T:System.Data.DataTable" />; otherwise, false.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool Identity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.DataColumn" /> is read-only.</summary>
		/// <returns>true if the data field is read-only; otherwise, false.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether values in the data field are required to be unique.</summary>
		/// <returns>true if data in the data field is unique; otherwise, false.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool IsUnique
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating the size of data that can be stored in the data field.</summary>
		/// <returns>The number of bytes the column can store.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public int Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the data field.</summary>
		/// <returns>The name of the data field.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the data field can accept null values.</summary>
		/// <returns>true if the data field can accept null values; otherwise, false.</returns>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool Nullable
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the maximum number of digits used to represent a numerical value in the data field.</summary>
		/// <returns>This property always returns -1.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public int Precision
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the data field is in the primary key for the containing table or view.</summary>
		/// <returns>true if the data field is in the primary key; otherwise, false.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool PrimaryKey
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of decimal places to which numerical values in the data field are resolved.</summary>
		/// <returns>This property always returns -1.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public int Scale
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
