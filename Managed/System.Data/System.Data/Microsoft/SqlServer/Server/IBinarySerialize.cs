using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Provides custom implementation for user-defined type (UDT) and user-defined aggregate serialization and deserialization.</summary>
	// Token: 0x020003B4 RID: 948
	public interface IBinarySerialize
	{
		/// <summary>Generates a user-defined type (UDT) or user-defined aggregate from its binary form.</summary>
		/// <param name="r">The <see cref="T:System.IO.BinaryReader" /> stream from which the object is deserialized.</param>
		// Token: 0x06002DF0 RID: 11760
		void Read(BinaryReader r);

		/// <summary>Converts a user-defined type (UDT) or user-defined aggregate into its binary format so that it may be persisted.</summary>
		/// <param name="w">The <see cref="T:System.IO.BinaryWriter" /> stream to which the UDT or user-defined aggregate is serialized.</param>
		// Token: 0x06002DF1 RID: 11761
		void Write(BinaryWriter w);
	}
}
