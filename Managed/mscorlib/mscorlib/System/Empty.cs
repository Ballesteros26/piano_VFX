using System;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x02000154 RID: 340
	[Serializable]
	internal sealed class Empty : ISerializable
	{
		// Token: 0x06000EAB RID: 3755 RVA: 0x00002111 File Offset: 0x00000311
		private Empty()
		{
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003A514 File Offset: 0x00038714
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003CEDF File Offset: 0x0003B0DF
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 1, null, null);
		}

		// Token: 0x040008EE RID: 2286
		public static readonly Empty Value = new Empty();
	}
}
