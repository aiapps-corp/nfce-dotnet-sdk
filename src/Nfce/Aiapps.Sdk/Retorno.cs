﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk
{
    public class Retorno
    {
        public bool Sucesso { get; set; }

        public string Mensagem { get; set; }
    }

    public class Response
    {
        public bool IsSuccessStatus { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public object Data { get; set; }
    }
}
