using TMPro;
using UnityEngine;

namespace Sourse.Balance
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;

        public void UpdateMoneyText(int money)
        {
            _moneyText.text = money.ToString();
        }
    }
}
