using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to insert a null value into a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is set to false.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000070 RID: 112
	[Serializable]
	public class NoNullAllowedException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x0600042A RID: 1066 RVA: 0x000143E9 File Offset: 0x000125E9
		protected NoNullAllowedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class.</summary>
		// Token: 0x0600042B RID: 1067 RVA: 0x0001457E File Offset: 0x0001277E
		public NoNullAllowedException()
			: base("Null not allowed.")
		{
			base.HResult = -2146232026;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x0600042C RID: 1068 RVA: 0x00014596 File Offset: 0x00012796
		public NoNullAllowedException(string s)
			: base(s)
		{
			base.HResult = -2146232026;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x0600042D RID: 1069 RVA: 0x000145AA File Offset: 0x000127AA
		public NoNullAllowedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232026;
		}
	}
}
