using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Base.Global;

namespace MinimumExperienceGain
{
    internal sealed class Settings : AttributeGlobalSettings<Settings>
    {
        private float _minLearningRate = 1f;
        private int _maxSkillValue = 300;
        public override string Id => "MinimumExperienceGain";
        public override string DisplayName => $"MinimumExperienceGain {typeof(Settings).Assembly.GetName().Version.ToString(3)}";
        public override string FolderName => "MinimumExperienceGain";
        public override string FormatType => "json";

        [SettingPropertyFloatingInteger("Minimum Learning Rate", 0f, 10f, "#0.00", Order = 1, RequireRestart = true, HintText = "Change the Minimum Learning Rate for Skills.")]
        [SettingPropertyGroup("Learning Rate")]
        public float MinLearningRate
        {
            get => _minLearningRate;
            set
            {
                if (_minLearningRate != value)
                {
                    _minLearningRate = value;
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyInteger("Maximum Skill Level", 0, 1000, "0 Max Skill Level", Order = 2, RequireRestart = true, HintText = "Change the Maximum Skill Level.")]
        [SettingPropertyGroup("Skill Level")]
        public int MaxSkillValue
        {
            get => _maxSkillValue;
            set
            {
                if (_maxSkillValue != value)
                {
                    _maxSkillValue = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
