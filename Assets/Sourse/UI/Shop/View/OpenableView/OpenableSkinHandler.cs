using System.Collections.Generic;
using Sourse.UI.Shop.SkinConfiguration;
using UnityEngine;

namespace Sourse.UI.Shop.View.OpenableSkinView
{
    public class OpenableSkinHandler : SkinHandler.SkinHandler
    {
        private LevelCompletePanel.LevelCompletePanel _levelCompletePanel;
        private int _currentIndex;

        private readonly List<SkinConfig> _openableSkins = new List<SkinConfig>();

        public void Unsubscribe()
            => _levelCompletePanel.SkinOpened -= OnSkinOpened;

        public void Initialize(LevelCompletePanel.LevelCompletePanel levelCompletePanel)
        {
            Initialize();
            _levelCompletePanel = levelCompletePanel;
            _levelCompletePanel.SkinOpened += OnSkinOpened;

            /*foreach (var skin in Skins)
                AddOpenableSkin(skin);*/
        }

        public SkinConfig GetOpenableSkin()
        {
            if (_openableSkins.Count <= 0)
                return null;

/*            if (Saver.TryGetValue(OpeningSkinIndexKey, out int value))
            {
                _currentIndex = value;
            }
            else
            {
                _currentIndex = Random.Range(0, _openableSkins.Count);
                Saver.Save(OpeningSkinIndexKey, _currentIndex);
            }*/

            return _openableSkins[_currentIndex];
        }

        private void AddOpenableSkin(SkinConfig skin)
        {
            if (skin.Type == SkinType.Openable /*&& skin.IsBought == false*/)
                _openableSkins.Add(skin);
        }

        private void OnSkinOpened(int id)
        {
            OpenSkin(id);

            for (int i = 0; i < _openableSkins.Count; i++)
            {
                if (_openableSkins[i].ID == id)
                {
                    _openableSkins.Remove(_openableSkins[i]);
                    //Saver.TryDeleteSaveData(OpeningSkinIndexKey);
                    i--;
                }
            }
        }

        private void OpenSkin(int id)
        {
            foreach (var skin in Skins)
            {
                if (id == skin.ID)
                {
                    //skin.Buy();
                    //Saver.SaveState(IsByKey, id, skin.IsBought);
                }
            }
        }
    }
}
