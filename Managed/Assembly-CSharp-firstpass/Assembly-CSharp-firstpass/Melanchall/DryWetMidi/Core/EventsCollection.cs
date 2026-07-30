using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200011F RID: 287
	public sealed class EventsCollection : IEnumerable<MidiEvent>, IEnumerable
	{
		// Token: 0x06000788 RID: 1928 RVA: 0x0001D8B8 File Offset: 0x0001BAB8
		internal EventsCollection()
		{
		}

		// Token: 0x17000117 RID: 279
		public MidiEvent this[int index]
		{
			get
			{
				ThrowIfArgument.IsInvalidIndex("index", index, this._events.Count);
				return this._events[index];
			}
			set
			{
				ThrowIfArgument.IsNull("value", value);
				ThrowIfArgument.IsInvalidIndex("index", index, this._events.Count);
				this._events[index] = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001D91F File Offset: 0x0001BB1F
		public int Count
		{
			get
			{
				return this._events.Count;
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001D92C File Offset: 0x0001BB2C
		public void Add(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			this._events.Add(midiEvent);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001D945 File Offset: 0x0001BB45
		public void AddRange(IEnumerable<MidiEvent> events)
		{
			ThrowIfArgument.IsNull("events", events);
			this._events.AddRange(events.Where((MidiEvent e) => e != null));
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001D982 File Offset: 0x0001BB82
		public void Insert(int index, MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			ThrowIfArgument.IsInvalidIndex("index", index, this._events.Count);
			this._events.Insert(index, midiEvent);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001D9B2 File Offset: 0x0001BBB2
		public void InsertRange(int index, IEnumerable<MidiEvent> midiEvents)
		{
			ThrowIfArgument.IsNull("midiEvents", midiEvents);
			ThrowIfArgument.IsInvalidIndex("index", index, this._events.Count);
			this._events.InsertRange(index, midiEvents);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001D9E2 File Offset: 0x0001BBE2
		public bool Remove(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			return this._events.Remove(midiEvent);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001D9FB File Offset: 0x0001BBFB
		public void RemoveAt(int index)
		{
			ThrowIfArgument.IsInvalidIndex("index", index, this._events.Count);
			this._events.RemoveAt(index);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001DA1F File Offset: 0x0001BC1F
		public int RemoveAll(Predicate<MidiEvent> match)
		{
			ThrowIfArgument.IsNull("match", match);
			return this._events.RemoveAll(match);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001DA38 File Offset: 0x0001BC38
		public int IndexOf(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			return this._events.IndexOf(midiEvent);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001DA51 File Offset: 0x0001BC51
		public void Clear()
		{
			this._events.Clear();
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001DA5E File Offset: 0x0001BC5E
		public IEnumerator<MidiEvent> GetEnumerator()
		{
			return this._events.GetEnumerator();
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001DA5E File Offset: 0x0001BC5E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._events.GetEnumerator();
		}

		// Token: 0x04000845 RID: 2117
		private readonly List<MidiEvent> _events = new List<MidiEvent>();
	}
}
