using System;

namespace System.Web
{
	// Token: 0x02000075 RID: 117
	internal class HeadersCollection : BaseParamsCollection
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x0000945A File Offset: 0x0000765A
		public HeadersCollection(HttpRequest request)
			: base(request)
		{
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00009463 File Offset: 0x00007663
		public override void Add(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new PlatformNotSupportedException();
			}
			base.Set(name, value);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00009463 File Offset: 0x00007663
		public override void Set(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new PlatformNotSupportedException();
			}
			base.Set(name, value);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000947B File Offset: 0x0000767B
		public override void Remove(string name)
		{
			if (base.IsReadOnly)
			{
				throw new PlatformNotSupportedException();
			}
			base.Remove(name);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00009494 File Offset: 0x00007694
		protected override void InsertInfo()
		{
			HttpWorkerRequest workerRequest = this._request.WorkerRequest;
			if (workerRequest != null)
			{
				for (int i = 0; i < 40; i++)
				{
					string knownRequestHeader = workerRequest.GetKnownRequestHeader(i);
					if (knownRequestHeader != null && !(knownRequestHeader == ""))
					{
						this.Add(HttpWorkerRequest.GetKnownRequestHeaderName(i), knownRequestHeader);
					}
				}
				string[][] unknownRequestHeaders = workerRequest.GetUnknownRequestHeaders();
				if (unknownRequestHeaders != null && unknownRequestHeaders.GetUpperBound(0) != -1)
				{
					int num = unknownRequestHeaders.GetUpperBound(0) + 1;
					for (int j = 0; j < num; j++)
					{
						this.Add(unknownRequestHeaders[j][0], unknownRequestHeaders[j][1]);
					}
				}
				base.Protect();
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000952C File Offset: 0x0000772C
		protected override string InternalGet(string name)
		{
			int knownRequestHeaderIndex = HttpWorkerRequest.GetKnownRequestHeaderIndex(name);
			string text = null;
			if (knownRequestHeaderIndex >= 0)
			{
				text = this._request.WorkerRequest.GetKnownRequestHeader(knownRequestHeaderIndex);
			}
			if (text == null)
			{
				text = this._request.WorkerRequest.GetUnknownRequestHeader(name);
			}
			return text;
		}
	}
}
