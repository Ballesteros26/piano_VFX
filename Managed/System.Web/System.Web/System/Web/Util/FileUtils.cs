using System;
using System.Diagnostics;
using System.IO;

namespace System.Web.Util
{
	// Token: 0x0200013B RID: 315
	internal sealed class FileUtils
	{
		// Token: 0x06000E74 RID: 3700 RVA: 0x000278A1 File Offset: 0x00025AA1
		internal static object CreateTemporaryFile(string tempdir, FileUtils.CreateTempFile createFile)
		{
			return FileUtils.CreateTemporaryFile(tempdir, null, null, createFile);
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x000278AC File Offset: 0x00025AAC
		internal static object CreateTemporaryFile(string tempdir, string extension, FileUtils.CreateTempFile createFile)
		{
			return FileUtils.CreateTemporaryFile(tempdir, null, extension, createFile);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x000278B8 File Offset: 0x00025AB8
		internal static object CreateTemporaryFile(string tempdir, string prefix, string extension, FileUtils.CreateTempFile createFile)
		{
			if (tempdir == null || tempdir.Length == 0)
			{
				return null;
			}
			if (createFile == null)
			{
				return null;
			}
			object obj = null;
			do
			{
				Random random = FileUtils.rnd;
				int num;
				lock (random)
				{
					num = FileUtils.rnd.Next();
				}
				string text = Path.Combine(tempdir, string.Format("{0}{1}{2}", (prefix != null) ? (prefix + ".") : "", num.ToString("x", Helpers.InvariantCulture), (extension != null) ? ("." + extension) : ""));
				try
				{
					obj = createFile(text);
				}
				catch (IOException)
				{
				}
				catch
				{
					throw;
				}
			}
			while (obj == null);
			return obj;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DEVEL")]
		public static void WriteLineLog(string logFilePath, string format, params object[] parms)
		{
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00027990 File Offset: 0x00025B90
		[Conditional("DEVEL")]
		public static void WriteLog(string logFilePath, string format, params object[] parms)
		{
			using (TextWriter textWriter = new StreamWriter((logFilePath != null && logFilePath.Length > 0) ? logFilePath : Path.Combine(Path.GetTempPath(), "System.Web.log"), true))
			{
				if (parms != null && parms.Length != 0)
				{
					textWriter.Write(format, parms);
				}
				else
				{
					textWriter.Write(format);
				}
			}
		}

		// Token: 0x040011FC RID: 4604
		private static Random rnd = new Random();

		// Token: 0x0200013C RID: 316
		// (Invoke) Token: 0x06000E7C RID: 3708
		internal delegate object CreateTempFile(string path);
	}
}
