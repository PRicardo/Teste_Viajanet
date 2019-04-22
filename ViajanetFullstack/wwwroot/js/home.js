$(document).ready(function () {
    $("#formCheckout").validate({
        rules: {
            airportDeparture: "required",
            airportArrive: "required",
            dataIda: "required",
            dataVolta: "required"
        },
        messages: {
            airportDeparture: "Informe o aeroporto de partida",
            airportArrive: "Informe o aeroporto de partida",
            dataIda: "Informe a data de partida",
            dataVolta: "Informe a data de retorno"
        }
    });

    function autocomplete(inp, arr) {
        var currentFocus;
        
        inp.addEventListener("input", function (e) {
            var a, b, i, val = this.value;
            
            closeAllLists();
            if (!val) { return false; }

            currentFocus = -1;
            
            a = document.createElement("DIV");
            a.setAttribute("id", this.id + "autocomplete-list");
            a.setAttribute("class", "autocomplete-items");
            
            this.parentNode.appendChild(a);
            
            for (i = 0; i < arr.length; i++) {
                if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                    b = document.createElement("DIV");
                    
                    b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                    b.innerHTML += arr[i].substr(val.length);
                    
                    b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                    
                    b.addEventListener("click", function (e) {
                        inp.value = this.getElementsByTagName("input")[0].value;
                        closeAllLists();
                    });
                    a.appendChild(b);
                }
            }
        });
        
        inp.addEventListener("keydown", function (e) {
            var x = document.getElementById(this.id + "autocomplete-list");
            if (x) x = x.getElementsByTagName("div");
            if (e.keyCode == 40) {
                currentFocus++;
                addActive(x);
            } else if (e.keyCode == 38) {
                currentFocus--;
                addActive(x);
            } else if (e.keyCode == 13) {
                e.preventDefault();
                if (currentFocus > -1) {
                    if (x) x[currentFocus].click();
                }
            }
        });

        function addActive(x) {
            if (!x) return false;
            removeActive(x);
            if (currentFocus >= x.length) currentFocus = 0;
            if (currentFocus < 0) currentFocus = (x.length - 1);
            x[currentFocus].classList.add("autocomplete-active");
        }

        function removeActive(x) {
            for (var i = 0; i < x.length; i++) {
                x[i].classList.remove("autocomplete-active");
            }
        }

        function closeAllLists(elmnt) {
            var x = document.getElementsByClassName("autocomplete-items");
            for (var i = 0; i < x.length; i++) {
                if (elmnt != x[i] && elmnt != inp) {
                    x[i].parentNode.removeChild(x[i]);
                }
            }
        }
        
        document.addEventListener("click", function (e) {
            closeAllLists(e.target);
        });
    }
    
    var airports = ["Belo Horizonte, Pampulha, Brasil",
        "Belo Horizonte, Tancredo Neves, Brasil",
        "São Paulo, Guarulhos, Brasil",
        "São Paulo, Congonhas, Brasil",
        "São Paulo, Viracopos, Brasil",
        "Rio de Janeiro, Galeão - Antônio Carlos Jobim, Brasil",
        "Rio de Janeiro, Santos Dumont, Brasil"
    ];
    
    autocomplete(document.getElementById("airportDeparture"), airports);
    autocomplete(document.getElementById("airportArrive"), airports);

    $('.datepicker').datepicker({
        language: "pt-BR",
        startDate: '0d',
        format: 'dd/mm/yyyy'
    });

    $('[name="optIdaVolta"]').change(function () {
        $(this).val() == 1 ? $('#dataVolta').prop('disabled', true) : $('#dataVolta').prop('disabled', false);
    });
    
    userIp = "";
    getIp(function (ip) {
        userIp = ip;
    });
    
    navigator.getBrowser = (function () {
        var ua = navigator.userAgent, tem,
            M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
        if (/trident/i.test(M[1])) {
            tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
            return 'IE ' + (tem[1] || '');
        }
        if (M[1] === 'Chrome') {
            tem = ua.match(/\b(OPR|Edge)\/(\d+)/);
            if (tem != null) return tem.slice(1).join(' ').replace('OPR', 'Opera');
        }
        M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
        if ((tem = ua.match(/version\/(\d+)/i)) != null) M.splice(1, 1, tem[1]);
        return M.join(' ');
    })();
    
    $('#btnPesquisar').click(function () {
        if (!$("#formCheckout").valid()) {
            return;
        }

        var data = {
            Ip: userIp,
            Pagina: location.pathname.substring(1),
            Browser: navigator.getBrowser,
            IdaVolta: $('[name="optIdaVolta"]:checked').val(),
            Origem: $('#airportDeparture').val(),
            Destino: $('#airportArrive').val(),
            DataIda: $('#dataIda').val(),
            DataRetorno: $('#dataVolta').val(),
            QtdAdultos: $('#qtdAdultos').val(),
            QtdCriancas: $('#qtdCriancas').val(),
            QtdBebes: $('#qtdBebes').val()
        };

        console.log(JSON.stringify(data))
        
        $.ajax({
            url: "https://localhost:44360/api/PedidosClientes",
            method: "POST",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                localStorage.setItem('PedidoCliente', JSON.stringify(data));
                window.location.replace("../html/CheckoutPedido.html");
            },
            error: function () {

            }
        });
    });
});

function getIp(callback) {
    function response(s) {
        callback(window.userip);

        s.onload = s.onerror = null;
        document.body.removeChild(s);
    }

    function trigger() {
        window.userip = false;

        var s = document.createElement("script");
        s.async = true;
        s.onload = function () {
            response(s);
        };
        s.onerror = function () {
            response(s);
        };

        s.src = "https://l2.io/ip.js?var=userip";
        document.body.appendChild(s);
    }

    if (/^(interactive|complete)$/i.test(document.readyState)) {
        trigger();
    } else {
        document.addEventListener('DOMContentLoaded', trigger);
    }
}