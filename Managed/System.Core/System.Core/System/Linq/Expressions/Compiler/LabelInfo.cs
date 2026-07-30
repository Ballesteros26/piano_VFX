using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection.Emit;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002CF RID: 719
	internal sealed class LabelInfo
	{
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x000414B9 File Offset: 0x0003F6B9
		internal Label Label
		{
			get
			{
				this.EnsureLabelAndValue();
				return this._label;
			}
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x000414C7 File Offset: 0x0003F6C7
		internal LabelInfo(ILGenerator il, LabelTarget node, bool canReturn)
		{
			this._ilg = il;
			this._node = node;
			this._canReturn = canReturn;
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00041505 File Offset: 0x0003F705
		internal bool CanReturn
		{
			get
			{
				return this._canReturn;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x0004150D File Offset: 0x0003F70D
		internal bool CanBranch
		{
			get
			{
				return this._opCode != OpCodes.Leave;
			}
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0004151F File Offset: 0x0003F71F
		internal void Reference(LabelScopeInfo block)
		{
			this._references.Add(block);
			if (this._definitions.Count > 0)
			{
				this.ValidateJump(block);
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00041544 File Offset: 0x0003F744
		internal void Define(LabelScopeInfo block)
		{
			for (LabelScopeInfo labelScopeInfo = block; labelScopeInfo != null; labelScopeInfo = labelScopeInfo.Parent)
			{
				if (labelScopeInfo.ContainsTarget(this._node))
				{
					throw Error.LabelTargetAlreadyDefined(this._node.Name);
				}
			}
			this._definitions.Add(block);
			block.AddLabelInfo(this._node, this);
			if (this._definitions.Count == 1)
			{
				using (List<LabelScopeInfo>.Enumerator enumerator = this._references.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						LabelScopeInfo labelScopeInfo2 = enumerator.Current;
						this.ValidateJump(labelScopeInfo2);
					}
					return;
				}
			}
			if (this._acrossBlockJump)
			{
				throw Error.AmbiguousJump(this._node.Name);
			}
			this._labelDefined = false;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0004160C File Offset: 0x0003F80C
		private void ValidateJump(LabelScopeInfo reference)
		{
			this._opCode = (this._canReturn ? OpCodes.Ret : OpCodes.Br);
			for (LabelScopeInfo labelScopeInfo = reference; labelScopeInfo != null; labelScopeInfo = labelScopeInfo.Parent)
			{
				if (this._definitions.Contains(labelScopeInfo))
				{
					return;
				}
				if (labelScopeInfo.Kind == LabelScopeKind.Finally || labelScopeInfo.Kind == LabelScopeKind.Filter)
				{
					break;
				}
				if (labelScopeInfo.Kind == LabelScopeKind.Try || labelScopeInfo.Kind == LabelScopeKind.Catch)
				{
					this._opCode = OpCodes.Leave;
				}
			}
			this._acrossBlockJump = true;
			if (this._node != null && this._node.Type != typeof(void))
			{
				throw Error.NonLocalJumpWithValue(this._node.Name);
			}
			if (this._definitions.Count > 1)
			{
				throw Error.AmbiguousJump(this._node.Name);
			}
			LabelScopeInfo labelScopeInfo2 = this._definitions.First<LabelScopeInfo>();
			LabelScopeInfo labelScopeInfo3 = Helpers.CommonNode<LabelScopeInfo>(labelScopeInfo2, reference, (LabelScopeInfo b) => b.Parent);
			this._opCode = (this._canReturn ? OpCodes.Ret : OpCodes.Br);
			for (LabelScopeInfo labelScopeInfo4 = reference; labelScopeInfo4 != labelScopeInfo3; labelScopeInfo4 = labelScopeInfo4.Parent)
			{
				if (labelScopeInfo4.Kind == LabelScopeKind.Finally)
				{
					throw Error.ControlCannotLeaveFinally();
				}
				if (labelScopeInfo4.Kind == LabelScopeKind.Filter)
				{
					throw Error.ControlCannotLeaveFilterTest();
				}
				if (labelScopeInfo4.Kind == LabelScopeKind.Try || labelScopeInfo4.Kind == LabelScopeKind.Catch)
				{
					this._opCode = OpCodes.Leave;
				}
			}
			LabelScopeInfo labelScopeInfo5 = labelScopeInfo2;
			while (labelScopeInfo5 != labelScopeInfo3)
			{
				if (!labelScopeInfo5.CanJumpInto)
				{
					if (labelScopeInfo5.Kind == LabelScopeKind.Expression)
					{
						throw Error.ControlCannotEnterExpression();
					}
					throw Error.ControlCannotEnterTry();
				}
				else
				{
					labelScopeInfo5 = labelScopeInfo5.Parent;
				}
			}
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x000417A3 File Offset: 0x0003F9A3
		internal void ValidateFinish()
		{
			if (this._references.Count > 0 && this._definitions.Count == 0)
			{
				throw Error.LabelTargetUndefined(this._node.Name);
			}
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x000417D4 File Offset: 0x0003F9D4
		internal void EmitJump()
		{
			if (this._opCode == OpCodes.Ret)
			{
				this._ilg.Emit(OpCodes.Ret);
				return;
			}
			this.StoreValue();
			this._ilg.Emit(this._opCode, this.Label);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00041821 File Offset: 0x0003FA21
		private void StoreValue()
		{
			this.EnsureLabelAndValue();
			if (this._value != null)
			{
				this._ilg.Emit(OpCodes.Stloc, this._value);
			}
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00041847 File Offset: 0x0003FA47
		internal void Mark()
		{
			if (this._canReturn)
			{
				if (!this._labelDefined)
				{
					return;
				}
				this._ilg.Emit(OpCodes.Ret);
			}
			else
			{
				this.StoreValue();
			}
			this.MarkWithEmptyStack();
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00041878 File Offset: 0x0003FA78
		internal void MarkWithEmptyStack()
		{
			this._ilg.MarkLabel(this.Label);
			if (this._value != null)
			{
				this._ilg.Emit(OpCodes.Ldloc, this._value);
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x000418AC File Offset: 0x0003FAAC
		private void EnsureLabelAndValue()
		{
			if (!this._labelDefined)
			{
				this._labelDefined = true;
				this._label = this._ilg.DefineLabel();
				if (this._node != null && this._node.Type != typeof(void))
				{
					this._value = this._ilg.DeclareLocal(this._node.Type);
				}
			}
		}

		// Token: 0x04000A33 RID: 2611
		private readonly LabelTarget _node;

		// Token: 0x04000A34 RID: 2612
		private Label _label;

		// Token: 0x04000A35 RID: 2613
		private bool _labelDefined;

		// Token: 0x04000A36 RID: 2614
		private LocalBuilder _value;

		// Token: 0x04000A37 RID: 2615
		private readonly HashSet<LabelScopeInfo> _definitions = new HashSet<LabelScopeInfo>();

		// Token: 0x04000A38 RID: 2616
		private readonly List<LabelScopeInfo> _references = new List<LabelScopeInfo>();

		// Token: 0x04000A39 RID: 2617
		private readonly bool _canReturn;

		// Token: 0x04000A3A RID: 2618
		private bool _acrossBlockJump;

		// Token: 0x04000A3B RID: 2619
		private OpCode _opCode = OpCodes.Leave;

		// Token: 0x04000A3C RID: 2620
		private readonly ILGenerator _ilg;
	}
}
