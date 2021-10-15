# nfce-dotnet-sdk

Biblioteca em C# para geração de NFC-e usando api mobi.

## Primeiros Passos

Criar uma conta de usuário no portal https://mobi.aiapps.com.br

- API Swagger https://invoices-api.aiapps.com.br/swagger (Emissão de NF-e e NFC-e)  
- API Swagger https://production-api.aiapps.com.br/swagger (Produtos)

- Com essa conta já é possível usar a API.

- Para emitir com sucesso uma NFC-e é necessário completar as informações da empresa e configurar o certificado digital: https://www.youtube.com/watch?v=6ZxWLVKValE

- Criando um novo usuário: https://www.youtube.com/watch?v=QL1L1WKQ06Q
- Configuração de imposto do produto: https://www.youtube.com/watch?v=b-BMGiyZzLg
- Quando é cadastrado um novo produto com o mesmo NCM e CFOP de outro produto existente é utilizado as mesmas configurações tributárias.

- Emitindo uma NFC-e
```C#

            var nfceApi = new NfceApi(new Credencial
            {
                Email = "",
                Senha = ""
            });
            var response = await nfceApi.EmitirAsync(new Pedido
            {
                Referencia = "6",
                Itens = new Item[] {
                    new Item {
                        Cfop = "5.405",
                        NCM = "2203.00.00",
                        ProdutoId = "1",
                        ProdutoNome = "Cerveja",
                        Quantidade = 1,
                        ValorUnitario = 5m
                    }
                },
                Pagamentos = new Pagamento[] {
                    new Pagamento {
                        Tipo = "01",
                        Valor = 5
                    }
                },
                Desconto = 0,
            });
            
```
- Cancelando uma NFC-e 
```C#
            var nfceApi = new NfceApi(new Credencial
            {
                Email = "",
                Senha = ""
            });
            var foiCancelado = await nfceApi.CancelarAsync("31200400000000000000650010000000051842021836", "Cliente cancelou a compra");
```

- Imprimindo o DANFE
```C#
            var nfceApi = new NfceApi(new Credencial
            {
                Email = "",
                Senha = ""
            });
            var response = await nfceApi.DanfeAsync("31200400000000000000650010000000051842021836");
```

## License
[MIT](https://choosealicense.com/licenses/mit/)
