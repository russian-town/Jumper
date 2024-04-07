using Sourse.UI.Shop.SkinConfiguration;

namespace Sourse.Yandex
{
    public class SkinRewarded : Rewarded
    {
        public Skin Skin { get; private set; }

        public SkinRewarded(Skin skin)
            => Skin = skin;

        public override void Accept()
            => Skin.Buy();
    }
}
