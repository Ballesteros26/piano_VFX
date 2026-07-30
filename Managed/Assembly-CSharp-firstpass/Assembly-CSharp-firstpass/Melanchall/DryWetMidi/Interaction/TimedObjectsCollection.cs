using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000D4 RID: 212
	public abstract class TimedObjectsCollection<TObject> : IEnumerable<TObject>, IEnumerable where TObject : ITimedObject
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x00017B28 File Offset: 0x00015D28
		internal TimedObjectsCollection(IEnumerable<TObject> objects)
		{
			this._objects.AddRange(objects.Where((TObject o) => o != null));
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00017B78 File Offset: 0x00015D78
		public void Add(IEnumerable<TObject> objects)
		{
			ThrowIfArgument.IsNull("objects", objects);
			List<TObject> list = objects.Where((TObject o) => o != null).ToList<TObject>();
			this._objects.AddRange(list);
			this.OnObjectsAdded(list);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00017BCE File Offset: 0x00015DCE
		public void Add(params TObject[] objects)
		{
			ThrowIfArgument.IsNull("objects", objects);
			this.Add(objects);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00017BE4 File Offset: 0x00015DE4
		public void Remove(IEnumerable<TObject> objects)
		{
			ThrowIfArgument.IsNull("objects", objects);
			List<TObject> list = new List<TObject>();
			foreach (TObject tobject in objects)
			{
				if (this._objects.Remove(tobject))
				{
					list.Add(tobject);
				}
			}
			this.OnObjectsRemoved(list);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00017C54 File Offset: 0x00015E54
		public void Remove(params TObject[] objects)
		{
			ThrowIfArgument.IsNull("objects", objects);
			this.Remove(objects);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00017C68 File Offset: 0x00015E68
		public void RemoveAll(Predicate<TObject> match)
		{
			ThrowIfArgument.IsNull("match", match);
			List<TObject> list = this._objects.Where((TObject o) => match(o)).ToList<TObject>();
			this._objects.RemoveAll(match);
			this.OnObjectsRemoved(list);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00017CC8 File Offset: 0x00015EC8
		public void Clear()
		{
			List<TObject> list = this._objects.ToList<TObject>();
			this._objects.Clear();
			this.OnObjectsRemoved(list);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00002994 File Offset: 0x00000B94
		protected virtual void OnObjectsAdded(IEnumerable<TObject> addedObjects)
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00002994 File Offset: 0x00000B94
		protected virtual void OnObjectsRemoved(IEnumerable<TObject> removedObjects)
		{
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00017CF3 File Offset: 0x00015EF3
		public virtual IEnumerator<TObject> GetEnumerator()
		{
			return this._objects.OrderBy((TObject o) => o.Time).GetEnumerator();
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00017D24 File Offset: 0x00015F24
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400072C RID: 1836
		protected readonly List<TObject> _objects = new List<TObject>();
	}
}
