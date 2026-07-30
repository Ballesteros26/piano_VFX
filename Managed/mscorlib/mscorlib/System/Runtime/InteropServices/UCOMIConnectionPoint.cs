using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IConnectionPoint" /> instead.</summary>
	// Token: 0x0200092C RID: 2348
	[Guid("b196b286-bab4-101a-b69c-00aa00341d07")]
	[Obsolete]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIConnectionPoint
	{
		/// <summary>Returns the IID of the outgoing interface managed by this connection point.</summary>
		/// <param name="pIID">On successful return, contains the IID of the outgoing interface managed by this connection point. </param>
		// Token: 0x060056FF RID: 22271
		void GetConnectionInterface(out Guid pIID);

		/// <summary>Retrieves the IConnectionPointContainer interface pointer to the connectable object that conceptually owns this connection point.</summary>
		/// <param name="ppCPC">On successful return, contains the connectable object's IConnectionPointContainer interface. </param>
		// Token: 0x06005700 RID: 22272
		void GetConnectionPointContainer(out UCOMIConnectionPointContainer ppCPC);

		/// <summary>Establishes an advisory connection between the connection point and the caller's sink object.</summary>
		/// <param name="pUnkSink">Reference to the sink to receive calls for the outgoing interface managed by this connection point. </param>
		/// <param name="pdwCookie">On successful return, contains the connection cookie. </param>
		// Token: 0x06005701 RID: 22273
		void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

		/// <summary>Terminates an advisory connection previously established through <see cref="M:System.Runtime.InteropServices.UCOMIConnectionPoint.Advise(System.Object,System.Int32@)" />.</summary>
		/// <param name="dwCookie">The connection cookie previously returned from <see cref="M:System.Runtime.InteropServices.UCOMIConnectionPoint.Advise(System.Object,System.Int32@)" />. </param>
		// Token: 0x06005702 RID: 22274
		void Unadvise(int dwCookie);

		/// <summary>Creates an enumerator object for iteration through the connections that exist to this connection point.</summary>
		/// <param name="ppEnum">On successful return, contains the newly created enumerator. </param>
		// Token: 0x06005703 RID: 22275
		void EnumConnections(out UCOMIEnumConnections ppEnum);
	}
}
