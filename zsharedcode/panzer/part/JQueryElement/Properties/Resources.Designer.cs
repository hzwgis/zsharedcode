﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.237
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace zoyobar.shared.panzer.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("zoyobar.shared.panzer.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 (function(j){function s(a){if(n.call(this,a,&quot;fill&quot;)){for(var b=a.__repeater,c=0;c&lt;b.filterfield.length;c++){var d=j(&quot;#&quot;+b.filterfield[c]+&quot;_filter&quot;);b.filtercondition[b.filterfield[c]]=d.length!=0?d.val():null}if(null!=b.prefill&amp;&amp;(c=b.prefill.call(this,a,{condition:b.filtercondition}),null!=c&amp;&amp;!c)){k.call(this,a,&quot;fill&quot;);return}null!=b.fill?b.fill.call(this,a,{filtercondition:b.filtercondition,sortcondition:b.sortcondition,callback:C}):C.call(this,a,data,!0)}}function C(a,b,c){k.call(this,a,&quot;fill&quot;);
        ///var d=a._ [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string repeater_min {
            get {
                return ResourceManager.GetString("repeater_min", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 (function(c){function e(a){d(a);var b=a.__timer;b.handler=setInterval(function(){null!=b.tick&amp;&amp;b.tick.call(this,a,{})},b.interval)}function d(a){a=a.__timer;null!=a.handler&amp;&amp;clearInterval(a.handler)}c.fn.__timer=function(){if(this.length==0)return this;var a=this.get(0),b=&quot;create&quot;;if(typeof arguments[0]==&quot;string&quot;){if(null==a.__timer)return this;b=arguments[0]==&quot;option&quot;?arguments.length==2?&quot;get&quot;:&quot;set&quot;:&quot;method&quot;}else arguments[0]=c.extend({},c.fn.__timer.defaults,arguments[0]);switch(b){case &quot;get&quot;:return a.__t [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string timer_min {
            get {
                return ResourceManager.GetString("timer_min", resourceCulture);
            }
        }
    }
}
