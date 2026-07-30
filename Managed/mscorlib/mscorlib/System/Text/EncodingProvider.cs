using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000283 RID: 643
	[ComVisible(true)]
	public abstract class EncodingProvider
	{
		// Token: 0x06001D93 RID: 7571 RVA: 0x00002111 File Offset: 0x00000311
		public EncodingProvider()
		{
		}

		// Token: 0x06001D94 RID: 7572
		public abstract Encoding GetEncoding(string name);

		// Token: 0x06001D95 RID: 7573
		public abstract Encoding GetEncoding(int codepage);

		// Token: 0x06001D96 RID: 7574 RVA: 0x0006EC14 File Offset: 0x0006CE14
		public virtual Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(name);
			if (encoding != null)
			{
				encoding = (Encoding)this.GetEncoding(name).Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0006EC50 File Offset: 0x0006CE50
		public virtual Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(codepage);
			if (encoding != null)
			{
				encoding = (Encoding)this.GetEncoding(codepage).Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0006EC8C File Offset: 0x0006CE8C
		internal static void AddProvider(EncodingProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			object obj = EncodingProvider.s_InternalSyncObject;
			lock (obj)
			{
				if (EncodingProvider.s_providers == null)
				{
					EncodingProvider.s_providers = new EncodingProvider[] { provider };
				}
				else if (Array.IndexOf<EncodingProvider>(EncodingProvider.s_providers, provider) < 0)
				{
					EncodingProvider[] array = new EncodingProvider[EncodingProvider.s_providers.Length + 1];
					Array.Copy(EncodingProvider.s_providers, array, EncodingProvider.s_providers.Length);
					array[array.Length - 1] = provider;
					EncodingProvider.s_providers = array;
				}
			}
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0006ED38 File Offset: 0x0006CF38
		internal static Encoding GetEncodingFromProvider(int codepage)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(codepage);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x0006ED78 File Offset: 0x0006CF78
		internal static Encoding GetEncodingFromProvider(string encodingName)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(encodingName);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0006EDB8 File Offset: 0x0006CFB8
		internal static Encoding GetEncodingFromProvider(int codepage, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(codepage, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0006EDF8 File Offset: 0x0006CFF8
		internal static Encoding GetEncodingFromProvider(string encodingName, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			for (int i = 0; i < array.Length; i++)
			{
				Encoding encoding = array[i].GetEncoding(encodingName, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x04001053 RID: 4179
		private static object s_InternalSyncObject = new object();

		// Token: 0x04001054 RID: 4180
		private static volatile EncodingProvider[] s_providers;
	}
}
