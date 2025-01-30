namespace TaskApp
{
    public partial class App : Application
    {
        public static string LoggedInUserame = null;
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
