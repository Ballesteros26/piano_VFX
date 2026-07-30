using System;

namespace System.Data.Common
{
	/// <summary>Provides a mechanism for enumerating all available instances of database servers within the local network. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000348 RID: 840
	public abstract class DbDataSourceEnumerator
	{
		/// <summary>Retrieves a <see cref="T:System.Data.DataTable" /> containing information about all visible instances of the server represented by the strongly typed instance of this class.</summary>
		/// <returns>Returns a <see cref="T:System.Data.DataTable" /> containing information about the visible instances of the associated data source.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060027F1 RID: 10225
		public abstract DataTable GetDataSources();
	}
}
