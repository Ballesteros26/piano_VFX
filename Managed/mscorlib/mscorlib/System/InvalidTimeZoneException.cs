using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System
{
	/// <summary>The exception that is thrown when time zone information is invalid.</summary>
	// Token: 0x02000195 RID: 405
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class InvalidTimeZoneException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class with the specified message string.</summary>
		/// <param name="message">A string that describes the exception.</param>
		// Token: 0x06001121 RID: 4385 RVA: 0x00047B84 File Offset: 0x00045D84
		public InvalidTimeZoneException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A string that describes the exception.    </param>
		/// <param name="innerException">The exception that is the cause of the current exception.  </param>
		// Token: 0x06001122 RID: 4386 RVA: 0x00047B8D File Offset: 0x00045D8D
		public InvalidTimeZoneException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class from serialized data.</summary>
		/// <param name="info">The object that contains the serialized data. </param>
		/// <param name="context">The stream that contains the serialized data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.-or-The <paramref name="context" /> parameter is null.</exception>
		// Token: 0x06001123 RID: 4387 RVA: 0x000325DC File Offset: 0x000307DC
		protected InvalidTimeZoneException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class with a system-supplied message.</summary>
		// Token: 0x06001124 RID: 4388 RVA: 0x00047B97 File Offset: 0x00045D97
		public InvalidTimeZoneException()
		{
		}
	}
}
