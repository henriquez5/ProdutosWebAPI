Public Class ProdutoService
    Implements IProdutoService
    Private _produtoRepository As IProdutoRepository = New ProdutoRepository

    Public Function ObterProdutos(ByVal filtro As String) As List(Of Produto) Implements IProdutoService.ObterProdutos
        Try
            If String.IsNullOrEmpty(filtro) Then
                filtro = ""
            End If

            Dim buscarClientes As New List(Of Produto)()

            buscarClientes = _produtoRepository.ObterProdutos(filtro)

            Return buscarClientes
        Catch ex As Exception
            Throw New Exception("Houve um erro ao buscar o(s) produtos(s): " & ex.Message)
        End Try
    End Function

    Public Function AdicionarProduto(ByVal novoProduto As Produto) As Produto Implements IProdutoService.AdicionarProduto
        Try

            Dim resultProdutoAdicionar As New Produto()

            resultProdutoAdicionar = _produtoRepository.AdicionarProduto(novoProduto)

            Return resultProdutoAdicionar
        Catch ex As Exception
            Throw New Exception("Houve um erro ao adicionar o produto: " & ex.Message)
        End Try
    End Function

    Public Function AtualizarProduto(ByVal idProduto As Integer, ByVal produtoAtualizado As Produto) As Produto Implements IProdutoService.AtualizarProduto
        Try
            Dim resultProdutoAtualizar As New Produto()

            resultProdutoAtualizar = _produtoRepository.AtualizarProduto(idProduto, produtoAtualizado)

            Return resultProdutoAtualizar
        Catch ex As Exception
            Throw New Exception("Houve um erro ao atualizar o produto: " & ex.Message)
        End Try
    End Function

    Public Function RemoverCliente(ByVal id As Integer) As Produto Implements IProdutoService.RemoverProduto
        Try
            Dim resultProdutoRemover As New Produto()

            resultProdutoRemover = _produtoRepository.ExcluirProduto(id)

            Return resultProdutoRemover
        Catch ex As Exception
            Throw New Exception("Houve um erro ao remover o produto: " & ex.Message)
        End Try
    End Function
End Class
