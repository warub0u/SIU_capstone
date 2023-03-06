namespace LoginAPI.Repo
{
    public class Generator: IGenerator
    {
        Guid guid;
        public Generator()
        {
            guid = Guid.NewGuid();
        }
        public string GetRecovery()
        {
            return guid.ToString();
        }
    }
}
