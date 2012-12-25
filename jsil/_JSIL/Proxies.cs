﻿using System;

namespace JSIL.Proxy {
    public enum JSProxyMemberPolicy {
        ReplaceDeclared,
        ReplaceNone
    }

    public enum JSProxyAttributePolicy {
        ReplaceDeclared,
        ReplaceAll
    }

    public enum JSProxyInterfacePolicy {
        ReplaceNone,
        ReplaceDeclared,
        ReplaceAll
    }

    /// <summary>
    /// Specifies that a type should be treated as a proxy for another type, replacing the target type's members and/or attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class JSProxy : Attribute {
        /// <param name="type">The type to proxy.</param>
        /// <param name="memberPolicy">Determines how members defined in the proxied type should be replaced with members defined by the proxy type.</param>
        /// <param name="attributePolicy">Determines how how attributes defined in the proxied type should be replaced with attributes attached to the proxy type.</param>
        /// <param name="interfacePolicy">Determines how how interfaces defined in the proxied type should be replaced with interfaces attached to the proxy type.</param>
        public JSProxy (
            Type type,
            JSProxyMemberPolicy memberPolicy = JSProxyMemberPolicy.ReplaceDeclared,
            JSProxyAttributePolicy attributePolicy = JSProxyAttributePolicy.ReplaceDeclared,
            JSProxyInterfacePolicy interfacePolicy = JSProxyInterfacePolicy.ReplaceDeclared,
            bool inheritable = true
        ) {
        }

        /// <param name="types">The types to proxy.</param>
        /// <param name="memberPolicy">Determines how members defined in the proxied type should be replaced with members defined by the proxy type.</param>
        /// <param name="attributePolicy">Determines how how attributes defined in the proxied type should be replaced with attributes attached to the proxy type.</param>
        /// <param name="interfacePolicy">Determines how how interfaces defined in the proxied type should be replaced with interfaces attached to the proxy type.</param>
        public JSProxy (
            Type[] types,
            JSProxyMemberPolicy memberPolicy = JSProxyMemberPolicy.ReplaceDeclared,
            JSProxyAttributePolicy attributePolicy = JSProxyAttributePolicy.ReplaceDeclared,
            JSProxyInterfacePolicy interfacePolicy = JSProxyInterfacePolicy.ReplaceDeclared,
            bool inheritable = true
        ) {
        }

        /// <param name="typeName">The type to proxy.</param>
        /// <param name="memberPolicy">Determines how members defined in the proxied type should be replaced with members defined by the proxy type.</param>
        /// <param name="attributePolicy">Determines how how attributes defined in the proxied type should be replaced with attributes attached to the proxy type.</param>
        /// <param name="interfacePolicy">Determines how how interfaces defined in the proxied type should be replaced with interfaces attached to the proxy type.</param>
        public JSProxy (
            string typeName,
            JSProxyMemberPolicy memberPolicy = JSProxyMemberPolicy.ReplaceDeclared,
            JSProxyAttributePolicy attributePolicy = JSProxyAttributePolicy.ReplaceDeclared,
            JSProxyInterfacePolicy interfacePolicy = JSProxyInterfacePolicy.ReplaceDeclared,
            bool inheritable = true
        ) {
        }

        /// <param name="typeNames">The types to proxy.</param>
        /// <param name="memberPolicy">Determines how members defined in the proxied type should be replaced with members defined by the proxy type.</param>
        /// <param name="attributePolicy">Determines how how attributes defined in the proxied type should be replaced with attributes attached to the proxy type.</param>
        /// <param name="interfacePolicy">Determines how how interfaces defined in the proxied type should be replaced with interfaces attached to the proxy type.</param>
        public JSProxy (
            string[] typeNames,
            JSProxyMemberPolicy memberPolicy = JSProxyMemberPolicy.ReplaceDeclared,
            JSProxyAttributePolicy attributePolicy = JSProxyAttributePolicy.ReplaceDeclared,
            JSProxyInterfacePolicy interfacePolicy = JSProxyInterfacePolicy.ReplaceDeclared,
            bool inheritable = true
        ) {
        }
    }

    [AttributeUsage(
        AttributeTargets.Event | AttributeTargets.Property |
        AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Field
    )]
    public class JSNeverReplace : Attribute {
    }

    [AttributeUsage(
        AttributeTargets.Event | AttributeTargets.Property |
        AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Field
    )]
    public class JSNeverInherit : Attribute {
    }

    /// <summary>
    /// Use this as a stand-in type when defining a proxy to specify that any type is valid for the given parameter/return type.
    /// </summary>
    public abstract class AnyType {
        private AnyType () {
            throw new InvalidOperationException();
        }
    }
}
