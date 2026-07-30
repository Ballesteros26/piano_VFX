using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x020000A5 RID: 165
	internal static class MacSupport
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x00015DB0 File Offset: 0x00013FB0
		static MacSupport()
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (string.Equals(assembly.GetName().Name, "System.Windows.Forms"))
				{
					Type type = assembly.GetType("System.Windows.Forms.XplatUICarbon");
					if (type != null)
					{
						MacSupport.hwnd_delegate = (Delegate)type.GetTypeInfo().GetField("HwndDelegate", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					}
				}
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00015E3C File Offset: 0x0001403C
		internal static CocoaContext GetCGContextForNSView(IntPtr handle)
		{
			IntPtr intPtr = MacSupport.objc_msgSend(MacSupport.objc_msgSend(MacSupport.objc_getClass("NSGraphicsContext"), MacSupport.sel_registerName("currentContext")), MacSupport.sel_registerName("graphicsPort"));
			Rect rect = default(Rect);
			MacSupport.CGContextSaveGState(intPtr);
			MacSupport.objc_msgSend_stret(ref rect, handle, MacSupport.sel_registerName("bounds"));
			if (MacSupport.bool_objc_msgSend(handle, MacSupport.sel_registerName("isFlipped")))
			{
				MacSupport.CGContextTranslateCTM(intPtr, rect.origin.x, rect.size.height);
				MacSupport.CGContextScaleCTM(intPtr, 1f, -1f);
			}
			return new CocoaContext(intPtr, (int)rect.size.width, (int)rect.size.height);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00015EF0 File Offset: 0x000140F0
		internal static CarbonContext GetCGContextForView(IntPtr handle)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			intPtr3 = MacSupport.GetControlOwner(handle);
			if (handle == IntPtr.Zero || intPtr3 == IntPtr.Zero)
			{
				intPtr2 = MacSupport.GetQDGlobalsThePort();
				MacSupport.CreateCGContextForPort(intPtr2, ref intPtr);
				Rect rect = MacSupport.CGDisplayBounds(MacSupport.CGMainDisplayID());
				return new CarbonContext(intPtr2, intPtr, (int)rect.size.width, (int)rect.size.height);
			}
			QDRect qdrect = default(QDRect);
			Rect rect2 = default(Rect);
			intPtr2 = MacSupport.GetWindowPort(intPtr3);
			intPtr = MacSupport.GetContext(intPtr2);
			MacSupport.GetWindowBounds(intPtr3, 32U, ref qdrect);
			MacSupport.HIViewGetBounds(handle, ref rect2);
			MacSupport.HIViewConvertRect(ref rect2, handle, IntPtr.Zero);
			if (rect2.size.height < 0f)
			{
				rect2.size.height = 0f;
			}
			if (rect2.size.width < 0f)
			{
				rect2.size.width = 0f;
			}
			MacSupport.CGContextTranslateCTM(intPtr, rect2.origin.x, (float)(qdrect.bottom - qdrect.top) - (rect2.origin.y + rect2.size.height));
			Rect rect3 = new Rect(0f, 0f, rect2.size.width, rect2.size.height);
			MacSupport.CGContextSaveGState(intPtr);
			Rectangle[] array = (Rectangle[])MacSupport.hwnd_delegate.DynamicInvoke(new object[] { handle });
			if (array != null && array.Length != 0)
			{
				int num = array.Length;
				MacSupport.CGContextBeginPath(intPtr);
				MacSupport.CGContextAddRect(intPtr, rect3);
				for (int i = 0; i < num; i++)
				{
					MacSupport.CGContextAddRect(intPtr, new Rect((float)array[i].X, rect2.size.height - (float)array[i].Y - (float)array[i].Height, (float)array[i].Width, (float)array[i].Height));
				}
				MacSupport.CGContextClosePath(intPtr);
				MacSupport.CGContextEOClip(intPtr);
			}
			else
			{
				MacSupport.CGContextBeginPath(intPtr);
				MacSupport.CGContextAddRect(intPtr, rect3);
				MacSupport.CGContextClosePath(intPtr);
				MacSupport.CGContextClip(intPtr);
			}
			return new CarbonContext(intPtr2, intPtr, (int)rect2.size.width, (int)rect2.size.height);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00016160 File Offset: 0x00014360
		internal static IntPtr GetContext(IntPtr port)
		{
			IntPtr zero = IntPtr.Zero;
			object obj = MacSupport.lockobj;
			lock (obj)
			{
				MacSupport.CreateCGContextForPort(port, ref zero);
			}
			return zero;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000161A8 File Offset: 0x000143A8
		internal static void ReleaseContext(IntPtr port, IntPtr context)
		{
			MacSupport.CGContextRestoreGState(context);
			object obj = MacSupport.lockobj;
			lock (obj)
			{
				MacSupport.CFRelease(context);
			}
		}

		// Token: 0x06000A0C RID: 2572
		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_getClass(string className);

		// Token: 0x06000A0D RID: 2573
		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_msgSend(IntPtr basePtr, IntPtr selector, string argument);

		// Token: 0x06000A0E RID: 2574
		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_msgSend(IntPtr basePtr, IntPtr selector);

		// Token: 0x06000A0F RID: 2575
		[DllImport("libobjc.dylib")]
		public static extern void objc_msgSend_stret(ref Rect arect, IntPtr basePtr, IntPtr selector);

		// Token: 0x06000A10 RID: 2576
		[DllImport("libobjc.dylib", EntryPoint = "objc_msgSend")]
		public static extern bool bool_objc_msgSend(IntPtr handle, IntPtr selector);

		// Token: 0x06000A11 RID: 2577
		[DllImport("libobjc.dylib")]
		public static extern IntPtr sel_registerName(string selectorName);

		// Token: 0x06000A12 RID: 2578
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr CGMainDisplayID();

		// Token: 0x06000A13 RID: 2579
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern Rect CGDisplayBounds(IntPtr display);

		// Token: 0x06000A14 RID: 2580
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewGetBounds(IntPtr vHnd, ref Rect r);

		// Token: 0x06000A15 RID: 2581
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewConvertRect(ref Rect r, IntPtr a, IntPtr b);

		// Token: 0x06000A16 RID: 2582
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetControlOwner(IntPtr aView);

		// Token: 0x06000A17 RID: 2583
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int GetWindowBounds(IntPtr wHnd, uint reg, ref QDRect rect);

		// Token: 0x06000A18 RID: 2584
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetWindowPort(IntPtr hWnd);

		// Token: 0x06000A19 RID: 2585
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetQDGlobalsThePort();

		// Token: 0x06000A1A RID: 2586
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CreateCGContextForPort(IntPtr port, ref IntPtr context);

		// Token: 0x06000A1B RID: 2587
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CFRelease(IntPtr context);

		// Token: 0x06000A1C RID: 2588
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void QDBeginCGContext(IntPtr port, ref IntPtr context);

		// Token: 0x06000A1D RID: 2589
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void QDEndCGContext(IntPtr port, ref IntPtr context);

		// Token: 0x06000A1E RID: 2590
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int CGContextClipToRect(IntPtr context, Rect clip);

		// Token: 0x06000A1F RID: 2591
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int CGContextClipToRects(IntPtr context, Rect[] clip_rects, int count);

		// Token: 0x06000A20 RID: 2592
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextTranslateCTM(IntPtr context, float tx, float ty);

		// Token: 0x06000A21 RID: 2593
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextScaleCTM(IntPtr context, float x, float y);

		// Token: 0x06000A22 RID: 2594
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextFlush(IntPtr context);

		// Token: 0x06000A23 RID: 2595
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextSynchronize(IntPtr context);

		// Token: 0x06000A24 RID: 2596
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr CGPathCreateMutable();

		// Token: 0x06000A25 RID: 2597
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGPathAddRects(IntPtr path, IntPtr _void, Rect[] rects, int count);

		// Token: 0x06000A26 RID: 2598
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGPathAddRect(IntPtr path, IntPtr _void, Rect rect);

		// Token: 0x06000A27 RID: 2599
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextAddRects(IntPtr context, Rect[] rects, int count);

		// Token: 0x06000A28 RID: 2600
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextAddRect(IntPtr context, Rect rect);

		// Token: 0x06000A29 RID: 2601
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextBeginPath(IntPtr context);

		// Token: 0x06000A2A RID: 2602
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextClosePath(IntPtr context);

		// Token: 0x06000A2B RID: 2603
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextAddPath(IntPtr context, IntPtr path);

		// Token: 0x06000A2C RID: 2604
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextClip(IntPtr context);

		// Token: 0x06000A2D RID: 2605
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextEOClip(IntPtr context);

		// Token: 0x06000A2E RID: 2606
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextEOFillPath(IntPtr context);

		// Token: 0x06000A2F RID: 2607
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextSaveGState(IntPtr context);

		// Token: 0x06000A30 RID: 2608
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextRestoreGState(IntPtr context);

		// Token: 0x0400061E RID: 1566
		internal static Hashtable contextReference = new Hashtable();

		// Token: 0x0400061F RID: 1567
		internal static object lockobj = new object();

		// Token: 0x04000620 RID: 1568
		internal static Delegate hwnd_delegate;
	}
}
