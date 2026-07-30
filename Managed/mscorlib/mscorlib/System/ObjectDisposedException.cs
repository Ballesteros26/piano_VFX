using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when an operation is performed on a disposed object.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A9 RID: 425
	[ComVisible(true)]
	[Serializable]
	public class ObjectDisposedException : InvalidOperationException
	{
		// Token: 0x060011E1 RID: 4577 RVA: 0x000496AE File Offset: 0x000478AE
		private ObjectDisposedException()
			: this(null, Environment.GetResourceString("Cannot access a disposed object."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with a string containing the name of the disposed object.</summary>
		/// <param name="objectName">A string containing the name of the disposed object. </param>
		// Token: 0x060011E2 RID: 4578 RVA: 0x000496C1 File Offset: 0x000478C1
		public ObjectDisposedException(string objectName)
			: this(objectName, Environment.GetResourceString("Cannot access a disposed object."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with the specified object name and message.</summary>
		/// <param name="objectName">The name of the disposed object. </param>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x060011E3 RID: 4579 RVA: 0x000496D4 File Offset: 0x000478D4
		public ObjectDisposedException(string objectName, string message)
			: base(message)
		{
			base.SetErrorCode(-2146232798);
			this.objectName = objectName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If <paramref name="innerException" /> is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x060011E4 RID: 4580 RVA: 0x000496EF File Offset: 0x000478EF
		public ObjectDisposedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146232798);
		}

		/// <summary>Gets the message that describes the error.</summary>
		/// <returns>A string that describes the error.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00049704 File Offset: 0x00047904
		public override string Message
		{
			get
			{
				string text = this.ObjectName;
				if (text == null || text.Length == 0)
				{
					return base.Message;
				}
				string resourceString = Environment.GetResourceString("Object name: '{0}'.", new object[] { text });
				return base.Message + Environment.NewLine + resourceString;
			}
		}

		/// <summary>Gets the name of the disposed object.</summary>
		/// <returns>A string containing the name of the disposed object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00049750 File Offset: 0x00047950
		public string ObjectName
		{
			get
			{
				if (this.objectName == null && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
				{
					return string.Empty;
				}
				return this.objectName;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x060011E7 RID: 4583 RVA: 0x0004976D File Offset: 0x0004796D
		protected ObjectDisposedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.objectName = info.GetString("ObjectName");
		}

		/// <summary>Retrieves the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060011E8 RID: 4584 RVA: 0x00049788 File Offset: 0x00047988
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ObjectName", this.ObjectName, typeof(string));
		}

		// Token: 0x04000A49 RID: 2633
		private string objectName;
	}
}
