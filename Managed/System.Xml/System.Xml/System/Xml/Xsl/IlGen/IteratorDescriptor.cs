using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.XPath;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x0200065E RID: 1630
	internal class IteratorDescriptor
	{
		// Token: 0x060041A5 RID: 16805 RVA: 0x0015F1FC File Offset: 0x0015D3FC
		public IteratorDescriptor(GenerateHelper helper)
		{
			this.Init(null, helper);
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x0015F20C File Offset: 0x0015D40C
		public IteratorDescriptor(IteratorDescriptor iterParent)
		{
			this.Init(iterParent, iterParent.helper);
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x0015F221 File Offset: 0x0015D421
		private void Init(IteratorDescriptor iterParent, GenerateHelper helper)
		{
			this.helper = helper;
			this.iterParent = iterParent;
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x060041A8 RID: 16808 RVA: 0x0015F231 File Offset: 0x0015D431
		public IteratorDescriptor ParentIterator
		{
			get
			{
				return this.iterParent;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x060041A9 RID: 16809 RVA: 0x0015F239 File Offset: 0x0015D439
		public bool HasLabelNext
		{
			get
			{
				return this.hasNext;
			}
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x0015F241 File Offset: 0x0015D441
		public Label GetLabelNext()
		{
			return this.lblNext;
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x0015F249 File Offset: 0x0015D449
		public void SetIterator(Label lblNext, StorageDescriptor storage)
		{
			this.lblNext = lblNext;
			this.hasNext = true;
			this.storage = storage;
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x0015F260 File Offset: 0x0015D460
		public void SetIterator(IteratorDescriptor iterInfo)
		{
			if (iterInfo.HasLabelNext)
			{
				this.lblNext = iterInfo.GetLabelNext();
				this.hasNext = true;
			}
			this.storage = iterInfo.Storage;
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x0015F289 File Offset: 0x0015D489
		public void LoopToEnd(Label lblOnEnd)
		{
			if (this.hasNext)
			{
				this.helper.BranchAndMark(this.lblNext, lblOnEnd);
				this.hasNext = false;
			}
			this.storage = StorageDescriptor.None();
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x060041AE RID: 16814 RVA: 0x0015F2B7 File Offset: 0x0015D4B7
		// (set) Token: 0x060041AF RID: 16815 RVA: 0x0015F2BF File Offset: 0x0015D4BF
		public LocalBuilder LocalPosition
		{
			get
			{
				return this.locPos;
			}
			set
			{
				this.locPos = value;
			}
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x0015F2C8 File Offset: 0x0015D4C8
		public void CacheCount()
		{
			this.PushValue();
			this.helper.CallCacheCount(this.storage.ItemStorageType);
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x0015F2E8 File Offset: 0x0015D4E8
		public void EnsureNoCache()
		{
			if (this.storage.IsCached)
			{
				if (!this.HasLabelNext)
				{
					this.EnsureStack();
					this.helper.LoadInteger(0);
					this.helper.CallCacheItem(this.storage.ItemStorageType);
					this.storage = StorageDescriptor.Stack(this.storage.ItemStorageType, false);
					return;
				}
				LocalBuilder localBuilder = this.helper.DeclareLocal("$$$idx", typeof(int));
				this.EnsureNoStack("$$$cache");
				this.helper.LoadInteger(-1);
				this.helper.Emit(OpCodes.Stloc, localBuilder);
				Label label = this.helper.DefineLabel();
				this.helper.MarkLabel(label);
				this.helper.Emit(OpCodes.Ldloc, localBuilder);
				this.helper.LoadInteger(1);
				this.helper.Emit(OpCodes.Add);
				this.helper.Emit(OpCodes.Stloc, localBuilder);
				this.helper.Emit(OpCodes.Ldloc, localBuilder);
				this.CacheCount();
				this.helper.Emit(OpCodes.Bge, this.GetLabelNext());
				this.PushValue();
				this.helper.Emit(OpCodes.Ldloc, localBuilder);
				this.helper.CallCacheItem(this.storage.ItemStorageType);
				this.SetIterator(label, StorageDescriptor.Stack(this.storage.ItemStorageType, false));
			}
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x0015F458 File Offset: 0x0015D658
		public void SetBranching(BranchingContext brctxt, Label lblBranch)
		{
			this.brctxt = brctxt;
			this.lblBranch = lblBranch;
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x060041B3 RID: 16819 RVA: 0x0015F468 File Offset: 0x0015D668
		public bool IsBranching
		{
			get
			{
				return this.brctxt > BranchingContext.None;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x060041B4 RID: 16820 RVA: 0x0015F473 File Offset: 0x0015D673
		public Label LabelBranch
		{
			get
			{
				return this.lblBranch;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x060041B5 RID: 16821 RVA: 0x0015F47B File Offset: 0x0015D67B
		public BranchingContext CurrentBranchingContext
		{
			get
			{
				return this.brctxt;
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x060041B6 RID: 16822 RVA: 0x0015F483 File Offset: 0x0015D683
		// (set) Token: 0x060041B7 RID: 16823 RVA: 0x0015F48B File Offset: 0x0015D68B
		public StorageDescriptor Storage
		{
			get
			{
				return this.storage;
			}
			set
			{
				this.storage = value;
			}
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x0015F494 File Offset: 0x0015D694
		public void PushValue()
		{
			switch (this.storage.Location)
			{
			case ItemLocation.Stack:
				this.helper.Emit(OpCodes.Dup);
				return;
			case ItemLocation.Parameter:
				this.helper.LoadParameter(this.storage.ParameterLocation);
				return;
			case ItemLocation.Local:
				this.helper.Emit(OpCodes.Ldloc, this.storage.LocalLocation);
				return;
			case ItemLocation.Current:
				this.helper.Emit(OpCodes.Ldloca, this.storage.CurrentLocation);
				this.helper.Call(this.storage.CurrentLocation.LocalType.GetMethod("get_Current"));
				return;
			default:
				return;
			}
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x0015F54C File Offset: 0x0015D74C
		public void EnsureStack()
		{
			switch (this.storage.Location)
			{
			case ItemLocation.Stack:
				return;
			case ItemLocation.Parameter:
			case ItemLocation.Local:
			case ItemLocation.Current:
				this.PushValue();
				break;
			case ItemLocation.Global:
				this.helper.LoadQueryRuntime();
				this.helper.Call(this.storage.GlobalLocation);
				break;
			}
			this.storage = this.storage.ToStack();
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x0015F5BE File Offset: 0x0015D7BE
		public void EnsureNoStack(string locName)
		{
			if (this.storage.Location == ItemLocation.Stack)
			{
				this.EnsureLocal(locName);
			}
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x0015F5D8 File Offset: 0x0015D7D8
		public void EnsureLocal(string locName)
		{
			if (this.storage.Location != ItemLocation.Local)
			{
				if (this.storage.IsCached)
				{
					this.EnsureLocal(this.helper.DeclareLocal(locName, typeof(IList<>).MakeGenericType(new Type[] { this.storage.ItemStorageType })));
					return;
				}
				this.EnsureLocal(this.helper.DeclareLocal(locName, this.storage.ItemStorageType));
			}
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x0015F653 File Offset: 0x0015D853
		public void EnsureLocal(LocalBuilder bldr)
		{
			if (this.storage.LocalLocation != bldr)
			{
				this.EnsureStack();
				this.helper.Emit(OpCodes.Stloc, bldr);
				this.storage = this.storage.ToLocal(bldr);
			}
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x0015F68C File Offset: 0x0015D88C
		public void DiscardStack()
		{
			if (this.storage.Location == ItemLocation.Stack)
			{
				this.helper.Emit(OpCodes.Pop);
				this.storage = StorageDescriptor.None();
			}
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x0015F6B7 File Offset: 0x0015D8B7
		public void EnsureStackNoCache()
		{
			this.EnsureNoCache();
			this.EnsureStack();
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x0015F6C5 File Offset: 0x0015D8C5
		public void EnsureNoStackNoCache(string locName)
		{
			this.EnsureNoCache();
			this.EnsureNoStack(locName);
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x0015F6D4 File Offset: 0x0015D8D4
		public void EnsureLocalNoCache(string locName)
		{
			this.EnsureNoCache();
			this.EnsureLocal(locName);
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x0015F6E3 File Offset: 0x0015D8E3
		public void EnsureLocalNoCache(LocalBuilder bldr)
		{
			this.EnsureNoCache();
			this.EnsureLocal(bldr);
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x0015F6F4 File Offset: 0x0015D8F4
		public void EnsureItemStorageType(XmlQueryType xmlType, Type storageTypeDest)
		{
			if (!(this.storage.ItemStorageType == storageTypeDest))
			{
				if (this.storage.IsCached)
				{
					if (this.storage.ItemStorageType == typeof(XPathNavigator))
					{
						this.EnsureStack();
						this.helper.Call(XmlILMethods.NavsToItems);
						goto IL_014D;
					}
					if (storageTypeDest == typeof(XPathNavigator))
					{
						this.EnsureStack();
						this.helper.Call(XmlILMethods.ItemsToNavs);
						goto IL_014D;
					}
				}
				this.EnsureStackNoCache();
				if (this.storage.ItemStorageType == typeof(XPathItem))
				{
					if (storageTypeDest == typeof(XPathNavigator))
					{
						this.helper.Emit(OpCodes.Castclass, typeof(XPathNavigator));
					}
					else
					{
						this.helper.CallValueAs(storageTypeDest);
					}
				}
				else if (!(this.storage.ItemStorageType == typeof(XPathNavigator)))
				{
					this.helper.LoadInteger(this.helper.StaticData.DeclareXmlType(xmlType));
					this.helper.LoadQueryRuntime();
					this.helper.Call(XmlILMethods.StorageMethods[this.storage.ItemStorageType].ToAtomicValue);
				}
			}
			IL_014D:
			this.storage = this.storage.ToStorageType(storageTypeDest);
		}

		// Token: 0x04002A08 RID: 10760
		private GenerateHelper helper;

		// Token: 0x04002A09 RID: 10761
		private IteratorDescriptor iterParent;

		// Token: 0x04002A0A RID: 10762
		private Label lblNext;

		// Token: 0x04002A0B RID: 10763
		private bool hasNext;

		// Token: 0x04002A0C RID: 10764
		private LocalBuilder locPos;

		// Token: 0x04002A0D RID: 10765
		private BranchingContext brctxt;

		// Token: 0x04002A0E RID: 10766
		private Label lblBranch;

		// Token: 0x04002A0F RID: 10767
		private StorageDescriptor storage;
	}
}
