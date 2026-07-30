using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000140 RID: 320
	internal sealed class RegexCode
	{
		// Token: 0x060008F4 RID: 2292 RVA: 0x0002C830 File Offset: 0x0002AA30
		internal RegexCode(int[] codes, List<string> stringlist, int trackcount, Hashtable caps, int capsize, RegexBoyerMoore bmPrefix, RegexPrefix fcPrefix, int anchors, bool rightToLeft)
		{
			this._codes = codes;
			this._strings = new string[stringlist.Count];
			this._trackcount = trackcount;
			this._caps = caps;
			this._capsize = capsize;
			this._bmPrefix = bmPrefix;
			this._fcPrefix = fcPrefix;
			this._anchors = anchors;
			this._rightToLeft = rightToLeft;
			stringlist.CopyTo(0, this._strings, 0, stringlist.Count);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002C8A8 File Offset: 0x0002AAA8
		internal static bool OpcodeBacktracks(int Op)
		{
			Op &= 63;
			switch (Op)
			{
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 38:
				return true;
			}
			return false;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002C958 File Offset: 0x0002AB58
		internal static int OpcodeSize(int Opcode)
		{
			Opcode &= 63;
			switch (Opcode)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 28:
			case 29:
			case 32:
				return 3;
			case 9:
			case 10:
			case 11:
			case 12:
			case 13:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 37:
			case 38:
			case 39:
				return 2;
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 30:
			case 31:
			case 33:
			case 34:
			case 35:
			case 36:
			case 40:
			case 41:
			case 42:
				return 1;
			default:
				throw RegexCode.MakeException(global::SR.GetString("Unexpected opcode in regular expression generation: {0}.", new object[] { Opcode.ToString(CultureInfo.CurrentCulture) }));
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0002CA49 File Offset: 0x0002AC49
		internal static ArgumentException MakeException(string message)
		{
			return new ArgumentException(message);
		}

		// Token: 0x04000E14 RID: 3604
		internal const int Onerep = 0;

		// Token: 0x04000E15 RID: 3605
		internal const int Notonerep = 1;

		// Token: 0x04000E16 RID: 3606
		internal const int Setrep = 2;

		// Token: 0x04000E17 RID: 3607
		internal const int Oneloop = 3;

		// Token: 0x04000E18 RID: 3608
		internal const int Notoneloop = 4;

		// Token: 0x04000E19 RID: 3609
		internal const int Setloop = 5;

		// Token: 0x04000E1A RID: 3610
		internal const int Onelazy = 6;

		// Token: 0x04000E1B RID: 3611
		internal const int Notonelazy = 7;

		// Token: 0x04000E1C RID: 3612
		internal const int Setlazy = 8;

		// Token: 0x04000E1D RID: 3613
		internal const int One = 9;

		// Token: 0x04000E1E RID: 3614
		internal const int Notone = 10;

		// Token: 0x04000E1F RID: 3615
		internal const int Set = 11;

		// Token: 0x04000E20 RID: 3616
		internal const int Multi = 12;

		// Token: 0x04000E21 RID: 3617
		internal const int Ref = 13;

		// Token: 0x04000E22 RID: 3618
		internal const int Bol = 14;

		// Token: 0x04000E23 RID: 3619
		internal const int Eol = 15;

		// Token: 0x04000E24 RID: 3620
		internal const int Boundary = 16;

		// Token: 0x04000E25 RID: 3621
		internal const int Nonboundary = 17;

		// Token: 0x04000E26 RID: 3622
		internal const int Beginning = 18;

		// Token: 0x04000E27 RID: 3623
		internal const int Start = 19;

		// Token: 0x04000E28 RID: 3624
		internal const int EndZ = 20;

		// Token: 0x04000E29 RID: 3625
		internal const int End = 21;

		// Token: 0x04000E2A RID: 3626
		internal const int Nothing = 22;

		// Token: 0x04000E2B RID: 3627
		internal const int Lazybranch = 23;

		// Token: 0x04000E2C RID: 3628
		internal const int Branchmark = 24;

		// Token: 0x04000E2D RID: 3629
		internal const int Lazybranchmark = 25;

		// Token: 0x04000E2E RID: 3630
		internal const int Nullcount = 26;

		// Token: 0x04000E2F RID: 3631
		internal const int Setcount = 27;

		// Token: 0x04000E30 RID: 3632
		internal const int Branchcount = 28;

		// Token: 0x04000E31 RID: 3633
		internal const int Lazybranchcount = 29;

		// Token: 0x04000E32 RID: 3634
		internal const int Nullmark = 30;

		// Token: 0x04000E33 RID: 3635
		internal const int Setmark = 31;

		// Token: 0x04000E34 RID: 3636
		internal const int Capturemark = 32;

		// Token: 0x04000E35 RID: 3637
		internal const int Getmark = 33;

		// Token: 0x04000E36 RID: 3638
		internal const int Setjump = 34;

		// Token: 0x04000E37 RID: 3639
		internal const int Backjump = 35;

		// Token: 0x04000E38 RID: 3640
		internal const int Forejump = 36;

		// Token: 0x04000E39 RID: 3641
		internal const int Testref = 37;

		// Token: 0x04000E3A RID: 3642
		internal const int Goto = 38;

		// Token: 0x04000E3B RID: 3643
		internal const int Prune = 39;

		// Token: 0x04000E3C RID: 3644
		internal const int Stop = 40;

		// Token: 0x04000E3D RID: 3645
		internal const int ECMABoundary = 41;

		// Token: 0x04000E3E RID: 3646
		internal const int NonECMABoundary = 42;

		// Token: 0x04000E3F RID: 3647
		internal const int Mask = 63;

		// Token: 0x04000E40 RID: 3648
		internal const int Rtl = 64;

		// Token: 0x04000E41 RID: 3649
		internal const int Back = 128;

		// Token: 0x04000E42 RID: 3650
		internal const int Back2 = 256;

		// Token: 0x04000E43 RID: 3651
		internal const int Ci = 512;

		// Token: 0x04000E44 RID: 3652
		internal int[] _codes;

		// Token: 0x04000E45 RID: 3653
		internal string[] _strings;

		// Token: 0x04000E46 RID: 3654
		internal int _trackcount;

		// Token: 0x04000E47 RID: 3655
		internal Hashtable _caps;

		// Token: 0x04000E48 RID: 3656
		internal int _capsize;

		// Token: 0x04000E49 RID: 3657
		internal RegexPrefix _fcPrefix;

		// Token: 0x04000E4A RID: 3658
		internal RegexBoyerMoore _bmPrefix;

		// Token: 0x04000E4B RID: 3659
		internal int _anchors;

		// Token: 0x04000E4C RID: 3660
		internal bool _rightToLeft;
	}
}
