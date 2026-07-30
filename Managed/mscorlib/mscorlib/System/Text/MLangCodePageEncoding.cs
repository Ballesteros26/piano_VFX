using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000285 RID: 645
	[Serializable]
	internal sealed class MLangCodePageEncoding : ISerializable, IObjectReference
	{
		// Token: 0x06001DAB RID: 7595 RVA: 0x0006F314 File Offset: 0x0006D514
		internal MLangCodePageEncoding(SerializationInfo info, StreamingContext context)
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

		// Token: 0x06001DAC RID: 7596 RVA: 0x0006F3D8 File Offset: 0x0006D5D8
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

		// Token: 0x06001DAD RID: 7597 RVA: 0x0006A774 File Offset: 0x00068974
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException(Environment.GetResourceString("Internal error in the runtime."));
		}

		// Token: 0x04001056 RID: 4182
		[NonSerialized]
		private int m_codePage;

		// Token: 0x04001057 RID: 4183
		[NonSerialized]
		private bool m_isReadOnly;

		// Token: 0x04001058 RID: 4184
		[NonSerialized]
		private bool m_deserializedFromEverett;

		// Token: 0x04001059 RID: 4185
		[NonSerialized]
		private EncoderFallback encoderFallback;

		// Token: 0x0400105A RID: 4186
		[NonSerialized]
		private DecoderFallback decoderFallback;

		// Token: 0x0400105B RID: 4187
		[NonSerialized]
		private Encoding realEncoding;

		// Token: 0x02000286 RID: 646
		[Serializable]
		internal sealed class MLangEncoder : ISerializable, IObjectReference
		{
			// Token: 0x06001DAE RID: 7598 RVA: 0x0006F444 File Offset: 0x0006D644
			internal MLangEncoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.realEncoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
			}

			// Token: 0x06001DAF RID: 7599 RVA: 0x0006F47A File Offset: 0x0006D67A
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				return this.realEncoding.GetEncoder();
			}

			// Token: 0x06001DB0 RID: 7600 RVA: 0x0006A774 File Offset: 0x00068974
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException(Environment.GetResourceString("Internal error in the runtime."));
			}

			// Token: 0x0400105C RID: 4188
			[NonSerialized]
			private Encoding realEncoding;
		}

		// Token: 0x02000287 RID: 647
		[Serializable]
		internal sealed class MLangDecoder : ISerializable, IObjectReference
		{
			// Token: 0x06001DB1 RID: 7601 RVA: 0x0006F487 File Offset: 0x0006D687
			internal MLangDecoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.realEncoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
			}

			// Token: 0x06001DB2 RID: 7602 RVA: 0x0006F4BD File Offset: 0x0006D6BD
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				return this.realEncoding.GetDecoder();
			}

			// Token: 0x06001DB3 RID: 7603 RVA: 0x0006A774 File Offset: 0x00068974
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException(Environment.GetResourceString("Internal error in the runtime."));
			}

			// Token: 0x0400105D RID: 4189
			[NonSerialized]
			private Encoding realEncoding;
		}
	}
}
