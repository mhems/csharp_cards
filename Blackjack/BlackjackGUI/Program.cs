using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blackjack;
using BlackjackViewPresenter;


namespace BlackjackGUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BlackjackApp app = new();
            IBlackjackTableView tableView = app.Table;

            IBlackjackConfig cfg = new StandardBlackjackConfig();
            BlackjackConfigPresenter configPresenter = new(tableView.Config, cfg);
            tableView.Config.Presenter = configPresenter;

            BlackjackTable table = new(1, cfg);
            BlackjackTablePresenter tablePresenter = new(tableView, table);

            BlackjackEventLogger logger = new();
            LogPresenter logPresenter = new(tableView.Log, logger);
            table.AddLogger(logger);

            ShoePresenter shoePresenter = new(tableView.Shoe, table.Shoe);
            BlackjackCountPresenter countPresenter = new(tableView.Count, table.Count);
            BankPresenter houseBankPresenter = new(tableView.Bank, table.TableBank);

            BlackjackTableSlotPresenter dealerSlotPresenter = new(tableView.DealerSlot, table.DealerSlot);
            BlackjackPlayerPresenter dealerPresenter = new(tableView.DealerSlot.Player, table.Dealer);
            BankPresenter dealerBankPresenter = new(tableView.DealerSlot.Player.Bank, table.Dealer.Bank);
            BlackjackHandPresenter dealerHandPresenter = new(tableView.DealerSlot.Hands[0], table.DealerSlot.Hand);
            BankPresenter dealerPotPresenter = new(tableView.DealerSlot.Pots[0], table.DealerSlot.Pot);

            BlackjackPlayer player = new("matt");
            player.Bank.Deposit(1000);
            table.SeatPlayer(player);
            BlackjackPlayerPresenter playerPresenter = new(tableView.PlayerSlots[0].Player, table.Slots[0].Player);
            BankPresenter playerBankPresenter = new(tableView.PlayerSlots[0].Player.Bank, table.Slots[0].Player.Bank);
            BlackjackHandPresenter playerHandPresenter = new(tableView.PlayerSlots[0].Hands[0], table.Slots[0].Hand);
            BankPresenter playerPotPresenter = new(tableView.PlayerSlots[0].Pots[0], table.Slots[0].Pot);
            BankPresenter playerInsurancePresenter = new(tableView.PlayerSlots[0].InsurancePot, table.Slots[0].InsurancePot);

            InteractiveDecisionPolicy decisionPresenter = new(tableView.Decision);
            InteractiveBettingPolicy bettingPresenter = new(tableView.Bet);
            InteractiveInsurancePolicy insurePresenter = new(tableView.Insurance);
            InteractiveSurrenderPolicy surrenderPresenter = new(tableView.Surrender);

            player.DecisionPolicy = decisionPresenter;
            player.BettingPolicy = bettingPresenter;
            player.InsurancePolicy = insurePresenter;
            player.EarlySurrenderPolicy = surrenderPresenter;

            Application.Run(app);
        }
    }
}
