using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when type-loading failures occur.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E1 RID: 481
	[ComVisible(true)]
	[Serializable]
	public class TypeLoadException : SystemException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TypeLoadException" /> class.</summary>
		// Token: 0x060015FF RID: 5631 RVA: 0x000588B1 File Offset: 0x00056AB1
		public TypeLoadException()
			: base(Environment.GetResourceString("Failure has occurred while loading a type."))
		{
			base.SetErrorCode(-2146233054);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeLoadException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06001600 RID: 5632 RVA: 0x000588CE File Offset: 0x00056ACE
		public TypeLoadException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233054);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeLoadException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001601 RID: 5633 RVA: 0x000588E2 File Offset: 0x00056AE2
		public TypeLoadException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233054);
		}

		/// <summary>Gets the error message for this exception.</summary>
		/// <returns>The error message string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x000588F7 File Offset: 0x00056AF7
		public override string Message
		{
			[SecuritySafeCritical]
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00058908 File Offset: 0x00056B08
		[SecurityCritical]
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this.ClassName == null && this.ResourceId == 0)
				{
					this._message = Environment.GetResourceString("Failure has occurred while loading a type.");
					return;
				}
				if (this.AssemblyName == null)
				{
					this.AssemblyName = Environment.GetResourceString("[Unknown]");
				}
				if (this.ClassName == null)
				{
					this.ClassName = Environment.GetResourceString("[Unknown]");
				}
				string text = "Could not load type '{0}' from assembly '{1}'.";
				this._message = string.Format(CultureInfo.CurrentCulture, text, this.ClassName, this.AssemblyName, this.MessageArg);
			}
		}

		/// <summary>Gets the fully qualified name of the type that causes the exception.</summary>
		/// <returns>The fully qualified type name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00058999 File Offset: 0x00056B99
		public string TypeName
		{
			get
			{
				if (this.ClassName == null)
				{
					return string.Empty;
				}
				return this.ClassName;
			}
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x000589AF File Offset: 0x00056BAF
		private TypeLoadException(string className, string assemblyName)
			: this(className, assemblyName, null, 0)
		{
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x000589BB File Offset: 0x00056BBB
		[SecurityCritical]
		private TypeLoadException(string className, string assemblyName, string messageArg, int resourceId)
			: base(null)
		{
			base.SetErrorCode(-2146233054);
			this.ClassName = className;
			this.AssemblyName = assemblyName;
			this.MessageArg = messageArg;
			this.ResourceId = resourceId;
			this.SetMessageField();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeLoadException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is null. </exception>
		// Token: 0x06001607 RID: 5639 RVA: 0x000589F4 File Offset: 0x00056BF4
		protected TypeLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.ClassName = info.GetString("TypeLoadClassName");
			this.AssemblyName = info.GetString("TypeLoadAssemblyName");
			this.MessageArg = info.GetString("TypeLoadMessageArg");
			this.ResourceId = info.GetInt32("TypeLoadResourceID");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the class name, method name, resource ID, and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001608 RID: 5640 RVA: 0x00058A5C File Offset: 0x00056C5C
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("TypeLoadClassName", this.ClassName, typeof(string));
			info.AddValue("TypeLoadAssemblyName", this.AssemblyName, typeof(string));
			info.AddValue("TypeLoadMessageArg", this.MessageArg, typeof(string));
			info.AddValue("TypeLoadResourceID", this.ResourceId);
		}

		// Token: 0x04000BB5 RID: 2997
		private string ClassName;

		// Token: 0x04000BB6 RID: 2998
		private string AssemblyName;

		// Token: 0x04000BB7 RID: 2999
		private string MessageArg;

		// Token: 0x04000BB8 RID: 3000
		internal int ResourceId;
	}
}
