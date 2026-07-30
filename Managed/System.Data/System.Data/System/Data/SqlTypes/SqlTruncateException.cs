using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The exception that is thrown when you set a value into a <see cref="N:System.Data.SqlTypes" /> structure would truncate that value.</summary>
	// Token: 0x020002D2 RID: 722
	[Serializable]
	public sealed class SqlTruncateException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class.</summary>
		// Token: 0x06002166 RID: 8550 RVA: 0x0009CB9A File Offset: 0x0009AD9A
		public SqlTruncateException()
			: this(SQLResource.TruncationMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06002167 RID: 8551 RVA: 0x0009CBA8 File Offset: 0x0009ADA8
		public SqlTruncateException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class with a specified error message and a reference to the <see cref="T:System.Exception" />.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="e">A reference to an inner <see cref="T:System.Exception" />. </param>
		// Token: 0x06002168 RID: 8552 RVA: 0x0009CBB2 File Offset: 0x0009ADB2
		public SqlTruncateException(string message, Exception e)
			: base(message, e)
		{
			base.HResult = -2146232014;
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0009CBC7 File Offset: 0x0009ADC7
		private static SerializationInfo SqlTruncateExceptionSerialization(SerializationInfo si, StreamingContext sc)
		{
			if (si != null && 1 == si.MemberCount)
			{
				new SqlTruncateException(si.GetString("SqlTruncateExceptionMessage")).GetObjectData(si, sc);
			}
			return si;
		}
	}
}
