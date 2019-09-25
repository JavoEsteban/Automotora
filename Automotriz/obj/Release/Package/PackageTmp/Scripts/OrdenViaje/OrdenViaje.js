    $(document).ready(function () {



        focusBootstrapSearch();
      
        //$('#txtCantidad').autoNumeric('init', { vMin: '0', vMax: '9999999999', aSep: '', aDec: '.' });
        //$('#TxtCantidadServicio').autoNumeric('init', { vMin: '0', vMax: '9999999999', aSep: '', aDec: '.' });

        $('#txtCantidad').autoNumeric('init', { vMin: '0', vMax: '9999999999', aSep: '.', aDec: ',', aSign: '', mDec: '2' });
        $('#TxtCantidadServicio').autoNumeric('init', { vMin: '0', vMax: '9999999999', aSep: '.', aDec: ',', aSign: '', mDec: '2' });
        
        CargaComboBasedeCobro();
        CargaComboTarifaServicios();

        try {
            var ID_Clientes;
            var Id_Ruta;

            ID_Clientes = $("#ID_Clientes").val();
            Id_Ruta = $("#Id_Ruta").val();
            // alert(Id_Sucursal);


            if (ID_Clientes != 0 && Id_Ruta != 0) {
                $("#CmbClientes").val(ID_Clientes).change();
                $("#CmbRutas").val(Id_Ruta).change();

                $("#CmbClientes").prop("disabled", "disabled");
                $("#CmbRutas").prop("disabled", "disabled");
                $("#CmbClientes").selectpicker("refresh");
                $("#CmbRutas").selectpicker("refresh");

                Buscar_Ordenes();
            }
            else {
                $("#CmbClientes").change(function () {
                    if ($("#CmbClientes").val() != 0 && $("#CmbRutas").val() != 0) {
                        Buscar_Ordenes();
                    }
                });

                $("#CmbRutas").change(function () {
                    if ($("#CmbClientes").val() != 0 && $("#CmbRutas").val() != 0) {
                        Buscar_Ordenes();
                    }
                });
            }

            

            
        }
        catch {

            $("#CmbClientes").change(function () {
                if ($("#CmbClientes").val() != 0 && $("#CmbRutas").val() != 0) {
                    Buscar_Ordenes();
                }
            });

            $("#CmbRutas").change(function () {
                if ($("#CmbClientes").val() != 0 && $("#CmbRutas").val() != 0) {
                    Buscar_Ordenes();
                }
            });
        }

       

    }); // Document Ready

// $('#TablaCliente').DataTable();
//$('#txtCantidad').change(function () {
//    alert('aqiii');
//    CalcularTarifaTransporte();
    

//    //$('#CmbComunas').attr('disabled', false);

//});
function txtCantidad_change() {

    CalcularTarifaTransporte();
}

function formateaNumero(num) {
    var numFormateado = new Intl.NumberFormat("de-DE").format(num);
    return numFormateado;
}

function limpiaNumero(numString) {
    var numeroLimpio = numString.replace(/[$.]/g, '');
    return parseInt(numeroLimpio);
}

// Funcion para validar las ordenes de viaje que estaran asociadas a las diferentes ordenes de transporte
$('#Btn_GenerarOrdenViaje').click(function () {

    var OrdenesTransporte = new Array();

    var indice = 0;
    // DATOS DE LA TABLA DE BULTOS



    // LEER OK !
    $.each($('#tbodyOrdenesTransporte').find('tr'), function (indexTr, itemTr) {

        var id = itemTr.id.split('-')[1];

        if ($('#chk-' + id).prop('checked')) {
            var OrdenTransporte = new Object();

            OrdenTransporte.ID_Orden_Transporte = id;

            OrdenesTransporte.push(OrdenTransporte);
        }
    });

    //$('#TblOrdenesTransporte >tbody >tr').each(function () {

    //    if ($("#chk-" + this.id + "").prop('checked')) {
    //        var OrdenTransporte = new Object();

    //        OrdenTransporte.ID_OrdenTransporte = this.id;
    //        OrdenTransporte.Estado = $("#chk-" + OrdenTransporte.ID_OrdenTransporte).prop('checked');

    //        OrdenesTransporte.push(OrdenTransporte);
    //    }

    //});
    

    // INGRESAR DATOS DE ORDEN DE VIAJE !!!




    var datos = new Object();
    datos.ID_Ruta = $("#CmbRutas").val();
    datos.ID_Clientes = $("#CmbClientes").val();
    datos.ID_TarifaTransporte = $("#txtID_TarifaTransporte").val();


    datos.Monto_Unitario = limpiaNumero($("#txtMontoTarifa").val());
    datos.Cantidad = $("#txtCantidad").val();
    datos.DescuentoTarifa = $("#txtDescuento").val();
    datos.MontoNeto = limpiaNumero($("#MontoNeto").text());
    datos.MontoIva = limpiaNumero($("#MontoIVA").text());
    datos.MontoTotal = limpiaNumero($('#MontoTotal').text());

    datos.Monto_Servicios = $("#TotalFooterServicios").text();
    datos.Monto_Transporte = limpiaNumero($("#txtTotalTransporte").val());
    datos.Monto_Tarifa = limpiaNumero($("#txtMontoTarifa").val());

    datos.ID_TipoCliente = $("#CmbTipoCliente").val();
    

    datos.Ruta = $("#CmbRutas option:selected").text();
    datos.Tipo_Cliente = $("#CmbTipoCliente option:selected").text();
    datos.Razon_Social = $("#CmbClientes option:selected").text();


    datos.BasedeCobro = $("#CmbBasedeCobro option:selected").text();
    datos.ID_BaseCobro = $("#CmbBasedeCobro option:selected").val();
    datos.TipodeCarga = $("#CmbTipoCarga option:selected").text();
    datos.ID_TipoCarga = $("#CmbTipoCarga option:selected").val();
    datos.UnidadCobro = $("#CmbUnidadCobro option:selected").text();


    var fecha = new Date(Date.now());
    var dia = '0' + fecha.getDate();
    var mes = '0' + (fecha.getMonth() + 1);
    var ano = '0000' + fecha.getFullYear();
    var hora = '0' + fecha.getHours();
    var minutos = '0' + fecha.getMinutes();
    var segundos = '00' + fecha.getSeconds();

    var fechaFinal = dia.substr(dia.length - 2) + '/' + mes.substr(mes.length - 2) + '/' + ano.substr(ano.length - 4) + ' ' + hora.substr(hora.length - 2) + ':' + minutos.substr(minutos.length - 2) + ':' + segundos.substr(segundos.length - 2);
    
    datos.FechaIngreso = fechaFinal;

    if ($('#chkServicios').prop('checked')) {
        datos.Servicios_Logisticos = "SI"
    }
    else {

        datos.Servicios_Logisticos = "NO"
    }


    // // RECORRER TARIFA SERVICIOS

    var indice = 0;
    var Total_Servicios = new Array();
    $('#TblOrdenesServicios >tbody >tr').each(function () {

        if (indice > 0) {

            var datos_TarifaServicio = new Object();


            datos_TarifaServicio.ID_Servicio = $(this).find("td").eq(0).html();

            datos_TarifaServicio.Cantidad = $(this).find("td").eq(4).html();
            datos_TarifaServicio.Tarifa = $(this).find("td").eq(3).html();
            datos_TarifaServicio.Monto_Total = $(this).find("td").eq(5).html();
            Total_Servicios.push(datos_TarifaServicio);

        }
        indice++;
    });

    console.log(datos);

    gfProceso();

    $.ajax({
        type: "POST",
        url: "/OrdenViaje/IngresaOrdenCobro",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { Datos: datos, OrdenesTransporte: OrdenesTransporte, Servicios: Total_Servicios },

        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT

                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO


                //   alert(JSON.stringify(detalle));


                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR 
                {


                    swal({
                        title: 'TOPTAINER ORDEN DE COBRO ',
                        text: 'Se genero Orden de Cobro',
                        type: 'success',
                        confirmButtonColor: '#FE6A00',
                        confirmButtonText: 'OK',
                        confirmButtonClass: 'btn btn-info'
                    }).then(function () {
                        window.location = document.referrer;
                    });


                    gfProceso();



                }  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                else {



                    swal({
                        title: 'TOPTAINER ORDEN DE COBRO',
                        text: 'Problemas al generar Orden de Cobro ' + data.Detalle_Error + '',
                        type: 'error',
                        confirmButtonColor: '#FE6A00',
                        confirmButtonText: 'OK',
                        confirmButtonClass: 'btn btn-info'
                    });


                    gfProceso();

                    return false;
                }


            } catch (err) { // ACCIÓN DEL ERROR DE JAVASCRIPT
                swal({
                    title: 'TOPTAINER ORDEN DE COBRO',
                    text: 'Problemas al generar Orden de Cobro ' + err + '',
                    type: 'error',
                    confirmButtonColor: '#FE6A00',
                    confirmButtonText: 'OK',
                    confirmButtonClass: 'btn btn-info'
                });


                gfProceso();
            }



        },
        error: function (xhr, textStatus, errorThrown) { // ACCIÓN DEL ERROR DEL SERVICIO AJAX.

            swal({
                title: 'TOPTAINER ORDEN DE COBRO',
                text: 'Problemas al generar Orden de Cobro ' + textStatus + '',
                type: 'error',
                confirmButtonColor: '#FE6A00',
                confirmButtonText: 'OK',
                confirmButtonClass: 'btn btn-info'
            });



            gfProceso();
        }
    });




    //  // datos.Fecha_Ingreso = $("#txtDescuento").val();
    //  // datos.Servicios_Logisticos = 
    // // datos.Monto_Servicios

    //   //   datos.ID_Usuario



});


function formatNumber(x) {
    if (isNaN(x)) return "";

    n = x.toString().split(',');
    return n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",") + (n.length > 1 ? "." + n[1] : "");
}

$('#CmbTipoCliente').change(function () {

    CargaComboClientes($('#CmbTipoCliente').val());

    //$('#CmbComunas').attr('disabled', false);

});

$('#CmbBasedeCobro').change(function () {

    var Id_Ruta;
    var ID_baseCobro;

    Id_Ruta = $('#CmbRutas').val();
    ID_baseCobro = $('#CmbBasedeCobro').val();

    $('#CmbTipoCarga').val(0);
    $('#CmbTipoCarga').selectpicker("refresh");

    $('#CmbUnidadCobro').val(0);
    $('#CmbUnidadCobro').selectpicker("refresh");

    $("#txtMontoTarifa").val('');
    $("#txtCantidad").val('');
    $("#txtDescuento").val('');
    $("#txtTotalTransporte").val('');

    Carga_Combo_TipoCarga(Id_Ruta, ID_baseCobro);



});

$('#CmbUnidadCobro').change(function () {

    var Id_Ruta;
    var ID_baseCobro;
    var ID_TipoCarga;
    var ID_UnidadCobro;
    var ID_Cliente;

    Id_Ruta = $('#CmbRutas').val();
    ID_baseCobro = $('#CmbBasedeCobro').val();
    ID_TipoCarga = $('#CmbTipoCarga').val();
    ID_UnidadCobro = $('#CmbUnidadCobro').val();
    ID_Cliente = $('#CmbClientes').val();



    Carga_Tarifa_Transporte(Id_Ruta, ID_baseCobro, ID_TipoCarga, ID_UnidadCobro, ID_Cliente);



});




function Carga_Tarifa_Transporte(ID_Ruta, ID_baseCobro, ID_TipoCarga, ID_UnidadCobro, ID_Cliente) {
    gfProceso();



    $.ajax({
        type: "POST",
        url: "/OrdenViaje/Consulta_TarifaTransporte_Detalle",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { ID_Ruta: ID_Ruta, ID_baseCobro: ID_baseCobro, ID_TipoCarga: ID_TipoCarga, ID_UnidadCobro: ID_UnidadCobro, ID_Cliente: ID_Cliente },

        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT

                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO
                
                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR 
                {
                    
                    detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE  

                    var TipodeCarga = $("#CmbTipoCarga option:selected").text();

                    if (TipodeCarga == "FRACCIONADA") {

                        var cantidad = 0;
                        var peso = 0;
                        var metros = 0;
                        var razonPesoM3 = detalle.Razon_Peso_Metro_Cubico;

                        $.each($('#tbodyOrdenesTransporte').find('tr'), function (indexTr, itemTr) {

                            var id = itemTr.id.split('-')[1];

                            if ($('#chk-' + id).prop("checked")) {

                                cantidad += parseFloat($('#trOTCantidad-' + id).html());
                                metros += parseFloat($('#trOTMetros-' + id).html());
                                peso += parseFloat($('#trOTPeso-' + id).html());
                            }
                        });
                        
                        if ((metros * razonPesoM3) > peso) {

                            $('#txtMontoTarifa').val(formateaNumero(detalle.Monto_Tarifa_M3));
                            $('#txtDescuento').val(detalle.Descuento);
                            $('#txtCantidad').val(metros);
                            $('#txtID_TarifaTransporte').val(detalle.ID_TarifaTransporte);

                            $('#txtCantidad').prop("disabled", "disabled");
                            $('#lblMontoTarifa').html('Tarifa M3');
                            $('#lblCantidad').html('M3');
                        }
                        else {

                            $('#txtMontoTarifa').val(detalle.Monto_Tarifa_Peso);
                            $('#txtDescuento').val(detalle.Descuento);
                            $('#txtCantidad').val(peso);
                            $('#txtID_TarifaTransporte').val(detalle.ID_TarifaTransporte);
                            $('#lblMontoTarifa').html('Tarifa Peso');
                            $('#lblCantidad').html('Peso');

                            $('#txtCantidad').prop("disabled", "disabled");
                        }

                    }
                    else {

                        $('#txtMontoTarifa').val(formateaNumero(detalle.Monto_Tarifa));
                        $('#txtDescuento').val(detalle.Descuento);
                        $('#txtCantidad').val(1);
                        $('#txtID_TarifaTransporte').val(detalle.ID_TarifaTransporte);
                        $('#lblMontoTarifa').html('Tarifa Normal');
                        $('#lblCantidad').html('Cantidad');

                        $('#txtCantidad').removeAttr("disabled");
                    }

                    CalcularTarifaTransporte();

                    gfProceso();

                }  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                else {
                    $("#MensajeError").text(data.Detalle_Error);
                    $("#ModalError").modal();
                }

            } catch (err) { // ACCIÓN DEL ERROR DE JAVASCRIPT
                alert(err);
            }



        },
        error: function (xhr, textStatus, errorThrown) { // ACCIÓN DEL ERROR DEL SERVICIO AJAX.
            alert(textStatus);

            gfProceso();
        }
    });


}


function findAndReplace(string, target, replacement) {

    var i = 0, length = string.length;

    for (i; i < length; i++) {

        string = string.replace(target, replacement);

    }

    return string;

}



function CalcularTarifaTransporte() {

    var TarifaNormal;
    var Cantidad;
    var Descuento;
    var TotalTransporte;
    var TotalIVA;
    var TotalServicio;



    TarifaNormal = $('#txtMontoTarifa').val();
    Descuento = $('#txtDescuento').val();

   

    Cantidad = findAndReplace($("#txtCantidad").val(), '.', '');
    Cantidad = limpiaNumero(Cantidad);


    TotalTransporte = Math.round((limpiaNumero(TarifaNormal) * Cantidad) - Descuento);
    TotalServicio = Math.round($("#TotalFooterServicios").val());

    TotalCobro = TotalTransporte + TotalServicio;
    TotalIVA = TotalCobro * 0.19;
    TotalConIva = parseInt(TotalCobro + TotalIVA);
    //console.log("total con iva: " + TotalConIva);
    //console.log(formateaNumero(TotalConIva));
   

    $('#txtTotalTransporte').val(formateaNumero(TotalTransporte));
    $('#MontoNeto').text("$ " +formateaNumero(TotalCobro));
    $('#MontoIVA').text("$ "+formateaNumero(TotalIVA));
    $('#MontoTotal').text("$ " + formateaNumero(TotalConIva));
}

$('#CmbTipoCarga').change(function () {

    var Id_Ruta;
    var ID_baseCobro;
    var ID_TipoCarga;

    Id_Ruta = $('#CmbRutas').val();
    ID_baseCobro = $('#CmbBasedeCobro').val();
    ID_TipoCarga = $('#CmbTipoCarga').val();

    Carga_Combo_UnidadCobro(Id_Ruta, ID_baseCobro, ID_TipoCarga);

});

$('#CmbRutas').change(function () {

    Buscar_Ordenes();


});

function formateaMedida(valorMedida)
{
    var medidaFormat = valorMedida.replace(",",".");
    medidaFormat = parseFloat(medidaFormat).toFixed(2);
    
    return medidaFormat;
}


function formateaMontos() {
    //Formatea los montos de la tabla de OT's
    //El formato por defecto es de 1234567,00
    $('#TblOrdenesTransporte > tbody > tr').each(function () {
        var monto = $(this).find(".monto_neto").html();
        var montoLimpio = monto.substring(0, monto.lastIndexOf(",")); //Se remueven todos los valores despues de la ","
        var formateado = formateaNumero(parseInt(montoLimpio)); //Se formatea el monto

        if (montoLimpio == 0 || montoLimpio == "") {
            $(this).find(".monto_neto").html("");
        } else {
            $(this).find(".monto_neto").html("$" + formateado); //Se asigna el valor formateado a la casilla
        }

    });

}

function Buscar_Ordenes() {

    var ID_Clientes;
    var ID_Ruta;
    var ID_Sucursal;
    var ID_TipoCliente;



    ID_Cliente = $("#CmbClientes").val();
    ID_Ruta = $("#CmbRutas").val();

    ID_TipoCliente = $("#CmbTipoCliente").val();

    gfProceso();



    $.ajax({
        type: "POST",
        url: "/OrdenViaje/ConsultaOrdenTransporte",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { ID_Cliente: ID_Cliente, ID_Ruta: ID_Ruta },

        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT


                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO




                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR 
                {
                    $("#tbodyOrdenesTransporte").html('');


                    detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE  
                    var Monto_OrdenTransporte = 0;

                    for (var i in detalle) {
                        m3Format = formateaMedida(detalle[i].total_MetroCubico);
                        pesoFormat = formateaMedida(detalle[i].total_Peso);

                        console.log(typeof (detalle[i].total_Peso));

                        StrTable = '<tr id="trOT-' + detalle[i].ID_Orden_Transporte + '">'; 
                        StrTable = StrTable + "<td class='text-center'>";
                        StrTable = StrTable + "<div class='checkbox'><label><input type='checkbox' id='chk-" + detalle[i].ID_Orden_Transporte + "' checked class='checkbox checkboxOTs' onchange='calculaMontoFinal()' /> </label></div></td>";

                        StrTable = StrTable + '<td class="text-center">' + detalle[i].ID_Orden_Transporte + '</td>';
                        StrTable = StrTable + '<td class="text-center">' + detalle[i].Razon_Social + '</td>';
                        StrTable = StrTable + '<td class="text-center"> ' + detalle[i].Descripcion_TipoCliente + '</td>';
                        
                        StrTable = StrTable + '<td id="trOTCantidad-' + detalle[i].ID_Orden_Transporte + '" class="text-center">' + detalle[i].CantidadBultos + '</td>';
                        StrTable = StrTable + '<td id="trOTMetros-' + detalle[i].ID_Orden_Transporte + '" class="text-center">' + m3Format + '</td>';
                        StrTable = StrTable + '<td id="trOTPeso-' + detalle[i].ID_Orden_Transporte + '" class="text-center">' + pesoFormat + '</td>';



                        StrTable = StrTable + '<td>';


                        var detalleGuias;

                        detalleGuias = jQuery.parseJSON(JSON.stringify(detalle[i].Guias));


                        for (var j in detalleGuias) {
                            StrTable = StrTable + '<a href=/guias/showUserPDF?IDGuias=' + detalleGuias[j].ID_Guia_Cliente + ' data-toggle="tooltip" title="' + detalleGuias[j].Numero_Guia + '" target="_blank"><i class="material-icons">description</i></a>';

                        }

                        StrTable = StrTable + '</td>';


                        StrTable = StrTable + '<td class="text-center">' + detalle[i].Proveedor + '</td>';
                        StrTable = StrTable + '<td class="text-center">' + detalle[i].Fecha_Ingreso + '</td>';

                        StrTable = StrTable + '<td class="text-center">' + detalle[i].Dias_Bodega + '</td>';
                        //  StrTable = StrTable + '<td class="text-center">' + detalle[i].DescripcionEstado + '</td>';
                        //     StrTable = StrTable + '<td>' + detalle[i].USUARIO + '</td>';



                        //   StrTable = StrTable + '<td class="td-actions text-right"><button type="button" rel="tooltip" class="btn btn-danger" onclick="del_Bulto(this);return false;"><i class="material-icons">close</i></button></td>'; // BUTON
                        StrTable = StrTable + '</tr>'; // CIERRE

                        //  $('#lblTituloSlot').text(detalle[0].DescripcionSlot);
                        Monto_OrdenTransporte = Monto_OrdenTransporte + parseFloat(detalle[i].Monto_Neto);

                        $('#tbodyOrdenesTransporte').html($('#tbodyOrdenesTransporte').html() + StrTable);
                        i++;
                    }

                    //formateaMedidasTabla("tbodyOrdenesTransporte");

                    $('.checkboxOTs').filter(":notmdproc").data("mdproc", !0).after("<span class='checkbox-material'><span class='check'></span></span>");


                    $('#TotalFooterOrden_Transporte').text(Monto_OrdenTransporte);
                    //  $('#txtTotalTransporte').val(Monto_OrdenTransporte);

                    // $('#txtMontoTarifa').val(Monto_OrdenTransporte);
                    //$('#txtCantidad').val(1);
                    //$('#txtDescuento').val(0);
                    //$('#txtID_TarifaTransporte').val(0);
                    //CalcularTarifaTransporte();

                    //$("#TablaCliente > tbody > tr:first-child").remove();
                    //$('#TablaCliente').append('<tr><td><p id="LblRazonSocial">' + detalle.Razon_Social + '</p></td><td>' + detalle.Rut_Cliente + '</td><td>' + detalle.Giro + '</td><td>' + detalle.Telefono + '</td><td>' + detalle.Email + '</td><td>' + detalle.Descripcion_TipoCliente + '</td></tr>');


                    //  $('#CmbBasedeCobro').val(1).change();
                    //  $('#CmbBasedeCobro').attr('disabled', 'disabled');

                    gfProceso();


                }  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                else {
                    swal({
                        title: 'TOPTAINER',
                        text: 'ERROR : ' + data.Detalle_Error,
                        type: 'warning',
                        confirmButtonColor: '#FE6A00',
                        confirmButtonText: 'OK',
                        confirmButtonClass: 'btn btn-info'
                    });
                    $("#TblOrdenesTransporte tr>td").remove();
                    gfProceso();
                }

            } catch (err) { // ACCIÓN DEL ERROR DE JAVASCRIPT
                swal({
                    title: 'TOPTAINER',
                    text: 'ERROR' + Data.Detalle_Error,
                    type: 'warning',
                    confirmButtonColor: '#FE6A00',
                    confirmButtonText: 'OK',
                    confirmButtonClass: 'btn btn-info'
                });
                gfProceso();
            }



        },
        error: function (xhr, textStatus, errorThrown) { // ACCIÓN DEL ERROR DEL SERVICIO AJAX.
            swal({
                title: 'TOPTAINER',
                text: 'ERROR DE CONEXIÓN' + textStatus,
                type: 'warning',
                confirmButtonColor: '#FE6A00',
                confirmButtonText: 'OK',
                confirmButtonClass: 'btn btn-info'
            });

            gfProceso();
        }
    });

}

function calculaMontoFinal() {
    var Id_Ruta;
    var ID_baseCobro;
    var ID_TipoCarga;
    var ID_UnidadCobro;
    var ID_Cliente;

    Id_Ruta = $('#CmbRutas').val();
    ID_baseCobro = $('#CmbBasedeCobro').val();
    ID_TipoCarga = $('#CmbTipoCarga').val();
    ID_UnidadCobro = $('#CmbUnidadCobro').val();
    ID_Cliente = $('#CmbClientes').val();



    return Carga_Tarifa_Transporte(Id_Ruta, ID_baseCobro, ID_TipoCarga, ID_UnidadCobro, ID_Cliente);
}

function CargaComboBasedeCobro() {

    gfProceso();


    $.ajax({
        type: "POST",
        url: "/Funciones/Combo_BasedeCobro",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: {},
        success: function (d) {

            var opt = '<option selected="selected" value="0">SELECCIONE BASE DE COBRO</option>';

            $.each(d, function (index, item) {
                var value = d[index].Value;
                var text = d[index].Text;

                opt = opt + '<option value="' + value + '">' + text + '</option>';
            });

            $('#CmbBasedeCobro').empty().append(opt);

            $('#CmbBasedeCobro').selectpicker('refresh');



            gfProceso();

        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
            gfProceso();
        }
    });

}







function CargaComboTarifaServicios() {

    gfProceso();


    $.ajax({
        type: "POST",
        url: "/OrdenViaje/Combo_TarifaServicios",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: {},
        success: function (d) {

            var opt = '<option selected="selected" value="0">SELECCIONE TARIFA</option>';

            $.each(d, function (index, item) {
                var value = d[index].Value;
                var text = d[index].Text;

                opt = opt + '<option value="' + value + '">' + text + '</option>';
            });

            $('#CmbServicios').empty().append(opt);

            $('#CmbServicios').selectpicker('refresh');

            gfProceso();

        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
            gfProceso();
        }
    });

}

function Carga_Combo_TipoCarga(ID_Ruta, ID_baseCobro) {


    gfProceso();



    $.ajax({
        type: "POST",
        url: "/Funciones/Carga_TarifaTransporte_TipoCarga",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: {ID_Ruta:  ID_Ruta ,ID_baseCobro: ID_baseCobro },
        success: function (d) {

            var opt = '<option selected="selected" value="0">Seleccione Tipo Carga</option>';

            $.each(d, function (index, item) {
                var value = d[index].Value;
                var text = d[index].Text;

                opt = opt + '<option value="' + value + '">' + text + '</option>';
            });


            $('#CmbTipoCarga').empty().append(opt);

            $('#CmbTipoCarga').selectpicker('refresh');

            gfProceso();

        },
        error: function (xhr, textStatus, errorThrown) {
            alert(errorThrown);
            gfProceso();
        }
    });

}

function Carga_Combo_UnidadCobro(ID_Ruta, ID_baseCobro, ID_TipoCarga) {


    gfProceso();



    $.ajax({
        type: "POST",
        url: "/Funciones/Carga_TarifaTransporte_UnidadCobro",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { ID_Ruta: ID_Ruta, ID_baseCobro: ID_baseCobro, ID_TipoCarga: ID_TipoCarga },
        success: function (d) {

            var opt = '<option selected="selected" value="0">Seleccione Unidad Cobro</option>';

            $.each(d, function (index, item) {
                var value = d[index].Value;
                var text = d[index].Text;

                opt = opt + '<option value="' + value + '">' + text + '</option>';
            });


            $('#CmbUnidadCobro').empty().append(opt);

            $('#CmbUnidadCobro').selectpicker('refresh');

            gfProceso();

        },
        error: function (xhr, textStatus, errorThrown) {
            alert(errorThrown);
            gfProceso();
        }
    });

}



function formatNumber(x) {
    if (isNaN(x)) return "";

    n = x.toString().split(',');
    return n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",") + (n.length > 1 ? "." + n[1] : "");
}



function ValidaServicios() {


    if ($('#chkServicios').prop('checked')) {
        $("#lblServicios").text("Servicios Activos");
    }
    else {

        $("#lblServicios").text("Sin Servicios Activos");
    }
}



$('#CmbServicios').change(function () {


    var ID_Servicio;

    ID_Servicio = $("#CmbServicios").val();

    gfProceso();


    $.ajax({
        type: "POST",
        url: "/Mantenedor/ConsultaDetalleTarifaServicio",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { ID_Servicio: ID_Servicio },


        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT




                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO

                //   alert(JSON.stringify(detalle));


                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR
                {


                    detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE

                    var tarifa = $("#TxtTarifaServicio").val();
                    var cantidad = $("#TxtCantidadServicio").val();
                    var total = Math.round(tarifa * cantidad);

                    console.log(typeof (total));
                    console.log(detalle);
                    $("#TxtUnidadCobroServicio").val(detalle.DescripcionUnidad);

                    $("#TxtTarifaServicio").val(detalle.Monto_Tarifa);
                    $("#TxtCantidadServicio").val(1);
                    $("#TxtTotalServicio").val(detalle.Monto_Tarifa*1);
                    //$("#txtMontoTarifa").val(detalle.Monto_Tarifa);
                    //$("#CmbVigencia").val(detalle.Vigencia).change();

                    //////  $("#Btn_Modificar").attr("disabled", true);

                    //$("#Btn_Modificar").removeAttr("disabled");
                    //$("#Btn_Guardar").attr("disabled", true);

                    gfProceso();


                }  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                else {



                    swal({
                        title: 'TOPTAINER',
                        text: 'Error al buscar información de Tarifa de Servicio ' + data.Detalle_Error + '',
                        type: 'error',
                        confirmButtonColor: '#FE6A00',
                        confirmButtonText: 'OK',
                        confirmButtonClass: 'btn btn-info'
                    });


                    gfProceso();

                    return false;
                }

            } catch (err) { // ACCIÓN DEL ERROR DE JAVASCRIPT


                swal({
                    title: 'TOPTAINER',
                    text: 'Error al buscar información de Tarifa de Servicio' + data.Detalle_Error + '',
                    type: 'error',
                    confirmButtonColor: '#FE6A00',
                    confirmButtonText: 'OK',
                    confirmButtonClass: 'btn btn-info'
                });
                gfProceso();


                return false;
            }



        },
        error: function (xhr, textStatus, errorThrown) { // ACCIÓN DEL ERROR DEL SERVICIO AJAX.
            swal({
                title: 'TOPTAINER',
                text: 'Error de Conexión' + textStatus + '',
                type: 'error',
                confirmButtonColor: '#FE6A00',
                confirmButtonText: 'OK',
                confirmButtonClass: 'btn btn-info'
            });

            gfProceso();
            return false;
        }
    });

});


$('#btnAgregarServicio').click(function () {

    var StrTable;
    var ID_Servicio;
    var DescripcionServicio;
    var TarifaServicio;
    var UnidadCobroServicio;
    var CantidadServicio;
    var TotalServicio;
    


    ID_Servicio = $('#CmbServicios').val();
    DescripcionServicio = $('#CmbServicios option:selected').text();
    UnidadCobroServicio = $('#TxtUnidadCobroServicio').val();
    TarifaServicio = $('#TxtTarifaServicio').val();
    CantidadServicio = $('#TxtCantidadServicio').val();
    TotalServicio = $('#TxtTotalServicio').val();

    StrTable = '<tr>'; // INICIO
    StrTable = StrTable + '<td class="text-center">' + ID_Servicio + '</td>'; // EMBALAJE
    StrTable = StrTable + '<td class="text-center">' + DescripcionServicio + '</td>'; // EMBALAJE
    StrTable = StrTable + '<td class="text-center">' + UnidadCobroServicio + '</td>'; // EMBALAJE
    StrTable = StrTable + '<td class="text-right">' + TarifaServicio + '</td>'; // EMBALAJE

    StrTable = StrTable + '<td class="text-right">' + CantidadServicio + '</td>'; // EMBALAJE
    StrTable = StrTable + '<td class="text-right">' + TotalServicio + '</td>'; // EMBALAJE
    StrTable = StrTable + '<td class="td-actions text-right"><button type="button" rel="tooltip" class="btn btn-danger" onclick="del_Guia(this);return false;"><i class="material-icons">close</i></button></td>'; // BUTON
    StrTable = StrTable + '</tr>'; // CIERRE


    $('#TblOrdenesServicios tr').last().after(StrTable);
    CalcularMonto();

    $("#TxtTarifaServicio").val(0);
    $("#TxtCantidadServicio").val(0);
    $("#TxtTotalServicio").val(0);
    $("#TxtUnidadCobroServicio").val('');
    $('#CmbServicios').val(0);
    $('#CmbServicios').selectpicker("refresh");



});

function del_Guia(remtr) {
    while ((remtr.nodeName.toLowerCase()) != 'tr')
        remtr = remtr.parentNode;

    remtr.parentNode.removeChild(remtr);
    CalcularMonto();
    // alert($('#TablaBulto >tbody >tr').length);
}

function calcularTarifaFinal()
{
    var montoServicios = limpiaNumero($("#TotalFooterServicios").text()); 
    var netoAnterior = limpiaNumero($("#MontoNeto").text());
    var montoTransporte = limpiaNumero($("#txtTotalTransporte").val());

    //var totalAnterior = limpiaNumero($("#MontoTotal").text());

    var nuevoNeto = montoServicios + montoTransporte;
    var nuevoIVA = nuevoNeto * 0.19;
    var nuevoTotal = nuevoNeto + nuevoIVA;


    $('#MontoNeto').text("$ " + formateaNumero(nuevoNeto));
    $('#MontoIVA').text("$ " + formateaNumero(nuevoIVA));
    $('#MontoTotal').text("$ " + formateaNumero(nuevoTotal));
    //console.log("tipo totalActual: " + typeof (totalActual));
    //console.log("totalActual "+totalActual);
    

    //console.log("tipo totalLimpio: " + typeof (totalLimpio));

    //console.log("total sin formato: " + MontoTotal);
    //console.log("total limpio: " + totalLimpio);
}

function CalcularMonto() {

    var MontoNeto = 0;
    var indice = 0;

    $('#TblOrdenesServicios >tbody >tr').each(function () {

        if (indice > 0) {
            MontoNeto = MontoNeto + parseFloat($(this).find("td").eq(5).html());
        }
        indice++;
    });

    //$('#MontoNeto').text(formatNumber(MontoNeto.toFixed(0)));
    //$('#MontoIVA').text(formatNumber((MontoNeto * 0.19).toFixed(0)));
    //$('#MontoTotal').text(formatNumber((MontoNeto * 1.19).toFixed(0)));

    $('#TotalFooterServicios').text(formateaNumero(MontoNeto));
    calcularTarifaFinal();

}


//$('#TxtCantidadServicio').change(function () {

//    var TotalServicio;


//    TotalServicio = Math.round($("#TxtTarifaServicio").val() * $("#TxtCantidadServicio").val());

//    $("#TxtTotalServicio").val(TotalServicio);


//});

$("#TxtCantidadServicio").keyup(function () {

    var tarifa = $("#TxtTarifaServicio").val();
    var cantidad = $("#TxtCantidadServicio").val();
    var total = tarifa * cantidad;

    if (cantidad == 0 || cantidad == "") {
        $("#TxtTotalServicio").val(0);
    }

    $("#TxtTotalServicio").val(parseInt(total));

    //console.log(cantidad);
    //console.log(total);
    //console.log(typeof (cantidad));
    //console.log(typeof (total));
});
