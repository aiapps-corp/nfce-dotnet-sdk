using System;

namespace Aiapps.Sdk.Financial
{
    [Obsolete]
    public enum Situacao
    {
        Pendente = 0,
        Autorizado = 1,
        Recusado = 2,
        EmProcessamento = 3,
        Encerrado = 4,
        Cancelado = 5
    }
    public enum PaymentStatus
    {
        Draft = 0,
        Approved = 1,
        Refused = 2,
        Processing = 3,
        Closed = 4,
        Canceled = 5
    }
    
}
