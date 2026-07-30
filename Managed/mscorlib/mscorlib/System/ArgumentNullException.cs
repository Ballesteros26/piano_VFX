using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when a null reference (Nothing in Visual Basic) is passed to a method that does not accept it as a valid argument. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000127 RID: 295
	[ComVisible(true)]
	[Serializable]
	public class ArgumentNullException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class.</summary>
		// Token: 0x06000A4E RID: 2638 RVA: 0x000329B0 File Offset: 0x00030BB0
		public ArgumentNullException()
			: base(Environment.GetResourceString("Value cannot be null."))
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception. </param>
		// Token: 0x06000A4F RID: 2639 RVA: 0x000329CD File Offset: 0x00030BCD
		public ArgumentNullException(string paramName)
			: base(Environment.GetResourceString("Value cannot be null."), paramName)
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with a specified error message and the exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for this exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000A50 RID: 2640 RVA: 0x000329EB File Offset: 0x00030BEB
		public ArgumentNullException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.ArgumentNullException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception. </param>
		/// <param name="message">A message that describes the error. </param>
		// Token: 0x06000A51 RID: 2641 RVA: 0x00032A00 File Offset: 0x00030C00
		public ArgumentNullException(string paramName, string message)
			: base(message, paramName)
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">An object that describes the source or destination of the serialized data. </param>
		// Token: 0x06000A52 RID: 2642 RVA: 0x00032A15 File Offset: 0x00030C15
		[SecurityCritical]
		protected ArgumentNullException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
