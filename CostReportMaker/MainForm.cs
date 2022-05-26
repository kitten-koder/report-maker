using CostReportMaker.Database.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Windows.Forms;

namespace CostReportMaker
{
    public partial class MainForm : Form
    {
        private readonly ILogger _logger;
        private readonly IUserProvider _userProvider;
        private readonly IUserRepository _userRepository;

        public MainForm(ILogger<MainForm> logger, IUserProvider userProvider, IUserRepository userRepository)
        {
            this._logger = logger;
            this._userProvider = userProvider;
            this._userRepository = userRepository;

            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            this._logger.LogInformation("MainForm {event} at {dateTime}", "Started", DateTime.UtcNow);

            this.Text = $"Cost Report Maker {Application.ProductVersion}";
            var allUsers = this._userProvider.GetAllAsync().Result;
            var numOfUsers = allUsers.Count;

            MessageBox.Show(String.Format("There is {0} users", numOfUsers));
        }
    }
}