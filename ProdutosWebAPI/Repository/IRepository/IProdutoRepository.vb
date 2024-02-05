Public Interface IProdutoRepository

    Function ObterProdutos(ByVal filtro As String) As List(Of Produto)

    Function AdicionarProduto(ByVal novoProduto As Produto) As Produto

    Function AtualizarProduto(ByVal idProduto As Integer, ByVal produtoAtualizado As Produto) As Produto

    Function ExcluirProduto(ByVal id As Integer) As Produto

End Interface


