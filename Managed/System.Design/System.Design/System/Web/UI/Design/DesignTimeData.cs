using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.Design
{
	/// <summary>Provides helper methods that can be used by control designers to generate sample data for data-bound properties at design time. This class cannot be inherited.</summary>
	// Token: 0x0200006D RID: 109
	public sealed class DesignTimeData
	{
		// Token: 0x06000364 RID: 868 RVA: 0x00002352 File Offset: 0x00000552
		private DesignTimeData()
		{
		}

		/// <summary>Creates a <see cref="T:System.Data.DataTable" /> object that contains three columns with names indicating that the columns are connected to a data source.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> object with three columns and no data.</returns>
		// Token: 0x06000365 RID: 869 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static DataTable CreateDummyDataBoundDataTable()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a <see cref="T:System.Data.DataTable" /> object that contains three columns with names that indicate that the columns contain sample data.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> with three columns. These columns can contain data of type string.</returns>
		// Token: 0x06000366 RID: 870 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static DataTable CreateDummyDataTable()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a sample <see cref="T:System.Data.DataTable" /> object with the same schema as the provided data.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object that contains columns with the same names and data types as the provided <paramref name="referenceData" />.</returns>
		/// <param name="referenceData">A data source with the desired schema to use as the format for the sample <see cref="T:System.Data.DataTable" /> object. </param>
		// Token: 0x06000367 RID: 871 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static DataTable CreateSampleDataTable(IEnumerable referenceData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a <see cref="T:System.Data.DataTable" /> object with the same schema as the provided data and optionally containing column names indicating that the data is bound data.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object.</returns>
		/// <param name="referenceData">An <see cref="T:System.Collections.IEnumerable" /> object containing data.</param>
		/// <param name="useDataBoundData">If true, the column names indicate that they contain bound data.</param>
		// Token: 0x06000368 RID: 872 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static DataTable CreateSampleDataTable(IEnumerable referenceData, bool useDataBoundData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a collection of property descriptors for the data fields of the specified data source.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> object that describes the data fields of the specified data source.</returns>
		/// <param name="dataSource">The data source from which to retrieve the data fields. </param>
		// Token: 0x06000369 RID: 873 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static PropertyDescriptorCollection GetDataFields(IEnumerable dataSource)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the specified data member from the specified data source.</summary>
		/// <returns>An object implementing <see cref="T:System.Collections.IEnumerable" /> containing the specified data member from the specified data source, if it exists.</returns>
		/// <param name="dataSource">An <see cref="T:System.ComponentModel.IListSource" /> that contains the data in which to find the member. </param>
		/// <param name="dataMember">The name of the data member to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataSource" /> is null-or-<paramref name="dataMember" /> is null.</exception>
		// Token: 0x0600036A RID: 874 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static IEnumerable GetDataMember(IListSource dataSource, string dataMember)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the names of the data members in the specified data source.</summary>
		/// <returns>An array of type String that contains the names of the data members in the specified data source.</returns>
		/// <param name="dataSource">The data source from which to retrieve the names of the members. </param>
		// Token: 0x0600036B RID: 875 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static string[] GetDataMembers(object dataSource)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds the specified number of sample rows to the specified data table.</summary>
		/// <returns>An object implementing <see cref="T:System.Collections.IEnumerable" /> containing sample data for use at design time.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> object to which the sample rows are added. </param>
		/// <param name="minimumRows">The minimum number of rows to add. </param>
		// Token: 0x0600036C RID: 876 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static IEnumerable GetDesignTimeDataSource(DataTable dataTable, int minimumRows)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a data source selected by name in the design host, represented by the specified component's site property and identified by the specified data source name.</summary>
		/// <returns>An object implementing either <see cref="T:System.ComponentModel.IListSource" /> or <see cref="T:System.Collections.IEnumerable" /> representing the selected data source, or null if the data source or the designer host could not be accessed.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> object that contains the data source. </param>
		/// <param name="dataSource">The name of the data source to retrieve. </param>
		// Token: 0x0600036D RID: 877 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static object GetSelectedDataSource(IComponent component, string dataSource)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a data source selected by name in the design host, represented by the specified component's site property and identified by the specified data-source name and member name.</summary>
		/// <returns>An object implementing <see cref="T:System.Collections.IEnumerable" /> containing the data member, or null if the data source, member, or component's site could not be accessed.</returns>
		/// <param name="component">The object implementing <see cref="T:System.ComponentModel.IComponent" /> that contains the data sourced property. </param>
		/// <param name="dataSource">The data source to retrieve. </param>
		/// <param name="dataMember">The data member to retrieve. </param>
		// Token: 0x0600036E RID: 878 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static IEnumerable GetSelectedDataSource(IComponent component, string dataSource, string dataMember)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		private static void OnDataBind(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an event handler for data binding.</summary>
		// Token: 0x0400012A RID: 298
		public static readonly EventHandler DataBindingHandler = new EventHandler(DesignTimeData.OnDataBind);
	}
}
