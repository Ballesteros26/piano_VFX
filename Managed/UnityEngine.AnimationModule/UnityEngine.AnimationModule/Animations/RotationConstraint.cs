using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000064 RID: 100
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[NativeHeader("Modules/Animation/Constraints/RotationConstraint.h")]
	[RequireComponent(typeof(Transform))]
	[UsedByNativeCode]
	public sealed class RotationConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x00007C71 File Offset: 0x00005E71
		private RotationConstraint()
		{
			RotationConstraint.Internal_Create(this);
		}

		// Token: 0x06000598 RID: 1432
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] RotationConstraint self);

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000599 RID: 1433
		// (set) Token: 0x0600059A RID: 1434
		public extern float weight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00007C84 File Offset: 0x00005E84
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x00007C9A File Offset: 0x00005E9A
		public Vector3 rotationAtRest
		{
			get
			{
				Vector3 vector;
				this.get_rotationAtRest_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_rotationAtRest_Injected(ref value);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00007CA4 File Offset: 0x00005EA4
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x00007CBA File Offset: 0x00005EBA
		public Vector3 rotationOffset
		{
			get
			{
				Vector3 vector;
				this.get_rotationOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_rotationOffset_Injected(ref value);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600059F RID: 1439
		// (set) Token: 0x060005A0 RID: 1440
		public extern Axis rotationAxis
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005A1 RID: 1441
		// (set) Token: 0x060005A2 RID: 1442
		public extern bool constraintActive
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005A3 RID: 1443
		// (set) Token: 0x060005A4 RID: 1444
		public extern bool locked
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public int sourceCount
		{
			get
			{
				return RotationConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x060005A6 RID: 1446
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(4096)]
		private static extern int GetSourceCountInternal([NotNull] RotationConstraint self);

		// Token: 0x060005A7 RID: 1447
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetSources([NotNull] List<ConstraintSource> sources);

		// Token: 0x060005A8 RID: 1448 RVA: 0x00007CDC File Offset: 0x00005EDC
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			RotationConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x060005A9 RID: 1449
		[FreeFunction("ConstraintBindings::SetSources")]
		[MethodImpl(4096)]
		private static extern void SetSourcesInternal([NotNull] RotationConstraint self, List<ConstraintSource> sources);

		// Token: 0x060005AA RID: 1450 RVA: 0x00007D05 File Offset: 0x00005F05
		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00007D0F File Offset: 0x00005F0F
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x060005AC RID: 1452
		[NativeName("RemoveSource")]
		[MethodImpl(4096)]
		private extern void RemoveSourceInternal(int index);

		// Token: 0x060005AD RID: 1453 RVA: 0x00007D24 File Offset: 0x00005F24
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00007D48 File Offset: 0x00005F48
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource constraintSource;
			this.GetSourceInternal_Injected(index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00007D5F File Offset: 0x00005F5F
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00007D73 File Offset: 0x00005F73
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00007D80 File Offset: 0x00005F80
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The RotationConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x060005B2 RID: 1458
		[MethodImpl(4096)]
		private extern void get_rotationAtRest_Injected(out Vector3 ret);

		// Token: 0x060005B3 RID: 1459
		[MethodImpl(4096)]
		private extern void set_rotationAtRest_Injected(ref Vector3 value);

		// Token: 0x060005B4 RID: 1460
		[MethodImpl(4096)]
		private extern void get_rotationOffset_Injected(out Vector3 ret);

		// Token: 0x060005B5 RID: 1461
		[MethodImpl(4096)]
		private extern void set_rotationOffset_Injected(ref Vector3 value);

		// Token: 0x060005B6 RID: 1462
		[MethodImpl(4096)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		// Token: 0x060005B7 RID: 1463
		[MethodImpl(4096)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		// Token: 0x060005B8 RID: 1464
		[MethodImpl(4096)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}
