using System;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Represents an ASP.NET trace message and any associated data.</summary>
	// Token: 0x020000DF RID: 223
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TraceContextRecord
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.TraceContextRecord" /> class. </summary>
		/// <param name="category">The trace category that receives the message.</param>
		/// <param name="msg">The trace message.</param>
		/// <param name="isWarning">true if the method associated with the <see cref="T:System.Web.TraceContextRecord" /> is the <see cref="Overload:System.Web.TraceContext.Warn" /> method; false if the tracing method is the <see cref="Overload:System.Web.TraceContext.Write" /> method.</param>
		/// <param name="errorInfo">A <see cref="T:System.Exception" /> object that contains additional error information. </param>
		// Token: 0x06000C0D RID: 3085 RVA: 0x0002029C File Offset: 0x0001E49C
		public TraceContextRecord(string category, string msg, bool isWarning, Exception errorInfo)
		{
			this.category = category;
			this.message = msg;
			this.isWarning = isWarning;
			this.errorInfo = errorInfo;
		}

		/// <summary>Gets the user-defined category for the trace record.</summary>
		/// <returns>A string that represents a category for the trace record.</returns>
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x000202C1 File Offset: 0x0001E4C1
		public string Category
		{
			get
			{
				return this.category;
			}
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> associated with the trace record, if one is available.</summary>
		/// <returns>A <see cref="T:System.Exception" /> associated with the trace record, if one exists, or null.</returns>
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x000202C9 File Offset: 0x0001E4C9
		public Exception ErrorInfo
		{
			get
			{
				return this.errorInfo;
			}
		}

		/// <summary>Gets a value indicating whether the trace record is associated with a <see cref="Overload:System.Web.TraceContext.Warn" /> method call.</summary>
		/// <returns>true if the <see cref="T:System.Web.TraceContextRecord" /> is associated with the <see cref="Overload:System.Web.TraceContext.Warn" /> method call; otherwise, false.</returns>
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x000202D1 File Offset: 0x0001E4D1
		public bool IsWarning
		{
			get
			{
				return this.isWarning;
			}
		}

		/// <summary>Gets the user-defined trace message.</summary>
		/// <returns>A string that represents a message for the trace record.</returns>
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x000202D9 File Offset: 0x0001E4D9
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x040010C9 RID: 4297
		private string category;

		// Token: 0x040010CA RID: 4298
		private Exception errorInfo;

		// Token: 0x040010CB RID: 4299
		private bool isWarning;

		// Token: 0x040010CC RID: 4300
		private string message;
	}
}
