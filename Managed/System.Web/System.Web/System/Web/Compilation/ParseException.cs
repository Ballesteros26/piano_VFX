using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Compilation
{
	// Token: 0x02000662 RID: 1634
	[Serializable]
	internal class ParseException : HtmlizedException
	{
		// Token: 0x060045F5 RID: 17909 RVA: 0x000C0A94 File Offset: 0x000BEC94
		private ParseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x000C0A9E File Offset: 0x000BEC9E
		public ParseException(ILocation location, string message)
			: this(location, message, null)
		{
			location = new Location(location);
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x000C0AB1 File Offset: 0x000BECB1
		public ParseException(ILocation location, string message, Exception inner)
			: base(message, inner)
		{
			this.location = location;
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x060045F8 RID: 17912 RVA: 0x000C0AC2 File Offset: 0x000BECC2
		public override string Title
		{
			get
			{
				return "Parser Error";
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x060045F9 RID: 17913 RVA: 0x000C0AC9 File Offset: 0x000BECC9
		public override string Description
		{
			get
			{
				return "Error parsing a resource required to service this request. Review your source file and modify it to fix this error.";
			}
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x060045FA RID: 17914 RVA: 0x000C0AD0 File Offset: 0x000BECD0
		public override string ErrorMessage
		{
			get
			{
				return this.Message;
			}
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x000C0AD8 File Offset: 0x000BECD8
		public override string SourceFile
		{
			get
			{
				return this.FileName;
			}
		}

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x060045FC RID: 17916 RVA: 0x000C0AE0 File Offset: 0x000BECE0
		public override string FileName
		{
			get
			{
				if (this.location == null)
				{
					return null;
				}
				return this.location.Filename;
			}
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x060045FD RID: 17917 RVA: 0x000C0AF8 File Offset: 0x000BECF8
		public override string FileText
		{
			get
			{
				if (this.fileText != null)
				{
					return this.fileText;
				}
				string text = ((this.location != null) ? this.location.FileText : null);
				if (text != null && text.Length > 0)
				{
					return text;
				}
				if (this.FileName == null)
				{
					return null;
				}
				using (TextReader textReader = new StreamReader(this.FileName))
				{
					this.fileText = textReader.ReadToEnd();
				}
				return this.fileText;
			}
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x060045FE RID: 17918 RVA: 0x000C0B80 File Offset: 0x000BED80
		public override int[] ErrorLines
		{
			get
			{
				if (this.location == null)
				{
					return null;
				}
				return new int[]
				{
					this.location.BeginLine,
					this.location.EndLine
				};
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x060045FF RID: 17919 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool ErrorLinesPaired
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x000C0BAE File Offset: 0x000BEDAE
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			base.GetObjectData(info, ctx);
		}

		// Token: 0x0400250E RID: 9486
		private ILocation location;

		// Token: 0x0400250F RID: 9487
		private string fileText;
	}
}
