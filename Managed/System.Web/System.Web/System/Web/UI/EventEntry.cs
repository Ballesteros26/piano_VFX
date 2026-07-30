using System;

namespace System.Web.UI
{
	/// <summary>Acts as the property entry for event handlers.</summary>
	// Token: 0x02000160 RID: 352
	public class EventEntry
	{
		/// <summary>Gets or sets the name of the method that handles the event.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the event handler method name.</returns>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0002B289 File Offset: 0x00029489
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0002B291 File Offset: 0x00029491
		public string HandlerMethodName
		{
			get
			{
				return this._handlerMethodName;
			}
			set
			{
				this._handlerMethodName = value;
			}
		}

		/// <summary>Gets or sets the type of delegate for the event.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of delegate for the event.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0002B29A File Offset: 0x0002949A
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x0002B2A2 File Offset: 0x000294A2
		public Type HandlerType
		{
			get
			{
				return this._handlerType;
			}
			set
			{
				this._handlerType = value;
			}
		}

		/// <summary>Gets or sets the name of the property from the expression.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the property.</returns>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0002B2AB File Offset: 0x000294AB
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x0002B2B3 File Offset: 0x000294B3
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x04001242 RID: 4674
		private Type _handlerType;

		// Token: 0x04001243 RID: 4675
		private string _handlerMethodName;

		// Token: 0x04001244 RID: 4676
		private string _name;
	}
}
