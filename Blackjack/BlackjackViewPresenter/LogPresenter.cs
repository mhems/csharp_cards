using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;

namespace BlackjackViewPresenter
{
    public class LogPresenter
    {
        private readonly ILogView view;
        private readonly BlackjackEventLogger log;

        public LogPresenter(ILogView view, BlackjackEventLogger log)
        {
            this.view = view;
            this.log = log;
            RegisterModel();
        }

        public void RegisterModel()
        {
            log.Logging += LogHandler;
        }

        public void UnRegisterModel()
        {
            log.Logging -= LogHandler;
        }

        private void LogHandler(object _, EventMessageArgs args)
        {
            view.Log(args.Message);
        }
    }
}
