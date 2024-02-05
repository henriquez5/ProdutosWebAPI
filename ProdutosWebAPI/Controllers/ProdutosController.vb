Imports System.Net.Http
Imports System.Net
Imports System.Web.Http
Imports System.Runtime.InteropServices

Namespace ProdutosWebAPI

    Public Class ProdutosController
        Inherits ApiController

        Private _produtoService As IProdutoService = New ProdutoService

        <HttpGet>
        Public Function ListarProdutos(ByVal filtro As String)
            Try
                Dim buscarProdutos As List(Of Produto) = _produtoService.ObterProdutos(filtro)

                If buscarProdutos.Count = 0 Then
                    Throw New Exception("Nenhum produto localizado")
                End If

                Return buscarProdutos
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function

        <HttpPost>
        Public Function AdicionarProduto(<FromBody> ByVal novoProduto As Produto)
            Try
                Dim produtoAdd As Produto = _produtoService.AdicionarProduto(novoProduto)

                If produtoAdd Is Nothing Then
                    Dim resp = New HttpResponseMessage(HttpStatusCode.NotFound)
                    Throw New HttpResponseException(resp)
                End If

                Return produtoAdd
            Catch ex As Exception
                Return ex
            End Try
        End Function

        <HttpPut>
        Public Function AtualizarProduto(ByVal id As Integer, <FromBody> ByVal produto As Produto)
            Try
                Dim produtoUpdate As Produto = _produtoService.AtualizarProduto(id, produto)

                If produtoUpdate Is Nothing Then
                    Dim resp = New HttpResponseMessage(HttpStatusCode.NotFound)
                    Throw New HttpResponseException(resp)
                End If

                Return produtoUpdate
            Catch ex As Exception
                Return ex
            End Try
        End Function

        <HttpDelete>
        Public Function DeletarProduto(ByVal id As Integer)
            Try
                Dim produtoDeletar As Produto = _produtoService.RemoverProduto(id)

                If produtoDeletar Is Nothing Then
                    Throw New Exception("Nenhum Cliente foi deletado.")
                End If

                Return produtoDeletar
            Catch ex As Exception
                Return ex
            End Try
        End Function

        'Public Function GetTodosProdutos() As IEnumerable(Of Produto)
        '    Return produtos
        'End Function
        'Public Function GetProdutoPorId(id As Integer) As Produto
        '    Dim produto = produtos.FirstOrDefault(Function(p) p.Id = id)
        '    If produto Is Nothing Then
        '        Dim resp = New HttpResponseMessage(HttpStatusCode.NotFound)
        '        Throw New HttpResponseException(resp)
        '    End If
        '    Return produto
        'End Function

        'Public Function GetProdutosPorCategoria(categoria As String) As IEnumerable(Of Produto)
        '    Return produtos.Where(Function(p) String.Equals(p.Categoria, categoria, StringComparison.OrdinalIgnoreCase))
        'End Function

    End Class
End Namespace