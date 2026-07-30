using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.ObjectDisposing" /> event of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	// Token: 0x020002ED RID: 749
	public class ObjectDataSourceDisposingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs" /> class using the specified object.</summary>
		/// <param name="objectInstance">The business object with which the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> interacts to perform data operations.</param>
		// Token: 0x06001BAF RID: 7087 RVA: 0x00046164 File Offset: 0x00044364
		public ObjectDataSourceDisposingEventArgs(object objectInstance)
		{
			this._objectInstance = objectInstance;
		}

		/// <summary>Gets an object that represents the business object with which the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control performs data operations.</summary>
		/// <returns>The business object the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to data operations; otherwise, null, if null is passed to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceEventArgs" />.</returns>
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x00046173 File Offset: 0x00044373
		public object ObjectInstance
		{
			get
			{
				return this._objectInstance;
			}
		}

		// Token: 0x0400172B RID: 5931
		private object _objectInstance;
	}
}
