using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000CD RID: 205
	[NativeHeader("Modules/IMGUI/GUIStyle.h")]
	[UsedByNativeCode]
	[Serializable]
	[StructLayout(0)]
	public class RectOffset : IFormattable
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x0000963D File Offset: 0x0000783D
		public RectOffset()
		{
			this.m_Ptr = RectOffset.InternalCreate();
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00009652 File Offset: 0x00007852
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		internal RectOffset(object sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000966C File Offset: 0x0000786C
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_SourceStyle == null;
				if (flag)
				{
					this.Destroy();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000096AC File Offset: 0x000078AC
		public RectOffset(int left, int right, int top, int bottom)
		{
			this.m_Ptr = RectOffset.InternalCreate();
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000096E4 File Offset: 0x000078E4
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00009708 File Offset: 0x00007908
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000972C File Offset: 0x0000792C
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return UnityString.Format("RectOffset (l:{0} r:{1} t:{2} b:{3})", new object[]
			{
				this.left.ToString(format, formatProvider),
				this.right.ToString(format, formatProvider),
				this.top.ToString(format, formatProvider),
				this.bottom.ToString(format, formatProvider)
			});
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000979C File Offset: 0x0000799C
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				RectOffset.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000598 RID: 1432
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		private static extern IntPtr InternalCreate();

		// Token: 0x06000599 RID: 1433
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		private static extern void InternalDestroy(IntPtr ptr);

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600059A RID: 1434
		// (set) Token: 0x0600059B RID: 1435
		[NativeProperty("left", false, TargetType.Field)]
		public extern int left
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600059C RID: 1436
		// (set) Token: 0x0600059D RID: 1437
		[NativeProperty("right", false, TargetType.Field)]
		public extern int right
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600059E RID: 1438
		// (set) Token: 0x0600059F RID: 1439
		[NativeProperty("top", false, TargetType.Field)]
		public extern int top
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005A0 RID: 1440
		// (set) Token: 0x060005A1 RID: 1441
		[NativeProperty("bottom", false, TargetType.Field)]
		public extern int bottom
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005A2 RID: 1442
		public extern int horizontal
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005A3 RID: 1443
		public extern int vertical
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000097D8 File Offset: 0x000079D8
		public Rect Add(Rect rect)
		{
			Rect rect2;
			this.Add_Injected(ref rect, out rect2);
			return rect2;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000097F0 File Offset: 0x000079F0
		public Rect Remove(Rect rect)
		{
			Rect rect2;
			this.Remove_Injected(ref rect, out rect2);
			return rect2;
		}

		// Token: 0x060005A6 RID: 1446
		[MethodImpl(4096)]
		private extern void Add_Injected(ref Rect rect, out Rect ret);

		// Token: 0x060005A7 RID: 1447
		[MethodImpl(4096)]
		private extern void Remove_Injected(ref Rect rect, out Rect ret);

		// Token: 0x04000250 RID: 592
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000251 RID: 593
		private readonly object m_SourceStyle;
	}
}
