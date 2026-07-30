using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000065 RID: 101
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/ScaleConstraint.h")]
	public sealed class ScaleConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x00007DE5 File Offset: 0x00005FE5
		private ScaleConstraint()
		{
			ScaleConstraint.Internal_Create(this);
		}

		// Token: 0x060005BA RID: 1466
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] ScaleConstraint self);

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005BB RID: 1467
		// (set) Token: 0x060005BC RID: 1468
		public extern float weight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00007DF8 File Offset: 0x00005FF8
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x00007E0E File Offset: 0x0000600E
		public Vector3 scaleAtRest
		{
			get
			{
				Vector3 vector;
				this.get_scaleAtRest_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_scaleAtRest_Injected(ref value);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00007E18 File Offset: 0x00006018
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x00007E2E File Offset: 0x0000602E
		public Vector3 scaleOffset
		{
			get
			{
				Vector3 vector;
				this.get_scaleOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_scaleOffset_Injected(ref value);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005C1 RID: 1473
		// (set) Token: 0x060005C2 RID: 1474
		public extern Axis scalingAxis
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005C3 RID: 1475
		// (set) Token: 0x060005C4 RID: 1476
		public extern bool constraintActive
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005C5 RID: 1477
		// (set) Token: 0x060005C6 RID: 1478
		public extern bool locked
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00007E38 File Offset: 0x00006038
		public int sourceCount
		{
			get
			{
				return ScaleConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x060005C8 RID: 1480
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(4096)]
		private static extern int GetSourceCountInternal([NotNull] ScaleConstraint self);

		// Token: 0x060005C9 RID: 1481
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetSources([NotNull] List<ConstraintSource> sources);

		// Token: 0x060005CA RID: 1482 RVA: 0x00007E50 File Offset: 0x00006050
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			ScaleConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x060005CB RID: 1483
		[FreeFunction("ConstraintBindings::SetSources")]
		[MethodImpl(4096)]
		private static extern void SetSourcesInternal([NotNull] ScaleConstraint self, List<ConstraintSource> sources);

		// Token: 0x060005CC RID: 1484 RVA: 0x00007E79 File Offset: 0x00006079
		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00007E83 File Offset: 0x00006083
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x060005CE RID: 1486
		[NativeName("RemoveSource")]
		[MethodImpl(4096)]
		private extern void RemoveSourceInternal(int index);

		// Token: 0x060005CF RID: 1487 RVA: 0x00007E98 File Offset: 0x00006098
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00007EBC File Offset: 0x000060BC
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource constraintSource;
			this.GetSourceInternal_Injected(index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00007ED3 File Offset: 0x000060D3
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00007EE7 File Offset: 0x000060E7
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00007EF4 File Offset: 0x000060F4
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The ScaleConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x060005D4 RID: 1492
		[MethodImpl(4096)]
		private extern void get_scaleAtRest_Injected(out Vector3 ret);

		// Token: 0x060005D5 RID: 1493
		[MethodImpl(4096)]
		private extern void set_scaleAtRest_Injected(ref Vector3 value);

		// Token: 0x060005D6 RID: 1494
		[MethodImpl(4096)]
		private extern void get_scaleOffset_Injected(out Vector3 ret);

		// Token: 0x060005D7 RID: 1495
		[MethodImpl(4096)]
		private extern void set_scaleOffset_Injected(ref Vector3 value);

		// Token: 0x060005D8 RID: 1496
		[MethodImpl(4096)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		// Token: 0x060005D9 RID: 1497
		[MethodImpl(4096)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		// Token: 0x060005DA RID: 1498
		[MethodImpl(4096)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}
