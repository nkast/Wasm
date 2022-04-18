using System;

namespace nkast.Wasm.Canvas
{
    public enum CompositeOperation
    {
        Source_Over = 1,
        Source_In = 2,
        Source_Out = 3,
        Source_Atop = 4,
        Destination_Over = 5,
        Destination_In = 6,
        Destination_Out = 7,
        Destination_Atop = 8,
        Lighter = 8,
        Copy = 9,
        Xor = 10,
        Multiply = 11,
        Screen = 12,
        Overlay = 13,
        Darken = 14,
        Lighten = 15,
        Color_Dodge = 16,
        Color_Burn = 17,
        Hard_Light = 18,
        Soft_Light = 19,
        Difference = 20,
        Exclusion = 21,
        Hue = 22,
        Saturation = 23,
        Color = 24,
        Luminosity = 25,
    }
}
