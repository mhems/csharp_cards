using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace BlackjackViewPresenter
{
    public class ShoePresenter
    {
        private readonly IShoeView view;
        private readonly Shoe shoe;

        public ShoePresenter(IShoeView view, Shoe shoe)
        {
            this.view = view;
            this.shoe = shoe;
            RegisterModel();
        }

        public void RegisterModel()
        {
            shoe.Burnt += BurnHandler;
            shoe.Dealt += DealtHandler;
            shoe.Shuffling += ShuffleHandler;
        }

        public void UnregisterModel()
        {
            shoe.Burnt -= BurnHandler;
            shoe.Dealt -= DealtHandler;
            shoe.Shuffling -= ShuffleHandler;
        }

        private void BurnHandler(object obj, BurnEventArgs args)
        {
            view.Burn(args.NumBurnt);
            if (obj is Shoe s)
            {
                UpdateView(s);
            }
        }

        private void DealtHandler(object obj, DealtEventArgs _)
        {
            if (obj is Shoe s)
            {
                UpdateView(s);
            }
        }

        private void ShuffleHandler(object obj, EventArgs _)
        {
            view.Shuffle();
        }

        private void UpdateView(Shoe arg)
        {
            view.Count = arg.Count;
            view.CutIndex = arg.CutIndex;
            view.Index = arg.Index;
        }
    }
}
