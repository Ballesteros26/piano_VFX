using System;
using System.ComponentModel;
using Unity;

namespace System.Web.Services.Protocols
{
	/// <summary>Represents the result of an asynchronously invoked web method.</summary>
	// Token: 0x0200001F RID: 31
	public class InvokeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060000AC RID: 172 RVA: 0x0000384D File Offset: 0x00001A4D
		internal InvokeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		/// <summary>Gets the results returned by the Web method.</summary>
		/// <returns>An array of objects returned by the Web method.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003860 File Offset: 0x00001A60
		public object[] Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003846 File Offset: 0x00001A46
		internal InvokeCompletedEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040001C4 RID: 452
		private object[] results;
	}
}
