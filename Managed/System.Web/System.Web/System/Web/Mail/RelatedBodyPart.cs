using System;
using System.IO;

namespace System.Web.Mail
{
	// Token: 0x020000FB RID: 251
	public class RelatedBodyPart
	{
		// Token: 0x06000D56 RID: 3414 RVA: 0x00023F7D File Offset: 0x0002217D
		public RelatedBodyPart(string id, string fileName)
		{
			this.id = id;
			if (this.FileExists(fileName))
			{
				this.fileName = fileName;
				return;
			}
			throw new HttpException(500, "Invalid related body part");
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00023FAC File Offset: 0x000221AC
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x00023FB4 File Offset: 0x000221B4
		public string Name
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x00023FBD File Offset: 0x000221BD
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x00023FC5 File Offset: 0x000221C5
		public string Path
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

		// Token: 0x06000D5B RID: 3419 RVA: 0x00023FD0 File Offset: 0x000221D0
		private bool FileExists(string fileName)
		{
			bool flag;
			try
			{
				File.OpenRead(fileName).Close();
				flag = true;
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04001149 RID: 4425
		private string id;

		// Token: 0x0400114A RID: 4426
		private string fileName;
	}
}
