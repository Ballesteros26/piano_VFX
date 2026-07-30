using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a requested method or operation is not implemented.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A4 RID: 420
	[ComVisible(true)]
	[Serializable]
	public class NotImplementedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with default properties.</summary>
		// Token: 0x060011A9 RID: 4521 RVA: 0x0004858C File Offset: 0x0004678C
		public NotImplementedException()
			: base(Environment.GetResourceString("The method or operation is not implemented."))
		{
			base.SetErrorCode(-2147467263);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x060011AA RID: 4522 RVA: 0x000485A9 File Offset: 0x000467A9
		public NotImplementedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467263);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060011AB RID: 4523 RVA: 0x000485BD File Offset: 0x000467BD
		public NotImplementedException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147467263);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x060011AC RID: 4524 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected NotImplementedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
