using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.ObjectCreating" /> and <see cref="E:System.Web.UI.WebControls.ObjectDataSource.ObjectCreated" /> events of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	// Token: 0x020002EF RID: 751
	public class ObjectDataSourceEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceEventArgs" /> class using the specified object.</summary>
		/// <param name="objectInstance">The business object with which the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> interacts to perform data operations.</param>
		// Token: 0x06001BB5 RID: 7093 RVA: 0x0004617B File Offset: 0x0004437B
		public ObjectDataSourceEventArgs(object objectInstance)
		{
			this._objectInstance = objectInstance;
		}

		/// <summary>Gets or sets an object that represents the business object with which the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control performs data operations.</summary>
		/// <returns>The business object the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to perform data operations; otherwise, null, if null is passed to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceEventArgs" />.</returns>
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x0004618A File Offset: 0x0004438A
		// (set) Token: 0x06001BB7 RID: 7095 RVA: 0x00046192 File Offset: 0x00044392
		public object ObjectInstance
		{
			get
			{
				return this._objectInstance;
			}
			set
			{
				this._objectInstance = value;
			}
		}

		// Token: 0x0400172C RID: 5932
		private object _objectInstance;
	}
}
