using System;

namespace System.Web.UI
{
	/// <summary>Represents the asynchronous callback method that a data-bound control supplies to a data source view for asynchronous insert, update, or delete data operations.</summary>
	/// <returns>A value indicating whether any exceptions thrown during the data operation were handled.</returns>
	/// <param name="affectedRecords">The number of records that the data operation affected.</param>
	/// <param name="ex">An <see cref="T:System.Exception" />, if one is thrown by the data operation during processing.</param>
	// Token: 0x020001C7 RID: 455
	// (Invoke) Token: 0x0600129F RID: 4767
	public delegate bool DataSourceViewOperationCallback(int affectedRecords, Exception ex);
}
