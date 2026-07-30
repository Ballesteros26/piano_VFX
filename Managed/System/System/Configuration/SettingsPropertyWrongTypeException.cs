using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Provides an exception that is thrown when an invalid type is used with a <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
	// Token: 0x0200019B RID: 411
	[Serializable]
	public class SettingsPropertyWrongTypeException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class.</summary>
		// Token: 0x06000C46 RID: 3142 RVA: 0x0003CB64 File Offset: 0x0003AD64
		public SettingsPropertyWrongTypeException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameter.</summary>
		/// <param name="message">A string containing an exception message.</param>
		// Token: 0x06000C47 RID: 3143 RVA: 0x0003CB6C File Offset: 0x0003AD6C
		public SettingsPropertyWrongTypeException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameters.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination of the serialized stream.</param>
		// Token: 0x06000C48 RID: 3144 RVA: 0x000275ED File Offset: 0x000257ED
		protected SettingsPropertyWrongTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameters.</summary>
		/// <param name="message">A string containing an exception message.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06000C49 RID: 3145 RVA: 0x0003CB75 File Offset: 0x0003AD75
		public SettingsPropertyWrongTypeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
