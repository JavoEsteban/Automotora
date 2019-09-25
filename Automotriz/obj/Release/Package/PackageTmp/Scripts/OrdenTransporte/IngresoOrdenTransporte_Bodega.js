

// CODIGO PARA PARTIR LA PAGINA WEB
var Indice_bulto = 0;
$(document).ready(function () {
   


    $(function () {
        $('#txtFechaRecepcion').datetimepicker({

            format: 'DD/MM/YYYY',
            ////locale: 'es'
            //format: 'YYYY-MM-DD HH:mm'
        });
    });


     
    
        var Indice = 0;
      
        CargaComboRutas();
        CargaComboSucursal();
        CargaComboBasedeCobro();
        CargaComboTipoCliente();
        CargaComboEmbalaje();
        CargaComboMedioPago();


        $('#txtCantidad').autoNumeric('init', { aSep: ',', aDec: '.', aSign: '' });
        $('#txtPeso').autoNumeric('init', { aSep: ',', aDec: '.', aSign: '' });
        $('#txtM3').autoNumeric('init', { aSep: ',', aDec: '.', aSign: '' });
        $('#txtGuias').autoNumeric('init', { vMin: '0', vMax: '9999999999', aSep: '' });
        $('#MontoTotal').autoNumeric('init', { vMin: '0', vMax: '9999999999', aSep: '' });


        $('#divTipoCarga').hide();
        $('#divTipoBulto').hide();


      
        




        $('.datetimepicker').datetimepicker({
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-chevron-up",
                down: "fa fa-chevron-down",
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-screenshot',
                clear: 'fa fa-trash',
                close: 'fa fa-remove'
            }
        });

        var Id_Sucursal;



        Id_Sucursal = $("#Id_Sucursal").val();

       // alert(Id_Sucursal);
        $("#CmbSucursalOrigen").val(Id_Sucursal).change();
       
       
    });


// FUNCIONES SIMPLE SIN LOGICA DE NEGOCIOS
function CargaComboRutas() {

    gfProceso();


    $.ajax({
        type: "POST",
        url: "/Funciones/Combo_Rutas",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: {},
        success: function (d) {

            var opt = '<option selected="selected" value="0">SELECCIONE RUTA</option>';

            $.each(d, function (index, item) {
                var value = d[index].Value;
                var text = d[index].Text;

                opt = opt + '<option value="' + value + '">' + text + '</option>';
            });

            $('#CmbRutas').empty().append(opt);

            $('#CmbRutas').selectpicker('refresh');

            gfProceso();

        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
            gfProceso();
        }
    });

}

function CargaComboSucursal() {

    gfProceso();


    $.ajax({
        type: "POST",
        url: "/Funciones/Combo_Sucursales",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: {},
        success: function (d) {

            var opt = '<option value="999">SELECCIONE SUCURSAL</option>';

            $.each(d, function (index, item) {
                var value = d[index].Value;
                var text = d[index].Text;
                var Id_Sucursal = $("#Id_Sucursal").val();

                if (value == Id_Sucursal) {
                    opt = opt + '<option selected="selected"  value="' + value + '">' + text + '</option>';
                }
                else {
                    opt = opt + '<option value="' + value + '">' + text + '</option>';
                }
                });

            $('#CmbSucursalDestino').empty().append(opt);

            $('#CmbSucursalDestino').selectpicker('refresh');

            $('#CmbSucursalOrigen').empty().append(opt);

            $('#CmbSucursalOrigen').selectpicker('refresh');

           
            


          


            gfProceso();

        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
            gfProceso();
        }
    });

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

function CargaComboTipoCliente() {



    gfProceso();



    $.ajax({
        type: "POST",
        url: "/Funciones/Combo_TipoCliente",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: {},
        success: function (d) {

            var opt = '<option selected="selected" value="0">SELECCIONE TIPO CLIENTE</option>';

            $.each(d, function (index, item) {
                var value = d[index].Value;
                var text = d[index].Text;

                opt = opt + '<option value="' + value + '">' + text + '</option>';
            });


            $('#CmbTipoCliente').empty().append(opt);

            $('#CmbTipoCliente').selectpicker('refresh');

            gfProceso();

        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
            gfProceso();
        }
    });

}

function CargaComboMedioPago() {



    gfProceso();



    $.ajax({
        type: "POST",
        url: "/Funciones/Combo_MediodePago",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: {},
        success: function (d) {

            var opt = '<option selected="selected" value="0">SELECCIONE MEDIO DE PAGO</option>';

            $.each(d, function (index, item) {
                var value = d[index].Value;
                var text = d[index].Text;

                opt = opt + '<option value="' + value + '">' + text + '</option>';
            });


            $('#CmbModoPago').empty().append(opt);

            $('#CmbModoPago').selectpicker('refresh');

            gfProceso();

        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error Combo Medio de Pago');
            gfProceso();
        }
    });

}
function CargaComboEmbalaje() {



    gfProceso();



    $.ajax({
        type: "POST",
        url: "/Funciones/Combo_Embalaje",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: {},
        success: function (d) {

            var opt = '<option selected="selected" value="0">SELECCIONE TIPO EMBALAJE</option>';

            $.each(d, function (index, item) {
                var value = d[index].Value;
                var text = d[index].Text;

                opt = opt + '<option value="' + value + '">' + text + '</option>';
            });


            $('#CmbTipoEmbalaje').empty().append(opt);

            $('#CmbTipoEmbalaje').selectpicker('refresh');

            gfProceso();

        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
            gfProceso();
        }
    });

}
// FUNCIONES SIMPLE CON LOGICA DE NEGOCIO Y VALIDACION
function CargaComboClientes(TipoCliente) {



    gfProceso();



    $.ajax({
        type: "POST",
        url: "/Funciones/Combo_Clientes",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { TipoCliente: TipoCliente },
        success: function (d) {

            var opt = '<option selected="selected" value="0">SELECCIONE  CLIENTE</option>';

            $.each(d, function (index, item) {
                var value = d[index].Value;
                var text = d[index].Text;

                opt = opt + '<option value="' + value + '">' + text + '</option>';
            });


            $('#CmbClientes').empty().append(opt);

            $('#CmbClientes').selectpicker('refresh');

            gfProceso();

        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
            gfProceso();
        }
    });

}
function SeleccionCliente()
{
        
    if ($("#CmbClientes").val() == 0 || $("#CmbClientes").val() == null) {

        alert(' DEBE SELECCIONAR CLIENTE')
        return false;
    }

    else {


        if ($("#CmbTipoCliente").val() == '2')
        { $('#DivGuias').hide(); }
        else
        { $('#DivGuias').show(); }



            
        $('#myModal').modal('hide');

        $('#myModal').hide();

         
    }

       
    gfProceso();

    $.ajax({
        type: "POST",
        url: "/Funciones/Busca_Cliente",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { IdCliente: $("#CmbClientes").val() },
        
        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT
                     
                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO

                detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE  
                   


                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR 
                {

                    $("#TablaCliente > tbody > tr:first-child").remove();
                    $('#TablaCliente').append('<tr><td><p id="LblRazonSocial">' + detalle.Razon_Social + '</p></td><td>' + detalle.Rut_Cliente + '</td><td>' + detalle.Giro + '</td><td>' + detalle.Telefono + '</td><td>' + detalle.Email + '</td><td>' + detalle.Descripcion_TipoCliente + '</td></tr>');


                    //  $('#CmbBasedeCobro').val(1).change();
                    //  $('#CmbBasedeCobro').attr('disabled', 'disabled');

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
            alert('Error!!');
               
            gfProceso();
        }
    });

}

function ClientePersona()
{
   // alert("PERSONA");
  //  $('#CmbBasedeCobro').val(1).change();
  //  $('#CmbBasedeCobro').attr('disabled', 'disabled');
    $('#divTipoCarga').hide();
    $('#divTipoBulto').show();
   
  
    
   
}
function ClienteEmpresa()
{   // alert("EMPRESA");
  //  $('#CmbBasedeCobro').val(0).change();
  //  $('#CmbBasedeCobro').attr('disabled', false);
    $('#divTipoCarga').show();
    $('#divTipoBulto').hide();
   
       
}


function Carga_Combo_TipoCarga(ID_Ruta, ID_baseCobro) {


  

    gfProceso();

    $.ajax({
        type: "POST",
        url: "/Funciones/Carga_TarifaTransporte_TipoCarga",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { ID_Ruta: $("#CmbRutas").val(), ID_BaseCobro: $("#CmbBasedeCobro").val() },

        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT

                           

                var opt = '<option selected="selected" value="0">SELECCIONE TIPO CARGA</option>';

                $.each(response, function (index, item) {
                    var value = response[index].Value;
                    var text = response[index].Text;

                    opt = opt + '<option value="' + value + '">' + text + '</option>';
                });


                                $('#CmbTipoCarga').empty().append(opt);
                                $('#CmbTipoCarga').selectpicker('refresh');

                                $('#CmbTipoCargaFraccion').empty().append(opt);
                                $('#CmbTipoCargaFraccion').selectpicker('refresh');

                gfProceso();


                //data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO

                //detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE  



                //if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR 
                //{

                //    $("#TablaCliente > tbody > tr:first-child").remove();
                //    $('#TablaCliente').append('<tr><td><p id="LblRazonSocial">' + detalle.Razon_Social + '</p></td><td>' + detalle.Rut_Cliente + '</td><td>' + detalle.Giro + '</td><td>' + detalle.Telefono + '</td><td>' + detalle.Email + '</td><td>' + detalle.Descripcion_TipoCliente + '</td></tr>');
                //    gfProceso();


                //}  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                //else {
                //    $("#MensajeError").text(data.Detalle_Error);
                //    $("#ModalError").modal();
                //}

            } catch (err) { // ACCIÓN DEL ERROR DE JAVASCRIPT
                alert(err);
            }



        },
        error: function (xhr, textStatus, errorThrown) { // ACCIÓN DEL ERROR DEL SERVICIO AJAX.
            alert('Error de Conexión al Cargar los Tipos de Cobro, Favor Revisar');

            gfProceso();
        }
    });

        

}


// FUNCIONES BOTONES

$('#Btn_SeleccionarCliente').click(function () {

    $('#myModal').modal('show');

   

});


function Imprimir_Recibo() {

   
    var html = "/OrdenTransporte/ImprimirRecibo"
    window.open(html, 'popup_window2', 'width=/700, height=600, left=0, top=0,scrollbars=1');


}

function Imprimir_Ticket() {

    var html = "/OrdenTransporte/ImprimirTickets"
    window.open(html, 'popup_window2', 'width=/700, height=600, left=0, top=0,scrollbars=1');


}


function CerrarModal() {


   
    $('#Modalexitoso').modal('hide');
    $('#Modalexitoso').hide();

}





$('#Btn_RecepcionCarga').click(function () {
    //$('#TablaBulto >tbody >tr').each(function () {

    //    var Embalaje = $(this).find("td").eq(0).html();


    //});

    

    var Total_Bultos = new Array();
    

    var indice = 0;
    // DATOS DE LA TABLA DE BULTOS

    $('#TablaBulto >tbody >tr').each(function () {

        if (indice > 0) {

            var Bultos = new Object();
            Bultos.ID_TarifaTransporte = $(this).find("td").eq(0).html();
            Bultos.ID_Base_Cobro       = $(this).find("td").eq(1).html();
            Bultos.Cantidad            = parseFloat($(this).find("td").eq(2).html());
            Bultos.DescripcionTipoCarga = $(this).find("td").eq(3).html();
            Bultos.DescripcionEmbalaje = $(this).find("td").eq(4).html();

            Bultos.Peso = parseFloat($(this).find("td").eq(5).html());
            Bultos.Metro_Cubicos = parseFloat($(this).find("td").eq(6).html());
            Bultos.PrecioUnitario = parseFloat($(this).find("td").eq(7).html());
            Bultos.Descuento = parseFloat($(this).find("td").eq(8).html());
            Bultos.Total = parseFloat($(this).find("td").eq(9).html());

            Bultos.ID_EstadoOrdenTransporte = '1';
            Total_Bultos.push(Bultos);
        }



        indice++;
    });



    // FIN TABLA

    var TotalGuias = new Array();
    

    indice = 0;
    // DATOS DE LA TABLA DE BULTOS

    $('#TablaGuia >tbody >tr').each(function () {

        if (indice > 0) {

            var Guia = new Object();
            Guia.Numero_Guia = parseFloat($(this).find("td").eq(0).html());
         

            Guia.ID_Clientes = $("#CmbClientes").val();
            TotalGuias.push(Guia);
        }



        indice++;
    });


     var datos = new Object();

            datos.Id_Ruta = $("#CmbRutas").val();
            datos.ID_Clientes = $("#CmbClientes").val();
    
            datos.ID_TipoCliente = $("#CmbTipoCliente").val();
            datos.ID_EstadoOrdenTransporte = '1' // ESTADO CREACIÓN
           


            datos.ID_Base_Cobro = $("#CmbBasedeCobro").val();
            datos.ID_Usuario   = '' // USER CONECTADO
            datos.Fecha_Ingreso = $("#txtFechaRecepcion").val();

            datos.ID_Sucursal_Recepcion = $("#CmbSucursalOrigen").val();

            datos.Sucursal_Recepcion = $("#CmbSucursalOrigen").val();
            datos.Observacion_Recepcion = $("#txtNotaRecepcion").val();
     
            datos.ID_Sucursal_Destino = $("#CmbSucursalDestino").val();
            datos.Sucursal_Destino = $("#CmbSucursalDestino").val();
            datos.Observacion_Destino = $("#txtNotaDestino").val();

            datos.Remitente = $("#txtRemitente").val();
            datos.Proveedor = $("#txtProveedor").val();

            datos.CantidadBultos = $('#TablaBulto >tbody >tr').length - 1;
   
            datos.Monto_Neto = $("#MontoNeto").text();
            datos.Monto_Iva = $("#MontoIVA").text();
            datos.Monto_Total = $("#MontoTotal").text();

            datos.ID_MododePago = $("#CmbModoPago").val();
            

    gfProceso();


    $.ajax({
        type: "POST",
        url: "/OrdenTransporte/GuardaOrdenTransporte",
        content: "application/json; charset=utf-8",
        dataType: "json",
        //   data: { Id_Cliente: Cliente, Id_Ruta: Ruta, Id_BaseCobro: BaseCobro, Id_TipoCarga: TipoCarga },
        data: { OrdenTransporte: datos, Bultos: Total_Bultos, Guias : TotalGuias},

        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT




                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO
             

                

                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR 
                {
                    detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE  
                    //swal({
                    //    title: 'ORDEN DE TRANSPORTE',
                    //    text: 'Se genero Orden de TRansporte N°' + data.Descripcion + '',
                    //    type: 'success',
                    //    confirmButtonColor: '#FE6A00',
                    //    confirmButtonText: 'OK',
                    //    confirmButtonClass: 'btn btn-info'
                    //});
                    $('#lbl_IdOrdenTransporte').text(detalle);
                    
                    $('#Modalexitoso').modal({ backdrop: 'static', keyboard: false })

                   
                    gfProceso();


                }  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                else {

                   

                    swal({
                        title: 'ORDEN DE TRANSPORTE',
                        text: 'Error al grabar Orden de Transporte ' + data.Detalle_Error  + '',
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
                    title: 'ORDEN DE TRANSPORTE',
                    text: 'Error al grabar Orden de Transporte ' + err + '',
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
            alert('Error de Conexión al guardar la Orden de TRanspoete, Favor Revisar');

            gfProceso();
            return false;
        }
    });

    //



});


$('#Btn_AgregarGuia').click(function () {

    var StrTable;
   

    N_GUIA = $('#txtGuias').val();
   


    StrTable = '<tr>'; // INICIO
    StrTable = StrTable + '<td class="text-center">' + N_GUIA + '</td>'; // EMBALAJE
    StrTable = StrTable + '<td class="td-actions text-right"><button type="button" rel="tooltip" class="btn btn-danger" onclick="del_Guia(this);return false;"><i class="material-icons">close</i></button></td>'; // BUTON
    StrTable = StrTable + '</tr>'; // CIERRE


    $('#TablaGuia tr').last().after(StrTable);

});

function formatNumber(x) {
    if (isNaN(x)) return "";

    n = x.toString().split(',');
    return n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",") + (n.length > 1 ? "." + n[1] : "");
}

$('#Btn_AgregarBulto').click(function () {


    
  

    var ID_TarifaTransporte = "Hola Mundo";
    var TipoEmbalaje = "Hola Mundo";
    var Peso = "Hola Mundo";
    var M3 = "0.0";
    
    StrTable = '<tr>'; // INICIO
 
    //Indice_bulto = Indice_bulto + 1;


    //StrTable = StrTable + '<td class="text-center">' + Indice_bulto + '</td>'; // EMBALAJE
    StrTable = StrTable + '<td class="text-center">' + TipoEmbalaje + '</td>'; // EMBALAJE
    StrTable = StrTable + '<td class="text-center">' + Peso + '</td>'; // EMBALAJE
    StrTable = StrTable + '<td class="text-center">' + M3 + '</td>'; // EMBALAJE
   
    StrTable = StrTable + '<td class="td-actions text-right"><button type="button" rel="tooltip" class="btn btn-danger" onclick="del_Bulto(this);return false;"><i class="material-icons">close</i></button></td>'; // BUTON
    StrTable = StrTable + '</tr>'; // CIERRE


    $('#TablaBulto tr').last().after(StrTable);

});

function CalcularMonto()
{

    var MontoNeto = 0;
    var indice = 0;

    $('#TablaBulto >tbody >tr').each(function () {

        if (indice > 0) {
            MontoNeto = MontoNeto + parseFloat($(this).find("td").eq(9).html());
        }
        indice++;
    });

    //$('#MontoNeto').text(formatNumber(MontoNeto.toFixed(0)));
    //$('#MontoIVA').text(formatNumber((MontoNeto * 0.19).toFixed(0)));
    //$('#MontoTotal').text(formatNumber((MontoNeto * 1.19).toFixed(0)));

    $('#MontoNeto').text(MontoNeto);
    $('#MontoIVA').text((MontoNeto * 0.19).toFixed(0));
    $('#MontoTotal').text((MontoNeto * 1.19).toFixed(0));

}


$('#CmbTipoCliente').change(function () {

    CargaComboClientes($('#CmbTipoCliente').val());
    //  Carga_Combo_TipoCarga(10, 2);
    //$('#CmbComunas').attr('disabled', false);

});

$('#CmbBasedeCobro').change(function () {


   // alert($('#CmbBasedeCobro').val());

    if ($('#CmbBasedeCobro').val() == '0') {
        return false;
    }
    


    if ($('#CmbBasedeCobro').val() == '1') {
        $('#divTipoCarga').hide();
        $('#divTipoBulto').show();
    }
    else {

        $('#divTipoCarga').show();
        $('#divTipoBulto').hide();



    }


  // rt('Cambio Ruta');
   Carga_Combo_TipoCarga($('#CmbRutas').val(), $('#CmbBasedeCobro').val());


});


$('#CmbRutas').change(function () {

   // var TipoCliente;

   // TipoCliente = $('#CmbTipoCliente').val();

  
   //// MARCA para identificar tipo de Cliente
   // if (TipoCliente == '1')
   // { ClientePersona(); }

   // else
   // { ClienteEmpresa(); }


});



//// FUNCION PARA ELIMINAR TR DE LA TABLE !
                function del_Bulto(remtr)
                            {
                                while((remtr.nodeName.toLowerCase())!='tr')
                                    remtr = remtr.parentNode;

                                remtr.parentNode.removeChild(remtr);
                                CalcularMonto();
                               // alert($('#TablaBulto >tbody >tr').length);
                }
                //// FUNCION PARA ELIMINAR TR DE LA TABLE !


            function del_Guia(remtr) {
                while ((remtr.nodeName.toLowerCase()) != 'tr')
                    remtr = remtr.parentNode;

                remtr.parentNode.removeChild(remtr);
  
                // alert($('#TablaBulto >tbody >tr').length);
            }



function del_id(id)
{
    del_tr(document.getElementById(id));
}

function Guias_pendiente()
    {
    var html = "/Guias/GuiasPendientes"
    $(location).attr('href', html);
    ///console.log("probando msg");

    }



