using System;
using iptool.NetworkHandling;
using iptool.Rendering;

namespace iptool.UI
{
    public class View
    {
        private INetworker _networkComponent;
        private IRenderer _rendererComponent;

        public View(INetworker networkComponent, IRenderer rendererComponent)
        {
            this._networkComponent = networkComponent;
            this._rendererComponent = rendererComponent;
        }

        public void Start()
        {
            var data = _networkComponent.GetAddressData();
            var output = _rendererComponent.Render(data);
            
            Console.WriteLine(output);
        }
    }
}