using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000264 RID: 612
	[Serializable]
	internal sealed class CodePageEncoding : ISerializable, IObjectReference
	{
		// Token: 0x06001C33 RID: 7219 RVA: 0x0006A644 File Offset: 0x00068844
		internal CodePageEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_codePage = (int)info.GetValue("m_codePage", typeof(int));
			try
			{
				this.m_isReadOnly = (bool)info.GetValue("m_isReadOnly", typeof(bool));
				this.encoderFallback = (EncoderFallback)info.GetValue("encoderFallback", typeof(EncoderFallback));
				this.decoderFallback = (DecoderFallback)info.GetValue("decoderFallback", typeof(DecoderFallback));
			}
			catch (SerializationException)
			{
				this.m_deserializedFromEverett = true;
				this.m_isReadOnly = true;
			}
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x0006A708 File Offset: 0x00068908
		[SecurityCritical]
		public object GetRealObject(StreamingContext context)
		{
			this.realEncoding = Encoding.GetEncoding(this.m_codePage);
			if (!this.m_deserializedFromEverett && !this.m_isReadOnly)
			{
				this.realEncoding = (Encoding)this.realEncoding.Clone();
				this.realEncoding.EncoderFallback = this.encoderFallback;
				this.realEncoding.DecoderFallback = this.decoderFallback;
			}
			return this.realEncoding;
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0006A774 File Offset: 0x00068974
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException(Environment.GetResourceString("Internal error in the runtime."));
		}

		// Token: 0x04000FB6 RID: 4022
		[NonSerialized]
		private int m_codePage;

		// Token: 0x04000FB7 RID: 4023
		[NonSerialized]
		private bool m_isReadOnly;

		// Token: 0x04000FB8 RID: 4024
		[NonSerialized]
		private bool m_deserializedFromEverett;

		// Token: 0x04000FB9 RID: 4025
		[NonSerialized]
		private EncoderFallback encoderFallback;

		// Token: 0x04000FBA RID: 4026
		[NonSerialized]
		private DecoderFallback decoderFallback;

		// Token: 0x04000FBB RID: 4027
		[NonSerialized]
		private Encoding realEncoding;

		// Token: 0x02000265 RID: 613
		[Serializable]
		internal sealed class Decoder : ISerializable, IObjectReference
		{
			// Token: 0x06001C36 RID: 7222 RVA: 0x0006A785 File Offset: 0x00068985
			internal Decoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.realEncoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
			}

			// Token: 0x06001C37 RID: 7223 RVA: 0x0006A7BB File Offset: 0x000689BB
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				return this.realEncoding.GetDecoder();
			}

			// Token: 0x06001C38 RID: 7224 RVA: 0x0006A774 File Offset: 0x00068974
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException(Environment.GetResourceString("Internal error in the runtime."));
			}

			// Token: 0x04000FBC RID: 4028
			[NonSerialized]
			private Encoding realEncoding;
		}
	}
}
