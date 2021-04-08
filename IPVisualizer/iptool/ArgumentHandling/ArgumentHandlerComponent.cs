using System.Linq;

namespace iptool.ArgumentHandling
{
    public class ArgumentHandlerComponent
    {
        public virtual bool WlanOnly { get; }
        public virtual bool IPV4Only { get; }

        public virtual bool RenderSubnetMask { get; }
        public virtual bool Reversed { get; }


        public ArgumentHandlerComponent() : this(null)
        {
        }

        public ArgumentHandlerComponent(string[] args)
        {
            WlanOnly = args?.Contains("/wlan") ?? false;
            IPV4Only = args?.Contains("/v4") ?? false;
            RenderSubnetMask = args?.Contains("/mask") ?? false;
            Reversed = args?.Contains("/reversed") ?? false;
        }

    }
}