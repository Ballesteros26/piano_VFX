using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000080 RID: 128
	internal sealed class KeyBuilder
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x00002111 File Offset: 0x00000311
		private KeyBuilder()
		{
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00017079 File Offset: 0x00015279
		private static RandomNumberGenerator Rng
		{
			get
			{
				if (KeyBuilder.rng == null)
				{
					KeyBuilder.rng = RandomNumberGenerator.Create();
				}
				return KeyBuilder.rng;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00017094 File Offset: 0x00015294
		public static byte[] Key(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000170B4 File Offset: 0x000152B4
		public static byte[] IV(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x04000550 RID: 1360
		private static RandomNumberGenerator rng;
	}
}
