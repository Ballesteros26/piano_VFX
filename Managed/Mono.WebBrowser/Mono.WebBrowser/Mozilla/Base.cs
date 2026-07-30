using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using Mono.WebBrowser;

namespace Mono.Mozilla
{
	// Token: 0x02000038 RID: 56
	internal class Base
	{
		// Token: 0x06000195 RID: 405 RVA: 0x000026AB File Offset: 0x000008AB
		private static bool isInitialized()
		{
			return Base.initialized;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000026B7 File Offset: 0x000008B7
		private static Base.BindingInfo getBinding(IWebBrowser control)
		{
			if (!Base.boundControls.ContainsKey(control))
			{
				return null;
			}
			return Base.boundControls[control] as Base.BindingInfo;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000026F6 File Offset: 0x000008F6
		public static void Debug(int signal)
		{
			Base.gluezilla_debug(signal);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00002700 File Offset: 0x00000900
		public static bool Init(WebBrowser control, Platform platform)
		{
			object obj = Base.initLock;
			lock (obj)
			{
				if (!Base.initialized)
				{
					Platform platform2;
					try
					{
						short num = Base.gluezilla_init(platform, out platform2);
						Base.monoMozDir = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ".mono"), "mozilla-" + num);
						if (!Directory.Exists(Base.monoMozDir))
						{
							Directory.CreateDirectory(Base.monoMozDir);
						}
					}
					catch (DllNotFoundException)
					{
						Console.WriteLine("libgluezilla not found. To have webbrowser support, you need libgluezilla installed");
						Base.initialized = false;
						return false;
					}
					control.enginePlatform = platform2;
					Base.initialized = true;
				}
			}
			return Base.initialized;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000027C4 File Offset: 0x000009C4
		public static bool Bind(WebBrowser control, IntPtr handle, int width, int height)
		{
			if (!Base.isInitialized())
			{
				return false;
			}
			Base.BindingInfo bindingInfo = new Base.BindingInfo();
			bindingInfo.callback = new CallbackBinder(control.callbacks);
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf<CallbackBinder>(bindingInfo.callback));
			Marshal.StructureToPtr<CallbackBinder>(bindingInfo.callback, intPtr, true);
			bindingInfo.gluezilla = Base.gluezilla_bind(intPtr, handle, width, height, Environment.CurrentDirectory, Base.monoMozDir, control.platform);
			object obj = Base.initLock;
			lock (obj)
			{
				if (bindingInfo.gluezilla == IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
					bindingInfo = null;
					Base.initialized = false;
					return false;
				}
			}
			Base.boundControls.Add(control, bindingInfo);
			return true;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00002890 File Offset: 0x00000A90
		public static bool Create(IWebBrowser control)
		{
			if (!Base.isInitialized())
			{
				return false;
			}
			Base.gluezilla_createBrowserWindow(Base.getBinding(control).gluezilla);
			return true;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000028B0 File Offset: 0x00000AB0
		public static void Shutdown(IWebBrowser control)
		{
			object obj = Base.initLock;
			lock (obj)
			{
				if (Base.initialized)
				{
					Base.gluezilla_shutdown(Base.getBinding(control).gluezilla);
					Base.boundControls.Remove(control);
					if (Base.boundControls.Count == 0)
					{
						Base.initialized = false;
					}
				}
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00002920 File Offset: 0x00000B20
		public static void Focus(IWebBrowser control, FocusOption focus)
		{
			if (!Base.isInitialized())
			{
				return;
			}
			Base.gluezilla_focus(Base.getBinding(control).gluezilla, focus);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000293C File Offset: 0x00000B3C
		public static void Blur(IWebBrowser control)
		{
			if (!Base.isInitialized())
			{
				return;
			}
			Base.gluezilla_blur(Base.getBinding(control).gluezilla);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00002957 File Offset: 0x00000B57
		public static void Activate(IWebBrowser control)
		{
			if (!Base.isInitialized())
			{
				return;
			}
			Base.gluezilla_activate(Base.getBinding(control).gluezilla);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00002972 File Offset: 0x00000B72
		public static void Deactivate(IWebBrowser control)
		{
			if (!Base.isInitialized())
			{
				return;
			}
			Base.gluezilla_deactivate(Base.getBinding(control).gluezilla);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000298D File Offset: 0x00000B8D
		public static void Resize(IWebBrowser control, int width, int height)
		{
			if (!Base.isInitialized())
			{
				return;
			}
			Base.gluezilla_resize(Base.getBinding(control).gluezilla, width, height);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000029AA File Offset: 0x00000BAA
		public static void Home(IWebBrowser control)
		{
			if (!Base.isInitialized())
			{
				return;
			}
			Base.gluezilla_home(Base.getBinding(control).gluezilla);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static nsIWebNavigation GetWebNavigation(IWebBrowser control)
		{
			if (!Base.isInitialized())
			{
				return null;
			}
			return Base.gluezilla_getWebNavigation(Base.getBinding(control).gluezilla);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000029E0 File Offset: 0x00000BE0
		public static IntPtr StringInit()
		{
			if (!Base.isInitialized())
			{
				return IntPtr.Zero;
			}
			return Base.gluezilla_stringInit();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000029F4 File Offset: 0x00000BF4
		public static void StringFinish(HandleRef str)
		{
			if (!Base.isInitialized())
			{
				return;
			}
			Base.gluezilla_stringFinish(str);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00002A05 File Offset: 0x00000C05
		public static string StringGet(HandleRef str)
		{
			if (!Base.isInitialized())
			{
				return string.Empty;
			}
			return Marshal.PtrToStringUni(Base.gluezilla_stringGet(str));
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00002A1F File Offset: 0x00000C1F
		public static void StringSet(HandleRef str, string text)
		{
			if (!Base.isInitialized())
			{
				return;
			}
			Base.gluezilla_stringSet(str, text);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00002A30 File Offset: 0x00000C30
		public static object GetProxyForObject(IWebBrowser control, Guid iid, object obj)
		{
			if (!Base.isInitialized())
			{
				return null;
			}
			IntPtr intPtr;
			Base.gluezilla_getProxyForObject(Base.getBinding(control).gluezilla, iid, obj, out intPtr);
			return Marshal.GetObjectForIUnknown(intPtr);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00002A60 File Offset: 0x00000C60
		public static nsIServiceManager GetServiceManager(IWebBrowser control)
		{
			if (!Base.isInitialized())
			{
				return null;
			}
			return Base.gluezilla_getServiceManager2(Base.getBinding(control).gluezilla);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00002A7B File Offset: 0x00000C7B
		public static string EvalScript(IWebBrowser control, string script)
		{
			if (!Base.isInitialized())
			{
				return null;
			}
			return Marshal.PtrToStringAuto(Base.gluezilla_evalScript(Base.getBinding(control).gluezilla, script));
		}

		// Token: 0x060001AC RID: 428
		[DllImport("gluezilla")]
		private static extern void gluezilla_debug(int signal);

		// Token: 0x060001AD RID: 429
		[DllImport("gluezilla")]
		private static extern short gluezilla_init(Platform platform, out Platform mozPlatform);

		// Token: 0x060001AE RID: 430
		[DllImport("gluezilla")]
		private static extern IntPtr gluezilla_bind(IntPtr events, IntPtr hwnd, int width, int height, string startDir, string dataDir, Platform platform);

		// Token: 0x060001AF RID: 431
		[DllImport("gluezilla")]
		private static extern int gluezilla_createBrowserWindow(IntPtr instance);

		// Token: 0x060001B0 RID: 432
		[DllImport("gluezilla")]
		private static extern IntPtr gluezilla_shutdown(IntPtr instance);

		// Token: 0x060001B1 RID: 433
		[DllImport("gluezilla")]
		private static extern int gluezilla_focus(IntPtr instance, FocusOption focus);

		// Token: 0x060001B2 RID: 434
		[DllImport("gluezilla")]
		private static extern int gluezilla_blur(IntPtr instance);

		// Token: 0x060001B3 RID: 435
		[DllImport("gluezilla")]
		private static extern int gluezilla_activate(IntPtr instance);

		// Token: 0x060001B4 RID: 436
		[DllImport("gluezilla")]
		private static extern int gluezilla_deactivate(IntPtr instance);

		// Token: 0x060001B5 RID: 437
		[DllImport("gluezilla")]
		private static extern int gluezilla_resize(IntPtr instance, int width, int height);

		// Token: 0x060001B6 RID: 438
		[DllImport("gluezilla")]
		private static extern int gluezilla_home(IntPtr instance);

		// Token: 0x060001B7 RID: 439
		[DllImport("gluezilla")]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern nsIWebNavigation gluezilla_getWebNavigation(IntPtr instance);

		// Token: 0x060001B8 RID: 440
		[DllImport("gluezilla")]
		private static extern IntPtr gluezilla_stringInit();

		// Token: 0x060001B9 RID: 441
		[DllImport("gluezilla")]
		private static extern int gluezilla_stringFinish(HandleRef str);

		// Token: 0x060001BA RID: 442
		[DllImport("gluezilla")]
		private static extern IntPtr gluezilla_stringGet(HandleRef str);

		// Token: 0x060001BB RID: 443
		[DllImport("gluezilla")]
		private static extern void gluezilla_stringSet(HandleRef str, [MarshalAs(UnmanagedType.LPWStr)] string text);

		// Token: 0x060001BC RID: 444
		[DllImport("gluezilla")]
		private static extern void gluezilla_getProxyForObject(IntPtr instance, [MarshalAs(UnmanagedType.LPStruct)] Guid iid, [MarshalAs(UnmanagedType.Interface)] object obj, out IntPtr ret);

		// Token: 0x060001BD RID: 445
		[DllImport("gluezilla")]
		public static extern uint gluezilla_StringContainerInit(HandleRef aStr);

		// Token: 0x060001BE RID: 446
		[DllImport("gluezilla")]
		public static extern void gluezilla_StringContainerFinish(HandleRef aStr);

		// Token: 0x060001BF RID: 447
		[DllImport("gluezilla")]
		public static extern uint gluezilla_StringGetData(HandleRef aStr, out IntPtr aBuf, out bool aTerm);

		// Token: 0x060001C0 RID: 448
		[DllImport("gluezilla")]
		public static extern uint gluezilla_StringSetData(HandleRef aStr, [MarshalAs(UnmanagedType.LPWStr)] string aBuf, uint aCount);

		// Token: 0x060001C1 RID: 449
		[DllImport("gluezilla")]
		public static extern uint gluezilla_CStringContainerInit(HandleRef aStr);

		// Token: 0x060001C2 RID: 450
		[DllImport("gluezilla")]
		public static extern void gluezilla_CStringContainerFinish(HandleRef aStr);

		// Token: 0x060001C3 RID: 451
		[DllImport("gluezilla")]
		public static extern uint gluezilla_CStringGetData(HandleRef aStr, out IntPtr aBuf, out bool aTerm);

		// Token: 0x060001C4 RID: 452
		[DllImport("gluezilla")]
		public static extern uint gluezilla_CStringSetData(HandleRef aStr, string aBuf, uint aCount);

		// Token: 0x060001C5 RID: 453
		[DllImport("gluezilla")]
		[return: MarshalAs(UnmanagedType.Interface)]
		public static extern nsIServiceManager gluezilla_getServiceManager2(IntPtr instance);

		// Token: 0x060001C6 RID: 454
		[DllImport("gluezilla")]
		private static extern IntPtr gluezilla_evalScript(IntPtr instance, string script);

		// Token: 0x0400008D RID: 141
		private static Hashtable boundControls = new Hashtable();

		// Token: 0x0400008E RID: 142
		private static bool initialized;

		// Token: 0x0400008F RID: 143
		private static object initLock = new object();

		// Token: 0x04000090 RID: 144
		private static string monoMozDir;

		// Token: 0x0200014A RID: 330
		private class BindingInfo
		{
			// Token: 0x04000169 RID: 361
			public CallbackBinder callback;

			// Token: 0x0400016A RID: 362
			public IntPtr gluezilla;
		}
	}
}
