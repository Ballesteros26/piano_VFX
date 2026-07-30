using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Filtering" /> event of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	// Token: 0x020002F0 RID: 752
	public class ObjectDataSourceFilteringEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceFilteringEventArgs" /> class by using the specified object.</summary>
		/// <param name="parameterValues">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects.</param>
		// Token: 0x06001BB8 RID: 7096 RVA: 0x0004619B File Offset: 0x0004439B
		public ObjectDataSourceFilteringEventArgs(IOrderedDictionary parameterValues)
		{
			this._parameterValues = parameterValues;
		}

		/// <summary>Gets an <see cref="T:System.Collections.Specialized.IOrderedDictionary" />  interface that provides access to the <see cref="T:System.Web.UI.WebControls.Parameter" /> objects of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> class.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects.</returns>
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x000461AA File Offset: 0x000443AA
		public IOrderedDictionary ParameterValues
		{
			get
			{
				return this._parameterValues;
			}
		}

		// Token: 0x0400172D RID: 5933
		private IOrderedDictionary _parameterValues;
	}
}
