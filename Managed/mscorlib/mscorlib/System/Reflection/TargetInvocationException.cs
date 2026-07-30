using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown by methods invoked through reflection. This class cannot be inherited.</summary>
	// Token: 0x02000301 RID: 769
	[ComVisible(true)]
	[Serializable]
	public sealed class TargetInvocationException : ApplicationException
	{
		// Token: 0x06002116 RID: 8470 RVA: 0x0007F081 File Offset: 0x0007D281
		private TargetInvocationException()
			: base(Environment.GetResourceString("Exception has been thrown by the target of an invocation."))
		{
			base.SetErrorCode(-2146232828);
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0007F09E File Offset: 0x0007D29E
		private TargetInvocationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232828);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetInvocationException" /> class with a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002118 RID: 8472 RVA: 0x0007F0B2 File Offset: 0x0007D2B2
		public TargetInvocationException(Exception inner)
			: base(Environment.GetResourceString("Exception has been thrown by the target of an invocation."), inner)
		{
			base.SetErrorCode(-2146232828);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetInvocationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002119 RID: 8473 RVA: 0x0007F0D0 File Offset: 0x0007D2D0
		public TargetInvocationException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146232828);
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x0007E05D File Offset: 0x0007C25D
		internal TargetInvocationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
