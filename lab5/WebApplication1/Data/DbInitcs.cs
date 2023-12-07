namespace WebApplication1.Data
{
    public class DbInitcs
    {
        public static void Initialize(DbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
