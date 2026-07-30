using System;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Caching;

namespace System.Web.UI
{
	/// <summary>Provides the base functionality for the <see cref="T:System.Web.UI.StaticPartialCachingControl" /> and <see cref="T:System.Web.UI.PartialCachingControl" /> classes.</summary>
	// Token: 0x020001A4 RID: 420
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class BasePartialCachingControl : Control
	{
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x0002C3E0 File Offset: 0x0002A5E0
		// (set) Token: 0x06001013 RID: 4115 RVA: 0x0002C3E8 File Offset: 0x0002A5E8
		internal string CtrlID
		{
			get
			{
				return this.ctrl_id;
			}
			set
			{
				this.ctrl_id = value;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0002C3F1 File Offset: 0x0002A5F1
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x0002C3F9 File Offset: 0x0002A5F9
		internal string Guid
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0002C402 File Offset: 0x0002A602
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x0002C40A File Offset: 0x0002A60A
		internal int Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				this.duration = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0002C413 File Offset: 0x0002A613
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x0002C41B File Offset: 0x0002A61B
		internal string VaryByParams
		{
			get
			{
				return this.varyby_params;
			}
			set
			{
				this.varyby_params = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0002C424 File Offset: 0x0002A624
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x0002C42C File Offset: 0x0002A62C
		internal string VaryByControls
		{
			get
			{
				return this.varyby_controls;
			}
			set
			{
				this.varyby_controls = value;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x0002C435 File Offset: 0x0002A635
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x0002C43D File Offset: 0x0002A63D
		internal string VaryByCustom
		{
			get
			{
				return this.varyby_custom;
			}
			set
			{
				this.varyby_custom = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x0002C446 File Offset: 0x0002A646
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x0002C44E File Offset: 0x0002A64E
		internal DateTime ExpirationTime
		{
			get
			{
				return this.expirationTime;
			}
			set
			{
				this.expirationTime = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0002C457 File Offset: 0x0002A657
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x0002C45F File Offset: 0x0002A65F
		internal bool SlidingExpiration
		{
			get
			{
				return this.slidingExpiration;
			}
			set
			{
				this.slidingExpiration = value;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0002C468 File Offset: 0x0002A668
		// (set) Token: 0x06001023 RID: 4131 RVA: 0x0002C470 File Offset: 0x0002A670
		internal string ProviderName { get; set; }

		// Token: 0x06001024 RID: 4132
		internal abstract Control CreateControl();

		/// <summary>Releases all resources used by the <see cref="T:System.Web.UI.BasePartialCachingControl" /> class. </summary>
		// Token: 0x06001025 RID: 4133 RVA: 0x0002C479 File Offset: 0x0002A679
		public override void Dispose()
		{
			if (this.dependency != null)
			{
				this.dependency.Dispose();
				this.dependency = null;
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0002C498 File Offset: 0x0002A698
		private void RetrieveCachedContents()
		{
			this.cacheKey = this.CreateKey();
			OutputCacheProvider provider = this.GetProvider();
			this.cachedData = provider.Get(this.cacheKey) as string;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0002C4D0 File Offset: 0x0002A6D0
		private OutputCacheProvider GetProvider()
		{
			string providerName = this.ProviderName;
			OutputCacheProvider outputCacheProvider;
			if (string.IsNullOrEmpty(providerName))
			{
				outputCacheProvider = OutputCache.DefaultProvider;
			}
			else
			{
				outputCacheProvider = OutputCache.GetProvider(providerName);
				if (outputCacheProvider == null)
				{
					outputCacheProvider = OutputCache.DefaultProvider;
				}
			}
			return outputCacheProvider;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0002C505 File Offset: 0x0002A705
		private void OnDependencyChanged(string key, object value, CacheItemRemovedReason reason)
		{
			Console.WriteLine("{0}.OnDependencyChanged (\"{0}\", {1}, {2})", new object[] { this, key, value, reason });
			this.GetProvider().Remove(key);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0002C538 File Offset: 0x0002A738
		internal override void InitRecursive(Control namingContainer)
		{
			this.RetrieveCachedContents();
			if (this.cachedData == null)
			{
				this.control = this.CreateControl();
				this.Controls.Add(this.control);
			}
			else
			{
				this.control = null;
			}
			base.InitRecursive(namingContainer);
		}

		/// <summary>Outputs the user control's content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> output stream.</summary>
		/// <param name="output">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that writes the cached control to the page.</param>
		// Token: 0x0600102A RID: 4138 RVA: 0x0002C578 File Offset: 0x0002A778
		protected internal override void Render(HtmlTextWriter output)
		{
			if (this.cachedData != null)
			{
				output.Write(this.cachedData);
				return;
			}
			if (this.control == null)
			{
				base.Render(output);
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			StringWriter stringWriter = new StringWriter();
			TextWriter textWriter = httpContext.Response.SetTextWriter(stringWriter);
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			string text;
			try
			{
				this.control.RenderControl(htmlTextWriter);
			}
			finally
			{
				text = stringWriter.ToString();
				httpContext.Response.SetTextWriter(textWriter);
				output.Write(text);
			}
			OutputCacheProvider provider = this.GetProvider();
			DateTime dateTime = DateTime.UtcNow.AddSeconds((double)this.duration);
			provider.Set(this.cacheKey, text, dateTime);
			httpContext.InternalCache.Insert(this.cacheKey, text, this.dependency, dateTime.ToLocalTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.ControlCachePolicy" /> object that is associated with the wrapped user control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCachePolicy" /> that stores output caching-related properties of the wrapped user control.</returns>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0002C65C File Offset: 0x0002A85C
		public ControlCachePolicy CachePolicy
		{
			get
			{
				if (this.cachePolicy == null)
				{
					this.cachePolicy = new ControlCachePolicy(this);
				}
				return this.cachePolicy;
			}
		}

		/// <summary>Gets or sets an instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class associated with the cached user control output.</summary>
		/// <returns>The <see cref="T:System.Web.Caching.CacheDependency" /> associated with the server control.</returns>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0002C678 File Offset: 0x0002A878
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x0002C680 File Offset: 0x0002A880
		public CacheDependency Dependency
		{
			get
			{
				return this.dependency;
			}
			set
			{
				this.dependency = value;
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0002C68C File Offset: 0x0002A88C
		private string CreateKey()
		{
			StringBuilder stringBuilder = new StringBuilder();
			HttpContext httpContext = HttpContext.Current;
			stringBuilder.Append("PartialCachingControl\n");
			stringBuilder.Append("GUID: " + this.guid + "\n");
			if (this.varyby_params != null && this.varyby_params.Length > 0)
			{
				string[] array = this.varyby_params.Split(new char[] { ';' });
				for (int i = 0; i < array.Length; i++)
				{
					string text = httpContext.Request.Params[array[i]];
					stringBuilder.Append("VP:");
					stringBuilder.Append(array[i]);
					stringBuilder.Append('=');
					stringBuilder.Append((text != null) ? text : "__null__");
					stringBuilder.Append('\n');
				}
			}
			if (this.varyby_controls != null && this.varyby_params.Length > 0)
			{
				string[] array2 = this.varyby_controls.Split(new char[] { ';' });
				for (int j = 0; j < array2.Length; j++)
				{
					string text2 = httpContext.Request.Params[array2[j]];
					stringBuilder.Append("VCN:");
					stringBuilder.Append(array2[j]);
					stringBuilder.Append('=');
					stringBuilder.Append((text2 != null) ? text2 : "__null__");
					stringBuilder.Append('\n');
				}
			}
			if (this.varyby_custom != null)
			{
				string varyByCustomString = httpContext.ApplicationInstance.GetVaryByCustomString(httpContext, this.varyby_custom);
				stringBuilder.Append("VC:");
				stringBuilder.Append(this.varyby_custom);
				stringBuilder.Append('=');
				stringBuilder.Append((varyByCustomString != null) ? varyByCustomString : "__null__");
				stringBuilder.Append('\n');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001351 RID: 4945
		private CacheDependency dependency;

		// Token: 0x04001352 RID: 4946
		private string ctrl_id;

		// Token: 0x04001353 RID: 4947
		private string guid;

		// Token: 0x04001354 RID: 4948
		private int duration;

		// Token: 0x04001355 RID: 4949
		private string varyby_params;

		// Token: 0x04001356 RID: 4950
		private string varyby_controls;

		// Token: 0x04001357 RID: 4951
		private string varyby_custom;

		// Token: 0x04001358 RID: 4952
		private DateTime expirationTime;

		// Token: 0x04001359 RID: 4953
		private bool slidingExpiration;

		// Token: 0x0400135A RID: 4954
		private Control control;

		// Token: 0x0400135B RID: 4955
		private ControlCachePolicy cachePolicy;

		// Token: 0x0400135C RID: 4956
		private string cacheKey;

		// Token: 0x0400135D RID: 4957
		private string cachedData;
	}
}
