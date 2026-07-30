using System;
using System.Runtime.Serialization;

namespace System.Transactions
{
	/// <summary>The exception that is thrown when you attempt to do work on a transaction that cannot accept new work.  </summary>
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class TransactionException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class.</summary>
		// Token: 0x0600008E RID: 142 RVA: 0x00002B3D File Offset: 0x00000D3D
		public TransactionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		// Token: 0x0600008F RID: 143 RVA: 0x00002B45 File Offset: 0x00000D45
		public TransactionException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		// Token: 0x06000090 RID: 144 RVA: 0x00002B4E File Offset: 0x00000D4E
		public TransactionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		// Token: 0x06000091 RID: 145 RVA: 0x00002B58 File Offset: 0x00000D58
		protected TransactionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
