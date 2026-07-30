using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for metadata for a model. </summary>
	// Token: 0x020006F3 RID: 1779
	public class ModelMetadata
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelMetadata" /> class.</summary>
		/// <param name="provider">The provider object.</param>
		/// <param name="containerType">The type of the container, or null to create metadata for the model type.</param>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="modelType">The type of the model.</param>
		/// <param name="propertyName">The name of the property, or null to create metadata for the model type.</param>
		// Token: 0x06004B2A RID: 19242 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ModelMetadata(ModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a collection that contains additional metadata about the model.</summary>
		/// <returns>A collection that contains additional metadata about the model.</returns>
		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x06004B2B RID: 19243 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public virtual Dictionary<string, object> AdditionalValues
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the type of the container. </summary>
		/// <returns>The type of the container, or null if there is no container.</returns>
		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x06004B2C RID: 19244 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Type ContainerType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that determines whether empty strings that are submitted in forms should be converted to null.</summary>
		/// <returns>true if empty strings should be converted to null; otherwise, false. The default value is true.</returns>
		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x06004B2D RID: 19245 RVA: 0x000CAA10 File Offset: 0x000C8C10
		// (set) Token: 0x06004B2E RID: 19246 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the data type.</summary>
		/// <returns>The name of the data type.</returns>
		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06004B2F RID: 19247 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B30 RID: 19248 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string DataTypeName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets text that describes the model.</summary>
		/// <returns>The description text.</returns>
		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06004B31 RID: 19249 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B32 RID: 19250 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string Description
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a format string that should be applied when the model is displayed in display mode (as opposed to in edit mode).</summary>
		/// <returns>The format string.</returns>
		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x06004B33 RID: 19251 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B34 RID: 19252 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string DisplayFormatString
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text to use in UI when the name of the model is displayed. </summary>
		/// <returns>The text to use as the name of the model.</returns>
		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x06004B35 RID: 19253 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B36 RID: 19254 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string DisplayName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the format string to use in UI in edit mode.</summary>
		/// <returns>The format string.</returns>
		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x06004B37 RID: 19255 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B38 RID: 19256 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string EditFormatString
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that determines whether surrounding HTML should be hidden.</summary>
		/// <returns>true if surrounding HTML should be hidden; otherwise, false.</returns>
		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x06004B39 RID: 19257 RVA: 0x000CAA2C File Offset: 0x000C8C2C
		// (set) Token: 0x06004B3A RID: 19258 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool HideSurroundingHtml
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value that specifies whether the model is a complex type.</summary>
		/// <returns>true if the model is a complex type; otherwise, false.</returns>
		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x06004B3B RID: 19259 RVA: 0x000CAA48 File Offset: 0x000C8C48
		public virtual bool IsComplexType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that specifies whether the model is a nullable value type.</summary>
		/// <returns>true if the model is a nullable value type; otherwise, false.</returns>
		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x06004B3C RID: 19260 RVA: 0x000CAA64 File Offset: 0x000C8C64
		public bool IsNullableValueType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets a value that specifies whether the model can be updated.</summary>
		/// <returns>true if the model is read-only; otherwise, false The default is false.</returns>
		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x06004B3D RID: 19261 RVA: 0x000CAA80 File Offset: 0x000C8C80
		// (set) Token: 0x06004B3E RID: 19262 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool IsReadOnly
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether the property is required.</summary>
		/// <returns>true if the property is required; otherwise, false. The default is true for non-nullable value types and false for all other types.</returns>
		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x06004B3F RID: 19263 RVA: 0x000CAA9C File Offset: 0x000C8C9C
		// (set) Token: 0x06004B40 RID: 19264 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool IsRequired
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the model object.</summary>
		/// <returns>The model object.</returns>
		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x06004B41 RID: 19265 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B42 RID: 19266 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public object Model
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the type of the model.</summary>
		/// <returns>The type of the model.</returns>
		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x06004B43 RID: 19267 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Type ModelType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the string that should be displayed when the model is null.</summary>
		/// <returns>The string to display when the model is null.</returns>
		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x06004B44 RID: 19268 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B45 RID: 19269 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string NullDisplayText
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets an integer that indicates the order in which to display this property relative to other properties.</summary>
		/// <returns>The relative order in which to display this property.</returns>
		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x06004B46 RID: 19270 RVA: 0x000CAAB8 File Offset: 0x000C8CB8
		// (set) Token: 0x06004B47 RID: 19271 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual int Order
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a collection of model metadata objects that describe the properties of the model.</summary>
		/// <returns>Metadata for all properties in the model.</returns>
		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x06004B48 RID: 19272 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public virtual IEnumerable<ModelMetadata> Properties
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the name of the property.</summary>
		/// <returns>The name of the property if the model is a property; otherwise, null.</returns>
		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x06004B49 RID: 19273 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string PropertyName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the provider object for this metadata.</summary>
		/// <returns>The provider object for this metadata.</returns>
		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x06004B4A RID: 19274 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B4B RID: 19275 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected ModelMetadataProvider Provider
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether request validation is enabled.</summary>
		/// <returns>true if request validation is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x06004B4C RID: 19276 RVA: 0x000CAAD4 File Offset: 0x000C8CD4
		// (set) Token: 0x06004B4D RID: 19277 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool RequestValidationEnabled
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a short version of the display name.</summary>
		/// <returns>The text to display when a short version of the model name is required.</returns>
		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x06004B4E RID: 19278 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B4F RID: 19279 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ShortDisplayName
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether the model should be displayed in the UI in display mode (as opposed to edit mode).</summary>
		/// <returns>true if the model should be displayed in display mode; otherwise, false. The default is true.</returns>
		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x06004B50 RID: 19280 RVA: 0x000CAAF0 File Offset: 0x000C8CF0
		// (set) Token: 0x06004B51 RID: 19281 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool ShowForDisplay
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether the property should be displayed in edit mode (as opposed to display mode).</summary>
		/// <returns>true if the property should be displayed in edit mode; otherwise, false. The default is true.</returns>
		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x06004B52 RID: 19282 RVA: 0x000CAB0C File Offset: 0x000C8D0C
		// (set) Token: 0x06004B53 RID: 19283 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool ShowForEdit
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets text to display for the model when the model represents a complex object</summary>
		/// <returns>Text to display in UI for a complex object. The default value is determined by calling the <see cref="M:System.Web.ModelBinding.ModelMetadata.GetSimpleDisplayText" /> method.</returns>
		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x06004B54 RID: 19284 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B55 RID: 19285 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string SimpleDisplayText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates what template (data control) should be used in UI for the model.</summary>
		/// <returns>A value that indicates what template (data control) should be used in UI for the model.</returns>
		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x06004B56 RID: 19286 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B57 RID: 19287 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string TemplateHint
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets watermark text for a text box.</summary>
		/// <returns>The watermark text.</returns>
		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x06004B58 RID: 19288 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004B59 RID: 19289 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string Watermark
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Returns text to display as the name of the model in UI.</summary>
		/// <returns>Text to display as the name of the model in UI. The default is the value of the <see cref="P:System.Web.ModelBinding.ModelMetadata.DisplayName" /> property if that value is not null. If the <see cref="P:System.Web.ModelBinding.ModelMetadata.DisplayName" /> property is null, the default value is the value of the <see cref="P:System.Web.ModelBinding.ModelMetadata.PropertyName" /> property. If the <see cref="P:System.Web.ModelBinding.ModelMetadata.PropertyName" /> property is null, the default value comes from the <see cref="P:System.Reflection.MemberInfo.Name" /> property of the <see cref="P:System.Web.ModelBinding.ModelMetadata.ModelType" /> property.</returns>
		// Token: 0x06004B5A RID: 19290 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string GetDisplayName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns text to display for the model when the model represents a complex object.</summary>
		/// <returns>The property value is derived by examining the following sequence of related values until a return value is found.If the <see cref="P:System.Web.ModelBinding.ModelMetadata.SimpleDisplayText" /> property value is not null, that value is returned.If the <see cref="P:System.Web.ModelBinding.ModelMetadata.Model" /> property is null, the <see cref="P:System.Web.ModelBinding.ModelMetadata.NullDisplayText" /> property value is returned. If an attempt to convert the <see cref="P:System.Web.ModelBinding.ModelMetadata.Model" /> property to a string returns null, an empty string is returned. If the string conversion of the <see cref="P:System.Web.ModelBinding.ModelMetadata.Model" /> property value is the same as the <see cref="P:System.Type.FullName" /> property of the <see cref="P:System.Web.ModelBinding.ModelMetadata.Model" /> property type, that value is returned. If there are no properties in the <see cref="P:System.Web.ModelBinding.ModelMetadata.Properties" /> collection, an empty string is returned.If none of the preceding tests have returned a value, the return value comes from the first property in the <see cref="P:System.Web.ModelBinding.ModelMetadata.Properties" /> collection. If the <see cref="P:System.Web.ModelBinding.ModelMetadata.Model" /> property of the first property is null, the <see cref="P:System.Web.ModelBinding.ModelMetadata.NullDisplayText" /> property value is returned; otherwise the result of converting the first property's <see cref="P:System.Web.ModelBinding.ModelMetadata.Model" /> property to a string is returned.</returns>
		// Token: 0x06004B5B RID: 19291 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual string GetSimpleDisplayText()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of validators that apply to the model.</summary>
		/// <returns>A collection of validators.</returns>
		/// <param name="context">The model binding execution context.</param>
		// Token: 0x06004B5C RID: 19292 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public virtual IEnumerable<ModelValidator> GetValidators(ModelBindingExecutionContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>An integer value that is always set to 10000. </summary>
		// Token: 0x040025E1 RID: 9697
		public const int DefaultOrder = 10000;
	}
}
