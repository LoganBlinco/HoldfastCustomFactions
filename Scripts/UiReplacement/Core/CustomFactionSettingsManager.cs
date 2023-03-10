using System.Collections.Generic;
using HoldfastSharedMethods;
using SceneChecker.SoundReplacement.FactionVoiceLineSounds;

namespace SceneChecker.Core
{
    public class CustomFactionSettingsManager
    {
        public readonly Dictionary<FactionCountry, FactionUIOverride> FactionToSettings;
        public CustomFactionSettingsManager(List<CustomFactionReplacementOverload.CustomFactionSettings> settings)
        {
            FactionToSettings = new Dictionary<FactionCountry, FactionUIOverride>();
            PopulateDictionary(settings);
        }
        private void PopulateDictionary(List<CustomFactionReplacementOverload.CustomFactionSettings> settings)
        {
            foreach (CustomFactionReplacementOverload.CustomFactionSettings workingFaction in settings)
            {
                FactionToSettings[workingFaction.FactionToOverride] = workingFaction.UIOverride;
            }
        }

        public FactionUIOverride GetSettingsOrNull(FactionCountry factionCountry)
        {
            if (FactionToSettings.TryGetValue(factionCountry,
                    out FactionUIOverride settings))
            {
                return settings;
            }
            return null;
        }
    }
}