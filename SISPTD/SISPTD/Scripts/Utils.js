var chosen_placeholder = "Selecione uma opção";


$('.chosen-select').chosen({
    width: "100%",
    placeholder_text_single: chosen_placeholder
});

$('.chosen-select-deselect').chosen({
    allow_single_deselect: true,
    placeholder_text_single: chosen_placeholder
});


$(document).ready(function () {

    //Adicionar acompanhantes------------
    $('#procurarAcompanhentes').click(function () {
        var cpf = $('#buscar').val();
        var cpfPaciente = $('#buscarPaciente').val();
        if (cpf == cpfPaciente) {
            $('.paciente').text("CPF do Acompnhante não pode ser o mesmo do paciente!")
        }
        else {
            $.ajax({
                method: 'GET',
                url: "/Pessoa/Pesquisar/?cpf=" + cpf,

                success: function (data) {
                    $('.paciente').text(data.Nome);
                    $('#pessoaId').val(data.Id);
                    $('#AddItens').data('id', data.Id);
                    $('#idPai').data('id', data.Id);
                },
                error: function (data) {
                    alert("Algo está errado!");
                }
            });
        }


    });
    //Adicionar acompanhantes--------------


    //Localiza pessoa na requisicao

    $('.procurarPaciente').change(function () {

        var cpf = $('#buscarPaciente').val();
        $.ajax({
            method: 'GET',
            url: "/Pessoa/Pesquisar/?cpf=" + cpf,
            success: function (data) {

                $('#nomeDoPaciente').val(data.Nome);
                $('#idDoPaciente').val(data.Id)
                top._pessoaId = data.Id;
            },
            error: function (data) {
                alert("Algo está errado, não foi possível pesquisar o paciente!");
            }
        });
    });
   
});

function AddPessoaLista() {
    var url = $('#AddItens').data("url").trim();
    var id = $('#AddItens').data("id");
    var pacienteId = top._pessoaId;
    var Url = url + "?id=" + id + "&pacienteId=" + pacienteId;
    var form = $('form').serialize();
    $.post(Url, $('form').serialize(),
        
        function (data) {
            console.log(data)
            $("#divItens").html(data);
        });
}
function RemoveAcompanhanteLista() {
    var url = $('#RemoveItens').data("url").trim();
    var id = $('#RemoveItens').data("id");
    //var idPai = top._pessoaId;
    var Url = url + "?id=" + id;
    var form = $('form').serialize();
    $.post(Url, $('form').serialize(),
        function (data) {

            $("#divItens").html(data);
        });
}

//function SalvarRequisicao() {


//    var usuario = $('#usuarioId').val();
//    var origem = $('#IdCidadesOrigem').val();
//    var destino = $('#IdCidadesDestino').val();
//    var observacoes = $('#observacoes').val();
//    var trecho = $('#trecho').val();
//    var via = $('#via').val();
//    var agendamento = $('#agendaId').val();

//    var token = $('input[name="__RequestVerificationToken"]').val();
//    var tokenadr = $('form[action="/Requisicao/Create"] input[name="__RequestVerificationToken"]').val();
//    var headers = {};
//    var headersadr = {};
//    headers['__RequestVerificationToken'] = token;
//    headersadr['__RequestVerificationToken'] = tokenadr;

//    var url = "/Requisicao/Create";

//    $.ajax({
//        url: url,
//        method: "POST",
//        datatype: "json",
//        headers: headersadr,
//        data: {
//            requisicaoId: 0,
//            usuarioId: usuario,
//            IdCidadesOrigem: origem,
//            IdCidadesDestino: destino,
//            Observacoes: observacoes,
//            Trecho: trecho,
//            Via: via,
//            Agendamento: agendamento,
//            __RequestVerificationToken: token
//        },
//        success: function (data) {

//            if (data.Requisicao > 0) {

//                ListarItens(data.Requisicao)
//            }
//        }


//    });
//}







