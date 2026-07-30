using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when a path or file name is longer than the system-defined maximum length.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A2 RID: 930
	[ComVisible(true)]
	[Serializable]
	public class PathTooLongException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.PathTooLongException" /> class with its HRESULT set to COR_E_PATHTOOLONG.</summary>
		// Token: 0x06002B38 RID: 11064 RVA: 0x0009A5E9 File Offset: 0x000987E9
		public PathTooLongException()
			: base(Environment.GetResourceString("The specified path, file name, or both are too long. The fully qualified file name must be less than 260 characters, and the directory name must be less than 248 characters."))
		{
			base.SetErrorCode(-2147024690);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.PathTooLongException" /> class with its message string set to <paramref name="message" /> and its HRESULT set to COR_E_PATHTOOLONG.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06002B39 RID: 11065 RVA: 0x0009A606 File Offset: 0x00098806
		public PathTooLongException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024690);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.PathTooLongException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002B3A RID: 11066 RVA: 0x0009A61A File Offset: 0x0009881A
		public PathTooLongException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024690);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.PathTooLongException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06002B3B RID: 11067 RVA: 0x00097FED File Offset: 0x000961ED
		protected PathTooLongException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
