var chosen_placeholder = "Selecione uma opção";


$('.chosen-select').chosen({
    width: "100%",
    placeholder_text_single: chosen_placeholder,
    allow_single_deselect: true
});

$('.chosen-select-deselect').chosen({
    allow_single_deselect: true,
    placeholder_text_single: chosen_placeholder
});

//chosen ajax cid
$('#cidId').ajaxChosen({
    type: 'GET',
    url: $(this).data('url'),
    jsonTermKey: "query",
    dataType: 'json',
    allow_single_deselect: true,
},
null,
{
    width: "100%",
    placeholder_text_single: "Buscar o Serviço",
   
});

//chosen ajax Setor
$('#setorId').ajaxChosen({
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

//chosen ajax busca cidade
$('#IdCidade').ajaxChosen({
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


//chosen ajax busca Clinica
$('#clinicaId').ajaxChosen({
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


        
        var cpfAcomp = $('#cpfAcomp').val();
        var acomp = $('.paciente').text();
        var cpf = $('#buscar').val();
        var cpfPaciente = $('#buscarPaciente').val();


        if (cpf == cpfPaciente) {
            $('.paciente').text("CPF do Acompnhante não pode ser o mesmo do paciente!")

        }

        else if (acomp == cpfAcomp) {
            $('.paciente').text("Ops! Já consta esse Acompnhante na equisição!")

        }
        else {
            $.ajax({
                method: 'GET',
                url: "/Pessoa/Pesquisar/?cpf=" + cpf,

                success: function (data) {
                    $('#cpfAcomp').val(data.Nome);
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
    $('.buscarPacienteDistrib').click(function () {
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
                    var btCriaPessoa = $('#btnCriaPessoa');
                    btCriaPessoa.show();
                    $('<a href="/Pessoa/CreateFuncionario/" title="Cadastra Pessoa" class="btn btn-primary">Cadastra Funcionario</a>').appendTo('#btnCriaPessoa');

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
                if (data.Id > 0) {
                    $('#nomeDoMedico').val(data.Nome);
                    $('#idDoMedico').val(data.Id);
                    $('#cnsDoMedico').val(data.Cns);
                    $('#telDoMedico').val(data.Tel);
                    $('#crmDoMedico').val(data.Crm);
                    $('#celDoMedico').val(data.Cel);

                }
                else {
                    debugger;
                    var btCriaMedico = $('#btCriaMedico');
                    btCriaMedico.show();
                    $('<a href="/Pessoa/CreateMedico/" title="Cadastra Novo Medico" class="btn btn-primary" >Cadastrar Medico</a>').appendTo('#btCriaMedico');
                }



                //top._pessoaId = data.Id;
            },
            error: function (data) {
                alert("Algo está errado, não foi possível pesquisar o paciente!");
            }
        });
    });

    //Localiza Paciente na Solicitaçõa de pericia
    $('#btnBuscaPacPericia').click(function () {
        
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

    //Localiza Paciente no Agendamento
    $('#btnBuscaPacAgendamento').click(function () {
        debugger
        var cpf = $('#buscarPacienteAgendamento').val();
        $.ajax({
            method: 'GET',
            url: "/Pessoa/Pesquisar/?cpf=" + cpf,
            success: function (data) {
                debugger;
                $('#nomeDoPacienteAgendamento').val(data.Nome);
                $('#idDoPacienteAgendamento').val(data.Id)
                //top._pessoaId = data.Id;
            },
            error: function (data) {
                alert("Algo está errado, não foi possível pesquisar o paciente!");
            }
        });
    });

    //Localiza processo na Movimentação
    $('#btnBuscaProc').click(function () {
        debugger;
        var nProcesso = $('#nProcesso').val();
        
        $.ajax({
            method: 'GET',
            url: "/Processo/BuscaProcesso/?nProcesso=" + nProcesso,
            success: function (data) {
                $('#pacienteNomeProc').val(data.pacienteNome);
                $('#pacienteCpfProc').val(data.pacienteCpf)
            },
            error: function (data) {
                alert("Ops!, Verifique o Valor no Campo de Busca!");
            }
        });

       
    });


});

function AddPessoaLista() {
    debugger
    var url = $('#AddItens').data("url").trim();
    var id = $('#AddItens').data("id");
    var pacienteId = top._pessoaId;
    var Url = url + "?id=" + id + "&pacienteId=" + pacienteId;
    var removeAcompanhante = $('#removeAcompanhante').val();
    var form = $('form').serialize();
    $.post(Url, $('form').serialize(),

        function (data) {
            if (data.id == removeAcompanhante) {
                $('.paciente').text("Ops! Já consta esse Acompnhante na equisição!")
            }

            $("#divItens").html(data);


        });

}


function Limpar() {
    debugger
    $('#btnLimpar').click(function () {
        $('#nome_pessoa').text("");
        $('#procurarAcompanhentes').attr('value', '');
    });
}
function RemoveAcompanhanteLista() {
    var url = $('#RemoveItens').data("url").trim();
    var id = $('#RemoveItens').data("id");
    var Url = url + "?id=" + id;
    var form = $('form').serialize();
    $.post(Url, $('form').serialize(),
        function (data) {

            $("#divItens").html(data);
        });
}



$




//Dates pickes
$('.date').datepicker({
    language: "pt-BR",
    format: "dd/mm/yyyy",
    clearBtn: true,
    orientation: "bottom auto",
    calendarWeeks: true,
    toggleActive: true,
    autoclose: true
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
// time do alert 
$(function () {
    if ($(".alert").length) {
        window.setTimeout(function () {
            $(".alert").fadeOut();
        }, 5000);
    };
});
// Drop estado cidade

$('.uf-change').on('change', function () {
    
    var id = $(this).val(),
      el = $(this).data("target-select"),
      route = $(this).data("url"),
      $targetSelect = $(el);

    $targetSelect.prop("disabled", true).html('<option>Carregando...</option>').trigger("chosen:updated");
    $.ajax({
        url: route,
        data: {
            Id: id
        }
    }).success(function (data) {
        $targetSelect[0].options.length = 0;
        var options = '';
        $.each(data, function (i, cidade) {
            options += '<option value="' + cidade.value + '">' + cidade.text + '</option>';
        });
        $targetSelect.html(options).trigger("chosen:updated");
    }).fail(function (error) {
        console.log(error);
        $targetSelect.html('');
    }).always(function () {
        $targetSelect.prop("disabled", false).trigger("chosen:updated");
    });
});


// Drop setor no usuario



//Abrir em nova Janela
//$(function novaJanela (URL){
//    window.open(URL,"janela1","width=800,height=600,directories=no,location=no,menubar=no,scrollbars=no,status=no,toolbar=no,resizable=no")
//    })



