using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000068 RID: 104
	[NativeHeader("Modules/Animation/Constraints/ParentConstraint.h")]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	public sealed class ParentConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x06000610 RID: 1552 RVA: 0x00008203 File Offset: 0x00006403
		private ParentConstraint()
		{
			ParentConstraint.Internal_Create(this);
		}

		// Token: 0x06000611 RID: 1553
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] ParentConstraint self);

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000612 RID: 1554
		// (set) Token: 0x06000613 RID: 1555
		public extern float weight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000614 RID: 1556
		// (set) Token: 0x06000615 RID: 1557
		public extern bool constraintActive
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000616 RID: 1558
		// (set) Token: 0x06000617 RID: 1559
		public extern bool locked
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00008214 File Offset: 0x00006414
		public int sourceCount
		{
			get
			{
				return ParentConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x06000619 RID: 1561
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(4096)]
		private static extern int GetSourceCountInternal([NotNull] ParentConstraint self);

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000822C File Offset: 0x0000642C
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x00008242 File Offset: 0x00006442
		public Vector3 translationAtRest
		{
			get
			{
				Vector3 vector;
				this.get_translationAtRest_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_translationAtRest_Injected(ref value);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0000824C File Offset: 0x0000644C
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x00008262 File Offset: 0x00006462
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

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600061E RID: 1566
		// (set) Token: 0x0600061F RID: 1567
		public extern Vector3[] translationOffsets
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000620 RID: 1568
		// (set) Token: 0x06000621 RID: 1569
		public extern Vector3[] rotationOffsets
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000622 RID: 1570
		// (set) Token: 0x06000623 RID: 1571
		public extern Axis translationAxis
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000624 RID: 1572
		// (set) Token: 0x06000625 RID: 1573
		public extern Axis rotationAxis
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000826C File Offset: 0x0000646C
		public Vector3 GetTranslationOffset(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetTranslationOffsetInternal(index);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0000828D File Offset: 0x0000648D
		public void SetTranslationOffset(int index, Vector3 value)
		{
			this.ValidateSourceIndex(index);
			this.SetTranslationOffsetInternal(index, value);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000082A4 File Offset: 0x000064A4
		[NativeName("GetTranslationOffset")]
		private Vector3 GetTranslationOffsetInternal(int index)
		{
			Vector3 vector;
			this.GetTranslationOffsetInternal_Injected(index, out vector);
			return vector;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000082BB File Offset: 0x000064BB
		[NativeName("SetTranslationOffset")]
		private void SetTranslationOffsetInternal(int index, Vector3 value)
		{
			this.SetTranslationOffsetInternal_Injected(index, ref value);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000082C8 File Offset: 0x000064C8
		public Vector3 GetRotationOffset(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetRotationOffsetInternal(index);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000082E9 File Offset: 0x000064E9
		public void SetRotationOffset(int index, Vector3 value)
		{
			this.ValidateSourceIndex(index);
			this.SetRotationOffsetInternal(index, value);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00008300 File Offset: 0x00006500
		[NativeName("GetRotationOffset")]
		private Vector3 GetRotationOffsetInternal(int index)
		{
			Vector3 vector;
			this.GetRotationOffsetInternal_Injected(index, out vector);
			return vector;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00008317 File Offset: 0x00006517
		[NativeName("SetRotationOffset")]
		private void SetRotationOffsetInternal(int index, Vector3 value)
		{
			this.SetRotationOffsetInternal_Injected(index, ref value);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00008324 File Offset: 0x00006524
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The ParentConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x0600062F RID: 1583
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetSources([NotNull] List<ConstraintSource> sources);

		// Token: 0x06000630 RID: 1584 RVA: 0x0000838C File Offset: 0x0000658C
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			ParentConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x06000631 RID: 1585
		[FreeFunction("ConstraintBindings::SetSources")]
		[MethodImpl(4096)]
		private static extern void SetSourcesInternal([NotNull] ParentConstraint self, List<ConstraintSource> sources);

		// Token: 0x06000632 RID: 1586 RVA: 0x000083B5 File Offset: 0x000065B5
		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000083BF File Offset: 0x000065BF
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x06000634 RID: 1588
		[NativeName("RemoveSource")]
		[MethodImpl(4096)]
		private extern void RemoveSourceInternal(int index);

		// Token: 0x06000635 RID: 1589 RVA: 0x000083D4 File Offset: 0x000065D4
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000083F8 File Offset: 0x000065F8
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource constraintSource;
			this.GetSourceInternal_Injected(index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000840F File Offset: 0x0000660F
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00008423 File Offset: 0x00006623
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		// Token: 0x06000639 RID: 1593
		[MethodImpl(4096)]
		private extern void get_translationAtRest_Injected(out Vector3 ret);

		// Token: 0x0600063A RID: 1594
		[MethodImpl(4096)]
		private extern void set_translationAtRest_Injected(ref Vector3 value);

		// Token: 0x0600063B RID: 1595
		[MethodImpl(4096)]
		private extern void get_rotationAtRest_Injected(out Vector3 ret);

		// Token: 0x0600063C RID: 1596
		[MethodImpl(4096)]
		private extern void set_rotationAtRest_Injected(ref Vector3 value);

		// Token: 0x0600063D RID: 1597
		[MethodImpl(4096)]
		private extern void GetTranslationOffsetInternal_Injected(int index, out Vector3 ret);

		// Token: 0x0600063E RID: 1598
		[MethodImpl(4096)]
		private extern void SetTranslationOffsetInternal_Injected(int index, ref Vector3 value);

		// Token: 0x0600063F RID: 1599
		[MethodImpl(4096)]
		private extern void GetRotationOffsetInternal_Injected(int index, out Vector3 ret);

		// Token: 0x06000640 RID: 1600
		[MethodImpl(4096)]
		private extern void SetRotationOffsetInternal_Injected(int index, ref Vector3 value);

		// Token: 0x06000641 RID: 1601
		[MethodImpl(4096)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		// Token: 0x06000642 RID: 1602
		[MethodImpl(4096)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		// Token: 0x06000643 RID: 1603
		[MethodImpl(4096)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}
