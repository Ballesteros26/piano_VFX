using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when an I/O error occurs.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020003A0 RID: 928
	[ComVisible(true)]
	[Serializable]
	public class IOException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with its message string set to the empty string (""), its HRESULT set to COR_E_IO, and its inner exception set to a null reference.</summary>
		// Token: 0x06002B0C RID: 11020 RVA: 0x000997C2 File Offset: 0x000979C2
		public IOException()
			: base(Environment.GetResourceString("I/O error occurred."))
		{
			base.SetErrorCode(-2146232800);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with its message string set to <paramref name="message" />, its HRESULT set to COR_E_IO, and its inner exception set to null.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06002B0D RID: 11021 RVA: 0x000997DF File Offset: 0x000979DF
		public IOException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232800);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with its message string set to <paramref name="message" /> and its HRESULT user-defined.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="hresult">An integer identifying the error that has occurred. </param>
		// Token: 0x06002B0E RID: 11022 RVA: 0x00047AE8 File Offset: 0x00045CE8
		public IOException(string message, int hresult)
			: base(message)
		{
			base.SetErrorCode(hresult);
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000997F3 File Offset: 0x000979F3
		internal IOException(string message, int hresult, string maybeFullPath)
			: base(message)
		{
			base.SetErrorCode(hresult);
			this._maybeFullPath = maybeFullPath;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002B10 RID: 11024 RVA: 0x0009980A File Offset: 0x00097A0A
		public IOException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146232800);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The data for serializing or deserializing the object. </param>
		/// <param name="context">The source and destination for the object. </param>
		// Token: 0x06002B11 RID: 11025 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected IOException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040016A5 RID: 5797
		[NonSerialized]
		private string _maybeFullPath;
	}
}
