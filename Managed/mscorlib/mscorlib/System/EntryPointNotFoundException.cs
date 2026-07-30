using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to load a class fails due to the absence of an entry method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000155 RID: 341
	[ComVisible(true)]
	[Serializable]
	public class EntryPointNotFoundException : TypeLoadException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class.</summary>
		// Token: 0x06000EAF RID: 3759 RVA: 0x0003CF04 File Offset: 0x0003B104
		public EntryPointNotFoundException()
			: base(Environment.GetResourceString("Entry point was not found."))
		{
			base.SetErrorCode(-2146233053);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003CF21 File Offset: 0x0003B121
		public EntryPointNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233053);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003CF35 File Offset: 0x0003B135
		public EntryPointNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233053);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003CA6C File Offset: 0x0003AC6C
		protected EntryPointNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
