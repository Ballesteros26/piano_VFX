using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Inserting" />, <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Updating" />, and <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Deleting" /> events of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	// Token: 0x020002F2 RID: 754
	public class ObjectDataSourceMethodEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs" /> class using the specified input parameters collection.</summary>
		/// <param name="inputParameters">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects that represent the names of the parameters of the business object method and their associated values. </param>
		// Token: 0x06001BBE RID: 7102 RVA: 0x000461B2 File Offset: 0x000443B2
		public ObjectDataSourceMethodEventArgs(IOrderedDictionary inputParameters)
		{
			this._inputParameters = inputParameters;
		}

		/// <summary>Gets a collection that contains business object method parameters and their values.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent the business object method parameters and their corresponding values.</returns>
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x000461C1 File Offset: 0x000443C1
		public IOrderedDictionary InputParameters
		{
			get
			{
				return this._inputParameters;
			}
		}

		// Token: 0x0400172E RID: 5934
		private IOrderedDictionary _inputParameters;
	}
}
