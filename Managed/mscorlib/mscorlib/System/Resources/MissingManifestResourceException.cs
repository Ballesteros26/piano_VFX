using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Resources
{
	/// <summary>The exception that is thrown if the main assembly does not contain the resources for the neutral culture, and an appropriate satellite assembly is missing.</summary>
	// Token: 0x020002A1 RID: 673
	[ComVisible(true)]
	[Serializable]
	public class MissingManifestResourceException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class with default properties.</summary>
		// Token: 0x06001EFC RID: 7932 RVA: 0x00078867 File Offset: 0x00076A67
		public MissingManifestResourceException()
			: base(Environment.GetResourceString("Unable to find manifest resource."))
		{
			base.SetErrorCode(-2146233038);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class with the specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06001EFD RID: 7933 RVA: 0x00078884 File Offset: 0x00076A84
		public MissingManifestResourceException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233038);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001EFE RID: 7934 RVA: 0x00078898 File Offset: 0x00076A98
		public MissingManifestResourceException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233038);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination of the exception. </param>
		// Token: 0x06001EFF RID: 7935 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected MissingManifestResourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
