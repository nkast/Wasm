using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Input
{
    public class Gamepad : JSObject
    {
        public bool Connected
        {
            get { return InvokeRet<bool>("nkGamepad.GetConnected"); }
        }

        public string Id
        {
            get { return InvokeRet<string>("nkGamepad.GetId"); }
        }

        public int Index
        {
            get { return InvokeRet<int>("nkGamepad.GetIndex"); }
        }

        public string Mapping
        {
            get { return InvokeRet<string>("nkGamepad.GetMapping"); }
        }

        public int Timestamp
        {
            get { return InvokeRet<int>("nkGamepad.GetTimestamp"); }
        }

        public GamepadButton[] Buttons
        {
            get
            {
                string str = InvokeRet<string>("nkGamepad.GetButtons");

                string[] strs = str.Split(',');
                GamepadButton[] ret = new GamepadButton[strs.Length/3];
                for (int cnt = 0; cnt < ret.Length; cnt++)
                {
                    ret[cnt].Value = float.Parse(strs[cnt*3+0]);
                    ret[cnt].Pressed = int.Parse(strs[cnt*3+1]) != 0;
                    ret[cnt].Touched = int.Parse(strs[cnt*3+2]) != 0;
                }

                return ret;
            }
        }

        public float[] Axes
        {
            get
            {
                string str = InvokeRet<string>("nkGamepad.GetAxes");

                string[] strs = str.Split(',');
                float[] ret = new float[strs.Length];
                for (int cnt = 0; cnt < ret.Length; cnt++)
                {
                    ret[cnt] = float.Parse(strs[cnt]);
                }

                return ret;
            }
        }

        internal Gamepad(int uid) : base(uid)
        {
        }

    }

    public struct GamepadButton
    {
        public float Value;
        public bool Pressed;
        public bool Touched;

        public override string ToString()
        {
            return String.Format("{{Value:{0}, Pressed:{1}, Touched:{2} }}", Value, Pressed, Touched);
        }
    }
}
