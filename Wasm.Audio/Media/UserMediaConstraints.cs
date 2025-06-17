using System;
using System.Text;

namespace nkast.Wasm.Media
{
    public struct UserMediaConstraints
    {
        public bool? Audio { get; set; }
        public bool? Video { get; set; }


        internal int ToBit()
        {
            int bits = 0;
            bits |= ((this.Audio == null) ? 3 : (this.Audio == true) ? 1 : 0) << 0;
            bits |= ((this.Video == null) ? 3 : (this.Video == true) ? 1 : 0) << 2;

            return bits;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string seperator = "\n ";
            sb.Append("{ ");
            ToStringAppendValue(sb, "audio", this.Audio, ref seperator);
            ToStringAppendValue(sb, "video", this.Video, ref seperator);
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