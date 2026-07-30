using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when one of the arguments provided to a method is not valid.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000126 RID: 294
	[ComVisible(true)]
	[Serializable]
	public class ArgumentException : SystemException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class.</summary>
		// Token: 0x06000A45 RID: 2629 RVA: 0x00032894 File Offset: 0x00030A94
		public ArgumentException()
			: base(Environment.GetResourceString("Value does not fall within the expected range."))
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06000A46 RID: 2630 RVA: 0x000328B1 File Offset: 0x00030AB1
		public ArgumentException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000A47 RID: 2631 RVA: 0x000328C5 File Offset: 0x00030AC5
		public ArgumentException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message, the parameter name, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="paramName">The name of the parameter that caused the current exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000A48 RID: 2632 RVA: 0x000328DA File Offset: 0x00030ADA
		public ArgumentException(string message, string paramName, Exception innerException)
			: base(message, innerException)
		{
			this.m_paramName = paramName;
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="paramName">The name of the parameter that caused the current exception. </param>
		// Token: 0x06000A49 RID: 2633 RVA: 0x000328F6 File Offset: 0x00030AF6
		public ArgumentException(string message, string paramName)
			: base(message)
		{
			this.m_paramName = paramName;
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000A4A RID: 2634 RVA: 0x00032911 File Offset: 0x00030B11
		protected ArgumentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_paramName = info.GetString("ParamName");
		}

		/// <summary>Gets the error message and the parameter name, or only the error message if no parameter name is set.</summary>
		/// <returns>A text string describing the details of the exception. The value of this property takes one of two forms: Condition Value The <paramref name="paramName" /> is a null reference (Nothing in Visual Basic) or of zero length. The <paramref name="message" /> string passed to the constructor. The <paramref name="paramName" /> is not null reference (Nothing in Visual Basic) and it has a length greater than zero. The <paramref name="message" /> string appended with the name of the invalid parameter. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0003292C File Offset: 0x00030B2C
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (!string.IsNullOrEmpty(this.m_paramName))
				{
					string resourceString = Environment.GetResourceString("Parameter name: {0}", new object[] { this.m_paramName });
					return message + Environment.NewLine + resourceString;
				}
				return message;
			}
		}

		/// <summary>Gets the name of the parameter that causes this exception.</summary>
		/// <returns>The parameter name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00032975 File Offset: 0x00030B75
		public virtual string ParamName
		{
			get
			{
				return this.m_paramName;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is a null reference (Nothing in Visual Basic). </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000A4D RID: 2637 RVA: 0x0003297D File Offset: 0x00030B7D
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("ParamName", this.m_paramName, typeof(string));
		}

		// Token: 0x0400079C RID: 1948
		private string m_paramName;
	}
}
