using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000267 RID: 615
	internal class GnomeHandler : PlatformMimeIconHandler
	{
		// Token: 0x060027DD RID: 10205 RVA: 0x000991F4 File Offset: 0x000973F4
		public override MimeExtensionHandlerStatus Start()
		{
			this.CreateUIIcons();
			return MimeExtensionHandlerStatus.OK;
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x00099200 File Offset: 0x00097400
		private void CreateUIIcons()
		{
			this.AddGnomeIcon("unknown/unknown", "gnome-fs-regular");
			this.AddGnomeIcon("inode/directory", "gnome-fs-directory");
			this.AddGnomeIcon("directory/home", "gnome-fs-home");
			this.AddGnomeIcon("desktop/desktop", "gnome-fs-desktop");
			this.AddGnomeIcon("recently/recently", "gnome-fs-directory-accept");
			this.AddGnomeIcon("workplace/workplace", "gnome-fs-client");
			this.AddGnomeIcon("network/network", "gnome-fs-network");
			this.AddGnomeIcon("nfs/nfs", "gnome-fs-nfs");
			this.AddGnomeIcon("smb/smb", "gnome-fs-smb");
			this.AddGnomeIcon("harddisk/harddisk", "gnome-dev-harddisk");
			this.AddGnomeIcon("cdrom/cdrom", "gnome-dev-cdrom");
			this.AddGnomeIcon("removable/removable", "gnome-dev-removable");
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000992D0 File Offset: 0x000974D0
		private void AddGnomeIcon(string internal_mime_type, string name)
		{
			if (MimeIconEngine.MimeIconIndex.ContainsKey(internal_mime_type))
			{
				return;
			}
			Image image = GnomeUtil.GetIcon(name, 48);
			if (image == null)
			{
				if (internal_mime_type == "unknown/unknown")
				{
					image = ResourceImageLoader.Get("text-x-generic.png");
				}
				else if (internal_mime_type == "inode/directory")
				{
					image = ResourceImageLoader.Get("folder.png");
				}
				else if (internal_mime_type == "directory/home")
				{
					image = ResourceImageLoader.Get("user-home.png");
				}
				else if (internal_mime_type == "desktop/desktop")
				{
					image = ResourceImageLoader.Get("user-desktop.png");
				}
				else if (internal_mime_type == "recently/recently")
				{
					image = ResourceImageLoader.Get("document-open.png");
				}
				else if (internal_mime_type == "workplace/workplace")
				{
					image = ResourceImageLoader.Get("computer.png");
				}
				else if (internal_mime_type == "network/network" || internal_mime_type == "nfs/nfs" || internal_mime_type == "smb/smb")
				{
					image = ResourceImageLoader.Get("folder-remote.png");
				}
				else if (internal_mime_type == "harddisk/harddisk" || internal_mime_type == "cdrom/cdrom" || internal_mime_type == "removable/removable")
				{
					image = ResourceImageLoader.Get("text-x-generic.png");
				}
			}
			if (image != null)
			{
				int num = MimeIconEngine.SmallIcons.Images.Add(image, Color.Transparent);
				MimeIconEngine.LargeIcons.Images.Add(image, Color.Transparent);
				MimeIconEngine.MimeIconIndex.Add(internal_mime_type, num);
			}
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x00099480 File Offset: 0x00097680
		public override object AddAndGetIconIndex(string filename, string mime_type)
		{
			int num = -1;
			Image icon = GnomeUtil.GetIcon(filename, mime_type, 48);
			if (icon != null)
			{
				num = MimeIconEngine.SmallIcons.Images.Add(icon, Color.Transparent);
				MimeIconEngine.LargeIcons.Images.Add(icon, Color.Transparent);
				MimeIconEngine.MimeIconIndex.Add(mime_type, num);
			}
			return num;
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000994E4 File Offset: 0x000976E4
		public override object AddAndGetIconIndex(string mime_type)
		{
			int num = -1;
			Image icon = GnomeUtil.GetIcon(mime_type, 48);
			if (icon != null)
			{
				num = MimeIconEngine.SmallIcons.Images.Add(icon, Color.Transparent);
				MimeIconEngine.LargeIcons.Images.Add(icon, Color.Transparent);
				MimeIconEngine.MimeIconIndex.Add(mime_type, num);
			}
			return num;
		}
	}
}
