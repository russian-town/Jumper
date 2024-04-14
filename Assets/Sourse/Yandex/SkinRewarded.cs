using Sourse.UI.Shop.SkinConfiguration;

namespace Sourse.Yandex
{
    public class SkinRewarded : Rewarded
    {
        public SkinRewarded(Skin skin)
            => Skin = skin;

        public Skin Skin { get; private set; }

        public override void Accept()
            => Skin.Buy();
    }
}
