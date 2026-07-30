using System;
using System.Collections.Generic;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002D2 RID: 722
	internal sealed class LabelScopeInfo
	{
		// Token: 0x0600158C RID: 5516 RVA: 0x0004192D File Offset: 0x0003FB2D
		internal LabelScopeInfo(LabelScopeInfo parent, LabelScopeKind kind)
		{
			this.Parent = parent;
			this.Kind = kind;
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x00041944 File Offset: 0x0003FB44
		internal bool CanJumpInto
		{
			get
			{
				LabelScopeKind kind = this.Kind;
				return kind <= LabelScopeKind.Lambda;
			}
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0004195F File Offset: 0x0003FB5F
		internal bool ContainsTarget(LabelTarget target)
		{
			return this._labels != null && this._labels.ContainsKey(target);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00041977 File Offset: 0x0003FB77
		internal bool TryGetLabelInfo(LabelTarget target, out LabelInfo info)
		{
			if (this._labels == null)
			{
				info = null;
				return false;
			}
			return this._labels.TryGetValue(target, out info);
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00041993 File Offset: 0x0003FB93
		internal void AddLabelInfo(LabelTarget target, LabelInfo info)
		{
			if (this._labels == null)
			{
				this._labels = new Dictionary<LabelTarget, LabelInfo>();
			}
			this._labels.Add(target, info);
		}

		// Token: 0x04000A49 RID: 2633
		private Dictionary<LabelTarget, LabelInfo> _labels;

		// Token: 0x04000A4A RID: 2634
		internal readonly LabelScopeKind Kind;

		// Token: 0x04000A4B RID: 2635
		internal readonly LabelScopeInfo Parent;
	}
}
