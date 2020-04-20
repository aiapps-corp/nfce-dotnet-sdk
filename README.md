# nfce-dotnet-sdk

Biblioteca em C# para geração de NFC-e usando api mobi.

## Primeiros Passos

Criar uma conta de usuário no portal https://mobi.aiapps.com.br
Com essa conta já é possível usar a API.

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
                        Cfop = "5.102",
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


## License
[MIT](https://choosealicense.com/licenses/mit/)
