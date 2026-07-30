using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Apple
{
	// Token: 0x020003E8 RID: 1000
	[NativeHeader("Runtime/Export/Apple/FrameCaptureMetalScriptBindings.h")]
	[NativeConditional("PLATFORM_IOS || PLATFORM_TVOS || PLATFORM_OSX")]
	public class FrameCapture
	{
		// Token: 0x060022BF RID: 8895 RVA: 0x000166AA File Offset: 0x000148AA
		private FrameCapture()
		{
		}

		// Token: 0x060022C0 RID: 8896
		[FreeFunction("FrameCaptureMetalScripting::IsDestinationSupported")]
		[MethodImpl(4096)]
		private static extern bool IsDestinationSupportedImpl(FrameCaptureDestination dest);

		// Token: 0x060022C1 RID: 8897
		[FreeFunction("FrameCaptureMetalScripting::BeginCapture")]
		[MethodImpl(4096)]
		private static extern void BeginCaptureImpl(FrameCaptureDestination dest, string path);

		// Token: 0x060022C2 RID: 8898
		[FreeFunction("FrameCaptureMetalScripting::EndCapture")]
		[MethodImpl(4096)]
		private static extern void EndCaptureImpl();

		// Token: 0x060022C3 RID: 8899 RVA: 0x0003A750 File Offset: 0x00038950
		public static bool IsDestinationSupported(FrameCaptureDestination dest)
		{
			bool flag = dest != FrameCaptureDestination.DevTools && dest != FrameCaptureDestination.GPUTraceDocument;
			if (flag)
			{
				throw new ArgumentException("dest", "Argument dest has bad value (not one of FrameCaptureDestination enum values)");
			}
			return FrameCapture.IsDestinationSupportedImpl(dest);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0003A78C File Offset: 0x0003898C
		public static void BeginCaptureToXcode()
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.DevTools);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture with DevTools is not supported.");
			}
			FrameCapture.BeginCaptureImpl(FrameCaptureDestination.DevTools, null);
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0003A7BC File Offset: 0x000389BC
		public static void BeginCaptureToFile(string path)
		{
			bool flag = !FrameCapture.IsDestinationSupported(FrameCaptureDestination.GPUTraceDocument);
			if (flag)
			{
				throw new InvalidOperationException("Frame Capture to file is not supported.");
			}
			bool flag2 = string.IsNullOrEmpty(path);
			if (flag2)
			{
				throw new ArgumentException("path", "Path must be supplied when capture destination is GPUTraceDocument.");
			}
			bool flag3 = Path.GetExtension(path) != ".gputrace";
			if (flag3)
			{
				throw new ArgumentException("path", "Destination file should have .gputrace extension.");
			}
			FrameCapture.BeginCaptureImpl(FrameCaptureDestination.GPUTraceDocument, new Uri(path).AbsoluteUri);
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x0003A832 File Offset: 0x00038A32
		public static void EndCapture()
		{
			FrameCapture.EndCaptureImpl();
		}
	}
}
