using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Winmedia_Database_Client
{ 
    /// <summary>
    /// All the levels in the peak level meter. G - green bars, Y - yellow bar, R - red bar, and W - white bar.
    /// 32 green, 22 yellow, 9 red and one white - a total of 64
    /// </summary>
    public enum LevelBar
    {
        G1, G2, G3, G4, G5, G6, G7, G8, G9, G10, G11, G12,
        G13, G14, G15, G16, G17, G18, G19, G20, G21, G22,
        G23, G24, G25, G26, G27, G28, G29, G30, G31, G32,
        Y1, Y2, Y3, Y4, Y5, Y6, Y7, Y8, Y9, Y10, Y11, Y12,
        Y13, Y14, Y15, Y16, Y17, Y18, Y19, Y20, Y21, Y22,
        R1, R2, R3, R4, R5, R6, R7, R8, R9,
        W1
    }

    public class LevelHelper
    {
    }
}