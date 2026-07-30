using System;
using System.Runtime.Serialization;

namespace System.Transactions
{
	/// <summary>The exception that is thrown when a promotion fails.</summary>
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class TransactionPromotionException : TransactionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class. </summary>
		// Token: 0x060000BD RID: 189 RVA: 0x00002AF9 File Offset: 0x00000CF9
		public TransactionPromotionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		// Token: 0x060000BE RID: 190 RVA: 0x00002B01 File Offset: 0x00000D01
		public TransactionPromotionException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		// Token: 0x060000BF RID: 191 RVA: 0x00002B0A File Offset: 0x00000D0A
		public TransactionPromotionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		// Token: 0x060000C0 RID: 192 RVA: 0x00002B14 File Offset: 0x00000D14
		protected TransactionPromotionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
