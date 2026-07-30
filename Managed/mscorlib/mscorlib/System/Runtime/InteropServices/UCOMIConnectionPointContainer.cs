using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IConnectionPointContainer" /> instead.</summary>
	// Token: 0x0200092D RID: 2349
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("b196b284-bab4-101a-b69c-00aa00341d07")]
	[Obsolete]
	[ComImport]
	public interface UCOMIConnectionPointContainer
	{
		/// <summary>Creates an enumerator of all the connection points supported in the connectable object, one connection point per IID.</summary>
		/// <param name="ppEnum">On successful return, contains the interface pointer of the enumerator. </param>
		// Token: 0x06005704 RID: 22276
		void EnumConnectionPoints(out UCOMIEnumConnectionPoints ppEnum);

		/// <summary>Asks the connectable object if it has a connection point for a particular IID, and if so, returns the IConnectionPoint interface pointer to that connection point.</summary>
		/// <param name="riid">A reference to the outgoing interface IID whose connection point is being requested. </param>
		/// <param name="ppCP">On successful return, contains the connection point that manages the outgoing interface <paramref name="riid" />. </param>
		// Token: 0x06005705 RID: 22277
		void FindConnectionPoint(ref Guid riid, out UCOMIConnectionPoint ppCP);
	}
}
