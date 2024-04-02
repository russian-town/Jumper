namespace Sourse.UI.Shop.SkinConfiguration
{
    public class OpenableSkinView : SkinView
    {
        //openableSkinBar
        private Skin _openableSkin;
        private float _completedPercent;

        public void Initiaze(OpenableSkin openableSkin)
        {
            _openableSkin = openableSkin;
            _completedPercent = openableSkin.Percent;
        }

        protected override Skin Skin()
        {
            return _openableSkin;
        }
    }
}
