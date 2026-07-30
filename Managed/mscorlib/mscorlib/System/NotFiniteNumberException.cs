using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when a floating-point value is positive infinity, negative infinity, or Not-a-Number (NaN).</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A3 RID: 419
	[ComVisible(true)]
	[Serializable]
	public class NotFiniteNumberException : ArithmeticException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class.</summary>
		// Token: 0x060011A0 RID: 4512 RVA: 0x0004847B File Offset: 0x0004667B
		public NotFiniteNumberException()
			: base(Environment.GetResourceString("Number encountered was not a finite quantity."))
		{
			this._offendingNumber = 0.0;
			base.SetErrorCode(-2146233048);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with the invalid number.</summary>
		/// <param name="offendingNumber">The value of the argument that caused the exception. </param>
		// Token: 0x060011A1 RID: 4513 RVA: 0x000484A7 File Offset: 0x000466A7
		public NotFiniteNumberException(double offendingNumber)
		{
			this._offendingNumber = offendingNumber;
			base.SetErrorCode(-2146233048);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060011A2 RID: 4514 RVA: 0x000484C1 File Offset: 0x000466C1
		public NotFiniteNumberException(string message)
			: base(message)
		{
			this._offendingNumber = 0.0;
			base.SetErrorCode(-2146233048);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message and the invalid number.</summary>
		/// <param name="message">The message that describes the error. </param>
		/// <param name="offendingNumber">The value of the argument that caused the exception. </param>
		// Token: 0x060011A3 RID: 4515 RVA: 0x000484E4 File Offset: 0x000466E4
		public NotFiniteNumberException(string message, double offendingNumber)
			: base(message)
		{
			this._offendingNumber = offendingNumber;
			base.SetErrorCode(-2146233048);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message and a reference to the inner exception that is root cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060011A4 RID: 4516 RVA: 0x000484FF File Offset: 0x000466FF
		public NotFiniteNumberException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233048);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message, the invalid number, and a reference to the inner exception that is root cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="offendingNumber">The value of the argument that caused the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060011A5 RID: 4517 RVA: 0x00048514 File Offset: 0x00046714
		public NotFiniteNumberException(string message, double offendingNumber, Exception innerException)
			: base(message, innerException)
		{
			this._offendingNumber = offendingNumber;
			base.SetErrorCode(-2146233048);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060011A6 RID: 4518 RVA: 0x00048530 File Offset: 0x00046730
		protected NotFiniteNumberException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._offendingNumber = (double)info.GetInt32("OffendingNumber");
		}

		/// <summary>Gets the invalid number that is a positive infinity, a negative infinity, or Not-a-Number (NaN).</summary>
		/// <returns>The invalid number.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x0004854C File Offset: 0x0004674C
		public double OffendingNumber
		{
			get
			{
				return this._offendingNumber;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the invalid number and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060011A8 RID: 4520 RVA: 0x00048554 File Offset: 0x00046754
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("OffendingNumber", this._offendingNumber, typeof(int));
		}

		// Token: 0x04000A3D RID: 2621
		private double _offendingNumber;
	}
}
