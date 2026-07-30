using System;
using System.Web.Caching;

namespace System.Web.UI
{
	/// <summary>Provides programmatic access to an ASP.NET user control's output cache settings.</summary>
	// Token: 0x020001B7 RID: 439
	public sealed class ControlCachePolicy
	{
		// Token: 0x060011D1 RID: 4561 RVA: 0x000315A7 File Offset: 0x0002F7A7
		internal ControlCachePolicy()
			: this(null)
		{
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000315B0 File Offset: 0x0002F7B0
		internal ControlCachePolicy(BasePartialCachingControl bpcc)
		{
			this.bpcc = bpcc;
		}

		/// <summary>Gets or sets a value indicating whether fragment caching is enabled for the user control.</summary>
		/// <returns>true if the user control's output is cached; otherwise, false.</returns>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.- or -The <see cref="P:System.Web.UI.ControlCachePolicy.Cached" /> property is set outside of the initialization and rendering stages of the control.</exception>
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x000315BF File Offset: 0x0002F7BF
		// (set) Token: 0x060011D4 RID: 4564 RVA: 0x000315CD File Offset: 0x0002F7CD
		public bool Cached
		{
			get
			{
				this.AssertBasePartialCachingControl();
				return this.cached;
			}
			set
			{
				this.AssertBasePartialCachingControl();
				this.cached = value;
			}
		}

		/// <summary>Gets or sets an instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class associated with the cached user control output.</summary>
		/// <returns>The <see cref="T:System.Web.Caching.CacheDependency" /> associated with the control. The default is null.</returns>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.- or -The <see cref="P:System.Web.UI.ControlCachePolicy.Dependency" /> property is set outside of the initialization and rendering stages of the control.</exception>
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x000315DC File Offset: 0x0002F7DC
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x000315EF File Offset: 0x0002F7EF
		public CacheDependency Dependency
		{
			get
			{
				this.AssertBasePartialCachingControl();
				return this.bpcc.Dependency;
			}
			set
			{
				this.AssertBasePartialCachingControl();
				this.bpcc.Dependency = value;
			}
		}

		/// <summary>Gets or sets the amount of time that cached items are to remain in the output cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that represents the amount of time a user control is to remain in the output cache. The default is <see cref="F:System.TimeSpan.Zero" />.</returns>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.- or -The <see cref="P:System.Web.UI.ControlCachePolicy.Duration" /> property is set outside of the initialization and rendering stages of the control.</exception>
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00031603 File Offset: 0x0002F803
		// (set) Token: 0x060011D8 RID: 4568 RVA: 0x0003161C File Offset: 0x0002F81C
		public TimeSpan Duration
		{
			get
			{
				this.AssertBasePartialCachingControl();
				return TimeSpan.FromMinutes((double)this.bpcc.Duration);
			}
			set
			{
				this.AssertBasePartialCachingControl();
				this.bpcc.Duration = value.Minutes;
			}
		}

		/// <summary>Gets or sets the name of the output-cache provider that is associated with a control instance.</summary>
		/// <returns>The name of the provider.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The provider name was not found.</exception>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to set the <see cref="P:System.Web.UI.ControlCachePolicy.ProviderName" /> property during or after the <see cref="E:System.Web.UI.Control.PreRender" /> event.</exception>
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00031636 File Offset: 0x0002F836
		// (set) Token: 0x060011DA RID: 4570 RVA: 0x00031649 File Offset: 0x0002F849
		public string ProviderName
		{
			get
			{
				this.AssertBasePartialCachingControl();
				return this.bpcc.ProviderName;
			}
			set
			{
				this.AssertBasePartialCachingControl();
				this.bpcc.ProviderName = value;
			}
		}

		/// <summary>Gets a value indicating whether the user control supports caching.</summary>
		/// <returns>true if the user control supports caching; otherwise, false.</returns>
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x0003165D File Offset: 0x0002F85D
		public bool SupportsCaching
		{
			get
			{
				return this.bpcc != null;
			}
		}

		/// <summary>Gets or sets a list of control identifiers to vary the cached output by.</summary>
		/// <returns>A semicolon-separated list of strings used to vary a user control's output cache. These strings represent the <see cref="P:System.Web.UI.Control.ID" /> property values of ASP.NET server controls declared in the user control.</returns>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.- or -The <see cref="P:System.Web.UI.ControlCachePolicy.VaryByControl" /> property is set outside of the initialization and rendering stages of the control.</exception>
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x00031668 File Offset: 0x0002F868
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x0003167B File Offset: 0x0002F87B
		public string VaryByControl
		{
			get
			{
				this.AssertBasePartialCachingControl();
				return this.bpcc.VaryByControls;
			}
			set
			{
				this.AssertBasePartialCachingControl();
				this.bpcc.VaryByControls = value;
			}
		}

		/// <summary>Gets or sets a list of GET or POST parameter names to vary the cached output by. </summary>
		/// <returns>A semicolon-separated list of strings used to vary the output cache. </returns>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.</exception>
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0003168F File Offset: 0x0002F88F
		public HttpCacheVaryByParams VaryByParams
		{
			get
			{
				this.AssertBasePartialCachingControl();
				throw new NotImplementedException();
			}
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.BasePartialCachingControl" /> control that wraps the user control to expire the cache entry at the specified date and time.</summary>
		/// <param name="expirationTime">A <see cref="T:System.DateTime" /> after which the cached entry expires.</param>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.</exception>
		// Token: 0x060011DF RID: 4575 RVA: 0x0003169C File Offset: 0x0002F89C
		public void SetExpires(DateTime expirationTime)
		{
			this.AssertBasePartialCachingControl();
			this.bpcc.ExpirationTime = expirationTime;
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.BasePartialCachingControl" /> control that wraps the user control to set the user control's cache entry to use sliding or absolute expiration. </summary>
		/// <param name="useSlidingExpiration">true to use sliding cache expiration instead of absolute expiration; otherwise, false.</param>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.</exception>
		// Token: 0x060011E0 RID: 4576 RVA: 0x000316B0 File Offset: 0x0002F8B0
		public void SetSlidingExpiration(bool useSlidingExpiration)
		{
			this.AssertBasePartialCachingControl();
			this.bpcc.SlidingExpiration = useSlidingExpiration;
		}

		/// <summary>Sets a list of custom strings that the output cache will use to vary the user control.</summary>
		/// <param name="varyByCustom">The list of custom strings.</param>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.</exception>
		// Token: 0x060011E1 RID: 4577 RVA: 0x000316C4 File Offset: 0x0002F8C4
		public void SetVaryByCustom(string varyByCustom)
		{
			this.AssertBasePartialCachingControl();
			this.bpcc.VaryByCustom = varyByCustom;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x000316D8 File Offset: 0x0002F8D8
		private void AssertBasePartialCachingControl()
		{
			if (this.bpcc == null)
			{
				throw new HttpException("The user control is not associated with a 'BasePartialCachingControl' and is not cacheable.");
			}
		}

		// Token: 0x04001400 RID: 5120
		private BasePartialCachingControl bpcc;

		// Token: 0x04001401 RID: 5121
		private bool cached;
	}
}
