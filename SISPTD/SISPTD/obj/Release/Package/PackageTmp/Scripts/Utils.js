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
},
null,
{
    width: "100%",
    placeholder_text_single: "Digite e Selecione o CID",
    allow_single_deselect: true,

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
    placeholder_text_single: "Digite e Selecione o Setor",
    allow_single_deselect: true,
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
    allow_single_deselect: true,

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
    placeholder_text_single: "Digite e Selecione a Clinica.",
    allow_single_deselect: true,
});






$(document).ready(function () {


    //Adicionar acompanhantes------------
    $('#procurarAcompanhentes').click(function () {
        var cpf = $('#buscar').val().replace(/[^\d]+/g, "");
        var cpfPaciente = $('#buscarPaciente').val();
        cpfPaciente = cpfPaciente.replace(/[^\d]+/g, "");
        var acompanhante = $('#divItens').find('.cpfAcompanhante').val();
        if (cpf == cpfPaciente) {
            alert("Ops! O Acompanhanhte Não deve ser o Paciente");
            $('#buscar').val('');
            $('#nome_pessoa').empty();
            return;
        }

        else if (cpf == acompanhante) {
            alert("Ops! Já consta esse Acompanhante na requisição!");
            $('#buscar').val('');
            $('#nome_pessoa').empty();
            return;
        }
        else {
            var data = {
                cpf: cpf
            }
            $.get(urlRequisicao, data)
            .success(function (data) {
                $('#cpfAcomp').val(data.Nome);
                $('.paciente').text(data.Nome);
                $('#pessoaId').val(data.Id);
                $('#AddItens').data('id', data.Id);
                $('#idPai').data('id', data.Id);
            })
            .error(function (data) {
                alert("Algo está errado!");
            })
        }
    });


    //Adicionar acompanhantes--------------


    //Localiza pessoa na requisicao
    $('.procurarPaciente').change(function () {

        var cpf = $('#buscarPaciente').val();
        var data = {
            cpf: cpf
        }
        $.get(urlRequisicao, data)
        .success(function (data) {
            $('#nomeDoPaciente').val(data.Nome);
            $('#idDoPaciente').val(data.Id)
            top._pessoaId = data.Id;
        }).error(function (data) {
            alert("Ops! Não foi possível localizar Paciente Para a Requição Verifique os Dados!");
        })
        //$.ajax({
        //    method: 'GET',
        //    url: "/Pessoa/Pesquisar/?cpf=" + cpf,
        //    success: function (data) {

        //        $('#nomeDoPaciente').val(data.Nome);
        //        $('#idDoPaciente').val(data.Id)
        //        top._pessoaId = data.Id;
        //    },
        //    error: function (data) {
        //        alert("Algo está errado, não foi possível pesquisar o paciente!");
        //    }
        //});
    });

    //Localiza paciente no Processo
    $('.buscarPacienteDistrib').click(function () {
        debugger;
        var cpf = $('#buscarPacienteDistrib').val();
        var data = {
            cpf: cpf
        }

        $.get(urlPacienteProcesso, data)
            .success(function (data) {
                $('#nomePacienteDistrib').val(data.Nome);
                $('#idPacienteDistrib').val(data.Id)

            })
            .error(function (data) {
                alert("Algo está errado, não foi possível pesquisar o paciente!");
            })


    });

    //Localiza Pessoa p/ Manutenção
    $('.buscarPessoaManutencao').click(function () {
        debugger;
        var cpf = $('#buscarPessoaManutencao').val();
        var data = {
            cpf: cpf
        }
        $.get(urlManutencao, data).success(function (data) {
            $('#idPessoaManutencao').val(data.Id)
            $('.cpf').val(data.Cpf)
            $('#nome').val(data.Nome);
            $('#dt_Nascimento').val(data.DtNascimento);
            $('#rg').val(data.Rg);
            $('#orgao_emissor').val(data.OrgaoEmissor);
            $('#cns').val(data.Cns);
            $('#crm').val(data.Crm);
            $('#nome_Mae').val(data.Mae);
            $('#nome_Pai').val(data.Pai);
            $('#tel').val(data.Tel);
            $('#cel').val(data.Cel);
            $('#email').val(data.Email);
            $('#Endereco_cep').val(data.Cep);
            $('#Endereco_rua').val(data.Rua);
            $('#Endereco_numero').val(data.Numero);
            $('#Endereco_bairro').val(data.Bairro);
        })
        .error(function (data) {
            alert("Ops ! não foi possível realizar a pesquisar da Pessoa!");
        })

    });

    //Localiza Pessoa p cadastrar usuario
    $('#btnBuscarPessoaUser').click(function () {
        debugger
        var cpf = $('#login').val();
        var data = {
            cpf: cpf
        }

        $.get(urlUsuario, data)
            .success(function (data) {
                if (data.Id > 0) {
                    $('#nomePessoaUser').val(data.Nome);
                    $('#idPessoaUser').val(data.Id)
                }
                else {
                    var btCriaPessoa = $('#btnCriaPessoa');
                    btCriaPessoa.show();
                }
            })
        .error(function (data) {
            alert("Algo está errado, não foi possível pesquisar o Usuário!");

        })
    });

    //Localiza Medico na Solicitação de pericia
    $('.procurarMedico').change(function () {
       
        var cpf = $('#buscarMedico').val();
        var data = {
            cpf: cpf
        }
        $.get(urlBuscarMedico, data)
            .success(function (data) {
                if (data.Id > 0) {
                    $('#nomeDoMedico').val(data.Nome);
                    $('#idDoMedico').val(data.Id);
                    $('#cnsDoMedico').val(data.Cns);
                    $('#telDoMedico').val(data.Tel);
                    $('#crmDoMedico').val(data.Crm);
                    $('#celDoMedico').val(data.Cel);

                }
                else {
                    var btCriaMedico = $('#btCriaMedico');
                    btCriaMedico.show();
                    //$('<a href="/Pessoa/CreateMedico/" title="Cadastra Novo Medico" class="btn btn-primary" >Cadastrar Medico</a>').appendTo('#btCriaMedico');
                }
            })
            .error(function (data) {
                alert("Algo está errado, não foi possível pesquisar o paciente!");
            })
    });

    //Localiza Paciente na Solicitaçõa de pericia
    $('#btnBuscaPacPericia').click(function () {

        var nProcesso = $('#buscarPacientePericia').val();
        var data = {
            nProcesso: nProcesso
        }
        $.get(urlBuscaPacPericia, data)
            .success(function (data) {
                $('#nomeDoPacientePericia').val(data.pacienteNome);
                $('#idDoPacientePericia').val(data.PacienteId)
                $('#pacienteCpf').val(data.pacienteCpf)
                $('.processoId').val(data.ProcessoId)

            }).error(function (data) {
                alert("Algo está errado, não foi possível localizar  o paciente para Pericia!");

            })

    });

    //Localiza Paciente no Agendamento
    $('#btnBuscaPacAgendamento').click(function () {
        debugger
        var cpf = $('#buscarPacienteAgendamento').val();
        var data = {
            cpf: cpf
        }
        $.get(urlBuscaPacAgendamento, data)
            .success(function (data) {
                $('#nomeDoPacienteAgendamento').val(data.Nome);
                $('#idDoPacienteAgendamento').val(data.Id)
            })
        .error(function (data) {
            alert("Algo está errado, não foi possível pesquisar o paciente!");
        })
    });


    //Localiza processo na Movimentação
    $('#btnBuscaProc').click(function () {
        debugger;
        var nProcesso = $('#nProcesso').val();

        var data = {
            nProcesso: nProcesso
        }

        $.get(urlBuscaProcesso, data)
        .success(function (data) {
            $('#pacienteNomeProc').val(data.pacienteNome)
            $('#pacienteCpfProc').val(data.pacienteCpf)
            $('#origemProcesso').val(data.origemProcesso)
        })
        .error(function () {
            alert("Ops!, Verifique o Valor no Campo de Busca!");
        })

    });


});

function AddPessoaLista() {
    var acompanhante = $('#divItens').find('td input').val();
    var url = $('#AddItens').data("url").trim();
    var id = $('#AddItens').data("id");
    if (acompanhante == id) {
        alert("Ops! Já consta esse Acompnhante na equisição!");
        $('#buscar').val('');
        $('#nome_pessoa').empty();
        return
    }
    else {
        var pacienteId = top._pessoaId;
        var Url = url + "?id=" + id + "&pacienteId=" + pacienteId;
        var removeAcompanhante = $('#removeAcompanhante').val();
        var form = $('form').serialize();
        $.post(Url, $('form').serialize(),

            function (data) {
                $("#divItens").html(data);
            });
    }
    $('#buscar').val('');
    $('#nome_pessoa').empty();

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

