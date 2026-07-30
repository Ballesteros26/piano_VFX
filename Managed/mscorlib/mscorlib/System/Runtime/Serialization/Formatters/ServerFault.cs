using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Contains information for a server fault. This class cannot be inherited.</summary>
	// Token: 0x02000702 RID: 1794
	[SoapType(Embedded = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ServerFault
	{
		// Token: 0x06004B41 RID: 19265 RVA: 0x0010C9FD File Offset: 0x0010ABFD
		internal ServerFault(Exception exception)
		{
			this.exception = exception;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.ServerFault" /> class.</summary>
		/// <param name="exceptionType">The type of the exception that occurred on the server. </param>
		/// <param name="message">The message that accompanied the exception. </param>
		/// <param name="stackTrace">The stack trace of the thread that threw the exception on the server. </param>
		// Token: 0x06004B42 RID: 19266 RVA: 0x0010CA0C File Offset: 0x0010AC0C
		public ServerFault(string exceptionType, string message, string stackTrace)
		{
			this.exceptionType = exceptionType;
			this.message = message;
			this.stackTrace = stackTrace;
		}

		/// <summary>Gets or sets the type of exception that was thrown by the server.</summary>
		/// <returns>The type of exception that was thrown by the server.</returns>
		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06004B43 RID: 19267 RVA: 0x0010CA29 File Offset: 0x0010AC29
		// (set) Token: 0x06004B44 RID: 19268 RVA: 0x0010CA31 File Offset: 0x0010AC31
		public string ExceptionType
		{
			get
			{
				return this.exceptionType;
			}
			set
			{
				this.exceptionType = value;
			}
		}

		/// <summary>Gets or sets the exception message that accompanied the exception thrown on the server.</summary>
		/// <returns>The exception message that accompanied the exception thrown on the server.</returns>
		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06004B45 RID: 19269 RVA: 0x0010CA3A File Offset: 0x0010AC3A
		// (set) Token: 0x06004B46 RID: 19270 RVA: 0x0010CA42 File Offset: 0x0010AC42
		public string ExceptionMessage
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		/// <summary>Gets or sets the stack trace of the thread that threw the exception on the server.</summary>
		/// <returns>The stack trace of the thread that threw the exception on the server.</returns>
		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06004B47 RID: 19271 RVA: 0x0010CA4B File Offset: 0x0010AC4B
		// (set) Token: 0x06004B48 RID: 19272 RVA: 0x0010CA53 File Offset: 0x0010AC53
		public string StackTrace
		{
			get
			{
				return this.stackTrace;
			}
			set
			{
				this.stackTrace = value;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06004B49 RID: 19273 RVA: 0x0010CA5C File Offset: 0x0010AC5C
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04002744 RID: 10052
		private string exceptionType;

		// Token: 0x04002745 RID: 10053
		private string message;

		// Token: 0x04002746 RID: 10054
		private string stackTrace;

		// Token: 0x04002747 RID: 10055
		private Exception exception;
	}
}
