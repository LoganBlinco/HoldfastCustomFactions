using HoldfastSharedMethods;
using SceneChecker.Core;
using SceneChecker.Scripts.Logging;
using UnityEngine.UI;

namespace SceneChecker
{
    public class MapVotingCard
    {
        private static readonly ILog Logger = LogFactory.GetLogger(typeof(MapVotingCard), LogLevelsEnum.Information);

        
        private readonly Image _attackingImage;
        private readonly Image _defendingImage;

        private bool _isValid;
        
        public MapVotingCard(Image attackingImage, Image defendingImage)
        {
            _attackingImage = attackingImage;
            _defendingImage = defendingImage;

            _isValid = _attackingImage != null && _defendingImage != null;
        }

        public void Replace(CustomFactionSettingsManager settingsManager)
        {
            string attackerName = _attackingImage.sprite.name;
            FactionCountry factionToReplace = GetFactionToReplace(attackerName);

            FactionUIOverride factionToUse = settingsManager.GetSettingsOrNull(factionToReplace);
            if (factionToUse != null)
            {
                _attackingImage.overrideSprite = factionToUse.mapVotingSprite;
            }

            string defendersName = _defendingImage.sprite.name;
            factionToReplace = GetFactionToReplace(defendersName);

            factionToUse = settingsManager.GetSettingsOrNull(factionToReplace);
            if (factionToUse != null)
            {
                _defendingImage.overrideSprite = factionToUse.mapVotingSprite;
            }

        }

        private static FactionCountry GetFactionToReplace(string attackerSpriteName)
        {
            switch (attackerSpriteName)
            {
                case "Russian-No-Ribbon-No-Header-Russia":
                    return FactionCountry.Russian;
                case "British-No-Ribbon-No-Header":
                    return FactionCountry.British;
                case "French-No-Ribbon-No-Header":
                    return FactionCountry.French;
                case "Prussia-No-Ribbon-No-Header":
                    return FactionCountry.Prussian;
                case "Italian-No-Ribbon-No-Header":
                    return FactionCountry.Italian;
                default:
                    Logger.LogError($"Not sure what to replace for map voting. Input sprite was {attackerSpriteName}");
                    return FactionCountry.None;
            }
        }

        public void Destroy()
        {
            if (!_isValid){return;}
            _attackingImage.overrideSprite = null;
            _defendingImage.overrideSprite = null;
        }
    }
}