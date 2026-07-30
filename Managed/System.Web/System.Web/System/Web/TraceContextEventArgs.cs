using System;
using System.Collections;

namespace System.Web
{
	/// <summary>Provides a collection of trace records to any method that handles the <see cref="E:System.Web.TraceContext.TraceFinished" /> event. This class cannot be inherited.</summary>
	// Token: 0x02000058 RID: 88
	public sealed class TraceContextEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.TraceContextEventArgs" /> class, using the provided collection of trace records.</summary>
		/// <param name="records">A collection of <see cref="T:System.Web.TraceContextRecord" /> objects that represent all the trace records logged for the current request.</param>
		// Token: 0x060003E3 RID: 995 RVA: 0x0000739B File Offset: 0x0000559B
		public TraceContextEventArgs(ICollection records)
		{
			this._records = records;
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.TraceContextRecord" /> messages that are associated with the current request.</summary>
		/// <returns>A collection of trace records that are associated with the current request.</returns>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x000073AA File Offset: 0x000055AA
		public ICollection TraceRecords
		{
			get
			{
				return this._records;
			}
		}

		// Token: 0x04000E2E RID: 3630
		private ICollection _records;
	}
}
