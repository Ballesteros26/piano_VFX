using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200008E RID: 142
	public sealed class KeyBuilder
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x0001877D File Offset: 0x0001697D
		private KeyBuilder()
		{
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00018785 File Offset: 0x00016985
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

		// Token: 0x06000537 RID: 1335 RVA: 0x000187A0 File Offset: 0x000169A0
		public static byte[] Key(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000187C0 File Offset: 0x000169C0
		public static byte[] IV(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x0400039A RID: 922
		private static RandomNumberGenerator rng;
	}
}
