using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbed.SuperMeatBoy.FileFormats.Animation
{
    public enum VersionTag : uint
    {
        Unknown = 0xFFFFFFFF,
        F100 = 0x46313030,
        F101 = 0x46313031,
    }
}
