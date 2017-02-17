$(document).ready(function () {
    var padraoItensPorPagina = 20;

    // 1.1//Carregar quantidade de itens
    var qtdItens = 0;

    $.ajax({
        type: "GET",
        url: "qtdItens",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            //exibe o resultado no console do chrome
            console.log(data);
            $("#qtdItens").html(data);
            qtdItens = parseInt(data);
            CriarPainel(qtdItens);

            var inicial = 1;
            var final = padraoItensPorPagina;
            var numeroPaginaSolicitada = 1;
            var totalPaginas = parseInt(qtdItens / padraoItensPorPagina) + 1;

            PuxarListar(inicial, final, numeroPaginaSolicitada, totalPaginas);
        }
    })

    // 1.2 Baseado na quantidade de itens, carregar objetos em tela

    function CriarPainel(qtdItens) {

        console.log("Método Iniciado Paginação");
        //de acordo com a qtd de itens determinamos a qtd de páginas
        var totalPaginas = parseInt(qtdItens / padraoItensPorPagina + 1);

        $('.demo4_top,.demo4_bottom').bootpag({

            total: totalPaginas, //enviar quantidade de itens
            page: 1,  //página inicial a ser exibida
            maxVisible: 5,//numero fixo
            leaps: true,
            firstLastUse: true,
            first: '←',
            last: '→',
            wrapClass: 'pagination',
            activeClass: 'active',
            disabledClass: 'disabled',
            nextClass: 'next',
            prevClass: 'prev',
            lastClass: 'last',
            firstClass: 'first'
        }).on("page", function (event, num) {
            //O método on só é disparado quando ocorre o click do mouse

            //Quando o usuário clicar vamos identificar o número da pagina que ele
            //esta pedindo, obtendo assim a posicao dos item inicial e final da sequencia
            //a variabel num nos dá a posicao
            LimparResultadosAnteriores();
            totalPaginas = parseInt(qtdItens / padraoItensPorPagina + 1);
            PuxarListar(null, null, num, totalPaginas);
        });
    }

    //enviar lista de array da página
    function PuxarListar(numINI, numFinal, numeroPaginaSolicitada, totalPaginas) {

        console.log("Puxar lista iniciado")

        var paginacaoFiltro = [];

        var initial;
        var final;

        if (numINI == null || numFinal == null) {

            initial = numeroPaginaSolicitada * padraoItensPorPagina;
            final = initial + padraoItensPorPagina;

            console.log("PuxarListar - totalpaginas :" + totalPaginas);
            console.log("PuxarLista: - numero pagina solicitada " + numeroPaginaSolicitada);
            console.log("Inicial :" + initial);
            console.log("Final :" + final);

        } else {
            initial = numINI;
            final = numFinal;
        }


        paginacaoFiltro.push(initial);
        paginacaoFiltro.push(final);


        $.ajax({
            type: "POST",
            url: "ExibicaoPaginas",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(paginacaoFiltro),
            success: function (data) {  //exibe o resultado no console do chrome

                PreencherHTML(data);
            }
        })


        paginacaoFiltro = [];

        initial = 0;
        final = 0;

    }

    function PreencherHTML(data) {
        console.log("Preencher HTml iniciado")
        data = JSON.parse(data);

        console.log(data);

        $.each(data, function (name, value) {

            var Id;
            var assunto;
            var destinatario;
            var emailRemetente;
            var mensagem;
            var nomeCliente;
            var nomeRemetente;

            try { Id = value.Id } catch (e) { Id = "" };
            try { assunto = value.assunto } catch (e) { assunto = "" };
            try { destinatario = value.destinatario } catch (e) { destinatario = "" };
            try { emailRemetente = value.emailRemetente } catch (e) { emailRemetente = "" };
            try { mensagem = value.mensagem } catch (e) { mensagem = "" };
            try { nomeCliente = value.nomeCliente } catch (e) { nomeCliente = "" };
            try { nomeRemetente = value.nomeRemetente } catch (e) { nomeRemetente = "" };

            $("#painelLista").append(
                "<tr class='itemLinha'>" +

                    "<td> <input id='" + Id + "'  class='check' type='checkbox' /></td>" +
                    "<td>" + Id + "</td>" +
                    "<td>" + assunto + "</td>" +
                    "<td>" + destinatario + "</td>" +
                    "<td>" + emailRemetente + "</td>" +
                    "<td>" + mensagem + "</td>" +
                    "<td>" + nomeCliente + "</td>" +
                    "<td>" + nomeRemetente + "</td>" +
                "</tr>")
        })
    }

    function LimparResultadosAnteriores() {
        $("#painelLista").empty();


    }









})