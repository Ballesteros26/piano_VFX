using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Data.Common
{
	/// <summary>The base class for all exceptions thrown on behalf of the data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200034B RID: 843
	[Serializable]
	public abstract class DbException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class.</summary>
		// Token: 0x06002803 RID: 10243 RVA: 0x000B1129 File Offset: 0x000AF329
		protected DbException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified error message.</summary>
		/// <param name="message">The message to display for this exception.</param>
		// Token: 0x06002804 RID: 10244 RVA: 0x000B1131 File Offset: 0x000AF331
		protected DbException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message string.</param>
		/// <param name="innerException">The inner exception reference.</param>
		// Token: 0x06002805 RID: 10245 RVA: 0x000B113A File Offset: 0x000AF33A
		protected DbException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified error message and error code.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="errorCode">The error code for the exception.</param>
		// Token: 0x06002806 RID: 10246 RVA: 0x000B1144 File Offset: 0x000AF344
		protected DbException(string message, int errorCode)
			: base(message, errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified serialization information and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06002807 RID: 10247 RVA: 0x000B114E File Offset: 0x000AF34E
		protected DbException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
