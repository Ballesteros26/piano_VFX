using System;
using System.Collections;

namespace Mono.WebBrowser
{
	// Token: 0x02000003 RID: 3
	public class Exception : Exception
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		static Exception()
		{
			Exception.messages.Insert(0, string.Intern("A critical error occurred."));
			Exception.messages.Insert(1, string.Intern("An error occurred while initializing gluezilla. Please make sure you have libgluezilla installed."));
			Exception.messages.Insert(2, string.Intern("Browser engine not supported at this time: "));
			Exception.messages.Insert(3, string.Intern("Error obtaining a handle to the service manager."));
			Exception.messages.Insert(4, string.Intern("Error obtaining a handle to the io service."));
			Exception.messages.Insert(5, string.Intern("Error obtaining a handle to the directory service."));
			Exception.messages.Insert(6, string.Intern("Error obtaining a handle to the preferences service."));
			Exception.messages.Insert(7, string.Intern("Stream is not open for writing. Call OpenStream before appending."));
			Exception.messages.Insert(8, string.Intern("An error occurred while initializing the navigation object."));
			Exception.messages.Insert(9, string.Intern("Error obtaining a handle to the accessibility service."));
			Exception.messages.Insert(10, string.Intern("Error obtaining a handle to the document encoder service."));
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002150 File Offset: 0x00000350
		internal Exception.ErrorCodes ErrorCode
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002158 File Offset: 0x00000358
		internal Exception(Exception.ErrorCodes code)
			: base(Exception.GetMessage(code, string.Empty))
		{
			this.code = code;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002172 File Offset: 0x00000372
		internal Exception(Exception.ErrorCodes code, string message)
			: base(Exception.GetMessage(code, message))
		{
			this.code = code;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002188 File Offset: 0x00000388
		internal Exception(Exception.ErrorCodes code, Exception innerException)
			: base(Exception.GetMessage(code, string.Empty), innerException)
		{
			this.code = code;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021A3 File Offset: 0x000003A3
		internal Exception(Exception.ErrorCodes code, string message, Exception innerException)
			: base(Exception.GetMessage(code, message), innerException)
		{
			this.code = code;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021BA File Offset: 0x000003BA
		private static string GetMessage(Exception.ErrorCodes code, string message)
		{
			return (Exception.messages[(int)code] as string) + " " + message;
		}

		// Token: 0x0400002A RID: 42
		private Exception.ErrorCodes code;

		// Token: 0x0400002B RID: 43
		private static ArrayList messages = new ArrayList();

		// Token: 0x02000148 RID: 328
		internal enum ErrorCodes
		{
			// Token: 0x0400015A RID: 346
			Other,
			// Token: 0x0400015B RID: 347
			GluezillaInit,
			// Token: 0x0400015C RID: 348
			EngineNotSupported,
			// Token: 0x0400015D RID: 349
			ServiceManager,
			// Token: 0x0400015E RID: 350
			IOService,
			// Token: 0x0400015F RID: 351
			DirectoryService,
			// Token: 0x04000160 RID: 352
			PrefService,
			// Token: 0x04000161 RID: 353
			StreamNotOpen,
			// Token: 0x04000162 RID: 354
			Navigation,
			// Token: 0x04000163 RID: 355
			AccessibilityService,
			// Token: 0x04000164 RID: 356
			DocumentEncoderService
		}
	}
}
