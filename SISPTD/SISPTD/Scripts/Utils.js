var chosen_placeholder = "Selecione uma opção";


$('.chosen-select').chosen({
    width: "100%",
    placeholder_text_single: chosen_placeholder
});

$('.chosen-select-deselect').chosen({
    allow_single_deselect: true,
    placeholder_text_single: chosen_placeholder
});

//chosen ajax
$('#cidId').ajaxChosen({
    type: 'GET',
    url: $(this).data('url'),
    jsonTermKey: "query",
    dataType: 'json',
},
null,
{
    width: "100%",
    placeholder_text_single: "Buscar o Serviço",
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

    //Localiza Pessoa p cadastrar usuario
    $('.buscarPessoaUser').change(function () {

        var cpf = $('#login').val();
        $.ajax({
            method: 'GET',
            url: "/Pessoa/Pesquisar/?cpf=" + cpf,
            success: function (data) {
                if (data.Id > 0) {
                    $('#nomePessoaUser').val(data.Nome);
                    $('#idPessoaUser').val(data.Id)
                }
                else {
                    alert("Pessoa não encontrada, Verifique o nome de login! ")
                }
            },
            error: function (data) {
                alert("Algo está errado, não foi possível pesquisar o paciente!");
            }
        });
    });

    //Localiza Medico na Solicitação de pericia
    $('.procurarMedico').change(function () {
        debugger;
        var cpf = $('#buscarMedico').val();
        $.ajax({
            method: 'GET',
            url: "/Pessoa/PesquisarMedico?cpf=" + cpf,
            success: function (data) {

                $('#nomeDoMedico').val(data.Nome);
                $('#idDoMedico').val(data.Id);
                $('#cnsDoMedico').val(data.Cns);
                $('#telDoMedico').val(data.Tel);
                $('#crmDoMedico').val(data.Crm);


                //top._pessoaId = data.Id;
            },
            error: function (data) {
                alert("Algo está errado, não foi possível pesquisar o paciente!");
            }
        });
    });

    //Localiza Paciente na Solicitaçõa de pericia
    $('#btnBuscaPacPericia').click(function () {
        debugger;
        var cpf = $('#buscarPacientePericia').val();
        $.ajax({
            method: 'GET',
            url: "/Pessoa/Pesquisar/?cpf=" + cpf,
            success: function (data) {
                debugger;
                $('#nomeDoPacientePericia').val(data.Nome);
                $('#idDoPacientePericia').val(data.Id)
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
    $(".date").datepicker({
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
$(function () {
    $("#dt_Nascimento").datepicker({
        showOn: "button",
        buttonImage: "calendario.png",
        buttonImageOnly: true
    });
});

//Aplica mascara
$(function () {
    $('.cpf').mask('999.999.999-99');
    $('.date').mask('99/99/9999');
    $('.cep').mask('99999-999');
    $('.cns').mask('999 9999 9999 9999');
    $('.telefone').mask('(99) 9999-9999');
});

//$('.nav > li > a[href="' + location.pathname + '"]').parent().addClass('active').siblings().removeClass('active');








