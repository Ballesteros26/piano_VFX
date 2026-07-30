using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The base exception type for all COM interop exceptions and structured exception handling (SEH) exceptions.</summary>
	// Token: 0x020008DE RID: 2270
	[ComVisible(true)]
	[Serializable]
	public class ExternalException : SystemException
	{
		/// <summary>Initializes a new instance of the ExternalException class with default properties.</summary>
		// Token: 0x06005562 RID: 21858 RVA: 0x00128D3F File Offset: 0x00126F3F
		public ExternalException()
			: base(Environment.GetResourceString("External component has thrown an exception."))
		{
			base.SetErrorCode(-2147467259);
		}

		/// <summary>Initializes a new instance of the ExternalException class with a specified error message.</summary>
		/// <param name="message">The error message that specifies the reason for the exception. </param>
		// Token: 0x06005563 RID: 21859 RVA: 0x00128D5C File Offset: 0x00126F5C
		public ExternalException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467259);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ExternalException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06005564 RID: 21860 RVA: 0x00128D70 File Offset: 0x00126F70
		public ExternalException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147467259);
		}

		/// <summary>Initializes a new instance of the ExternalException class with a specified error message and the HRESULT of the error.</summary>
		/// <param name="message">The error message that specifies the reason for the exception. </param>
		/// <param name="errorCode">The HRESULT of the error. </param>
		// Token: 0x06005565 RID: 21861 RVA: 0x00047AE8 File Offset: 0x00045CE8
		public ExternalException(string message, int errorCode)
			: base(message)
		{
			base.SetErrorCode(errorCode);
		}

		/// <summary>Initializes a new instance of the ExternalException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06005566 RID: 21862 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected ExternalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the HRESULT of the error.</summary>
		/// <returns>The HRESULT of the error.</returns>
		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06005567 RID: 21863 RVA: 0x00128D85 File Offset: 0x00126F85
		public virtual int ErrorCode
		{
			get
			{
				return base.HResult;
			}
		}

		/// <summary>Returns a string that contains the HRESULT of the error.</summary>
		/// <returns>A string that represents the HRESULT. </returns>
		// Token: 0x06005568 RID: 21864 RVA: 0x00128D90 File Offset: 0x00126F90
		public override string ToString()
		{
			string message = this.Message;
			string text = base.GetType().ToString() + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (!string.IsNullOrEmpty(message))
			{
				text = text + ": " + message;
			}
			Exception innerException = base.InnerException;
			if (innerException != null)
			{
				text = text + " ---> " + innerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			return text;
		}
	}
}
