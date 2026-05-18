using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Battler.BattleSystem.Units
{
    public abstract class Spawner<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T _prefab;

        private List<T> _activeElements = new();
        private int _poolCapacity = 50;
        private int _poolMaxSize = 100;

        protected T Prefab => _prefab;
        protected ObjectPool<T> Pool { get; private set; }

        private void Awake()
        {
            Pool = new ObjectPool<T>(
                createFunc: () => Instantiate(_prefab),
                actionOnGet: (obj) => obj.gameObject.SetActive(true),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );

            OnAwake();
        }

        public virtual void Restart()
        {
            while (_activeElements.Count > 0)
                Release(_activeElements[0]);

            _activeElements.Clear();
        }

        protected virtual void OnAwake() { }

        protected abstract void Release(T item);

        protected bool TryAddToActiveList(T item)
        {
            if (_activeElements.Contains(item))
                return false;

            _activeElements.Add(item);
            return true;
        }

        protected bool TryRemoveFromActiveList(T item)
        {
            if (_activeElements.Contains(item) == false)
                return false;

            _activeElements.Remove(item);
            return true;
        }
    }
}
