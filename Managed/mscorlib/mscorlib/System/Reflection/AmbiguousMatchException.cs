using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when binding to a member results in more than one member matching the binding criteria. This class cannot be inherited.</summary>
	// Token: 0x020002C0 RID: 704
	[ComVisible(true)]
	[Serializable]
	public sealed class AmbiguousMatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with an empty message string and the root cause exception set to null.</summary>
		// Token: 0x0600201B RID: 8219 RVA: 0x0007DD9C File Offset: 0x0007BF9C
		public AmbiguousMatchException()
			: base(Environment.GetResourceString("Ambiguous match found."))
		{
			base.SetErrorCode(-2147475171);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with its message string set to the given message and the root cause exception set to null.</summary>
		/// <param name="message">A string indicating the reason this exception was thrown. </param>
		// Token: 0x0600201C RID: 8220 RVA: 0x0007DDB9 File Offset: 0x0007BFB9
		public AmbiguousMatchException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147475171);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600201D RID: 8221 RVA: 0x0007DDCD File Offset: 0x0007BFCD
		public AmbiguousMatchException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147475171);
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x00031FC1 File Offset: 0x000301C1
		internal AmbiguousMatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
