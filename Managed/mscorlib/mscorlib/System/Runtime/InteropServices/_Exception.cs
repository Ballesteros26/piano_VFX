using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>Exposes the public members of the <see cref="T:System.Exception" /> class to unmanaged code.</summary>
	// Token: 0x02000943 RID: 2371
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[CLSCompliant(false)]
	[Guid("b36b5c63-42ef-38bc-a07e-0b34c98f164a")]
	public interface _Exception
	{
		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.HelpLink" /> property.</summary>
		/// <returns>The Uniform Resource Name (URN) or Uniform Resource Locator (URL) to a help file.</returns>
		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x060057DA RID: 22490
		// (set) Token: 0x060057DB RID: 22491
		string HelpLink { get; set; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.InnerException" /> property.</summary>
		/// <returns>An instance of <see cref="T:System.Exception" /> that describes the error that caused the current exception. The <see cref="P:System.Exception.InnerException" /> property returns the same value that was passed to the constructor, or a null reference (Nothing in Visual Basic) if the inner exception value was not supplied to the constructor. This property is read-only.</returns>
		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x060057DC RID: 22492
		Exception InnerException { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.Message" /> property.</summary>
		/// <returns>The error message that explains the reason for the exception, or an empty string("").</returns>
		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x060057DD RID: 22493
		string Message { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.Source" /> property.</summary>
		/// <returns>The name of the application or the object that causes the error.</returns>
		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x060057DE RID: 22494
		// (set) Token: 0x060057DF RID: 22495
		string Source { get; set; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.StackTrace" /> property.</summary>
		/// <returns>A string that describes the contents of the call stack, with the most recent method call appearing first.</returns>
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x060057E0 RID: 22496
		string StackTrace { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Exception.TargetSite" /> property.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> object that threw the current exception.</returns>
		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x060057E1 RID: 22497
		MethodBase TargetSite { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		// Token: 0x060057E2 RID: 22498
		bool Equals(object obj);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Exception.GetBaseException" /> method.</summary>
		/// <returns>The first exception thrown in a chain of exceptions. If the <see cref="P:System.Exception.InnerException" /> property of the current exception is a null reference (Nothing in Visual Basic), this property returns the current exception.</returns>
		// Token: 0x060057E3 RID: 22499
		Exception GetBaseException();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>The hash code for the current instance.</returns>
		// Token: 0x060057E4 RID: 22500
		int GetHashCode();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Exception.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains contextual information about the source or destination. </param>
		// Token: 0x060057E5 RID: 22501
		void GetObjectData(SerializationInfo info, StreamingContext context);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Exception.GetType" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the exact runtime type of the current instance.</returns>
		// Token: 0x060057E6 RID: 22502
		Type GetType();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Exception.ToString" /> method.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Exception" /> object.</returns>
		// Token: 0x060057E7 RID: 22503
		string ToString();
	}
}
