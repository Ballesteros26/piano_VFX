using System;
using System.Diagnostics;

namespace System.Xml.Xsl
{
	// Token: 0x020004C1 RID: 1217
	[DebuggerDisplay("{Uri} [{StartLine},{StartPos} -- {EndLine},{EndPos}]")]
	internal class SourceLineInfo : ISourceLineInfo
	{
		// Token: 0x06003154 RID: 12628 RVA: 0x0011CC8F File Offset: 0x0011AE8F
		public SourceLineInfo(string uriString, int startLine, int startPos, int endLine, int endPos)
			: this(uriString, new Location(startLine, startPos), new Location(endLine, endPos))
		{
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x0011CCA8 File Offset: 0x0011AEA8
		public SourceLineInfo(string uriString, Location start, Location end)
		{
			this.uriString = uriString;
			this.start = start;
			this.end = end;
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x0011CCC5 File Offset: 0x0011AEC5
		public string Uri
		{
			get
			{
				return this.uriString;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06003157 RID: 12631 RVA: 0x0011CCCD File Offset: 0x0011AECD
		public int StartLine
		{
			get
			{
				return this.start.Line;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x0011CCDA File Offset: 0x0011AEDA
		public int StartPos
		{
			get
			{
				return this.start.Pos;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06003159 RID: 12633 RVA: 0x0011CCE7 File Offset: 0x0011AEE7
		public int EndLine
		{
			get
			{
				return this.end.Line;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x0011CCF4 File Offset: 0x0011AEF4
		public int EndPos
		{
			get
			{
				return this.end.Pos;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x0011CD01 File Offset: 0x0011AF01
		public Location End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x0011CD09 File Offset: 0x0011AF09
		public Location Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x0011CD11 File Offset: 0x0011AF11
		public bool IsNoSource
		{
			get
			{
				return this.StartLine == 16707566;
			}
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x0011CD20 File Offset: 0x0011AF20
		[Conditional("DEBUG")]
		public static void Validate(ISourceLineInfo lineInfo)
		{
			if (lineInfo.Start.Line != 0)
			{
				int line = lineInfo.Start.Line;
			}
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x0011CD54 File Offset: 0x0011AF54
		public static string GetFileName(string uriString)
		{
			Uri uri;
			if (uriString.Length != 0 && global::System.Uri.TryCreate(uriString, UriKind.Absolute, out uri) && uri.IsFile)
			{
				return uri.LocalPath;
			}
			return uriString;
		}

		// Token: 0x0400203E RID: 8254
		protected string uriString;

		// Token: 0x0400203F RID: 8255
		protected Location start;

		// Token: 0x04002040 RID: 8256
		protected Location end;

		// Token: 0x04002041 RID: 8257
		protected const int NoSourceMagicNumber = 16707566;

		// Token: 0x04002042 RID: 8258
		public static SourceLineInfo NoSource = new SourceLineInfo(string.Empty, 16707566, 0, 16707566, 0);
	}
}
