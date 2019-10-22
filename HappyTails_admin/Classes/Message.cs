using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace HappyTails_admin
{
    class Message
    {
        private string mess;

        public async void ShowMessage(string message)
        {
            var messToShow = new MessageDialog(message);
            await messToShow.ShowAsync();
        }
    }
}
