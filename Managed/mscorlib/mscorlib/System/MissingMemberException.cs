using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dynamically access a class member that does not exist.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200019F RID: 415
	[ComVisible(true)]
	[Serializable]
	public class MissingMemberException : MemberAccessException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class.</summary>
		// Token: 0x06001188 RID: 4488 RVA: 0x00048184 File Offset: 0x00046384
		public MissingMemberException()
			: base(Environment.GetResourceString("Attempted to access a missing member."))
		{
			base.SetErrorCode(-2146233070);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06001189 RID: 4489 RVA: 0x000481A1 File Offset: 0x000463A1
		public MissingMemberException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233070);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with a specified error message and a reference to the inner exception that is the root cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">An instance of <see cref="T:System.Exception" /> that is the cause of the current Exception. If <paramref name="inner" /> is not a null reference (Nothing in Visual Basic), then the current Exception is raised in a catch block handling <paramref name="inner" />. </param>
		// Token: 0x0600118A RID: 4490 RVA: 0x000481B5 File Offset: 0x000463B5
		public MissingMemberException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233070);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600118B RID: 4491 RVA: 0x000481CC File Offset: 0x000463CC
		protected MissingMemberException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ClassName = info.GetString("MMClassName");
			this.MemberName = info.GetString("MMMemberName");
			this.Signature = (byte[])info.GetValue("MMSignature", typeof(byte[]));
		}

		/// <summary>Gets the text string showing the class name, the member name, and the signature of the missing member.</summary>
		/// <returns>The error message string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00048224 File Offset: 0x00046424
		public override string Message
		{
			[SecuritySafeCritical]
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				return Environment.GetResourceString("Member '{0}' not found.", new object[] { this.ClassName + "." + this.MemberName + ((this.Signature != null) ? (" " + MissingMemberException.FormatSignature(this.Signature)) : "") });
			}
		}

		// Token: 0x0600118D RID: 4493
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string FormatSignature(byte[] signature);

		// Token: 0x0600118E RID: 4494 RVA: 0x0004828D File Offset: 0x0004648D
		private MissingMemberException(string className, string memberName, byte[] signature)
		{
			this.ClassName = className;
			this.MemberName = memberName;
			this.Signature = signature;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with the specified class name and member name.</summary>
		/// <param name="className">The name of the class in which access to a nonexistent member was attempted. </param>
		/// <param name="memberName">The name of the member that cannot be accessed. </param>
		// Token: 0x0600118F RID: 4495 RVA: 0x000482AA File Offset: 0x000464AA
		public MissingMemberException(string className, string memberName)
		{
			this.ClassName = className;
			this.MemberName = memberName;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the class name, the member name, the signature of the missing member, and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001190 RID: 4496 RVA: 0x000482C0 File Offset: 0x000464C0
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("MMClassName", this.ClassName, typeof(string));
			info.AddValue("MMMemberName", this.MemberName, typeof(string));
			info.AddValue("MMSignature", this.Signature, typeof(byte[]));
		}

		/// <summary>Holds the class name of the missing member.</summary>
		// Token: 0x04000A39 RID: 2617
		protected string ClassName;

		/// <summary>Holds the name of the missing member.</summary>
		// Token: 0x04000A3A RID: 2618
		protected string MemberName;

		/// <summary>Holds the signature of the missing member.</summary>
		// Token: 0x04000A3B RID: 2619
		protected byte[] Signature;
	}
}
