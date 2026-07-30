using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dynamically access a field that does not exist.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200019E RID: 414
	[ComVisible(true)]
	[Serializable]
	public class MissingFieldException : MissingMemberException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class.</summary>
		// Token: 0x06001181 RID: 4481 RVA: 0x00048097 File Offset: 0x00046297
		public MissingFieldException()
			: base(Environment.GetResourceString("Attempted to access a non-existing field."))
		{
			base.SetErrorCode(-2146233071);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x06001182 RID: 4482 RVA: 0x000480B4 File Offset: 0x000462B4
		public MissingFieldException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233071);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001183 RID: 4483 RVA: 0x000480C8 File Offset: 0x000462C8
		public MissingFieldException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233071);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001184 RID: 4484 RVA: 0x000480DD File Offset: 0x000462DD
		protected MissingFieldException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the text string showing the signature of the missing field, the class name, and the field name. This property is read-only.</summary>
		/// <returns>The error message string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x000480E8 File Offset: 0x000462E8
		public override string Message
		{
			[SecuritySafeCritical]
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				return Environment.GetResourceString("Field '{0}' not found.", new object[] { ((this.Signature != null) ? (MissingMemberException.FormatSignature(this.Signature) + " ") : "") + this.ClassName + "." + this.MemberName });
			}
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00048151 File Offset: 0x00046351
		private MissingFieldException(string className, string fieldName, byte[] signature)
		{
			this.ClassName = className;
			this.MemberName = fieldName;
			this.Signature = signature;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with the specified class name and field name.</summary>
		/// <param name="className">The name of the class in which access to a nonexistent field was attempted. </param>
		/// <param name="fieldName">The name of the field that cannot be accessed. </param>
		// Token: 0x06001187 RID: 4487 RVA: 0x0004816E File Offset: 0x0004636E
		public MissingFieldException(string className, string fieldName)
		{
			this.ClassName = className;
			this.MemberName = fieldName;
		}
	}
}
