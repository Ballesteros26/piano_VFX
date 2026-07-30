using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002BF RID: 703
	internal sealed class CompilerScope
	{
		// Token: 0x060014EF RID: 5359 RVA: 0x0003E7A8 File Offset: 0x0003C9A8
		internal CompilerScope(object node, bool isMethod)
		{
			this.Node = node;
			this.IsMethod = isMethod;
			IReadOnlyList<ParameterExpression> variables = CompilerScope.GetVariables(node);
			this.Definitions = new Dictionary<ParameterExpression, VariableStorageKind>(variables.Count);
			foreach (ParameterExpression parameterExpression in variables)
			{
				this.Definitions.Add(parameterExpression, VariableStorageKind.Local);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0003E838 File Offset: 0x0003CA38
		internal HoistedLocals NearestHoistedLocals
		{
			get
			{
				return this._hoistedLocals ?? this._closureHoistedLocals;
			}
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0003E84C File Offset: 0x0003CA4C
		internal CompilerScope Enter(LambdaCompiler lc, CompilerScope parent)
		{
			this.SetParent(lc, parent);
			this.AllocateLocals(lc);
			if (this.IsMethod && this._closureHoistedLocals != null)
			{
				this.EmitClosureAccess(lc, this._closureHoistedLocals);
			}
			this.EmitNewHoistedLocals(lc);
			if (this.IsMethod)
			{
				this.EmitCachedVariables();
			}
			return this;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0003E89C File Offset: 0x0003CA9C
		internal CompilerScope Exit()
		{
			if (!this.IsMethod)
			{
				foreach (CompilerScope.Storage storage in this._locals.Values)
				{
					storage.FreeLocal();
				}
			}
			CompilerScope parent = this._parent;
			this._parent = null;
			this._hoistedLocals = null;
			this._closureHoistedLocals = null;
			this._locals.Clear();
			return parent;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0003E920 File Offset: 0x0003CB20
		internal void EmitVariableAccess(LambdaCompiler lc, ReadOnlyCollection<ParameterExpression> vars)
		{
			if (this.NearestHoistedLocals != null && vars.Count > 0)
			{
				ArrayBuilder<long> arrayBuilder = new ArrayBuilder<long>(vars.Count);
				foreach (ParameterExpression parameterExpression in vars)
				{
					ulong num = 0UL;
					HoistedLocals hoistedLocals = this.NearestHoistedLocals;
					while (!hoistedLocals.Indexes.ContainsKey(parameterExpression))
					{
						num += 1UL;
						hoistedLocals = hoistedLocals.Parent;
					}
					ulong num2 = (num << 32) | (ulong)hoistedLocals.Indexes[parameterExpression];
					arrayBuilder.UncheckedAdd((long)num2);
				}
				this.EmitGet(this.NearestHoistedLocals.SelfVariable);
				lc.EmitConstantArray<long>(arrayBuilder.ToArray());
				lc.IL.Emit(OpCodes.Call, CachedReflectionInfo.RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array);
				return;
			}
			lc.IL.Emit(OpCodes.Call, CachedReflectionInfo.RuntimeOps_CreateRuntimeVariables);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0003EA18 File Offset: 0x0003CC18
		internal void AddLocal(LambdaCompiler gen, ParameterExpression variable)
		{
			this._locals.Add(variable, new CompilerScope.LocalStorage(gen, variable));
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0003EA2D File Offset: 0x0003CC2D
		internal void EmitGet(ParameterExpression variable)
		{
			this.ResolveVariable(variable).EmitLoad();
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0003EA3B File Offset: 0x0003CC3B
		internal void EmitSet(ParameterExpression variable)
		{
			this.ResolveVariable(variable).EmitStore();
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0003EA49 File Offset: 0x0003CC49
		internal void EmitAddressOf(ParameterExpression variable)
		{
			this.ResolveVariable(variable).EmitAddress();
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0003EA57 File Offset: 0x0003CC57
		private CompilerScope.Storage ResolveVariable(ParameterExpression variable)
		{
			return this.ResolveVariable(variable, this.NearestHoistedLocals);
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0003EA68 File Offset: 0x0003CC68
		private CompilerScope.Storage ResolveVariable(ParameterExpression variable, HoistedLocals hoistedLocals)
		{
			for (CompilerScope compilerScope = this; compilerScope != null; compilerScope = compilerScope._parent)
			{
				CompilerScope.Storage storage;
				if (compilerScope._locals.TryGetValue(variable, out storage))
				{
					return storage;
				}
				if (compilerScope.IsMethod)
				{
					break;
				}
			}
			for (HoistedLocals hoistedLocals2 = hoistedLocals; hoistedLocals2 != null; hoistedLocals2 = hoistedLocals2.Parent)
			{
				int num;
				if (hoistedLocals2.Indexes.TryGetValue(variable, out num))
				{
					return new CompilerScope.ElementBoxStorage(this.ResolveVariable(hoistedLocals2.SelfVariable, hoistedLocals), num, variable);
				}
			}
			throw Error.UndefinedVariable(variable.Name, variable.Type, this.CurrentLambdaName);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0003EAE8 File Offset: 0x0003CCE8
		private void SetParent(LambdaCompiler lc, CompilerScope parent)
		{
			this._parent = parent;
			if (this.NeedsClosure && this._parent != null)
			{
				this._closureHoistedLocals = this._parent.NearestHoistedLocals;
			}
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = (from p in this.GetVariables()
				where this.Definitions[p] == VariableStorageKind.Hoisted
				select p).ToReadOnly<ParameterExpression>();
			if (readOnlyCollection.Count > 0)
			{
				this._hoistedLocals = new HoistedLocals(this._closureHoistedLocals, readOnlyCollection);
				this.AddLocal(lc, this._hoistedLocals.SelfVariable);
			}
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0003EB68 File Offset: 0x0003CD68
		private void EmitNewHoistedLocals(LambdaCompiler lc)
		{
			if (this._hoistedLocals == null)
			{
				return;
			}
			lc.IL.EmitPrimitive(this._hoistedLocals.Variables.Count);
			lc.IL.Emit(OpCodes.Newarr, typeof(object));
			int num = 0;
			foreach (ParameterExpression parameterExpression in this._hoistedLocals.Variables)
			{
				lc.IL.Emit(OpCodes.Dup);
				lc.IL.EmitPrimitive(num++);
				Type type = typeof(StrongBox<>).MakeGenericType(new Type[] { parameterExpression.Type });
				int num2;
				if (this.IsMethod && (num2 = lc.Parameters.IndexOf(parameterExpression)) >= 0)
				{
					lc.EmitLambdaArgument(num2);
					lc.IL.Emit(OpCodes.Newobj, type.GetConstructor(new Type[] { parameterExpression.Type }));
				}
				else if (parameterExpression == this._hoistedLocals.ParentVariable)
				{
					this.ResolveVariable(parameterExpression, this._closureHoistedLocals).EmitLoad();
					lc.IL.Emit(OpCodes.Newobj, type.GetConstructor(new Type[] { parameterExpression.Type }));
				}
				else
				{
					lc.IL.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
				}
				if (this.ShouldCache(parameterExpression))
				{
					lc.IL.Emit(OpCodes.Dup);
					this.CacheBoxToLocal(lc, parameterExpression);
				}
				lc.IL.Emit(OpCodes.Stelem_Ref);
			}
			this.EmitSet(this._hoistedLocals.SelfVariable);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0003ED34 File Offset: 0x0003CF34
		private void EmitCachedVariables()
		{
			if (this.ReferenceCount == null)
			{
				return;
			}
			foreach (KeyValuePair<ParameterExpression, int> keyValuePair in this.ReferenceCount)
			{
				if (this.ShouldCache(keyValuePair.Key, keyValuePair.Value))
				{
					CompilerScope.ElementBoxStorage elementBoxStorage = this.ResolveVariable(keyValuePair.Key) as CompilerScope.ElementBoxStorage;
					if (elementBoxStorage != null)
					{
						elementBoxStorage.EmitLoadBox();
						this.CacheBoxToLocal(elementBoxStorage.Compiler, keyValuePair.Key);
					}
				}
			}
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0003EDD0 File Offset: 0x0003CFD0
		private bool ShouldCache(ParameterExpression v, int refCount)
		{
			return refCount > 2 && !this._locals.ContainsKey(v);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0003EDE8 File Offset: 0x0003CFE8
		private bool ShouldCache(ParameterExpression v)
		{
			int num;
			return this.ReferenceCount != null && this.ReferenceCount.TryGetValue(v, out num) && this.ShouldCache(v, num);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0003EE1C File Offset: 0x0003D01C
		private void CacheBoxToLocal(LambdaCompiler lc, ParameterExpression v)
		{
			CompilerScope.LocalBoxStorage localBoxStorage = new CompilerScope.LocalBoxStorage(lc, v);
			localBoxStorage.EmitStoreBox();
			this._locals.Add(v, localBoxStorage);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0003EE44 File Offset: 0x0003D044
		private void EmitClosureAccess(LambdaCompiler lc, HoistedLocals locals)
		{
			if (locals == null)
			{
				return;
			}
			this.EmitClosureToVariable(lc, locals);
			while ((locals = locals.Parent) != null)
			{
				ParameterExpression selfVariable = locals.SelfVariable;
				CompilerScope.LocalStorage localStorage = new CompilerScope.LocalStorage(lc, selfVariable);
				localStorage.EmitStore(this.ResolveVariable(selfVariable));
				this._locals.Add(selfVariable, localStorage);
			}
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0003EE93 File Offset: 0x0003D093
		private void EmitClosureToVariable(LambdaCompiler lc, HoistedLocals locals)
		{
			lc.EmitClosureArgument();
			lc.IL.Emit(OpCodes.Ldfld, CachedReflectionInfo.Closure_Locals);
			this.AddLocal(lc, locals.SelfVariable);
			this.EmitSet(locals.SelfVariable);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0003EECC File Offset: 0x0003D0CC
		private void AllocateLocals(LambdaCompiler lc)
		{
			foreach (ParameterExpression parameterExpression in this.GetVariables())
			{
				if (this.Definitions[parameterExpression] == VariableStorageKind.Local)
				{
					CompilerScope.Storage storage;
					if (this.IsMethod && lc.Parameters.Contains(parameterExpression))
					{
						storage = new CompilerScope.ArgumentStorage(lc, parameterExpression);
					}
					else
					{
						storage = new CompilerScope.LocalStorage(lc, parameterExpression);
					}
					this._locals.Add(parameterExpression, storage);
				}
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0003EF58 File Offset: 0x0003D158
		private IEnumerable<ParameterExpression> GetVariables()
		{
			if (this.MergedScopes != null)
			{
				return this.GetVariablesIncludingMerged();
			}
			return CompilerScope.GetVariables(this.Node);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0003EF81 File Offset: 0x0003D181
		private IEnumerable<ParameterExpression> GetVariablesIncludingMerged()
		{
			foreach (ParameterExpression parameterExpression in CompilerScope.GetVariables(this.Node))
			{
				yield return parameterExpression;
			}
			IEnumerator<ParameterExpression> enumerator = null;
			foreach (BlockExpression blockExpression in this.MergedScopes)
			{
				foreach (ParameterExpression parameterExpression2 in blockExpression.Variables)
				{
					yield return parameterExpression2;
				}
				enumerator = null;
			}
			HashSet<BlockExpression>.Enumerator enumerator2 = default(HashSet<BlockExpression>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0003EF94 File Offset: 0x0003D194
		private static IReadOnlyList<ParameterExpression> GetVariables(object scope)
		{
			LambdaExpression lambdaExpression = scope as LambdaExpression;
			if (lambdaExpression != null)
			{
				return new ParameterList(lambdaExpression);
			}
			BlockExpression blockExpression = scope as BlockExpression;
			if (blockExpression != null)
			{
				return blockExpression.Variables;
			}
			return new ParameterExpression[] { ((CatchBlock)scope).Variable };
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0003EFD8 File Offset: 0x0003D1D8
		private string CurrentLambdaName
		{
			get
			{
				for (CompilerScope compilerScope = this; compilerScope != null; compilerScope = compilerScope._parent)
				{
					LambdaExpression lambdaExpression = compilerScope.Node as LambdaExpression;
					if (lambdaExpression != null)
					{
						return lambdaExpression.Name;
					}
				}
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x04000A03 RID: 2563
		private CompilerScope _parent;

		// Token: 0x04000A04 RID: 2564
		internal readonly object Node;

		// Token: 0x04000A05 RID: 2565
		internal readonly bool IsMethod;

		// Token: 0x04000A06 RID: 2566
		internal bool NeedsClosure;

		// Token: 0x04000A07 RID: 2567
		internal readonly Dictionary<ParameterExpression, VariableStorageKind> Definitions = new Dictionary<ParameterExpression, VariableStorageKind>();

		// Token: 0x04000A08 RID: 2568
		internal Dictionary<ParameterExpression, int> ReferenceCount;

		// Token: 0x04000A09 RID: 2569
		internal HashSet<BlockExpression> MergedScopes;

		// Token: 0x04000A0A RID: 2570
		private HoistedLocals _hoistedLocals;

		// Token: 0x04000A0B RID: 2571
		private HoistedLocals _closureHoistedLocals;

		// Token: 0x04000A0C RID: 2572
		private readonly Dictionary<ParameterExpression, CompilerScope.Storage> _locals = new Dictionary<ParameterExpression, CompilerScope.Storage>();

		// Token: 0x020002C0 RID: 704
		private abstract class Storage
		{
			// Token: 0x06001508 RID: 5384 RVA: 0x0003F01F File Offset: 0x0003D21F
			internal Storage(LambdaCompiler compiler, ParameterExpression variable)
			{
				this.Compiler = compiler;
				this.Variable = variable;
			}

			// Token: 0x06001509 RID: 5385
			internal abstract void EmitLoad();

			// Token: 0x0600150A RID: 5386
			internal abstract void EmitAddress();

			// Token: 0x0600150B RID: 5387
			internal abstract void EmitStore();

			// Token: 0x0600150C RID: 5388 RVA: 0x0003F035 File Offset: 0x0003D235
			internal virtual void EmitStore(CompilerScope.Storage value)
			{
				value.EmitLoad();
				this.EmitStore();
			}

			// Token: 0x0600150D RID: 5389 RVA: 0x00003C4C File Offset: 0x00001E4C
			internal virtual void FreeLocal()
			{
			}

			// Token: 0x04000A0D RID: 2573
			internal readonly LambdaCompiler Compiler;

			// Token: 0x04000A0E RID: 2574
			internal readonly ParameterExpression Variable;
		}

		// Token: 0x020002C1 RID: 705
		private sealed class LocalStorage : CompilerScope.Storage
		{
			// Token: 0x0600150E RID: 5390 RVA: 0x0003F043 File Offset: 0x0003D243
			internal LocalStorage(LambdaCompiler compiler, ParameterExpression variable)
				: base(compiler, variable)
			{
				this._local = compiler.GetLocal(variable.IsByRef ? variable.Type.MakeByRefType() : variable.Type);
			}

			// Token: 0x0600150F RID: 5391 RVA: 0x0003F074 File Offset: 0x0003D274
			internal override void EmitLoad()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._local);
			}

			// Token: 0x06001510 RID: 5392 RVA: 0x0003F091 File Offset: 0x0003D291
			internal override void EmitStore()
			{
				this.Compiler.IL.Emit(OpCodes.Stloc, this._local);
			}

			// Token: 0x06001511 RID: 5393 RVA: 0x0003F0AE File Offset: 0x0003D2AE
			internal override void EmitAddress()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloca, this._local);
			}

			// Token: 0x06001512 RID: 5394 RVA: 0x0003F0CB File Offset: 0x0003D2CB
			internal override void FreeLocal()
			{
				this.Compiler.FreeLocal(this._local);
			}

			// Token: 0x04000A0F RID: 2575
			private readonly LocalBuilder _local;
		}

		// Token: 0x020002C2 RID: 706
		private sealed class ArgumentStorage : CompilerScope.Storage
		{
			// Token: 0x06001513 RID: 5395 RVA: 0x0003F0DE File Offset: 0x0003D2DE
			internal ArgumentStorage(LambdaCompiler compiler, ParameterExpression p)
				: base(compiler, p)
			{
				this._argument = compiler.GetLambdaArgument(compiler.Parameters.IndexOf(p));
			}

			// Token: 0x06001514 RID: 5396 RVA: 0x0003F100 File Offset: 0x0003D300
			internal override void EmitLoad()
			{
				this.Compiler.IL.EmitLoadArg(this._argument);
			}

			// Token: 0x06001515 RID: 5397 RVA: 0x0003F118 File Offset: 0x0003D318
			internal override void EmitStore()
			{
				this.Compiler.IL.EmitStoreArg(this._argument);
			}

			// Token: 0x06001516 RID: 5398 RVA: 0x0003F130 File Offset: 0x0003D330
			internal override void EmitAddress()
			{
				this.Compiler.IL.EmitLoadArgAddress(this._argument);
			}

			// Token: 0x04000A10 RID: 2576
			private readonly int _argument;
		}

		// Token: 0x020002C3 RID: 707
		private sealed class ElementBoxStorage : CompilerScope.Storage
		{
			// Token: 0x06001517 RID: 5399 RVA: 0x0003F148 File Offset: 0x0003D348
			internal ElementBoxStorage(CompilerScope.Storage array, int index, ParameterExpression variable)
				: base(array.Compiler, variable)
			{
				this._array = array;
				this._index = index;
				this._boxType = typeof(StrongBox<>).MakeGenericType(new Type[] { variable.Type });
				this._boxValueField = this._boxType.GetField("Value");
			}

			// Token: 0x06001518 RID: 5400 RVA: 0x0003F1AA File Offset: 0x0003D3AA
			internal override void EmitLoad()
			{
				this.EmitLoadBox();
				this.Compiler.IL.Emit(OpCodes.Ldfld, this._boxValueField);
			}

			// Token: 0x06001519 RID: 5401 RVA: 0x0003F1D0 File Offset: 0x0003D3D0
			internal override void EmitStore()
			{
				LocalBuilder local = this.Compiler.GetLocal(this.Variable.Type);
				this.Compiler.IL.Emit(OpCodes.Stloc, local);
				this.EmitLoadBox();
				this.Compiler.IL.Emit(OpCodes.Ldloc, local);
				this.Compiler.FreeLocal(local);
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x0600151A RID: 5402 RVA: 0x0003F24D File Offset: 0x0003D44D
			internal override void EmitStore(CompilerScope.Storage value)
			{
				this.EmitLoadBox();
				value.EmitLoad();
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x0600151B RID: 5403 RVA: 0x0003F276 File Offset: 0x0003D476
			internal override void EmitAddress()
			{
				this.EmitLoadBox();
				this.Compiler.IL.Emit(OpCodes.Ldflda, this._boxValueField);
			}

			// Token: 0x0600151C RID: 5404 RVA: 0x0003F29C File Offset: 0x0003D49C
			internal void EmitLoadBox()
			{
				this._array.EmitLoad();
				this.Compiler.IL.EmitPrimitive(this._index);
				this.Compiler.IL.Emit(OpCodes.Ldelem_Ref);
				this.Compiler.IL.Emit(OpCodes.Castclass, this._boxType);
			}

			// Token: 0x04000A11 RID: 2577
			private readonly int _index;

			// Token: 0x04000A12 RID: 2578
			private readonly CompilerScope.Storage _array;

			// Token: 0x04000A13 RID: 2579
			private readonly Type _boxType;

			// Token: 0x04000A14 RID: 2580
			private readonly FieldInfo _boxValueField;
		}

		// Token: 0x020002C4 RID: 708
		private sealed class LocalBoxStorage : CompilerScope.Storage
		{
			// Token: 0x0600151D RID: 5405 RVA: 0x0003F2FC File Offset: 0x0003D4FC
			internal LocalBoxStorage(LambdaCompiler compiler, ParameterExpression variable)
				: base(compiler, variable)
			{
				Type type = typeof(StrongBox<>).MakeGenericType(new Type[] { variable.Type });
				this._boxValueField = type.GetField("Value");
				this._boxLocal = compiler.GetLocal(type);
			}

			// Token: 0x0600151E RID: 5406 RVA: 0x0003F34E File Offset: 0x0003D54E
			internal override void EmitLoad()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				this.Compiler.IL.Emit(OpCodes.Ldfld, this._boxValueField);
			}

			// Token: 0x0600151F RID: 5407 RVA: 0x0003F386 File Offset: 0x0003D586
			internal override void EmitAddress()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				this.Compiler.IL.Emit(OpCodes.Ldflda, this._boxValueField);
			}

			// Token: 0x06001520 RID: 5408 RVA: 0x0003F3C0 File Offset: 0x0003D5C0
			internal override void EmitStore()
			{
				LocalBuilder local = this.Compiler.GetLocal(this.Variable.Type);
				this.Compiler.IL.Emit(OpCodes.Stloc, local);
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				this.Compiler.IL.Emit(OpCodes.Ldloc, local);
				this.Compiler.FreeLocal(local);
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x06001521 RID: 5409 RVA: 0x0003F452 File Offset: 0x0003D652
			internal override void EmitStore(CompilerScope.Storage value)
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				value.EmitLoad();
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x06001522 RID: 5410 RVA: 0x0003F490 File Offset: 0x0003D690
			internal void EmitStoreBox()
			{
				this.Compiler.IL.Emit(OpCodes.Stloc, this._boxLocal);
			}

			// Token: 0x06001523 RID: 5411 RVA: 0x0003F4AD File Offset: 0x0003D6AD
			internal override void FreeLocal()
			{
				this.Compiler.FreeLocal(this._boxLocal);
			}

			// Token: 0x04000A15 RID: 2581
			private readonly LocalBuilder _boxLocal;

			// Token: 0x04000A16 RID: 2582
			private readonly FieldInfo _boxValueField;
		}
	}
}
