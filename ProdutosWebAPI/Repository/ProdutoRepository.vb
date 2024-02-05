Imports System.IO
Imports Newtonsoft.Json

Public Class ProdutoRepository
    Implements IProdutoRepository

    Public ReadOnly jsonDiretorio As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datajson\produtos.json")

    Public Function ObterProdutos(ByVal filtro As String) As List(Of Produto) Implements IProdutoRepository.ObterProdutos
        Try
            Dim buscarClientesResult As New List(Of Produto)()

            Dim jsonString As String = File.ReadAllText(jsonDiretorio)

            If String.IsNullOrEmpty(jsonString) Then
                Throw New Exception("Não existe nenhum dado inserido no JSON")
            End If

            Dim list As List(Of Produto) = JsonConvert.DeserializeObject(Of List(Of Produto))(jsonString)

            Dim query = (From Produto In list
                         Where (Produto.Nome.Contains(filtro) OrElse Produto.Categoria.Contains(filtro)) _
                 OrElse (String.IsNullOrEmpty(filtro) OrElse filtro Is Nothing)
                         Select Produto).ToList()

            If query.Count > 0 Then
                buscarClientesResult = query
            End If

            Return buscarClientesResult
        Catch ex As Exception
            Throw New Exception("Houve um erro ao buscar dado no arquivo JSON : " & ex.Message)
        End Try
    End Function

    ' Método para adicionar um novo produto
    Public Function AdicionarProduto(ByVal novoProduto As Produto) As Produto Implements IProdutoRepository.AdicionarProduto
        Try
            Dim jsonString As String = File.ReadAllText(jsonDiretorio)

            ' Se o arquivo for vazio, realiza a primeira inserção
            If String.IsNullOrEmpty(jsonString) Then
                Dim produtoFirst As New Produto With {
                    .Id = novoProduto.Id,
                    .Nome = novoProduto.Nome,
                    .Categoria = novoProduto.Categoria,
                    .Preco = novoProduto.Preco
                }

                Dim clienteJson As String = JsonConvert.SerializeObject(produtoFirst, Formatting.Indented)

                File.AppendAllText(jsonDiretorio, "[" & clienteJson & "]")

                Return novoProduto
            End If

            Dim list As List(Of Produto) = JsonConvert.DeserializeObject(Of List(Of Produto))(jsonString)

            Dim produto As New Produto With {
                .Id = novoProduto.Id,
                .Nome = novoProduto.Nome,
                .Categoria = novoProduto.Categoria,
                .Preco = novoProduto.Preco
            }

            list.Add(produto)

            Dim produtoToJson As String = JsonConvert.SerializeObject(list, Formatting.Indented)

            File.WriteAllText(jsonDiretorio, produtoToJson)

            Return novoProduto
        Catch ex As Exception
            Throw New Exception("Houve um erro ao adicionar no arquivo JSON: " & ex.Message)
        End Try

    End Function

    ' Método para atualizar um produto existente
    Public Function AtualizarProduto(ByVal idProduto As Integer, ByVal produtoAtualizado As Produto) As Produto Implements IProdutoRepository.AtualizarProduto
        Try
            Dim atualizado As New Produto()

            Dim jsonString As String = File.ReadAllText(jsonDiretorio)

            If String.IsNullOrEmpty(jsonString) Then
                Throw New Exception("Não existe nenhum dado inserido no JSON")
            End If

            Dim list As List(Of Produto) = JsonConvert.DeserializeObject(Of List(Of Produto))(jsonString)

            For Each item In list
                If item.Id = idProduto Then
                    item.Id = idProduto
                    item.Nome = produtoAtualizado.Nome
                    item.Categoria = produtoAtualizado.Categoria
                    item.Preco = produtoAtualizado.Preco
                    atualizado = item
                Else
                    Continue For
                End If
            Next

            Dim produtoToJson As String = JsonConvert.SerializeObject(list, Formatting.Indented)

            File.WriteAllText(jsonDiretorio, produtoToJson)

            Return atualizado

        Catch ex As Exception
            Throw New Exception("Houve um erro ao atualizar dado no arquivo JSON: " & ex.Message)
        End Try
    End Function

    ' Método para excluir um produto do banco de dados por ID
    Public Function ExcluirProduto(ByVal id As Integer) As Produto Implements IProdutoRepository.ExcluirProduto
        Try
            Dim removerProduto As New Produto()

            Dim jsonString As String = File.ReadAllText(jsonDiretorio)

            If String.IsNullOrEmpty(jsonString) Then
                Throw New Exception("Não existe nenhum dado inserido no JSON")
            End If

            Dim list As List(Of Produto) = JsonConvert.DeserializeObject(Of List(Of Produto))(jsonString)

            removerProduto = list.FirstOrDefault(Function(c) c.Id = id)

            If removerProduto Is Nothing Then
                Throw New Exception("Insira um ID válido!!")
            End If

            list.Remove(removerProduto)

            Dim clienteToJson As String = JsonConvert.SerializeObject(list, Formatting.Indented)

            File.WriteAllText(jsonDiretorio, clienteToJson)

            Return removerProduto
        Catch ex As Exception
            Throw New Exception("Houve um erro ao excluir no arquivo JSON: " & ex.Message)
        End Try
    End Function

End Class


