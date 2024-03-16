using UnityEngine;

[RequireComponent(typeof(WalletView), typeof(Saver))]
public class Wallet : MonoBehaviour
{
    private const string MoneyKey = "Money";

    private int _money;
    private WalletView _walletView;
    private Saver _saver;

    public int Money => _money;

    private void Awake()
    {
        _walletView = GetComponent<WalletView>();
        _saver = GetComponent<Saver>();

        if (_saver.TryGetValue(MoneyKey, out int money))
            _money = money;

        _walletView.UpdateMoneyText(_money);
    }

    public void DicreaseMoney(int money)
    {
        if (money <= 0)
            return;

        _money -= money;
        _saver.Save(MoneyKey, _money);
        _walletView.UpdateMoneyText(_money);
    }

    public void AddMoney(int money)
    {
        if (money <= 0)
            return;

        _money += money;
        _saver.Save(MoneyKey, _money);
        _walletView.UpdateMoneyText(_money);
    }
}
