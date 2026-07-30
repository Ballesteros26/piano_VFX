using System;
using System.Collections;

namespace System.Windows.Forms.RTF
{
	// Token: 0x0200001E RID: 30
	internal class Charcode
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x0000497C File Offset: 0x00002B7C
		public Charcode()
			: this(256)
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000498C File Offset: 0x00002B8C
		private Charcode(int size)
		{
			this.size = size;
			this.codes = new StandardCharCode[size];
			this.reverse = new Hashtable(size);
		}

		// Token: 0x17000026 RID: 38
		public int this[StandardCharCode c]
		{
			get
			{
				object obj = this.reverse[c];
				if (obj != null)
				{
					return (int)obj;
				}
				for (int i = 0; i < this.size; i++)
				{
					if (this.codes[i] == c)
					{
						return i;
					}
				}
				return -1;
			}
		}

		// Token: 0x17000027 RID: 39
		public StandardCharCode this[int c]
		{
			get
			{
				if (c < 0 || c >= this.size)
				{
					return StandardCharCode.nothing;
				}
				return this.codes[c];
			}
			private set
			{
				if (c < 0 || c >= this.size)
				{
					return;
				}
				this.codes[c] = value;
				this.reverse[value] = c;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004A6C File Offset: 0x00002C6C
		public static Charcode AnsiGeneric
		{
			get
			{
				if (Charcode.ansi_generic != null)
				{
					return Charcode.ansi_generic;
				}
				Charcode.ansi_generic = new Charcode(256);
				Charcode.ansi_generic[6] = StandardCharCode.formula;
				Charcode.ansi_generic[30] = StandardCharCode.nobrkhyphen;
				Charcode.ansi_generic[31] = StandardCharCode.opthyphen;
				Charcode.ansi_generic[32] = StandardCharCode.space;
				Charcode.ansi_generic[33] = StandardCharCode.exclam;
				Charcode.ansi_generic[34] = StandardCharCode.quotedbl;
				Charcode.ansi_generic[35] = StandardCharCode.numbersign;
				Charcode.ansi_generic[36] = StandardCharCode.dollar;
				Charcode.ansi_generic[37] = StandardCharCode.percent;
				Charcode.ansi_generic[38] = StandardCharCode.ampersand;
				Charcode.ansi_generic[92] = StandardCharCode.quoteright;
				Charcode.ansi_generic[40] = StandardCharCode.parenleft;
				Charcode.ansi_generic[41] = StandardCharCode.parenright;
				Charcode.ansi_generic[42] = StandardCharCode.asterisk;
				Charcode.ansi_generic[43] = StandardCharCode.plus;
				Charcode.ansi_generic[44] = StandardCharCode.comma;
				Charcode.ansi_generic[45] = StandardCharCode.hyphen;
				Charcode.ansi_generic[46] = StandardCharCode.period;
				Charcode.ansi_generic[47] = StandardCharCode.slash;
				Charcode.ansi_generic[48] = StandardCharCode.zero;
				Charcode.ansi_generic[49] = StandardCharCode.one;
				Charcode.ansi_generic[50] = StandardCharCode.two;
				Charcode.ansi_generic[51] = StandardCharCode.three;
				Charcode.ansi_generic[52] = StandardCharCode.four;
				Charcode.ansi_generic[53] = StandardCharCode.five;
				Charcode.ansi_generic[54] = StandardCharCode.six;
				Charcode.ansi_generic[55] = StandardCharCode.seven;
				Charcode.ansi_generic[56] = StandardCharCode.eight;
				Charcode.ansi_generic[57] = StandardCharCode.nine;
				Charcode.ansi_generic[58] = StandardCharCode.colon;
				Charcode.ansi_generic[59] = StandardCharCode.semicolon;
				Charcode.ansi_generic[60] = StandardCharCode.less;
				Charcode.ansi_generic[61] = StandardCharCode.equal;
				Charcode.ansi_generic[62] = StandardCharCode.greater;
				Charcode.ansi_generic[63] = StandardCharCode.question;
				Charcode.ansi_generic[64] = StandardCharCode.at;
				Charcode.ansi_generic[65] = StandardCharCode.A;
				Charcode.ansi_generic[66] = StandardCharCode.B;
				Charcode.ansi_generic[67] = StandardCharCode.C;
				Charcode.ansi_generic[68] = StandardCharCode.D;
				Charcode.ansi_generic[69] = StandardCharCode.E;
				Charcode.ansi_generic[70] = StandardCharCode.F;
				Charcode.ansi_generic[71] = StandardCharCode.G;
				Charcode.ansi_generic[72] = StandardCharCode.H;
				Charcode.ansi_generic[73] = StandardCharCode.I;
				Charcode.ansi_generic[74] = StandardCharCode.J;
				Charcode.ansi_generic[75] = StandardCharCode.K;
				Charcode.ansi_generic[76] = StandardCharCode.L;
				Charcode.ansi_generic[77] = StandardCharCode.M;
				Charcode.ansi_generic[78] = StandardCharCode.N;
				Charcode.ansi_generic[79] = StandardCharCode.O;
				Charcode.ansi_generic[80] = StandardCharCode.P;
				Charcode.ansi_generic[81] = StandardCharCode.Q;
				Charcode.ansi_generic[82] = StandardCharCode.R;
				Charcode.ansi_generic[83] = StandardCharCode.S;
				Charcode.ansi_generic[84] = StandardCharCode.T;
				Charcode.ansi_generic[85] = StandardCharCode.U;
				Charcode.ansi_generic[86] = StandardCharCode.V;
				Charcode.ansi_generic[87] = StandardCharCode.W;
				Charcode.ansi_generic[88] = StandardCharCode.X;
				Charcode.ansi_generic[89] = StandardCharCode.Y;
				Charcode.ansi_generic[90] = StandardCharCode.Z;
				Charcode.ansi_generic[91] = StandardCharCode.bracketleft;
				Charcode.ansi_generic[92] = StandardCharCode.backslash;
				Charcode.ansi_generic[93] = StandardCharCode.bracketright;
				Charcode.ansi_generic[94] = StandardCharCode.asciicircum;
				Charcode.ansi_generic[95] = StandardCharCode.underscore;
				Charcode.ansi_generic[96] = StandardCharCode.quoteleft;
				Charcode.ansi_generic[97] = StandardCharCode.a;
				Charcode.ansi_generic[98] = StandardCharCode.b;
				Charcode.ansi_generic[99] = StandardCharCode.c;
				Charcode.ansi_generic[100] = StandardCharCode.d;
				Charcode.ansi_generic[101] = StandardCharCode.e;
				Charcode.ansi_generic[102] = StandardCharCode.f;
				Charcode.ansi_generic[103] = StandardCharCode.g;
				Charcode.ansi_generic[104] = StandardCharCode.h;
				Charcode.ansi_generic[105] = StandardCharCode.i;
				Charcode.ansi_generic[106] = StandardCharCode.j;
				Charcode.ansi_generic[107] = StandardCharCode.k;
				Charcode.ansi_generic[108] = StandardCharCode.l;
				Charcode.ansi_generic[109] = StandardCharCode.m;
				Charcode.ansi_generic[110] = StandardCharCode.n;
				Charcode.ansi_generic[111] = StandardCharCode.o;
				Charcode.ansi_generic[112] = StandardCharCode.p;
				Charcode.ansi_generic[113] = StandardCharCode.q;
				Charcode.ansi_generic[114] = StandardCharCode.r;
				Charcode.ansi_generic[115] = StandardCharCode.s;
				Charcode.ansi_generic[116] = StandardCharCode.t;
				Charcode.ansi_generic[117] = StandardCharCode.u;
				Charcode.ansi_generic[118] = StandardCharCode.v;
				Charcode.ansi_generic[119] = StandardCharCode.w;
				Charcode.ansi_generic[120] = StandardCharCode.x;
				Charcode.ansi_generic[121] = StandardCharCode.y;
				Charcode.ansi_generic[122] = StandardCharCode.z;
				Charcode.ansi_generic[123] = StandardCharCode.braceleft;
				Charcode.ansi_generic[124] = StandardCharCode.bar;
				Charcode.ansi_generic[125] = StandardCharCode.braceright;
				Charcode.ansi_generic[126] = StandardCharCode.asciitilde;
				Charcode.ansi_generic[160] = StandardCharCode.nobrkspace;
				Charcode.ansi_generic[161] = StandardCharCode.exclamdown;
				Charcode.ansi_generic[162] = StandardCharCode.cent;
				Charcode.ansi_generic[163] = StandardCharCode.sterling;
				Charcode.ansi_generic[164] = StandardCharCode.currency;
				Charcode.ansi_generic[165] = StandardCharCode.yen;
				Charcode.ansi_generic[166] = StandardCharCode.brokenbar;
				Charcode.ansi_generic[167] = StandardCharCode.section;
				Charcode.ansi_generic[168] = StandardCharCode.dieresis;
				Charcode.ansi_generic[169] = StandardCharCode.copyright;
				Charcode.ansi_generic[170] = StandardCharCode.ordfeminine;
				Charcode.ansi_generic[171] = StandardCharCode.guillemotleft;
				Charcode.ansi_generic[172] = StandardCharCode.logicalnot;
				Charcode.ansi_generic[173] = StandardCharCode.opthyphen;
				Charcode.ansi_generic[174] = StandardCharCode.registered;
				Charcode.ansi_generic[175] = StandardCharCode.macron;
				Charcode.ansi_generic[176] = StandardCharCode.degree;
				Charcode.ansi_generic[177] = StandardCharCode.plusminus;
				Charcode.ansi_generic[178] = StandardCharCode.twosuperior;
				Charcode.ansi_generic[179] = StandardCharCode.threesuperior;
				Charcode.ansi_generic[180] = StandardCharCode.acute;
				Charcode.ansi_generic[181] = StandardCharCode.mu;
				Charcode.ansi_generic[182] = StandardCharCode.paragraph;
				Charcode.ansi_generic[183] = StandardCharCode.periodcentered;
				Charcode.ansi_generic[184] = StandardCharCode.cedilla;
				Charcode.ansi_generic[185] = StandardCharCode.onesuperior;
				Charcode.ansi_generic[186] = StandardCharCode.ordmasculine;
				Charcode.ansi_generic[187] = StandardCharCode.guillemotright;
				Charcode.ansi_generic[188] = StandardCharCode.onequarter;
				Charcode.ansi_generic[189] = StandardCharCode.onehalf;
				Charcode.ansi_generic[190] = StandardCharCode.threequarters;
				Charcode.ansi_generic[191] = StandardCharCode.questiondown;
				Charcode.ansi_generic[192] = StandardCharCode.Agrave;
				Charcode.ansi_generic[193] = StandardCharCode.Aacute;
				Charcode.ansi_generic[194] = StandardCharCode.Acircumflex;
				Charcode.ansi_generic[195] = StandardCharCode.Atilde;
				Charcode.ansi_generic[196] = StandardCharCode.Adieresis;
				Charcode.ansi_generic[197] = StandardCharCode.Aring;
				Charcode.ansi_generic[198] = StandardCharCode.AE;
				Charcode.ansi_generic[199] = StandardCharCode.Ccedilla;
				Charcode.ansi_generic[200] = StandardCharCode.Egrave;
				Charcode.ansi_generic[201] = StandardCharCode.Eacute;
				Charcode.ansi_generic[202] = StandardCharCode.Ecircumflex;
				Charcode.ansi_generic[203] = StandardCharCode.Edieresis;
				Charcode.ansi_generic[204] = StandardCharCode.Igrave;
				Charcode.ansi_generic[205] = StandardCharCode.Iacute;
				Charcode.ansi_generic[206] = StandardCharCode.Icircumflex;
				Charcode.ansi_generic[207] = StandardCharCode.Idieresis;
				Charcode.ansi_generic[208] = StandardCharCode.Eth;
				Charcode.ansi_generic[209] = StandardCharCode.Ntilde;
				Charcode.ansi_generic[210] = StandardCharCode.Ograve;
				Charcode.ansi_generic[211] = StandardCharCode.Oacute;
				Charcode.ansi_generic[212] = StandardCharCode.Ocircumflex;
				Charcode.ansi_generic[213] = StandardCharCode.Otilde;
				Charcode.ansi_generic[214] = StandardCharCode.Odieresis;
				Charcode.ansi_generic[215] = StandardCharCode.multiply;
				Charcode.ansi_generic[216] = StandardCharCode.Oslash;
				Charcode.ansi_generic[217] = StandardCharCode.Ugrave;
				Charcode.ansi_generic[218] = StandardCharCode.Uacute;
				Charcode.ansi_generic[219] = StandardCharCode.Ucircumflex;
				Charcode.ansi_generic[220] = StandardCharCode.Udieresis;
				Charcode.ansi_generic[221] = StandardCharCode.Yacute;
				Charcode.ansi_generic[222] = StandardCharCode.Thorn;
				Charcode.ansi_generic[223] = StandardCharCode.germandbls;
				Charcode.ansi_generic[224] = StandardCharCode.agrave;
				Charcode.ansi_generic[225] = StandardCharCode.aacute;
				Charcode.ansi_generic[226] = StandardCharCode.acircumflex;
				Charcode.ansi_generic[227] = StandardCharCode.atilde;
				Charcode.ansi_generic[228] = StandardCharCode.adieresis;
				Charcode.ansi_generic[229] = StandardCharCode.aring;
				Charcode.ansi_generic[230] = StandardCharCode.ae;
				Charcode.ansi_generic[231] = StandardCharCode.ccedilla;
				Charcode.ansi_generic[232] = StandardCharCode.egrave;
				Charcode.ansi_generic[233] = StandardCharCode.eacute;
				Charcode.ansi_generic[234] = StandardCharCode.ecircumflex;
				Charcode.ansi_generic[235] = StandardCharCode.edieresis;
				Charcode.ansi_generic[236] = StandardCharCode.igrave;
				Charcode.ansi_generic[237] = StandardCharCode.iacute;
				Charcode.ansi_generic[238] = StandardCharCode.icircumflex;
				Charcode.ansi_generic[239] = StandardCharCode.idieresis;
				Charcode.ansi_generic[240] = StandardCharCode.eth;
				Charcode.ansi_generic[241] = StandardCharCode.ntilde;
				Charcode.ansi_generic[242] = StandardCharCode.ograve;
				Charcode.ansi_generic[243] = StandardCharCode.oacute;
				Charcode.ansi_generic[244] = StandardCharCode.ocircumflex;
				Charcode.ansi_generic[245] = StandardCharCode.otilde;
				Charcode.ansi_generic[246] = StandardCharCode.odieresis;
				Charcode.ansi_generic[247] = StandardCharCode.divide;
				Charcode.ansi_generic[248] = StandardCharCode.oslash;
				Charcode.ansi_generic[249] = StandardCharCode.ugrave;
				Charcode.ansi_generic[250] = StandardCharCode.uacute;
				Charcode.ansi_generic[251] = StandardCharCode.ucircumflex;
				Charcode.ansi_generic[252] = StandardCharCode.udieresis;
				Charcode.ansi_generic[253] = StandardCharCode.yacute;
				Charcode.ansi_generic[254] = StandardCharCode.thorn;
				Charcode.ansi_generic[255] = StandardCharCode.ydieresis;
				return Charcode.ansi_generic;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005754 File Offset: 0x00003954
		public static Charcode AnsiSymbol
		{
			get
			{
				Charcode charcode = new Charcode(256);
				charcode[6] = StandardCharCode.formula;
				charcode[30] = StandardCharCode.nobrkhyphen;
				charcode[31] = StandardCharCode.opthyphen;
				charcode[32] = StandardCharCode.space;
				charcode[33] = StandardCharCode.exclam;
				charcode[34] = StandardCharCode.universal;
				charcode[35] = StandardCharCode.mathnumbersign;
				charcode[36] = StandardCharCode.existential;
				charcode[37] = StandardCharCode.percent;
				charcode[38] = StandardCharCode.ampersand;
				charcode[92] = StandardCharCode.suchthat;
				charcode[40] = StandardCharCode.parenleft;
				charcode[41] = StandardCharCode.parenright;
				charcode[42] = StandardCharCode.mathasterisk;
				charcode[43] = StandardCharCode.mathplus;
				charcode[44] = StandardCharCode.comma;
				charcode[45] = StandardCharCode.mathminus;
				charcode[46] = StandardCharCode.period;
				charcode[47] = StandardCharCode.slash;
				charcode[48] = StandardCharCode.zero;
				charcode[49] = StandardCharCode.one;
				charcode[50] = StandardCharCode.two;
				charcode[51] = StandardCharCode.three;
				charcode[52] = StandardCharCode.four;
				charcode[53] = StandardCharCode.five;
				charcode[54] = StandardCharCode.six;
				charcode[55] = StandardCharCode.seven;
				charcode[56] = StandardCharCode.eight;
				charcode[57] = StandardCharCode.nine;
				charcode[58] = StandardCharCode.colon;
				charcode[59] = StandardCharCode.semicolon;
				charcode[60] = StandardCharCode.less;
				charcode[61] = StandardCharCode.mathequal;
				charcode[62] = StandardCharCode.greater;
				charcode[63] = StandardCharCode.question;
				charcode[64] = StandardCharCode.congruent;
				charcode[65] = StandardCharCode.Alpha;
				charcode[66] = StandardCharCode.Beta;
				charcode[67] = StandardCharCode.Chi;
				charcode[68] = StandardCharCode.Delta;
				charcode[69] = StandardCharCode.Epsilon;
				charcode[70] = StandardCharCode.Phi;
				charcode[71] = StandardCharCode.Gamma;
				charcode[72] = StandardCharCode.Eta;
				charcode[73] = StandardCharCode.Iota;
				charcode[75] = StandardCharCode.Kappa;
				charcode[76] = StandardCharCode.Lambda;
				charcode[77] = StandardCharCode.Mu;
				charcode[78] = StandardCharCode.Nu;
				charcode[79] = StandardCharCode.Omicron;
				charcode[80] = StandardCharCode.Pi;
				charcode[81] = StandardCharCode.Theta;
				charcode[82] = StandardCharCode.Rho;
				charcode[83] = StandardCharCode.Sigma;
				charcode[84] = StandardCharCode.Tau;
				charcode[85] = StandardCharCode.Upsilon;
				charcode[86] = StandardCharCode.varsigma;
				charcode[87] = StandardCharCode.Omega;
				charcode[88] = StandardCharCode.Xi;
				charcode[89] = StandardCharCode.Psi;
				charcode[90] = StandardCharCode.Zeta;
				charcode[91] = StandardCharCode.bracketleft;
				charcode[92] = StandardCharCode.backslash;
				charcode[93] = StandardCharCode.bracketright;
				charcode[94] = StandardCharCode.asciicircum;
				charcode[95] = StandardCharCode.underscore;
				charcode[96] = StandardCharCode.quoteleft;
				charcode[97] = StandardCharCode.alpha;
				charcode[98] = StandardCharCode.beta;
				charcode[99] = StandardCharCode.chi;
				charcode[100] = StandardCharCode.delta;
				charcode[101] = StandardCharCode.epsilon;
				charcode[102] = StandardCharCode.phi;
				charcode[103] = StandardCharCode.gamma;
				charcode[104] = StandardCharCode.eta;
				charcode[105] = StandardCharCode.iota;
				charcode[107] = StandardCharCode.kappa;
				charcode[108] = StandardCharCode.lambda;
				charcode[109] = StandardCharCode.mu;
				charcode[110] = StandardCharCode.nu;
				charcode[111] = StandardCharCode.omicron;
				charcode[112] = StandardCharCode.pi;
				charcode[113] = StandardCharCode.theta;
				charcode[114] = StandardCharCode.rho;
				charcode[115] = StandardCharCode.sigma;
				charcode[116] = StandardCharCode.tau;
				charcode[117] = StandardCharCode.upsilon;
				charcode[119] = StandardCharCode.omega;
				charcode[120] = StandardCharCode.xi;
				charcode[121] = StandardCharCode.psi;
				charcode[122] = StandardCharCode.zeta;
				charcode[123] = StandardCharCode.braceleft;
				charcode[124] = StandardCharCode.bar;
				charcode[125] = StandardCharCode.braceright;
				charcode[126] = StandardCharCode.mathtilde;
				return charcode;
			}
		}

		// Token: 0x0400005B RID: 91
		private StandardCharCode[] codes;

		// Token: 0x0400005C RID: 92
		private Hashtable reverse;

		// Token: 0x0400005D RID: 93
		private int size;

		// Token: 0x0400005E RID: 94
		private static Charcode ansi_generic;
	}
}
