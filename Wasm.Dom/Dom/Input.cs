using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public class Input : HTMLElement<Input>
    {
        private static readonly int _fid_Create = RegisterFunction("nkInput.Create");
        private readonly int _fid_GetType;
        private readonly int _fid_SetType;
        private readonly int _fid_GetValue;
        private readonly int _fid_SetValue;
        private readonly int _fid_GetValueAsNumber;
        private readonly int _fid_SetValueAsNumber;

        public HTMLInputType Type
        {
            get 
            { 
                int type = InvokeRetInt(_fid_GetType);
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
                Invoke(_fid_SetType, type);
            }
        }

        public string Value
        {
            get { return InvokeRetString(_fid_GetValue); }
            set { Invoke<string>(_fid_SetValue,value); }
        }

        public double ValueAsNumber
        {
            get { return InvokeRetDouble(_fid_GetValueAsNumber); }
            set { Invoke<double>(_fid_SetValueAsNumber, value); }
        }

        private Input(int uid) : base(uid)
        {
            _fid_GetType = RegisterFunction("nkInput.GetType");
            _fid_SetType = RegisterFunction("nkInput.SetType");
            _fid_GetValue = RegisterFunction("nkInput.GetValue");
            _fid_SetValue = RegisterFunction("nkInput.SetValue");
            _fid_GetValueAsNumber = RegisterFunction("nkInput.GetValueAsNumber");
            _fid_SetValueAsNumber = RegisterFunction("nkInput.SetValueAsNumber");
        }

        public Input() : base(Register())
        {
            _fid_GetType = RegisterFunction("nkInput.GetType");
            _fid_SetType = RegisterFunction("nkInput.SetType");
            _fid_GetValue = RegisterFunction("nkInput.GetValue");
            _fid_SetValue = RegisterFunction("nkInput.SetValue");
            _fid_GetValueAsNumber = RegisterFunction("nkInput.GetValueAsNumber");
            _fid_SetValueAsNumber = RegisterFunction("nkInput.SetValueAsNumber");
        }

        private static int Register()
        {
            int uid = JSObject.StaticInvokeRetInt(_fid_Create);
            return uid;
        }
    }
}
