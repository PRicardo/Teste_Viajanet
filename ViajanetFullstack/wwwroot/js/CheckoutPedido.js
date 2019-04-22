$(document).ready(function () {
    var pedido = JSON.parse(localStorage.getItem('PedidoCliente'));

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

    if (pedido) {
        $('#tablePedido').append("<tr>"
            + "<td>" + pedido.origem + "</td>"
            + "<td>" + pedido.destino + "</td>"
            + "<td>" + pedido.dataIda + "</td>"
            + "<td>" + pedido.dataVolta + "</td>"
            + "<td> R$" + pedido.totalPedido + "</td>"
            + "</tr>");

        $('#btnConfirmacaoPedido').fadeIn();
    }
    
    $('#btnConfirmacaoPedido').click(function () {
        pedido.Ip = userIp,
        pedido.Pagina = location.pathname.substring(1);
        pedido.Browser = navigator.getBrowser;

        $.ajax({
            url: 'https://localhost:44360/api/PedidosClientes',
            type: 'PUT',
            data: JSON.stringify(pedido),
            contentType: "application/json",
            success: function (data) {
                localStorage.clear();
                window.location.replace("../html/ConfirmacaoPedido.html");
            },
            error: function (data) {

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