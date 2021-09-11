using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Blackjack
{
    public enum BlackjackCountEnum
    {
        HiLo,
        HiOptOne,
        HiOptTwo,
        KO,
        OmegaTwo,
        RedSeven,
        ZenCount
    }

    public class BlackjackCount
    {
        private static readonly Dictionary<BlackjackCountEnum, List<float>> deltaMap = new()
        {
            { BlackjackCountEnum.HiLo, new List<float>() { 1, 1, 1, 1, 1, 0, 0, 0, -1, -1 } },
            { BlackjackCountEnum.HiOptOne, new List<float>() { 0, 1, 1, 1, 1, 0, 0, 0, -1, 0 } },
            { BlackjackCountEnum.HiOptTwo, new List<float>() {1, 1, 2, 2, 1, 1, 0,  0, -2, 0 } },
            { BlackjackCountEnum.KO, new List<float>() {1, 1, 1, 1, 1, 1, 0,  0, -1, -1 } },
            { BlackjackCountEnum.OmegaTwo, new List<float>() {1, 1, 2, 2, 2, 1, 0, -1, -2, 0 } },
            { BlackjackCountEnum.RedSeven, new List<float>() {1, 1, 1, 1, 1, 0.5f, 0,  0, -1, -1 } },
            { BlackjackCountEnum.ZenCount, new List<float>() {1, 1, 2, 2, 2, 1, 0,  0, -2, -1 } }
        };
        private readonly Dictionary<BlackjackCountEnum, float> countMap = new();
        private Shoe shoe;

        public BlackjackCount(Shoe shoe)
        {
            this.shoe = shoe;
            this.shoe.Dealt += DealtHandler;
            this.shoe.Shuffling += ShuffledHandler;
            foreach (BlackjackCountEnum system in Enum.GetValues(typeof(BlackjackCountEnum)))
            {
                countMap.Add(system, 0);
            }
        }

        public float this[BlackjackCountEnum system] => countMap[system];

        public void Reset()
        {
            foreach (BlackjackCountEnum system in countMap.Keys)
            {
                countMap[system] = 0;
            }
        }

        public void ClearHandlers()
        {
            shoe.Dealt -= DealtHandler;
            shoe.Shuffling -= ShuffledHandler;
        }

        private void DealtHandler(object _, DealtEventArgs args)
        {
            foreach (Card card in args.DealtCards)
            {
                int value = BlackjackHand.CardValue(card);
                foreach (BlackjackCountEnum system in countMap.Keys)
                {
                    countMap[system] += deltaMap[system][value - 2];
                }
            }
        }

        private void ShuffledHandler(object obj, EventArgs _)
        {
            Reset();
        }
    }
}
