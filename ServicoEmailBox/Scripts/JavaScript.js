
$(document).ready(function () {

    var elementosMarcados = [];

        //Descobrir quais elementos estão marcados e retornar um array com este elementos
        function IterarElementos(){
            
            var itemns = $(".check");
                    
            $(itemns).each(function(){                           

                var numeroID = $(this).attr("Id");
                var verificarCheck = $(this).prop('checked');
                    
                //Se estiver marcado entao incluir no array
                if (verificarCheck == true)
                {
                    numeroID = parseInt(numeroID);
                    elementosMarcados.push(numeroID);
                }                    
                
            })          
            
        }


        $("#btnDeletar").click(function(){            
            //limpar estado do array de elementos
            elementosMarcados = null;

            //transformalo em um array vazio
            elementosMarcados = [];

            //iterar elementos e alimentar o array
            IterarElementos()
            
            //Array de elementos marcados pronto para uso, basta enviar ao back end para apagar
            $(elementosMarcados).each(function () {
                console.log(this);            
            })

            $.ajax({
                type: "POST",
                url: "ListaDeletar",
                data: JSON.stringify( elementosMarcados ),                
                //data: elementosMarcados,
                contentType: "application/json; charset=utf-8",
                success: function () {
                    alert("Deletado com sucesso")
                    location.reload()
                }
            });

            
        })

        
       



    })











