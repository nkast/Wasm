using System;
using System.Text;

namespace nkast.Wasm.Canvas.WebGPU
{
    public struct GPUAdapterOptions
    {
        public enum PowerPreferenceType
        {
            Default = 0,
            HighPerformance = 1,
            LowPower = 2,
        }

        public enum GPUFeatureLevelType
        {
            Default = 0,
            Core = 1,
            Compatibility = 2,
        }

        public PowerPreferenceType? PowerPreference { get; set; }
        public GPUFeatureLevelType? FeatureLevel { get; set; }

        internal int ToBit()
        {
            int bits = 0;
            bits |= ((this.PowerPreference == null) ? 3 : (int)this.PowerPreference) << 0;
            bits |= ((this.FeatureLevel == null) ? 3 : (int)this.FeatureLevel) << 2;

            return bits;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string seperator = "\n ";
            sb.Append("{ ");
            ToStringAppendValue(sb, "powerPreference", this.PowerPreference, ref seperator);
            ToStringAppendValue(sb, "FeatureLevel", this.FeatureLevel, ref seperator);
            sb.AppendLine("\n }");

            return sb.ToString();
        }

        private void ToStringAppendValue(StringBuilder sb, string name, PowerPreferenceType? value, ref string seperator)
        {
            if (value == null)
                return;

            sb.Append(seperator); seperator = ",\n ";
            sb.Append(name); sb.Append(": ");
            switch (value)
            {
                case PowerPreferenceType.Default:
                    sb.Append("\"default\"");
                    break;
                case PowerPreferenceType.HighPerformance:
                    sb.Append("\"high-performance\"");
                    break;
                case PowerPreferenceType.LowPower:
                    sb.Append("\"low-power\"");
                    break;

                default:
                    throw new InvalidOperationException("GPUPowerPreferenceType");
            }

            return;
        }

        private void ToStringAppendValue(StringBuilder sb, string name, GPUFeatureLevelType? value, ref string seperator)
        {
            if (value == null)
                return;

            sb.Append(seperator); seperator = ",\n ";
            sb.Append(name); sb.Append(": ");
            switch (value)
            {
                case GPUFeatureLevelType.Default:
                    sb.Append("\"default\"");
                    break;
                case GPUFeatureLevelType.Core:
                    sb.Append("\"core\"");
                    break;
                case GPUFeatureLevelType.Compatibility:
                    sb.Append("\"compatibility\"");
                    break;

                default:
                    throw new InvalidOperationException("GPUFeatureLevelType");
            }

            return;
        }

    }
}
