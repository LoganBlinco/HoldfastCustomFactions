using UnityEngine;
using UnityEngine.UI;

namespace SceneChecker
{
    //Ok, bit redundant at the moment to make this a class.
    public class PlayerInfoObjects
    {
        private readonly Image _factionImage;

        private readonly bool _isValid;

        public PlayerInfoObjects(Image factionImage)
        {
            _factionImage = factionImage;
            _isValid = _factionImage != null;
        }
        
        public void Replace(Sprite playersFactionSprite)
        {
            if (!_isValid){return;}
            if (playersFactionSprite != null)
            {
                _factionImage.overrideSprite = playersFactionSprite;
            }
        }

        public void Destroy()
        {
            if (!_isValid){return;}

            _factionImage.overrideSprite = null;
        }
    }
}