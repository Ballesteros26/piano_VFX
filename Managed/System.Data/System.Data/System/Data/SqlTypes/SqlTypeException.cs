using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The base exception class for the <see cref="N:System.Data.SqlTypes" />.</summary>
	// Token: 0x020002D0 RID: 720
	[Serializable]
	public class SqlTypeException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class.</summary>
		// Token: 0x0600215D RID: 8541 RVA: 0x0009CADF File Offset: 0x0009ACDF
		public SqlTypeException()
			: this("SqlType error.", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x0600215E RID: 8542 RVA: 0x0009CAED File Offset: 0x0009ACED
		public SqlTypeException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="e">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600215F RID: 8543 RVA: 0x0009CAF7 File Offset: 0x0009ACF7
		public SqlTypeException(string message, Exception e)
			: base(message, e)
		{
			base.HResult = -2146232016;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class with serialized data.</summary>
		/// <param name="si">The object that holds the serialized object data. </param>
		/// <param name="sc">The contextual information about the source or destination. </param>
		// Token: 0x06002160 RID: 8544 RVA: 0x0009CB0C File Offset: 0x0009AD0C
		protected SqlTypeException(SerializationInfo si, StreamingContext sc)
			: base(SqlTypeException.SqlTypeExceptionSerialization(si, sc), sc)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0009CB21 File Offset: 0x0009AD21
		private static SerializationInfo SqlTypeExceptionSerialization(SerializationInfo si, StreamingContext sc)
		{
			if (si != null && 1 == si.MemberCount)
			{
				new SqlTypeException(si.GetString("SqlTypeExceptionMessage")).GetObjectData(si, sc);
			}
			return si;
		}
	}
}
