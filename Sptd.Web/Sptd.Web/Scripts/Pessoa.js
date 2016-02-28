function SalvarPessoa() {

    debugger
    var cpf = $("#cpf").val();
    var nome = $("#nome").val();
    var dt_Nascimento = $("#dt_Nascimento").val();
    var rg = $("#rg").val();
    var orgaoemissor = $("#orgaoemissor").val();
    var dt_Emissao = $("#dt_Emissao").val();
    var cns = $("#cns").val();
    var nome_Mae = $("#nome_Mae").val();
    var nome_Pai = $("#nome_Pai").val();
    var tel = $("#tel").val();
    var cel = $("#cel").val();
    var email = $("#email").val();

    var token = $('input[name="__RequestVerificationToken"]').val();
    var tokenadr = $('form[action="/Pessoa/Create"] input[name="__RequestVerificationToken"]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;


    var url = "/Pessoa/Create";

    $.ajax({
        url: url
        , type: "POST"
        , datatype: "json"
        , headers: headersadr
        , data: {
            pessoaId: 0
            , Cpf: cpf
            , Nome: nome
            , Dt_Nascimento: dt_Nascimento
            , Rg: rg, OrgaoEmissor: orgaoemissor
            , Dt_Emissao: dt_Emissao
            , Cns: cns
            , Nome_Mae: nome_Mae
            , Nome_Pai: nome_Pai
            , Tel: tel
            , Cel: cel
            , Email: email
            , __RequestVerificationToken: token
        }
         , success: function (data) {
             debugger
             if (data.PessoaResult > 0) {
                 CriaCampEndereco(data.PessoaResult);
             }
         }
    });
}
debugger
function CriaCampEndereco(pessoaId) {
    debugger
    url = "End/ListarItens";
    $.ajax({
        url: url
        , type: "GET"
        , data: { id: pessoaId }
         , datatype: "html"
        , success: function (data) {
            var divEndereco = $("#divEndereco");
            divEndereco.empty();
            divEndereco.show();
            divEndereco.html(data);
        }

    });
}

//function SalvarEndereco() {
//    debugger
//    var cep = $("#cep").val();
//    var rua = $("#rua").val();
//    var numero = $("#numero").val();
//    var bairro = $("#bairro").val();
//    //var estado = $("#estado").val();
//    //var cidade = $("#numero").val();

//    var token = $('input[name="__RequestVerificationToken"]').val();
//    var tokenadr = $('form[action="/Endereco/Create"] input[name="__RequestVerificationToken"]').val();
//    var headers = {};
//    var headersadr = {};
//    headers['__RequestVerificationToken'] = token;
//    headersadr['__RequestVerificationToken'] = tokenadr;

//    var url = "Endereco/Create";

//    $.ajax({
//        url: url
//        , type: "POST"
//        , datatype: "json"
//        , headers: headersadr
//        , data: {
//            enderecoId: 0
//            , Cep: cep
//            , Rua: rua
//            , numero: numero
//            , bairro: bairro
//            , __RequestVerificationToken: token
//             , success: function (data) {
//                 debugger
//                 if (data.enderecoResul > 0) {
//                     //ListarItens(data.Resultado);
//                 }
//             }
//        }

//    });
//}