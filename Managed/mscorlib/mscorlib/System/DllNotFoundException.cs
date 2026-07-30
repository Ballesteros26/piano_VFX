using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a DLL specified in a DLL import cannot be found.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000151 RID: 337
	[ComVisible(true)]
	[Serializable]
	public class DllNotFoundException : TypeLoadException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with default properties.</summary>
		// Token: 0x06000E72 RID: 3698 RVA: 0x0003CA26 File Offset: 0x0003AC26
		public DllNotFoundException()
			: base(Environment.GetResourceString("Dll was not found."))
		{
			base.SetErrorCode(-2146233052);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06000E73 RID: 3699 RVA: 0x0003CA43 File Offset: 0x0003AC43
		public DllNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233052);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000E74 RID: 3700 RVA: 0x0003CA57 File Offset: 0x0003AC57
		public DllNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233052);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06000E75 RID: 3701 RVA: 0x0003CA6C File Offset: 0x0003AC6C
		protected DllNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
