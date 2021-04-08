using System;
using System.Collections.Generic;
using System.Text;
using iptool.NetworkHandling;

namespace iptool.Rendering
{
    public interface IRenderer
    {
        string Render(List<NetworkData> data);
    }
}
