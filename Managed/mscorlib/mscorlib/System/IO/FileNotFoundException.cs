using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
	/// <summary>The exception that is thrown when an attempt to access a file that does not exist on disk fails.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000394 RID: 916
	[ComVisible(true)]
	[Serializable]
	public class FileNotFoundException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with its message string set to a system-supplied message and its HRESULT set to COR_E_FILENOTFOUND.</summary>
		// Token: 0x06002AB4 RID: 10932 RVA: 0x00098746 File Offset: 0x00096946
		public FileNotFoundException()
			: base(Environment.GetResourceString("Unable to find the specified file."))
		{
			base.SetErrorCode(-2147024894);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with its message string set to <paramref name="message" /> and its HRESULT set to COR_E_FILENOTFOUND.</summary>
		/// <param name="message">A description of the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06002AB5 RID: 10933 RVA: 0x00098763 File Offset: 0x00096963
		public FileNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024894);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A description of the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002AB6 RID: 10934 RVA: 0x00098777 File Offset: 0x00096977
		public FileNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024894);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with its message string set to <paramref name="message" />, specifying the file name that cannot be found, and its HRESULT set to COR_E_FILENOTFOUND.</summary>
		/// <param name="message">A description of the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="fileName">The full name of the file with the invalid image. </param>
		// Token: 0x06002AB7 RID: 10935 RVA: 0x0009878C File Offset: 0x0009698C
		public FileNotFoundException(string message, string fileName)
			: base(message)
		{
			base.SetErrorCode(-2147024894);
			this._fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="fileName">The full name of the file with the invalid image. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002AB8 RID: 10936 RVA: 0x000987A7 File Offset: 0x000969A7
		public FileNotFoundException(string message, string fileName, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024894);
			this._fileName = fileName;
		}

		/// <summary>Gets the error message that explains the reason for the exception.</summary>
		/// <returns>The error message.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x000987C3 File Offset: 0x000969C3
		public override string Message
		{
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000987D4 File Offset: 0x000969D4
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this._fileName == null && base.HResult == -2146233088)
				{
					this._message = Environment.GetResourceString("Unable to find the specified file.");
					return;
				}
				if (this._fileName != null)
				{
					this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, base.HResult);
				}
			}
		}

		/// <summary>Gets the name of the file that cannot be found.</summary>
		/// <returns>The name of the file, or null if no file name was passed to the constructor for this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002ABB RID: 10939 RVA: 0x0009882E File Offset: 0x00096A2E
		public string FileName
		{
			get
			{
				return this._fileName;
			}
		}

		/// <summary>Returns the fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</summary>
		/// <returns>The fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06002ABC RID: 10940 RVA: 0x00098838 File Offset: 0x00096A38
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

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">An object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">An object that contains contextual information about the source or destination. </param>
		// Token: 0x06002ABD RID: 10941 RVA: 0x00098924 File Offset: 0x00096B24
		protected FileNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._fileName = info.GetString("FileNotFound_FileName");
			try
			{
				this._fusionLog = info.GetString("FileNotFound_FusionLog");
			}
			catch
			{
				this._fusionLog = null;
			}
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x00098978 File Offset: 0x00096B78
		private FileNotFoundException(string fileName, string fusionLog, int hResult)
			: base(null)
		{
			base.SetErrorCode(hResult);
			this._fileName = fileName;
			this._fusionLog = fusionLog;
			this.SetMessageField();
		}

		/// <summary>Gets the log file that describes why loading of an assembly failed.</summary>
		/// <returns>The errors reported by the assembly cache.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x0009899C File Offset: 0x00096B9C
		public string FusionLog
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)]
			get
			{
				return this._fusionLog;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The object that contains contextual information about the source or destination. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06002AC0 RID: 10944 RVA: 0x000989A4 File Offset: 0x00096BA4
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileNotFound_FileName", this._fileName, typeof(string));
			try
			{
				info.AddValue("FileNotFound_FusionLog", this.FusionLog, typeof(string));
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x04001684 RID: 5764
		private string _fileName;

		// Token: 0x04001685 RID: 5765
		private string _fusionLog;
	}
}
