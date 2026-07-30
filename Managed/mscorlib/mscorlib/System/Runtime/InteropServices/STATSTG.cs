using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.STATSTG" /> instead.</summary>
	// Token: 0x02000924 RID: 2340
	[Obsolete]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		/// <summary>Pointer to a null-terminated string containing the name of the object described by this structure.</summary>
		// Token: 0x04002DD5 RID: 11733
		public string pwcsName;

		/// <summary>Indicates the type of storage object which is one of the values from the STGTY enumeration.</summary>
		// Token: 0x04002DD6 RID: 11734
		public int type;

		/// <summary>Specifies the size in bytes of the stream or byte array.</summary>
		// Token: 0x04002DD7 RID: 11735
		public long cbSize;

		/// <summary>Indicates the last modification time for this storage, stream, or byte array.</summary>
		// Token: 0x04002DD8 RID: 11736
		public FILETIME mtime;

		/// <summary>Indicates the creation time for this storage, stream, or byte array.</summary>
		// Token: 0x04002DD9 RID: 11737
		public FILETIME ctime;

		/// <summary>Indicates the last access time for this storage, stream or byte array </summary>
		// Token: 0x04002DDA RID: 11738
		public FILETIME atime;

		/// <summary>Indicates the access mode that was specified when the object was opened.</summary>
		// Token: 0x04002DDB RID: 11739
		public int grfMode;

		/// <summary>Indicates the types of region locking supported by the stream or byte array.</summary>
		// Token: 0x04002DDC RID: 11740
		public int grfLocksSupported;

		/// <summary>Indicates the class identifier for the storage object.</summary>
		// Token: 0x04002DDD RID: 11741
		public Guid clsid;

		/// <summary>Indicates the current state bits of the storage object (the value most recently set by the IStorage::SetStateBits method).</summary>
		// Token: 0x04002DDE RID: 11742
		public int grfStateBits;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002DDF RID: 11743
		public int reserved;
	}
}
