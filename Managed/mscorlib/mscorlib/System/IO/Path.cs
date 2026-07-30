using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
	/// <summary>Performs operations on <see cref="T:System.String" /> instances that contain file or directory path information. These operations are performed in a cross-platform manner.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020003E0 RID: 992
	[ComVisible(true)]
	public static class Path
	{
		/// <summary>Changes the extension of a path string.</summary>
		/// <returns>The modified path information.On Windows-based desktop platforms, if <paramref name="path" /> is null or an empty string (""), the path information is returned unmodified. If <paramref name="extension" /> is null, the returned string contains the specified path with its extension removed. If <paramref name="path" /> has no extension, and <paramref name="extension" /> is not null, the returned path string contains <paramref name="extension" /> appended to the end of <paramref name="path" />.</returns>
		/// <param name="path">The path information to modify. The path cannot contain any of the characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />. </param>
		/// <param name="extension">The new extension (with or without a leading period). Specify null to remove an existing extension from <paramref name="path" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002EBC RID: 11964 RVA: 0x000A67B8 File Offset: 0x000A49B8
		public static string ChangeExtension(string path, string extension)
		{
			if (path == null)
			{
				return null;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = Path.findExtension(path);
			if (extension == null)
			{
				if (num >= 0)
				{
					return path.Substring(0, num);
				}
				return path;
			}
			else if (extension.Length == 0)
			{
				if (num >= 0)
				{
					return path.Substring(0, num + 1);
				}
				return path + ".";
			}
			else
			{
				if (path.Length != 0)
				{
					if (extension.Length > 0 && extension[0] != '.')
					{
						extension = "." + extension;
					}
				}
				else
				{
					extension = string.Empty;
				}
				if (num < 0)
				{
					return path + extension;
				}
				if (num > 0)
				{
					return path.Substring(0, num) + extension;
				}
				return extension;
			}
		}

		/// <summary>Combines two strings into a path.</summary>
		/// <returns>The combined paths. If one of the specified paths is a zero-length string, this method returns the other path. If <paramref name="path2" /> contains an absolute path, this method returns <paramref name="path2" />.</returns>
		/// <param name="path1">The first path to combine. </param>
		/// <param name="path2">The second path to combine. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path1" /> or <paramref name="path2" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path1" /> or <paramref name="path2" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002EBD RID: 11965 RVA: 0x000A6874 File Offset: 0x000A4A74
		public static string Combine(string path1, string path2)
		{
			if (path1 == null)
			{
				throw new ArgumentNullException("path1");
			}
			if (path2 == null)
			{
				throw new ArgumentNullException("path2");
			}
			if (path1.Length == 0)
			{
				return path2;
			}
			if (path2.Length == 0)
			{
				return path1;
			}
			if (path1.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			if (path2.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			if (Path.IsPathRooted(path2))
			{
				return path2;
			}
			char c = path1[path1.Length - 1];
			if (c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar && c != Path.VolumeSeparatorChar)
			{
				return path1 + Path.DirectorySeparatorStr + path2;
			}
			return path1 + path2;
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000A6928 File Offset: 0x000A4B28
		internal static string CleanPath(string s)
		{
			int length = s.Length;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			char c = s[0];
			if (length > 2 && c == '\\' && s[1] == '\\')
			{
				num3 = 2;
			}
			if (length == 1 && (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar))
			{
				return s;
			}
			for (int i = num3; i < length; i++)
			{
				char c2 = s[i];
				if (c2 == Path.DirectorySeparatorChar || c2 == Path.AltDirectorySeparatorChar)
				{
					if (Path.DirectorySeparatorChar != Path.AltDirectorySeparatorChar && c2 == Path.AltDirectorySeparatorChar)
					{
						num2++;
					}
					if (i + 1 == length)
					{
						num++;
					}
					else
					{
						c2 = s[i + 1];
						if (c2 == Path.DirectorySeparatorChar || c2 == Path.AltDirectorySeparatorChar)
						{
							num++;
						}
					}
				}
			}
			if (num == 0 && num2 == 0)
			{
				return s;
			}
			char[] array = new char[length - num];
			if (num3 != 0)
			{
				array[0] = '\\';
				array[1] = '\\';
			}
			int j = num3;
			int num4 = num3;
			while (j < length && num4 < array.Length)
			{
				char c3 = s[j];
				if (c3 != Path.DirectorySeparatorChar && c3 != Path.AltDirectorySeparatorChar)
				{
					array[num4++] = c3;
				}
				else if (num4 + 1 != array.Length)
				{
					array[num4++] = Path.DirectorySeparatorChar;
					while (j < length - 1)
					{
						c3 = s[j + 1];
						if (c3 != Path.DirectorySeparatorChar && c3 != Path.AltDirectorySeparatorChar)
						{
							break;
						}
						j++;
					}
				}
				j++;
			}
			return new string(array);
		}

		/// <summary>Returns the directory information for the specified path string.</summary>
		/// <returns>Directory information for <paramref name="path" />, or null if <paramref name="path" /> denotes a root directory or is null. Returns <see cref="F:System.String.Empty" /> if <paramref name="path" /> does not contain directory information.</returns>
		/// <param name="path">The path of a file or directory. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter contains invalid characters, is empty, or contains only white spaces. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.The <paramref name="path" /> parameter is longer than the system-defined maximum length.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002EBF RID: 11967 RVA: 0x000A6AA4 File Offset: 0x000A4CA4
		public static string GetDirectoryName(string path)
		{
			if (path == string.Empty)
			{
				throw new ArgumentException("Invalid path");
			}
			if (path == null || Path.GetPathRoot(path) == path)
			{
				return null;
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("Argument string consists of whitespace characters only.");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) > -1)
			{
				throw new ArgumentException("Path contains invalid characters");
			}
			int num = path.LastIndexOfAny(Path.PathSeparatorChars);
			if (num == 0)
			{
				num++;
			}
			if (num <= 0)
			{
				return string.Empty;
			}
			string text = path.Substring(0, num);
			int length = text.Length;
			if (length >= 2 && Path.DirectorySeparatorChar == '\\' && text[length - 1] == Path.VolumeSeparatorChar)
			{
				return text + Path.DirectorySeparatorChar.ToString();
			}
			if (length == 1 && Path.DirectorySeparatorChar == '\\' && path.Length >= 2 && path[num] == Path.VolumeSeparatorChar)
			{
				return text + Path.VolumeSeparatorChar.ToString();
			}
			return Path.CleanPath(text);
		}

		/// <summary>Returns the extension of the specified path string.</summary>
		/// <returns>The extension of the specified path (including the period "."), or null, or <see cref="F:System.String.Empty" />. If <paramref name="path" /> is null, <see cref="M:System.IO.Path.GetExtension(System.String)" /> returns null. If <paramref name="path" /> does not have extension information, <see cref="M:System.IO.Path.GetExtension(System.String)" /> returns <see cref="F:System.String.Empty" />.</returns>
		/// <param name="path">The path string from which to get the extension. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.  </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002EC0 RID: 11968 RVA: 0x000A6BAC File Offset: 0x000A4DAC
		public static string GetExtension(string path)
		{
			if (path == null)
			{
				return null;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = Path.findExtension(path);
			if (num > -1 && num < path.Length - 1)
			{
				return path.Substring(num);
			}
			return string.Empty;
		}

		/// <summary>Returns the file name and extension of the specified path string.</summary>
		/// <returns>The characters after the last directory character in <paramref name="path" />. If the last character of <paramref name="path" /> is a directory or volume separator character, this method returns <see cref="F:System.String.Empty" />. If <paramref name="path" /> is null, this method returns null.</returns>
		/// <param name="path">The path string from which to obtain the file name and extension. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002EC1 RID: 11969 RVA: 0x000A6BFC File Offset: 0x000A4DFC
		public static string GetFileName(string path)
		{
			if (path == null || path.Length == 0)
			{
				return path;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = path.LastIndexOfAny(Path.PathSeparatorChars);
			if (num >= 0)
			{
				return path.Substring(num + 1);
			}
			return path;
		}

		/// <summary>Returns the file name of the specified path string without the extension.</summary>
		/// <returns>The string returned by <see cref="M:System.IO.Path.GetFileName(System.String)" />, minus the last period (.) and all characters following it.</returns>
		/// <param name="path">The path of the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002EC2 RID: 11970 RVA: 0x000A6C4A File Offset: 0x000A4E4A
		public static string GetFileNameWithoutExtension(string path)
		{
			return Path.ChangeExtension(Path.GetFileName(path), null);
		}

		/// <summary>Returns the absolute path for the specified path string.</summary>
		/// <returns>The fully qualified location of <paramref name="path" />, such as "C:\MyFile.txt".</returns>
		/// <param name="path">The file or directory for which to obtain absolute path information. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.-or- The system could not retrieve the absolute path. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon (":") that is not part of a volume identifier (for example, "c:\"). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002EC3 RID: 11971 RVA: 0x000A6C58 File Offset: 0x000A4E58
		public static string GetFullPath(string path)
		{
			return Path.InsecureGetFullPath(path);
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000A6C58 File Offset: 0x000A4E58
		internal static string GetFullPathInternal(string path)
		{
			return Path.InsecureGetFullPath(path);
		}

		// Token: 0x06002EC5 RID: 11973
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int GetFullPathName(string path, int numBufferChars, StringBuilder buffer, ref IntPtr lpFilePartOrNull);

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000A6C60 File Offset: 0x000A4E60
		internal static string GetFullPathName(string path)
		{
			StringBuilder stringBuilder = new StringBuilder(260);
			IntPtr zero = IntPtr.Zero;
			int fullPathName = Path.GetFullPathName(path, 260, stringBuilder, ref zero);
			if (fullPathName == 0)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw new IOException("Windows API call to GetFullPathName failed, Windows error code: " + lastWin32Error);
			}
			if (fullPathName > 260)
			{
				stringBuilder = new StringBuilder(fullPathName);
				Path.GetFullPathName(path, fullPathName, stringBuilder, ref zero);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000A6CCC File Offset: 0x000A4ECC
		internal static string WindowsDriveAdjustment(string path)
		{
			if (path.Length < 2)
			{
				if (path.Length == 1 && (path[0] == '\\' || path[0] == '/'))
				{
					return Path.GetPathRoot(Directory.GetCurrentDirectory());
				}
				return path;
			}
			else
			{
				if (path[1] != ':' || !char.IsLetter(path[0]))
				{
					return path;
				}
				string text = Directory.InsecureGetCurrentDirectory();
				if (path.Length == 2)
				{
					if (text[0] == path[0])
					{
						path = text;
					}
					else
					{
						path = Path.GetFullPathName(path);
					}
				}
				else if (path[2] != Path.DirectorySeparatorChar && path[2] != Path.AltDirectorySeparatorChar)
				{
					if (text[0] == path[0])
					{
						path = Path.Combine(text, path.Substring(2, path.Length - 2));
					}
					else
					{
						path = Path.GetFullPathName(path);
					}
				}
				return path;
			}
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000A6DA8 File Offset: 0x000A4FA8
		internal static string InsecureGetFullPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException(Locale.GetText("The specified path is not of a legal form (empty)."));
			}
			if (Environment.IsRunningOnWindows)
			{
				path = Path.WindowsDriveAdjustment(path);
			}
			char c = path[path.Length - 1];
			bool flag = true;
			if (path.Length >= 2 && Path.IsDirectorySeparator(path[0]) && Path.IsDirectorySeparator(path[1]))
			{
				if (path.Length == 2 || path.IndexOf(path[0], 2) < 0)
				{
					throw new ArgumentException("UNC paths should be of the form \\\\server\\share.");
				}
				if (path[0] != Path.DirectorySeparatorChar)
				{
					path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
				}
			}
			else if (!Path.IsPathRooted(path))
			{
				if (!Environment.IsRunningOnWindows)
				{
					int num = 0;
					while ((num = path.IndexOf('.', num)) != -1 && ++num != path.Length && path[num] != Path.DirectorySeparatorChar && path[num] != Path.AltDirectorySeparatorChar)
					{
					}
					flag = num > 0;
				}
				string text = Directory.InsecureGetCurrentDirectory();
				if (text[text.Length - 1] == Path.DirectorySeparatorChar)
				{
					path = text + path;
				}
				else
				{
					path = text + Path.DirectorySeparatorChar.ToString() + path;
				}
			}
			else if (Path.DirectorySeparatorChar == '\\' && path.Length >= 2 && Path.IsDirectorySeparator(path[0]) && !Path.IsDirectorySeparator(path[1]))
			{
				string text2 = Directory.InsecureGetCurrentDirectory();
				if (text2[1] == Path.VolumeSeparatorChar)
				{
					path = text2.Substring(0, 2) + path;
				}
				else
				{
					path = text2.Substring(0, text2.IndexOf('\\', text2.IndexOfUnchecked("\\\\", 0, text2.Length) + 1));
				}
			}
			if (flag)
			{
				path = Path.CanonicalizePath(path);
			}
			if (Path.IsDirectorySeparator(c) && path[path.Length - 1] != Path.DirectorySeparatorChar)
			{
				path += Path.DirectorySeparatorChar.ToString();
			}
			string text3;
			if (MonoIO.RemapPath(path, out text3))
			{
				path = text3;
			}
			return path;
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000A6FDD File Offset: 0x000A51DD
		internal static bool IsDirectorySeparator(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		/// <summary>Gets the root directory information of the specified path.</summary>
		/// <returns>The root directory of <paramref name="path" />, such as "C:\", or null if <paramref name="path" /> is null, or an empty string if <paramref name="path" /> does not contain root directory information.</returns>
		/// <param name="path">The path from which to obtain root directory information. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.-or- <see cref="F:System.String.Empty" /> was passed to <paramref name="path" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002ECA RID: 11978 RVA: 0x000A6FF4 File Offset: 0x000A51F4
		public static string GetPathRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("The specified path is not of a legal form.");
			}
			if (!Path.IsPathRooted(path))
			{
				return string.Empty;
			}
			if (Path.DirectorySeparatorChar == '/')
			{
				if (!Path.IsDirectorySeparator(path[0]))
				{
					return string.Empty;
				}
				return Path.DirectorySeparatorStr;
			}
			else
			{
				int num = 2;
				if (path.Length == 1 && Path.IsDirectorySeparator(path[0]))
				{
					return Path.DirectorySeparatorStr;
				}
				if (path.Length < 2)
				{
					return string.Empty;
				}
				if (Path.IsDirectorySeparator(path[0]) && Path.IsDirectorySeparator(path[1]))
				{
					while (num < path.Length && !Path.IsDirectorySeparator(path[num]))
					{
						num++;
					}
					if (num < path.Length)
					{
						num++;
						while (num < path.Length && !Path.IsDirectorySeparator(path[num]))
						{
							num++;
						}
					}
					return Path.DirectorySeparatorStr + Path.DirectorySeparatorStr + path.Substring(2, num - 2).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
				}
				if (Path.IsDirectorySeparator(path[0]))
				{
					return Path.DirectorySeparatorStr;
				}
				if (path[1] == Path.VolumeSeparatorChar)
				{
					if (path.Length >= 3 && Path.IsDirectorySeparator(path[2]))
					{
						num++;
					}
					return path.Substring(0, num);
				}
				return Directory.GetCurrentDirectory().Substring(0, 2);
			}
		}

		/// <summary>Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.</summary>
		/// <returns>The full path of the temporary file.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as no unique temporary file name is available.- or -This method was unable to create a temporary file.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002ECB RID: 11979 RVA: 0x000A7160 File Offset: 0x000A5360
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		public static string GetTempFileName()
		{
			FileStream fileStream = null;
			int num = 0;
			Random random = new Random();
			string tempPath = Path.GetTempPath();
			string text;
			do
			{
				int num2 = random.Next();
				text = Path.Combine(tempPath, "tmp" + (num2 + 1).ToString("x", CultureInfo.InvariantCulture) + ".tmp");
				try
				{
					fileStream = new FileStream(text, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read, 8192, false, (FileOptions)1);
				}
				catch (IOException ex)
				{
					if (ex._HResult != -2147024816 || num++ > 65536)
					{
						throw;
					}
				}
				catch (UnauthorizedAccessException ex2)
				{
					if (num++ > 65536)
					{
						throw new IOException(ex2.Message, ex2);
					}
				}
			}
			while (fileStream == null);
			fileStream.Close();
			return text;
		}

		/// <summary>Returns the path of the current user's temporary folder.</summary>
		/// <returns>The path to the temporary folder, ending with a backslash.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002ECC RID: 11980 RVA: 0x000A7230 File Offset: 0x000A5430
		[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
		public static string GetTempPath()
		{
			string temp_path = Path.get_temp_path();
			if (temp_path.Length > 0 && temp_path[temp_path.Length - 1] != Path.DirectorySeparatorChar)
			{
				return temp_path + Path.DirectorySeparatorChar.ToString();
			}
			return temp_path;
		}

		// Token: 0x06002ECD RID: 11981
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_temp_path();

		/// <summary>Determines whether a path includes a file name extension.</summary>
		/// <returns>true if the characters that follow the last directory separator (\\ or /) or volume separator (:) in the path include a period (.) followed by one or more characters; otherwise, false.</returns>
		/// <param name="path">The path to search for an extension. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002ECE RID: 11982 RVA: 0x000A7278 File Offset: 0x000A5478
		public static bool HasExtension(string path)
		{
			if (path == null || path.Trim().Length == 0)
			{
				return false;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = Path.findExtension(path);
			return 0 <= num && num < path.Length - 1;
		}

		/// <summary>Gets a value indicating whether the specified path string contains a root.</summary>
		/// <returns>true if <paramref name="path" /> contains a root; otherwise, false.</returns>
		/// <param name="path">The path to test. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002ECF RID: 11983 RVA: 0x000A72C8 File Offset: 0x000A54C8
		public static bool IsPathRooted(string path)
		{
			if (path == null || path.Length == 0)
			{
				return false;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			char c = path[0];
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar || (!Path.dirEqualsVolume && path.Length > 1 && path[1] == Path.VolumeSeparatorChar);
		}

		/// <summary>Gets an array containing the characters that are not allowed in file names.</summary>
		/// <returns>An array containing the characters that are not allowed in file names.</returns>
		// Token: 0x06002ED0 RID: 11984 RVA: 0x000A7335 File Offset: 0x000A5535
		public static char[] GetInvalidFileNameChars()
		{
			if (Environment.IsRunningOnWindows)
			{
				return new char[]
				{
					'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
					'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
					'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d',
					'\u001e', '\u001f', '"', '<', '>', '|', ':', '*', '?', '\\',
					'/'
				};
			}
			return new char[] { '\0', '/' };
		}

		/// <summary>Gets an array containing the characters that are not allowed in path names.</summary>
		/// <returns>An array containing the characters that are not allowed in path names.</returns>
		// Token: 0x06002ED1 RID: 11985 RVA: 0x000A735C File Offset: 0x000A555C
		public static char[] GetInvalidPathChars()
		{
			if (Environment.IsRunningOnWindows)
			{
				return new char[]
				{
					'"', '<', '>', '|', '\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005',
					'\u0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\u000e', '\u000f',
					'\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019',
					'\u001a', '\u001b', '\u001c', '\u001d', '\u001e', '\u001f'
				};
			}
			return new char[1];
		}

		/// <summary>Returns a random folder name or file name.</summary>
		/// <returns>A random folder name or file name.</returns>
		// Token: 0x06002ED2 RID: 11986 RVA: 0x000A7380 File Offset: 0x000A5580
		public static string GetRandomFileName()
		{
			StringBuilder stringBuilder = new StringBuilder(12);
			RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
			byte[] array = new byte[11];
			randomNumberGenerator.GetBytes(array);
			for (int i = 0; i < array.Length; i++)
			{
				if (stringBuilder.Length == 8)
				{
					stringBuilder.Append('.');
				}
				int num = (int)(array[i] % 36);
				char c = (char)((num < 26) ? (num + 97) : (num - 26 + 48));
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000A73F4 File Offset: 0x000A55F4
		private static int findExtension(string path)
		{
			if (path != null)
			{
				int num = path.LastIndexOf('.');
				int num2 = path.LastIndexOfAny(Path.PathSeparatorChars);
				if (num > num2)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000A74C4 File Offset: 0x000A56C4
		private static string GetServerAndShare(string path)
		{
			int num = 2;
			while (num < path.Length && !Path.IsDirectorySeparator(path[num]))
			{
				num++;
			}
			if (num < path.Length)
			{
				num++;
				while (num < path.Length && !Path.IsDirectorySeparator(path[num]))
				{
					num++;
				}
			}
			return path.Substring(2, num - 2).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000A7534 File Offset: 0x000A5734
		private static bool SameRoot(string root, string path)
		{
			if (root.Length < 2 || path.Length < 2)
			{
				return false;
			}
			if (!Path.IsDirectorySeparator(root[0]) || !Path.IsDirectorySeparator(root[1]))
			{
				return root[0].Equals(path[0]) && path[1] == Path.VolumeSeparatorChar && (root.Length <= 2 || path.Length <= 2 || (Path.IsDirectorySeparator(root[2]) && Path.IsDirectorySeparator(path[2])));
			}
			if (!Path.IsDirectorySeparator(path[0]) || !Path.IsDirectorySeparator(path[1]))
			{
				return false;
			}
			string serverAndShare = Path.GetServerAndShare(root);
			string serverAndShare2 = Path.GetServerAndShare(path);
			return string.Compare(serverAndShare, serverAndShare2, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000A7608 File Offset: 0x000A5808
		private static string CanonicalizePath(string path)
		{
			if (path == null)
			{
				return path;
			}
			if (Environment.IsRunningOnWindows)
			{
				path = path.Trim();
			}
			if (path.Length == 0)
			{
				return path;
			}
			string pathRoot = Path.GetPathRoot(path);
			string[] array = path.Split(new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar
			});
			int num = 0;
			bool flag = Environment.IsRunningOnWindows && pathRoot.Length > 2 && Path.IsDirectorySeparator(pathRoot[0]) && Path.IsDirectorySeparator(pathRoot[1]);
			int num2 = (flag ? 3 : 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (Environment.IsRunningOnWindows)
				{
					array[i] = array[i].TrimEnd(Array.Empty<char>());
				}
				if (!(array[i] == ".") && (i == 0 || array[i].Length != 0))
				{
					if (array[i] == "..")
					{
						if (num > num2)
						{
							num--;
						}
					}
					else
					{
						array[num++] = array[i];
					}
				}
			}
			if (num == 0 || (num == 1 && array[0] == ""))
			{
				return pathRoot;
			}
			string text = string.Join(Path.DirectorySeparatorStr, array, 0, num);
			if (!Environment.IsRunningOnWindows)
			{
				if (pathRoot != "" && text.Length > 0 && text[0] != '/')
				{
					text = pathRoot + text;
				}
				return text;
			}
			if (flag)
			{
				text = Path.DirectorySeparatorStr + text;
			}
			if (!Path.SameRoot(pathRoot, text))
			{
				text = pathRoot + text;
			}
			if (flag)
			{
				return text;
			}
			if (!Path.IsDirectorySeparator(path[0]) && Path.SameRoot(pathRoot, path))
			{
				if (text.Length <= 2 && !text.EndsWith(Path.DirectorySeparatorStr))
				{
					text += Path.DirectorySeparatorChar.ToString();
				}
				return text;
			}
			string currentDirectory = Directory.GetCurrentDirectory();
			if (currentDirectory.Length > 1 && currentDirectory[1] == Path.VolumeSeparatorChar)
			{
				if (text.Length == 0 || Path.IsDirectorySeparator(text[0]))
				{
					text += "\\";
				}
				return currentDirectory.Substring(0, 2) + text;
			}
			if (Path.IsDirectorySeparator(currentDirectory[currentDirectory.Length - 1]) && Path.IsDirectorySeparator(text[0]))
			{
				return currentDirectory + text.Substring(1);
			}
			return currentDirectory + text;
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000A786C File Offset: 0x000A5A6C
		internal static bool IsPathSubsetOf(string subset, string path)
		{
			if (subset.Length > path.Length)
			{
				return false;
			}
			int num = subset.LastIndexOfAny(Path.PathSeparatorChars);
			if (string.Compare(subset, 0, path, 0, num) != 0)
			{
				return false;
			}
			num++;
			int num2 = path.IndexOfAny(Path.PathSeparatorChars, num);
			if (num2 >= num)
			{
				return string.Compare(subset, num, path, num, path.Length - num2) == 0;
			}
			return subset.Length == path.Length && string.Compare(subset, num, path, num, subset.Length - num) == 0;
		}

		/// <summary>Combines an array of strings into a path.</summary>
		/// <returns>The combined paths.</returns>
		/// <param name="paths">An array of parts of the path.</param>
		/// <exception cref="T:System.ArgumentException">One of the strings in the array contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">One of the strings in the array is null. </exception>
		// Token: 0x06002ED9 RID: 11993 RVA: 0x000A78F4 File Offset: 0x000A5AF4
		public static string Combine(params string[] paths)
		{
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = paths.Length;
			bool flag = false;
			foreach (string text in paths)
			{
				if (text == null)
				{
					throw new ArgumentNullException("One of the paths contains a null value", "paths");
				}
				if (text.Length != 0)
				{
					if (text.IndexOfAny(Path.InvalidPathChars) != -1)
					{
						throw new ArgumentException("Illegal characters in path.");
					}
					if (flag)
					{
						flag = false;
						stringBuilder.Append(Path.DirectorySeparatorStr);
					}
					num--;
					if (Path.IsPathRooted(text))
					{
						stringBuilder.Length = 0;
					}
					stringBuilder.Append(text);
					int length = text.Length;
					if (length > 0 && num > 0)
					{
						char c = text[length - 1];
						if (c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar && c != Path.VolumeSeparatorChar)
						{
							flag = true;
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>Combines three strings into a path.</summary>
		/// <returns>The combined paths.</returns>
		/// <param name="path1">The first path to combine. </param>
		/// <param name="path2">The second path to combine. </param>
		/// <param name="path3">The third path to combine.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path1" />, <paramref name="path2" />, or <paramref name="path3" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path1" />, <paramref name="path2" />, or <paramref name="path3" /> is null. </exception>
		// Token: 0x06002EDA RID: 11994 RVA: 0x000A79E8 File Offset: 0x000A5BE8
		public static string Combine(string path1, string path2, string path3)
		{
			if (path1 == null)
			{
				throw new ArgumentNullException("path1");
			}
			if (path2 == null)
			{
				throw new ArgumentNullException("path2");
			}
			if (path3 == null)
			{
				throw new ArgumentNullException("path3");
			}
			return Path.Combine(new string[] { path1, path2, path3 });
		}

		/// <summary>Combines four strings into a path.</summary>
		/// <returns>The combined paths.</returns>
		/// <param name="path1">The first path to combine. </param>
		/// <param name="path2">The second path to combine. </param>
		/// <param name="path3">The third path to combine.</param>
		/// <param name="path4">The fourth path to combine.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path1" />, <paramref name="path2" />, <paramref name="path3" />, or <paramref name="path4" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path1" />, <paramref name="path2" />, <paramref name="path3" />, or <paramref name="path4" /> is null. </exception>
		// Token: 0x06002EDB RID: 11995 RVA: 0x000A7A38 File Offset: 0x000A5C38
		public static string Combine(string path1, string path2, string path3, string path4)
		{
			if (path1 == null)
			{
				throw new ArgumentNullException("path1");
			}
			if (path2 == null)
			{
				throw new ArgumentNullException("path2");
			}
			if (path3 == null)
			{
				throw new ArgumentNullException("path3");
			}
			if (path4 == null)
			{
				throw new ArgumentNullException("path4");
			}
			return Path.Combine(new string[] { path1, path2, path3, path4 });
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000A7A98 File Offset: 0x000A5C98
		internal static void Validate(string path)
		{
			Path.Validate(path, "path");
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000A7AA8 File Offset: 0x000A5CA8
		internal static void Validate(string path, string parameterName)
		{
			if (path == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentException(Locale.GetText("Path is empty"));
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException(Locale.GetText("Path contains invalid chars"));
			}
			if (Environment.IsRunningOnWindows)
			{
				int num = path.IndexOf(':');
				if (num >= 0 && num != 1)
				{
					throw new ArgumentException(parameterName);
				}
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002EDE RID: 11998 RVA: 0x000A7B14 File Offset: 0x000A5D14
		internal static string DirectorySeparatorCharAsString
		{
			get
			{
				return Path.DirectorySeparatorStr;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x000A7B1B File Offset: 0x000A5D1B
		internal static char[] TrimEndChars
		{
			get
			{
				if (!Environment.IsRunningOnWindows)
				{
					return Path.trimEndCharsUnix;
				}
				return Path.trimEndCharsWindows;
			}
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000A7B30 File Offset: 0x000A5D30
		internal static void CheckSearchPattern(string searchPattern)
		{
			int num;
			while ((num = searchPattern.IndexOf("..", StringComparison.Ordinal)) != -1)
			{
				if (num + 2 == searchPattern.Length)
				{
					throw new ArgumentException(Environment.GetResourceString("Search pattern cannot contain \"..\" to move up directories and can be contained only internally in file/directory names, as in \"a..b\"."));
				}
				if (searchPattern[num + 2] == Path.DirectorySeparatorChar || searchPattern[num + 2] == Path.AltDirectorySeparatorChar)
				{
					throw new ArgumentException(Environment.GetResourceString("Search pattern cannot contain \"..\" to move up directories and can be contained only internally in file/directory names, as in \"a..b\"."));
				}
				searchPattern = searchPattern.Substring(num + 2);
			}
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000A7BA6 File Offset: 0x000A5DA6
		internal static void CheckInvalidPathChars(string path, bool checkAdditional = false)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (PathInternal.HasIllegalCharacters(path, checkAdditional))
			{
				throw new ArgumentException(Environment.GetResourceString("Illegal characters in path."));
			}
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000A7BD0 File Offset: 0x000A5DD0
		internal static string InternalCombine(string path1, string path2)
		{
			if (path1 == null || path2 == null)
			{
				throw new ArgumentNullException((path1 == null) ? "path1" : "path2");
			}
			Path.CheckInvalidPathChars(path1, false);
			Path.CheckInvalidPathChars(path2, false);
			if (path2.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Path cannot be the empty string or all whitespace."), "path2");
			}
			if (Path.IsPathRooted(path2))
			{
				throw new ArgumentException(Environment.GetResourceString("Second path fragment must not be a drive or UNC name."), "path2");
			}
			int length = path1.Length;
			if (length == 0)
			{
				return path2;
			}
			char c = path1[length - 1];
			if (c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar && c != Path.VolumeSeparatorChar)
			{
				return path1 + Path.DirectorySeparatorCharAsString + path2;
			}
			return path1 + path2;
		}

		/// <summary>Provides a platform-specific array of characters that cannot be specified in path string arguments passed to members of the <see cref="T:System.IO.Path" /> class.</summary>
		/// <returns>A character array of invalid path characters for the current platform.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400183D RID: 6205
		[Obsolete("see GetInvalidPathChars and GetInvalidFileNameChars methods.")]
		public static readonly char[] InvalidPathChars = Path.GetInvalidPathChars();

		/// <summary>Provides a platform-specific alternate character used to separate directory levels in a path string that reflects a hierarchical file system organization.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400183E RID: 6206
		public static readonly char AltDirectorySeparatorChar = MonoIO.AltDirectorySeparatorChar;

		/// <summary>Provides a platform-specific character used to separate directory levels in a path string that reflects a hierarchical file system organization.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400183F RID: 6207
		public static readonly char DirectorySeparatorChar = MonoIO.DirectorySeparatorChar;

		/// <summary>A platform-specific separator character used to separate path strings in environment variables.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001840 RID: 6208
		public static readonly char PathSeparator = MonoIO.PathSeparator;

		// Token: 0x04001841 RID: 6209
		internal static readonly string DirectorySeparatorStr = Path.DirectorySeparatorChar.ToString();

		/// <summary>Provides a platform-specific volume separator character.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001842 RID: 6210
		public static readonly char VolumeSeparatorChar = MonoIO.VolumeSeparatorChar;

		// Token: 0x04001843 RID: 6211
		internal static readonly char[] PathSeparatorChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar,
			Path.VolumeSeparatorChar
		};

		// Token: 0x04001844 RID: 6212
		private static readonly bool dirEqualsVolume = Path.DirectorySeparatorChar == Path.VolumeSeparatorChar;

		// Token: 0x04001845 RID: 6213
		internal const int MAX_PATH = 260;

		// Token: 0x04001846 RID: 6214
		internal static readonly char[] trimEndCharsWindows = new char[] { '\t', '\n', '\v', '\f', '\r', ' ', '\u0085', '\u00a0' };

		// Token: 0x04001847 RID: 6215
		internal static readonly char[] trimEndCharsUnix = new char[0];
	}
}
