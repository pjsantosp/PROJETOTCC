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

    //Localiza paciente no DistribProcesso
    $('.buscarPacienteDistrib').change(function () {
        debugger;
        var cpf = $('#buscarPacienteDistrib').val();
        $.ajax({
            method: 'GET',
            url: "/Pessoa/Pesquisar/?cpf=" + cpf,
            success: function (data) {

                $('#nomePacienteDistrib').val(data.Nome);
                $('#idPacienteDistrib').val(data.Id)
                //top._pessoaId = data.Id;
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

$(function () {
    $("#dt_Nascimento").datepicker({
        language: "pt-BR",
        format: "dd/mm/yyyy",
        clearBtn: true,
        orientation: "bottom auto",
        calendarWeeks: true,
        toggleActive: true,
        autoclose: true

    });
    //$("#calendario").datepicker();
});
$(function() {
    $("#dt_Nascimento").datepicker({
        showOn: "button",
        buttonImage: "calendario.png",
        buttonImageOnly: true
    });
});
$(function() {
    $("#calendario").datepicker({
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo','Segunda','Terça','Quarta','Quinta','Sexta','Sábado','Domingo'],
        dayNamesMin: ['D','S','T','Q','Q','S','S','D'],
        dayNamesShort: ['Dom','Seg','Ter','Qua','Qui','Sex','Sáb','Dom'],
        monthNames: ['Janeiro','Fevereiro','Março','Abril','Maio','Junho','Julho','Agosto','Setembro','Outubro','Novembro','Dezembro'],
        monthNamesShort: ['Jan','Fev','Mar','Abr','Mai','Jun','Jul','Ago','Set','Out','Nov','Dez']
    });
});








