using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when errors are generated using ADO.NET components.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000069 RID: 105
	[Serializable]
	public class DataException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class with the specified serialization information and context.</summary>
		/// <param name="info">The data necessary to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x0600040E RID: 1038 RVA: 0x000143A4 File Offset: 0x000125A4
		protected DataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class. This is the default constructor.</summary>
		// Token: 0x0600040F RID: 1039 RVA: 0x000143B3 File Offset: 0x000125B3
		public DataException()
			: base("Data Exception.")
		{
			base.HResult = -2146232032;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000410 RID: 1040 RVA: 0x000143CB File Offset: 0x000125CB
		public DataException(string s)
			: base(s)
		{
			base.HResult = -2146232032;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class with the specified string and inner exception.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		/// <param name="innerException">A reference to an inner exception. </param>
		// Token: 0x06000411 RID: 1041 RVA: 0x000143DF File Offset: 0x000125DF
		public DataException(string s, Exception innerException)
			: base(s, innerException)
		{
		}
	}
}
