using System;

namespace System.Windows.Forms
{
	// Token: 0x02000266 RID: 614
	internal class PlatformDefaultHandler : PlatformMimeIconHandler
	{
		// Token: 0x060027DB RID: 10203 RVA: 0x00099150 File Offset: 0x00097350
		public override MimeExtensionHandlerStatus Start()
		{
			MimeIconEngine.AddIconByImage("inode/directory", ResourceImageLoader.Get("folder.png"));
			MimeIconEngine.AddIconByImage("unknown/unknown", ResourceImageLoader.Get("text-x-generic.png"));
			MimeIconEngine.AddIconByImage("desktop/desktop", ResourceImageLoader.Get("user-desktop.png"));
			MimeIconEngine.AddIconByImage("directory/home", ResourceImageLoader.Get("user-home.png"));
			MimeIconEngine.AddIconByImage("network/network", ResourceImageLoader.Get("folder-remote.png"));
			MimeIconEngine.AddIconByImage("recently/recently", ResourceImageLoader.Get("document-open.png"));
			MimeIconEngine.AddIconByImage("workplace/workplace", ResourceImageLoader.Get("computer.png"));
			return MimeExtensionHandlerStatus.OK;
		}
	}
}
