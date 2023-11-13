using System.Collections.Generic;

namespace CacheProvider
{
    public class CacheProvider<TKey, TVal>
    {
        private static CacheProvider<TKey, TVal> _instance;
        private LinkedList<CacheItem<TKey, TVal>> _cache;
        private static int _maxSize;
        public static readonly object lockob = new object();
        private CacheProvider()
        {
            _cache = new LinkedList<CacheItem<TKey, TVal>>();

        }
        public static CacheProvider<TKey, TVal> GetCacheInstance(int size)
        {
            if (_instance is null)
            {
                _instance = new CacheProvider<TKey, TVal>();
                _maxSize = size;
                return _instance;
            }
            else
            {
                return _instance;
            }


        }
        public bool AddItem(TKey key, TVal val,out string evictedKey)
        {
            try
            {
                lock (lockob)
                {
                    if (_cache.Count() < _maxSize)
                    {
                        //check if the item exists 
                        LinkedListNode<CacheItem<TKey, TVal>>? firstElement = _cache.First;
                        CacheItem<TKey, TVal> cacheIt = new CacheItem<TKey, TVal>(key, val);
                        LinkedListNode<CacheItem<TKey, TVal>> linkedListNode = new LinkedListNode<CacheItem<TKey, TVal>>(cacheIt);
                        _cache.AddFirst(linkedListNode);
                        evictedKey=string.Empty;
                        return true;

                    }
                    else
                    {
                        LinkedListNode<CacheItem<TKey, TVal>>? firstElement = _cache.First;
                        CacheItem<TKey, TVal> cacheIt = new CacheItem<TKey, TVal>(key, val);
                        LinkedListNode<CacheItem<TKey, TVal>> linkedListNode = new LinkedListNode<CacheItem<TKey, TVal>>(cacheIt);
                        evictedKey = _cache.Last.Value.key.ToString();
                        _cache.RemoveLast();
                        _cache.AddFirst(linkedListNode);
                        
                        return true;

                    }
                }



            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public TVal GetItem(TKey key)
        {
            try
            {
                lock (lockob)
                {


                    LinkedListNode<CacheItem<TKey, TVal>>? firstElement = _cache.First;
                    LinkedListNode<CacheItem<TKey, TVal>>? foundElement = null;
                    while (firstElement != null)
                    {
                        if (firstElement.Value.key.Equals(key))
                        {
                            foundElement = firstElement;
                            break;
                        }
                        firstElement = firstElement.Next;
                    }
                    if (foundElement != null)
                    {
                        CacheItem<TKey, TVal> cacheIt = new CacheItem<TKey, TVal>(foundElement.Value.key, foundElement.Value.value);
                        LinkedListNode<CacheItem<TKey, TVal>> linkedListNode = new LinkedListNode<CacheItem<TKey, TVal>>(cacheIt);

                        _cache.Remove(foundElement);
                        _cache.AddFirst(linkedListNode);

                        return foundElement.Value.value;
                    }
                    else
                    {
                        return default(TVal);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


    }

    class CacheItem<K, V>
    {
        public CacheItem(K k, V v)
        {
            key = k;
            value = v;
        }
        public K key;
        public V value;
    }
}