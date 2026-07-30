using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000063 RID: 99
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[NativeHeader("Modules/Animation/Constraints/PositionConstraint.h")]
	[RequireComponent(typeof(Transform))]
	[UsedByNativeCode]
	public sealed class PositionConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x00007AFE File Offset: 0x00005CFE
		private PositionConstraint()
		{
			PositionConstraint.Internal_Create(this);
		}

		// Token: 0x06000576 RID: 1398
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] PositionConstraint self);

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000577 RID: 1399
		// (set) Token: 0x06000578 RID: 1400
		public extern float weight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00007B10 File Offset: 0x00005D10
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x00007B26 File Offset: 0x00005D26
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

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00007B30 File Offset: 0x00005D30
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x00007B46 File Offset: 0x00005D46
		public Vector3 translationOffset
		{
			get
			{
				Vector3 vector;
				this.get_translationOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_translationOffset_Injected(ref value);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600057D RID: 1405
		// (set) Token: 0x0600057E RID: 1406
		public extern Axis translationAxis
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600057F RID: 1407
		// (set) Token: 0x06000580 RID: 1408
		public extern bool constraintActive
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000581 RID: 1409
		// (set) Token: 0x06000582 RID: 1410
		public extern bool locked
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00007B50 File Offset: 0x00005D50
		public int sourceCount
		{
			get
			{
				return PositionConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x06000584 RID: 1412
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(4096)]
		private static extern int GetSourceCountInternal([NotNull] PositionConstraint self);

		// Token: 0x06000585 RID: 1413
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetSources([NotNull] List<ConstraintSource> sources);

		// Token: 0x06000586 RID: 1414 RVA: 0x00007B68 File Offset: 0x00005D68
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			PositionConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x06000587 RID: 1415
		[FreeFunction("ConstraintBindings::SetSources")]
		[MethodImpl(4096)]
		private static extern void SetSourcesInternal([NotNull] PositionConstraint self, List<ConstraintSource> sources);

		// Token: 0x06000588 RID: 1416 RVA: 0x00007B91 File Offset: 0x00005D91
		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00007B9B File Offset: 0x00005D9B
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x0600058A RID: 1418
		[NativeName("RemoveSource")]
		[MethodImpl(4096)]
		private extern void RemoveSourceInternal(int index);

		// Token: 0x0600058B RID: 1419 RVA: 0x00007BB0 File Offset: 0x00005DB0
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00007BD4 File Offset: 0x00005DD4
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource constraintSource;
			this.GetSourceInternal_Injected(index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00007BEB File Offset: 0x00005DEB
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00007BFF File Offset: 0x00005DFF
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00007C0C File Offset: 0x00005E0C
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The PositionConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x06000590 RID: 1424
		[MethodImpl(4096)]
		private extern void get_translationAtRest_Injected(out Vector3 ret);

		// Token: 0x06000591 RID: 1425
		[MethodImpl(4096)]
		private extern void set_translationAtRest_Injected(ref Vector3 value);

		// Token: 0x06000592 RID: 1426
		[MethodImpl(4096)]
		private extern void get_translationOffset_Injected(out Vector3 ret);

		// Token: 0x06000593 RID: 1427
		[MethodImpl(4096)]
		private extern void set_translationOffset_Injected(ref Vector3 value);

		// Token: 0x06000594 RID: 1428
		[MethodImpl(4096)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		// Token: 0x06000595 RID: 1429
		[MethodImpl(4096)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		// Token: 0x06000596 RID: 1430
		[MethodImpl(4096)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}
