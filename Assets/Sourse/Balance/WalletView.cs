using TMPro;
using UnityEngine;

namespace Sourse.Balance
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;

        private Wallet _wallet;

        public void Initialize(Wallet wallet)
        {
            _wallet = wallet;
            _moneyText.text = _wallet.Money.ToString();
        }

        public void Subscribe()
            => _wallet.MoneyChanged += OnMoneyChanged;

        public void Unsubscribe()
            => _wallet.MoneyChanged -= OnMoneyChanged;

        private void OnMoneyChanged(int money)
            => _moneyText.text = money.ToString();
    }
}
