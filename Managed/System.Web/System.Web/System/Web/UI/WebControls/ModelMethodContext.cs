using System;
using System.Web.ModelBinding;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Used to invoke the <see cref="M:System.Web.UI.Page.UpdateModel``1(``0)" /> or <see cref="M:System.Web.UI.Page.TryUpdateModel``1(``0)" /> method when the <see cref="T:System.Web.UI.Page" /> object is not directly accessible.</summary>
	// Token: 0x0200079B RID: 1947
	public class ModelMethodContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ModelMethodContext" /> class.</summary>
		/// <param name="page">The page object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="page" /> parameter is null.</exception>
		// Token: 0x06004E94 RID: 20116 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelMethodContext(Page page)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.ModelMethodContext" /> object that corresponds to the <see cref="T:System.Web.UI.Page" /> object that is in the <see cref="P:System.Web.HttpContext.Current" /> property.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.ModelMethodContext" /> object., or null if the current request is not for a page.</returns>
		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x06004E95 RID: 20117 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static ModelMethodContext Current
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the model state.</summary>
		/// <returns>The model state.</returns>
		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x06004E96 RID: 20118 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ModelStateDictionary ModelState
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Updates the specified model instance using values from a value provider.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="model">The model.</param>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		// Token: 0x06004E97 RID: 20119 RVA: 0x000CB4AC File Offset: 0x000C96AC
		public virtual bool TryUpdateModel<TModel>(TModel model)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Updates the specified model instance using values from the specified value provider.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="model">The model.</param>
		/// <param name="valueProvider">The value provider.</param>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		// Token: 0x06004E98 RID: 20120 RVA: 0x000CB4C8 File Offset: 0x000C96C8
		public virtual bool TryUpdateModel<TModel>(TModel model, IValueProvider valueProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Updates the specified model instance using values from a value provider.</summary>
		/// <param name="model">The model.</param>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		// Token: 0x06004E99 RID: 20121 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void UpdateModel<TModel>(TModel model)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Updates the specified model instance using values from the specified value provider.</summary>
		/// <param name="model">The model.</param>
		/// <param name="valueProvider">The value provider.</param>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		// Token: 0x06004E9A RID: 20122 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void UpdateModel<TModel>(TModel model, IValueProvider valueProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
