using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when trying to access a drive or share that is not available.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000390 RID: 912
	[ComVisible(true)]
	[Serializable]
	public class DriveNotFoundException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DriveNotFoundException" /> class with its message string set to a system-supplied message and its HRESULT set to COR_E_DIRECTORYNOTFOUND. </summary>
		// Token: 0x06002A7D RID: 10877 RVA: 0x00097FF7 File Offset: 0x000961F7
		public DriveNotFoundException()
			: base(Environment.GetResourceString("Attempted to access a drive that is not available."))
		{
			base.SetErrorCode(-2147024893);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DriveNotFoundException" /> class with the specified message string and the HRESULT set to COR_E_DIRECTORYNOTFOUND. </summary>
		/// <param name="message">A <see cref="T:System.String" /> object that describes the error. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06002A7E RID: 10878 RVA: 0x00097FC4 File Offset: 0x000961C4
		public DriveNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024893);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DriveNotFoundException" /> class with the specified error message and a reference to the inner exception that is the cause of this exception. </summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002A7F RID: 10879 RVA: 0x00097FD8 File Offset: 0x000961D8
		public DriveNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024893);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DriveNotFoundException" /> class with the specified serialization and context information. </summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the serialized object data about the exception being thrown. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination of the exception being thrown. </param>
		// Token: 0x06002A80 RID: 10880 RVA: 0x00097FED File Offset: 0x000961ED
		protected DriveNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
