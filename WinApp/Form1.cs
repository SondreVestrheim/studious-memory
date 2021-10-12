using System.Windows.Forms;

namespace WinLiteApplication
{
    public partial class Form1 : Form
    {
        public UserSession User { get; set; }
        public Form1()
        {
            InitializeComponent();
            this.Hide();
        }        

        private async void Form1_Load(object sender, System.EventArgs e)
        {
            Appframe.Win.LoginHandler.Init();
            if (await Appframe.Win.LoginHandler.ShowLoginDialog() == true) 
            {
                // login success. load your data here
                this.Visible = true;
                this.User = await GetUserSessionAsync();
                this.CurrentUriText.Text = Appframe.Data.DataApi.BaseAddress?.Host ?? "None";
                this.CurrentUserText.Text = User?.Name ?? "None";


                //if (!string.IsNullOrEmpty(User?.Error))
                //    throw new System.Exception(User.Error);
                this.CurrentStatusText.Text = "Completed";
            }
        }

        private async System.Threading.Tasks.Task<UserSession> GetUserSessionAsync()
        {
            var userStr = await Appframe.Data.DataApi.PostAsync("/api/system/usersession/", null);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UserSession>(userStr);
        }
    }

    public class UserSession
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public string UICulture { get; set; }
        public bool IsDeveloper { get; set; }
        public string Error { get; set; }
    }
}
