using System;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	/// <summary>Represents a file system directory path that should not be searched for references to add to a Web services discovery document.</summary>
	// Token: 0x020000B4 RID: 180
	public sealed class ExcludePathInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.ExcludePathInfo" /> class. </summary>
		// Token: 0x060004B6 RID: 1206 RVA: 0x0000210F File Offset: 0x0000030F
		public ExcludePathInfo()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.ExcludePathInfo" /> class and specifies the file system path to exclude from searches. </summary>
		/// <param name="path">The path to exclude from searches.</param>
		// Token: 0x060004B7 RID: 1207 RVA: 0x000161ED File Offset: 0x000143ED
		public ExcludePathInfo(string path)
		{
			this.path = path;
		}

		/// <summary>Gets or sets the file system directory path that should not be searched for references to add to a discovery document.</summary>
		/// <returns>The file system directory path that should be excluded from searches.</returns>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000161FC File Offset: 0x000143FC
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x00016204 File Offset: 0x00014404
		[XmlAttribute("path")]
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		// Token: 0x0400035B RID: 859
		private string path;
	}
}
