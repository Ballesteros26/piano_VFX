using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an object appears more than once in an array of synchronization objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000153 RID: 339
	[ComVisible(true)]
	[Serializable]
	public class DuplicateWaitObjectException : ArgumentException
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x0003CE61 File Offset: 0x0003B061
		private static string DuplicateWaitObjectMessage
		{
			get
			{
				if (DuplicateWaitObjectException._duplicateWaitObjectMessage == null)
				{
					DuplicateWaitObjectException._duplicateWaitObjectMessage = Environment.GetResourceString("Duplicate objects in argument.");
				}
				return DuplicateWaitObjectException._duplicateWaitObjectMessage;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class.</summary>
		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003CE84 File Offset: 0x0003B084
		public DuplicateWaitObjectException()
			: base(DuplicateWaitObjectException.DuplicateWaitObjectMessage)
		{
			base.SetErrorCode(-2146233047);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with the name of the parameter that causes this exception.</summary>
		/// <param name="parameterName">The name of the parameter that caused the exception. </param>
		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003CE9C File Offset: 0x0003B09C
		public DuplicateWaitObjectException(string parameterName)
			: base(DuplicateWaitObjectException.DuplicateWaitObjectMessage, parameterName)
		{
			base.SetErrorCode(-2146233047);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="parameterName">The name of the parameter that caused the exception. </param>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003CEB5 File Offset: 0x0003B0B5
		public DuplicateWaitObjectException(string parameterName, string message)
			: base(message, parameterName)
		{
			base.SetErrorCode(-2146233047);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003CECA File Offset: 0x0003B0CA
		public DuplicateWaitObjectException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233047);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000EA9 RID: 3753 RVA: 0x00032A15 File Offset: 0x00030C15
		protected DuplicateWaitObjectException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040008ED RID: 2285
		private static volatile string _duplicateWaitObjectMessage;
	}
}
