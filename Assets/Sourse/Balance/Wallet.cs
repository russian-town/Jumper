using System;

namespace Sourse.Balance
{
    public class Wallet
    {
        private int _money;

        public int Money => _money;

        public event Action<int> MoneyChanged;

        public void DicreaseMoney(int money)
        {
            if (money <= 0)
                return;

            _money -= money;
            MoneyChanged?.Invoke(_money);
        }

        public void AddMoney(int money)
        {
            if (money <= 0)
                return;

            _money += money;
            MoneyChanged?.Invoke(_money);
        }
    }
}
