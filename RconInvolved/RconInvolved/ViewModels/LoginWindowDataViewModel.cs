namespace RconInvolved.ViewModels
{
    using System.Threading.Tasks;
    using Framework.UI.Controls;
    using Framework.UI.Input;
    using RconInvolved.Models;

    public sealed class LoginWindowDataViewModel
    {
        private  ServerProfileCollection serversProfiles;
        private  AsyncDelegateCommand pinCommand;

        public LoginWindowDataViewModel()
        {
            this.serversProfiles = new ServerProfileCollection();
            this.pinCommand = new AsyncDelegateCommand(this.Pin);
        }

        public ServerProfileCollection ServersProfiles
        {
            get { return this.serversProfiles; }
        }

        public AsyncDelegateCommand PinCommand
        {
            get { return this.pinCommand; }
        }

        private async Task Pin()
        {
            await MessageDialog.ShowAsync("PinCommand", "PinCommand Fired.");
        }
    }
}
