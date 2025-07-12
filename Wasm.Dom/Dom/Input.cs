using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public class Input : HTMLElement<Input>
    {
        public HTMLInputType Type
        {
            get 
            { 
                int type = InvokeRetInt("nkInput.GetType");
                switch(type)
                {
                    case 1: return HTMLInputType.Button;
                    case 2: return HTMLInputType.Submit;
                    case 3: return HTMLInputType.Text;
                    case 4: return HTMLInputType.Password;
                    case 5: return HTMLInputType.Hidden;
                    case 6: return HTMLInputType.Checkbox;
                    case 7: return HTMLInputType.Radio;

                    default: throw new NotSupportedException($"Unknown input type {type}");
                }
            }
            set 
            {
                int type;
                switch(value)
                {
                    case HTMLInputType.Button:   type = 1; break;
                    case HTMLInputType.Submit:   type = 2; break;
                    case HTMLInputType.Text:     type = 3; break;
                    case HTMLInputType.Password: type = 4; break;
                    case HTMLInputType.Hidden:   type = 5; break;
                    case HTMLInputType.Checkbox: type = 6; break;
                    case HTMLInputType.Radio:    type = 7; break;

                    default: throw new NotSupportedException($"Unknown input type {value}");
                }
                Invoke("nkInput.SetType", type);
            }
        }

        public string Value
        {
            get { return InvokeRetString("nkInput.GetValue"); }
            set { Invoke<string>("nkInput.SetValue",value); }
        }

        public double ValueAsNumber
        {
            get { return InvokeRetDouble("nkInput.GetValueAsNumber"); }
            set { Invoke<double>("nkInput.SetValueAsNumber", value); }
        }

        private Input(int uid) : base(uid)
        {
        }

        public Input() : base(Register())
        {
        }

        private static int Register()
        {
            int uid = JSObject.StaticInvokeRetInt("nkInput.Create");
            return uid;
        }
    }
}
