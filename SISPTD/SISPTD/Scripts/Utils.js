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
    $('.procurarPessoa').change(function () {
        var cpf = $('#buscar').val();
        $.ajax({
            method: 'GET',
            url: "/Pessoa/Pesquisar/?cpf=" + cpf,
            //url: '/DistribProcesso/Pesquisar',data: cpf,
            success: function (data) {
                console.debug
                $('#paciente').val(data.Nome);
            },
            error: function (data) {
                console.error
            }
        });
    });
});
