using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000DF RID: 223
	[NativeHeader("Runtime/GfxDevice/GfxDevice.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Camera/Camera.h")]
	[NativeHeader("Runtime/Camera/CameraUtil.h")]
	[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
	public sealed class GL
	{
		// Token: 0x0600073E RID: 1854
		[NativeName("ImmediateVertex")]
		[MethodImpl(4096)]
		public static extern void Vertex3(float x, float y, float z);

		// Token: 0x0600073F RID: 1855 RVA: 0x0000BD02 File Offset: 0x00009F02
		public static void Vertex(Vector3 v)
		{
			GL.Vertex3(v.x, v.y, v.z);
		}

		// Token: 0x06000740 RID: 1856
		[NativeName("ImmediateTexCoordAll")]
		[MethodImpl(4096)]
		public static extern void TexCoord3(float x, float y, float z);

		// Token: 0x06000741 RID: 1857 RVA: 0x0000BD1D File Offset: 0x00009F1D
		public static void TexCoord(Vector3 v)
		{
			GL.TexCoord3(v.x, v.y, v.z);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000BD38 File Offset: 0x00009F38
		public static void TexCoord2(float x, float y)
		{
			GL.TexCoord3(x, y, 0f);
		}

		// Token: 0x06000743 RID: 1859
		[NativeName("ImmediateTexCoord")]
		[MethodImpl(4096)]
		public static extern void MultiTexCoord3(int unit, float x, float y, float z);

		// Token: 0x06000744 RID: 1860 RVA: 0x0000BD48 File Offset: 0x00009F48
		public static void MultiTexCoord(int unit, Vector3 v)
		{
			GL.MultiTexCoord3(unit, v.x, v.y, v.z);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0000BD64 File Offset: 0x00009F64
		public static void MultiTexCoord2(int unit, float x, float y)
		{
			GL.MultiTexCoord3(unit, x, y, 0f);
		}

		// Token: 0x06000746 RID: 1862
		[NativeName("ImmediateColor")]
		[MethodImpl(4096)]
		private static extern void ImmediateColor(float r, float g, float b, float a);

		// Token: 0x06000747 RID: 1863 RVA: 0x0000BD75 File Offset: 0x00009F75
		public static void Color(Color c)
		{
			GL.ImmediateColor(c.r, c.g, c.b, c.a);
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000748 RID: 1864
		// (set) Token: 0x06000749 RID: 1865
		public static extern bool wireframe
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600074A RID: 1866
		// (set) Token: 0x0600074B RID: 1867
		public static extern bool sRGBWrite
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600074C RID: 1868
		// (set) Token: 0x0600074D RID: 1869
		[NativeProperty("UserBackfaceMode")]
		public static extern bool invertCulling
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600074E RID: 1870
		[MethodImpl(4096)]
		public static extern void Flush();

		// Token: 0x0600074F RID: 1871
		[MethodImpl(4096)]
		public static extern void RenderTargetBarrier();

		// Token: 0x06000750 RID: 1872 RVA: 0x0000BD98 File Offset: 0x00009F98
		private static Matrix4x4 GetWorldViewMatrix()
		{
			Matrix4x4 matrix4x;
			GL.GetWorldViewMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0000BDAD File Offset: 0x00009FAD
		private static void SetViewMatrix(Matrix4x4 m)
		{
			GL.SetViewMatrix_Injected(ref m);
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x0000BDCF File Offset: 0x00009FCF
		public static Matrix4x4 modelview
		{
			get
			{
				return GL.GetWorldViewMatrix();
			}
			set
			{
				GL.SetViewMatrix(value);
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000BDD9 File Offset: 0x00009FD9
		[NativeName("SetWorldMatrix")]
		public static void MultMatrix(Matrix4x4 m)
		{
			GL.MultMatrix_Injected(ref m);
		}

		// Token: 0x06000755 RID: 1877
		[NativeName("InsertCustomMarker")]
		[Obsolete("IssuePluginEvent(eventID) is deprecated. Use IssuePluginEvent(callback, eventID) instead.", false)]
		[MethodImpl(4096)]
		public static extern void IssuePluginEvent(int eventID);

		// Token: 0x06000756 RID: 1878
		[Obsolete("SetRevertBackfacing(revertBackFaces) is deprecated. Use invertCulling property instead.", false)]
		[NativeName("SetUserBackfaceMode")]
		[MethodImpl(4096)]
		public static extern void SetRevertBackfacing(bool revertBackFaces);

		// Token: 0x06000757 RID: 1879
		[FreeFunction("GLPushMatrixScript")]
		[MethodImpl(4096)]
		public static extern void PushMatrix();

		// Token: 0x06000758 RID: 1880
		[FreeFunction("GLPopMatrixScript")]
		[MethodImpl(4096)]
		public static extern void PopMatrix();

		// Token: 0x06000759 RID: 1881
		[FreeFunction("GLLoadIdentityScript")]
		[MethodImpl(4096)]
		public static extern void LoadIdentity();

		// Token: 0x0600075A RID: 1882
		[FreeFunction("GLLoadOrthoScript")]
		[MethodImpl(4096)]
		public static extern void LoadOrtho();

		// Token: 0x0600075B RID: 1883
		[FreeFunction("GLLoadPixelMatrixScript")]
		[MethodImpl(4096)]
		public static extern void LoadPixelMatrix();

		// Token: 0x0600075C RID: 1884 RVA: 0x0000BDE2 File Offset: 0x00009FE2
		[FreeFunction("GLLoadProjectionMatrixScript")]
		public static void LoadProjectionMatrix(Matrix4x4 mat)
		{
			GL.LoadProjectionMatrix_Injected(ref mat);
		}

		// Token: 0x0600075D RID: 1885
		[FreeFunction("GLInvalidateState")]
		[MethodImpl(4096)]
		public static extern void InvalidateState();

		// Token: 0x0600075E RID: 1886 RVA: 0x0000BDEC File Offset: 0x00009FEC
		[FreeFunction("GLGetGPUProjectionMatrix")]
		public static Matrix4x4 GetGPUProjectionMatrix(Matrix4x4 proj, bool renderIntoTexture)
		{
			Matrix4x4 matrix4x;
			GL.GetGPUProjectionMatrix_Injected(ref proj, renderIntoTexture, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600075F RID: 1887
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern void GLLoadPixelMatrixScript(float left, float right, float bottom, float top);

		// Token: 0x06000760 RID: 1888 RVA: 0x0000BE04 File Offset: 0x0000A004
		public static void LoadPixelMatrix(float left, float right, float bottom, float top)
		{
			GL.GLLoadPixelMatrixScript(left, right, bottom, top);
		}

		// Token: 0x06000761 RID: 1889
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern void GLIssuePluginEvent(IntPtr callback, int eventID);

		// Token: 0x06000762 RID: 1890 RVA: 0x0000BE14 File Offset: 0x0000A014
		public static void IssuePluginEvent(IntPtr callback, int eventID)
		{
			bool flag = callback == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Null callback specified.", "callback");
			}
			GL.GLIssuePluginEvent(callback, eventID);
		}

		// Token: 0x06000763 RID: 1891
		[FreeFunction("GLBegin", ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern void Begin(int mode);

		// Token: 0x06000764 RID: 1892
		[FreeFunction("GLEnd")]
		[MethodImpl(4096)]
		public static extern void End();

		// Token: 0x06000765 RID: 1893 RVA: 0x0000BE49 File Offset: 0x0000A049
		[FreeFunction]
		private static void GLClear(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
		{
			GL.GLClear_Injected(clearDepth, clearColor, ref backgroundColor, depth);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000BE55 File Offset: 0x0000A055
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor, [DefaultValue("1.0f")] float depth)
		{
			GL.GLClear(clearDepth, clearColor, backgroundColor, depth);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000BE62 File Offset: 0x0000A062
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor)
		{
			GL.GLClear(clearDepth, clearColor, backgroundColor, 1f);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000BE73 File Offset: 0x0000A073
		[FreeFunction("SetGLViewport")]
		public static void Viewport(Rect pixelRect)
		{
			GL.Viewport_Injected(ref pixelRect);
		}

		// Token: 0x06000769 RID: 1897
		[FreeFunction("ClearWithSkybox")]
		[MethodImpl(4096)]
		public static extern void ClearWithSkybox(bool clearDepth, Camera camera);

		// Token: 0x0600076B RID: 1899
		[MethodImpl(4096)]
		private static extern void GetWorldViewMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x0600076C RID: 1900
		[MethodImpl(4096)]
		private static extern void SetViewMatrix_Injected(ref Matrix4x4 m);

		// Token: 0x0600076D RID: 1901
		[MethodImpl(4096)]
		private static extern void MultMatrix_Injected(ref Matrix4x4 m);

		// Token: 0x0600076E RID: 1902
		[MethodImpl(4096)]
		private static extern void LoadProjectionMatrix_Injected(ref Matrix4x4 mat);

		// Token: 0x0600076F RID: 1903
		[MethodImpl(4096)]
		private static extern void GetGPUProjectionMatrix_Injected(ref Matrix4x4 proj, bool renderIntoTexture, out Matrix4x4 ret);

		// Token: 0x06000770 RID: 1904
		[MethodImpl(4096)]
		private static extern void GLClear_Injected(bool clearDepth, bool clearColor, ref Color backgroundColor, float depth);

		// Token: 0x06000771 RID: 1905
		[MethodImpl(4096)]
		private static extern void Viewport_Injected(ref Rect pixelRect);

		// Token: 0x0400026F RID: 623
		public const int TRIANGLES = 4;

		// Token: 0x04000270 RID: 624
		public const int TRIANGLE_STRIP = 5;

		// Token: 0x04000271 RID: 625
		public const int QUADS = 7;

		// Token: 0x04000272 RID: 626
		public const int LINES = 1;

		// Token: 0x04000273 RID: 627
		public const int LINE_STRIP = 2;
	}
}
