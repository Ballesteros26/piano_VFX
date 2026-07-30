using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Provides an exception for <see cref="T:System.Configuration.SettingsProperty" /> objects that are not found.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000198 RID: 408
	[Serializable]
	public class SettingsPropertyNotFoundException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyNotFoundException" /> class. </summary>
		// Token: 0x06000C26 RID: 3110 RVA: 0x0003CB64 File Offset: 0x0003AD64
		public SettingsPropertyNotFoundException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyNotFoundException" /> class, based on a supplied parameter.</summary>
		/// <param name="message">A string containing an exception message.</param>
		// Token: 0x06000C27 RID: 3111 RVA: 0x0003CB6C File Offset: 0x0003AD6C
		public SettingsPropertyNotFoundException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyNotFoundException" /> class, based on supplied parameters.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination of the serialized stream.</param>
		// Token: 0x06000C28 RID: 3112 RVA: 0x000275ED File Offset: 0x000257ED
		protected SettingsPropertyNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyNotFoundException" /> class, based on supplied parameters.</summary>
		/// <param name="message">A string containing an exception message.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06000C29 RID: 3113 RVA: 0x0003CB75 File Offset: 0x0003AD75
		public SettingsPropertyNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
