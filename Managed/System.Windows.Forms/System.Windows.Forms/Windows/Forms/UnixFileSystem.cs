using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace System.Windows.Forms
{
	// Token: 0x02000178 RID: 376
	internal class UnixFileSystem : FileSystem
	{
		// Token: 0x060018DA RID: 6362 RVA: 0x0005DDD0 File Offset: 0x0005BFD0
		public UnixFileSystem()
		{
			this.personal_folder = ThemeEngine.Current.Places(UIIcon.PlacesPersonal);
			this.recently_used_path = Path.Combine(this.personal_folder, ".recently-used");
			this.full_kde_recent_document_dir = this.personal_folder + "/.kde/share/apps/RecentDocuments";
			this.desktopFSEntry = new FSEntry();
			this.desktopFSEntry.Attributes = 16;
			this.desktopFSEntry.FullName = MWFVFS.DesktopPrefix;
			this.desktopFSEntry.Name = "Desktop";
			this.desktopFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesDesktop);
			this.desktopFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.desktopFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("desktop/desktop");
			this.desktopFSEntry.LastAccessTime = DateTime.Now;
			this.recentlyusedFSEntry = new FSEntry();
			this.recentlyusedFSEntry.Attributes = 16;
			this.recentlyusedFSEntry.FullName = MWFVFS.RecentlyUsedPrefix;
			this.recentlyusedFSEntry.Name = "Recently Used";
			this.recentlyusedFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.recentlyusedFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("recently/recently");
			this.recentlyusedFSEntry.LastAccessTime = DateTime.Now;
			this.personalFSEntry = new FSEntry();
			this.personalFSEntry.Attributes = 16;
			this.personalFSEntry.FullName = MWFVFS.PersonalPrefix;
			this.personalFSEntry.Name = "Personal";
			this.personalFSEntry.MainTopNode = this.GetDesktopFSEntry();
			this.personalFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesPersonal);
			this.personalFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.personalFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("directory/home");
			this.personalFSEntry.LastAccessTime = DateTime.Now;
			this.mycomputerpersonalFSEntry = new FSEntry();
			this.mycomputerpersonalFSEntry.Attributes = 16;
			this.mycomputerpersonalFSEntry.FullName = MWFVFS.MyComputerPersonalPrefix;
			this.mycomputerpersonalFSEntry.Name = "Personal";
			this.mycomputerpersonalFSEntry.MainTopNode = this.GetMyComputerFSEntry();
			this.mycomputerpersonalFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesPersonal);
			this.mycomputerpersonalFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.mycomputerpersonalFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("directory/home");
			this.mycomputerpersonalFSEntry.LastAccessTime = DateTime.Now;
			this.mycomputerFSEntry = new FSEntry();
			this.mycomputerFSEntry.Attributes = 16;
			this.mycomputerFSEntry.FullName = MWFVFS.MyComputerPrefix;
			this.mycomputerFSEntry.Name = "My Computer";
			this.mycomputerFSEntry.MainTopNode = this.GetDesktopFSEntry();
			this.mycomputerFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.mycomputerFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("workplace/workplace");
			this.mycomputerFSEntry.LastAccessTime = DateTime.Now;
			this.mynetworkFSEntry = new FSEntry();
			this.mynetworkFSEntry.Attributes = 16;
			this.mynetworkFSEntry.FullName = MWFVFS.MyNetworkPrefix;
			this.mynetworkFSEntry.Name = "My Network";
			this.mynetworkFSEntry.MainTopNode = this.GetDesktopFSEntry();
			this.mynetworkFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.mynetworkFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("network/network");
			this.mynetworkFSEntry.LastAccessTime = DateTime.Now;
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0005E128 File Offset: 0x0005C328
		public override void WriteRecentlyUsedFiles(string fileToAdd)
		{
			if (File.Exists(this.recently_used_path) && new FileInfo(this.recently_used_path).Length > 0L)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(this.recently_used_path);
				XmlNode xmlNode = xmlDocument.SelectSingleNode("RecentFiles");
				if (xmlNode != null)
				{
					XmlElement xmlElement = xmlDocument.CreateElement("RecentItem");
					XmlElement xmlElement2 = xmlDocument.CreateElement("URI");
					UriBuilder uriBuilder = new UriBuilder();
					uriBuilder.Path = fileToAdd;
					uriBuilder.Host = null;
					uriBuilder.Scheme = "file";
					XmlText xmlText = xmlDocument.CreateTextNode(uriBuilder.ToString());
					xmlElement2.AppendChild(xmlText);
					xmlElement.AppendChild(xmlElement2);
					xmlElement2 = xmlDocument.CreateElement("Mime-Type");
					xmlText = xmlDocument.CreateTextNode(Mime.GetMimeTypeForFile(fileToAdd));
					xmlElement2.AppendChild(xmlText);
					xmlElement.AppendChild(xmlElement2);
					xmlElement2 = xmlDocument.CreateElement("Timestamp");
					xmlText = xmlDocument.CreateTextNode(((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString());
					xmlElement2.AppendChild(xmlText);
					xmlElement.AppendChild(xmlElement2);
					xmlElement2 = xmlDocument.CreateElement("Groups");
					xmlElement.AppendChild(xmlElement2);
					foreach (object obj in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						XmlNode xmlNode3 = xmlNode2.SelectSingleNode("URI");
						if (xmlNode3 != null)
						{
							XmlNode firstChild = xmlNode3.FirstChild;
							if (firstChild is XmlText && uriBuilder.ToString() == ((XmlText)firstChild).Data)
							{
								xmlNode.RemoveChild(xmlNode2);
								break;
							}
						}
					}
					xmlNode.PrependChild(xmlElement);
					if (xmlNode.ChildNodes.Count > 10)
					{
						while (xmlNode.ChildNodes.Count > 10)
						{
							xmlNode.RemoveChild(xmlNode.LastChild);
						}
					}
					try
					{
						xmlDocument.Save(this.recently_used_path);
					}
					catch (Exception)
					{
					}
				}
			}
			else
			{
				XmlDocument xmlDocument2 = new XmlDocument();
				xmlDocument2.AppendChild(xmlDocument2.CreateXmlDeclaration("1.0", string.Empty, string.Empty));
				XmlElement xmlElement3 = xmlDocument2.CreateElement("RecentFiles");
				XmlElement xmlElement4 = xmlDocument2.CreateElement("RecentItem");
				XmlElement xmlElement5 = xmlDocument2.CreateElement("URI");
				XmlText xmlText2 = xmlDocument2.CreateTextNode(new UriBuilder
				{
					Path = fileToAdd,
					Host = null,
					Scheme = "file"
				}.ToString());
				xmlElement5.AppendChild(xmlText2);
				xmlElement4.AppendChild(xmlElement5);
				xmlElement5 = xmlDocument2.CreateElement("Mime-Type");
				xmlText2 = xmlDocument2.CreateTextNode(Mime.GetMimeTypeForFile(fileToAdd));
				xmlElement5.AppendChild(xmlText2);
				xmlElement4.AppendChild(xmlElement5);
				xmlElement5 = xmlDocument2.CreateElement("Timestamp");
				xmlText2 = xmlDocument2.CreateTextNode(((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString());
				xmlElement5.AppendChild(xmlText2);
				xmlElement4.AppendChild(xmlElement5);
				xmlElement5 = xmlDocument2.CreateElement("Groups");
				xmlElement4.AppendChild(xmlElement5);
				xmlElement3.AppendChild(xmlElement4);
				xmlDocument2.AppendChild(xmlElement3);
				try
				{
					xmlDocument2.Save(this.recently_used_path);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0005E50C File Offset: 0x0005C70C
		public override ArrayList GetRecentlyUsedFiles()
		{
			ArrayList arrayList = new ArrayList();
			if (File.Exists(this.recently_used_path))
			{
				try
				{
					XmlTextReader xmlTextReader = new XmlTextReader(this.recently_used_path);
					while (xmlTextReader.Read())
					{
						if (xmlTextReader.NodeType == 1 && xmlTextReader.Name.ToUpper() == "URI")
						{
							xmlTextReader.Read();
							Uri uri = new Uri(xmlTextReader.Value);
							if (!arrayList.Contains(uri.LocalPath) && File.Exists(uri.LocalPath))
							{
								FSEntry fileFSEntry = this.GetFileFSEntry(new FileInfo(uri.LocalPath));
								if (fileFSEntry != null)
								{
									arrayList.Add(fileFSEntry);
								}
							}
						}
					}
					xmlTextReader.Close();
				}
				catch (Exception)
				{
				}
			}
			if (Directory.Exists(this.full_kde_recent_document_dir))
			{
				string[] files = Directory.GetFiles(this.full_kde_recent_document_dir, "*.desktop");
				foreach (string text in files)
				{
					StreamReader streamReader = new StreamReader(text);
					for (string text2 = streamReader.ReadLine(); text2 != null; text2 = streamReader.ReadLine())
					{
						text2 = text2.Trim();
						if (text2.StartsWith("URL="))
						{
							text2 = text2.Replace("URL=", string.Empty);
							text2 = text2.Replace("$HOME", this.personal_folder);
							Uri uri2 = new Uri(text2);
							if (!arrayList.Contains(uri2.LocalPath) && File.Exists(uri2.LocalPath))
							{
								FSEntry fileFSEntry2 = this.GetFileFSEntry(new FileInfo(uri2.LocalPath));
								if (fileFSEntry2 != null)
								{
									arrayList.Add(fileFSEntry2);
								}
							}
							break;
						}
					}
					streamReader.Close();
				}
			}
			return arrayList;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0005E700 File Offset: 0x0005C900
		public override ArrayList GetMyComputerContent()
		{
			ArrayList arrayList = new ArrayList();
			if (this.masterMount.ProcMountAvailable)
			{
				this.masterMount.GetMounts();
				foreach (object obj in this.masterMount.Block_devices)
				{
					MasterMount.Mount mount = (MasterMount.Mount)obj;
					FSEntry fsentry = new FSEntry();
					fsentry.FileType = FSEntry.FSEntryType.Device;
					fsentry.FullName = mount.mount_point;
					fsentry.Name = string.Concat(new object[] { "HDD (", mount.fsType, ", ", mount.device_short, ")" });
					fsentry.FsType = mount.fsType;
					fsentry.DeviceShort = mount.device_short;
					fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("harddisk/harddisk");
					fsentry.Attributes = 16;
					fsentry.MainTopNode = this.GetMyComputerFSEntry();
					arrayList.Add(fsentry);
					if (!MWFVFS.MyComputerDevicesPrefix.Contains(fsentry.FullName + "://"))
					{
						MWFVFS.MyComputerDevicesPrefix.Add(fsentry.FullName + "://", fsentry);
					}
				}
				foreach (object obj2 in this.masterMount.Removable_devices)
				{
					MasterMount.Mount mount2 = (MasterMount.Mount)obj2;
					FSEntry fsentry2 = new FSEntry();
					fsentry2.FileType = FSEntry.FSEntryType.RemovableDevice;
					fsentry2.FullName = mount2.mount_point;
					bool flag = mount2.fsType != MasterMount.FsTypes.usbfs;
					string text = ((!flag) ? "USB" : "DVD/CD-Rom");
					string text2 = ((!flag) ? "removable/removable" : "cdrom/cdrom");
					fsentry2.Name = text + " (" + mount2.device_short + ")";
					fsentry2.IconIndex = MimeIconEngine.GetIconIndexForMimeType(text2);
					fsentry2.FsType = mount2.fsType;
					fsentry2.DeviceShort = mount2.device_short;
					fsentry2.Attributes = 16;
					fsentry2.MainTopNode = this.GetMyComputerFSEntry();
					arrayList.Add(fsentry2);
					string text3 = fsentry2.FullName + "://";
					if (!MWFVFS.MyComputerDevicesPrefix.Contains(text3))
					{
						MWFVFS.MyComputerDevicesPrefix.Add(text3, fsentry2);
					}
				}
			}
			arrayList.Add(this.GetMyComputerPersonalFSEntry());
			return arrayList;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0005E9E0 File Offset: 0x0005CBE0
		public override ArrayList GetMyNetworkContent()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.masterMount.Network_devices)
			{
				MasterMount.Mount mount = (MasterMount.Mount)obj;
				FSEntry fsentry = new FSEntry();
				fsentry.FileType = FSEntry.FSEntryType.Network;
				fsentry.FullName = mount.mount_point;
				fsentry.FsType = mount.fsType;
				fsentry.DeviceShort = mount.device_short;
				fsentry.Name = string.Concat(new object[] { "Network (", mount.fsType, ", ", mount.device_short, ")" });
				switch (mount.fsType)
				{
				case MasterMount.FsTypes.ncpfs:
					fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("network/network");
					break;
				case MasterMount.FsTypes.nfs:
					fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("nfs/nfs");
					break;
				case MasterMount.FsTypes.smbfs:
					fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("smb/smb");
					break;
				case MasterMount.FsTypes.cifs:
					fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("network/network");
					break;
				}
				IL_0123:
				fsentry.Attributes = 16;
				fsentry.MainTopNode = this.GetMyNetworkFSEntry();
				arrayList.Add(fsentry);
				continue;
				goto IL_0123;
			}
			return arrayList;
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0005EB70 File Offset: 0x0005CD70
		protected override FSEntry GetDesktopFSEntry()
		{
			return this.desktopFSEntry;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0005EB78 File Offset: 0x0005CD78
		protected override FSEntry GetRecentlyUsedFSEntry()
		{
			return this.recentlyusedFSEntry;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0005EB80 File Offset: 0x0005CD80
		protected override FSEntry GetPersonalFSEntry()
		{
			return this.personalFSEntry;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0005EB88 File Offset: 0x0005CD88
		protected override FSEntry GetMyComputerPersonalFSEntry()
		{
			return this.mycomputerpersonalFSEntry;
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0005EB90 File Offset: 0x0005CD90
		protected override FSEntry GetMyComputerFSEntry()
		{
			return this.mycomputerFSEntry;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0005EB98 File Offset: 0x0005CD98
		protected override FSEntry GetMyNetworkFSEntry()
		{
			return this.mynetworkFSEntry;
		}

		// Token: 0x04000DE3 RID: 3555
		private MasterMount masterMount = new MasterMount();

		// Token: 0x04000DE4 RID: 3556
		private FSEntry desktopFSEntry;

		// Token: 0x04000DE5 RID: 3557
		private FSEntry recentlyusedFSEntry;

		// Token: 0x04000DE6 RID: 3558
		private FSEntry personalFSEntry;

		// Token: 0x04000DE7 RID: 3559
		private FSEntry mycomputerpersonalFSEntry;

		// Token: 0x04000DE8 RID: 3560
		private FSEntry mycomputerFSEntry;

		// Token: 0x04000DE9 RID: 3561
		private FSEntry mynetworkFSEntry;

		// Token: 0x04000DEA RID: 3562
		private string personal_folder;

		// Token: 0x04000DEB RID: 3563
		private string recently_used_path;

		// Token: 0x04000DEC RID: 3564
		private string full_kde_recent_document_dir;
	}
}
