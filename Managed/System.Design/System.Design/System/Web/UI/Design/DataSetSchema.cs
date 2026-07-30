using System;
using System.Data;

namespace System.Web.UI.Design
{
	/// <summary>The <see cref="T:System.Web.UI.Design.DataSetSchema" /> class represents the structure, or schema, of a data source. This class cannot be inherited.</summary>
	// Token: 0x02000067 RID: 103
	public sealed class DataSetSchema : IDataSourceSchema
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.UI.Design.DataSetSchema" /> class using a specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> that the <see cref="T:System.Web.UI.Design.DataSetSchema" /> instance will describe.</param>
		// Token: 0x0600033A RID: 826 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		public DataSetSchema(DataSet dataSet)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an array containing information about each view in the data source.</summary>
		/// <returns>An array of <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" /> objects.</returns>
		// Token: 0x0600033B RID: 827 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public IDataSourceViewSchema[] GetViews()
		{
			throw new NotImplementedException();
		}
	}
}
