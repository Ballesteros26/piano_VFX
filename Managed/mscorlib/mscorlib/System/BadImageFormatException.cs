using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System
{
	/// <summary>The exception that is thrown when the file image of a dynamic link library (DLL) or an executable program is invalid. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000131 RID: 305
	[ComVisible(true)]
	[Serializable]
	public class BadImageFormatException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class.</summary>
		// Token: 0x06000AC0 RID: 2752 RVA: 0x00033AB5 File Offset: 0x00031CB5
		public BadImageFormatException()
			: base(Environment.GetResourceString("Format of the executable (.exe) or library (.dll) is invalid."))
		{
			base.SetErrorCode(-2147024885);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000AC1 RID: 2753 RVA: 0x00033AD2 File Offset: 0x00031CD2
		public BadImageFormatException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024885);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000AC2 RID: 2754 RVA: 0x00033AE6 File Offset: 0x00031CE6
		public BadImageFormatException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147024885);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message and file name.</summary>
		/// <param name="message">A message that describes the error. </param>
		/// <param name="fileName">The full name of the file with the invalid image. </param>
		// Token: 0x06000AC3 RID: 2755 RVA: 0x00033AFB File Offset: 0x00031CFB
		public BadImageFormatException(string message, string fileName)
			: base(message)
		{
			base.SetErrorCode(-2147024885);
			this._fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="fileName">The full name of the file with the invalid image. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000AC4 RID: 2756 RVA: 0x00033B16 File Offset: 0x00031D16
		public BadImageFormatException(string message, string fileName, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147024885);
			this._fileName = fileName;
		}

		/// <summary>Gets the error message and the name of the file that caused this exception.</summary>
		/// <returns>A string containing the error message and the name of the file that caused this exception.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00033B32 File Offset: 0x00031D32
		public override string Message
		{
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00033B40 File Offset: 0x00031D40
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this._fileName == null && base.HResult == -2146233088)
				{
					this._message = Environment.GetResourceString("Format of the executable (.exe) or library (.dll) is invalid.");
					return;
				}
				this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, base.HResult);
			}
		}

		/// <summary>Gets the name of the file that causes this exception.</summary>
		/// <returns>The name of the file with the invalid image, or a null reference if no file name was passed to the constructor for the current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00033B92 File Offset: 0x00031D92
		public string FileName
		{
			get
			{
				return this._fileName;
			}
		}

		/// <summary>Returns the fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</summary>
		/// <returns>A string containing the fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06000AC8 RID: 2760 RVA: 0x00033B9C File Offset: 0x00031D9C
		public override string ToString()
		{
			string text = base.GetType().FullName + ": " + this.Message;
			if (this._fileName != null && this._fileName.Length != 0)
			{
				text = text + Environment.NewLine + Environment.GetResourceString("File name: '{0}'", new object[] { this._fileName });
			}
			if (base.InnerException != null)
			{
				text = text + " ---> " + base.InnerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			try
			{
				if (this.FusionLog != null)
				{
					if (text == null)
					{
						text = " ";
					}
					text += Environment.NewLine;
					text += Environment.NewLine;
					text += this.FusionLog;
				}
			}
			catch (SecurityException)
			{
			}
			return text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06000AC9 RID: 2761 RVA: 0x00033C88 File Offset: 0x00031E88
		protected BadImageFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._fileName = info.GetString("BadImageFormat_FileName");
			try
			{
				this._fusionLog = info.GetString("BadImageFormat_FusionLog");
			}
			catch
			{
				this._fusionLog = null;
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00033CDC File Offset: 0x00031EDC
		private BadImageFormatException(string fileName, string fusionLog, int hResult)
			: base(null)
		{
			base.SetErrorCode(hResult);
			this._fileName = fileName;
			this._fusionLog = fusionLog;
			this.SetMessageField();
		}

		/// <summary>Gets the log file that describes why an assembly load failed.</summary>
		/// <returns>A String containing errors reported by the assembly cache.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00033D00 File Offset: 0x00031F00
		public string FusionLog
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)]
			get
			{
				return this._fusionLog;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name, assembly cache log, and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06000ACC RID: 2764 RVA: 0x00033D08 File Offset: 0x00031F08
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("BadImageFormat_FileName", this._fileName, typeof(string));
			try
			{
				info.AddValue("BadImageFormat_FusionLog", this.FusionLog, typeof(string));
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x040007BB RID: 1979
		private string _fileName;

		// Token: 0x040007BC RID: 1980
		private string _fusionLog;
	}
}
