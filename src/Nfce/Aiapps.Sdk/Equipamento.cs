﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk
{
    [Obsolete("Use Device")]
    public class Equipamento
    {
        public string Codigo { get; set; }
        public string MacAddress { get; set; }
        public string Nome { get; set; }
    }
}
