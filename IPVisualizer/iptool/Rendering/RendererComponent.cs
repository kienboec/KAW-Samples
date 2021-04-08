using System.Collections.Generic;
using System.Text;
using iptool.ArgumentHandling;
using iptool.NetworkHandling;

namespace iptool.Rendering
{
    public class RendererComponent : IRenderer
    {
        protected ArgumentHandlerComponent _argumentHandlerComponent;

        public RendererComponent(ArgumentHandlerComponent argumentHandlerComponent)
        {
            this._argumentHandlerComponent = argumentHandlerComponent;
        }

        public virtual string Render(List<NetworkData> data)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in (data ?? new List<NetworkData>()))
            {
                string subnetMask = _argumentHandlerComponent.RenderSubnetMask
                    ? $"/{item.SubnetPrefix}"
                    : string.Empty;
                builder.AppendLine($"{item.IP}{subnetMask}");
            }

            return builder.ToString();
        }
    }
}