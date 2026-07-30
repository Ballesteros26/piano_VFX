using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dynamically access a method that does not exist.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A0 RID: 416
	[ComVisible(true)]
	[Serializable]
	public class MissingMethodException : MissingMemberException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class.</summary>
		// Token: 0x06001191 RID: 4497 RVA: 0x00048334 File Offset: 0x00046534
		public MissingMethodException()
			: base(Environment.GetResourceString("Attempted to access a missing method."))
		{
			base.SetErrorCode(-2146233069);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x06001192 RID: 4498 RVA: 0x00048351 File Offset: 0x00046551
		public MissingMethodException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233069);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001193 RID: 4499 RVA: 0x00048365 File Offset: 0x00046565
		public MissingMethodException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233069);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001194 RID: 4500 RVA: 0x000480DD File Offset: 0x000462DD
		protected MissingMethodException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the text string showing the class name, the method name, and the signature of the missing method. This property is read-only.</summary>
		/// <returns>The error message string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0004837C File Offset: 0x0004657C
		public override string Message
		{
			[SecuritySafeCritical]
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				string text = this.ClassName + "." + this.MemberName;
				if (!string.IsNullOrEmpty(this.signature))
				{
					text = string.Format(CultureInfo.InvariantCulture, this.signature, text);
				}
				if (!string.IsNullOrEmpty(this._message))
				{
					text = text + " Due to: " + this._message;
				}
				return text;
			}
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00048151 File Offset: 0x00046351
		private MissingMethodException(string className, string methodName, byte[] signature)
		{
			this.ClassName = className;
			this.MemberName = methodName;
			this.Signature = signature;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMethodException" /> class with the specified class name and method name.</summary>
		/// <param name="className">The name of the class in which access to a nonexistent method was attempted. </param>
		/// <param name="methodName">The name of the method that cannot be accessed. </param>
		// Token: 0x06001197 RID: 4503 RVA: 0x0004816E File Offset: 0x0004636E
		public MissingMethodException(string className, string methodName)
		{
			this.ClassName = className;
			this.MemberName = methodName;
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000483EE File Offset: 0x000465EE
		private MissingMethodException(string className, string methodName, string signature, string message)
			: base(message)
		{
			this.ClassName = className;
			this.MemberName = methodName;
			this.signature = signature;
		}

		// Token: 0x04000A3C RID: 2620
		[NonSerialized]
		private string signature;
	}
}
