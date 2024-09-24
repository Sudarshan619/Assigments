namespace Day15_PatientLogin.Interface
{
    public interface ILogin<K,T,V> where V : class
    {
        public V Get(K key, T pass);
        public List<V> GetAll();
        public V ChangePassword(V Item);
        public V Delete(K key, T pass);

        public V Login(K key, T pass);
    }
}
