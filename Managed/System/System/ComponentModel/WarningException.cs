using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Specifies an exception that is handled as a warning instead of an error.</summary>
	// Token: 0x020002F4 RID: 756
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class WarningException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class. </summary>
		// Token: 0x06001854 RID: 6228 RVA: 0x0006035D File Offset: 0x0005E55D
		public WarningException()
			: this(null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message and no Help file.</summary>
		/// <param name="message">The message to display to the end user. </param>
		// Token: 0x06001855 RID: 6229 RVA: 0x00060368 File Offset: 0x0005E568
		public WarningException(string message)
			: this(message, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message, and with access to the specified Help file.</summary>
		/// <param name="message">The message to display to the end user. </param>
		/// <param name="helpUrl">The Help file to display if the user requests help. </param>
		// Token: 0x06001856 RID: 6230 RVA: 0x00060373 File Offset: 0x0005E573
		public WarningException(string message, string helpUrl)
			: this(message, helpUrl, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified detailed description and the specified exception. </summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x06001857 RID: 6231 RVA: 0x00039C0D File Offset: 0x00037E0D
		public WarningException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message, and with access to the specified Help file and topic.</summary>
		/// <param name="message">The message to display to the end user. </param>
		/// <param name="helpUrl">The Help file to display if the user requests help. </param>
		/// <param name="helpTopic">The Help topic to display if the user requests help. </param>
		// Token: 0x06001858 RID: 6232 RVA: 0x0006037E File Offset: 0x0005E57E
		public WarningException(string message, string helpUrl, string helpTopic)
			: base(message)
		{
			this.helpUrl = helpUrl;
			this.helpTopic = helpTopic;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06001859 RID: 6233 RVA: 0x00060398 File Offset: 0x0005E598
		protected WarningException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.helpUrl = (string)info.GetValue("helpUrl", typeof(string));
			this.helpTopic = (string)info.GetValue("helpTopic", typeof(string));
		}

		/// <summary>Gets the Help file associated with the warning.</summary>
		/// <returns>The Help file associated with the warning.</returns>
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x000603ED File Offset: 0x0005E5ED
		public string HelpUrl
		{
			get
			{
				return this.helpUrl;
			}
		}

		/// <summary>Gets the Help topic associated with the warning.</summary>
		/// <returns>The Help topic associated with the warning.</returns>
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x000603F5 File Offset: 0x0005E5F5
		public string HelpTopic
		{
			get
			{
				return this.helpTopic;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the parameter name and additional exception information.</summary>
		/// <param name="info">Stores the data that was being used to serialize or deserialize the object that the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> was serializing or deserializing. </param>
		/// <param name="context">Describes the source and destination of the stream that generated the exception, as well as a means for serialization to retain that context and an additional caller-defined context. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x0600185C RID: 6236 RVA: 0x000603FD File Offset: 0x0005E5FD
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("helpUrl", this.helpUrl);
			info.AddValue("helpTopic", this.helpTopic);
			base.GetObjectData(info, context);
		}

		// Token: 0x04001421 RID: 5153
		private readonly string helpUrl;

		// Token: 0x04001422 RID: 5154
		private readonly string helpTopic;
	}
}
