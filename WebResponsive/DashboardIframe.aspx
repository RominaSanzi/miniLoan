<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DashboardIframe.aspx.vb" MasterPageFile="~/BaseIncludes.Master" Inherits="di.financiera.webResponsive.DashboardIframe" %>

<%@ Register TagPrefix="cc1" Namespace="ControlesWeb" Assembly="ControlesWeb" %>
<%@ MasterType TypeName="di.financiera.webResponsive.BaseIncludes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/chart.js@2.9.3/dist/Chart.min.css" rel="stylesheet" />

    <script src="https://cdn.jsdelivr.net/npm/chart.js@2.9.3/dist/Chart.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js@2.9.3/dist/Chart.bundle.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ClientIDMode="Static" ContentPlaceHolderID="Body" runat="server">
    <div class="main-panel" id="main-panel">
        <script>
            validarMenu();
        </script>
        <nav class="navbar navbar-expand-lg navbar-transparent  bg-primary  navbar-absolute">
            <div class="container-fluid">
                <div class="navbar-wrapper">
                    <div class="navbar-toggle">
                        <button type="button" class="navbar-toggler">
                            <span class="navbar-toggler-bar bar1"></span>
                            <span class="navbar-toggler-bar bar2"></span>
                            <span class="navbar-toggler-bar bar3"></span>
                        </button>
                    </div>
                    <a class="navbar-brand"></a>
                </div>
            </div>
        </nav>
        <div class="panel-header panel-header-lg">
            <canvas id="bigDashboardChart"></canvas>
        </div>

        <div class="content">
            <%--<div class="row">
                <div class="col-md-12">
                    <div class="card card-chart">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6" style="padding-top: 80px; padding-bottom: 45px; background: linear-gradient(to right, #314466 0%, #48668e 50%, #808fc2 100%); position: relative; overflow: hidden;">
                                    <canvas id="chartjs-0" class="chartjs"></canvas>
                                    <script>

                                        gradientChartOptionsConfiguration = {
                                            maintainAspectRatio: false,
                                            legend: {
                                                display: false
                                            },
                                            tooltips: {
                                                bodySpacing: 4,
                                                mode: "nearest",
                                                intersect: 0,
                                                position: "nearest",
                                                xPadding: 10,
                                                yPadding: 10,
                                                caretPadding: 10
                                            },
                                            responsive: 1,
                                            scales: {
                                                yAxes: [{
                                                    display: 0,
                                                    gridLines: 0,
                                                    ticks: {
                                                        display: false
                                                    },
                                                    gridLines: {
                                                        zeroLineColor: "transparent",
                                                        drawTicks: false,
                                                        display: false,
                                                        drawBorder: false
                                                    }
                                                }],
                                                xAxes: [{
                                                    display: 0,
                                                    gridLines: 0,
                                                    ticks: {
                                                        display: false
                                                    },
                                                    gridLines: {
                                                        zeroLineColor: "transparent",
                                                        drawTicks: false,
                                                        display: false,
                                                        drawBorder: false
                                                    }
                                                }]
                                            },
                                            layout: {
                                                padding: {
                                                    left: 0,
                                                    right: 0,
                                                    top: 15,
                                                    bottom: 15
                                                }
                                            }
                                        };

                                        //var ctx = document.getElementById('chartjs-0').getContext("2d");
                                        chartColor = "#80b6f4";

                                        var ctx = document.getElementById('chartjs-0').getContext("2d");

                                        var gradientStroke = ctx.createLinearGradient(500, 0, 100, 0);
                                        gradientStroke.addColorStop(0, '#80b6f4');
                                        gradientStroke.addColorStop(1, chartColor);

                                        var gradientFill = ctx.createLinearGradient(0, 170, 0, 50);
                                        gradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
                                        gradientFill.addColorStop(1, "rgba(249, 99, 59, 0.40)");

                                        new Chart(document.getElementById("chartjs-0"), {
                                            type: 'line', data: {
                                                labels: ["January", "February", "March", "April", "May", "June", "July"],
                                                datasets: [{
                                                    label: "Data",
                                                    borderColor: chartColor,
                                                    pointBorderColor: chartColor,
                                                    pointBackgroundColor: "#80b6f4",
                                                    pointHoverBackgroundColor: "#80b6f4",
                                                    pointHoverBorderColor: chartColor,
                                                    pointBorderWidth: 1,
                                                    pointHoverRadius: 7,
                                                    pointHoverBorderWidth: 2,
                                                    pointRadius: 5,
                                                    fill: true,
                                                    backgroundColor: gradientFill,
                                                    borderWidth: 2,
                                                    data: [65, 59, 100, 250, 95, 55, 40]
                                                }]
                                            }, options: gradientChartOptionsConfiguration
                                        });</script>
                                </div>
                                <div class="col-md-6">
                                    <canvas id="chartjs-2" class="chartjs"></canvas>
                                    <script>new Chart(document.getElementById("chartjs-2"), {
                                            "type": "line", "data": {
                                                "labels": ["January", "February", "March", "April", "May", "June", "July"],
                                                "datasets": [{ "label": "My First Dataset", "data": [65, 59, 80, 81, 56, 55, 40], "fill": false, "borderColor": "rgb(75, 192, 192)", "lineTension": 0.1 }]
                                            }, "options": {}
                                        });</script>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
            <div id="graficos" runat="server"></div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="assets/site/site.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            site.initDashboardPageCharts();
        });
        var map;
        function initMap() {
            var myLatLng = { lat: -34.5891849, lng: -58.4097305 };
            map = new google.maps.Map(document.getElementById('map'), {
                center: myLatLng,
                zoom: 12,
                mapTypeId: 'hybrid'
            });

            var locations = [
                ['CENTRO', -34.603526673, -58.3774303351, 1],
                ['ALMAGRO', -34.6032627026, -58.4193483351, 2],
                ['BELGRANO', -34.5610677784, -58.4576320993, 4],
                ['NORTE', -34.5946913333, -58.4011695007, 5],
                ['PORTAL ROSARIO', -32.9478000779, -60.6375162201, 6],
                ['LINIERS', -34.6392490263, -58.5267304095, 7],
                ['LOMAS DE ZAMORA', -34.7616311111, -58.3996142669, 8],
                ['QUILMES', -34.7035956965, -58.292596558, 9],
                ['SAN ISIDRO', -34.4696732302, -58.5168686899, 10],
                ['MORON', -34.6504324014, -58.6197814569, 11],
                ['CONSTITUCION', -34.6279563772, -58.3836368214, 12],
                ['SAN JUSTO', -34.6811676251, -58.557274904, 14],
                ['LA PLATA', -34.8952975695, -57.9782909075, 15],
                ['SAN MARTIN', -34.5786348063, -58.536416104, 16],
                ['CIUDAD FRAVEGA', -34.6054797019, -58.4058506458, 18],
                ['MERLO', -34.6669478137, -58.7268238349, 20],
                ['RAMOS MEJIA', -34.6403992506, -58.5622650963, 21],
                ['BERAZATEGUI', -34.761835871, -58.2094487909, 22],
                ['SAN FERNANDO', -34.4396956902, -58.5586652233, 23],
                ['OUTLET POMPEYA', -34.6539613629, -58.4166440274, 24],
                ['MONTE GRANDE', -34.8159636534, -58.4678162767, 25],
                ['VILLA BALLESTER', -34.5319491217, -58.5383495262, 26],
                ['BOULOGNE', -34.5091131614, -58.5663762144, 27],
                ['CALETA OLIVIA', -46.4433706, -67.5191229, 28],
                ['LANUS', -34.7085237804, -58.3895322499, 30],
                ['SAN MIGUEL', -34.5456189435, -58.7074715566, 31],
                ['PALMAS DE PILAR', -34.4562619924, -58.7203845625, 32],
                ['VILLA DEL PARQUE', -34.6019836109, -58.4949367584, 33],
                ['MENDOZA', -32.8852625978, -68.8408152438, 34],
                ['LAFERRERE', -34.747348128, -58.5895013293, 35],
                ['BAHIA BLANCA', -38.7209774721, -62.2655272135, 36],
                ['MAR DEL PLATA', -38.01012, -57.55257, 37],
                ['ROSARIO', -32.9478000779, -60.6375162201, 38],
                ['SANTA FE', -31.6443417981, -60.7072947516, 39],
                ['TUCUMAN', -26.8312078504, -65.2048967221, 40],
                ['SALTA', -24.7910302429, -65.4138390653, 41],
                ['PARANA', -31.7335581401, -60.532180066, 42],
                ['NEUQUEN', -38.9591474352, -68.0632039003, 43],
                ['SOLANO', -34.7800454449, -58.3112891378, 44],
                ['UNICENTER MOBILE', -34.5087206195164, -58.5219964435198, 48],
                ['RIO CUARTO', -33.12363613, -64.3471505688, 49],
                ['JOSE C. PAZ', -34.5193346095, -58.7529799318, 51],
                ['RIO GALLEGOS', -51.6206574370373, -69.217590743556, 55],
                ['MENDOZA SHOPPING', -32.9033624974, -68.7999748793, 56],
                ['SALTA SHOPPING', -24.7815012945, -65.4032997991, 59],
                ['PALERMO', -34.589230133, -58.4097438128, 61],
                ['SAN JUAN VIP', -31.5362475581, -68.5222051849, 62],
                ['SALTA VIP', -24.7915730585, -65.4113062659, 63],
                ['FLORIDA VIP', -34.6046420021, -58.3752479497, 65],
                ['TUCUMAN VIP', -26.8301901046, -65.2062417121, 66],
                ['ROSARIO VIP', -32.9460597743, -60.6398042779, 67],
                ['GRAND BOURG', -34.4867153917353, -58.7257240925402, 68],
                ['SANTA FE VIP', -31.645327921, -60.6989763719, 69],
                ['FLORES VIP', -34.6272301728, -58.4578781757, 70],
                ['CABALLITO', -34.6195794958, -58.4391480735, 71],
                ['ALTO AVELLANEDA', -34.677419421, -58.3664409344, 76],
                ['EL DORADO', -26.4083395, -54.6046359, 77],
                ['SAENZ PE\\A', -26.7886576, -60.4418885, 78],
                ['POMPEYA', -34.6507705206, -58.4166121195, 79],
                ['MENDOZA III', -32.8532459797, -68.8342570761, 80],
                ['CORDOBA VIP', -31.4145800515, -64.1842332906, 81],
                ['CORDOBA', -31.4117126141, -64.1853058431, 82],
                ['NUEVA LA PLATA', -34.862604, -58.077417, 83],
                ['TANDIL', -37.3261883241, -59.1349498142, 84],
                ['MAR DEL PLATA VIP', -37.9982174073, -57.5529498642, 92],
                ['FLORENCIO VARELA', -34.808086158, -58.2767769217, 93],
                ['ROSARIO SHOPPING', -32.9266455915, -60.6707111446, 101],
                ['ABASTO SHOPPING', -34.6039657913, -58.4105763781, 103],
                ['MORENO', -34.6494708017, -58.7878985483, 104],
                ['JUJUY', -24.1852674234, -65.3033256954, 105],
                ['PANAMERICANA I', -34.5107806875, -58.526162483, 106],
                ['COMODORO RIVADAVIA', -45.8610684566, -67.4792872192, 110],
                ['CATAMARCA', -28.4716823891, -65.7782404879, 113],
                ['BARILOCHE', -41.1370294852, -71.2973955555, 116],
                ['SAN LUIS', -33.3129363654, -66.3330253897, 118],
                ['PILAR', -34.4568390684, -58.9121936469, 119],
                ['MORENO SHOPPING', -34.6353019427, -58.7918052527, 122],
                ['CORRIENTES II', -27.4677252594, -58.8359073468, 123],
                ['RESISTENCIA II', -27.4521944237, -58.9890036908, 124],
                ['PACHECO', -34.4535302377, -58.6001122817, 125],
                ['DOT BAIRES SHOPPING', -34.54633964, -58.4865924315, 126],
                ['SAN JUSTO SHOPPING', -34.6841782887, -58.5568770296, 127],
                ['GRAL. ROCA', -39.0277948296, -67.580792945, 129],
                ['TORTUGUITAS', -34.4562619924, -58.7203845625, 130],
                ['SANTA FE NORTE', -31.6108293253, -60.6943776785, 131],
                ['SAN RAFAEL MENDOZA', -34.6164677955, -68.3299872687, 132],
                ['PERGAMINO', -33.8934947273, -60.5727212759, 133],
                ['GUALEGUAYCHU', -33.0095148762, -58.5184457772, 134],
                ['EZEIZA', -34.853376981, -58.5215990766, 135],
                ['TEMPERLEY', -34.7614045669, -58.3613358298, 136],
                ['FLORENCIO VARELA II', -34.808927, -58.274399, 143],
                ['POSADAS', -27.3673518782, -55.8956674778, 144],
                ['GONZALEZ CATAN', -34.7627879674, -58.6167276573, 145],
                ['LA RIOJA', -29.414642454, -66.8578162956, 146],
                ['ZARATE', -34.1020845345, -59.0201262638, 147],
                ['QUILMES PEATONAL', -34.7222342177, -58.2572703698, 148],
                ['FORMOSA', -26.182133, -58.165215, 149],
                ['LA PAMPA', -36.619543675, -64.2898401685, 152],
                ['LUJAN', -34.5648251457, -59.1183571152, 153],
                ['CONCORDIA', -31.3947213013, -58.0182997748, 154],
                ['SAN MIGUEL II', -34.532943, -58.7035575, 164],
                ['ROSARIO SUR', -32.9478000779, -60.6375162201, 166],
                ['SAN JUAN RAWSON', -31.580995486884, -68.5468023826754, 167],
                ['SANTIAGO DEL ESTERO', -27.7840123022, -64.2620194213, 168],
                ['CORDOBA NORTE SAGRADA FAMILIA', -31.3975029729, -64.236073746, 169],
                ['PQUE. COM. AVELLANEDA SARANDI', -34.678123, -58.332708, 171],
                ['CIPOLLETTI', -38.9385926527836, -67.9994877012896, 172],
                ['MAR DEL PLATA ALDREY SHOPPING', -38.0124168821, -57.5433248332, 174],
                ['CAMPANA', -33.8934947273, -60.5727212759, 175],
                ['CARLOS PAZ', -31.4201551197955, -64.4980447464724, 176]
            ];

            var marker, i;

            var infowindow = new google.maps.InfoWindow;

            for (i = 0; i < locations.length; i++) {
                marker = new google.maps.Marker({
                    position: new google.maps.LatLng(locations[i][1], locations[i][2]),
                    map: map
                });

                google.maps.event.addListener(marker, 'click', (function (marker, i) {
                    return function () {
                        infowindow.setContent(locations[i][0]);
                        infowindow.open(map, marker);
                    }
                })(marker, i));
            }


        }

    </script>
    <%--<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCvr2M6TEhIHcEbvZhalch7SjmKqS5f_f8&callback=initMap" async defer></script>--%>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBWmnQTm0Bvyd5BffH-vUe_bD5Ab1_4zI8&callback=initMap" async defer></script>

</asp:Content>
