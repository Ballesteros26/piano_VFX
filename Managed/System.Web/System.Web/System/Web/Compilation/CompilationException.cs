using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Compilation
{
	// Token: 0x0200064C RID: 1612
	[Serializable]
	internal class CompilationException : HtmlizedException
	{
		// Token: 0x06004551 RID: 17745 RVA: 0x000BDBF4 File Offset: 0x000BBDF4
		private CompilationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.filename = info.GetString("filename");
			this.errors = info.GetValue("errors", typeof(CompilerErrorCollection)) as CompilerErrorCollection;
			this.results = info.GetValue("results", typeof(CompilerResults)) as CompilerResults;
			this.fileText = info.GetString("fileText");
			this.errmsg = info.GetString("errmsg");
			this.errorLines = info.GetValue("errorLines", typeof(int[])) as int[];
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x000BDC9C File Offset: 0x000BBE9C
		public CompilationException(string filename, CompilerErrorCollection errors, string fileText)
		{
			this.filename = filename;
			this.errors = errors;
			this.fileText = fileText;
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x000BDCB9 File Offset: 0x000BBEB9
		public CompilationException(string filename, CompilerResults results, string fileText)
			: this(filename, (results != null) ? results.Errors : null, fileText)
		{
			this.results = results;
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x000BDCD8 File Offset: 0x000BBED8
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			base.GetObjectData(info, ctx);
			info.AddValue("filename", this.filename);
			info.AddValue("errors", this.errors);
			info.AddValue("results", this.results);
			info.AddValue("fileText", this.fileText);
			info.AddValue("errmsg", this.errmsg);
			info.AddValue("errorLines", this.errorLines);
		}

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x06004555 RID: 17749 RVA: 0x000BDD53 File Offset: 0x000BBF53
		public override string Message
		{
			get
			{
				return this.ErrorMessage;
			}
		}

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06004556 RID: 17750 RVA: 0x000BDD5B File Offset: 0x000BBF5B
		public override string SourceFile
		{
			get
			{
				if (this.errors == null || this.errors.Count == 0)
				{
					return this.filename;
				}
				return this.errors[0].FileName;
			}
		}

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06004557 RID: 17751 RVA: 0x000BDD8A File Offset: 0x000BBF8A
		public override string FileName
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06004558 RID: 17752 RVA: 0x000BDD92 File Offset: 0x000BBF92
		public override string Title
		{
			get
			{
				return "Compilation Error";
			}
		}

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x000BDD99 File Offset: 0x000BBF99
		public override string Description
		{
			get
			{
				return "Error compiling a resource required to service this request. Review your source file and modify it to fix this error.";
			}
		}

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x0600455A RID: 17754 RVA: 0x000BDDA0 File Offset: 0x000BBFA0
		public override string ErrorMessage
		{
			get
			{
				if (this.errmsg == null && this.errors != null)
				{
					CompilerError compilerError = null;
					foreach (object obj in this.errors)
					{
						CompilerError compilerError2 = (CompilerError)obj;
						if (!compilerError2.IsWarning)
						{
							compilerError = compilerError2;
							break;
						}
					}
					if (compilerError != null)
					{
						this.errmsg = compilerError.ToString();
						int num = this.errmsg.IndexOf(" : error ");
						if (num > -1)
						{
							this.errmsg = this.errmsg.Substring(num + 9);
						}
					}
					else
					{
						this.errmsg = string.Empty;
					}
				}
				return this.errmsg;
			}
		}

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x000BDE68 File Offset: 0x000BC068
		public override string FileText
		{
			get
			{
				return this.fileText;
			}
		}

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x0600455C RID: 17756 RVA: 0x000BDE70 File Offset: 0x000BC070
		public override int[] ErrorLines
		{
			get
			{
				if (this.errorLines == null && this.errors != null)
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj in this.errors)
					{
						CompilerError compilerError = (CompilerError)obj;
						if (!compilerError.IsWarning && compilerError.Line != 0 && !arrayList.Contains(compilerError.Line))
						{
							arrayList.Add(compilerError.Line);
						}
					}
					this.errorLines = (int[])arrayList.ToArray(typeof(int));
					Array.Sort<int>(this.errorLines);
				}
				return this.errorLines;
			}
		}

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x0600455D RID: 17757 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool ErrorLinesPaired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x0600455E RID: 17758 RVA: 0x000BDF40 File Offset: 0x000BC140
		public StringCollection CompilerOutput
		{
			get
			{
				if (this.results == null)
				{
					return null;
				}
				return this.results.Output;
			}
		}

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x000BDF57 File Offset: 0x000BC157
		public CompilerResults Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x040024DE RID: 9438
		private string filename;

		// Token: 0x040024DF RID: 9439
		private CompilerErrorCollection errors;

		// Token: 0x040024E0 RID: 9440
		private CompilerResults results;

		// Token: 0x040024E1 RID: 9441
		private string fileText;

		// Token: 0x040024E2 RID: 9442
		private string errmsg;

		// Token: 0x040024E3 RID: 9443
		private int[] errorLines;
	}
}
