using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000114 RID: 276
	public sealed class ChunkTypesCollection : IEnumerable<ChunkType>, IEnumerable
	{
		// Token: 0x06000748 RID: 1864 RVA: 0x0001C9D7 File Offset: 0x0001ABD7
		public void Add(Type type, string id)
		{
			this._ids.Add(type, id);
			this._types.Add(id, type);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001C9F3 File Offset: 0x0001ABF3
		public bool TryGetType(string id, out Type type)
		{
			return this._types.TryGetValue(id, out type);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001CA02 File Offset: 0x0001AC02
		public bool TryGetId(Type type, out string id)
		{
			return this._ids.TryGetValue(type, out id);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001CA11 File Offset: 0x0001AC11
		public IEnumerator<ChunkType> GetEnumerator()
		{
			return this._ids.Select((KeyValuePair<Type, string> kv) => new ChunkType(kv.Key, kv.Value)).GetEnumerator();
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001CA42 File Offset: 0x0001AC42
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400083B RID: 2107
		private readonly Dictionary<Type, string> _ids = new Dictionary<Type, string>();

		// Token: 0x0400083C RID: 2108
		private readonly Dictionary<string, Type> _types = new Dictionary<string, Type>();
	}
}
