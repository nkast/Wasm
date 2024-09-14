// Copyright (C)2022-2024 Nick Kastellanos

using System;

namespace nkast.Wasm.Input
{
    public struct GamepadHapticActuatorParams
    {
        public float StartDelay { get; set; }
        public float Duration { get; set; }
        public float StrongMagnitude { get; set; }
        public float WeakMagnitude { get; set; }
        public float LeftTrigger { get; set; }
        public float RightTrigger { get; set; }
    }
}
