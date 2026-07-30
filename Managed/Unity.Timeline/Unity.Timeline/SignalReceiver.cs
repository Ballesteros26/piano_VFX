using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002C RID: 44
	public class SignalReceiver : MonoBehaviour, INotificationReceiver
	{
		// Token: 0x0600023C RID: 572 RVA: 0x000080DC File Offset: 0x000062DC
		public void OnNotify(Playable origin, INotification notification, object context)
		{
			SignalEmitter signalEmitter = notification as SignalEmitter;
			UnityEvent unityEvent;
			if (signalEmitter != null && signalEmitter.asset != null && this.m_Events.TryGetValue(signalEmitter.asset, out unityEvent) && unityEvent != null)
			{
				unityEvent.Invoke();
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008128 File Offset: 0x00006328
		public void AddReaction(SignalAsset asset, UnityEvent reaction)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (this.m_Events.signals.Contains(asset))
			{
				throw new ArgumentException("SignalAsset already used.");
			}
			this.m_Events.Append(asset, reaction);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00008174 File Offset: 0x00006374
		public int AddEmptyReaction(UnityEvent reaction)
		{
			this.m_Events.Append(null, reaction);
			return this.m_Events.events.Count - 1;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008195 File Offset: 0x00006395
		public void Remove(SignalAsset asset)
		{
			if (!this.m_Events.signals.Contains(asset))
			{
				throw new ArgumentException("The SignalAsset is not registered with this receiver.");
			}
			this.m_Events.Remove(asset);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000081C1 File Offset: 0x000063C1
		public IEnumerable<SignalAsset> GetRegisteredSignals()
		{
			return this.m_Events.signals;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000081D0 File Offset: 0x000063D0
		public UnityEvent GetReaction(SignalAsset key)
		{
			UnityEvent unityEvent;
			if (this.m_Events.TryGetValue(key, out unityEvent))
			{
				return unityEvent;
			}
			return null;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000081F0 File Offset: 0x000063F0
		public int Count()
		{
			return this.m_Events.signals.Count;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008204 File Offset: 0x00006404
		public void ChangeSignalAtIndex(int idx, SignalAsset newKey)
		{
			if (idx < 0 || idx > this.m_Events.signals.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			if (this.m_Events.signals[idx] == newKey)
			{
				return;
			}
			bool flag = this.m_Events.signals.Contains(newKey);
			if (newKey == null || this.m_Events.signals[idx] == null || !flag)
			{
				this.m_Events.signals[idx] = newKey;
			}
			if (flag)
			{
				throw new ArgumentException("SignalAsset already used.");
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000082A1 File Offset: 0x000064A1
		public void RemoveAtIndex(int idx)
		{
			if (idx < 0 || idx > this.m_Events.signals.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			this.m_Events.Remove(idx);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000082CE File Offset: 0x000064CE
		public void ChangeReactionAtIndex(int idx, UnityEvent reaction)
		{
			if (idx < 0 || idx > this.m_Events.events.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			this.m_Events.events[idx] = reaction;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00008301 File Offset: 0x00006501
		public UnityEvent GetReactionAtIndex(int idx)
		{
			if (idx < 0 || idx > this.m_Events.events.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			return this.m_Events.events[idx];
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008333 File Offset: 0x00006533
		public SignalAsset GetSignalAssetAtIndex(int idx)
		{
			if (idx < 0 || idx > this.m_Events.signals.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			return this.m_Events.signals[idx];
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000028DC File Offset: 0x00000ADC
		private void OnEnable()
		{
		}

		// Token: 0x040000CD RID: 205
		[SerializeField]
		private SignalReceiver.EventKeyValue m_Events = new SignalReceiver.EventKeyValue();

		// Token: 0x0200006C RID: 108
		[Serializable]
		private class EventKeyValue
		{
			// Token: 0x0600032E RID: 814 RVA: 0x0000AF08 File Offset: 0x00009108
			public bool TryGetValue(SignalAsset key, out UnityEvent value)
			{
				int num = this.m_Signals.IndexOf(key);
				if (num != -1)
				{
					value = this.m_Events[num];
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x0600032F RID: 815 RVA: 0x0000AF3A File Offset: 0x0000913A
			public void Append(SignalAsset key, UnityEvent value)
			{
				this.m_Signals.Add(key);
				this.m_Events.Add(value);
			}

			// Token: 0x06000330 RID: 816 RVA: 0x0000AF54 File Offset: 0x00009154
			public void Remove(int idx)
			{
				if (idx != -1)
				{
					this.m_Signals.RemoveAt(idx);
					this.m_Events.RemoveAt(idx);
				}
			}

			// Token: 0x06000331 RID: 817 RVA: 0x0000AF74 File Offset: 0x00009174
			public void Remove(SignalAsset key)
			{
				int num = this.m_Signals.IndexOf(key);
				if (num != -1)
				{
					this.m_Signals.RemoveAt(num);
					this.m_Events.RemoveAt(num);
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06000332 RID: 818 RVA: 0x0000AFAA File Offset: 0x000091AA
			public List<SignalAsset> signals
			{
				get
				{
					return this.m_Signals;
				}
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000333 RID: 819 RVA: 0x0000AFB2 File Offset: 0x000091B2
			public List<UnityEvent> events
			{
				get
				{
					return this.m_Events;
				}
			}

			// Token: 0x04000157 RID: 343
			[SerializeField]
			private List<SignalAsset> m_Signals = new List<SignalAsset>();

			// Token: 0x04000158 RID: 344
			[SerializeField]
			[CustomSignalEventDrawer]
			private List<UnityEvent> m_Events = new List<UnityEvent>();
		}
	}
}
