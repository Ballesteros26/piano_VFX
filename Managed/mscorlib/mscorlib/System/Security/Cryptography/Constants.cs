using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000695 RID: 1685
	internal static class Constants
	{
		// Token: 0x040024D7 RID: 9431
		internal const int S_OK = 0;

		// Token: 0x040024D8 RID: 9432
		internal const int NTE_FILENOTFOUND = -2147024894;

		// Token: 0x040024D9 RID: 9433
		internal const int NTE_NO_KEY = -2146893811;

		// Token: 0x040024DA RID: 9434
		internal const int NTE_BAD_KEYSET = -2146893802;

		// Token: 0x040024DB RID: 9435
		internal const int NTE_KEYSET_NOT_DEF = -2146893799;

		// Token: 0x040024DC RID: 9436
		internal const int KP_IV = 1;

		// Token: 0x040024DD RID: 9437
		internal const int KP_MODE = 4;

		// Token: 0x040024DE RID: 9438
		internal const int KP_MODE_BITS = 5;

		// Token: 0x040024DF RID: 9439
		internal const int KP_EFFECTIVE_KEYLEN = 19;

		// Token: 0x040024E0 RID: 9440
		internal const int ALG_CLASS_SIGNATURE = 8192;

		// Token: 0x040024E1 RID: 9441
		internal const int ALG_CLASS_DATA_ENCRYPT = 24576;

		// Token: 0x040024E2 RID: 9442
		internal const int ALG_CLASS_HASH = 32768;

		// Token: 0x040024E3 RID: 9443
		internal const int ALG_CLASS_KEY_EXCHANGE = 40960;

		// Token: 0x040024E4 RID: 9444
		internal const int ALG_TYPE_DSS = 512;

		// Token: 0x040024E5 RID: 9445
		internal const int ALG_TYPE_RSA = 1024;

		// Token: 0x040024E6 RID: 9446
		internal const int ALG_TYPE_BLOCK = 1536;

		// Token: 0x040024E7 RID: 9447
		internal const int ALG_TYPE_STREAM = 2048;

		// Token: 0x040024E8 RID: 9448
		internal const int ALG_TYPE_ANY = 0;

		// Token: 0x040024E9 RID: 9449
		internal const int CALG_MD5 = 32771;

		// Token: 0x040024EA RID: 9450
		internal const int CALG_SHA1 = 32772;

		// Token: 0x040024EB RID: 9451
		internal const int CALG_SHA_256 = 32780;

		// Token: 0x040024EC RID: 9452
		internal const int CALG_SHA_384 = 32781;

		// Token: 0x040024ED RID: 9453
		internal const int CALG_SHA_512 = 32782;

		// Token: 0x040024EE RID: 9454
		internal const int CALG_RSA_KEYX = 41984;

		// Token: 0x040024EF RID: 9455
		internal const int CALG_RSA_SIGN = 9216;

		// Token: 0x040024F0 RID: 9456
		internal const int CALG_DSS_SIGN = 8704;

		// Token: 0x040024F1 RID: 9457
		internal const int CALG_DES = 26113;

		// Token: 0x040024F2 RID: 9458
		internal const int CALG_RC2 = 26114;

		// Token: 0x040024F3 RID: 9459
		internal const int CALG_3DES = 26115;

		// Token: 0x040024F4 RID: 9460
		internal const int CALG_3DES_112 = 26121;

		// Token: 0x040024F5 RID: 9461
		internal const int CALG_AES_128 = 26126;

		// Token: 0x040024F6 RID: 9462
		internal const int CALG_AES_192 = 26127;

		// Token: 0x040024F7 RID: 9463
		internal const int CALG_AES_256 = 26128;

		// Token: 0x040024F8 RID: 9464
		internal const int CALG_RC4 = 26625;

		// Token: 0x040024F9 RID: 9465
		internal const int PROV_RSA_FULL = 1;

		// Token: 0x040024FA RID: 9466
		internal const int PROV_DSS_DH = 13;

		// Token: 0x040024FB RID: 9467
		internal const int PROV_RSA_AES = 24;

		// Token: 0x040024FC RID: 9468
		internal const int AT_KEYEXCHANGE = 1;

		// Token: 0x040024FD RID: 9469
		internal const int AT_SIGNATURE = 2;

		// Token: 0x040024FE RID: 9470
		internal const int PUBLICKEYBLOB = 6;

		// Token: 0x040024FF RID: 9471
		internal const int PRIVATEKEYBLOB = 7;

		// Token: 0x04002500 RID: 9472
		internal const int CRYPT_OAEP = 64;

		// Token: 0x04002501 RID: 9473
		internal const uint CRYPT_VERIFYCONTEXT = 4026531840U;

		// Token: 0x04002502 RID: 9474
		internal const uint CRYPT_NEWKEYSET = 8U;

		// Token: 0x04002503 RID: 9475
		internal const uint CRYPT_DELETEKEYSET = 16U;

		// Token: 0x04002504 RID: 9476
		internal const uint CRYPT_MACHINE_KEYSET = 32U;

		// Token: 0x04002505 RID: 9477
		internal const uint CRYPT_SILENT = 64U;

		// Token: 0x04002506 RID: 9478
		internal const uint CRYPT_EXPORTABLE = 1U;

		// Token: 0x04002507 RID: 9479
		internal const uint CLR_KEYLEN = 1U;

		// Token: 0x04002508 RID: 9480
		internal const uint CLR_PUBLICKEYONLY = 2U;

		// Token: 0x04002509 RID: 9481
		internal const uint CLR_EXPORTABLE = 3U;

		// Token: 0x0400250A RID: 9482
		internal const uint CLR_REMOVABLE = 4U;

		// Token: 0x0400250B RID: 9483
		internal const uint CLR_HARDWARE = 5U;

		// Token: 0x0400250C RID: 9484
		internal const uint CLR_ACCESSIBLE = 6U;

		// Token: 0x0400250D RID: 9485
		internal const uint CLR_PROTECTED = 7U;

		// Token: 0x0400250E RID: 9486
		internal const uint CLR_UNIQUE_CONTAINER = 8U;

		// Token: 0x0400250F RID: 9487
		internal const uint CLR_ALGID = 9U;

		// Token: 0x04002510 RID: 9488
		internal const uint CLR_PP_CLIENT_HWND = 10U;

		// Token: 0x04002511 RID: 9489
		internal const uint CLR_PP_PIN = 11U;

		// Token: 0x04002512 RID: 9490
		internal const string OID_RSA_SMIMEalgCMS3DESwrap = "1.2.840.113549.1.9.16.3.6";

		// Token: 0x04002513 RID: 9491
		internal const string OID_RSA_MD5 = "1.2.840.113549.2.5";

		// Token: 0x04002514 RID: 9492
		internal const string OID_RSA_RC2CBC = "1.2.840.113549.3.2";

		// Token: 0x04002515 RID: 9493
		internal const string OID_RSA_DES_EDE3_CBC = "1.2.840.113549.3.7";

		// Token: 0x04002516 RID: 9494
		internal const string OID_OIWSEC_desCBC = "1.3.14.3.2.7";

		// Token: 0x04002517 RID: 9495
		internal const string OID_OIWSEC_SHA1 = "1.3.14.3.2.26";

		// Token: 0x04002518 RID: 9496
		internal const string OID_OIWSEC_SHA256 = "2.16.840.1.101.3.4.2.1";

		// Token: 0x04002519 RID: 9497
		internal const string OID_OIWSEC_SHA384 = "2.16.840.1.101.3.4.2.2";

		// Token: 0x0400251A RID: 9498
		internal const string OID_OIWSEC_SHA512 = "2.16.840.1.101.3.4.2.3";

		// Token: 0x0400251B RID: 9499
		internal const string OID_OIWSEC_RIPEMD160 = "1.3.36.3.2.1";
	}
}
