using System;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x020004C9 RID: 1225
	internal class TrackingStringDictionary : StringDictionary
	{
		// Token: 0x0600245F RID: 9311 RVA: 0x0008DDB9 File Offset: 0x0008BFB9
		internal TrackingStringDictionary()
			: this(false)
		{
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x0008DDC2 File Offset: 0x0008BFC2
		internal TrackingStringDictionary(bool isReadOnly)
		{
			this.isReadOnly = isReadOnly;
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x0008DDD1 File Offset: 0x0008BFD1
		// (set) Token: 0x06002462 RID: 9314 RVA: 0x0008DDD9 File Offset: 0x0008BFD9
		internal bool IsChanged
		{
			get
			{
				return this.isChanged;
			}
			set
			{
				this.isChanged = value;
			}
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x0008DDE2 File Offset: 0x0008BFE2
		public override void Add(string key, string value)
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(global::SR.GetString("The collection is read-only."));
			}
			base.Add(key, value);
			this.isChanged = true;
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x0008DE0B File Offset: 0x0008C00B
		public override void Clear()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(global::SR.GetString("The collection is read-only."));
			}
			base.Clear();
			this.isChanged = true;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x0008DE32 File Offset: 0x0008C032
		public override void Remove(string key)
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(global::SR.GetString("The collection is read-only."));
			}
			base.Remove(key);
			this.isChanged = true;
		}

		// Token: 0x17000786 RID: 1926
		public override string this[string key]
		{
			get
			{
				return base[key];
			}
			set
			{
				if (this.isReadOnly)
				{
					throw new InvalidOperationException(global::SR.GetString("The collection is read-only."));
				}
				base[key] = value;
				this.isChanged = true;
			}
		}

		// Token: 0x04002025 RID: 8229
		private bool isChanged;

		// Token: 0x04002026 RID: 8230
		private bool isReadOnly;
	}
}
