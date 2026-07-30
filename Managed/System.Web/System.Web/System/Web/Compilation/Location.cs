using System;

namespace System.Web.Compilation
{
	// Token: 0x0200065C RID: 1628
	internal class Location : ILocation
	{
		// Token: 0x060045BB RID: 17851 RVA: 0x000BEF1A File Offset: 0x000BD11A
		public Location(ILocation location)
		{
			this.Init(location);
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x000BEF2C File Offset: 0x000BD12C
		public void Init(ILocation location)
		{
			if (location == null)
			{
				this.beginLine = 0;
				this.endLine = 0;
				this.beginColumn = 0;
				this.endColumn = 0;
				this.fileName = null;
				this.plainText = null;
			}
			else
			{
				this.beginLine = location.BeginLine;
				this.endLine = location.EndLine;
				this.beginColumn = location.BeginColumn;
				this.endColumn = location.EndColumn;
				this.fileName = location.Filename;
				this.plainText = location.PlainText;
			}
			this.location = location;
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x060045BD RID: 17853 RVA: 0x000BEFB7 File Offset: 0x000BD1B7
		// (set) Token: 0x060045BE RID: 17854 RVA: 0x000BEFBF File Offset: 0x000BD1BF
		public string Filename
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x060045BF RID: 17855 RVA: 0x000BEFC8 File Offset: 0x000BD1C8
		// (set) Token: 0x060045C0 RID: 17856 RVA: 0x000BEFD0 File Offset: 0x000BD1D0
		public int BeginLine
		{
			get
			{
				return this.beginLine;
			}
			set
			{
				this.beginLine = value;
			}
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x060045C1 RID: 17857 RVA: 0x000BEFD9 File Offset: 0x000BD1D9
		// (set) Token: 0x060045C2 RID: 17858 RVA: 0x000BEFE1 File Offset: 0x000BD1E1
		public int EndLine
		{
			get
			{
				return this.endLine;
			}
			set
			{
				this.endLine = value;
			}
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x060045C3 RID: 17859 RVA: 0x000BEFEA File Offset: 0x000BD1EA
		// (set) Token: 0x060045C4 RID: 17860 RVA: 0x000BEFF2 File Offset: 0x000BD1F2
		public int BeginColumn
		{
			get
			{
				return this.beginColumn;
			}
			set
			{
				this.beginColumn = value;
			}
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x000BEFFB File Offset: 0x000BD1FB
		// (set) Token: 0x060045C6 RID: 17862 RVA: 0x000BF003 File Offset: 0x000BD203
		public int EndColumn
		{
			get
			{
				return this.endColumn;
			}
			set
			{
				this.endColumn = value;
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x060045C7 RID: 17863 RVA: 0x000BF00C File Offset: 0x000BD20C
		// (set) Token: 0x060045C8 RID: 17864 RVA: 0x000BF014 File Offset: 0x000BD214
		public string PlainText
		{
			get
			{
				return this.plainText;
			}
			set
			{
				this.plainText = value;
			}
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x060045C9 RID: 17865 RVA: 0x000BF01D File Offset: 0x000BD21D
		public string FileText
		{
			get
			{
				if (this.location != null)
				{
					return this.location.FileText;
				}
				return null;
			}
		}

		// Token: 0x04002503 RID: 9475
		private int beginLine;

		// Token: 0x04002504 RID: 9476
		private int endLine;

		// Token: 0x04002505 RID: 9477
		private int beginColumn;

		// Token: 0x04002506 RID: 9478
		private int endColumn;

		// Token: 0x04002507 RID: 9479
		private string fileName;

		// Token: 0x04002508 RID: 9480
		private string plainText;

		// Token: 0x04002509 RID: 9481
		private ILocation location;
	}
}
