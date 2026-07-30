using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when attempting an action that violates a constraint.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200006A RID: 106
	[Serializable]
	public class ConstraintException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class using the specified serialization and stream context.</summary>
		/// <param name="info">The data necessary to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x06000412 RID: 1042 RVA: 0x000143E9 File Offset: 0x000125E9
		protected ConstraintException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class. This is the default constructor.</summary>
		// Token: 0x06000413 RID: 1043 RVA: 0x000143F8 File Offset: 0x000125F8
		public ConstraintException()
			: base("Constraint Exception.")
		{
			base.HResult = -2146232022;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000414 RID: 1044 RVA: 0x00014410 File Offset: 0x00012610
		public ConstraintException(string s)
			: base(s)
		{
			base.HResult = -2146232022;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class using the specified string and inner exception.</summary>
		/// <param name="message">The string to display when the exception is thrown. </param>
		/// <param name="innerException">Gets the Exception instance that caused the current exception.</param>
		// Token: 0x06000415 RID: 1045 RVA: 0x00014424 File Offset: 0x00012624
		public ConstraintException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232022;
		}
	}
}
