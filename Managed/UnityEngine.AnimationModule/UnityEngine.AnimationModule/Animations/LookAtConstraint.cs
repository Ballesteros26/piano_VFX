using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000066 RID: 102
	[NativeHeader("Modules/Animation/Constraints/LookAtConstraint.h")]
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	public sealed class LookAtConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x00007F59 File Offset: 0x00006159
		private LookAtConstraint()
		{
			LookAtConstraint.Internal_Create(this);
		}

		// Token: 0x060005DC RID: 1500
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] LookAtConstraint self);

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005DD RID: 1501
		// (set) Token: 0x060005DE RID: 1502
		public extern float weight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005DF RID: 1503
		// (set) Token: 0x060005E0 RID: 1504
		public extern float roll
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005E1 RID: 1505
		// (set) Token: 0x060005E2 RID: 1506
		public extern bool constraintActive
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005E3 RID: 1507
		// (set) Token: 0x060005E4 RID: 1508
		public extern bool locked
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00007F6C File Offset: 0x0000616C
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x00007F82 File Offset: 0x00006182
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00007F8C File Offset: 0x0000618C
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x00007FA2 File Offset: 0x000061A2
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

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060005E9 RID: 1513
		// (set) Token: 0x060005EA RID: 1514
		public extern Transform worldUpObject
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060005EB RID: 1515
		// (set) Token: 0x060005EC RID: 1516
		public extern bool useUpObject
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00007FAC File Offset: 0x000061AC
		public int sourceCount
		{
			get
			{
				return LookAtConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x060005EE RID: 1518
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(4096)]
		private static extern int GetSourceCountInternal([NotNull] LookAtConstraint self);

		// Token: 0x060005EF RID: 1519
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetSources([NotNull] List<ConstraintSource> sources);

		// Token: 0x060005F0 RID: 1520 RVA: 0x00007FC4 File Offset: 0x000061C4
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			LookAtConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x060005F1 RID: 1521
		[FreeFunction("ConstraintBindings::SetSources")]
		[MethodImpl(4096)]
		private static extern void SetSourcesInternal([NotNull] LookAtConstraint self, List<ConstraintSource> sources);

		// Token: 0x060005F2 RID: 1522 RVA: 0x00007FED File Offset: 0x000061ED
		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00007FF7 File Offset: 0x000061F7
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x060005F4 RID: 1524
		[NativeName("RemoveSource")]
		[MethodImpl(4096)]
		private extern void RemoveSourceInternal(int index);

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000800C File Offset: 0x0000620C
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00008030 File Offset: 0x00006230
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource constraintSource;
			this.GetSourceInternal_Injected(index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00008047 File Offset: 0x00006247
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000805B File Offset: 0x0000625B
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00008068 File Offset: 0x00006268
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The LookAtConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x060005FA RID: 1530
		[MethodImpl(4096)]
		private extern void get_rotationAtRest_Injected(out Vector3 ret);

		// Token: 0x060005FB RID: 1531
		[MethodImpl(4096)]
		private extern void set_rotationAtRest_Injected(ref Vector3 value);

		// Token: 0x060005FC RID: 1532
		[MethodImpl(4096)]
		private extern void get_rotationOffset_Injected(out Vector3 ret);

		// Token: 0x060005FD RID: 1533
		[MethodImpl(4096)]
		private extern void set_rotationOffset_Injected(ref Vector3 value);

		// Token: 0x060005FE RID: 1534
		[MethodImpl(4096)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		// Token: 0x060005FF RID: 1535
		[MethodImpl(4096)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		// Token: 0x06000600 RID: 1536
		[MethodImpl(4096)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}
