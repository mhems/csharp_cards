using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;

namespace BlackjackViewPresenter
{
    public class BlackjackConfigPresenter
    {
        private readonly IBlackjackConfigView view;
        private readonly IBlackjackConfig config;

        public BlackjackConfigPresenter(IBlackjackConfigView view, IBlackjackConfig config)
        {
            this.view = view;
            this.config = config;
            view.Changed += OnConfigChanged;
        }

        public void PresentConfigToView()
        {
            Dictionary<string, string> strCfg = new();
            // turn config into strings using reflection
            view.Config = strCfg;
        }

        private void OnConfigChanged(object sender, EventArgs args)
        {
            foreach ((string name, string value) in view.Config)
            {
                // use reflection to get config's property from value
                // use reflection to turn value into the property's type
                // use reflection to set the config's property to the typed value
            }
        }
    }
}
