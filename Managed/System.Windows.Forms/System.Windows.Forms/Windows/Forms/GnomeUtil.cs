using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000268 RID: 616
	internal class GnomeUtil
	{
		// Token: 0x060027E4 RID: 10212
		[DllImport("librsvg-2.so.2")]
		private static extern IntPtr rsvg_pixbuf_from_file_at_size(string file_name, int width, int height, out IntPtr error);

		// Token: 0x060027E5 RID: 10213
		[DllImport("libgdk_pixbuf-2.0.so.0")]
		private static extern bool gdk_pixbuf_save_to_buffer(IntPtr pixbuf, out IntPtr buffer, out UIntPtr buffer_size, string type, out IntPtr error, IntPtr option_dummy);

		// Token: 0x060027E6 RID: 10214
		[DllImport("libglib-2.0.so.0")]
		private static extern void g_free(IntPtr mem);

		// Token: 0x060027E7 RID: 10215
		[DllImport("libgdk-x11-2.0.so.0")]
		private static extern bool gdk_init_check(IntPtr argc, IntPtr argv);

		// Token: 0x060027E8 RID: 10216
		[DllImport("libgobject-2.0.so.0")]
		private static extern void g_object_unref(IntPtr nativeObject);

		// Token: 0x060027E9 RID: 10217
		[DllImport("libgnomeui-2.so.0")]
		private static extern string gnome_icon_lookup(IntPtr icon_theme, IntPtr thumbnail_factory, string file_uri, string custom_icon, IntPtr file_info, string mime_type, GnomeUtil.GnomeIconLookupFlags flags, IntPtr result);

		// Token: 0x060027EA RID: 10218
		[DllImport("libgtk-x11-2.0.so.0")]
		private static extern IntPtr gtk_icon_theme_get_default();

		// Token: 0x060027EB RID: 10219
		[DllImport("libgtk-x11-2.0.so.0")]
		private static extern IntPtr gtk_icon_theme_load_icon(IntPtr icon_theme, string icon_name, int size, GnomeUtil.GtkIconLookupFlags flags, out IntPtr error);

		// Token: 0x060027EC RID: 10220
		[DllImport("libgtk-x11-2.0.so.0")]
		private static extern bool gtk_icon_theme_has_icon(IntPtr icon_theme, string icon_name);

		// Token: 0x060027ED RID: 10221 RVA: 0x00099564 File Offset: 0x00097764
		private static void Init()
		{
			GnomeUtil.gdk_init_check(IntPtr.Zero, IntPtr.Zero);
			GnomeUtil.inited = true;
			GnomeUtil.default_icon_theme = GnomeUtil.gtk_icon_theme_get_default();
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x00099594 File Offset: 0x00097794
		public static Image GetIcon(string file_name, string mime_type, int size)
		{
			if (!GnomeUtil.inited)
			{
				GnomeUtil.Init();
			}
			Uri uri = new Uri(file_name);
			string text = GnomeUtil.gnome_icon_lookup(GnomeUtil.default_icon_theme, IntPtr.Zero, uri.AbsoluteUri, null, IntPtr.Zero, mime_type, GnomeUtil.GnomeIconLookupFlags.GNOME_ICON_LOOKUP_FLAGS_NONE, IntPtr.Zero);
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = GnomeUtil.gtk_icon_theme_load_icon(GnomeUtil.default_icon_theme, text, size, GnomeUtil.GtkIconLookupFlags.GTK_ICON_LOOKUP_USE_BUILTIN, out zero);
			if (zero != IntPtr.Zero)
			{
				return null;
			}
			return GnomeUtil.GdkPixbufToImage(intPtr);
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x00099608 File Offset: 0x00097808
		public static Image GetIcon(string icon, int size)
		{
			if (!GnomeUtil.inited)
			{
				GnomeUtil.Init();
			}
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = GnomeUtil.gtk_icon_theme_load_icon(GnomeUtil.default_icon_theme, icon, size, GnomeUtil.GtkIconLookupFlags.GTK_ICON_LOOKUP_USE_BUILTIN, out zero);
			if (zero != IntPtr.Zero)
			{
				return null;
			}
			return GnomeUtil.GdkPixbufToImage(intPtr);
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x00099654 File Offset: 0x00097854
		public static Image GdkPixbufToImage(IntPtr pixbuf)
		{
			IntPtr zero = IntPtr.Zero;
			string text = "png";
			IntPtr intPtr;
			UIntPtr uintPtr;
			if (!GnomeUtil.gdk_pixbuf_save_to_buffer(pixbuf, out intPtr, out uintPtr, text, out zero, IntPtr.Zero))
			{
				return null;
			}
			int num = (int)((uint)(ulong)uintPtr);
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			GnomeUtil.g_free(intPtr);
			GnomeUtil.g_object_unref(pixbuf);
			MemoryStream memoryStream = new MemoryStream(array);
			return Image.FromStream(memoryStream);
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000996CC File Offset: 0x000978CC
		public static Image GetSVGasImage(string filename, int width, int height)
		{
			if (!GnomeUtil.inited)
			{
				GnomeUtil.Init();
			}
			if (!File.Exists(filename))
			{
				return null;
			}
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = GnomeUtil.rsvg_pixbuf_from_file_at_size(filename, width, height, out zero);
			if (zero != IntPtr.Zero)
			{
				return null;
			}
			return GnomeUtil.GdkPixbufToImage(intPtr);
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x00099720 File Offset: 0x00097920
		public static bool HasImage(string name)
		{
			if (!GnomeUtil.inited)
			{
				GnomeUtil.Init();
			}
			return GnomeUtil.gtk_icon_theme_has_icon(GnomeUtil.default_icon_theme, name);
		}

		// Token: 0x040013F3 RID: 5107
		private const string libgdk = "libgdk-x11-2.0.so.0";

		// Token: 0x040013F4 RID: 5108
		private const string libgdk_pixbuf = "libgdk_pixbuf-2.0.so.0";

		// Token: 0x040013F5 RID: 5109
		private const string libgtk = "libgtk-x11-2.0.so.0";

		// Token: 0x040013F6 RID: 5110
		private const string libglib = "libglib-2.0.so.0";

		// Token: 0x040013F7 RID: 5111
		private const string libgobject = "libgobject-2.0.so.0";

		// Token: 0x040013F8 RID: 5112
		private const string libgnomeui = "libgnomeui-2.so.0";

		// Token: 0x040013F9 RID: 5113
		private const string librsvg = "librsvg-2.so.2";

		// Token: 0x040013FA RID: 5114
		private static bool inited = false;

		// Token: 0x040013FB RID: 5115
		private static IntPtr default_icon_theme = IntPtr.Zero;

		// Token: 0x02000269 RID: 617
		private enum GnomeIconLookupFlags
		{
			// Token: 0x040013FD RID: 5117
			GNOME_ICON_LOOKUP_FLAGS_NONE,
			// Token: 0x040013FE RID: 5118
			GNOME_ICON_LOOKUP_FLAGS_EMBEDDING_TEXT,
			// Token: 0x040013FF RID: 5119
			GNOME_ICON_LOOKUP_FLAGS_SHOW_SMALL_IMAGES_AS_THEMSELVES,
			// Token: 0x04001400 RID: 5120
			GNOME_ICON_LOOKUP_FLAGS_ALLOW_SVG_AS_THEMSELVES = 4
		}

		// Token: 0x0200026A RID: 618
		private enum GtkIconLookupFlags
		{
			// Token: 0x04001402 RID: 5122
			GTK_ICON_LOOKUP_NO_SVG = 1,
			// Token: 0x04001403 RID: 5123
			GTK_ICON_LOOKUP_FORCE_SVG,
			// Token: 0x04001404 RID: 5124
			GTK_ICON_LOOKUP_USE_BUILTIN = 4
		}
	}
}
