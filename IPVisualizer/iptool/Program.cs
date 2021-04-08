using System;
using iptool.ArgumentHandling;
using iptool.NetworkHandling;
using iptool.Rendering;
using iptool.UI;

namespace iptool
{
    class Program
    {
        public static View View
        {
            get;
            set;
        }

        static void Main(string[] args)
        {
            ArgumentHandlerComponent argumentHandlerComponent = new ArgumentHandlerComponent(args);
            INetworker networkComponent = new NetworkComponent(argumentHandlerComponent);
            
            IRenderer rendererComponent = null;
            if (argumentHandlerComponent.Reversed)
            {
                rendererComponent = new ReverseRendererComponent(argumentHandlerComponent);
            }
            else
            {
                rendererComponent = new RendererComponent(argumentHandlerComponent);
            }


            View = new View(networkComponent, rendererComponent);
            View.Start();
        }
    }
}
