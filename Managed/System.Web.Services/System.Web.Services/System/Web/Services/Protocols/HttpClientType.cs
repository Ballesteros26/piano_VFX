using System;
using System.Collections;
using System.Threading;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000030 RID: 48
	internal class HttpClientType
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00004D98 File Offset: 0x00002F98
		internal HttpClientType(Type type)
		{
			LogicalMethodInfo[] array = LogicalMethodInfo.Create(type.GetMethods(), LogicalMethodTypes.Sync);
			Hashtable hashtable = new Hashtable();
			foreach (LogicalMethodInfo logicalMethodInfo in array)
			{
				try
				{
					object[] customAttributes = logicalMethodInfo.GetCustomAttributes(typeof(HttpMethodAttribute));
					if (customAttributes.Length != 0)
					{
						HttpMethodAttribute httpMethodAttribute = (HttpMethodAttribute)customAttributes[0];
						HttpClientMethod httpClientMethod = new HttpClientMethod();
						httpClientMethod.readerType = httpMethodAttribute.ReturnFormatter;
						httpClientMethod.writerType = httpMethodAttribute.ParameterFormatter;
						httpClientMethod.methodInfo = logicalMethodInfo;
						HttpClientType.AddFormatter(hashtable, httpClientMethod.readerType, httpClientMethod);
						HttpClientType.AddFormatter(hashtable, httpClientMethod.writerType, httpClientMethod);
						this.methods.Add(logicalMethodInfo.Name, httpClientMethod);
					}
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					throw new InvalidOperationException(Res.GetString("WebReflectionError", new object[]
					{
						logicalMethodInfo.DeclaringType.FullName,
						logicalMethodInfo.Name
					}), ex);
				}
			}
			foreach (object obj in hashtable.Keys)
			{
				Type type2 = (Type)obj;
				ArrayList arrayList = (ArrayList)hashtable[type2];
				LogicalMethodInfo[] array2 = new LogicalMethodInfo[arrayList.Count];
				for (int j = 0; j < arrayList.Count; j++)
				{
					array2[j] = ((HttpClientMethod)arrayList[j]).methodInfo;
				}
				object[] initializers = MimeFormatter.GetInitializers(type2, array2);
				bool flag = typeof(MimeParameterWriter).IsAssignableFrom(type2);
				for (int k = 0; k < arrayList.Count; k++)
				{
					if (flag)
					{
						((HttpClientMethod)arrayList[k]).writerInitializer = initializers[k];
					}
					else
					{
						((HttpClientMethod)arrayList[k]).readerInitializer = initializers[k];
					}
				}
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004FC8 File Offset: 0x000031C8
		private static void AddFormatter(Hashtable formatterTypes, Type formatterType, HttpClientMethod method)
		{
			if (formatterType == null)
			{
				return;
			}
			ArrayList arrayList = (ArrayList)formatterTypes[formatterType];
			if (arrayList == null)
			{
				arrayList = new ArrayList();
				formatterTypes.Add(formatterType, arrayList);
			}
			arrayList.Add(method);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005005 File Offset: 0x00003205
		internal HttpClientMethod GetMethod(string name)
		{
			return (HttpClientMethod)this.methods[name];
		}

		// Token: 0x040001EC RID: 492
		private Hashtable methods = new Hashtable();
	}
}
