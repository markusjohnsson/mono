using JSIL.Meta;

namespace System.Reflection
{
    public abstract class MethodBase : MemberInfo
    {
        [JSReplacement("($thisObject[$this._data.mangledName].call($thisObject, $arguments))")]
        public extern object Invoke(object thisObject, object[] arguments);
    }
}
