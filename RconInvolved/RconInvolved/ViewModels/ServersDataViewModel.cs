namespace RconInvolved.ViewModels
{
    using System.Threading.Tasks;
    using Framework.UI.Controls;
    using Framework.UI.Input;
    using RconInvolved.Models;

    public sealed class ServersDataViewModel
    {
        private readonly ServerCollection servers;
        private readonly AsyncDelegateCommand pinCommand;

        public ServersDataViewModel()
        {
            this.servers = new ServerCollection();
            this.pinCommand = new AsyncDelegateCommand(this.Pin);
        }

        public ServerCollection Servers
        {
            get { return this.servers; }
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
