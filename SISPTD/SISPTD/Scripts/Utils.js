var chosen_placeholder = "Selecione uma opção";

$('.chosen-select').chosen({
    width: "100%",
    placeholder_text_single: chosen_placeholder
});

$('.chosen-select-deselect').chosen({
    allow_single_deselect: true,
    placeholder_text_single: chosen_placeholder
});

