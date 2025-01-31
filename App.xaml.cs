namespace TaskApp
{
    public partial class App : Application
    {
        public static string LoggedInUserame = null;
        public static int SelectedCategoryId = 0;
        public static int SelectedTaskId = 0;
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
