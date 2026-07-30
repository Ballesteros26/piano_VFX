using System;
using System.Text;

namespace System.Web.Util
{
	// Token: 0x0200014F RID: 335
	internal class UrlUtils
	{
		// Token: 0x06000EF8 RID: 3832 RVA: 0x0002A8E8 File Offset: 0x00028AE8
		public static string InsertSessionId(string id, string path)
		{
			string text = UrlUtils.GetDirectory(path);
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			string text2 = HttpRuntime.AppDomainAppVirtualPath;
			if (!text2.EndsWith("/"))
			{
				text2 += "/";
			}
			if (path.StartsWith(text2))
			{
				path = path.Substring(text2.Length);
			}
			if (path.StartsWith("/"))
			{
				path = ((path.Length > 1) ? path.Substring(1) : "");
			}
			return UrlUtils.Canonic(string.Concat(new string[] { text2, "(", id, ")/", path }));
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0002A9A0 File Offset: 0x00028BA0
		public static string GetSessionId(string path)
		{
			if (path == null)
			{
				return null;
			}
			int length = HttpRuntime.AppDomainAppVirtualPath.Length;
			if (path.Length <= length)
			{
				return null;
			}
			path = path.Substring(length);
			int num = path.Length;
			if (num == 0 || path[0] != '/')
			{
				path = "/" + path;
				num++;
			}
			if (num < 27 || path[1] != '(' || path[26] != ')')
			{
				return null;
			}
			return path.Substring(2, 24);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0002AA1E File Offset: 0x00028C1E
		public static bool HasSessionId(string path)
		{
			return path != null && path.Length >= 5 && StrUtils.StartsWith(path, "/(") && path.IndexOf(")/") > 2;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0002AA4C File Offset: 0x00028C4C
		public static string RemoveSessionId(string base_path, string file_path)
		{
			int num = base_path.IndexOf("/(");
			string text = base_path.Substring(0, num + 1);
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			num = base_path.IndexOf(")/");
			if (num != -1 && base_path.Length > num + 2)
			{
				string text2 = base_path.Substring(num + 2);
				if (!text2.EndsWith("/"))
				{
					text2 += "/";
				}
				text += text2;
			}
			return UrlUtils.Canonic(text + UrlUtils.GetFile(file_path));
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0002AAE0 File Offset: 0x00028CE0
		public static string Combine(string basePath, string relPath)
		{
			if (relPath == null)
			{
				throw new ArgumentNullException("relPath");
			}
			int length = relPath.Length;
			if (length == 0)
			{
				return "";
			}
			relPath = relPath.Replace('\\', '/');
			if (UrlUtils.IsRooted(relPath))
			{
				return UrlUtils.Canonic(relPath);
			}
			char c = relPath[0];
			if (length >= 3 && c != '~' && c != '/' && c != '\\')
			{
				if (basePath == null || basePath.Length == 0 || basePath[0] == '~')
				{
					basePath = HttpRuntime.AppDomainAppVirtualPath;
				}
				if (basePath.Length <= 1)
				{
					basePath = string.Empty;
				}
				return UrlUtils.Canonic(basePath + "/" + relPath);
			}
			if (basePath == null || (basePath.Length == 1 && basePath[0] == '/'))
			{
				basePath = string.Empty;
			}
			string text = ((c == '/') ? "" : "/");
			if (c == '~')
			{
				if (length == 1)
				{
					relPath = "";
				}
				else if (length > 1 && relPath[1] == '/')
				{
					relPath = relPath.Substring(2);
					text = "/";
				}
				string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
				if (appDomainAppVirtualPath.EndsWith("/"))
				{
					text = "";
				}
				return UrlUtils.Canonic(appDomainAppVirtualPath + text + relPath);
			}
			return UrlUtils.Canonic(basePath + text + relPath);
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0002AC18 File Offset: 0x00028E18
		public static string Canonic(string path)
		{
			bool flag = UrlUtils.IsRooted(path);
			bool flag2 = path.EndsWith("/");
			string[] array = path.Split(UrlUtils.path_sep);
			int num = array.Length;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				string text = array[i];
				if (text.Length != 0 && !(text == "."))
				{
					if (text == "..")
					{
						num2--;
					}
					else
					{
						if (num2 < 0)
						{
							if (!flag)
							{
								throw new HttpException("Invalid path.");
							}
							num2 = 0;
						}
						array[num2++] = text;
					}
				}
			}
			if (num2 < 0)
			{
				throw new HttpException("Invalid path.");
			}
			if (num2 == 0)
			{
				return "/";
			}
			string text2 = string.Join("/", array, 0, num2);
			text2 = UrlUtils.RemoveDoubleSlashes(text2);
			if (flag)
			{
				text2 = "/" + text2;
			}
			if (flag2)
			{
				text2 += "/";
			}
			return text2;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0002AD08 File Offset: 0x00028F08
		public static string GetDirectory(string url)
		{
			url = url.Replace('\\', '/');
			int num = url.LastIndexOf('/');
			if (num > 0)
			{
				if (num < url.Length)
				{
					num++;
				}
				return UrlUtils.RemoveDoubleSlashes(url.Substring(0, num));
			}
			return "/";
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0002AD50 File Offset: 0x00028F50
		public static string RemoveDoubleSlashes(string input)
		{
			int num = -1;
			for (int i = 1; i < input.Length; i++)
			{
				if (input[i] == '/' && input[i - 1] == '/')
				{
					num = i - 1;
					break;
				}
			}
			if (num == -1)
			{
				return input;
			}
			StringBuilder stringBuilder = new StringBuilder(input.Length);
			stringBuilder.Append(input, 0, num);
			for (int j = num; j < input.Length; j++)
			{
				if (input[j] == '/')
				{
					int num2 = j + 1;
					if (num2 >= input.Length || input[num2] != '/')
					{
						stringBuilder.Append('/');
					}
				}
				else
				{
					stringBuilder.Append(input[j]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0002AE00 File Offset: 0x00029000
		public static string GetFile(string url)
		{
			url = url.Replace('\\', '/');
			int num = url.LastIndexOf('/');
			if (num < 0)
			{
				throw new ArgumentException(string.Format("GetFile: `{0}' does not contain a /", url));
			}
			if (url.Length == 1)
			{
				return "";
			}
			return url.Substring(num + 1);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0002AE50 File Offset: 0x00029050
		public static bool IsRooted(string path)
		{
			if (path == null || path.Length == 0)
			{
				return true;
			}
			char c = path[0];
			return c == '/' || c == '\\';
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0002AE7F File Offset: 0x0002907F
		public static bool IsRelativeUrl(string path)
		{
			return path[0] != '/' && path.IndexOf(':') == -1;
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0002AE9C File Offset: 0x0002909C
		public static string ResolveVirtualPathFromAppAbsolute(string path)
		{
			if (path[0] != '~')
			{
				return path;
			}
			if (path.Length == 1)
			{
				return HttpRuntime.AppDomainAppVirtualPath;
			}
			if (path[1] != '/' && path[1] != '\\')
			{
				return path;
			}
			string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
			if (appDomainAppVirtualPath.Length > 1)
			{
				return appDomainAppVirtualPath + "/" + path.Substring(2);
			}
			return "/" + path.Substring(2);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0002AF10 File Offset: 0x00029110
		public static string ResolvePhysicalPathFromAppAbsolute(string path)
		{
			if (path[0] != '~')
			{
				return path;
			}
			if (path.Length == 1)
			{
				return HttpRuntime.AppDomainAppPath;
			}
			if (path[1] != '/' && path[1] != '\\')
			{
				return path;
			}
			string appDomainAppPath = HttpRuntime.AppDomainAppPath;
			if (appDomainAppPath.Length > 1)
			{
				return appDomainAppPath + "/" + path.Substring(2);
			}
			return "/" + path.Substring(2);
		}

		// Token: 0x04001221 RID: 4641
		private static char[] path_sep = new char[] { '\\', '/' };
	}
}
