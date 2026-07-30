using System;
using System.Collections.Generic;

namespace System.Web.ModelBinding
{
	/// <summary>Encapsulates all of the information that is external to the model binding system that the model binding system needs. </summary>
	// Token: 0x02000521 RID: 1313
	public class ModelBindingExecutionContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBindingExecutionContext" /> class, using the HTTP context and the model state.</summary>
		/// <param name="httpContext">The HTTP context.</param>
		/// <param name="modelState">The model state.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="httpContext" /> or <paramref name="modelState" /> parameter is null.</exception>
		// Token: 0x060039EE RID: 14830 RVA: 0x0009CF79 File Offset: 0x0009B179
		public ModelBindingExecutionContext(HttpContextBase httpContext, ModelStateDictionary modelState)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (modelState == null)
			{
				throw new ArgumentNullException("modelState");
			}
			this._httpContext = httpContext;
			this._modelState = modelState;
		}

		/// <summary>Gets the HTTP context.</summary>
		/// <returns>The HTTP context.</returns>
		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x060039EF RID: 14831 RVA: 0x0009CFB6 File Offset: 0x0009B1B6
		public virtual HttpContextBase HttpContext
		{
			get
			{
				return this._httpContext;
			}
		}

		/// <summary>Gets the model state.</summary>
		/// <returns>The model state.</returns>
		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x060039F0 RID: 14832 RVA: 0x0009CFBE File Offset: 0x0009B1BE
		public virtual ModelStateDictionary ModelState
		{
			get
			{
				return this._modelState;
			}
		}

		/// <summary>Stores an object that contains values that are used for model binding and that will be accessed by using the <see cref="M:System.Web.ModelBinding.ModelBindingExecutionContext.GetService``1" /> method.</summary>
		/// <param name="service">The object that contains values to store.</param>
		/// <typeparam name="TService">The type of the object that contains values to store.</typeparam>
		// Token: 0x060039F1 RID: 14833 RVA: 0x0009CFC6 File Offset: 0x0009B1C6
		public virtual void PublishService<TService>(TService service)
		{
			this._services[typeof(TService)] = service;
		}

		/// <summary>Gets an object that contains values that are used for model binding and that have been stored by using the <see cref="M:System.Web.ModelBinding.ModelBindingExecutionContext.PublishService``1(``0)" /> method. </summary>
		/// <returns>The object that contains values that are used for model binding.</returns>
		/// <typeparam name="TService">The type of the object that contains values that are used for model binding.</typeparam>
		// Token: 0x060039F2 RID: 14834 RVA: 0x0009CFE3 File Offset: 0x0009B1E3
		public virtual TService GetService<TService>()
		{
			return (TService)((object)this._services[typeof(TService)]);
		}

		/// <summary>Gets an object that contains values that are used for model binding and that have been stored by using the <see cref="M:System.Web.ModelBinding.ModelBindingExecutionContext.PublishService``1(``0)" /> method.</summary>
		/// <returns>The object that contains values that are used for model binding, or the default value of <paramref name="TService" /> if the requested object is not found.</returns>
		/// <typeparam name="TService">The type of the object that contains values that are used for model binding.</typeparam>
		// Token: 0x060039F3 RID: 14835 RVA: 0x0009D000 File Offset: 0x0009B200
		public virtual TService TryGetService<TService>()
		{
			if (this._services.ContainsKey(typeof(TService)))
			{
				return (TService)((object)this._services[typeof(TService)]);
			}
			return default(TService);
		}

		// Token: 0x04001F52 RID: 8018
		private Dictionary<Type, object> _services = new Dictionary<Type, object>();

		// Token: 0x04001F53 RID: 8019
		private HttpContextBase _httpContext;

		// Token: 0x04001F54 RID: 8020
		private ModelStateDictionary _modelState;
	}
}
