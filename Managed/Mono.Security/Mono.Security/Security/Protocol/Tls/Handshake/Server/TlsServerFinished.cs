using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x0200005D RID: 93
	internal class TlsServerFinished : HandshakeMessage
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x00013ED6 File Offset: 0x000120D6
		public TlsServerFinished(Context context)
			: base(context, HandshakeType.Finished)
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00013EE4 File Offset: 0x000120E4
		protected override void ProcessAsSsl3()
		{
			HashAlgorithm hashAlgorithm = new SslHandshakeHash(base.Context.MasterSecret);
			byte[] array = base.Context.HandshakeMessages.ToArray();
			hashAlgorithm.TransformBlock(array, 0, array.Length, array, 0);
			hashAlgorithm.TransformBlock(TlsServerFinished.Ssl3Marker, 0, TlsServerFinished.Ssl3Marker.Length, TlsServerFinished.Ssl3Marker, 0);
			hashAlgorithm.TransformFinalBlock(CipherSuite.EmptyArray, 0, 0);
			base.Write(hashAlgorithm.Hash);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00013F58 File Offset: 0x00012158
		protected override void ProcessAsTls1()
		{
			HashAlgorithm hashAlgorithm = new MD5SHA1();
			byte[] array = base.Context.HandshakeMessages.ToArray();
			byte[] array2 = hashAlgorithm.ComputeHash(array, 0, array.Length);
			base.Write(base.Context.Current.Cipher.PRF(base.Context.MasterSecret, "server finished", array2, 12));
		}

		// Token: 0x040001E0 RID: 480
		private static byte[] Ssl3Marker = new byte[] { 83, 82, 86, 82 };
	}
}
