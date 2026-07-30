using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>The exception that is thrown by a strongly typed <see cref="T:System.Data.DataSet" /> when the user accesses a DBNull value.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000FC RID: 252
	[Serializable]
	public class StrongTypingException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class using the specified serialization information and streaming context.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure. </param>
		// Token: 0x06000CF9 RID: 3321 RVA: 0x000143E9 File Offset: 0x000125E9
		protected StrongTypingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class.</summary>
		// Token: 0x06000CFA RID: 3322 RVA: 0x0003C23F File Offset: 0x0003A43F
		public StrongTypingException()
		{
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class with the specified string.</summary>
		/// <param name="message">The string to display when the exception is thrown. </param>
		// Token: 0x06000CFB RID: 3323 RVA: 0x0003C252 File Offset: 0x0003A452
		public StrongTypingException(string message)
			: base(message)
		{
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class with the specified string and inner exception.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		/// <param name="innerException">A reference to an inner exception. </param>
		// Token: 0x06000CFC RID: 3324 RVA: 0x0003C266 File Offset: 0x0003A466
		public StrongTypingException(string s, Exception innerException)
			: base(s, innerException)
		{
			base.HResult = -2146232021;
		}
	}
}
