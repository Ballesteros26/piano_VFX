using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	internal struct MarkerList : ISerializationCallbackReceiver
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00007D30 File Offset: 0x00005F30
		public List<IMarker> markers
		{
			get
			{
				this.BuildCache();
				return this.m_Cache;
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007D3E File Offset: 0x00005F3E
		public MarkerList(int capacity)
		{
			this.m_Objects = new List<ScriptableObject>(capacity);
			this.m_Cache = new List<IMarker>(capacity);
			this.m_CacheDirty = true;
			this.m_HasNotifications = false;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007D66 File Offset: 0x00005F66
		public void Add(ScriptableObject item)
		{
			if (item == null)
			{
				return;
			}
			this.m_Objects.Add(item);
			this.m_CacheDirty = true;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007D85 File Offset: 0x00005F85
		public bool Remove(IMarker item)
		{
			if (!(item is ScriptableObject))
			{
				throw new InvalidOperationException("Supplied type must be a ScriptableObject");
			}
			return this.Remove((ScriptableObject)item, item.parent.timelineAsset, item.parent);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007DB7 File Offset: 0x00005FB7
		public bool Remove(ScriptableObject item, TimelineAsset timelineAsset, PlayableAsset thingToDirty)
		{
			if (!this.m_Objects.Contains(item))
			{
				return false;
			}
			this.m_Objects.Remove(item);
			this.m_CacheDirty = true;
			TimelineUndo.PushDestroyUndo(timelineAsset, thingToDirty, item, "Delete Marker");
			return true;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007DEB File Offset: 0x00005FEB
		public void Clear()
		{
			this.m_Objects.Clear();
			this.m_CacheDirty = true;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007DFF File Offset: 0x00005FFF
		public bool Contains(ScriptableObject item)
		{
			return this.m_Objects.Contains(item);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007E0D File Offset: 0x0000600D
		public IEnumerable<IMarker> GetMarkers()
		{
			return this.markers;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00007E15 File Offset: 0x00006015
		public int Count
		{
			get
			{
				return this.markers.Count;
			}
		}

		// Token: 0x170000AC RID: 172
		public IMarker this[int idx]
		{
			get
			{
				return this.markers[idx];
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007E30 File Offset: 0x00006030
		public List<ScriptableObject> GetRawMarkerList()
		{
			return this.m_Objects;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007E38 File Offset: 0x00006038
		public IMarker CreateMarker(Type type, double time, TrackAsset owner)
		{
			if (!typeof(ScriptableObject).IsAssignableFrom(type) || !typeof(IMarker).IsAssignableFrom(type))
			{
				throw new InvalidOperationException("The requested type needs to inherit from ScriptableObject and implement IMarker");
			}
			if (!owner.supportsNotifications && typeof(INotification).IsAssignableFrom(type))
			{
				throw new InvalidOperationException("Markers implementing the INotification interface cannot be added on tracks that do not support notifications");
			}
			ScriptableObject scriptableObject = ScriptableObject.CreateInstance(type);
			IMarker marker = (IMarker)scriptableObject;
			marker.time = time;
			TimelineCreateUtilities.SaveAssetIntoObject(scriptableObject, owner);
			this.Add(scriptableObject);
			marker.Initialize(owner);
			return marker;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007EC2 File Offset: 0x000060C2
		public bool HasNotifications()
		{
			this.BuildCache();
			return this.m_HasNotifications;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000028DC File Offset: 0x00000ADC
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007ED0 File Offset: 0x000060D0
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_CacheDirty = true;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00007EDC File Offset: 0x000060DC
		private void BuildCache()
		{
			if (this.m_CacheDirty)
			{
				this.m_Cache = new List<IMarker>(this.m_Objects.Count);
				this.m_HasNotifications = false;
				foreach (ScriptableObject scriptableObject in this.m_Objects)
				{
					if (scriptableObject != null)
					{
						this.m_Cache.Add(scriptableObject as IMarker);
						if (scriptableObject is INotification)
						{
							this.m_HasNotifications = true;
						}
					}
				}
				this.m_CacheDirty = false;
			}
		}

		// Token: 0x040000C5 RID: 197
		[SerializeField]
		[HideInInspector]
		private List<ScriptableObject> m_Objects;

		// Token: 0x040000C6 RID: 198
		[HideInInspector]
		[NonSerialized]
		private List<IMarker> m_Cache;

		// Token: 0x040000C7 RID: 199
		private bool m_CacheDirty;

		// Token: 0x040000C8 RID: 200
		private bool m_HasNotifications;
	}
}
