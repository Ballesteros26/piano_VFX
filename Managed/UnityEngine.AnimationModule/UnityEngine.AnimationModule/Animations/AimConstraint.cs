using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000045 RID: 69
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[RequireComponent(typeof(Transform))]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/Constraints/AimConstraint.h")]
	public sealed class AimConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x00004654 File Offset: 0x00002854
		private AimConstraint()
		{
			AimConstraint.Internal_Create(this);
		}

		// Token: 0x060002A7 RID: 679
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] AimConstraint self);

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002A8 RID: 680
		// (set) Token: 0x060002A9 RID: 681
		public extern float weight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002AA RID: 682
		// (set) Token: 0x060002AB RID: 683
		public extern bool constraintActive
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002AC RID: 684
		// (set) Token: 0x060002AD RID: 685
		public extern bool locked
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00004668 File Offset: 0x00002868
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000467E File Offset: 0x0000287E
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00004688 File Offset: 0x00002888
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000469E File Offset: 0x0000289E
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002B2 RID: 690
		// (set) Token: 0x060002B3 RID: 691
		public extern Axis rotationAxis
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000046A8 File Offset: 0x000028A8
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x000046BE File Offset: 0x000028BE
		public Vector3 aimVector
		{
			get
			{
				Vector3 vector;
				this.get_aimVector_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_aimVector_Injected(ref value);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x000046C8 File Offset: 0x000028C8
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x000046DE File Offset: 0x000028DE
		public Vector3 upVector
		{
			get
			{
				Vector3 vector;
				this.get_upVector_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_upVector_Injected(ref value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000046E8 File Offset: 0x000028E8
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x000046FE File Offset: 0x000028FE
		public Vector3 worldUpVector
		{
			get
			{
				Vector3 vector;
				this.get_worldUpVector_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_worldUpVector_Injected(ref value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002BA RID: 698
		// (set) Token: 0x060002BB RID: 699
		public extern Transform worldUpObject
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002BC RID: 700
		// (set) Token: 0x060002BD RID: 701
		public extern AimConstraint.WorldUpType worldUpType
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00004708 File Offset: 0x00002908
		public int sourceCount
		{
			get
			{
				return AimConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x060002BF RID: 703
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(4096)]
		private static extern int GetSourceCountInternal([NotNull] AimConstraint self);

		// Token: 0x060002C0 RID: 704
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetSources([NotNull] List<ConstraintSource> sources);

		// Token: 0x060002C1 RID: 705 RVA: 0x00004720 File Offset: 0x00002920
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			AimConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x060002C2 RID: 706
		[FreeFunction("ConstraintBindings::SetSources")]
		[MethodImpl(4096)]
		private static extern void SetSourcesInternal([NotNull] AimConstraint self, List<ConstraintSource> sources);

		// Token: 0x060002C3 RID: 707 RVA: 0x00004749 File Offset: 0x00002949
		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00004753 File Offset: 0x00002953
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x060002C5 RID: 709
		[NativeName("RemoveSource")]
		[MethodImpl(4096)]
		private extern void RemoveSourceInternal(int index);

		// Token: 0x060002C6 RID: 710 RVA: 0x00004768 File Offset: 0x00002968
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000478C File Offset: 0x0000298C
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource constraintSource;
			this.GetSourceInternal_Injected(index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000047A3 File Offset: 0x000029A3
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000047B7 File Offset: 0x000029B7
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000047C4 File Offset: 0x000029C4
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The AimConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x060002CB RID: 715
		[MethodImpl(4096)]
		private extern void get_rotationAtRest_Injected(out Vector3 ret);

		// Token: 0x060002CC RID: 716
		[MethodImpl(4096)]
		private extern void set_rotationAtRest_Injected(ref Vector3 value);

		// Token: 0x060002CD RID: 717
		[MethodImpl(4096)]
		private extern void get_rotationOffset_Injected(out Vector3 ret);

		// Token: 0x060002CE RID: 718
		[MethodImpl(4096)]
		private extern void set_rotationOffset_Injected(ref Vector3 value);

		// Token: 0x060002CF RID: 719
		[MethodImpl(4096)]
		private extern void get_aimVector_Injected(out Vector3 ret);

		// Token: 0x060002D0 RID: 720
		[MethodImpl(4096)]
		private extern void set_aimVector_Injected(ref Vector3 value);

		// Token: 0x060002D1 RID: 721
		[MethodImpl(4096)]
		private extern void get_upVector_Injected(out Vector3 ret);

		// Token: 0x060002D2 RID: 722
		[MethodImpl(4096)]
		private extern void set_upVector_Injected(ref Vector3 value);

		// Token: 0x060002D3 RID: 723
		[MethodImpl(4096)]
		private extern void get_worldUpVector_Injected(out Vector3 ret);

		// Token: 0x060002D4 RID: 724
		[MethodImpl(4096)]
		private extern void set_worldUpVector_Injected(ref Vector3 value);

		// Token: 0x060002D5 RID: 725
		[MethodImpl(4096)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		// Token: 0x060002D6 RID: 726
		[MethodImpl(4096)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		// Token: 0x060002D7 RID: 727
		[MethodImpl(4096)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);

		// Token: 0x02000046 RID: 70
		public enum WorldUpType
		{
			// Token: 0x04000140 RID: 320
			SceneUp,
			// Token: 0x04000141 RID: 321
			ObjectUp,
			// Token: 0x04000142 RID: 322
			ObjectRotationUp,
			// Token: 0x04000143 RID: 323
			Vector,
			// Token: 0x04000144 RID: 324
			None
		}
	}
}
