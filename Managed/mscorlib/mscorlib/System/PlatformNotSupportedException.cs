using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a feature does not run on a particular platform.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B0 RID: 432
	[ComVisible(true)]
	[Serializable]
	public class PlatformNotSupportedException : NotSupportedException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with default properties.</summary>
		// Token: 0x06001208 RID: 4616 RVA: 0x00049A1C File Offset: 0x00047C1C
		public PlatformNotSupportedException()
			: base(Environment.GetResourceString("Operation is not supported on this platform."))
		{
			base.SetErrorCode(-2146233031);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with a specified error message.</summary>
		/// <param name="message">The text message that explains the reason for the exception. </param>
		// Token: 0x06001209 RID: 4617 RVA: 0x00049A39 File Offset: 0x00047C39
		public PlatformNotSupportedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233031);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600120A RID: 4618 RVA: 0x00049A4D File Offset: 0x00047C4D
		public PlatformNotSupportedException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233031);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x0600120B RID: 4619 RVA: 0x00049A62 File Offset: 0x00047C62
		protected PlatformNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
