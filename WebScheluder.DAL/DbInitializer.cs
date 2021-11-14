namespace WebScheluder.DAL
{
    public class DbInitializer
    {
        public static void Initialize(WebSchedulerContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
