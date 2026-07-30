using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000D4 RID: 212
	[NativeHeader("Runtime/Export/Graphics/BillboardRenderer.bindings.h")]
	[NativeHeader("Runtime/Graphics/Billboard/BillboardAsset.h")]
	public sealed class BillboardAsset : Object
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x00009E30 File Offset: 0x00008030
		public BillboardAsset()
		{
			BillboardAsset.Internal_Create(this);
		}

		// Token: 0x06000603 RID: 1539
		[FreeFunction(Name = "BillboardRenderer_Bindings::Internal_Create")]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] BillboardAsset obj);

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000604 RID: 1540
		// (set) Token: 0x06000605 RID: 1541
		public extern float width
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000606 RID: 1542
		// (set) Token: 0x06000607 RID: 1543
		public extern float height
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000608 RID: 1544
		// (set) Token: 0x06000609 RID: 1545
		public extern float bottom
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600060A RID: 1546
		public extern int imageCount
		{
			[NativeMethod("GetNumImages")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600060B RID: 1547
		public extern int vertexCount
		{
			[NativeMethod("GetNumVertices")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600060C RID: 1548
		public extern int indexCount
		{
			[NativeMethod("GetNumIndices")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600060D RID: 1549
		// (set) Token: 0x0600060E RID: 1550
		public extern Material material
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00009E44 File Offset: 0x00008044
		public void GetImageTexCoords(List<Vector4> imageTexCoords)
		{
			bool flag = imageTexCoords == null;
			if (flag)
			{
				throw new ArgumentNullException("imageTexCoords");
			}
			this.GetImageTexCoordsInternal(imageTexCoords);
		}

		// Token: 0x06000610 RID: 1552
		[NativeMethod("GetBillboardDataReadonly().GetImageTexCoords")]
		[MethodImpl(4096)]
		public extern Vector4[] GetImageTexCoords();

		// Token: 0x06000611 RID: 1553
		[FreeFunction(Name = "BillboardRenderer_Bindings::GetImageTexCoordsInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void GetImageTexCoordsInternal(object list);

		// Token: 0x06000612 RID: 1554 RVA: 0x00009E70 File Offset: 0x00008070
		public void SetImageTexCoords(List<Vector4> imageTexCoords)
		{
			bool flag = imageTexCoords == null;
			if (flag)
			{
				throw new ArgumentNullException("imageTexCoords");
			}
			this.SetImageTexCoordsInternalList(imageTexCoords);
		}

		// Token: 0x06000613 RID: 1555
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetImageTexCoords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetImageTexCoords([NotNull] Vector4[] imageTexCoords);

		// Token: 0x06000614 RID: 1556
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetImageTexCoordsInternalList", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void SetImageTexCoordsInternalList(object list);

		// Token: 0x06000615 RID: 1557 RVA: 0x00009E9C File Offset: 0x0000809C
		public void GetVertices(List<Vector2> vertices)
		{
			bool flag = vertices == null;
			if (flag)
			{
				throw new ArgumentNullException("vertices");
			}
			this.GetVerticesInternal(vertices);
		}

		// Token: 0x06000616 RID: 1558
		[NativeMethod("GetBillboardDataReadonly().GetVertices")]
		[MethodImpl(4096)]
		public extern Vector2[] GetVertices();

		// Token: 0x06000617 RID: 1559
		[FreeFunction(Name = "BillboardRenderer_Bindings::GetVerticesInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void GetVerticesInternal(object list);

		// Token: 0x06000618 RID: 1560 RVA: 0x00009EC8 File Offset: 0x000080C8
		public void SetVertices(List<Vector2> vertices)
		{
			bool flag = vertices == null;
			if (flag)
			{
				throw new ArgumentNullException("vertices");
			}
			this.SetVerticesInternalList(vertices);
		}

		// Token: 0x06000619 RID: 1561
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetVertices", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetVertices([NotNull] Vector2[] vertices);

		// Token: 0x0600061A RID: 1562
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetVerticesInternalList", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void SetVerticesInternalList(object list);

		// Token: 0x0600061B RID: 1563 RVA: 0x00009EF4 File Offset: 0x000080F4
		public void GetIndices(List<ushort> indices)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("indices");
			}
			this.GetIndicesInternal(indices);
		}

		// Token: 0x0600061C RID: 1564
		[NativeMethod("GetBillboardDataReadonly().GetIndices")]
		[MethodImpl(4096)]
		public extern ushort[] GetIndices();

		// Token: 0x0600061D RID: 1565
		[FreeFunction(Name = "BillboardRenderer_Bindings::GetIndicesInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void GetIndicesInternal(object list);

		// Token: 0x0600061E RID: 1566 RVA: 0x00009F20 File Offset: 0x00008120
		public void SetIndices(List<ushort> indices)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("indices");
			}
			this.SetIndicesInternalList(indices);
		}

		// Token: 0x0600061F RID: 1567
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetIndices", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetIndices([NotNull] ushort[] indices);

		// Token: 0x06000620 RID: 1568
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetIndicesInternalList", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void SetIndicesInternalList(object list);

		// Token: 0x06000621 RID: 1569
		[FreeFunction(Name = "BillboardRenderer_Bindings::MakeMaterialProperties", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void MakeMaterialProperties(MaterialPropertyBlock properties, Camera camera);
	}
}
