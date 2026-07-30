using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to read or write protected memory.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200010A RID: 266
	[ComVisible(true)]
	[Serializable]
	public class AccessViolationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x060009B4 RID: 2484 RVA: 0x00031F7B File Offset: 0x0003017B
		public AccessViolationException()
			: base(Environment.GetResourceString("Attempted to read or write protected memory. This is often an indication that other memory is corrupt."))
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060009B5 RID: 2485 RVA: 0x00031F98 File Offset: 0x00030198
		public AccessViolationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060009B6 RID: 2486 RVA: 0x00031FAC File Offset: 0x000301AC
		public AccessViolationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x060009B7 RID: 2487 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected AccessViolationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000789 RID: 1929
		private IntPtr _ip;

		// Token: 0x0400078A RID: 1930
		private IntPtr _target;

		// Token: 0x0400078B RID: 1931
		private int _accessType;
	}
}
