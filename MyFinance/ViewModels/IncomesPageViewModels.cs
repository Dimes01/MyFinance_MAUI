using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ViewModels
{
    class IncomesPageViewModels : TransactionsViewModel
    {
        public override Command AddTransactionCommand => throw new NotImplementedException();

        public override Command EditTransactionCommand => throw new NotImplementedException();

        public override Command RemoveTransactionCommand => throw new NotImplementedException();
    }
}
