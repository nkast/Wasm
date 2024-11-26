using System;
using System.Text;

namespace nkast.Wasm.XR
{
    public struct XRWebGLLayerOptions
    {
        public bool? Alpha { get; set; }
        public bool? Depth { get; set; }
        public bool? Stencil { get; set; }
        public bool? Antialias { get; set; }
        public bool? IgnoreDepthValues { get; set; }

        internal int ToBit()
        {
            int bits = 0;
            bits |= ((this.Alpha == null) ? 3 : (this.Alpha == true) ? 1 : 0) << 0;
            bits |= ((this.Depth == null) ? 3 : (this.Depth == true) ? 1 : 0) << 2;
            bits |= ((this.Stencil == null) ? 3 : (this.Stencil == true) ? 1 : 0) << 4;
            bits |= ((this.Antialias == null) ? 3 : (this.Antialias == true) ? 1 : 0) << 6;
            bits |= ((this.IgnoreDepthValues == null) ? 3 : (this.IgnoreDepthValues == true) ? 1 : 0) << 8;

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
            ToStringAppendValue(sb, "antialias", this.Antialias, ref seperator);
            ToStringAppendValue(sb, "ignoreDepthValues", this.IgnoreDepthValues, ref seperator);
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
    }
}