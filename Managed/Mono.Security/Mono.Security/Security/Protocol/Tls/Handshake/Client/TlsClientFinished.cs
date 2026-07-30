using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x02000063 RID: 99
	internal class TlsClientFinished : HandshakeMessage
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x00014744 File Offset: 0x00012944
		public TlsClientFinished(Context context)
			: base(context, HandshakeType.Finished)
		{
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001474F File Offset: 0x0001294F
		public override void Update()
		{
			base.Update();
			base.Reset();
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00014760 File Offset: 0x00012960
		protected override void ProcessAsSsl3()
		{
			HashAlgorithm hashAlgorithm = new SslHandshakeHash(base.Context.MasterSecret);
			byte[] array = base.Context.HandshakeMessages.ToArray();
			hashAlgorithm.TransformBlock(array, 0, array.Length, array, 0);
			hashAlgorithm.TransformBlock(TlsClientFinished.Ssl3Marker, 0, TlsClientFinished.Ssl3Marker.Length, TlsClientFinished.Ssl3Marker, 0);
			hashAlgorithm.TransformFinalBlock(CipherSuite.EmptyArray, 0, 0);
			base.Write(hashAlgorithm.Hash);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000147D4 File Offset: 0x000129D4
		protected override void ProcessAsTls1()
		{
			HashAlgorithm hashAlgorithm = new MD5SHA1();
			byte[] array = base.Context.HandshakeMessages.ToArray();
			byte[] array2 = hashAlgorithm.ComputeHash(array, 0, array.Length);
			base.Write(base.Context.Write.Cipher.PRF(base.Context.MasterSecret, "client finished", array2, 12));
		}

		// Token: 0x040001E5 RID: 485
		private static byte[] Ssl3Marker = new byte[] { 67, 76, 78, 84 };
	}
}
