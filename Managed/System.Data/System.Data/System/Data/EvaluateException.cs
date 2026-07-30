using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when the <see cref="P:System.Data.DataColumn.Expression" /> property of a <see cref="T:System.Data.DataColumn" /> cannot be evaluated.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000B5 RID: 181
	[Serializable]
	public class EvaluateException : InvalidExpressionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object. </param>
		/// <param name="context">The source and destination of a particular serialized stream. </param>
		// Token: 0x06000A93 RID: 2707 RVA: 0x00031F2A File Offset: 0x0003012A
		protected EvaluateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class.</summary>
		// Token: 0x06000A94 RID: 2708 RVA: 0x00031F39 File Offset: 0x00030139
		public EvaluateException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000A95 RID: 2709 RVA: 0x00031F41 File Offset: 0x00030141
		public EvaluateException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000A96 RID: 2710 RVA: 0x00031F4A File Offset: 0x0003014A
		public EvaluateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
