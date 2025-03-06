namespace WebTest5.Components.Model
{
    public class LoginSingleton
    {
        private static LoginSingleton instance;
        public bool isLoggedIn;
        public bool isManager;
        public bool isRecepcionist;
        public bool isEmployee;

        public async static Task<LoginSingleton> GetInstance()
        {

            if (instance == null)
                instance = new LoginSingleton();
            return instance;
        }

    }
}
