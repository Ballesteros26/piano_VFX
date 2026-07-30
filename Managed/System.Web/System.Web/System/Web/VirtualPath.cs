using System;
using System.IO;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000E8 RID: 232
	internal class VirtualPath : IDisposable
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x000213E4 File Offset: 0x0001F5E4
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x000213EC File Offset: 0x0001F5EC
		public bool IsAbsolute { get; private set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x000213F5 File Offset: 0x0001F5F5
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x000213FD File Offset: 0x0001F5FD
		public bool IsFake { get; private set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00021406 File Offset: 0x0001F606
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x0002140E File Offset: 0x0001F60E
		public bool IsRooted { get; private set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00021417 File Offset: 0x0001F617
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x0002141F File Offset: 0x0001F61F
		public bool IsAppRelative { get; private set; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00021428 File Offset: 0x0001F628
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x00021430 File Offset: 0x0001F630
		public string Original { get; private set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0002143C File Offset: 0x0001F63C
		public string Absolute
		{
			get
			{
				if (this.IsAbsolute)
				{
					return this.Original;
				}
				if (this._absolute == null)
				{
					string original = this.Original;
					if (!VirtualPathUtility.IsRooted(original))
					{
						this._absolute = this.MakeRooted(original);
					}
					else
					{
						this._absolute = original;
					}
					if (VirtualPathUtility.IsAppRelative(this._absolute))
					{
						this._absolute = VirtualPathUtility.ToAbsolute(this._absolute);
					}
				}
				return this._absolute;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000214AC File Offset: 0x0001F6AC
		public string AppRelative
		{
			get
			{
				if (this.IsAppRelative)
				{
					return this.Original;
				}
				if (this._appRelative == null)
				{
					string original = this.Original;
					if (!VirtualPathUtility.IsRooted(original))
					{
						this._appRelative = this.MakeRooted(original);
					}
					else
					{
						this._appRelative = original;
					}
					if (VirtualPathUtility.IsAbsolute(this._appRelative))
					{
						this._appRelative = VirtualPathUtility.ToAppRelative(this._appRelative);
					}
				}
				return this._appRelative;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00021519 File Offset: 0x0001F719
		public string AppRelativeNotRooted
		{
			get
			{
				if (this._appRelativeNotRooted == null)
				{
					this._appRelativeNotRooted = this.AppRelative.Substring(2);
				}
				return this._appRelativeNotRooted;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0002153B File Offset: 0x0001F73B
		public string Extension
		{
			get
			{
				if (this._extension == null)
				{
					this._extension = VirtualPathUtility.GetExtension(this.Original);
				}
				return this._extension;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0002155C File Offset: 0x0001F75C
		public string Directory
		{
			get
			{
				if (this._directory == null)
				{
					this._directory = VirtualPathUtility.GetDirectory(this.Absolute);
				}
				return this._directory;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0002157D File Offset: 0x0001F77D
		public string DirectoryNoNormalize
		{
			get
			{
				if (this._directoryNoNormalize == null)
				{
					this._directoryNoNormalize = VirtualPathUtility.GetDirectory(this.Absolute, false);
				}
				return this._directoryNoNormalize;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x000215A0 File Offset: 0x0001F7A0
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x000215DF File Offset: 0x0001F7DF
		public string CurrentRequestDirectory
		{
			get
			{
				if (this._currentRequestDirectory != null)
				{
					return this._currentRequestDirectory;
				}
				HttpContext httpContext = HttpContext.Current;
				HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
				if (httpRequest != null)
				{
					return VirtualPathUtility.GetDirectory(httpRequest.CurrentExecutionFilePath);
				}
				return null;
			}
			set
			{
				this._currentRequestDirectory = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x000215E8 File Offset: 0x0001F7E8
		public string PhysicalPath
		{
			get
			{
				if (this._physicalPath != null)
				{
					return this._physicalPath;
				}
				HttpContext httpContext = HttpContext.Current;
				HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
				if (httpRequest != null)
				{
					this._physicalPath = httpRequest.MapPath(this.Absolute);
					return this._physicalPath;
				}
				return null;
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00021636 File Offset: 0x0001F836
		public VirtualPath(string vpath)
			: this(vpath, null, false)
		{
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00021641 File Offset: 0x0001F841
		public VirtualPath(string vpath, string baseVirtualDir)
			: this(vpath, null, false)
		{
			this.CurrentRequestDirectory = baseVirtualDir;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00021654 File Offset: 0x0001F854
		public VirtualPath(string vpath, string physicalPath, bool isFake)
		{
			this.IsRooted = VirtualPathUtility.IsRooted(vpath);
			this.IsAbsolute = VirtualPathUtility.IsAbsolute(vpath);
			this.IsAppRelative = VirtualPathUtility.IsAppRelative(vpath);
			if (!isFake)
			{
				this.Original = vpath;
				this.IsFake = false;
				return;
			}
			if (string.IsNullOrEmpty(physicalPath))
			{
				throw new ArgumentException("physicalPath");
			}
			this._physicalPath = physicalPath;
			this.Original = "~/" + Path.GetFileName(this._physicalPath);
			this.IsFake = true;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x000216D9 File Offset: 0x0001F8D9
		public bool StartsWith(string s)
		{
			return StrUtils.StartsWith(this.Original, s);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x000216E8 File Offset: 0x0001F8E8
		private string MakeRooted(string original)
		{
			string currentRequestDirectory = this.CurrentRequestDirectory;
			if (!string.IsNullOrEmpty(currentRequestDirectory))
			{
				return VirtualPathUtility.Combine(currentRequestDirectory, original);
			}
			return VirtualPathUtility.Combine(HttpRuntime.AppDomainAppVirtualPath, original);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00021717 File Offset: 0x0001F917
		public void Dispose()
		{
			this._absolute = null;
			this._appRelative = null;
			this._appRelativeNotRooted = null;
			this._extension = null;
			this._directory = null;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002173C File Offset: 0x0001F93C
		public override string ToString()
		{
			string text = this.Original;
			if (string.IsNullOrEmpty(text))
			{
				return base.GetType().ToString();
			}
			if (this.IsFake)
			{
				text = text + " [fake: " + this.PhysicalPath + "]";
			}
			return text;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00021784 File Offset: 0x0001F984
		public static VirtualPath PhysicalToVirtual(string physical_path)
		{
			if (string.IsNullOrEmpty(physical_path))
			{
				return null;
			}
			string appDomainAppPath = HttpRuntime.AppDomainAppPath;
			if (!StrUtils.StartsWith(physical_path, appDomainAppPath))
			{
				return null;
			}
			string text = physical_path.Substring(appDomainAppPath.Length - 1);
			if (text[0] != '/')
			{
				return null;
			}
			return new VirtualPath(text);
		}

		// Token: 0x04001106 RID: 4358
		private string _absolute;

		// Token: 0x04001107 RID: 4359
		private string _appRelative;

		// Token: 0x04001108 RID: 4360
		private string _appRelativeNotRooted;

		// Token: 0x04001109 RID: 4361
		private string _extension;

		// Token: 0x0400110A RID: 4362
		private string _directory;

		// Token: 0x0400110B RID: 4363
		private string _directoryNoNormalize;

		// Token: 0x0400110C RID: 4364
		private string _currentRequestDirectory;

		// Token: 0x0400110D RID: 4365
		private string _physicalPath;
	}
}
