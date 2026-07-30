using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown as a wrapper around the exception thrown by the class initializer. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E0 RID: 480
	[ComVisible(true)]
	[Serializable]
	public sealed class TypeInitializationException : SystemException
	{
		// Token: 0x060015F9 RID: 5625 RVA: 0x000587FB File Offset: 0x000569FB
		private TypeInitializationException()
			: base(Environment.GetResourceString("Type constructor threw an exception."))
		{
			base.SetErrorCode(-2146233036);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00058818 File Offset: 0x00056A18
		private TypeInitializationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233036);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeInitializationException" /> class with the default error message, the specified type name, and a reference to the inner exception that is the root cause of this exception.</summary>
		/// <param name="fullTypeName">The fully qualified name of the type that fails to initialize. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060015FB RID: 5627 RVA: 0x0005882C File Offset: 0x00056A2C
		public TypeInitializationException(string fullTypeName, Exception innerException)
			: base(Environment.GetResourceString("The type initializer for '{0}' threw an exception.", new object[] { fullTypeName }), innerException)
		{
			this._typeName = fullTypeName;
			base.SetErrorCode(-2146233036);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0005885B File Offset: 0x00056A5B
		internal TypeInitializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._typeName = info.GetString("TypeName");
		}

		/// <summary>Gets the fully qualified name of the type that fails to initialize.</summary>
		/// <returns>The fully qualified name of the type that fails to initialize.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x00058876 File Offset: 0x00056A76
		public string TypeName
		{
			get
			{
				if (this._typeName == null)
				{
					return string.Empty;
				}
				return this._typeName;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the type name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060015FE RID: 5630 RVA: 0x0005888C File Offset: 0x00056A8C
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("TypeName", this.TypeName, typeof(string));
		}

		// Token: 0x04000BB4 RID: 2996
		private string _typeName;
	}
}
