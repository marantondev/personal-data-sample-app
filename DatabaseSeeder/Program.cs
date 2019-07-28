namespace PersonalInfoSampleApp.DatabaseSeeder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                new MainRoutine(args[0]).Run();
            }
            new MainRoutine().Run();
        }
    }
}