using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Provides an exception for read-only <see cref="T:System.Configuration.SettingsProperty" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000197 RID: 407
	[Serializable]
	public class SettingsPropertyIsReadOnlyException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class.</summary>
		// Token: 0x06000C22 RID: 3106 RVA: 0x0003CB64 File Offset: 0x0003AD64
		public SettingsPropertyIsReadOnlyException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class based on a supplied parameter.</summary>
		/// <param name="message">A string containing an exception message.</param>
		// Token: 0x06000C23 RID: 3107 RVA: 0x0003CB6C File Offset: 0x0003AD6C
		public SettingsPropertyIsReadOnlyException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class based on the supplied parameters.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination of the serialized stream.</param>
		// Token: 0x06000C24 RID: 3108 RVA: 0x000275ED File Offset: 0x000257ED
		protected SettingsPropertyIsReadOnlyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class based on supplied parameters.</summary>
		/// <param name="message">A string containing an exception message.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06000C25 RID: 3109 RVA: 0x0003CB75 File Offset: 0x0003AD75
		public SettingsPropertyIsReadOnlyException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
