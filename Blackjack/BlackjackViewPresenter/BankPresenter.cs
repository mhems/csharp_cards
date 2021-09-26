using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace BlackjackViewPresenter
{
    public class BankPresenter
    {
        private readonly IBankView view;
        private readonly Bank bank;

        public BankPresenter(IBankView view, Bank bank)
        {
            this.view = view;
            this.bank = bank;
            RegisterModel();
        }

        public void RegisterModel()
        {
            bank.Deposited += BankActivityHandler;
            bank.Withdrawn += BankActivityHandler;
        }

        public void UnregisterModel()
        {
            bank.Deposited -= BankActivityHandler;
            bank.Withdrawn -= BankActivityHandler;
        }

        private void BankActivityHandler(object _, BankTransactionEventArgs args)
        {
            view.Balance = args.Balance;
        }
    }
}
