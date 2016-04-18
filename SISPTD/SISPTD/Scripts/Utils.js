var chosen_placeholder = "Selecione uma opção";

$('.chosen-select').chosen({
    width: "100%",
    placeholder_text_single: chosen_placeholder
});

$('.chosen-select-deselect').chosen({
    allow_single_deselect: true,
    placeholder_text_single: chosen_placeholder
});

//Busca pessoa e carrega no input do distribProcesso
$(document).ready(function () {
    $('.procurarPessoa').change(function () {
        var cpf = $('#buscar').val();
        $.ajax({
            method: 'GET',
            url: "/Pessoa/Pesquisar/?cpf=" + cpf,
            success: function (data) {
                $('.paciente').val(data.Nome);
                $('#pessoaId').val(data.Id);
            },
            error: function (data) {
                alert("Algo está errado!");
            }
        });
    });
});

function SalvarRequisicao() {
    

    var usuario = $('#usuarioId').val();
    var pessoa  = $('#pessoaId').val();
    var origem  = $('#IdCidadeOrigem').val();
    var destino = $('#IdCidadeDestino').val();
    var observacoes = $('#observacoes').val();
    var trecho = $('#trecho').val();

    var token = $('input[name="__RequestVerificationToken"]').val();
    var tokenadr = $('form[action="/Pedido/Create"] input[name="__RequestVerificationToken"]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    var url = "/Requisicao/Create";

    $.ajax({
        url: url,
        method: "POST",
        datatype: "json",
        headers:headersadr,
        data: {
            requisicaoId: 0,
            usuarioId: usuario,
            pessoaId: pessoa,
            IdCidadeOrigem: origem,
            IdCidadeDestino: destino,
            Observacoes: observacoes,
            Trecho: trecho,
            __RequestVerificationToken: token
        },
        success: function (data) {
            
            if (data.Requisicao > 0) {
                ListarItens(data.Requisicao)
            }
        }


    });
}
function ListarItens(requisicaoId) {
    
    var url = "/Itens/ListarItens";

    $.ajax({
        
        url: url
        , type: "GET"
        , data: { id: requisicaoId }
        , datatype: "json"
        , success: function (data) {
            
            var divItens = $("#divItens");
            divItens.empty();
            divItens.show();
            divItens.html(data);
        }
    });

}

