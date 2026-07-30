using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains statistical information about an open storage, stream, or byte-array object.</summary>
	// Token: 0x02000984 RID: 2436
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		/// <summary>Represents a pointer to a null-terminated string containing the name of the object described by this structure.</summary>
		// Token: 0x04002E31 RID: 11825
		public string pwcsName;

		/// <summary>Indicates the type of storage object, which is one of the values from the STGTY enumeration.</summary>
		// Token: 0x04002E32 RID: 11826
		public int type;

		/// <summary>Specifies the size, in bytes, of the stream or byte array.</summary>
		// Token: 0x04002E33 RID: 11827
		public long cbSize;

		/// <summary>Indicates the last modification time for this storage, stream, or byte array.</summary>
		// Token: 0x04002E34 RID: 11828
		public FILETIME mtime;

		/// <summary>Indicates the creation time for this storage, stream, or byte array.</summary>
		// Token: 0x04002E35 RID: 11829
		public FILETIME ctime;

		/// <summary>Specifies the last access time for this storage, stream, or byte array. </summary>
		// Token: 0x04002E36 RID: 11830
		public FILETIME atime;

		/// <summary>Indicates the access mode that was specified when the object was opened.</summary>
		// Token: 0x04002E37 RID: 11831
		public int grfMode;

		/// <summary>Indicates the types of region locking supported by the stream or byte array.</summary>
		// Token: 0x04002E38 RID: 11832
		public int grfLocksSupported;

		/// <summary>Indicates the class identifier for the storage object.</summary>
		// Token: 0x04002E39 RID: 11833
		public Guid clsid;

		/// <summary>Indicates the current state bits of the storage object (the value most recently set by the IStorage::SetStateBits method).</summary>
		// Token: 0x04002E3A RID: 11834
		public int grfStateBits;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002E3B RID: 11835
		public int reserved;
	}
}
