using System;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> class encapsulates a pointer to the opaque data used to bind an authenticated transaction to a secure channel.</summary>
	// Token: 0x02000383 RID: 899
	public abstract class ChannelBinding : SafeHandleZeroOrMinusOneIsInvalid
	{
		/// <summary>The <see cref="P:System.Security.Authentication.ExtendedProtection.ChannelBinding.Size" /> property gets the size, in bytes, of the channel binding token associated with the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> instance.</summary>
		/// <returns>The size, in bytes, of the channel binding token in the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> instance.</returns>
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001B5D RID: 7005
		public abstract int Size { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> class.</summary>
		// Token: 0x06001B5E RID: 7006 RVA: 0x0006D52F File Offset: 0x0006B72F
		protected ChannelBinding()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> class.</summary>
		/// <param name="ownsHandle">A Boolean value that indicates if the application owns the safe handle to a native memory region containing the byte data that would be passed to native calls that provide extended protection for integrated windows authentication.</param>
		// Token: 0x06001B5F RID: 7007 RVA: 0x0006D538 File Offset: 0x0006B738
		protected ChannelBinding(bool ownsHandle)
			: base(ownsHandle)
		{
		}
	}
}
