using System;
using System.IO;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000D9 RID: 217
	internal class StaticFileHandler : IHttpHandler
	{
		// Token: 0x06000BCB RID: 3019 RVA: 0x0001F51C File Offset: 0x0001D71C
		private static bool ValidFileName(string fileName)
		{
			return !RuntimeHelpers.RunningOnWindows || (fileName != null && fileName.Length != 0 && !StrUtils.EndsWith(fileName, " ") && !StrUtils.EndsWith(fileName, "."));
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0001F554 File Offset: 0x0001D754
		public void ProcessRequest(HttpContext context)
		{
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			if (HostingEnvironment.HaveCustomVPP)
			{
				VirtualFile virtualFile = null;
				VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
				string filePath = request.FilePath;
				if (virtualPathProvider.FileExists(filePath))
				{
					virtualFile = virtualPathProvider.GetFile(filePath);
				}
				if (virtualFile == null)
				{
					throw new HttpException(404, "Path '" + filePath + "' was not found.", filePath);
				}
				response.ContentType = MimeTypes.GetMimeType(filePath);
				response.TransmitFile(virtualFile, true);
				return;
			}
			else
			{
				string physicalPath = request.PhysicalPath;
				FileInfo fileInfo = new FileInfo(physicalPath);
				if (!fileInfo.Exists || !StaticFileHandler.ValidFileName(physicalPath))
				{
					throw new HttpException(404, "Path '" + request.FilePath + "' was not found.", request.FilePath);
				}
				if ((fileInfo.Attributes & FileAttributes.Directory) != (FileAttributes)0)
				{
					response.Redirect(request.Path + "/");
					return;
				}
				string text = request.Headers["If-Modified-Since"];
				try
				{
					if (text != null)
					{
						DateTime dateTime = DateTime.ParseExact(text, "r", null);
						if (fileInfo.LastWriteTime.ToUniversalTime() <= dateTime)
						{
							response.ContentType = MimeTypes.GetMimeType(physicalPath);
							response.StatusCode = 304;
							return;
						}
					}
				}
				catch
				{
				}
				try
				{
					response.AddHeader("Last-Modified", fileInfo.LastWriteTime.ToUniversalTime().ToString("r"));
					response.ContentType = MimeTypes.GetMimeType(physicalPath);
					response.TransmitFile(physicalPath, true);
				}
				catch (Exception)
				{
					throw new HttpException(403, "Forbidden.");
				}
				return;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00008B66 File Offset: 0x00006D66
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}
