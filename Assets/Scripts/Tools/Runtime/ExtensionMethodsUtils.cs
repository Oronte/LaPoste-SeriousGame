using System;
using Unity.VisualScripting;
using UnityEngine;

public static class ExtensionMethodsUtils
{
    #region TryGetComponent
        #region Children
    public static bool TryGetComponentInChildren<T>(this GameObject _this, out T _component) where T : Component
    {
        _component = _this.GetComponentInChildren<T>();
        return _component != null;
    }
    public static bool TryGetComponentInChildren(this GameObject _this, Type _type, out Component _component)
    {
        _component = _this.GetComponentInChildren(_type);
        return _component != null;
    }
    public static bool TryGetComponentInChildren<T>(this GameObject _this, out T _component, bool _includeInactive) where T : Component
    {
        _component = _this.GetComponentInChildren<T>(_includeInactive);
        return _component != null;
    }
    public static bool TryGetComponentInChildren(this GameObject _this, Type _type, out Component _component, bool _includeInactive)
    {
        _component = _this.GetComponentInChildren(_type, _includeInactive);
        return _component != null;
    }
    public static bool TryGetComponentInChildren<T>(this Component _this, out T _component) where T : Component
    {
        _component = _this.GetComponentInChildren<T>();
        return _component != null;
    }
    public static bool TryGetComponentInChildren(this Component _this, Type _type, out Component _component)
    {
        _component = _this.GetComponentInChildren(_type);
        return _component != null;
    }
    public static bool TryGetComponentInChildren<T>(this Component _this, out T _component, bool _includeInactive) where T : Component
    {
        _component = _this.GetComponentInChildren<T>(_includeInactive);
        return _component != null;
    }
    public static bool TryGetComponentInChildren(this Component _this, Type _type, out Component _component, bool _includeInactive)
    {
        _component = _this.GetComponentInChildren(_type, _includeInactive);
        return _component != null;
    }
    #endregion
        #region Parent
    public static bool TryGetComponentInParent<T>(this GameObject _this, out T _component) where T : Component
    {
        _component = _this.GetComponentInParent<T>();
        return _component != null;
    }
    public static bool TryGetComponentInParent(this GameObject _this, Type _type, out Component _component)
    {
        _component = _this.GetComponentInParent(_type);
        return _component != null;
    }
    public static bool GetComponentInParent<T>(this GameObject _this, out T _component, bool _includeInactive) where T : Component
    {
        _component = _this.GetComponentInParent<T>(_includeInactive);
        return _component != null;
    }
    public static bool GetComponentInParent(this GameObject _this, Type _type, out Component _component, bool _includeInactive)
    {
        _component = _this.GetComponentInParent(_type, _includeInactive);
        return _component != null;
    }
    public static bool TryGetComponentInParent<T>(this Component _this, out T _component) where T : Component
    {
        _component = _this.GetComponentInParent<T>();
        return _component != null;
    }
    public static bool GetComponentInParent(this Component _this, Type _type, out Component _component)
    {
        _component = _this.GetComponentInParent(_type);
        return _component != null;
    }
    public static bool GetComponentInParent<T>(this Component _this, out T _component, bool _includeInactive) where T : Component
    {
        _component = _this.GetComponentInParent<T>(_includeInactive);
        return _component != null;
    }
    public static bool GetComponentInParent(this Component _this, Type _type, out Component _component, bool _includeInactive)
    {
        _component = _this.GetComponentInParent(_type, _includeInactive);
        return _component != null;
    }
    #endregion
    #endregion
    #region TryGetComponents
        #region Children
    public static bool TryGetComponentsInChildren<T>(this GameObject _this, out T[] _components) where T : Component
    {
        _components = _this.GetComponentsInChildren<T>();
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInChildren(this GameObject _this, Type _type, out Component[] _components)
    {
        _components = _this.GetComponentsInChildren(_type);
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInChildren<T>(this GameObject _this, out T[] _components, bool _includeInactive) where T : Component
    {
        _components = _this.GetComponentsInChildren<T>(_includeInactive);
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInChildren(this GameObject _this, Type _type, out Component[] _components, bool _includeInactive)
    {
        _components = _this.GetComponentsInChildren(_type, _includeInactive);
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInChildren<T>(this Component _this, out T[] _components) where T : Component
    {
        _components = _this.GetComponentsInChildren<T>();
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInChildren(this Component _this, Type _type, out Component[] _components)
    {
        _components = _this.GetComponentsInChildren(_type);
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInChildren<T>(this Component _this, out T[] _components, bool _includeInactive) where T : Component
    {
        _components = _this.GetComponentsInChildren<T>(_includeInactive);
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInChildren(this Component _this, Type _type, out Component[] _components, bool _includeInactive)
    {
        _components = _this.GetComponentsInChildren(_type, _includeInactive);
        return _components.Length <= 0;
    }
    #endregion
        #region Parent
    public static bool TryGetComponentsInParent<T>(this GameObject _this, out T[] _components) where T : Component
    {
        _components = _this.GetComponentsInParent<T>();
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInParent(this GameObject _this, Type _type, out Component[] _components)
    {
        _components = _this.GetComponentsInParent(_type);
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInParent<T>(this GameObject _this, out T[] _components, bool _includeInactive) where T : Component
    {
        _components = _this.GetComponentsInParent<T>(_includeInactive);
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInParent(this GameObject _this, Type _type, out Component[] _components, bool _includeInactive)
    {
        _components = _this.GetComponentsInParent(_type, _includeInactive);
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInParent<T>(this Component _this, out T[] _components) where T : Component
    {
        _components = _this.GetComponentsInParent<T>();
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInParent(this Component _this, Type _type, out Component[] _components)
    {
        _components = _this.GetComponentsInParent(_type);
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInParent<T>(this Component _this, out T[] _components, bool _includeInactive) where T : Component
    {
        _components = _this.GetComponentsInParent<T>(_includeInactive);
        return _components.Length <= 0;
    }
    public static bool TryGetComponentsInParent(this Component _this, Type _type, out Component[] _components, bool _includeInactive)
    {
        _components = _this.GetComponentsInParent(_type, _includeInactive);
        return _components.Length <= 0;
    }
    #endregion
    #endregion
}
