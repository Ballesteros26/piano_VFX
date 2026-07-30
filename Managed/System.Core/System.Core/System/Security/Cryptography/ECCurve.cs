using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200007A RID: 122
	public struct ECCurve
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000227E File Offset: 0x0000047E
		public bool IsCharacteristic2
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000227E File Offset: 0x0000047E
		public bool IsExplicit
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000227E File Offset: 0x0000047E
		public bool IsNamed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000227E File Offset: 0x0000047E
		public bool IsPrime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000227E File Offset: 0x0000047E
		public Oid Oid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000227E File Offset: 0x0000047E
		public static ECCurve CreateFromFriendlyName(string oidFriendlyName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000227E File Offset: 0x0000047E
		public static ECCurve CreateFromOid(Oid curveOid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000227E File Offset: 0x0000047E
		public static ECCurve CreateFromValue(string oidValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000227E File Offset: 0x0000047E
		public void Validate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040002EE RID: 750
		public byte[] A;

		// Token: 0x040002EF RID: 751
		public byte[] B;

		// Token: 0x040002F0 RID: 752
		public byte[] Cofactor;

		// Token: 0x040002F1 RID: 753
		public ECCurve.ECCurveType CurveType;

		// Token: 0x040002F2 RID: 754
		public ECPoint G;

		// Token: 0x040002F3 RID: 755
		public HashAlgorithmName? Hash;

		// Token: 0x040002F4 RID: 756
		public byte[] Order;

		// Token: 0x040002F5 RID: 757
		public byte[] Polynomial;

		// Token: 0x040002F6 RID: 758
		public byte[] Prime;

		// Token: 0x040002F7 RID: 759
		public byte[] Seed;

		// Token: 0x0200007B RID: 123
		public enum ECCurveType
		{
			// Token: 0x040002F9 RID: 761
			Implicit,
			// Token: 0x040002FA RID: 762
			PrimeShortWeierstrass,
			// Token: 0x040002FB RID: 763
			PrimeTwistedEdwards,
			// Token: 0x040002FC RID: 764
			PrimeMontgomery,
			// Token: 0x040002FD RID: 765
			Characteristic2,
			// Token: 0x040002FE RID: 766
			Named
		}

		// Token: 0x0200007C RID: 124
		public static class NamedCurves
		{
			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060002EB RID: 747 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP160r1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060002EC RID: 748 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP160t1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060002ED RID: 749 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP192r1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060002EE RID: 750 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP192t1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060002EF RID: 751 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP224r1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP224t1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP256r1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP256t1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP320r1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP320t1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP384r1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP384t1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP512r1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve brainpoolP512t1
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve nistP256
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x060002FA RID: 762 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve nistP384
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x060002FB RID: 763 RVA: 0x0000227E File Offset: 0x0000047E
			public static ECCurve nistP521
			{
				get
				{
					throw new NotImplementedException();
				}
			}
		}
	}
}
