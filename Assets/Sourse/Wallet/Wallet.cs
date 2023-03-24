using UnityEngine;

[RequireComponent(typeof(WalletView), typeof(MoneySaver))]
public class Wallet : MonoBehaviour
{
    private const string MoneyKey = "Money";

    [SerializeField] private int _money;

    private WalletView _walletView;
    private MoneySaver _moneySaver;

    public int Money => _money;

    private void Awake()
    {
        _moneySaver = GetComponent<MoneySaver>();
        _walletView = GetComponent<WalletView>();

        if (_moneySaver.TryGetValue(MoneyKey, out int value))
            _money = value;

        _walletView.UpdateMoneyText(Money);
    }

    public void DicreaseMoney(int money)
    {
        if (money <= 0)
            return;

        _money -= money;
        _walletView.UpdateMoneyText(Money);
        _moneySaver.Save(MoneyKey, _money);
    }

    private void AddMoney(int money)
    {
        if (money <= 0)
            return;

        _money += money;
        _walletView.UpdateMoneyText(Money);
        _moneySaver.Save(MoneyKey, _money);
    }
}
