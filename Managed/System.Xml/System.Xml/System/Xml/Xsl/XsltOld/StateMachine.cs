using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200053F RID: 1343
	internal class StateMachine
	{
		// Token: 0x06003664 RID: 13924 RVA: 0x00130F5E File Offset: 0x0012F15E
		internal StateMachine()
		{
			this._State = 0;
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06003665 RID: 13925 RVA: 0x00130F6D File Offset: 0x0012F16D
		// (set) Token: 0x06003666 RID: 13926 RVA: 0x00130F75 File Offset: 0x0012F175
		internal int State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
			}
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x00130F7E File Offset: 0x0012F17E
		internal void Reset()
		{
			this._State = 0;
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x00130F87 File Offset: 0x0012F187
		internal static int StateOnly(int state)
		{
			return state & 15;
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x00130F8D File Offset: 0x0012F18D
		internal int BeginOutlook(XPathNodeType nodeType)
		{
			return StateMachine.s_BeginTransitions[(int)nodeType][this._State];
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x00130FA0 File Offset: 0x0012F1A0
		internal int Begin(XPathNodeType nodeType)
		{
			int num = StateMachine.s_BeginTransitions[(int)nodeType][this._State];
			if (num != 16 && num != 32)
			{
				this._State = num & 15;
			}
			return num;
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x00130FD1 File Offset: 0x0012F1D1
		internal int EndOutlook(XPathNodeType nodeType)
		{
			return StateMachine.s_EndTransitions[(int)nodeType][this._State];
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x00130FE4 File Offset: 0x0012F1E4
		internal int End(XPathNodeType nodeType)
		{
			int num = StateMachine.s_EndTransitions[(int)nodeType][this._State];
			if (num != 16 && num != 32)
			{
				this._State = num & 15;
			}
			return num;
		}

		// Token: 0x040022CA RID: 8906
		private const int Init = 0;

		// Token: 0x040022CB RID: 8907
		private const int Elem = 1;

		// Token: 0x040022CC RID: 8908
		private const int NsN = 2;

		// Token: 0x040022CD RID: 8909
		private const int NsV = 3;

		// Token: 0x040022CE RID: 8910
		private const int Ns = 4;

		// Token: 0x040022CF RID: 8911
		private const int AttrN = 5;

		// Token: 0x040022D0 RID: 8912
		private const int AttrV = 6;

		// Token: 0x040022D1 RID: 8913
		private const int Attr = 7;

		// Token: 0x040022D2 RID: 8914
		private const int InElm = 8;

		// Token: 0x040022D3 RID: 8915
		private const int EndEm = 9;

		// Token: 0x040022D4 RID: 8916
		private const int InCmt = 10;

		// Token: 0x040022D5 RID: 8917
		private const int InPI = 11;

		// Token: 0x040022D6 RID: 8918
		private const int StateMask = 15;

		// Token: 0x040022D7 RID: 8919
		internal const int Error = 16;

		// Token: 0x040022D8 RID: 8920
		private const int Ignor = 32;

		// Token: 0x040022D9 RID: 8921
		private const int Assrt = 48;

		// Token: 0x040022DA RID: 8922
		private const int U = 256;

		// Token: 0x040022DB RID: 8923
		private const int D = 512;

		// Token: 0x040022DC RID: 8924
		internal const int DepthMask = 768;

		// Token: 0x040022DD RID: 8925
		internal const int DepthUp = 256;

		// Token: 0x040022DE RID: 8926
		internal const int DepthDown = 512;

		// Token: 0x040022DF RID: 8927
		private const int C = 1024;

		// Token: 0x040022E0 RID: 8928
		private const int H = 2048;

		// Token: 0x040022E1 RID: 8929
		private const int M = 4096;

		// Token: 0x040022E2 RID: 8930
		internal const int BeginChild = 1024;

		// Token: 0x040022E3 RID: 8931
		internal const int HadChild = 2048;

		// Token: 0x040022E4 RID: 8932
		internal const int EmptyTag = 4096;

		// Token: 0x040022E5 RID: 8933
		private const int B = 8192;

		// Token: 0x040022E6 RID: 8934
		private const int E = 16384;

		// Token: 0x040022E7 RID: 8935
		internal const int BeginRecord = 8192;

		// Token: 0x040022E8 RID: 8936
		internal const int EndRecord = 16384;

		// Token: 0x040022E9 RID: 8937
		private const int S = 32768;

		// Token: 0x040022EA RID: 8938
		private const int P = 65536;

		// Token: 0x040022EB RID: 8939
		internal const int PushScope = 32768;

		// Token: 0x040022EC RID: 8940
		internal const int PopScope = 65536;

		// Token: 0x040022ED RID: 8941
		private int _State;

		// Token: 0x040022EE RID: 8942
		private static readonly int[][] s_BeginTransitions = new int[][]
		{
			new int[]
			{
				16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
				16, 16
			},
			new int[]
			{
				40961, 42241, 16, 16, 41985, 16, 16, 41985, 40961, 106497,
				16, 16
			},
			new int[]
			{
				16, 261, 16, 16, 5, 16, 16, 5, 16, 16,
				16, 16
			},
			new int[]
			{
				16, 258, 16, 16, 2, 16, 16, 16, 16, 16,
				16, 16
			},
			new int[]
			{
				8200, 9480, 259, 3, 9224, 262, 6, 9224, 8, 73736,
				10, 11
			},
			new int[]
			{
				8200, 9480, 259, 3, 9224, 262, 6, 9224, 8, 73736,
				10, 11
			},
			new int[]
			{
				8200, 9480, 259, 3, 9224, 262, 6, 9224, 8, 73736,
				10, 11
			},
			new int[]
			{
				8203, 9483, 16, 16, 9227, 16, 16, 9227, 8203, 73739,
				16, 16
			},
			new int[]
			{
				8202, 9482, 16, 16, 9226, 16, 16, 9226, 8202, 73738,
				16, 16
			},
			new int[]
			{
				16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
				16, 16
			}
		};

		// Token: 0x040022EF RID: 8943
		private static readonly int[][] s_EndTransitions = new int[][]
		{
			new int[]
			{
				48, 48, 48, 48, 48, 48, 48, 48, 48, 48,
				48, 48
			},
			new int[]
			{
				48, 94217, 48, 48, 94729, 48, 48, 94729, 92681, 92681,
				48, 48
			},
			new int[]
			{
				48, 48, 48, 48, 48, 7, 519, 48, 48, 48,
				48, 48
			},
			new int[]
			{
				48, 48, 4, 516, 48, 48, 48, 48, 48, 48,
				48, 48
			},
			new int[]
			{
				48, 48, 48, 48, 48, 48, 48, 48, 48, 48,
				48, 48
			},
			new int[]
			{
				48, 48, 48, 48, 48, 48, 48, 48, 48, 48,
				48, 48
			},
			new int[]
			{
				48, 48, 48, 48, 48, 48, 48, 48, 48, 48,
				48, 48
			},
			new int[]
			{
				48, 48, 48, 48, 48, 48, 48, 48, 48, 48,
				48, 16393
			},
			new int[]
			{
				48, 48, 48, 48, 48, 48, 48, 48, 48, 48,
				16393, 48
			},
			new int[]
			{
				48, 48, 48, 48, 48, 48, 48, 48, 48, 48,
				48, 48
			}
		};
	}
}
