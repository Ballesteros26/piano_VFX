using System;
using System.Collections;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000264 RID: 612
	internal class MimeIconEngine
	{
		// Token: 0x060027CF RID: 10191 RVA: 0x00098C88 File Offset: 0x00096E88
		static MimeIconEngine()
		{
			MimeIconEngine.SmallIcons.ColorDepth = ColorDepth.Depth32Bit;
			MimeIconEngine.SmallIcons.TransparentColor = Color.Transparent;
			MimeIconEngine.LargeIcons.ColorDepth = ColorDepth.Depth32Bit;
			MimeIconEngine.LargeIcons.TransparentColor = Color.Transparent;
			string text = Environment.GetEnvironmentVariable("DESKTOP_SESSION");
			if (text != null)
			{
				text = text.ToUpper();
				if (text == "DEFAULT")
				{
					string environmentVariable = Environment.GetEnvironmentVariable("GNOME_DESKTOP_SESSION_ID");
					if (environmentVariable != null)
					{
						text = "GNOME";
					}
				}
			}
			else
			{
				text = string.Empty;
			}
			if (Mime.MimeAvailable && text == "GNOME")
			{
				MimeIconEngine.SmallIcons.ImageSize = new Size(24, 24);
				MimeIconEngine.LargeIcons.ImageSize = new Size(48, 48);
				MimeIconEngine.platformMimeHandler = new GnomeHandler();
				if (MimeIconEngine.platformMimeHandler.Start() == MimeExtensionHandlerStatus.OK)
				{
					MimeIconEngine.platform = EPlatformHandler.GNOME;
				}
				else
				{
					MimeIconEngine.LargeIcons.Images.Clear();
					MimeIconEngine.SmallIcons.Images.Clear();
					MimeIconEngine.platformMimeHandler = new PlatformDefaultHandler();
					MimeIconEngine.platformMimeHandler.Start();
				}
			}
			else
			{
				MimeIconEngine.SmallIcons.ImageSize = new Size(16, 16);
				MimeIconEngine.LargeIcons.ImageSize = new Size(48, 48);
				MimeIconEngine.platformMimeHandler = new PlatformDefaultHandler();
				MimeIconEngine.platformMimeHandler.Start();
			}
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x00098E20 File Offset: 0x00097020
		public static int GetIconIndexForFile(string full_filename)
		{
			object obj = MimeIconEngine.lock_object;
			int num;
			lock (obj)
			{
				if (MimeIconEngine.platform == EPlatformHandler.Default)
				{
					num = (int)MimeIconEngine.MimeIconIndex["unknown/unknown"];
				}
				else
				{
					string mimeTypeForFile = Mime.GetMimeTypeForFile(full_filename);
					object obj2 = MimeIconEngine.GetIconIndex(mimeTypeForFile);
					if (obj2 == null)
					{
						int num2 = full_filename.IndexOf(':');
						if (num2 > 1)
						{
							obj2 = MimeIconEngine.MimeIconIndex["unknown/unknown"];
						}
						else
						{
							obj2 = MimeIconEngine.platformMimeHandler.AddAndGetIconIndex(full_filename, mimeTypeForFile);
							if (obj2 == null)
							{
								obj2 = MimeIconEngine.MimeIconIndex["unknown/unknown"];
							}
						}
					}
					num = (int)obj2;
				}
			}
			return num;
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x00098EF4 File Offset: 0x000970F4
		public static int GetIconIndexForMimeType(string mime_type)
		{
			object obj = MimeIconEngine.lock_object;
			int num;
			lock (obj)
			{
				if (MimeIconEngine.platform == EPlatformHandler.Default)
				{
					if (mime_type == "inode/directory")
					{
						num = (int)MimeIconEngine.MimeIconIndex["inode/directory"];
					}
					else
					{
						num = (int)MimeIconEngine.MimeIconIndex["unknown/unknown"];
					}
				}
				else
				{
					object obj2 = MimeIconEngine.GetIconIndex(mime_type);
					if (obj2 == null)
					{
						obj2 = MimeIconEngine.platformMimeHandler.AddAndGetIconIndex(mime_type);
						if (obj2 == null)
						{
							obj2 = MimeIconEngine.MimeIconIndex["unknown/unknown"];
						}
					}
					num = (int)obj2;
				}
			}
			return num;
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x00098FC0 File Offset: 0x000971C0
		public static Image GetIconForMimeTypeAndSize(string mime_type, Size size)
		{
			object obj = MimeIconEngine.lock_object;
			Image image;
			lock (obj)
			{
				object iconIndex = MimeIconEngine.GetIconIndex(mime_type);
				Bitmap bitmap = new Bitmap(MimeIconEngine.LargeIcons.Images[(int)iconIndex], size);
				image = bitmap;
			}
			return image;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x0009902C File Offset: 0x0009722C
		internal static void AddIconByImage(string mime_type, Image image)
		{
			int num = MimeIconEngine.SmallIcons.Images.Add(image, Color.Transparent);
			MimeIconEngine.LargeIcons.Images.Add(image, Color.Transparent);
			MimeIconEngine.MimeIconIndex.Add(mime_type, num);
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x00099078 File Offset: 0x00097278
		private static object GetIconIndex(string mime_type)
		{
			object obj = null;
			if (mime_type != null)
			{
				obj = MimeIconEngine.MimeIconIndex[mime_type];
				if (obj == null)
				{
					string mimeAlias = Mime.GetMimeAlias(mime_type);
					if (mimeAlias != null)
					{
						string[] array = mimeAlias.Split(new char[] { ',' });
						for (int i = 0; i < array.Length; i++)
						{
							obj = MimeIconEngine.MimeIconIndex[array[i]];
							if (obj != null)
							{
								return obj;
							}
						}
					}
					string text = Mime.SubClasses[mime_type];
					if (text != null)
					{
						obj = MimeIconEngine.MimeIconIndex[text];
						if (obj != null)
						{
							return obj;
						}
					}
					string text2 = mime_type.Substring(0, mime_type.IndexOf('/'));
					return MimeIconEngine.MimeIconIndex[text2];
				}
			}
			return obj;
		}

		// Token: 0x040013EC RID: 5100
		public static ImageList SmallIcons = new ImageList();

		// Token: 0x040013ED RID: 5101
		public static ImageList LargeIcons = new ImageList();

		// Token: 0x040013EE RID: 5102
		private static EPlatformHandler platform = EPlatformHandler.Default;

		// Token: 0x040013EF RID: 5103
		internal static Hashtable MimeIconIndex = new Hashtable();

		// Token: 0x040013F0 RID: 5104
		private static PlatformMimeIconHandler platformMimeHandler = null;

		// Token: 0x040013F1 RID: 5105
		private static object lock_object = new object();
	}
}
