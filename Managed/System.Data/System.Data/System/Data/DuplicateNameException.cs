using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when a duplicate database object name is encountered during an add operation in a <see cref="T:System.Data.DataSet" /> -related object.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200006C RID: 108
	[Serializable]
	public class DuplicateNameException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x0600041A RID: 1050 RVA: 0x000143E9 File Offset: 0x000125E9
		protected DuplicateNameException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class.</summary>
		// Token: 0x0600041B RID: 1051 RVA: 0x0001447A File Offset: 0x0001267A
		public DuplicateNameException()
			: base("Duplicate name not allowed.")
		{
			base.HResult = -2146232030;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x0600041C RID: 1052 RVA: 0x00014492 File Offset: 0x00012692
		public DuplicateNameException(string s)
			: base(s)
		{
			base.HResult = -2146232030;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with the specified string and exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x0600041D RID: 1053 RVA: 0x000144A6 File Offset: 0x000126A6
		public DuplicateNameException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232030;
		}
	}
}
