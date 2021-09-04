using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public enum BlackjackActionEnum
    {
        Hit,
        Stand,
        Double,
        Split,
        Surrender
    }

    public class ActionUnavailableException : Exception
    {
        public ActionUnavailableException() : base() { }
        public ActionUnavailableException(string msg) : base(msg) { }
        public ActionUnavailableException(string msg, Exception inner) : base(msg, inner) { }
    }

    public abstract class BlackjackAction
    {
        public abstract BlackjackActionEnum Kind { get; }  

        public bool Act()
        {
            if (!Available())
            {
                throw new ActionUnavailableException($"Command '{Kind}' is unavailable");
            }

            return Execute();
        }
        public abstract bool Execute();
        public abstract bool Available();

        public override string ToString()
        { 
            return Kind.ToString();
        }
    }

    public class HitAction : BlackjackAction
    {
        public override BlackjackActionEnum Kind => BlackjackActionEnum.Hit;

        public override bool Available()
        {
            return true;
        }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }

    public class StandAction : BlackjackAction
    {
        public override BlackjackActionEnum Kind => BlackjackActionEnum.Stand;

        public override bool Available()
        {
            return true;
        }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }

    public class DoubleAction: BlackjackAction
    {
        public override BlackjackActionEnum Kind => BlackjackActionEnum.Double;

        public override bool Available()
        {
            throw new NotImplementedException();
        }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }

    public class SplitAction : BlackjackAction
    {
        public override BlackjackActionEnum Kind => BlackjackActionEnum.Split;

        public override bool Available()
        {
            throw new NotImplementedException();
        }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }

    public class SurrenderAction : BlackjackAction
    {
        public override BlackjackActionEnum Kind => BlackjackActionEnum.Surrender;

        public override bool Available()
        {
            throw new NotImplementedException();
        }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}
