using System;
using System.Collections.Generic;
using System.Text;

namespace nkast.Wasm.Canvas
{
    public class ContextAttributes
    {
        public enum PowerPreferenceType
        {
            Default = 0,
            HighPerformance = 1,
            LowPower = 2,
        }

        public bool? Alpha { get; set; }
        public bool? Depth { get; set; }
        public bool? Stencil { get; set; }
        public bool? Desynchronized { get; set; }
        public bool? Antialias { get; set; }
        public PowerPreferenceType? PowerPreference { get; set; }
        public bool? PremultipliedAlpha { get; set; }
        public bool? PreserveDrawingBuffer { get; set; }
        public bool? XrCompatible { get; set; }


        public static bool IsNullOrEmpty(ContextAttributes value)
        {
            if (value == null)
                return true;

            if (value.Alpha == null
            &&  value.Depth == null
            &&  value.Stencil == null           
            &&  value.Desynchronized == null
            &&  value.Antialias == null
            &&  value.PowerPreference == null
            &&  value.PremultipliedAlpha == null
            &&  value.PreserveDrawingBuffer == null
            &&  value.XrCompatible == null)
                return true;

            return false;
        }

        internal int ToBit()
        {
            int bits = 0;
            bits |= ((this.Alpha == null) ? 3 : (this.Alpha == true) ? 1 : 0) <<  0;
            bits |= ((this.Desynchronized == null) ? 3 : (this.Desynchronized == true) ? 1 : 0) <<  2;
            bits |= ((this.Depth == null) ? 3 : (this.Depth == true) ? 1 : 0) <<  4;
            bits |= ((this.Stencil == null) ? 3 : (this.Stencil == true) ? 1 : 0) <<  6;
            bits |= ((this.Antialias == null) ? 3 : (this.Antialias == true) ? 1 : 0) <<  8;
            bits |= ((this.PremultipliedAlpha == null) ? 3 : (this.PremultipliedAlpha == true) ? 1 : 0) << 10;
            bits |= ((this.PreserveDrawingBuffer == null) ? 3 : (this.PreserveDrawingBuffer == true) ? 1 : 0) << 12;
            bits |= ((this.XrCompatible == null) ? 3 : (this.XrCompatible == true) ? 1 : 0) << 14;
            bits |= ((this.PowerPreference == null) ? 3 : (int)this.PowerPreference) << 16;

            return bits;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string seperator = "\n ";
            sb.Append("{ ");
            ToStringAppendValue(sb, "alpha", this.Alpha, ref seperator);
            ToStringAppendValue(sb, "depth", this.Depth, ref seperator);
            ToStringAppendValue(sb, "stencil", this.Stencil, ref seperator);
            ToStringAppendValue(sb, "desynchronized", this.Desynchronized, ref seperator);
            ToStringAppendValue(sb, "antialias", this.Antialias, ref seperator);
            ToStringAppendValue(sb, "powerPreference", this.PowerPreference, ref seperator);
            ToStringAppendValue(sb, "premultipliedAlpha", this.PremultipliedAlpha, ref seperator);
            ToStringAppendValue(sb, "preserveDrawingBuffer", this.PreserveDrawingBuffer, ref seperator);
            ToStringAppendValue(sb, "xrCompatible", this.XrCompatible, ref seperator);
            sb.AppendLine("\n }");
            
            return sb.ToString();
        }

        private void ToStringAppendValue(StringBuilder sb, string name, bool? value, ref string seperator)
        {
            if (value == null)
                return;

            sb.Append(seperator); seperator = ",\n ";
            sb.Append(name); sb.Append(": ");
            switch (value)
            {
                case false:
                    sb.Append("false");
                    break;
                case true:
                    sb.Append("true");
                    break;

                default:
                    throw new InvalidOperationException("");
            }
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
                    throw new InvalidOperationException("PowerPreferenceType");
            }

            return;
        }
    }
}
