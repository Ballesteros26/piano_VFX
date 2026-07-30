using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	/// <summary>The exception that is thrown when an error occurs while retrieving network information.</summary>
	// Token: 0x0200060C RID: 1548
	[Serializable]
	public class NetworkInformationException : Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class.</summary>
		// Token: 0x06003194 RID: 12692 RVA: 0x00060437 File Offset: 0x0005E637
		public NetworkInformationException()
			: base(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with the specified error code.</summary>
		/// <param name="errorCode">A Win32 error code. </param>
		// Token: 0x06003195 RID: 12693 RVA: 0x0007D765 File Offset: 0x0007B965
		public NetworkInformationException(int errorCode)
			: base(errorCode)
		{
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x0007D765 File Offset: 0x0007B965
		internal NetworkInformationException(SocketError socketError)
			: base((int)socketError)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">A SerializationInfo object that contains the serialized exception data. </param>
		/// <param name="streamingContext">A StreamingContext that contains contextual information about the serialized exception. </param>
		// Token: 0x06003197 RID: 12695 RVA: 0x0007D778 File Offset: 0x0007B978
		protected NetworkInformationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the Win32 error code for this exception.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the Win32 error code.</returns>
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x0007D782 File Offset: 0x0007B982
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}
	}
}
