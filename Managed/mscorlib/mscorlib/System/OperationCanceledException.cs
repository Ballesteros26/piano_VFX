using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System
{
	/// <summary>The exception that is thrown in a thread upon cancellation of an operation that the thread was executing.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001AB RID: 427
	[ComVisible(true)]
	[Serializable]
	public class OperationCanceledException : SystemException
	{
		/// <summary>Gets a token associated with the operation that was canceled.</summary>
		/// <returns>A token associated with the operation that was canceled, or a default token.</returns>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x000497FF File Offset: 0x000479FF
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x00049807 File Offset: 0x00047A07
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
			private set
			{
				this._cancellationToken = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a system-supplied error message.</summary>
		// Token: 0x060011F0 RID: 4592 RVA: 0x00049810 File Offset: 0x00047A10
		public OperationCanceledException()
			: base(Environment.GetResourceString("The operation was canceled."))
		{
			base.SetErrorCode(-2146233029);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x060011F1 RID: 4593 RVA: 0x0004982D File Offset: 0x00047A2D
		public OperationCanceledException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233029);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060011F2 RID: 4594 RVA: 0x00049841 File Offset: 0x00047A41
		public OperationCanceledException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233029);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a cancellation token.</summary>
		/// <param name="token">A cancellation token associated with the operation that was canceled.</param>
		// Token: 0x060011F3 RID: 4595 RVA: 0x00049856 File Offset: 0x00047A56
		public OperationCanceledException(CancellationToken token)
			: this()
		{
			this.CancellationToken = token;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a specified error message and a cancellation token.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="token">A cancellation token associated with the operation that was canceled.</param>
		// Token: 0x060011F4 RID: 4596 RVA: 0x00049865 File Offset: 0x00047A65
		public OperationCanceledException(string message, CancellationToken token)
			: this(message)
		{
			this.CancellationToken = token;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with a specified error message, a reference to the inner exception that is the cause of this exception, and a cancellation token.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		/// <param name="token">A cancellation token associated with the operation that was canceled.</param>
		// Token: 0x060011F5 RID: 4597 RVA: 0x00049875 File Offset: 0x00047A75
		public OperationCanceledException(string message, Exception innerException, CancellationToken token)
			: this(message, innerException)
		{
			this.CancellationToken = token;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OperationCanceledException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060011F6 RID: 4598 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected OperationCanceledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000A4C RID: 2636
		[NonSerialized]
		private CancellationToken _cancellationToken;
	}
}
