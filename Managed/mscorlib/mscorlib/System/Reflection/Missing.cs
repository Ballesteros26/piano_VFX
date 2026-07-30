using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	/// <summary>Represents a missing <see cref="T:System.Object" />. This class cannot be inherited.</summary>
	// Token: 0x020002F6 RID: 758
	[ComVisible(true)]
	[Serializable]
	public sealed class Missing : ISerializable
	{
		// Token: 0x060020EB RID: 8427 RVA: 0x00002111 File Offset: 0x00000311
		private Missing()
		{
		}

		/// <summary>Sets a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate the sole instance of the <see cref="T:System.Reflection.Missing" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to be populated with serialization information.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object representing the destination context of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060020EC RID: 8428 RVA: 0x0007ECAB File Offset: 0x0007CEAB
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, this);
		}

		/// <summary>Represents the sole instance of the <see cref="T:System.Reflection.Missing" /> class.</summary>
		// Token: 0x0400128A RID: 4746
		public static readonly Missing Value = new Missing();
	}
}
