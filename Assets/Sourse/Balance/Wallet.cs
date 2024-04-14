using System;
using Sourse.Save;

namespace Sourse.Balance
{
    public class Wallet : IDataReader, IDataWriter
    {
        private int _money;

        public event Action<int> MoneyChanged;

        public int Money => _money;

        public void DicreaseMoney(int money)
        {
            if (money <= 0)
                return;

            if (_money - money < 0)
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

        public void Read(PlayerData playerData)
            => _money = playerData.Money;

        public void Write(PlayerData playerData)
            => playerData.Money = _money;
    }
}
