
$(document).ready(function () {

    

    $('#but').click(function () {

        var _assunto = $('#assunto').val();
        var _nomeRemetente = $('#nomeRemetente').val();
        var _emailremetente = $('#emailRemetente').val();
        var _destinatario = $('#destinatario').val();
        var _mensagem = $('#mensagem').val();

        var model =
        {
            nomeCliente: "pizzaPortuga",
            assunto: _assunto,
            nomeRemetente: _nomeRemetente,
            emailRemetente: _emailremetente,
            destinatario: _destinatario,
            mensagem: _mensagem
        };


        $.ajax({
            type: "POST",
            data: JSON.stringify(model),
            url: "http://52.54.242.89:90/api/EmailAPI",
            contentType: "application/json",
			dataType: "json"

        }).success(function () {


            alert("Email enviado com sucesso! ")


        }).error(function () {

            console.log("error")


        });


    });

    $('#formContato input, #formContato textarea').blur(function () {
        var Id = this.id;

        Id = "#" + Id;

        if ($(Id).val() == "") {
            //alert(Id + "Este campo é obrigatorio");

            $(Id).next().show();
        }
        else {

            $(Id).next().hide();

        };

    });


})