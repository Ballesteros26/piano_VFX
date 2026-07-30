using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200013C RID: 316
	public sealed class EventTypesCollection : IEnumerable<EventType>, IEnumerable
	{
		// Token: 0x0600081F RID: 2079 RVA: 0x0001EB00 File Offset: 0x0001CD00
		public void Add(Type type, byte statusByte)
		{
			this._statusBytes.Add(type, statusByte);
			this._types.Add(statusByte, type);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001EB1C File Offset: 0x0001CD1C
		public bool TryGetType(byte statusByte, out Type type)
		{
			return this._types.TryGetValue(statusByte, out type);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001EB2B File Offset: 0x0001CD2B
		public bool TryGetStatusByte(Type type, out byte statusByte)
		{
			return this._statusBytes.TryGetValue(type, out statusByte);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001EB3A File Offset: 0x0001CD3A
		public IEnumerator<EventType> GetEnumerator()
		{
			return this._statusBytes.Select((KeyValuePair<Type, byte> kv) => new EventType(kv.Key, kv.Value)).GetEnumerator();
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001EB6B File Offset: 0x0001CD6B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000898 RID: 2200
		private readonly Dictionary<Type, byte> _statusBytes = new Dictionary<Type, byte>();

		// Token: 0x04000899 RID: 2201
		private readonly Dictionary<byte, Type> _types = new Dictionary<byte, Type>();
	}
}
