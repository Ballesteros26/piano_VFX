using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace System.Web.Caching
{
	/// <summary>Combines multiple dependencies between an item stored in an ASP.NET application's <see cref="T:System.Web.Caching.Cache" /> object and an array of <see cref="T:System.Web.Caching.CacheDependency" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200067C RID: 1660
	public sealed class AggregateCacheDependency : CacheDependency
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.AggregateCacheDependency" /> class.</summary>
		// Token: 0x060046E2 RID: 18146 RVA: 0x000C6DE7 File Offset: 0x000C4FE7
		public AggregateCacheDependency()
		{
			base.FinishInit();
		}

		/// <summary>Adds an array of <see cref="T:System.Web.Caching.CacheDependency" /> objects to the <see cref="T:System.Web.Caching.AggregateCacheDependency" /> object.</summary>
		/// <param name="dependencies">The array of <see cref="T:System.Web.Caching.CacheDependency" />  objects to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dependencies" /> is null.- or -A <see cref="T:System.Web.Caching.CacheDependency" /> object in <paramref name="dependencies" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Web.Caching.CacheDependency" /> object is referenced from more than one <see cref="T:System.Web.Caching.Cache" /> entry.</exception>
		// Token: 0x060046E3 RID: 18147 RVA: 0x000C6E00 File Offset: 0x000C5000
		public void Add(params CacheDependency[] dependencies)
		{
			if (dependencies == null)
			{
				throw new ArgumentNullException("dependencies");
			}
			if (dependencies.Length == 0)
			{
				return;
			}
			bool flag = false;
			foreach (CacheDependency cacheDependency in dependencies)
			{
				if (cacheDependency == null || cacheDependency.IsUsed)
				{
					throw new InvalidOperationException("Cache dependency already in use");
				}
				if (!flag && cacheDependency != null && cacheDependency.HasChanged)
				{
					flag = true;
				}
			}
			object obj = this.dependenciesLock;
			lock (obj)
			{
				if (this.dependencies == null)
				{
					this.dependencies = new List<CacheDependency>(dependencies.Length);
				}
				foreach (CacheDependency cacheDependency2 in dependencies)
				{
					if (cacheDependency2 != null)
					{
						cacheDependency2.DependencyChanged += this.OnAnyChanged;
					}
				}
				this.dependencies.AddRange(dependencies);
				base.Start = DateTime.UtcNow;
			}
			if (flag)
			{
				base.NotifyDependencyChanged(this, null);
			}
		}

		/// <summary>Retrieves a unique identifier for the <see cref="T:System.Web.Caching.AggregateCacheDependency" /> object.</summary>
		/// <returns>The unique identifier for the <see cref="T:System.Web.Caching.AggregateCacheDependency" /> object. If one of the associated dependency objects does not have a unique identifier, the <see cref="M:System.Web.Caching.AggregateCacheDependency.GetUniqueId" /> method returns null.</returns>
		// Token: 0x060046E4 RID: 18148 RVA: 0x000C6EF4 File Offset: 0x000C50F4
		public override string GetUniqueID()
		{
			if (this.dependencies == null || this.dependencies.Count == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			object obj = this.dependenciesLock;
			lock (obj)
			{
				foreach (CacheDependency cacheDependency in this.dependencies)
				{
					string uniqueID = cacheDependency.GetUniqueID();
					if (string.IsNullOrEmpty(uniqueID))
					{
						return null;
					}
					stringBuilder.Append(uniqueID);
					stringBuilder.Append(';');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x000C6FB4 File Offset: 0x000C51B4
		protected override void DependencyDispose()
		{
			base.DependencyDispose();
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x000C6FBC File Offset: 0x000C51BC
		internal override void DependencyDisposeInternal()
		{
			if (this.dependencies != null && this.dependencies.Count > 0)
			{
				foreach (CacheDependency cacheDependency in this.dependencies)
				{
					cacheDependency.DependencyChanged -= this.OnAnyChanged;
				}
			}
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x000C7030 File Offset: 0x000C5230
		private void OnAnyChanged(object sender, EventArgs args)
		{
			base.NotifyDependencyChanged(sender, args);
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string[] GetFileDependencies()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04002556 RID: 9558
		private object dependenciesLock = new object();

		// Token: 0x04002557 RID: 9559
		private List<CacheDependency> dependencies;
	}
}
