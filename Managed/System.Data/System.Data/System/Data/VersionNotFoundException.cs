using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to return a version of a <see cref="T:System.Data.DataRow" /> that has been deleted.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000073 RID: 115
	[Serializable]
	public class VersionNotFoundException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x06000436 RID: 1078 RVA: 0x000143E9 File Offset: 0x000125E9
		protected VersionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class.</summary>
		// Token: 0x06000437 RID: 1079 RVA: 0x00014641 File Offset: 0x00012841
		public VersionNotFoundException()
			: base("Version not found.")
		{
			base.HResult = -2146232023;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000438 RID: 1080 RVA: 0x00014659 File Offset: 0x00012859
		public VersionNotFoundException(string s)
			: base(s)
		{
			base.HResult = -2146232023;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000439 RID: 1081 RVA: 0x0001466D File Offset: 0x0001286D
		public VersionNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232023;
		}
	}
}
