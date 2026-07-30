using System;

namespace System.Runtime.Serialization
{
	/// <summary>Enables serialization of custom exception data in security-transparent code.</summary>
	// Token: 0x020006E3 RID: 1763
	public interface ISafeSerializationData
	{
		/// <summary>This method is called when the instance is deserialized. </summary>
		/// <param name="deserialized">An object that contains the state of the instance.</param>
		// Token: 0x06004A85 RID: 19077
		void CompleteDeserialization(object deserialized);
	}
}
