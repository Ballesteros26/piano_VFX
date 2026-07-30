using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x0200028A RID: 650
	[Serializable]
	internal sealed class SurrogateEncoder : ISerializable, IObjectReference
	{
		// Token: 0x06001E13 RID: 7699 RVA: 0x0007141B File Offset: 0x0006F61B
		internal SurrogateEncoder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.realEncoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x00071451 File Offset: 0x0006F651
		[SecurityCritical]
		public object GetRealObject(StreamingContext context)
		{
			return this.realEncoding.GetEncoder();
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x0006A774 File Offset: 0x00068974
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException(Environment.GetResourceString("Internal error in the runtime."));
		}

		// Token: 0x0400106B RID: 4203
		[NonSerialized]
		private Encoding realEncoding;
	}
}
