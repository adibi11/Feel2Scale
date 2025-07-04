using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feel2Scale.Data
{
    public class OpenAISettings
    {
        public string? ApiKey {  get; set; }
        public string? Model { get; set; }

        public string ToString()
        {
            return $"OpenAISettings: {{ secretKey: {ApiKey}, model: {Model} }}";
        }

    }

    public class DBSettings
    {
        public string? ConnectionString { get; set; } 

        public string ToString()
        {
            return $"DBSettings: {{ ConnectionString: {ConnectionString} }}";
        }
    }
}
