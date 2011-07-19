﻿/*
 * wiki: http://code.google.com/p/zsharedcode/wiki/JQueryUI
 * 如果您无法运行此文件, 可能由于缺少相关类文件, 请下载解决方案后重试, 具体请参考: http://code.google.com/p/zsharedcode/wiki/HowToDownloadAndUse
 * 原始代码: http://zsharedcode.googlecode.com/svn/trunk/zsharedcode/panzer/.class/web/jqueryui/JQueryUI.cs
 * 版本: .net 4.0, 其它版本可能有所不同
 * 
 * 使用许可: 此文件是开源共享免费的, 但您仍然需要遵守, 下载并将 panzer 许可证 http://zsharedcode.googlecode.com/svn/trunk/zsharedcode/panzer/panzer.license.txt 包含在你的产品中.
 * */

using System.Collections.Generic;

namespace zoyobar.shared.panzer.web.jqueryui
{

	#region " JQueryUI "
	/// <summary>
	/// jQuery UI 辅助类.
	/// </summary>
	public sealed class JQueryUI
		: JQuery
	{

		private static string makeOptionExpression ( List<Option> options )
		{

			if ( null == options || options.Count == 0 )
				return string.Empty;

			string optionExpression = "{";

			foreach ( Option option in options )
				if ( null != option && option.Type != OptionType.none && option.Value != string.Empty )
					optionExpression += string.Format ( " {0}: {1},", option.Type, option.Value );

			return optionExpression.TrimEnd ( ',' ) + " }";
		}

		private static string makeParameterExpression ( List<Parameter> parameters, bool isWebService, string quote )
		{

			if ( null == quote )
				quote = string.Empty;

			if ( null == parameters || parameters.Count == 0 )
				if ( isWebService )
					return quote + "{ }" + quote;
				else
					return "{ }";


			string parameterExpression = string.Empty;

			foreach ( Parameter parameter in parameters )
				if ( null != parameter && parameter.Value != string.Empty )
					switch ( parameter.Type )
					{
						case ParameterType.Selector:

							if ( isWebService )
							{

								if ( parameterExpression != string.Empty )
									parameterExpression += string.Format ( " + {0} ,{0}", quote );

								parameterExpression += string.Format ( " + {0}{1}: {0} + {0}\\{0}{0} + {2} + {0}\\{0}{0}", quote, parameter.Name, JQuery.Create ( parameter.Value ).Val ( ).Code );
							}
							else
								parameterExpression += string.Format ( " {0}: {1},", parameter.Name, JQuery.Create ( parameter.Value ).Val ( ).Code );

							break;

						case ParameterType.Expression:

							if ( isWebService )
							{

								if ( parameterExpression != string.Empty )
									parameterExpression += string.Format ( " + {0} ,{0}", quote );

								parameterExpression += string.Format ( " + {0}{1}: {0} + {2}", quote, parameter.Name, parameter.Value );
							}
							else
								parameterExpression += string.Format ( " {0}: {1},", parameter.Name, parameter.Value );

							break;
					}

			if ( isWebService )
				parameterExpression = quote + "{" + quote + parameterExpression + " + " + quote + "}" + quote;
			else
				parameterExpression = "{" + parameterExpression.TrimEnd ( ',' ) + "}";

			return parameterExpression;
		}

		/// <summary>
		/// Ajax 操作.
		/// </summary>
		/// <param name="setting">Ajax 相关设置.</param>
		/// <returns>更新后的 JQueryUI 对象.</returns>
		public static JQuery Ajax ( AjaxSetting setting )
		{

			if ( string.IsNullOrEmpty ( setting.Url ) )
				return null;

			string quote;

			if ( setting.IsSingleQuote )
				quote = "'";
			else
				quote = "\"";

			string data;
			bool isWebService = !string.IsNullOrEmpty ( setting.MethodName );

			if ( string.IsNullOrEmpty ( setting.Form ) )
				data = makeParameterExpression ( setting.Parameters, isWebService, quote );
			else
				data = JQuery.Create ( setting.Form ).Serialize ( ).Code;

			string map = string.Format ( "url: {0}{1}{0}, dataType: {0}{2}{0}, data: {3}, type: {0}{4}{0}",
				quote,
				setting.Url + ( isWebService ? "/" + setting.MethodName : string.Empty ),
				setting.DataType,
				data,
				setting.Type
				);

			if ( !string.IsNullOrEmpty ( setting.ContentType ) )
				map += string.Format ( ", contentType: {0}{1}{0}", quote, setting.ContentType );

			foreach ( Event @event in setting.Events )
				if ( @event.Type != EventType.none && @event.Type != EventType.__init )
					map += ", " + @event.Type + ": " + @event.Value;

			return JQuery.Create ( false, true ).Ajax ( "{" + map + "}" );
		}

		#region " 构造 "

		/// <summary>
		/// 从另一个 JQuery 上创建具有相同 Code 属性的 JQuery UI 实例.
		/// </summary>
		/// <param name="jQuery">jQuery 实例, 新实例将复制其 Code 属性.</param>
		/// <returns>JQuery UI 实例.</returns>
		public static JQueryUI Create ( JQueryUI jQuery )
		{ return new JQueryUI ( jQuery ); }

		/// <summary>
		/// 创建使用别名的空的 JQuery UI.
		/// </summary>
		/// <returns>JQuery UI 实例.</returns>
		public static new JQueryUI Create ( )
		{ return Create ( null, null, true ); }
		/// <summary>
		/// 创建空的 JQuery UI.
		/// </summary>
		/// <param name="isAlias">是否在脚本中使用 $ 作为 jQuery 的别名.</param>
		/// <returns>JQuery UI 实例.</returns>
		public static new JQueryUI Create ( bool isAlias )
		{ return Create ( null, null, isAlias ); }
		/// <summary>
		/// 创建使用别名的 JQuery UI.
		/// </summary>
		/// <param name="expressionI">可以是选择器, 比如: "'body table .red'", 也可以是 DOM 元素, 比如: "document.getElementById('myTable')", "[document.getElementById('myTable1'), document.getElementById('myTable2')]", 也可以是脚本中另一个 jQuery 实例, 比如: "myJQuery", 也可以是页面载入的回调函数, 比如: "function(){}", 或者是一段要添加的 html 代码, 比如: "'&lt;stong&gt;&lt;/stong&gt;'".</param>
		/// <returns>JQuery UI 实例.</returns>
		public static new JQueryUI Create ( string expressionI )
		{ return Create ( expressionI, null, true ); }
		/// <summary>
		/// 创建 JQuery UI.
		/// </summary>
		/// <param name="expressionI">可以是选择器, 比如: "'body table .red'", 也可以是 DOM 元素, 比如: "document.getElementById('myTable')", "[document.getElementById('myTable1'), document.getElementById('myTable2')]", 也可以是脚本中另一个 jQuery 实例, 比如: "myJQuery", 也可以是页面载入的回调函数, 比如: "function(){}", 或者是一段要添加的 html 代码, 比如: "'&lt;stong&gt;&lt;/stong&gt;'".</param>
		/// <param name="isAlias">是否在脚本中使用 $ 作为 jQuery 的别名.</param>
		/// <returns>JQuery UI 实例.</returns>
		public static new JQueryUI Create ( string expressionI, bool isAlias )
		{ return Create ( expressionI, null, isAlias ); }
		/// <summary>
		/// 创建使用别名的 JQuery UI.
		/// </summary>
		/// <param name="expressionI">可以是选择器, 比如: "'body table .red'", 或者是一段要添加的 html 代码, 比如: "'&lt;stong&gt;&lt;/stong&gt;'".</param>
		/// <param name="expressionII">当 expressionI 是选择器时, expressionII 是一个 DOM 元素, 指定搜索上下文, 比如: "document.body", 当 expressionI 是一段 html 代码时, expressionII 可以是 document 元素, 指定 html 代码创建位置, 比如: "document", 也可以是属性集合, 用来初始化只包含单一元素 html 代码元素, 比如: "{type: 'text'}".</param>
		/// <returns>JQuery UI 实例.</returns>
		public static new JQueryUI Create ( string expressionI, string expressionII )
		{ return Create ( expressionI, expressionII, true ); }
		/// <summary>
		/// 创建 JQuery UI.
		/// </summary>
		/// <param name="expressionI">可以是选择器, 比如: "'body table .red'", 或者是一段要添加的 html 代码, 比如: "'&lt;stong&gt;&lt;/stong&gt;'".</param>
		/// <param name="expressionII">当 expressionI 是选择器时, expressionII 是一个 DOM 元素, 指定搜索上下文, 比如: "document.body", 当 expressionI 是一段 html 代码时, expressionII 可以是 document 元素, 指定 html 代码创建位置, 比如: "document", 也可以是属性集合, 用来初始化只包含单一元素 html 代码元素, 比如: "{type: 'text'}".</param>
		/// <param name="isAlias">是否在脚本中使用 $ 作为 jQuery 的别名.</param>
		/// <returns>JQuery UI 实例.</returns>
		public static new JQueryUI Create ( string expressionI, string expressionII, bool isAlias )
		{ return new JQueryUI ( expressionI, expressionII, isAlias ); }

		/// <summary>
		/// 创建 JQuery UI.
		/// </summary>
		/// <param name="isInstance">是否创建为实例, 为 false, 则创建为 $, 否则为 $().</param>
		/// <param name="isAlias">是否在脚本中使用 $ 作为 jQuery 的别名.</param>
		/// <returns>JQuery UI 实例.</returns>
		public static new JQueryUI Create ( bool isInstance, bool isAlias )
		{ return new JQueryUI ( isInstance, isAlias ); }


		/// <summary>
		/// 从另一个 JQuery 上创建具有相同 Code 属性的 JQuery UI 实例.
		/// </summary>
		/// <param name="jQuery">jQuery 实例, 新实例将复制其 Code 属性.</param>
		public JQueryUI ( JQueryUI jQuery )
			: base ( jQuery )
		{ }

		/// <summary>
		/// 创建使用别名的空的 JQuery UI.
		/// </summary>
		public JQueryUI ( )
			: this ( null, null, true )
		{ }
		/// <summary>
		/// 创建空的 JQuery UI.
		/// </summary>
		/// <param name="isAlias">是否在脚本中使用 $ 作为 jQuery 的别名.</param>
		public JQueryUI ( bool isAlias )
			: this ( null, null, isAlias )
		{ }
		/// <summary>
		/// 创建使用别名的 JQuery UI.
		/// </summary>
		/// <param name="expressionI">可以是选择器, 比如: "'body table .red'", 也可以是 DOM 元素, 比如: "document.getElementById('myTable')", "[document.getElementById('myTable1'), document.getElementById('myTable2')]", 也可以是脚本中另一个 jQuery 实例, 比如: "myJQuery", 也可以是页面载入的回调函数, 比如: "function(){}", 或者是一段要添加的 html 代码, 比如: "'&lt;stong&gt;&lt;/stong&gt;'".</param>
		public JQueryUI ( string expressionI )
			: this ( expressionI, null, true )
		{ }
		/// <summary>
		/// 创建 JQuery UI.
		/// </summary>
		/// <param name="expressionI">可以是选择器, 比如: "'body table .red'", 也可以是 DOM 元素, 比如: "document.getElementById('myTable')", "[document.getElementById('myTable1'), document.getElementById('myTable2')]", 也可以是脚本中另一个 jQuery 实例, 比如: "myJQuery", 也可以是页面载入的回调函数, 比如: "function(){}", 或者是一段要添加的 html 代码, 比如: "'&lt;stong&gt;&lt;/stong&gt;'".</param>
		/// <param name="isAlias">是否在脚本中使用 $ 作为 jQuery 的别名.</param>
		public JQueryUI ( string expressionI, bool isAlias )
			: this ( expressionI, null, isAlias )
		{ }
		/// <summary>
		/// 创建使用别名的 JQuery UI.
		/// </summary>
		/// <param name="expressionI">可以是选择器, 比如: "'body table .red'", 或者是一段要添加的 html 代码, 比如: "'&lt;stong&gt;&lt;/stong&gt;'".</param>
		/// <param name="expressionII">当 expressionI 是选择器时, expressionII 是一个 DOM 元素, 指定搜索上下文, 比如: "document.body", 当 expressionI 是一段 html 代码时, expressionII 可以是 document 元素, 指定 html 代码创建位置, 比如: "document", 也可以是属性集合, 用来初始化只包含单一元素 html 代码元素, 比如: "{type: 'text'}".</param>
		public JQueryUI ( string expressionI, string expressionII )
			: this ( expressionI, expressionII, true )
		{ }
		/// <summary>
		/// 创建 JQuery UI.
		/// </summary>
		/// <param name="expressionI">可以是选择器, 比如: "'body table .red'", 或者是一段要添加的 html 代码, 比如: "'&lt;stong&gt;&lt;/stong&gt;'".</param>
		/// <param name="expressionII">当 expressionI 是选择器时, expressionII 是一个 DOM 元素, 指定搜索上下文, 比如: "document.body", 当 expressionI 是一段 html 代码时, expressionII 可以是 document 元素, 指定 html 代码创建位置, 比如: "document", 也可以是属性集合, 用来初始化只包含单一元素 html 代码元素, 比如: "{type: 'text'}".</param>
		/// <param name="isAlias">是否在脚本中使用 $ 作为 jQuery 的别名.</param>
		public JQueryUI ( string expressionI, string expressionII, bool isAlias )
			: base ( expressionI, expressionII, isAlias )
		{ }

		/// <summary>
		/// 创建 JQuery UI.
		/// </summary>
		/// <param name="isInstance">是否创建为实例, 为 false, 则创建为 $, 否则为 $().</param>
		/// <param name="isAlias">是否在脚本中使用 $ 作为 jQuery 的别名.</param>
		public JQueryUI ( bool isInstance, bool isAlias )
			: base ( isInstance, isAlias )
		{ }

		#endregion

		/// <summary>
		/// 拖动操作.
		/// </summary>
		/// <param name="setting">拖动的相关设置.</param>
		/// <returns>更新后的 JQueryUI 对象.</returns>
		public JQueryUI Draggable ( DraggableSetting setting )
		{

			if ( null == setting || !setting.IsDraggable )
				return this;

			return this.Execute ( "draggable", makeOptionExpression ( setting.Options ) ) as JQueryUI;
		}

		/// <summary>
		/// 拖放操作.
		/// </summary>
		/// <param name="setting">拖放的相关设置.</param>
		/// <returns>更新后的 JQueryUI 对象.</returns>
		public JQueryUI Droppable ( DroppableSetting setting )
		{

			if ( null == setting || !setting.IsDroppable )
				return this;

			return this.Execute ( "droppable", makeOptionExpression ( setting.Options ) ) as JQueryUI;
		}

		/// <summary>
		/// 排列操作.
		/// </summary>
		/// <param name="setting">排列的相关设置.</param>
		/// <returns>更新后的 JQueryUI 对象.</returns>
		public JQueryUI Sortable ( SortableSetting setting )
		{

			if ( null == setting || !setting.IsSortable )
				return this;

			return this.Execute ( "sortable", makeOptionExpression ( setting.Options ) ) as JQueryUI;
		}

		/// <summary>
		/// 选中操作.
		/// </summary>
		/// <param name="setting">选中的相关设置.</param>
		/// <returns>更新后的 JQueryUI 对象.</returns>
		public JQueryUI Selectable ( SelectableSetting setting )
		{

			if ( null == setting || !setting.IsSelectable )
				return this;

			return this.Execute ( "selectable", makeOptionExpression ( setting.Options ) ) as JQueryUI;
		}

		/// <summary>
		/// 缩放操作.
		/// </summary>
		/// <param name="setting">缩放的相关设置.</param>
		/// <returns>更新后的 JQueryUI 对象.</returns>
		public JQueryUI Resizable ( ResizableSetting setting )
		{

			if ( null == setting || !setting.IsResizable )
				return this;

			return this.Execute ( "resizable", makeOptionExpression ( setting.Options ) ) as JQueryUI;
		}

		/// <summary>
		/// Widget 操作.
		/// </summary>
		/// <param name="setting">Widget 相关设置.</param>
		/// <returns>更新后的 JQueryUI 对象.</returns>
		public JQueryUI Widget ( WidgetSetting setting )
		{

			if ( null == setting || setting.WidgetType == WidgetType.none )
				return this;

			if ( setting.WidgetType != WidgetType.empty )
				this.Execute ( setting.WidgetType.ToString ( ), makeOptionExpression ( setting.Options ) );

			foreach ( Event @event in setting.Events )
				if ( @event.Type != EventType.none && @event.Type != EventType.__init )
					this.Execute ( @event.Type.ToString ( ), @event.Value );

			foreach ( AjaxSetting ajaxSetting in setting.AjaxSettings )
			{

				if ( ajaxSetting.WidgetEventType == EventType.none )
					continue;

				JQuery ajax = Ajax ( ajaxSetting );

				if ( null == ajax )
					continue;

				if ( ajaxSetting.WidgetEventType == EventType.__init )
					this.EndLine ( ).AppendCode ( ajax.Code );
				else
					this.Bind ( string.Format ( "'{0}'", ajaxSetting.WidgetEventType ), "function(e){" + ajax.Code + "}" );

				return this;

			}

			return this;
		}

	}
	#endregion

}
