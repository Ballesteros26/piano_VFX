using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when the value of an argument is outside the allowable range of values as defined by the invoked method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000128 RID: 296
	[ComVisible(true)]
	[Serializable]
	public class ArgumentOutOfRangeException : ArgumentException, ISerializable
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00032A1F File Offset: 0x00030C1F
		private static string RangeMessage
		{
			get
			{
				if (ArgumentOutOfRangeException._rangeMessage == null)
				{
					ArgumentOutOfRangeException._rangeMessage = Environment.GetResourceString("Specified argument was out of the range of valid values.");
				}
				return ArgumentOutOfRangeException._rangeMessage;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class.</summary>
		// Token: 0x06000A54 RID: 2644 RVA: 0x00032A42 File Offset: 0x00030C42
		public ArgumentOutOfRangeException()
			: base(ArgumentOutOfRangeException.RangeMessage)
		{
			base.SetErrorCode(-2146233086);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that causes this exception. </param>
		// Token: 0x06000A55 RID: 2645 RVA: 0x00032A5A File Offset: 0x00030C5A
		public ArgumentOutOfRangeException(string paramName)
			: base(ArgumentOutOfRangeException.RangeMessage, paramName)
		{
			base.SetErrorCode(-2146233086);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with the name of the parameter that causes this exception and a specified error message.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception. </param>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000A56 RID: 2646 RVA: 0x00032A73 File Offset: 0x00030C73
		public ArgumentOutOfRangeException(string paramName, string message)
			: base(message, paramName)
		{
			base.SetErrorCode(-2146233086);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with a specified error message and the exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for this exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000A57 RID: 2647 RVA: 0x00032A88 File Offset: 0x00030C88
		public ArgumentOutOfRangeException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233086);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with the parameter name, the value of the argument, and a specified error message.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception. </param>
		/// <param name="actualValue">The value of the argument that causes this exception. </param>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000A58 RID: 2648 RVA: 0x00032A9D File Offset: 0x00030C9D
		public ArgumentOutOfRangeException(string paramName, object actualValue, string message)
			: base(message, paramName)
		{
			this.m_actualValue = actualValue;
			base.SetErrorCode(-2146233086);
		}

		/// <summary>Gets the error message and the string representation of the invalid argument value, or only the error message if the argument value is null.</summary>
		/// <returns>The text message for this exception. The value of this property takes one of two forms, as follows.Condition Value The <paramref name="actualValue" /> is null. The <paramref name="message" /> string passed to the constructor. The <paramref name="actualValue" /> is not null. The <paramref name="message" /> string appended with the string representation of the invalid argument value. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00032ABC File Offset: 0x00030CBC
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (this.m_actualValue == null)
				{
					return message;
				}
				string resourceString = Environment.GetResourceString("Actual value was {0}.", new object[] { this.m_actualValue.ToString() });
				if (message == null)
				{
					return resourceString;
				}
				return message + Environment.NewLine + resourceString;
			}
		}

		/// <summary>Gets the argument value that causes this exception.</summary>
		/// <returns>An Object that contains the value of the parameter that caused the current <see cref="T:System.Exception" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00032B0A File Offset: 0x00030D0A
		public virtual object ActualValue
		{
			get
			{
				return this.m_actualValue;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the invalid argument value and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">An object that describes the source or destination of the serialized data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000A5B RID: 2651 RVA: 0x00032B12 File Offset: 0x00030D12
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("ActualValue", this.m_actualValue, typeof(object));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">An object that describes the source or destination of the serialized data. </param>
		// Token: 0x06000A5C RID: 2652 RVA: 0x00032B45 File Offset: 0x00030D45
		protected ArgumentOutOfRangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_actualValue = info.GetValue("ActualValue", typeof(object));
		}

		// Token: 0x0400079D RID: 1949
		private static volatile string _rangeMessage;

		// Token: 0x0400079E RID: 1950
		private object m_actualValue;
	}
}
