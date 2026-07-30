using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.SqlDataSource.Filtering" /> event of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
	// Token: 0x0200030F RID: 783
	public class SqlDataSourceFilteringEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceFilteringEventArgs" /> class by using the specified object.</summary>
		/// <param name="parameterValues">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects.</param>
		// Token: 0x06001C01 RID: 7169 RVA: 0x000462F4 File Offset: 0x000444F4
		public SqlDataSourceFilteringEventArgs(IOrderedDictionary parameterValues)
		{
			this._parameterValues = parameterValues;
		}

		/// <summary>Gets an <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object that provides access to the <see cref="T:System.Web.UI.WebControls.Parameter" /> objects of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> class.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects.</returns>
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x00046303 File Offset: 0x00044503
		public IOrderedDictionary ParameterValues
		{
			get
			{
				return this._parameterValues;
			}
		}

		// Token: 0x04001768 RID: 5992
		private IOrderedDictionary _parameterValues;
	}
}
