using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002BD RID: 701
	internal sealed class BoundConstants
	{
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x0003E453 File Offset: 0x0003C653
		internal int Count
		{
			get
			{
				return this._values.Count;
			}
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0003E460 File Offset: 0x0003C660
		internal object[] ToArray()
		{
			return this._values.ToArray();
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0003E46D File Offset: 0x0003C66D
		internal void AddReference(object value, Type type)
		{
			if (this._indexes.TryAdd(value, this._values.Count))
			{
				this._values.Add(value);
			}
			Helpers.IncrementCount<BoundConstants.TypedConstant>(new BoundConstants.TypedConstant(value, type), this._references);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0003E4A8 File Offset: 0x0003C6A8
		internal void EmitConstant(LambdaCompiler lc, object value, Type type)
		{
			if (!lc.CanEmitBoundConstants)
			{
				throw Error.CannotCompileConstant(value);
			}
			LocalBuilder localBuilder;
			if (this._cache.TryGetValue(new BoundConstants.TypedConstant(value, type), out localBuilder))
			{
				lc.IL.Emit(OpCodes.Ldloc, localBuilder);
				return;
			}
			BoundConstants.EmitConstantsArray(lc);
			this.EmitConstantFromArray(lc, value, type);
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0003E4FC File Offset: 0x0003C6FC
		internal void EmitCacheConstants(LambdaCompiler lc)
		{
			int num = 0;
			foreach (KeyValuePair<BoundConstants.TypedConstant, int> keyValuePair in this._references)
			{
				if (!lc.CanEmitBoundConstants)
				{
					throw Error.CannotCompileConstant(keyValuePair.Key.Value);
				}
				if (BoundConstants.ShouldCache(keyValuePair.Value))
				{
					num++;
				}
			}
			if (num == 0)
			{
				return;
			}
			BoundConstants.EmitConstantsArray(lc);
			this._cache.Clear();
			foreach (KeyValuePair<BoundConstants.TypedConstant, int> keyValuePair2 in this._references)
			{
				if (BoundConstants.ShouldCache(keyValuePair2.Value))
				{
					if (--num > 0)
					{
						lc.IL.Emit(OpCodes.Dup);
					}
					LocalBuilder localBuilder = lc.IL.DeclareLocal(keyValuePair2.Key.Type);
					this.EmitConstantFromArray(lc, keyValuePair2.Key.Value, localBuilder.LocalType);
					lc.IL.Emit(OpCodes.Stloc, localBuilder);
					this._cache.Add(keyValuePair2.Key, localBuilder);
				}
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0003E64C File Offset: 0x0003C84C
		private static bool ShouldCache(int refCount)
		{
			return refCount > 2;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0003E652 File Offset: 0x0003C852
		private static void EmitConstantsArray(LambdaCompiler lc)
		{
			lc.EmitClosureArgument();
			lc.IL.Emit(OpCodes.Ldfld, CachedReflectionInfo.Closure_Constants);
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0003E670 File Offset: 0x0003C870
		private void EmitConstantFromArray(LambdaCompiler lc, object value, Type type)
		{
			int count;
			if (!this._indexes.TryGetValue(value, out count))
			{
				this._indexes.Add(value, count = this._values.Count);
				this._values.Add(value);
			}
			lc.IL.EmitPrimitive(count);
			lc.IL.Emit(OpCodes.Ldelem_Ref);
			if (type.IsValueType)
			{
				lc.IL.Emit(OpCodes.Unbox_Any, type);
				return;
			}
			if (type != typeof(object))
			{
				lc.IL.Emit(OpCodes.Castclass, type);
			}
		}

		// Token: 0x040009FD RID: 2557
		private readonly List<object> _values = new List<object>();

		// Token: 0x040009FE RID: 2558
		private readonly Dictionary<object, int> _indexes = new Dictionary<object, int>(global::System.Collections.Generic.ReferenceEqualityComparer<object>.Instance);

		// Token: 0x040009FF RID: 2559
		private readonly Dictionary<BoundConstants.TypedConstant, int> _references = new Dictionary<BoundConstants.TypedConstant, int>();

		// Token: 0x04000A00 RID: 2560
		private readonly Dictionary<BoundConstants.TypedConstant, LocalBuilder> _cache = new Dictionary<BoundConstants.TypedConstant, LocalBuilder>();

		// Token: 0x020002BE RID: 702
		private struct TypedConstant : IEquatable<BoundConstants.TypedConstant>
		{
			// Token: 0x060014EB RID: 5355 RVA: 0x0003E744 File Offset: 0x0003C944
			internal TypedConstant(object value, Type type)
			{
				this.Value = value;
				this.Type = type;
			}

			// Token: 0x060014EC RID: 5356 RVA: 0x0003E754 File Offset: 0x0003C954
			public override int GetHashCode()
			{
				return RuntimeHelpers.GetHashCode(this.Value) ^ this.Type.GetHashCode();
			}

			// Token: 0x060014ED RID: 5357 RVA: 0x0003E76D File Offset: 0x0003C96D
			public bool Equals(BoundConstants.TypedConstant other)
			{
				return this.Value == other.Value && this.Type.Equals(other.Type);
			}

			// Token: 0x060014EE RID: 5358 RVA: 0x0003E790 File Offset: 0x0003C990
			public override bool Equals(object obj)
			{
				return obj is BoundConstants.TypedConstant && this.Equals((BoundConstants.TypedConstant)obj);
			}

			// Token: 0x04000A01 RID: 2561
			internal readonly object Value;

			// Token: 0x04000A02 RID: 2562
			internal readonly Type Type;
		}
	}
}
