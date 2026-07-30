using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000D0 RID: 208
	[NativeHeader("Runtime/Export/Gizmos/Gizmos.bindings.h")]
	[StaticAccessor("GizmoBindings", StaticAccessorType.DoubleColon)]
	public sealed class Gizmos
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x00009823 File Offset: 0x00007A23
		[NativeThrows]
		public static void DrawLine(Vector3 from, Vector3 to)
		{
			Gizmos.DrawLine_Injected(ref from, ref to);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000982E File Offset: 0x00007A2E
		[NativeThrows]
		public static void DrawWireSphere(Vector3 center, float radius)
		{
			Gizmos.DrawWireSphere_Injected(ref center, radius);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00009838 File Offset: 0x00007A38
		[NativeThrows]
		public static void DrawSphere(Vector3 center, float radius)
		{
			Gizmos.DrawSphere_Injected(ref center, radius);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00009842 File Offset: 0x00007A42
		[NativeThrows]
		public static void DrawWireCube(Vector3 center, Vector3 size)
		{
			Gizmos.DrawWireCube_Injected(ref center, ref size);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000984D File Offset: 0x00007A4D
		[NativeThrows]
		public static void DrawCube(Vector3 center, Vector3 size)
		{
			Gizmos.DrawCube_Injected(ref center, ref size);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00009858 File Offset: 0x00007A58
		[NativeThrows]
		public static void DrawMesh(Mesh mesh, int submeshIndex, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawMesh_Injected(mesh, submeshIndex, ref position, ref rotation, ref scale);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00009867 File Offset: 0x00007A67
		[NativeThrows]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawWireMesh_Injected(mesh, submeshIndex, ref position, ref rotation, ref scale);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00009876 File Offset: 0x00007A76
		[NativeThrows]
		public static void DrawIcon(Vector3 center, string name, [DefaultValue("true")] bool allowScaling)
		{
			Gizmos.DrawIcon(center, name, allowScaling, Color.white);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00009887 File Offset: 0x00007A87
		[NativeThrows]
		public static void DrawIcon(Vector3 center, string name, [DefaultValue("true")] bool allowScaling, [DefaultValue("Color(255,255,255,255)")] Color tint)
		{
			Gizmos.DrawIcon_Injected(ref center, name, allowScaling, ref tint);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00009894 File Offset: 0x00007A94
		[NativeThrows]
		public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat)
		{
			Gizmos.DrawGUITexture_Injected(ref screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat);
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x000098A8 File Offset: 0x00007AA8
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x000098BD File Offset: 0x00007ABD
		public static Color color
		{
			get
			{
				Color color;
				Gizmos.get_color_Injected(out color);
				return color;
			}
			set
			{
				Gizmos.set_color_Injected(ref value);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000098C8 File Offset: 0x00007AC8
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x000098DD File Offset: 0x00007ADD
		public static Matrix4x4 matrix
		{
			get
			{
				Matrix4x4 matrix4x;
				Gizmos.get_matrix_Injected(out matrix4x);
				return matrix4x;
			}
			set
			{
				Gizmos.set_matrix_Injected(ref value);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005D3 RID: 1491
		// (set) Token: 0x060005D4 RID: 1492
		public static extern Texture exposure
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005D5 RID: 1493
		public static extern float probeSize
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000098E6 File Offset: 0x00007AE6
		public static void DrawFrustum(Vector3 center, float fov, float maxRange, float minRange, float aspect)
		{
			Gizmos.DrawFrustum_Injected(ref center, fov, maxRange, minRange, aspect);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000098F4 File Offset: 0x00007AF4
		public static void DrawRay(Ray r)
		{
			Gizmos.DrawLine(r.origin, r.origin + r.direction);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00009917 File Offset: 0x00007B17
		public static void DrawRay(Vector3 from, Vector3 direction)
		{
			Gizmos.DrawLine(from, from + direction);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00009928 File Offset: 0x00007B28
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawMesh(mesh, position, rotation, one);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00009948 File Offset: 0x00007B48
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawMesh(mesh, position, identity, one);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000996C File Offset: 0x00007B6C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawMesh(mesh, zero, identity, one);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00009996 File Offset: 0x00007B96
		public static void DrawMesh(Mesh mesh, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawMesh(mesh, -1, position, rotation, scale);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000099A4 File Offset: 0x00007BA4
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawMesh(mesh, submeshIndex, position, rotation, one);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000099C4 File Offset: 0x00007BC4
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, int submeshIndex, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawMesh(mesh, submeshIndex, position, identity, one);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000099EC File Offset: 0x00007BEC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, int submeshIndex)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawMesh(mesh, submeshIndex, zero, identity, one);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00009A18 File Offset: 0x00007C18
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawWireMesh(mesh, position, rotation, one);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00009A38 File Offset: 0x00007C38
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawWireMesh(mesh, position, identity, one);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00009A5C File Offset: 0x00007C5C
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawWireMesh(mesh, zero, identity, one);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00009A86 File Offset: 0x00007C86
		public static void DrawWireMesh(Mesh mesh, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawWireMesh(mesh, -1, position, rotation, scale);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00009A94 File Offset: 0x00007C94
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawWireMesh(mesh, submeshIndex, position, rotation, one);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00009AB4 File Offset: 0x00007CB4
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawWireMesh(mesh, submeshIndex, position, identity, one);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00009ADC File Offset: 0x00007CDC
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawWireMesh(mesh, submeshIndex, zero, identity, one);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00009B08 File Offset: 0x00007D08
		[ExcludeFromDocs]
		public static void DrawIcon(Vector3 center, string name)
		{
			bool flag = true;
			Gizmos.DrawIcon(center, name, flag);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00009B24 File Offset: 0x00007D24
		[ExcludeFromDocs]
		public static void DrawGUITexture(Rect screenRect, Texture texture)
		{
			Material material = null;
			Gizmos.DrawGUITexture(screenRect, texture, material);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00009B3D File Offset: 0x00007D3D
		public static void DrawGUITexture(Rect screenRect, Texture texture, [DefaultValue("null")] Material mat)
		{
			Gizmos.DrawGUITexture(screenRect, texture, 0, 0, 0, 0, mat);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00009B50 File Offset: 0x00007D50
		[ExcludeFromDocs]
		public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Material material = null;
			Gizmos.DrawGUITexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, material);
		}

		// Token: 0x060005EC RID: 1516
		[MethodImpl(4096)]
		private static extern void DrawLine_Injected(ref Vector3 from, ref Vector3 to);

		// Token: 0x060005ED RID: 1517
		[MethodImpl(4096)]
		private static extern void DrawWireSphere_Injected(ref Vector3 center, float radius);

		// Token: 0x060005EE RID: 1518
		[MethodImpl(4096)]
		private static extern void DrawSphere_Injected(ref Vector3 center, float radius);

		// Token: 0x060005EF RID: 1519
		[MethodImpl(4096)]
		private static extern void DrawWireCube_Injected(ref Vector3 center, ref Vector3 size);

		// Token: 0x060005F0 RID: 1520
		[MethodImpl(4096)]
		private static extern void DrawCube_Injected(ref Vector3 center, ref Vector3 size);

		// Token: 0x060005F1 RID: 1521
		[MethodImpl(4096)]
		private static extern void DrawMesh_Injected(Mesh mesh, int submeshIndex, [DefaultValue("Vector3.zero")] ref Vector3 position, [DefaultValue("Quaternion.identity")] ref Quaternion rotation, [DefaultValue("Vector3.one")] ref Vector3 scale);

		// Token: 0x060005F2 RID: 1522
		[MethodImpl(4096)]
		private static extern void DrawWireMesh_Injected(Mesh mesh, int submeshIndex, [DefaultValue("Vector3.zero")] ref Vector3 position, [DefaultValue("Quaternion.identity")] ref Quaternion rotation, [DefaultValue("Vector3.one")] ref Vector3 scale);

		// Token: 0x060005F3 RID: 1523
		[MethodImpl(4096)]
		private static extern void DrawIcon_Injected(ref Vector3 center, string name, [DefaultValue("true")] bool allowScaling, [DefaultValue("Color(255,255,255,255)")] ref Color tint);

		// Token: 0x060005F4 RID: 1524
		[MethodImpl(4096)]
		private static extern void DrawGUITexture_Injected(ref Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat);

		// Token: 0x060005F5 RID: 1525
		[MethodImpl(4096)]
		private static extern void get_color_Injected(out Color ret);

		// Token: 0x060005F6 RID: 1526
		[MethodImpl(4096)]
		private static extern void set_color_Injected(ref Color value);

		// Token: 0x060005F7 RID: 1527
		[MethodImpl(4096)]
		private static extern void get_matrix_Injected(out Matrix4x4 ret);

		// Token: 0x060005F8 RID: 1528
		[MethodImpl(4096)]
		private static extern void set_matrix_Injected(ref Matrix4x4 value);

		// Token: 0x060005F9 RID: 1529
		[MethodImpl(4096)]
		private static extern void DrawFrustum_Injected(ref Vector3 center, float fov, float maxRange, float minRange, float aspect);
	}
}
