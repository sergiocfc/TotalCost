namespace TotalCost.UI.Lib
{
    public class RepositoryFactory
    {
        private static RepositoryFactory _default;

        public static RepositoryFactory Default
        {
            get {
                if (_default == null)
                    _default = new RepositoryFactory();
                return _default; }
        }

        public IDataRepository GetRepository()
        {
            return new Repository();
        }
    }
}
