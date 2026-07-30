using System;
using System.Text;
using System.Web.Configuration;
using System.Web.Util;
using Microsoft.Win32;

namespace System.Web
{
	/// <summary>Provides utility methods for common virtual path operations.  </summary>
	// Token: 0x020000E9 RID: 233
	public static class VirtualPathUtility
	{
		// Token: 0x06000C82 RID: 3202 RVA: 0x000217D0 File Offset: 0x0001F9D0
		static VirtualPathUtility()
		{
			try
			{
				VirtualPathUtility.runningOnWindows = RuntimeHelpers.RunningOnWindows;
				MonoSettingsSection monoSettingsSection = WebConfigurationManager.GetWebApplicationSection("system.web/monoSettings") as MonoSettingsSection;
				if (monoSettingsSection != null)
				{
					VirtualPathUtility.monoSettingsVerifyCompatibility = monoSettingsSection.VerificationCompatibility != 1;
				}
			}
			catch
			{
			}
		}

		/// <summary>Appends the literal slash mark (/) to the end of the virtual path, if one does not already exist.</summary>
		/// <returns>The modified virtual path.</returns>
		/// <param name="virtualPath">The virtual path to append the slash mark to.</param>
		// Token: 0x06000C83 RID: 3203 RVA: 0x00021850 File Offset: 0x0001FA50
		public static string AppendTrailingSlash(string virtualPath)
		{
			if (virtualPath == null)
			{
				return virtualPath;
			}
			int length = virtualPath.Length;
			if (length == 0 || virtualPath[length - 1] == '/')
			{
				return virtualPath;
			}
			return virtualPath + "/";
		}

		/// <summary>Combines a base path and a relative path.</summary>
		/// <returns>The combined <paramref name="basePath" /> and <paramref name="relativePath" />.</returns>
		/// <param name="basePath">The base path.</param>
		/// <param name="relativePath">The relative path.</param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="relativePath" /> is a physical path.-or-<paramref name="relativePath" /> includes one or more colons.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="relativePath" /> is null or an empty string.-or-<paramref name="basePath" /> is null or an empty string.</exception>
		// Token: 0x06000C84 RID: 3204 RVA: 0x00021888 File Offset: 0x0001FA88
		public static string Combine(string basePath, string relativePath)
		{
			basePath = VirtualPathUtility.Normalize(basePath);
			if (VirtualPathUtility.IsRooted(relativePath))
			{
				return VirtualPathUtility.Normalize(relativePath);
			}
			int length = basePath.Length;
			if (basePath[length - 1] != '/')
			{
				if (length > 1)
				{
					int num = basePath.LastIndexOf('/');
					if (num >= 0)
					{
						basePath = basePath.Substring(0, num + 1);
					}
				}
				else
				{
					basePath += "/";
				}
			}
			return VirtualPathUtility.Normalize(basePath + relativePath);
		}

		/// <summary>Returns the directory portion of a virtual path.</summary>
		/// <returns>The directory referenced in the virtual path. </returns>
		/// <param name="virtualPath">The virtual path.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="virtualPath" /> is not rooted. - or -<paramref name="virtualPath" /> is null or an empty string.</exception>
		// Token: 0x06000C85 RID: 3205 RVA: 0x000218F8 File Offset: 0x0001FAF8
		public static string GetDirectory(string virtualPath)
		{
			return VirtualPathUtility.GetDirectory(virtualPath, true);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00021904 File Offset: 0x0001FB04
		internal static string GetDirectory(string virtualPath, bool normalize)
		{
			if (normalize)
			{
				virtualPath = VirtualPathUtility.Normalize(virtualPath);
			}
			int num = virtualPath.Length;
			if (VirtualPathUtility.IsAppRelative(virtualPath) && num < 3)
			{
				virtualPath = VirtualPathUtility.ToAbsolute(virtualPath);
				num = virtualPath.Length;
			}
			if (num == 1 && virtualPath[0] == '/')
			{
				return null;
			}
			int num2 = virtualPath.LastIndexOf('/', num - 2, num - 2);
			if (num2 > 0)
			{
				return virtualPath.Substring(0, num2 + 1);
			}
			return "/";
		}

		/// <summary>Retrieves the extension of the file that is referenced in the virtual path.</summary>
		/// <returns>The file name extension string literal, including the period (.), null, or an empty string ("").</returns>
		/// <param name="virtualPath">The virtual path.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="virtualPath" /> contains one or more characters that are not valid, as defined in <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		// Token: 0x06000C87 RID: 3207 RVA: 0x00021974 File Offset: 0x0001FB74
		public static string GetExtension(string virtualPath)
		{
			if (StrUtils.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentNullException("virtualPath");
			}
			virtualPath = VirtualPathUtility.Canonize(virtualPath);
			int num = virtualPath.LastIndexOf('.');
			if (num == -1 || num == virtualPath.Length - 1 || num < virtualPath.LastIndexOf('/'))
			{
				return string.Empty;
			}
			return virtualPath.Substring(num);
		}

		/// <summary>Retrieves the file name of the file that is referenced in the virtual path.</summary>
		/// <returns>The file name literal after the last directory character in <paramref name="virtualPath" />; otherwise, the last directory name if the last character of <paramref name="virtualPath" /> is a directory or volume separator character.</returns>
		/// <param name="virtualPath">The virtual path. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="virtualPath" /> contains one or more characters that are not valid, as defined in <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		// Token: 0x06000C88 RID: 3208 RVA: 0x000219CC File Offset: 0x0001FBCC
		public static string GetFileName(string virtualPath)
		{
			virtualPath = VirtualPathUtility.Normalize(virtualPath);
			if (VirtualPathUtility.IsAppRelative(virtualPath) && virtualPath.Length < 3)
			{
				virtualPath = VirtualPathUtility.ToAbsolute(virtualPath);
			}
			if (virtualPath.Length == 1 && virtualPath[0] == '/')
			{
				return string.Empty;
			}
			virtualPath = VirtualPathUtility.RemoveTrailingSlash(virtualPath);
			int num = virtualPath.LastIndexOf('/');
			return virtualPath.Substring(num + 1);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00021A2E File Offset: 0x0001FC2E
		internal static bool IsRooted(string virtualPath)
		{
			return VirtualPathUtility.IsAbsolute(virtualPath) || VirtualPathUtility.IsAppRelative(virtualPath);
		}

		/// <summary>Returns a Boolean value indicating whether the specified virtual path is absolute; that is, it starts with a literal slash mark (/).</summary>
		/// <returns>true if <paramref name="virtualPath" /> is an absolute path and is not null or an empty string (""); otherwise, false.</returns>
		/// <param name="virtualPath">The virtual path to check. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x06000C8A RID: 3210 RVA: 0x00021A40 File Offset: 0x0001FC40
		public static bool IsAbsolute(string virtualPath)
		{
			if (StrUtils.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentNullException("virtualPath");
			}
			return virtualPath[0] == '/' || virtualPath[0] == '\\';
		}

		/// <summary>Returns a Boolean value indicating whether the specified virtual path is relative to the application.</summary>
		/// <returns>true if <paramref name="virtualPath" /> is relative to the application; otherwise, false.</returns>
		/// <param name="virtualPath">The virtual path to check. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x06000C8B RID: 3211 RVA: 0x00021A70 File Offset: 0x0001FC70
		public static bool IsAppRelative(string virtualPath)
		{
			if (StrUtils.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentNullException("virtualPath");
			}
			return (virtualPath.Length == 1 && virtualPath[0] == '~') || (virtualPath[0] == '~' && (virtualPath[1] == '/' || virtualPath[1] == '\\'));
		}

		/// <summary>Returns the relative virtual path from one virtual path containing the root operator (the tilde [~]) to another.</summary>
		/// <returns>The relative virtual path from <paramref name="fromPath" /> to <paramref name="toPath" />.</returns>
		/// <param name="fromPath">The starting virtual path to return the relative virtual path from.</param>
		/// <param name="toPath">The ending virtual path to return the relative virtual path to.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromPath" /> is not rooted.- or -<paramref name="toPath" /> is not rooted.</exception>
		// Token: 0x06000C8C RID: 3212 RVA: 0x00021ACC File Offset: 0x0001FCCC
		public static string MakeRelative(string fromPath, string toPath)
		{
			if (fromPath == null || toPath == null)
			{
				throw new NullReferenceException();
			}
			if (toPath == "")
			{
				return toPath;
			}
			toPath = VirtualPathUtility.ToAbsoluteInternal(toPath);
			fromPath = VirtualPathUtility.ToAbsoluteInternal(fromPath);
			if (string.CompareOrdinal(fromPath, toPath) == 0 && fromPath[fromPath.Length - 1] == '/')
			{
				return "./";
			}
			string[] array = toPath.Split(new char[] { '/' });
			string[] array2 = fromPath.Split(new char[] { '/' });
			int num = 1;
			while (array[num] == array2[num] && array.Length != num + 1 && array2.Length != num + 1)
			{
				num++;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < array2.Length - num; i++)
			{
				stringBuilder.Append("../");
			}
			for (int j = num; j < array.Length; j++)
			{
				stringBuilder.Append(array[j]);
				if (j < array.Length - 1)
				{
					stringBuilder.Append('/');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00021BC9 File Offset: 0x0001FDC9
		private static string ToAbsoluteInternal(string virtualPath)
		{
			if (VirtualPathUtility.IsAppRelative(virtualPath))
			{
				return VirtualPathUtility.ToAbsolute(virtualPath, HttpRuntime.AppDomainAppVirtualPath);
			}
			if (VirtualPathUtility.IsAbsolute(virtualPath))
			{
				return VirtualPathUtility.Normalize(virtualPath);
			}
			throw new ArgumentOutOfRangeException("Specified argument was out of the range of valid values.");
		}

		/// <summary>Removes a trailing slash mark (/) from a virtual path.</summary>
		/// <returns>A virtual path without a trailing slash mark, if the virtual path is not already the root directory ("/"); otherwise, null.</returns>
		/// <param name="virtualPath">The virtual path to remove any trailing slash mark from. </param>
		// Token: 0x06000C8E RID: 3214 RVA: 0x00021BF8 File Offset: 0x0001FDF8
		public static string RemoveTrailingSlash(string virtualPath)
		{
			if (virtualPath == null || virtualPath == "")
			{
				return null;
			}
			int num = virtualPath.Length - 1;
			if (num == 0 || virtualPath[num] != '/')
			{
				return virtualPath;
			}
			return virtualPath.Substring(0, num);
		}

		/// <summary>Converts a virtual path to an application absolute path.</summary>
		/// <returns>The absolute path representation of the specified virtual path. </returns>
		/// <param name="virtualPath">The virtual path to convert to an application-relative path. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="virtualPath" /> is not rooted. </exception>
		/// <exception cref="T:System.Web.HttpException">A leading double period (..) is used to exit above the top directory.</exception>
		// Token: 0x06000C8F RID: 3215 RVA: 0x00021C38 File Offset: 0x0001FE38
		public static string ToAbsolute(string virtualPath)
		{
			return VirtualPathUtility.ToAbsolute(virtualPath, true);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00021C44 File Offset: 0x0001FE44
		internal static string ToAbsolute(string virtualPath, bool normalize)
		{
			if (VirtualPathUtility.IsAbsolute(virtualPath))
			{
				if (normalize)
				{
					return VirtualPathUtility.Normalize(virtualPath);
				}
				return virtualPath;
			}
			else
			{
				string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
				if (appDomainAppVirtualPath == null)
				{
					throw new HttpException("The path to the application is not known");
				}
				if (virtualPath.Length == 1 && virtualPath[0] == '~')
				{
					return appDomainAppVirtualPath;
				}
				return VirtualPathUtility.ToAbsolute(virtualPath, appDomainAppVirtualPath, normalize);
			}
		}

		/// <summary>Converts a virtual path to an application absolute path using the specified application path.</summary>
		/// <returns>The absolute virtual path representation of <paramref name="virtualPath" />.</returns>
		/// <param name="virtualPath">The virtual path to convert to an application-relative path.</param>
		/// <param name="applicationPath">The application path to use to convert <paramref name="virtualPath" /> to a relative path.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="applicationPath" /> is not rooted.</exception>
		/// <exception cref="T:System.Web.HttpException">A leading double period (..) is used in the application path to exit above the top directory.</exception>
		// Token: 0x06000C91 RID: 3217 RVA: 0x00021C97 File Offset: 0x0001FE97
		public static string ToAbsolute(string virtualPath, string applicationPath)
		{
			return VirtualPathUtility.ToAbsolute(virtualPath, applicationPath, true);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00021CA4 File Offset: 0x0001FEA4
		internal static string ToAbsolute(string virtualPath, string applicationPath, bool normalize)
		{
			if (StrUtils.IsNullOrEmpty(applicationPath))
			{
				throw new ArgumentNullException("applicationPath");
			}
			if (StrUtils.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentNullException("virtualPath");
			}
			if (VirtualPathUtility.IsAppRelative(virtualPath))
			{
				if (applicationPath[0] != '/')
				{
					throw new ArgumentException("appPath is not rooted", "applicationPath");
				}
				string text = applicationPath + ((virtualPath.Length == 1) ? "/" : virtualPath.Substring(1));
				if (normalize)
				{
					return VirtualPathUtility.Normalize(text);
				}
				return text;
			}
			else
			{
				if (virtualPath[0] != '/')
				{
					throw new ArgumentException(string.Format("Relative path not allowed: '{0}'", virtualPath));
				}
				if (normalize)
				{
					return VirtualPathUtility.Normalize(virtualPath);
				}
				return virtualPath;
			}
		}

		/// <summary>Converts a virtual path to an application-relative path using the application virtual path that is in the <see cref="P:System.Web.HttpRuntime.AppDomainAppVirtualPath" /> property. </summary>
		/// <returns>The application-relative path representation of <paramref name="virtualPath" />.</returns>
		/// <param name="virtualPath">The virtual path to convert to an application-relative path. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="virtualPath" /> is null. </exception>
		// Token: 0x06000C93 RID: 3219 RVA: 0x00021D4C File Offset: 0x0001FF4C
		public static string ToAppRelative(string virtualPath)
		{
			string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
			if (appDomainAppVirtualPath == null)
			{
				throw new HttpException("The path to the application is not known");
			}
			return VirtualPathUtility.ToAppRelative(virtualPath, appDomainAppVirtualPath);
		}

		/// <summary>Converts a virtual path to an application-relative path using a specified application path.</summary>
		/// <returns>The application-relative path representation of <paramref name="virtualPath" />.</returns>
		/// <param name="virtualPath">The virtual path to convert to an application-relative path. </param>
		/// <param name="applicationPath">The application path to use to convert <paramref name="virtualPath" /> to a relative path. </param>
		// Token: 0x06000C94 RID: 3220 RVA: 0x00021D74 File Offset: 0x0001FF74
		public static string ToAppRelative(string virtualPath, string applicationPath)
		{
			virtualPath = VirtualPathUtility.Normalize(virtualPath);
			if (VirtualPathUtility.IsAppRelative(virtualPath))
			{
				return virtualPath;
			}
			if (!VirtualPathUtility.IsAbsolute(applicationPath))
			{
				throw new ArgumentException("appPath is not absolute", "applicationPath");
			}
			applicationPath = VirtualPathUtility.Normalize(applicationPath);
			if (applicationPath.Length == 1)
			{
				return "~" + virtualPath;
			}
			int length = applicationPath.Length;
			if (string.CompareOrdinal(virtualPath, applicationPath) == 0)
			{
				return "~/";
			}
			if (string.CompareOrdinal(virtualPath, 0, applicationPath, 0, length) == 0)
			{
				return "~" + virtualPath.Substring(length);
			}
			return virtualPath;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00021E00 File Offset: 0x00020000
		internal static string Normalize(string path)
		{
			if (!VirtualPathUtility.IsRooted(path))
			{
				throw new ArgumentException(string.Format("The relative virtual path '{0}' is not allowed here.", path));
			}
			if (path.Length == 1)
			{
				return path;
			}
			path = VirtualPathUtility.Canonize(path);
			int num = path.IndexOf('.');
			while (num >= 0 && ++num != path.Length)
			{
				char c = path[num];
				if (c == '/' || c == '.')
				{
					break;
				}
				num = path.IndexOf('.', num);
			}
			if (num < 0)
			{
				return path;
			}
			bool flag = false;
			bool flag2 = false;
			string[] array = null;
			if (path[0] == '~')
			{
				if (path.Length == 2)
				{
					return "~/";
				}
				flag = true;
				path = path.Substring(1);
			}
			else if (path.Length == 1)
			{
				return "/";
			}
			if (path[path.Length - 1] == '/')
			{
				flag2 = true;
			}
			string[] array2 = StrUtils.SplitRemoveEmptyEntries(path, VirtualPathUtility.path_sep);
			int num2 = array2.Length;
			int num3 = 0;
			for (int i = 0; i < num2; i++)
			{
				string text = array2[i];
				if (!(text == "."))
				{
					if (text == "..")
					{
						num3--;
						if (num3 < 0)
						{
							if (flag)
							{
								if (array == null)
								{
									array = StrUtils.SplitRemoveEmptyEntries(HttpRuntime.AppDomainAppVirtualPath, VirtualPathUtility.path_sep);
								}
								if (array.Length + num3 >= 0)
								{
									goto IL_014E;
								}
							}
							throw new HttpException("Cannot use a leading .. to exit above the top directory.");
						}
					}
					else
					{
						if (num3 >= 0)
						{
							array2[num3] = text;
						}
						else
						{
							array[array.Length + num3] = text;
						}
						num3++;
					}
				}
				IL_014E:;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (array != null)
			{
				int num4 = array.Length;
				if (num3 < 0)
				{
					num4 += num3;
				}
				for (int j = 0; j < num4; j++)
				{
					stringBuilder.Append('/');
					stringBuilder.Append(array[j]);
				}
			}
			else if (flag)
			{
				stringBuilder.Append('~');
			}
			for (int k = 0; k < num3; k++)
			{
				stringBuilder.Append('/');
				stringBuilder.Append(array2[k]);
			}
			if (stringBuilder.Length > 0)
			{
				if (flag2)
				{
					stringBuilder.Append('/');
				}
				return stringBuilder.ToString();
			}
			return "/";
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0002200C File Offset: 0x0002020C
		internal static string Canonize(string path)
		{
			int num = -1;
			for (int i = 0; i < path.Length; i++)
			{
				if (path[i] == '\\' || (path[i] == '/' && i + 1 < path.Length && (path[i + 1] == '/' || path[i + 1] == '\\')))
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				return path;
			}
			StringBuilder stringBuilder = new StringBuilder(path.Length);
			stringBuilder.Append(path, 0, num);
			for (int j = num; j < path.Length; j++)
			{
				if (path[j] == '\\' || path[j] == '/')
				{
					int num2 = j + 1;
					if (num2 >= path.Length || (path[num2] != '\\' && path[num2] != '/'))
					{
						stringBuilder.Append('/');
					}
				}
				else
				{
					stringBuilder.Append(path[j]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x000220F4 File Offset: 0x000202F4
		internal static bool IsValidVirtualPath(string path)
		{
			if (path == null)
			{
				return false;
			}
			bool flag = true;
			if (VirtualPathUtility.runningOnWindows)
			{
				try
				{
					object value = Registry.GetValue(VirtualPathUtility.aspNetVerificationKey, "VerificationCompatibility", null);
					if (value != null && value is int)
					{
						flag = (int)value != 1;
					}
				}
				catch
				{
				}
			}
			if (flag)
			{
				flag = VirtualPathUtility.monoSettingsVerifyCompatibility;
			}
			return !flag || path.IndexOfAny(VirtualPathUtility.invalidVirtualPathChars) == -1;
		}

		// Token: 0x04001113 RID: 4371
		private static bool monoSettingsVerifyCompatibility;

		// Token: 0x04001114 RID: 4372
		private static bool runningOnWindows;

		// Token: 0x04001115 RID: 4373
		private static char[] path_sep = new char[] { '/' };

		// Token: 0x04001116 RID: 4374
		private static readonly char[] invalidVirtualPathChars = new char[] { ':', '*' };

		// Token: 0x04001117 RID: 4375
		private static readonly string aspNetVerificationKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ASP.NET";
	}
}
