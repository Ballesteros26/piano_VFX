using System;
using System.Data;

namespace System.Web.UI.Design
{
	/// <summary>Represents the structure, or schema, of a <see cref="T:System.Data.DataTable" />. This class cannot be inherited.</summary>
	// Token: 0x02000068 RID: 104
	public sealed class DataSetViewSchema : IDataSourceViewSchema
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.UI.Design.DataSetViewSchema" /> class using a specified <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Web.UI.Design.DataSetViewSchema" /> instance will describe.</param>
		// Token: 0x0600033C RID: 828 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		public DataSetViewSchema(DataTable dataTable)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the name of the view using its <see cref="P:System.Data.DataTable.TableName" /> property.</summary>
		/// <returns>The name of the view.</returns>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an array representing the child views contained in the current view.</summary>
		/// <returns>null.</returns>
		// Token: 0x0600033E RID: 830 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public IDataSourceViewSchema[] GetChildren()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an array containing information about each data field in the view.</summary>
		/// <returns>An array of <see cref="T:System.Web.UI.Design.IDataSourceFieldSchema" /> objects.</returns>
		// Token: 0x0600033F RID: 831 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public IDataSourceFieldSchema[] GetFields()
		{
			throw new NotImplementedException();
		}
	}
}
