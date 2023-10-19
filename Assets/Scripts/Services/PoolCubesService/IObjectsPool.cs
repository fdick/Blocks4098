namespace Code.Pools
{
    public interface IObjectsPool<T>
    {
        public T Get(int index);
        public void Return(T obj);
    }
}