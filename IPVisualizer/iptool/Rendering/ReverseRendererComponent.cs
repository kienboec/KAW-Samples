using iptool.ArgumentHandling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iptool.NetworkHandling;

namespace iptool.Rendering
{
    public class ReverseRendererComponent : RendererComponent
    {
        public ReverseRendererComponent(ArgumentHandlerComponent argumentHandlerComponent) 
            : base(argumentHandlerComponent)
        {
        }

        public override string Render(List<NetworkData> data)
        {
            StringReader reader = new StringReader(base.Render(data));
            StringBuilder builder = new StringBuilder();

            string line;
            while ((line = reader.ReadLine())!=null)
            {
                builder.AppendLine(ToReversedString(line));
            }

            return builder.ToString();
        }

        private string ToReversedString(string original)
        {
            return new string(original.Reverse().ToArray());
        }
    }
}
