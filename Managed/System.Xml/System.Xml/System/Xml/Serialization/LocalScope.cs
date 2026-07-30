using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x020002CE RID: 718
	internal class LocalScope
	{
		// Token: 0x06001B13 RID: 6931 RVA: 0x000968B9 File Offset: 0x00094AB9
		public LocalScope()
		{
			this.locals = new Dictionary<string, LocalBuilder>();
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000968CC File Offset: 0x00094ACC
		public LocalScope(LocalScope parent)
			: this()
		{
			this.parent = parent;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000968DB File Offset: 0x00094ADB
		public void Add(string key, LocalBuilder value)
		{
			this.locals.Add(key, value);
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x000968EA File Offset: 0x00094AEA
		public bool ContainsKey(string key)
		{
			return this.locals.ContainsKey(key) || (this.parent != null && this.parent.ContainsKey(key));
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00096912 File Offset: 0x00094B12
		public bool TryGetValue(string key, out LocalBuilder value)
		{
			if (this.locals.TryGetValue(key, out value))
			{
				return true;
			}
			if (this.parent != null)
			{
				return this.parent.TryGetValue(key, out value);
			}
			value = null;
			return false;
		}

		// Token: 0x1700052A RID: 1322
		public LocalBuilder this[string key]
		{
			get
			{
				LocalBuilder localBuilder;
				this.TryGetValue(key, out localBuilder);
				return localBuilder;
			}
			set
			{
				this.locals[key] = value;
			}
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00096968 File Offset: 0x00094B68
		public void AddToFreeLocals(Dictionary<Tuple<Type, string>, Queue<LocalBuilder>> freeLocals)
		{
			foreach (KeyValuePair<string, LocalBuilder> keyValuePair in this.locals)
			{
				Tuple<Type, string> tuple = new Tuple<Type, string>(keyValuePair.Value.LocalType, keyValuePair.Key);
				Queue<LocalBuilder> queue;
				if (freeLocals.TryGetValue(tuple, out queue))
				{
					queue.Enqueue(keyValuePair.Value);
				}
				else
				{
					queue = new Queue<LocalBuilder>();
					queue.Enqueue(keyValuePair.Value);
					freeLocals.Add(tuple, queue);
				}
			}
		}

		// Token: 0x040015C3 RID: 5571
		public readonly LocalScope parent;

		// Token: 0x040015C4 RID: 5572
		private readonly Dictionary<string, LocalBuilder> locals;
	}
}
