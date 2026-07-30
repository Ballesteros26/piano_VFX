using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a unit of data is read from or written to an address that is not a multiple of the data size. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000146 RID: 326
	[ComVisible(true)]
	[Serializable]
	public sealed class DataMisalignedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DataMisalignedException" /> class. </summary>
		// Token: 0x06000CEB RID: 3307 RVA: 0x00037EC2 File Offset: 0x000360C2
		public DataMisalignedException()
			: base(Environment.GetResourceString("A datatype misalignment was detected in a load or store instruction."))
		{
			base.SetErrorCode(-2146233023);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DataMisalignedException" /> class using the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> object that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06000CEC RID: 3308 RVA: 0x00037EDF File Offset: 0x000360DF
		public DataMisalignedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233023);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DataMisalignedException" /> class using the specified error message and underlying exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> object that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current <see cref="T:System.DataMisalignedException" />. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000CED RID: 3309 RVA: 0x00037EF3 File Offset: 0x000360F3
		public DataMisalignedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233023);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00031FC1 File Offset: 0x000301C1
		internal DataMisalignedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
