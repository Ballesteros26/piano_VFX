using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>The exception that is thrown when an attempt to check out a file that is checked into a source code management program is canceled or fails.</summary>
	// Token: 0x02000307 RID: 775
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[Serializable]
	public class CheckoutException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with no associated message or error code.</summary>
		// Token: 0x060018CD RID: 6349 RVA: 0x00069363 File Offset: 0x00067563
		public CheckoutException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified message.</summary>
		/// <param name="message">A message describing the exception. </param>
		// Token: 0x060018CE RID: 6350 RVA: 0x0006936B File Offset: 0x0006756B
		public CheckoutException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified message and error code.</summary>
		/// <param name="message">A message describing the exception. </param>
		/// <param name="errorCode">The error code to pass. </param>
		// Token: 0x060018CF RID: 6351 RVA: 0x00069374 File Offset: 0x00067574
		public CheckoutException(string message, int errorCode)
			: base(message, errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class using the specified serialization data and context. </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x060018D0 RID: 6352 RVA: 0x0006937E File Offset: 0x0006757E
		protected CheckoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class with the specified detailed description and the specified exception. </summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x060018D1 RID: 6353 RVA: 0x00069388 File Offset: 0x00067588
		public CheckoutException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CheckoutException" /> class that specifies that the check out was canceled. This field is read-only.</summary>
		// Token: 0x0400144F RID: 5199
		public static readonly CheckoutException Canceled = new CheckoutException(global::SR.GetString("The checkout was canceled by the user."), -2147467260);
	}
}
